// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.MasterGridViewElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>
  /// Configuration element for the <see cref="!:MasterView" /> view.
  /// </summary>
  public class MasterGridViewElement : 
    ContentViewMasterElement,
    IMasterViewDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private List<IWidgetDefinition> titleWidgets;
    private List<IDecisionScreenDefinition> decisionScreens;
    private List<IDialogDefinition> dialogs;
    private List<ILinkDefinition> links;
    private List<IPromptDialogDefinition> promptDialogs;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewMasterElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public MasterGridViewElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new MasterGridViewDefinition((ConfigElement) this);

    /// <summary>
    /// Gets the collection of decision screen config elements that are used on the view.
    /// </summary>
    [ConfigurationProperty("decisionScreens")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendGridDecisionScreensDescription", Title = "BackendGridDecisionScreensCaption")]
    public ConfigElementDictionary<string, DecisionScreenElement> DecisionScreensConfig => (ConfigElementDictionary<string, DecisionScreenElement>) this["decisionScreens"];

    /// <summary>
    /// Gets the collection of dialog config elements that are used on the view.
    /// </summary>
    [ConfigurationProperty("dialogs")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendGridDialogsDescription", Title = "BackendGridDialogsCaption")]
    public ConfigElementList<DialogElement> DialogsConfig => (ConfigElementList<DialogElement>) this["dialogs"];

    /// <summary>
    /// Gets the collection of dialog config elements that are used on the view.
    /// </summary>
    [ConfigurationProperty("PromptDialogs")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendGridPromptDialogsDescription", Title = "BackendGridPromptDialogsCaption")]
    public new ConfigElementList<PromptDialogElement> PromptDialogsConfig => (ConfigElementList<PromptDialogElement>) this["PromptDialogs"];

    /// <summary>
    /// Gets the collection of column config elements that are displayed on the view.
    /// </summary>
    [ConfigurationProperty("viewModes")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendMasterViewModesDescription", Title = "BackendMasterViewModesCaption")]
    public ConfigElementList<ViewModeElement> ViewModesConfig => (ConfigElementList<ViewModeElement>) this["viewModes"];

    /// <summary>
    /// Gets the collection of link config elements that are used by the view.
    /// </summary>
    [ConfigurationProperty("links")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendGridLinksDescription", Title = "BackendGridLinksCaption")]
    public ConfigElementDictionary<string, LinkElement> LinksConfig => (ConfigElementDictionary<string, LinkElement>) this["links"];

    /// <summary>Gets the toolbar.</summary>
    /// <value>The toolbar.</value>
    [ConfigurationProperty("toolbar")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolbarDescription", Title = "ToolbarCaption")]
    public WidgetBarElement ToolbarConfig => (WidgetBarElement) this["toolbar"];

    /// <summary>Gets the sidebar.</summary>
    /// <value>The sidebar.</value>
    [ConfigurationProperty("sidebar")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SidebarDescription", Title = "SidebarCaption")]
    public WidgetBarElement SidebarConfig => (WidgetBarElement) this["sidebar"];

    /// <summary>Gets the context bar.</summary>
    /// <value>The context bar.</value>
    [ConfigurationProperty("contextBar")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContextBarDescription", Title = "ContextBarCaption")]
    public WidgetBarElement ContextBarConfig => (WidgetBarElement) this["contextBar"];

    /// <summary>
    /// Gets or sets the css class that is applied to the grid.
    /// </summary>
    [ConfigurationProperty("gridCssClass")]
    public string GridCssClass
    {
      get => (string) this["gridCssClass"];
      set => this["gridCssClass"] = (object) value;
    }

    /// <summary>
    /// Comma separated list of fields (properties) on which the search ought to be performed
    /// </summary>
    [ConfigurationProperty("searchFields")]
    public string SearchFields
    {
      get => (string) this["searchFields"];
      set => this["searchFields"] = (object) value;
    }

    /// <summary>
    /// Comma separated list of fields (properties) of type <see cref="!:Lstring" /> on which the search ought to be performed
    /// </summary>
    [ConfigurationProperty("extendedSearchFields")]
    public string ExtendedSearchFields
    {
      get => (string) this["extendedSearchFields"];
      set => this["extendedSearchFields"] = (object) value;
    }

    /// <summary>
    /// Filter expression that will show only items that aren't published or scheduled
    /// </summary>
    [ConfigurationProperty("draftFilter")]
    public string DraftFilter
    {
      get => (string) this["draftFilter"];
      set => this["draftFilter"] = (object) value;
    }

    /// <summary>
    /// Filter expression that will show only items that are published (live)
    /// </summary>
    [ConfigurationProperty("publicFilter")]
    public string PublishedFilter
    {
      get => (string) this["publicFilter"];
      set => this["publicFilter"] = (object) value;
    }

    /// <summary>
    /// Filter expresion that will show only items that are scheduled
    /// </summary>
    [ConfigurationProperty("publicFilter")]
    public string ScheduledFilter
    {
      get => (string) this["publicFilter"];
      set => this["publicFilter"] = (object) value;
    }

    /// <summary>
    /// Filter expresion that will show only items that are pending approval
    /// </summary>
    [ConfigurationProperty("pendingApprovalFilter")]
    public string PendingApprovalFilter
    {
      get => (string) this["pendingApprovalFilter"];
      set => this["pendingApprovalFilter"] = (object) value;
    }

    /// <summary>
    /// Defines a dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> configuration elements.
    /// </summary>
    /// <value>The dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> configuration element.</value>
    [ConfigurationProperty("titleWidgets")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TitleWidgetDescription", Title = "TitleWidgetCaption")]
    [ConfigurationCollection(typeof (WidgetElement), AddItemName = "item")]
    public ConfigElementList<WidgetElement> TitleWidgetsConfig => (ConfigElementList<WidgetElement>) this["titleWidgets"];

    public List<IWidgetDefinition> TitleWidgets
    {
      get
      {
        if (this.titleWidgets == null)
          this.titleWidgets = this.TitleWidgetsConfig.Elements.Select<WidgetElement, IWidgetDefinition>((Func<WidgetElement, IWidgetDefinition>) (t => (IWidgetDefinition) t.ToDefinition())).ToList<IWidgetDefinition>();
        return this.titleWidgets;
      }
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IDecisionScreenDefinition> DecisionScreens
    {
      get
      {
        if (this.decisionScreens == null)
          this.decisionScreens = this.DecisionScreensConfig.Elements.Select<DecisionScreenElement, IDecisionScreenDefinition>((Func<DecisionScreenElement, IDecisionScreenDefinition>) (d => (IDecisionScreenDefinition) d.ToDefinition())).ToList<IDecisionScreenDefinition>();
        return this.decisionScreens;
      }
    }

    public List<IDialogDefinition> Dialogs
    {
      get
      {
        if (this.dialogs == null)
          this.dialogs = this.DialogsConfig.Elements.Select<DialogElement, IDialogDefinition>((Func<DialogElement, IDialogDefinition>) (d => (IDialogDefinition) d.ToDefinition())).ToList<IDialogDefinition>();
        return this.dialogs;
      }
    }

    public new List<IPromptDialogDefinition> PromptDialogs
    {
      get
      {
        if (this.promptDialogs == null)
          this.promptDialogs = this.PromptDialogsConfig.Elements.Select<PromptDialogElement, IPromptDialogDefinition>((Func<PromptDialogElement, IPromptDialogDefinition>) (p => (IPromptDialogDefinition) p.ToDefinition())).ToList<IPromptDialogDefinition>();
        return this.promptDialogs;
      }
      set => this.promptDialogs = value;
    }

    public Dictionary<string, IViewModeDefinition> ViewModes
    {
      get
      {
        Dictionary<string, IViewModeDefinition> viewModes = new Dictionary<string, IViewModeDefinition>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        foreach (ViewModeElement viewModeElement in this.ViewModesConfig)
          viewModes.Add(viewModeElement.Name, (IViewModeDefinition) viewModeElement.GetDefinition());
        return viewModes;
      }
    }

    public List<ILinkDefinition> Links
    {
      get
      {
        if (this.links == null)
          this.links = this.LinksConfig.Elements.Select<LinkElement, ILinkDefinition>((Func<LinkElement, ILinkDefinition>) (l => (ILinkDefinition) l.ToDefinition())).ToList<ILinkDefinition>();
        return this.links;
      }
    }

    public IWidgetBarDefinition Toolbar => (IWidgetBarDefinition) this.ToolbarConfig;

    public IWidgetBarDefinition Sidebar => (IWidgetBarDefinition) this.SidebarConfig;

    public IWidgetBarDefinition ContextBar => (IWidgetBarDefinition) this.ContextBarConfig;

    /// <summary>
    /// Gets or sets the message to be shown when propting the user if they are sure they want to delete single item.
    /// </summary>
    [ConfigurationProperty("deleteSingleConfirmationMessage", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DeleteSingleConfirmationMessageDescription", Title = "DeleteSingleConfirmationMessage")]
    public string DeleteSingleConfirmationMessage
    {
      get => (string) this["deleteSingleConfirmationMessage"];
      set => this["deleteSingleConfirmationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the message to be shown when propting the user if they are sure they want to delete multiple items.
    /// </summary>
    [ConfigurationProperty("deleteMultipleConfirmationMessage", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DeleteMultipleConfirmationMessageDescription", Title = "DeleteMultipleConfirmationMessage")]
    public string DeleteMultipleConfirmationMessage
    {
      get => (string) this["deleteMultipleConfirmationMessage"];
      set => this["deleteMultipleConfirmationMessage"] = (object) value;
    }

    /// <summary>
    /// If set to true, the MGV will not bind the current ItemsListBase on the client when the page is loaded. Default is false, i.e.
    /// it will bind. This is different from the current ItemsListBase.ClientBinder.BindOnLoad, which is always false and not affected by this property
    /// </summary>
    [ConfigurationProperty("doNotBindOnClientWhenPageIsLoaded", DefaultValue = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DoNotBindOnClientWhenPageIsLoadedDescription", Title = "DoNotBindOnClientWhenPageIsLoadedCaption")]
    public bool DoNotBindOnClientWhenPageIsLoaded
    {
      get => (bool) this["doNotBindOnClientWhenPageIsLoaded"];
      set => this["doNotBindOnClientWhenPageIsLoaded"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the ID of the page displaying the master view.
    /// </summary>
    [ConfigurationProperty("landingPageId", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LandingPageIdDescription", Title = "LandingPageIdCaption")]
    public Guid? LandingPageId
    {
      get => (Guid?) this["landingPageId"];
      set => this["landingPageId"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string decisionScreens = "decisionScreens";
      public const string dialogs = "dialogs";
      public const string viewModes = "viewModes";
      public const string links = "links";
      public const string hasSidebar = "hasSidebar";
      public const string searchMode = "searchMode";
      public const string toolbar = "toolbar";
      public const string sidebar = "sidebar";
      public const string contextBar = "contextBar";
      public const string gridCssClass = "gridCssClass";
      public const string searchFields = "searchFields";
      public const string extendedSearchFields = "extendedSearchFields";
      public const string titleWidgets = "titleWidgets";
      public const string draftFilter = "draftFilter";
      public const string publicFilter = "publicFilter";
      public const string scheduledFilter = "publicFilter";
      public const string pendingApprovalFilter = "pendingApprovalFilter";
      public const string promptDialogs = "PromptDialogs";
      public const string DeleteSingleConfirmationMessage = "deleteSingleConfirmationMessage";
      public const string DeleteMultipleConfirmationMessage = "deleteMultipleConfirmationMessage";
      public const string DoNotBindOnClientWhenPageIsLoaded = "doNotBindOnClientWhenPageIsLoaded";
      public const string landingPageId = "landingPageId";
    }
  }
}
