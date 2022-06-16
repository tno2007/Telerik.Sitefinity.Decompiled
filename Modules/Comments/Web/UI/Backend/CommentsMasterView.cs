// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsMasterView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend
{
  /// <summary>
  /// This is class that represent comments list view in backend
  /// </summary>
  internal class CommentsMasterView : SimpleScriptView, IDefinedControl
  {
    private IList<string> blackListedWindows;
    private IList<string> dialogIds;
    private PromptDialog deleteConfirmationDialog;
    private const string viewScript = "Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.CommentsMasterView.js";
    private const string sidebarId = "sidebar";
    private const string toolbarId = "toolbar";
    public static readonly string layoutTemplatePath = string.Empty;
    /// <summary>Common name used for a command that shows all items.</summary>
    public const string ShowAllCommentsCommandName = "showAllComments";
    public const string ShowOnlyCommentsCommandName = "showOnlyComments";
    public const string ShowRaviewsCommandName = "showReviews";
    public const string ShowMyCommentsCommandName = "showMyComments";
    public const string ShowWaitingApprovalItemsCommandName = "showWaitingApprovalItems";
    public const string ShowPublishedItemsCommandName = "showPublishedItems";
    public const string ShowHiddenItemsCommandName = "showHiddenItems";
    public const string ShowMarkedAsSpamItemsCommandName = "showMarkedAsSpamItems";
    public const string ShowInapropriateItemsCommandName = "showInapropriateItems";
    public const string FilterCommandName = "filter";
    private string selectedFilterItemCssClass = "sfSel";
    private Dictionary<string, DecisionScreen> decisionScreens;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsMasterView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The script control type name passed to the <see cref="T:System.Web.UI.ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected new virtual string ScriptDescriptorTypeName => typeof (CommentsMasterView).FullName;

    public IViewDefinition Definition { get; set; }

    /// <summary>Gets the black listed windows.</summary>
    protected IList<string> BlackListedWindows
    {
      get
      {
        if (this.blackListedWindows == null)
          this.blackListedWindows = (IList<string>) new List<string>();
        return this.blackListedWindows;
      }
    }

    /// <summary>Gets the sidebar.</summary>
    /// <value>The sidebar.</value>
    protected virtual WidgetBar Sidebar => this.Container.GetControl<WidgetBar>("sidebar", false);

    /// <summary>Gets the grid.</summary>
    /// <value>The grid.</value>
    protected virtual Control Grid => this.Container.GetControl<Control>(nameof (Grid), false);

    /// <summary>
    /// Reference to the RadWindowManager used to create and open dialogs
    /// </summary>
    protected virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>Gets the CommentsThreadHeaderWidget.</summary>
    /// <value>The CommentsThreadHeaderWidget.</value>
    protected virtual CommentsThreadHeaderWidget ContextBar => this.Container.GetControl<CommentsThreadHeaderWidget>("Header", false);

    /// <summary>Gets the sidebar.</summary>
    /// <value>The sidebar.</value>
    protected virtual HtmlAnchor BackToComments => this.Container.GetControl<HtmlAnchor>("backToComments", false);

    /// <summary>Gets or sets the toolbar.</summary>
    /// <value>The toolbar.</value>
    public WidgetBar Toolbar => this.Container.GetControl<WidgetBar>("toolbar", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container</param>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.Definition is ICommentsMasterViewDefinition definition)
      {
        this.ConfigureWidgetBar(this.Toolbar, "toolbar", definition.Toolbar);
        this.ConfigureWidgetBar(this.Sidebar, "sidebar", definition.Sidebar);
        this.dialogIds = (IList<string>) new List<string>();
        foreach (IDialogDefinition dialog in definition.Dialogs)
          this.ConstructDialog(dialog);
        if (this.ShouldShowDecisionScreens())
          this.SetDecisionScreens((IList<IDecisionScreenDefinition>) definition.DecisionScreens);
      }
      this.Controls.Add((Control) new UserPreferences());
      this.CreateConfirmationDialog();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private bool ShouldShowDecisionScreens()
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual)
      {
        CultureInfo[] frontendLanguages = appSettings.DefinedFrontendLanguages;
        ICommentService commentsService = SystemManager.GetCommentsService();
        CommentFilter commentFilter1 = new CommentFilter();
        commentFilter1.Take = new int?(1);
        CommentFilter commentFilter2 = commentFilter1;
        commentFilter2.GroupKey.AddRange((IEnumerable<string>) CommentsUtilities.GetGroupKeys());
        commentFilter2.Language.AddRange(((IEnumerable<CultureInfo>) frontendLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.ToString())));
        int num = 0;
        CommentFilter filter = commentFilter2;
        ref int local = ref num;
        commentsService.GetComments(filter, out local);
        if (num > 0)
          return false;
      }
      return true;
    }

    private void CreateConfirmationDialog()
    {
      this.deleteConfirmationDialog = new PromptDialog();
      this.deleteConfirmationDialog.Width = 350;
      this.deleteConfirmationDialog.Height = 300;
      this.deleteConfirmationDialog.Mode = PromptMode.Confirm;
      this.deleteConfirmationDialog.AllowCloseButton = true;
      this.deleteConfirmationDialog.ShowOnLoad = false;
      this.deleteConfirmationDialog.InputRows = 5;
      this.deleteConfirmationDialog.Message = Res.Get<Labels>().QuestionBeforeDeletingItem;
      Collection<CommandToolboxItem> commands1 = this.deleteConfirmationDialog.Commands;
      CommandToolboxItem commandToolboxItem1 = new CommandToolboxItem();
      commandToolboxItem1.CommandName = "delete";
      commandToolboxItem1.CommandType = CommandType.NormalButton;
      commandToolboxItem1.WrapperTagName = "LI";
      commandToolboxItem1.Text = Res.Get<Labels>().YesDelete;
      commandToolboxItem1.CssClass = "sfDelete";
      commands1.Add(commandToolboxItem1);
      Collection<CommandToolboxItem> commands2 = this.deleteConfirmationDialog.Commands;
      CommandToolboxItem commandToolboxItem2 = new CommandToolboxItem();
      commandToolboxItem2.CommandName = "delete";
      commandToolboxItem2.CssClass = "sfDelete";
      commandToolboxItem2.Text = Res.Get<Labels>().YesDeleteTheseItems;
      commandToolboxItem2.CommandType = CommandType.NormalButton;
      commands2.Add(commandToolboxItem2);
      Collection<CommandToolboxItem> commands3 = this.deleteConfirmationDialog.Commands;
      CommandToolboxItem commandToolboxItem3 = new CommandToolboxItem();
      commandToolboxItem3.CommandName = "cancel";
      commandToolboxItem3.Text = Res.Get<Labels>().Cancel;
      commandToolboxItem3.CommandType = CommandType.CancelButton;
      commandToolboxItem3.WrapperTagName = "LI";
      commands3.Add(commandToolboxItem3);
      this.Container.Controls.Add((Control) this.deleteConfirmationDialog);
    }

    /// <summary>Sets and configures a widgetBar control.</summary>
    /// <param name="widgetBar">The widget bar.</param>
    /// <param name="widgetBarID">The ID of the widget bar.</param>
    /// <param name="widgetBarDefinition">The widget bar definition.</param>
    /// <returns></returns>
    internal bool ConfigureWidgetBar(
      WidgetBar widgetBar,
      string widgetBarID,
      IWidgetBarDefinition widgetBarDefinition)
    {
      bool hasSections = widgetBarDefinition.HasSections;
      if (hasSections)
      {
        if (widgetBar == null)
        {
          ControlCollection controls = this.Container.GetControl<Control>(widgetBarID + "Panel", true).Controls;
          WidgetBar child = new WidgetBar();
          child.ID = widgetBarID;
          controls.Add((Control) child);
        }
        widgetBarDefinition = (IWidgetBarDefinition) widgetBarDefinition.GetDefinition<WidgetBarDefinition>();
        foreach (IWidgetBarSectionDefinition section in widgetBarDefinition.Sections)
        {
          foreach (IWidgetDefinition widgetDefinition in section.Items)
          {
            widgetDefinition.Visible = new bool?(this.IsVisible(widgetDefinition as IModuleDependentItem));
            if (widgetDefinition is IModeStateWidgetDefinition)
            {
              foreach (IStateCommandWidgetDefinition state in ((IStateWidgetDefinition) widgetDefinition).States)
                this.ConfigureViewModeSwitchingLink(state);
            }
          }
        }
        widgetBar.WidgetBarDefiniton = widgetBarDefinition;
        ITextControl control = this.Container.GetControl<ITextControl>(widgetBarID + "Title", false);
        if (control != null)
          control.Text = this.GetLabel(widgetBarDefinition.ResourceClassId, widgetBarDefinition.Title);
      }
      return hasSections;
    }

    private bool IsVisible(IModuleDependentItem item) => item == null || SystemManager.ValidateModuleItem(item);

    private void ConfigureViewModeSwitchingLink(IStateCommandWidgetDefinition stateCommandWidget)
    {
      stateCommandWidget.IsSelected = true;
      if (stateCommandWidget.IsSelected)
      {
        stateCommandWidget.NavigateUrl = string.Empty;
      }
      else
      {
        string rawUrl = SystemManager.CurrentHttpContext.Request.RawUrl;
        string empty = string.Empty;
        int length = rawUrl.IndexOf('?');
        if (length <= -1)
          return;
        rawUrl.Right(length - 1);
        rawUrl.Left(length);
      }
    }

    private bool IsActionRestricted(ICommandWidgetDefinition action)
    {
      foreach (IRestrictionStrategy restrictionStrategy in ObjectFactory.Container.ResolveAll<ICommandWidgetRestrictionStrategy>())
      {
        if (restrictionStrategy.IsRestricted((object) action))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Sets the properties of the decision screens in the template.
    /// </summary>
    /// <param name="decisionScreens">The decision screens.</param>
    internal void SetDecisionScreens(IList<IDecisionScreenDefinition> dsDefinitions)
    {
      if (dsDefinitions.Count <= 0)
        return;
      Control control = this.Container.GetControl<Control>("decisionScreens", false);
      if (control == null)
        return;
      this.decisionScreens = new Dictionary<string, DecisionScreen>();
      foreach (IDecisionScreenDefinition dsDefinition in (IEnumerable<IDecisionScreenDefinition>) dsDefinitions)
      {
        DecisionScreen child = new DecisionScreen();
        child.DecisionType = dsDefinition.DecisionType;
        if (!string.IsNullOrEmpty(dsDefinition.Name))
          ControlUtilities.SetControlIdFromName(dsDefinition.Name, (Control) child);
        bool? displayed = dsDefinition.Displayed;
        if (displayed.HasValue)
        {
          DecisionScreen decisionScreen = child;
          displayed = dsDefinition.Displayed;
          int num = displayed.Value ? 1 : 0;
          decisionScreen.Displayed = num != 0;
        }
        foreach (ICommandWidgetDefinition action in dsDefinition.Actions)
        {
          ActionItem actionItem = new ActionItem();
          actionItem.CssClass = action.CssClass;
          if (ClaimsManager.GetCurrentIdentity().IsUnrestricted && !this.IsActionRestricted(action))
          {
            actionItem.CommandName = action.CommandName;
            actionItem.Title = this.GetLabel(action.ResourceClassId, action.Text);
          }
          else
            actionItem.CssClass += " sfDisabled";
          child.ActionItems.Add(actionItem);
        }
        child.MessageText = this.GetLabel(dsDefinition.ResourceClassId, dsDefinition.MessageText);
        child.MessageType = dsDefinition.MessageType;
        child.Title = this.GetLabel(dsDefinition.ResourceClassId, dsDefinition.Title);
        control.Controls.Add((Control) child);
        if (!this.decisionScreens.ContainsKey(dsDefinition.DecisionType.ToString()))
          this.decisionScreens.Add(dsDefinition.DecisionType.ToString(), child);
      }
    }

    protected void ConstructDialog(IDialogDefinition dialog)
    {
      Telerik.Web.UI.RadWindow control = new Telerik.Web.UI.RadWindow();
      control.ID = dialog.OpenOnCommandName;
      control.Behaviors = dialog.Behaviors;
      control.InitialBehaviors = dialog.InitialBehaviors;
      control.AutoSizeBehaviors = dialog.AutoSizeBehaviors;
      control.Width = dialog.Width;
      control.Height = dialog.Height;
      control.VisibleTitlebar = dialog.VisibleTitleBar;
      control.VisibleStatusbar = dialog.VisibleStatusBar;
      control.NavigateUrl = dialog.NavigateUrl;
      control.Modal = dialog.IsModal;
      Telerik.Web.UI.RadWindow radWindow1 = control;
      bool? nullable;
      int num1;
      if (!dialog.ReloadOnShow.HasValue)
      {
        num1 = 0;
      }
      else
      {
        nullable = dialog.ReloadOnShow;
        num1 = nullable.Value ? 1 : 0;
      }
      radWindow1.ReloadOnShow = num1 != 0;
      Telerik.Web.UI.RadWindow radWindow2 = control;
      nullable = dialog.DestroyOnClose;
      int num2;
      if (!nullable.HasValue)
      {
        num2 = 0;
      }
      else
      {
        nullable = dialog.DestroyOnClose;
        num2 = nullable.Value ? 1 : 0;
      }
      radWindow2.DestroyOnClose = num2 != 0;
      if (!string.IsNullOrEmpty(dialog.Skin))
        control.Skin = dialog.Skin;
      if (!string.IsNullOrEmpty(dialog.CssClass))
        control.CssClass = dialog.CssClass;
      nullable = dialog.IsBlackListed;
      if (nullable.HasValue)
      {
        nullable = dialog.IsBlackListed;
        if (nullable.Value)
          this.BlackListedWindows.Add(dialog.OpenOnCommandName);
      }
      this.WindowManager.Windows.Add(control);
      this.dialogIds.Add(control.ID);
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddProperty("showAllCommentsCommandName", (object) "showAllComments");
      controlDescriptor.AddProperty("showOnlyCommentsCommandName", (object) "showOnlyComments");
      controlDescriptor.AddProperty("showReviewsCommandName", (object) "showReviews");
      controlDescriptor.AddProperty("showMyCommentsCommandName", (object) "showMyComments");
      controlDescriptor.AddProperty("showWaitingApprovalItemsCommandName", (object) "showWaitingApprovalItems");
      controlDescriptor.AddProperty("showPublishedItemsCommandName", (object) "showPublishedItems");
      controlDescriptor.AddProperty("showHiddenItemsCommandName", (object) "showHiddenItems");
      controlDescriptor.AddProperty("showMarkedAsSpamItemsCommandName", (object) "showMarkedAsSpamItems");
      controlDescriptor.AddProperty("showInapropriateItemsCommandName", (object) "showInapropriateItems");
      controlDescriptor.AddProperty("filterCommandName", (object) "filter");
      controlDescriptor.AddProperty("settingsLink", (object) RouteHelper.ResolveUrl(RouteHelper.CreateNodeReference(SiteInitializer.AdvancedSettingsNodeId) + "/CommentsModule", UrlResolveOptions.Rooted));
      controlDescriptor.AddProperty("showSectionsExceptAndResetFilterCommandName", (object) "showSectionsExceptAndResetFilter");
      controlDescriptor.AddProperty("_currentUserId", (object) SecurityManager.GetCurrentUserId());
      controlDescriptor.AddProperty("_selectedItemFilterCssClass", (object) this.selectedFilterItemCssClass);
      controlDescriptor.AddProperty("_commentsUrl", (object) this.Page.Request.Url.AbsolutePath);
      if (this.Toolbar != null)
        controlDescriptor.AddComponentProperty("toolbar", this.Toolbar.ClientID);
      if (this.Sidebar != null)
        controlDescriptor.AddComponentProperty("sidebar", this.Sidebar.ClientID);
      if (this.Grid != null)
        controlDescriptor.AddComponentProperty("commentsGrid", this.Grid.ClientID);
      if (this.BackToComments != null)
        controlDescriptor.AddElementProperty("backToComments", this.BackToComments.ClientID);
      if (this.ContextBar != null)
        controlDescriptor.AddComponentProperty("contextBar", this.ContextBar.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      controlDescriptor.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      controlDescriptor.AddProperty("blackListedWindows", (object) scriptSerializer.Serialize((object) this.BlackListedWindows));
      controlDescriptor.AddProperty("dialogIds", (object) scriptSerializer.Serialize((object) this.dialogIds));
      controlDescriptor.AddProperty("selectedLanguage", (object) (this.Context.Request.QueryString["lang"] ?? SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name));
      controlDescriptor.AddProperty("multiLingual", (object) SystemManager.CurrentContext.AppSettings.Multilingual);
      controlDescriptor.AddComponentProperty("deleteConfirmationDialog", this.deleteConfirmationDialog.ClientID);
      if (this.decisionScreens != null)
      {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (KeyValuePair<string, DecisionScreen> decisionScreen in this.decisionScreens)
          dictionary.Add(decisionScreen.Key, decisionScreen.Value.ClientID);
        string str = scriptSerializer.Serialize((object) dictionary);
        controlDescriptor.AddProperty("decisionScreens", (object) str);
      }
      controlDescriptor.AddProperty("singleItemDeleteMsg", (object) Res.Get<Labels>().QuestionBeforeDeletingItem);
      controlDescriptor.AddProperty("multipleItemsDeleteMsg", (object) Res.Get<Labels>().ItemsAreAboutToBeDeleted);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = typeof (CommentsMasterView).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsRestClient.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Scripts.CommentsMasterView.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
