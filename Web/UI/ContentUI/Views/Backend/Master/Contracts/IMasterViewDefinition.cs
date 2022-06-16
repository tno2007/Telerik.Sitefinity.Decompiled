// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IMasterViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts
{
  /// <summary>
  /// Defines the members of the backend master grid view definition.
  /// </summary>
  public interface IMasterViewDefinition : 
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Gets the collection of decision screen definitions that are used on the view.
    /// </summary>
    List<IDecisionScreenDefinition> DecisionScreens { get; }

    /// <summary>
    /// Gets the collection of dialog definitions that are used on the view.
    /// </summary>
    List<IDialogDefinition> Dialogs { get; }

    /// <summary>
    /// Gets the collection of prompt dialog definitions that are used on the view.
    /// </summary>
    new List<IPromptDialogDefinition> PromptDialogs { get; }

    /// <summary>
    /// Gets or sets the view modes (Grid, List, TreeView, etc.).
    /// </summary>
    /// <value>The view modes.</value>
    Dictionary<string, IViewModeDefinition> ViewModes { get; }

    /// <summary>
    /// Gets the collection of link definitions that are used by the view.
    /// </summary>
    List<ILinkDefinition> Links { get; }

    /// <summary>Gets the toolbar.</summary>
    /// <value>The toolbar.</value>
    IWidgetBarDefinition Toolbar { get; }

    /// <summary>Gets the sidebar.</summary>
    /// <value>The sidebar.</value>
    IWidgetBarDefinition Sidebar { get; }

    /// <summary>Gets the context bar.</summary>
    /// <value>The context bar.</value>
    IWidgetBarDefinition ContextBar { get; }

    /// <summary>
    /// Gets or sets the css class that is applied to the grid.
    /// </summary>
    string GridCssClass { get; set; }

    /// <summary>
    /// Comma separated list of fields (properties) on which the search ought to be performed
    /// </summary>
    string SearchFields { get; set; }

    /// <summary>
    /// Comma separated list of fields (properties) of type <see cref="!:Lstring" /> on which the search ought to be performed
    /// </summary>
    string ExtendedSearchFields { get; set; }

    /// <summary>
    /// Filter expression that will show only items that aren't published or scheduled
    /// </summary>
    string DraftFilter { get; set; }

    /// <summary>
    /// Filter expression that will show only items that are published (live)
    /// </summary>
    string PublishedFilter { get; set; }

    /// <summary>
    /// Filter expresion that will show only items that are scheduled
    /// </summary>
    string ScheduledFilter { get; set; }

    /// <summary>
    /// Filter expresion that will show only items that are pending approval
    /// </summary>
    string PendingApprovalFilter { get; set; }

    /// <summary>
    /// Gets the title widget usually displayed near the title.
    /// </summary>
    /// <value>The title widget.</value>
    List<IWidgetDefinition> TitleWidgets { get; }

    /// <summary>
    /// Gets or sets the message to be shown when propting the user if they are sure they want to delete single item.
    /// </summary>
    string DeleteSingleConfirmationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message to be shown when propting the user if they are sure they want to delete multiple items.
    /// </summary>
    string DeleteMultipleConfirmationMessage { get; set; }

    /// <summary>
    /// If set to true, the MGV will not bind the current ItemsListBase on the client when the page is loaded. Default is false, i.e.
    /// it will bind. This is different from the current ItemsListBase.ClientBinder.BindOnLoad, which is always false and not affected by this property
    /// </summary>
    bool DoNotBindOnClientWhenPageIsLoaded { get; set; }

    /// <summary>
    /// Gets or sets a comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).
    /// </summary>
    /// <value>A comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).</value>
    string ProvidersGroups { get; set; }

    /// <summary>
    /// Gets or sets the ID of the page displaying the master view.
    /// </summary>
    Guid? LandingPageId { get; set; }
  }
}
