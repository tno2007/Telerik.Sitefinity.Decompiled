// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Modules.Toolboxes;
using Telerik.Sitefinity.Fluent.Modules.Workflow;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.Fluent.Modules
{
  /// <summary>Fluent API for installing a module.</summary>
  public class ModuleInstallFacade
  {
    private PageManager pageManager;
    private IModuleInstallContext context;
    private string moduleName;
    private AppSettings settings;

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    public ModuleInstallFacade(string moduleName, PageManager pageManager)
      : this(moduleName, (AppSettings) null, pageManager)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    public ModuleInstallFacade(string moduleName, AppSettings settings)
      : this(moduleName, settings, (PageManager) null, (IModuleInstallContext) null)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    public ModuleInstallFacade(string moduleName, AppSettings settings, PageManager pageManager)
      : this(moduleName, settings, pageManager, (IModuleInstallContext) null)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    internal ModuleInstallFacade(
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
    /// The instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade.PageManager" /> that should be used in the current transaction.
    /// </param>
    protected ModuleInstallFacade(
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
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade.PageManager" /> to be used by module facade and it's child facades.
    /// </summary>
    protected PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
        {
          this.pageManager = this.context == null ? (this.settings == null ? PageManager.GetManager() : PageManager.GetManager(this.settings.PagesProviderName, this.settings.TransactionName)) : this.context.GetManager<PageManager>();
          this.pageManager.Provider.SuppressSecurityChecks = true;
        }
        return this.pageManager;
      }
    }

    protected internal IModuleInstallContext Context => this.context;

    public WorkflowElementFacade<ModuleInstallFacade> AddWorkflowType<TWorkflowItem>()
    {
      WorkflowConfig config = this.Context.GetConfig<WorkflowConfig>();
      string fullName = typeof (TWorkflowItem).FullName;
      WorkflowElement workflowElement;
      if (!config.Workflows.TryGetValue(fullName, out workflowElement))
      {
        workflowElement = new WorkflowElement((ConfigElement) config.Workflows)
        {
          ContentType = fullName
        };
        config.Workflows.Add(workflowElement);
      }
      workflowElement.ModuleName = this.moduleName;
      return new WorkflowElementFacade<ModuleInstallFacade>(workflowElement, this);
    }

    /// <summary>Provides fluent API for creating a new module node.</summary>
    /// <param name="moduleNodeId">Id of the module node.</param>
    /// <returns>The instance of the current <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<ModuleInstallFacade> CreateModuleGroupPage(
      Guid moduleNodeId,
      string groupPageName)
    {
      return new ModuleNodeFacade<ModuleInstallFacade>(this.PageManager, this.moduleName, moduleNodeId, groupPageName, this);
    }

    /// <summary>Provides fluent API for creating a new module page.</summary>
    /// <param name="modulePageId">Id of the module page.</param>
    /// <returns>The instance of the current <see cref="!:ModuleNodeFacade" />.</returns>
    public ModulePageFacade<ModuleInstallFacade> CreateModulePage(
      Guid modulePageId,
      string pageName)
    {
      return new ModulePageFacade<ModuleInstallFacade>(this.PageManager, this.moduleName, modulePageId, pageName, this);
    }

    public ModuleInstallFacade RegisterControlTemplate<TControl>(
      string templateName,
      string viewName)
    {
      ControlPresentation presentationItem = this.PageManager.CreatePresentationItem<ControlPresentation>();
      presentationItem.DataType = "ASP_NET_TEMPLATE";
      presentationItem.Name = viewName;
      presentationItem.ControlType = typeof (TControl).FullName;
      presentationItem.AreaName = (string) null;
      Type attributeType = typeof (ControlTemplateInfoAttribute);
      if (TypeDescriptor.GetAttributes(typeof (TControl))[attributeType] is ControlTemplateInfoAttribute attribute)
      {
        if (string.IsNullOrEmpty(attribute.ResourceClassId))
        {
          presentationItem.AreaName = attribute.AreaName;
          presentationItem.FriendlyControlName = attribute.ControlDisplayName;
        }
        else
        {
          presentationItem.AreaName = Res.Get(attribute.ResourceClassId, attribute.AreaName);
          presentationItem.FriendlyControlName = Res.Get(attribute.ResourceClassId, attribute.ControlDisplayName);
        }
      }
      presentationItem.EmbeddedTemplateName = templateName;
      presentationItem.ResourceAssemblyName = "Telerik.Sitefinity.Resources";
      presentationItem.IsDifferentFromEmbedded = false;
      return this;
    }

    /// <summary>
    /// Adds a witdget toolbox item in the PageControls toolbox, Content section.
    /// </summary>
    /// <typeparam name="TWidget">The type of the widget.</typeparam>
    /// <param name="widgetName">Name of the widget.</param>
    /// <returns></returns>
    private ToolboxItemFacade<ModuleInstallFacade> AddContentToolboxItem<TWidget>(
      string widgetName)
      where TWidget : Control
    {
      return this.Toolbox(CommonToolbox.PageWidgets).LoadOrAddSection("ContentToolboxSection").SetTitle("ContentToolboxSectionTitle").SetDescription("ContentToolboxSectionDescription").LocalizeUsing<PageResources>().LoadOrAddWidget<TWidget, ModuleInstallFacade>(widgetName, this);
    }

    /// <summary>
    /// Provides fluent API for initializing page toolbox items.
    /// </summary>
    /// <param name="targetToolbox">
    /// One of the values that determines for which toolbox fluent API will be loaded.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" />.</returns>
    public PageToolboxInstallFacade PageToolbox() => new PageToolboxInstallFacade(this.moduleName, this);

    /// <summary>Provides fluent API for initializing toolbox items.</summary>
    /// <param name="targetToolbox">
    /// One of the values that determines for which toolbox fluent API will be loaded.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" />.</returns>
    public ToolboxInstallFacade Toolbox(CommonToolbox targetToolbox) => new ToolboxInstallFacade(targetToolbox, this.moduleName, this);

    /// <summary>Provides fluent API for initializing toolbox items.</summary>
    /// <param name="targetToolboxName">
    /// Name of the toolbox for which the fluent API will be loaded.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Modules.Toolboxes.ToolboxInitializeFacade" />.</returns>
    public ToolboxInstallFacade Toolbox(string targetToolboxName) => !string.IsNullOrEmpty(targetToolboxName) ? new ToolboxInstallFacade(targetToolboxName, this.moduleName, this) : throw new ArgumentNullException(nameof (targetToolboxName));

    public ToolboxInstallFacade Toolbox(Telerik.Sitefinity.Modules.Pages.Configuration.Toolbox toolbox) => toolbox != null ? new ToolboxInstallFacade(toolbox, this.moduleName, this) : throw new ArgumentNullException(nameof (toolbox));

    /// <summary>
    /// Commits all the items that have been placed into the transaction while working with the fluent API.
    /// </summary>
    /// <returns>The instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.</returns>
    public ModuleInstallFacade SaveChanges()
    {
      this.pageManager.SaveChanges();
      return this;
    }
  }
}
