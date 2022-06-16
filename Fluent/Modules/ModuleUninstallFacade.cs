// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.ModuleUninstallFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.Fluent.Modules
{
  /// <summary>Fluent API for uninstalling a module.</summary>
  public class ModuleUninstallFacade
  {
    private PageManager pageManager;
    private IModuleInstallContext context;
    private string moduleName;
    private AppSettings settings;

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleUninstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    public ModuleUninstallFacade(string moduleName, PageManager pageManager)
      : this(moduleName, (AppSettings) null, pageManager)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleUninstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    public ModuleUninstallFacade(string moduleName, AppSettings settings)
      : this(moduleName, settings, (PageManager) null, (IModuleInstallContext) null)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleUninstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    public ModuleUninstallFacade(string moduleName, AppSettings settings, PageManager pageManager)
      : this(moduleName, settings, pageManager, (IModuleInstallContext) null)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleUninstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    internal ModuleUninstallFacade(
      string moduleName,
      AppSettings settings,
      IModuleInstallContext installContext)
      : this(moduleName, settings, (PageManager) null, installContext)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleUninstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    protected ModuleUninstallFacade(
      string moduleName,
      AppSettings settings,
      PageManager pageManager,
      IModuleInstallContext installContext)
    {
      this.moduleName = moduleName;
      this.settings = settings;
      this.pageManager = pageManager;
      this.context = installContext;
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleUninstallFacade.PageManager" /> to be used by module facade and it's child facades.
    /// </summary>
    protected PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
        {
          this.pageManager = this.context == null ? (this.settings == null ? PageManager.GetManager() : PageManager.GetManager(this.settings.PagesProviderName, this.settings.TransactionName)) : this.context.GetManager<PageManager>();
          this.pageManager.Provider.SuppressSecurityChecks = true;
          if (this.pageManager.Provider is IOpenAccessDataProvider provider)
          {
            SitefinityOAContext context = provider.GetContext();
            if (context != null)
              context.ContextOptions.EnableDataSynchronization = true;
          }
        }
        return this.pageManager;
      }
    }

    protected internal IModuleInstallContext Context => this.context;

    /// <summary>
    /// Automatically delete module dependent artifacts (workflow, toolbox, pages, etc..).
    /// </summary>
    /// <returns></returns>
    internal ModuleUninstallFacade Auto(IModule module)
    {
      if (module.Name != "Libraries")
      {
        WorkflowConfig config = this.Context.GetConfig<WorkflowConfig>();
        IEnumerable<string> keysToDelete = config.Workflows.Values.Where<WorkflowElement>((Func<WorkflowElement, bool>) (i => i.ModuleName == this.moduleName)).Select<WorkflowElement, string>((Func<WorkflowElement, string>) (i => i.GetKey()));
        this.DeleteCollectionElements((ConfigElementCollection) config.Workflows, keysToDelete);
      }
      List<string> source1 = new List<string>();
      List<string> stringList = new List<string>();
      foreach (Toolbox toolbox in (IEnumerable<Toolbox>) this.Context.GetConfig<ToolboxesConfig>().Toolboxes.Values)
      {
        foreach (ToolboxSection section in toolbox.Sections)
        {
          IEnumerable<ToolboxItem> source2 = section.Tools.Where<ToolboxItem>((Func<ToolboxItem, bool>) (i => i.ModuleName == this.moduleName && i.ControllerType.IsNullOrEmpty()));
          source1.AddRange(source2.Select<ToolboxItem, string>((Func<ToolboxItem, string>) (t => ToolboxesConfig.GetShortTypeName(t.ControlType))));
          IEnumerable<ToolboxItem> source3 = section.Tools.Where<ToolboxItem>((Func<ToolboxItem, bool>) (i => i.ModuleName == this.moduleName && !i.ControllerType.IsNullOrEmpty()));
          stringList.AddRange(source3.Select<ToolboxItem, string>((Func<ToolboxItem, string>) (t => t.ControllerType)));
          if (module.Name != "Libraries")
          {
            IEnumerable<string> keysToDelete1 = source2.Select<ToolboxItem, string>((Func<ToolboxItem, string>) (i => i.GetKey()));
            this.DeleteCollectionElements((ConfigElementCollection) section.Tools, keysToDelete1);
            IEnumerable<string> keysToDelete2 = source3.Select<ToolboxItem, string>((Func<ToolboxItem, string>) (i => i.GetKey()));
            this.DeleteCollectionElements((ConfigElementCollection) section.Tools, keysToDelete2);
          }
        }
      }
      this.DeleteControls(source1.Distinct<string>().ToArray<string>(), stringList.ToArray());
      IQueryable<PageNode> pageNodes = this.PageManager.GetPageNodes();
      Expression<Func<PageNode, bool>> predicate = (Expression<Func<PageNode, bool>>) (p => p.ModuleName == this.moduleName);
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes.Where<PageNode>(predicate))
        this.PageManager.Delete(pageNode);
      return this;
    }

    internal ModuleUninstallFacade DeleteControlTemplates(
      string templatesAssemblyName,
      IEnumerable<string> controlTypes)
    {
      IQueryable<ControlPresentation> presentationItems = this.PageManager.GetPresentationItems<ControlPresentation>();
      Expression<Func<ControlPresentation, bool>> predicate = (Expression<Func<ControlPresentation, bool>>) (t => t.ResourceAssemblyName == templatesAssemblyName && controlTypes.Contains<string>(t.ControlType));
      foreach (PresentationData presentationData in (IEnumerable<ControlPresentation>) presentationItems.Where<ControlPresentation>(predicate))
        this.PageManager.Delete(presentationData);
      return this;
    }

    private void DeleteControls(string[] widgetTypes, string[] mvcWidgetTypes)
    {
      List<ControlData> controlDataList = new List<ControlData>();
      controlDataList.AddRange((IEnumerable<ControlData>) this.GetControlsToDelete(widgetTypes, false));
      controlDataList.AddRange((IEnumerable<ControlData>) this.GetControlsToDelete(mvcWidgetTypes, true));
      List<PageData> source = new List<PageData>();
      foreach (ControlData controlData in controlDataList)
      {
        switch (controlData)
        {
          case PageControl _:
            PageData page = ((PageControl) controlData).Page;
            if (page != null)
            {
              source.Add(page);
              break;
            }
            break;
          case TemplateControl _:
            source.AddRange((IEnumerable<PageData>) ((TemplateControl) controlData).Page.Pages());
            break;
        }
        this.PageManager.Delete(controlData);
      }
      foreach (PageData pageData in source.Distinct<PageData>())
        ++pageData.BuildStamp;
    }

    private List<ControlData> GetControlsToDelete(string[] widgetTypes, bool isMvc)
    {
      PageManager pageManager = this.PageManager;
      List<ControlData> controlsToDelete;
      try
      {
        controlsToDelete = this.GetControlsToDelete<ControlData>(pageManager, widgetTypes, isMvc);
      }
      catch
      {
        controlsToDelete = new List<ControlData>();
        controlsToDelete.AddRange((IEnumerable<ControlData>) this.GetControlsToDelete<PageControl>(pageManager, widgetTypes, isMvc));
        controlsToDelete.AddRange((IEnumerable<ControlData>) this.GetControlsToDelete<PageDraftControl>(pageManager, widgetTypes, isMvc));
        controlsToDelete.AddRange((IEnumerable<ControlData>) this.GetControlsToDelete<TemplateControl>(pageManager, widgetTypes, isMvc));
        controlsToDelete.AddRange((IEnumerable<ControlData>) this.GetControlsToDelete<TemplateDraftControl>(pageManager, widgetTypes, isMvc));
      }
      return controlsToDelete;
    }

    private List<ControlData> GetControlsToDelete<TControlData>(
      PageManager pageManager,
      string[] widgetTypes,
      bool isMvc)
      where TControlData : ControlData
    {
      return isMvc ? this.GetMvcControlsToDelete<TControlData>(pageManager, widgetTypes) : this.GetControlsToDelete<TControlData>(pageManager, widgetTypes);
    }

    private List<ControlData> GetControlsToDelete<TControlData>(
      PageManager pageManager,
      string[] widgetTypes)
      where TControlData : ControlData
    {
      List<ControlData> controlsToDelete = new List<ControlData>();
      foreach (string widgetType1 in widgetTypes)
      {
        string widgetType = widgetType1;
        controlsToDelete.AddRange((IEnumerable<ControlData>) pageManager.GetControls<TControlData>().Where<TControlData>((Expression<Func<TControlData, bool>>) (c => c.ObjectType.StartsWith(widgetType))).ToList<TControlData>());
      }
      return controlsToDelete;
    }

    private List<ControlData> GetMvcControlsToDelete<TControlData>(
      PageManager pageManager,
      string[] mvcWidgetTypes)
      where TControlData : ControlData
    {
      List<ControlData> controlsToDelete = new List<ControlData>();
      foreach (string mvcWidgetType1 in mvcWidgetTypes)
      {
        string mvcWidgetType = mvcWidgetType1;
        controlsToDelete.AddRange((IEnumerable<ControlData>) pageManager.GetControls<TControlData>().Where<TControlData>((Expression<Func<TControlData, bool>>) (c => c.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && p.Value == mvcWidgetType)))).ToList<TControlData>());
      }
      return controlsToDelete;
    }

    private void DeleteCollectionElements(
      ConfigElementCollection collection,
      IEnumerable<string> keysToDelete)
    {
      foreach (string key in keysToDelete)
        collection.Remove(collection.GetElementByKey(key));
    }

    /// <summary>
    /// Commits all the items that have been placed into the transaction while working with the fluent API.
    /// </summary>
    /// <returns>The instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.</returns>
    public ModuleUninstallFacade SaveChanges()
    {
      this.pageManager.SaveChanges();
      return this;
    }
  }
}
