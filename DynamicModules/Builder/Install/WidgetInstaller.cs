// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.WidgetInstaller
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  /// <summary>
  /// This class provides functionality for installing widgets for dynamic modules.
  /// </summary>
  internal class WidgetInstaller
  {
    private static readonly PluralsResolver PluralsNameResolver = PluralsResolver.Instance;
    private PageManager pageManager;
    private ModuleBuilderManager moduleBuilderManager;
    private WidgetTemplateInstaller widgetTemplateInstaller;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Install.WidgetInstaller" /> class.
    /// </summary>
    /// <param name="pageManager">The page manager.</param>
    /// <param name="moduleBuilderManager">The module builder manager.</param>
    public WidgetInstaller(PageManager pageManager, ModuleBuilderManager moduleBuilderManager)
    {
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager));
      if (moduleBuilderManager == null)
        throw new ArgumentNullException(nameof (moduleBuilderManager));
      this.pageManager = pageManager;
      this.moduleBuilderManager = moduleBuilderManager;
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.DynamicModules.Builder.Install.WidgetInstaller.WidgetTemplateInstaller" />
    /// </summary>
    internal WidgetTemplateInstaller WidgetTemplateInstaller
    {
      get
      {
        if (this.widgetTemplateInstaller == null)
          this.widgetTemplateInstaller = new WidgetTemplateInstaller(this.pageManager, this.moduleBuilderManager);
        return this.widgetTemplateInstaller;
      }
    }

    /// <summary>Installs the widgets for the specified module.</summary>
    /// <param name="module">The module.</param>
    /// <param name="configureForDefaultSite">if set to <c>true</c> [configure for default site].</param>
    public void Install(DynamicModule module, bool configureForDefaultSite = true)
    {
      this.CreateModuleToolboxSection(module);
      foreach (DynamicModuleType type in module.Types)
        this.Install(module, type, configureForDefaultSite: configureForDefaultSite);
    }

    /// <summary>Installs the widgets for the specified module type.</summary>
    /// <param name="dynamicModule">The module.</param>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="toolboxesConfig">The toolboxes configuration.</param>
    /// <param name="configureForDefaultSite">if set to <c>true</c> [configure for default site].</param>
    public void Install(
      DynamicModule dynamicModule,
      DynamicModuleType moduleType,
      ToolboxesConfig toolboxesConfig = null,
      bool configureForDefaultSite = true)
    {
      ConfigManager manager = ConfigManager.GetManager();
      if (toolboxesConfig == null)
        toolboxesConfig = manager.GetSection<ToolboxesConfig>();
      ToolboxSection moduleToolboxSection = this.GetModuleToolboxSection(dynamicModule, toolboxesConfig);
      Guid defaultMasterTemplateId = Guid.Empty;
      Guid defaultDetailTemplateId = Guid.Empty;
      this.GetTemplatesIDs(dynamicModule, moduleType, out defaultMasterTemplateId, out defaultDetailTemplateId);
      if (defaultMasterTemplateId == Guid.Empty || defaultDetailTemplateId == Guid.Empty)
        this.RegisterTemplates(dynamicModule, moduleType, out defaultMasterTemplateId, out defaultDetailTemplateId);
      if (configureForDefaultSite)
        this.ConfigureForDefaultSite(defaultMasterTemplateId, defaultDetailTemplateId);
      this.CreateOrUpdateToolboxItem(dynamicModule, moduleType, moduleToolboxSection, defaultMasterTemplateId, defaultDetailTemplateId);
      if (moduleType.ParentModuleTypeId != Guid.Empty)
      {
        DynamicModuleType parentType = moduleType.ParentModuleType;
        ToolboxItem toolboxItem = moduleToolboxSection.Tools.FirstOrDefault<ToolboxItem>((Func<ToolboxItem, bool>) (e => e.Name == parentType.GetFullTypeName()));
        if (toolboxItem != null)
          WidgetInstaller.SetHideListViewOnChildDetailsViewParam(toolboxItem.Parameters, true);
      }
      manager.SaveSection((ConfigSection) toolboxesConfig);
      this.ValidateToolboxItemTemplateKeys(dynamicModule, moduleType, defaultMasterTemplateId, defaultDetailTemplateId);
    }

    /// <summary>
    /// Validates master and detail template keys for all toolbox items for specified moduleType
    /// </summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="defaultMasterTemplateId">The default master template id.</param>
    /// <param name="defaultDetailTemplateId">The default detail template id.</param>
    public void ValidateToolboxItemTemplateKeys(
      DynamicModule dynamicModule,
      DynamicModuleType moduleType,
      Guid defaultMasterTemplateId = default (Guid),
      Guid defaultDetailTemplateId = default (Guid))
    {
      ConfigManager manager = ConfigManager.GetManager();
      ToolboxesConfig section1 = manager.GetSection<ToolboxesConfig>();
      Toolbox toolbox = section1.Toolboxes["PageControls"];
      if (defaultMasterTemplateId == Guid.Empty && defaultDetailTemplateId == Guid.Empty)
      {
        this.GetTemplatesIDs(dynamicModule, moduleType, out defaultMasterTemplateId, out defaultDetailTemplateId);
        if (defaultMasterTemplateId == Guid.Empty && defaultDetailTemplateId == Guid.Empty)
          this.RegisterTemplates(dynamicModule, moduleType, out defaultMasterTemplateId, out defaultDetailTemplateId);
      }
      foreach (ToolboxSection section2 in toolbox.Sections)
      {
        foreach (ToolboxItem toolboxItem in section2.Tools.Elements.Where<ToolboxItem>((Func<ToolboxItem, bool>) (e => e.Name == moduleType.GetFullTypeName())).ToList<ToolboxItem>())
          this.ValidateToolboxItemTemplateKeys(toolboxItem, defaultMasterTemplateId, defaultDetailTemplateId, dynamicModule, moduleType);
      }
      manager.SaveSection((ConfigSection) section1);
    }

    /// <summary>
    /// Validates that default master and detail template keys are correct. If they are not, defaults are set.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="defaultMasterTemplateId">The default master template id.</param>
    /// <param name="defaultDetailTemplateId">The default detail template id.</param>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="moduleType">Type of the module.</param>
    public void ValidateToolboxItemTemplateKeys(
      ToolboxItem item,
      Guid defaultMasterTemplateId,
      Guid defaultDetailTemplateId,
      DynamicModule dynamicModule,
      DynamicModuleType moduleType)
    {
      string parameter1 = item.Parameters["DefaultMasterTemplateKey"];
      string parameter2 = item.Parameters["DefaultDetailTemplateKey"];
      if (!string.IsNullOrEmpty(parameter1) && !this.widgetTemplateInstaller.WidgetTemplateExists(new Guid(parameter1), typeof (DynamicContentViewMaster).FullName, dynamicModule, moduleType))
        item.Parameters["DefaultMasterTemplateKey"] = defaultMasterTemplateId.ToString();
      if (string.IsNullOrEmpty(parameter2) || this.widgetTemplateInstaller.WidgetTemplateExists(new Guid(parameter2), typeof (DynamicContentViewDetail).FullName, dynamicModule, moduleType))
        return;
      item.Parameters["DefaultDetailTemplateKey"] = defaultDetailTemplateId.ToString();
    }

    /// <summary>
    /// Uninstalls the widgets for the specified content type names from the configuration.
    /// </summary>
    /// <param name="contentTypeNames">The content type names.</param>
    public void Uninstall(List<string> contentTypeNames)
    {
      foreach (string contentTypeName in contentTypeNames)
        this.Uninstall(contentTypeName);
    }

    /// <summary>
    /// Deletes the widgets for the specified content type name from the configuration.
    /// </summary>
    /// <param name="contentTypeName">Name of the content type.</param>
    public void Uninstall(string contentTypeName)
    {
      ConfigManager manager = ConfigManager.GetManager();
      ToolboxesConfig section = manager.GetSection<ToolboxesConfig>();
      Toolbox toolbox = section.Toolboxes["PageControls"];
      int num1 = WidgetInstaller.RemoveWidgetFromSection(contentTypeName, toolbox, "ContentToolboxSection", false) ? 1 : 0;
      string sectionName = contentTypeName.Substring(0, contentTypeName.LastIndexOf('.'));
      int num2 = WidgetInstaller.RemoveWidgetFromSection(contentTypeName, toolbox, sectionName, true) ? 1 : 0;
      if ((num1 | num2) == 0)
        return;
      manager.SaveSection((ConfigSection) section);
    }

    /// <summary>
    /// Registers the templates for the specified dynamic module type.
    /// </summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="defaultMasterTemplateId">The default master template id.</param>
    /// <param name="defaultDetailTemplateId">The default detail template id.</param>
    public void RegisterTemplates(
      DynamicModule dynamicModule,
      DynamicModuleType dynamicModuleType,
      out Guid defaultMasterTemplateId,
      out Guid defaultDetailTemplateId)
    {
      defaultMasterTemplateId = this.WidgetTemplateInstaller.InstallDefaultMasterTemplate(dynamicModule, dynamicModuleType);
      defaultDetailTemplateId = this.WidgetTemplateInstaller.InstallDefaultDetailsTemplate(dynamicModule, dynamicModuleType);
    }

    /// <summary>
    /// Unregisters the widget templates for the specified module type.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    public void UnregisterTemplates(DynamicModuleType dynamicModuleType)
    {
    }

    /// <summary>
    /// Unregisters the widget templates for the specified module types.
    /// </summary>
    /// <param name="contentTypeNames">The content type names.</param>
    public void UnregisterTemplates(List<string> contentTypeNames)
    {
      foreach (string contentTypeName in contentTypeNames)
        this.UnregisterTemplates(contentTypeName);
    }

    /// <summary>
    /// Unregister the widget templates for the specified dynamic module type.
    /// </summary>
    /// <param name="dynamicModuleTypeName">Name of the dynamic module type.</param>
    public void UnregisterTemplates(string dynamicModuleTypeName) => this.WidgetTemplateInstaller.UnInstallWidgetTemplates(dynamicModuleTypeName);

    /// <summary>Updates the widget templates.</summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="dynamicType">Type of the dynamic module.</param>
    public void UpdateWidgetTemplates(DynamicModule dynamicModule, DynamicModuleType dynamicType)
    {
      string dynamicTypeName = dynamicType.GetFullTypeName();
      IQueryable<ControlPresentation> presentationItems = this.pageManager.GetPresentationItems<ControlPresentation>();
      Expression<Func<ControlPresentation, bool>> predicate = (Expression<Func<ControlPresentation, bool>>) (cp => cp.Condition == dynamicTypeName);
      foreach (ControlPresentation controlPresentation in (IEnumerable<ControlPresentation>) presentationItems.Where<ControlPresentation>(predicate))
      {
        Type controlType = TypeResolutionService.ResolveType(controlPresentation.ControlType, false);
        if (controlType != (Type) null)
          controlPresentation.Data = this.WidgetTemplateInstaller.GetDefaultTemplate(dynamicType, controlType);
      }
      ConfigManager manager = ConfigManager.GetManager();
      ToolboxesConfig section = manager.GetSection<ToolboxesConfig>();
      Toolbox toolbox = section.Toolboxes["PageControls"];
      string moduleSectionName = this.moduleBuilderManager.GetTypeNamespace(dynamicModule.Name);
      foreach (ToolboxSection toolboxSection in toolbox.Sections.Where<ToolboxSection>((Func<ToolboxSection, bool>) (s => s.Name == "ContentToolboxSection" || s.Name == moduleSectionName)))
      {
        ToolboxItem toolboxItem = toolboxSection.Tools.FirstOrDefault<ToolboxItem>((Func<ToolboxItem, bool>) (e => e.Name == dynamicType.GetFullTypeName()));
        if (toolboxItem != null)
        {
          string assemblyQualifiedName = typeof (DynamicContentView).AssemblyQualifiedName;
          toolboxItem.ControlType = assemblyQualifiedName;
          NameValueCollection parameters = toolboxItem.Parameters;
          bool hideListView = this.HasContentTypeChildren(dynamicModule, dynamicType);
          WidgetInstaller.SetHideListViewOnChildDetailsViewParam(parameters, hideListView);
          toolboxItem.Parameters = parameters;
          manager.SaveSection((ConfigSection) section);
        }
      }
    }

    /// <summary>
    /// Updates the toolbox item.
    /// The widget templates remain unchanged.
    /// </summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    public void UpdateToolboxItem(DynamicModule dynamicModule, DynamicModuleType dynamicModuleType)
    {
      ConfigManager manager = ConfigManager.GetManager();
      ToolboxesConfig section = manager.GetSection<ToolboxesConfig>();
      ToolboxSection moduleToolboxSection = this.GetModuleToolboxSection(dynamicModule, section);
      Guid defaultMasterTemplateId = Guid.Empty;
      Guid defaultDetailTemplateId = Guid.Empty;
      this.GetTemplatesIDs(dynamicModule, dynamicModuleType, out defaultMasterTemplateId, out defaultDetailTemplateId);
      ToolboxItem updateToolboxItem = this.CreateOrUpdateToolboxItem(dynamicModule, dynamicModuleType, moduleToolboxSection, defaultMasterTemplateId, defaultDetailTemplateId);
      string assemblyQualifiedName = typeof (DynamicContentView).AssemblyQualifiedName;
      if (updateToolboxItem != null && updateToolboxItem.ControlType != assemblyQualifiedName)
        updateToolboxItem.ControlType = assemblyQualifiedName;
      manager.SaveSection((ConfigSection) section);
      this.ValidateToolboxItemTemplateKeys(dynamicModule, dynamicModuleType);
    }

    /// <summary>
    /// Sets the HideListViewOnChildDetailsView property of parent type widgets in toolbox.
    /// </summary>
    /// <param name="itemParams">The item parameters.</param>
    /// <param name="hideListView">The hide list view.</param>
    private static void SetHideListViewOnChildDetailsViewParam(
      NameValueCollection itemParams,
      bool hideListView)
    {
      if (hideListView)
      {
        if (itemParams.Keys.Contains("HideListViewOnChildDetailsView"))
          itemParams["HideListViewOnChildDetailsView"] = "true";
        else
          itemParams.Add("HideListViewOnChildDetailsView", "true");
      }
      else
      {
        if (!itemParams.Keys.Contains("HideListViewOnChildDetailsView"))
          return;
        itemParams["HideListViewOnChildDetailsView"] = "false";
      }
    }

    private static bool RemoveWidgetFromSection(
      string contentTypeName,
      Toolbox pageControls,
      string sectionName,
      bool removeSectionIfEmpty)
    {
      bool flag = false;
      ToolboxSection toolboxSection = pageControls.Sections.Where<ToolboxSection>((Func<ToolboxSection, bool>) (e => e.Name == sectionName)).FirstOrDefault<ToolboxSection>();
      if (toolboxSection != null)
      {
        if (toolboxSection.Tools.Any<ToolboxItem>((Func<ToolboxItem, bool>) (e => e.Name == contentTypeName)))
        {
          ToolboxItem toolboxItem = toolboxSection.Tools.FirstOrDefault<ToolboxItem>((Func<ToolboxItem, bool>) (e => e.Name == contentTypeName));
          toolboxSection.Tools.Remove(toolboxItem);
          if (!flag)
            flag = true;
        }
        if (removeSectionIfEmpty && !toolboxSection.Tools.Any<ToolboxItem>())
          pageControls.Sections.Remove(toolboxSection);
      }
      return flag;
    }

    private ToolboxSection GetModuleToolboxSection(
      DynamicModule dynamicModule,
      ToolboxesConfig toolboxesConfig)
    {
      Toolbox toolbox = toolboxesConfig.Toolboxes["PageControls"];
      string moduleSectionName = this.moduleBuilderManager.GetTypeNamespace(dynamicModule.Name);
      ToolboxSection element = toolbox.Sections.Where<ToolboxSection>((Func<ToolboxSection, bool>) (e => e.Name == moduleSectionName)).FirstOrDefault<ToolboxSection>() ?? toolbox.Sections.FirstOrDefault<ToolboxSection>((Func<ToolboxSection, bool>) (s => s.Name == "ContentToolboxSection"));
      if (element == null)
      {
        string str = string.Format(Res.Get<ModuleBuilderResources>().ModuleSectionDescription, (object) dynamicModule.GetTitle());
        element = new ToolboxSection((ConfigElement) toolbox.Sections)
        {
          Name = moduleSectionName,
          Title = dynamicModule.Title,
          Description = str
        };
        toolbox.Sections.Add(element);
      }
      return element;
    }

    private ToolboxItem CreateOrUpdateToolboxItem(
      DynamicModule dynamicModule,
      DynamicModuleType dynamicModuleType,
      ToolboxSection section,
      Guid defaultMasterTemplateId,
      Guid defaultDetailTemplateId)
    {
      ToolboxItem element = section.Tools.FirstOrDefault<ToolboxItem>((Func<ToolboxItem, bool>) (e => e.Name == dynamicModuleType.GetFullTypeName()));
      if (element == null)
      {
        ToolboxItem toolboxItem = new ToolboxItem((ConfigElement) section.Tools);
        toolboxItem.Name = dynamicModuleType.GetFullTypeName();
        toolboxItem.Title = WidgetInstaller.PluralsNameResolver.ToPlural(dynamicModuleType.DisplayName);
        toolboxItem.Description = string.Empty;
        toolboxItem.ModuleName = dynamicModule.Name;
        toolboxItem.CssClass = "sfNewsViewIcn";
        toolboxItem.ControlType = typeof (DynamicContentView).AssemblyQualifiedName;
        toolboxItem.Parameters = new NameValueCollection()
        {
          {
            "DynamicContentTypeName",
            dynamicModuleType.GetFullTypeName()
          },
          {
            "DefaultMasterTemplateKey",
            defaultMasterTemplateId.ToString()
          },
          {
            "DefaultDetailTemplateKey",
            defaultDetailTemplateId.ToString()
          }
        };
        toolboxItem.Origin = OriginWrapperObject.ToArrayJson(dynamicModuleType.Origin);
        element = toolboxItem;
        section.Tools.Add(element);
      }
      NameValueCollection parameters = element.Parameters;
      bool hideListView = this.HasContentTypeChildren(dynamicModule, dynamicModuleType);
      WidgetInstaller.SetHideListViewOnChildDetailsViewParam(parameters, hideListView);
      element.Parameters = parameters;
      return element;
    }

    /// <summary>Gets the templates I ds.</summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="defaultMasterTemplateId">The default master template id.</param>
    /// <param name="defaultDetailTemplateId">The default detail template id.</param>
    private void GetTemplatesIDs(
      DynamicModule dynamicModule,
      DynamicModuleType moduleType,
      out Guid defaultMasterTemplateId,
      out Guid defaultDetailTemplateId)
    {
      defaultMasterTemplateId = this.WidgetTemplateInstaller.GetDefaultMasterTemplateID(dynamicModule, moduleType);
      defaultDetailTemplateId = this.WidgetTemplateInstaller.GetDefaultDetailsTemplateID(dynamicModule, moduleType);
    }

    /// <summary>
    /// Determined whether hierarchical widget should be installed.
    /// </summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="moduleType">Dynamic module type.</param>
    /// <returns>Value indicating whether the given type has children.</returns>
    private bool HasContentTypeChildren(DynamicModule dynamicModule, DynamicModuleType moduleType) => this.moduleBuilderManager.HasContentTypeChildren(dynamicModule, moduleType);

    /// <summary>
    /// Default widget templates are shared with the current site if there is only one site.
    /// </summary>
    /// <param name="masterTemplateId">The master template identifier.</param>
    /// <param name="detailsTemplateId">The details template identifier.</param>
    private void ConfigureForDefaultSite(Guid masterTemplateId, Guid detailsTemplateId)
    {
      if (!SystemManager.CurrentContext.IsOneSiteMode)
        return;
      if (!this.pageManager.Provider.GetSiteItemLinks().Any<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (sil => sil.ItemId == detailsTemplateId && sil.SiteId == SystemManager.CurrentContext.CurrentSite.Id)))
      {
        ControlPresentation controlPresentation = new ControlPresentation();
        controlPresentation.Id = detailsTemplateId;
        this.pageManager.LinkPresentationItemToSite((PresentationData) controlPresentation, SystemManager.CurrentContext.CurrentSite.Id);
      }
      if (this.pageManager.Provider.GetSiteItemLinks().Any<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (sil => sil.ItemId == masterTemplateId && sil.SiteId == SystemManager.CurrentContext.CurrentSite.Id)))
        return;
      ControlPresentation controlPresentation1 = new ControlPresentation();
      controlPresentation1.Id = masterTemplateId;
      this.pageManager.LinkPresentationItemToSite((PresentationData) controlPresentation1, SystemManager.CurrentContext.CurrentSite.Id);
    }

    private void CreateModuleToolboxSection(DynamicModule module)
    {
      ConfigManager manager = ConfigManager.GetManager();
      ToolboxesConfig section = manager.GetSection<ToolboxesConfig>();
      Toolbox toolbox = section.Toolboxes["PageControls"];
      ToolboxSection toolboxSection = toolbox.Sections.FirstOrDefault<ToolboxSection>((Func<ToolboxSection, bool>) (s => s.Name == "ContentToolboxSection"));
      if (toolboxSection == null || !toolboxSection.Tools.Any<ToolboxItem>((Func<ToolboxItem, bool>) (t => t.ModuleName == module.Name)))
      {
        string moduleSectionName = this.moduleBuilderManager.GetTypeNamespace(module.Name);
        if (toolbox.Sections.FirstOrDefault<ToolboxSection>((Func<ToolboxSection, bool>) (s => s.Name == moduleSectionName)) == null)
        {
          string str = string.Format(Res.Get<ModuleBuilderResources>().ModuleSectionDescription, (object) module.GetTitle());
          ToolboxSection element = new ToolboxSection((ConfigElement) toolbox.Sections)
          {
            Name = moduleSectionName,
            Title = module.Title,
            Description = str
          };
          toolbox.Sections.Add(element);
        }
      }
      manager.SaveSection((ConfigSection) section);
    }
  }
}
