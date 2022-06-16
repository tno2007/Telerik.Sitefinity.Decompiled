// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.MasterGridViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions
{
  /// <summary>
  /// A definition class that the determines the way in which the instance of
  /// backend master grid view will be constructed.
  /// </summary>
  public class MasterGridViewDefinition : 
    ContentViewMasterDefinition,
    IMasterViewDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private List<IDecisionScreenDefinition> decisionScreens;
    private List<IDialogDefinition> dialogs;
    private List<IPromptDialogDefinition> promptDialogs;
    private Dictionary<string, IViewModeDefinition> viewModes;
    private List<ILinkDefinition> links;
    private IWidgetBarDefinition toolbar;
    private IWidgetBarDefinition sidebar;
    private IWidgetBarDefinition contextBar;
    private List<IWidgetDefinition> titleWidgets;
    private string gridCssClass;
    private string searchFields;
    private string extendedSearchFields;
    private string draftFilter;
    private string publishedFilter;
    private string scheduledFilter;
    private string pendingApprovalFilter;
    private string deleteSingleConfirmationMessage;
    private string deleteMultipleConfirmationMessage;
    private bool? doNotBindOnClientWhenPageIsLoaded;
    private string providersGroups;
    private Guid? landingPageId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ColumnDefinition" /> class.
    /// </summary>
    public MasterGridViewDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public MasterGridViewDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public MasterGridViewDefinition GetDefinition() => this;

    /// <summary>
    /// Gets the collection of decision screen definitions that are used on the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IDecisionScreenDefinition> DecisionScreens
    {
      get
      {
        if (this.decisionScreens == null)
          this.decisionScreens = this.GetMasterGridViewConfiguration().DecisionScreens.Select<IDecisionScreenDefinition, IDecisionScreenDefinition>((Func<IDecisionScreenDefinition, IDecisionScreenDefinition>) (d => (IDecisionScreenDefinition) d.GetDefinition())).ToList<IDecisionScreenDefinition>();
        return this.decisionScreens;
      }
      set => this.decisionScreens = value;
    }

    /// <summary>
    /// Gets the collection of dialog definitions that are used on the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IDialogDefinition> Dialogs
    {
      get
      {
        if (this.dialogs == null)
          this.dialogs = this.GetMasterGridViewConfiguration().Dialogs.Select<IDialogDefinition, IDialogDefinition>((Func<IDialogDefinition, IDialogDefinition>) (c => (IDialogDefinition) c.GetDefinition())).ToList<IDialogDefinition>();
        return this.dialogs;
      }
      set => this.dialogs = value;
    }

    /// <summary>
    /// Gets the collection of dialog definitions that are used on the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public new List<IPromptDialogDefinition> PromptDialogs
    {
      get => this.ResolveProperty<List<IPromptDialogDefinition>>(nameof (PromptDialogs), this.promptDialogs);
      internal set => this.promptDialogs = value;
    }

    /// <summary>
    /// Gets or sets the view modes (Grid, List, TreeView, etc.).
    /// </summary>
    /// <value>The view modes.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Dictionary<string, IViewModeDefinition> ViewModes
    {
      get
      {
        if (this.viewModes == null || this.viewModes.Count == 0)
          this.viewModes = this.GetMasterGridViewConfiguration().ViewModes.ToDictionary<KeyValuePair<string, IViewModeDefinition>, string, IViewModeDefinition>((Func<KeyValuePair<string, IViewModeDefinition>, string>) (vm => vm.Key), (Func<KeyValuePair<string, IViewModeDefinition>, IViewModeDefinition>) (vm => (IViewModeDefinition) vm.Value.GetDefinition()), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        return this.viewModes;
      }
      internal set => this.viewModes = value;
    }

    /// <summary>
    /// Gets the collection of link definitions that are used by the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<ILinkDefinition> Links
    {
      get
      {
        if (this.links == null)
          this.links = this.GetMasterGridViewConfiguration().Links.Select<ILinkDefinition, ILinkDefinition>((Func<ILinkDefinition, ILinkDefinition>) (p => (ILinkDefinition) p.GetDefinition())).ToList<ILinkDefinition>();
        return this.links;
      }
    }

    /// <summary>Gets the definitions to be display the toolbar.</summary>
    /// <value></value>
    public IWidgetBarDefinition Toolbar
    {
      get
      {
        if (this.toolbar == null)
          this.toolbar = this.ResolveComplexPropertyAsDefinition<MasterGridViewElement, IWidgetBarDefinition>((Func<MasterGridViewElement, IWidgetBarDefinition>) (e => e.Toolbar));
        return this.toolbar;
      }
    }

    /// <summary>Gets the definitions to be display the sidebar.</summary>
    /// <value></value>
    public IWidgetBarDefinition Sidebar
    {
      get
      {
        if (this.sidebar == null)
          this.sidebar = this.ResolveComplexPropertyAsDefinition<MasterGridViewElement, IWidgetBarDefinition>((Func<MasterGridViewElement, IWidgetBarDefinition>) (e => e.Sidebar));
        return this.sidebar;
      }
    }

    /// <summary>Gets the context bar.</summary>
    /// <value>The context bar.</value>
    public IWidgetBarDefinition ContextBar
    {
      get
      {
        if (this.contextBar == null)
          this.contextBar = this.ResolveComplexPropertyAsDefinition<MasterGridViewElement, IWidgetBarDefinition>((Func<MasterGridViewElement, IWidgetBarDefinition>) (e => e.ContextBar));
        return this.contextBar;
      }
    }

    /// <summary>
    /// Gets the collection of link definitions that are used by the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IWidgetDefinition> TitleWidgets
    {
      get
      {
        if (this.titleWidgets == null)
          this.titleWidgets = this.GetMasterGridViewConfiguration().TitleWidgets.Select<IWidgetDefinition, IWidgetDefinition>((Func<IWidgetDefinition, IWidgetDefinition>) (p => (IWidgetDefinition) p.GetDefinition())).ToList<IWidgetDefinition>();
        return this.titleWidgets;
      }
    }

    /// <summary>
    /// Gets or sets the css class that is applied to the grid.
    /// </summary>
    public string GridCssClass
    {
      get => this.ResolveProperty<string>(nameof (GridCssClass), this.gridCssClass);
      set => this.gridCssClass = value;
    }

    /// <summary>
    /// Comma separated list of fields (properties) on which the search ought to be performed
    /// </summary>
    public string SearchFields
    {
      get => this.ResolveProperty<string>(nameof (SearchFields), this.searchFields);
      set => this.searchFields = value;
    }

    /// <summary>
    /// Comma separated list of fields (properties) of type <see cref="!:Lstring" /> on which the search ought to be performed
    /// </summary>
    public string ExtendedSearchFields
    {
      get => this.ResolveProperty<string>(nameof (ExtendedSearchFields), this.extendedSearchFields);
      set => this.extendedSearchFields = value;
    }

    /// <summary>
    /// Filter expression that will show only items that aren't published or scheduled
    /// </summary>
    public string DraftFilter
    {
      get => this.ResolveProperty<string>(nameof (DraftFilter), this.draftFilter);
      set => this.draftFilter = value;
    }

    /// <summary>
    /// Filter expresion that will show only items that are published (live)
    /// </summary>
    public string PublishedFilter
    {
      get => this.ResolveProperty<string>(nameof (PublishedFilter), this.publishedFilter);
      set => this.publishedFilter = value;
    }

    /// <summary>
    /// Filter expresion that will show only items that are scheduled
    /// </summary>
    public string ScheduledFilter
    {
      get => this.ResolveProperty<string>(nameof (ScheduledFilter), this.scheduledFilter);
      set => this.scheduledFilter = value;
    }

    /// <summary>
    /// Filter expression that will show only items that are pending approval
    /// </summary>
    public string PendingApprovalFilter
    {
      get => this.ResolveProperty<string>(nameof (PendingApprovalFilter), this.pendingApprovalFilter);
      set => this.pendingApprovalFilter = value;
    }

    /// <summary>
    /// Gets or sets the message to be shown when propting the user if they are sure they want to delete single item.
    /// </summary>
    public string DeleteSingleConfirmationMessage
    {
      get => this.ResolveProperty<string>(nameof (DeleteSingleConfirmationMessage), this.deleteSingleConfirmationMessage);
      set => this.deleteSingleConfirmationMessage = value;
    }

    /// <summary>
    /// Gets or sets the message to be shown when propting the user if they are sure they want to delete multiple items.
    /// </summary>
    public string DeleteMultipleConfirmationMessage
    {
      get => this.ResolveProperty<string>(nameof (DeleteMultipleConfirmationMessage), this.deleteMultipleConfirmationMessage);
      set => this.deleteMultipleConfirmationMessage = value;
    }

    /// <summary>
    /// If set to true, the MGV will not bind the current ItemsListBase on the client when the page is loaded. Default is false, i.e.
    /// it will bind. This is different from the current ItemsListBase.ClientBinder.BindOnLoad, which is always false and not affected by this property
    /// </summary>
    public bool DoNotBindOnClientWhenPageIsLoaded
    {
      get => this.ResolveProperty<bool?>(nameof (DoNotBindOnClientWhenPageIsLoaded), this.doNotBindOnClientWhenPageIsLoaded) ?? false;
      set => this.doNotBindOnClientWhenPageIsLoaded = new bool?(value);
    }

    /// <summary>
    /// Gets or sets a comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).
    /// </summary>
    /// <value>A comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).</value>
    public new string ProvidersGroups
    {
      get => this.ResolveProperty<string>(nameof (ProvidersGroups), this.providersGroups);
      set => this.providersGroups = value;
    }

    /// <summary>
    /// Gets or sets the ID of the page displaying the master view.
    /// </summary>
    public Guid? LandingPageId
    {
      get => this.ResolveProperty<Guid?>(nameof (LandingPageId), this.landingPageId);
      set => this.landingPageId = value;
    }

    private MasterGridViewElement GetMasterGridViewConfiguration() => (MasterGridViewElement) this.ConfigDefinition;
  }
}
