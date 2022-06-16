// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolBar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Ldap;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Components;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Workflow.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Represents the tool bar in page editor.</summary>
  public class EditorToolBar : SimpleView, IScriptControl
  {
    private Control personlizationSelector;
    private IList<Control> editorToolbarSelectors = (IList<Control>) new List<Control>();
    private IDictionary<string, string> externalClientScripts;
    private List<IEditorToolbarPlugin> plugins;
    /// <summary>The layout template path.</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.EditorToolBar.ascx");
    private Label statusLabel;
    private IAppSettings settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolBar" /> class.
    /// </summary>
    public EditorToolBar()
    {
      this.MediaType = DesignMediaType.Page;
      this.settings = SystemManager.CurrentContext.AppSettings;
      this.LayoutTemplatePath = EditorToolBar.layoutTemplatePath;
    }

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the draft ID for the first-level editable zones
    /// </summary>
    /// <value>The draft ID.</value>
    public Guid DraftId
    {
      get => this.ViewState[nameof (DraftId)] == null ? Guid.Empty : (Guid) this.ViewState[nameof (DraftId)];
      set => this.ViewState[nameof (DraftId)] = (object) value;
    }

    /// <summary>Gets or sets the page node ID</summary>
    /// <value>The page node ID.</value>
    public Guid PageNodeId
    {
      get => this.ViewState[nameof (PageNodeId)] == null ? Guid.Empty : (Guid) this.ViewState[nameof (PageNodeId)];
      set => this.ViewState[nameof (PageNodeId)] = (object) value;
    }

    /// <summary>Gets or sets the parent item ID.</summary>
    /// <value>The ID of the parent item.</value>
    public Guid ParentItemId
    {
      get => this.ViewState[nameof (ParentItemId)] == null ? Guid.Empty : (Guid) this.ViewState[nameof (ParentItemId)];
      set => this.ViewState[nameof (ParentItemId)] = (object) value;
    }

    internal Telerik.Sitefinity.Localization.LocalizationStrategy? LocalizationStrategy
    {
      get => this.ViewState[nameof (LocalizationStrategy)] == null ? new Telerik.Sitefinity.Localization.LocalizationStrategy?() : new Telerik.Sitefinity.Localization.LocalizationStrategy?((Telerik.Sitefinity.Localization.LocalizationStrategy) this.ViewState[nameof (LocalizationStrategy)]);
      set => this.ViewState[nameof (LocalizationStrategy)] = (object) value;
    }

    /// <summary>
    /// Gets the personalization master id of the current page.
    /// </summary>
    public Guid PersonalizationMasterId
    {
      get
      {
        if (this.Proxy.PageProvider == null)
          return Guid.Empty;
        PageData pageData = PageManager.GetManager(this.Proxy.PageProvider.Name).GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.Id == this.ParentItemId)).SingleOrDefault<PageData>();
        if (pageData == null)
          return Guid.Empty;
        return pageData.PersonalizationMasterId == Guid.Empty ? this.ParentItemId : pageData.PersonalizationMasterId;
      }
    }

    /// <summary>Gets or sets the page title.</summary>
    /// <value>The page title.</value>
    public string PageTitle
    {
      get => this.ViewState[nameof (PageTitle)] == null ? string.Empty : (string) this.ViewState[nameof (PageTitle)];
      set => this.ViewState[nameof (PageTitle)] = (object) value;
    }

    /// <summary>Gets or sets the title used in Locking Dialog.</summary>
    /// <value>The title used in Locking Dialog.</value>
    public string Title { get; set; }

    /// <summary>Gets or sets the ZoneEditor ID.</summary>
    /// <value>The ZoneEditor ID.</value>
    public string ZoneEditorID
    {
      get => this.ViewState[nameof (ZoneEditorID)] == null ? string.Empty : (string) this.ViewState[nameof (ZoneEditorID)];
      set => this.ViewState[nameof (ZoneEditorID)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this page represents a template.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance is template; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    public DesignMediaType ProxyType
    {
      get => (DesignMediaType) this.ViewState[nameof (ProxyType)];
      set => this.ViewState[nameof (ProxyType)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this page is backend.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this page is backend; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    public bool IsBackend
    {
      get => (bool) (this.ViewState[nameof (IsBackend)] != null ? this.ViewState[nameof (IsBackend)] : (object) false);
      set => this.ViewState[nameof (IsBackend)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a dictionary of external scripts to use with the MasterView. The key of each
    /// element is the virtual path to the external script, while the value is the name of a method in
    /// that external script that will handle the MasterView's ViewLoaded event.
    /// </summary>
    /// <value>The dictionary of external client scripts.</value>
    public IDictionary<string, string> ExternalClientScripts
    {
      get
      {
        if (this.externalClientScripts == null)
          this.externalClientScripts = (IDictionary<string, string>) new Dictionary<string, string>();
        return this.externalClientScripts;
      }
      set => this.externalClientScripts = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this page represents a template.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance is template; otherwise, <c>false</c>.
    /// </value>
    public DesignMediaType MediaType { get; set; }

    /// <summary>
    /// Gets or sets a string representing the status of the page in the UI.
    /// </summary>
    public string PageStatusText
    {
      get => this.ViewState[nameof (PageStatusText)] != null ? (string) this.ViewState[nameof (PageStatusText)] : string.Empty;
      set
      {
        this.ViewState[nameof (PageStatusText)] = (object) value;
        this.StatusLabel.Text = value;
      }
    }

    /// <summary>Gets a value indicating whether it has status.</summary>
    public bool HasStatus => this.MediaType == DesignMediaType.Page || this.MediaType == DesignMediaType.Form;

    /// <summary>
    /// Gets or sets a value indicating whether the item is visible.
    /// </summary>
    public bool ItemVisible { get; set; }

    /// <summary>Gets or sets the version of the content item.</summary>
    public int ItemVersion { get; set; }

    /// <summary>Gets or sets the status of the content.</summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the URL of the preview page of the latest published version.
    /// </summary>
    public string PreviewPublishedUrl { get; set; }

    /// <summary>
    /// Gets or sets the link that will be opened when the Cancel button is clicked
    /// </summary>
    public string CancelUrl { get; set; }

    /// <summary>Gets or sets the workflow item state.</summary>
    public string WorkflowItemState { get; set; }

    /// <summary>Gets the configured plug-ins for this tool bar.</summary>
    /// <value>The configured for this control plug-ins.</value>
    internal IList<IEditorToolbarPlugin> Plugins
    {
      get
      {
        if (this.plugins == null)
          this.plugins = new List<IEditorToolbarPlugin>(ObjectFactory.Container.ResolveAll<IEditorToolbarPlugin>());
        return (IList<IEditorToolbarPlugin>) this.plugins;
      }
    }

    /// <summary>Gets or sets whether legacy controls should be hidden</summary>
    internal bool HideLegacyControls { get; set; }

    /// <summary>
    /// Converts a control ID used in conditional templates according to this.MediaType
    /// </summary>
    /// <param name="originalName">Original ID of the control</param>
    /// <returns>Unique control ID</returns>
    protected virtual string GetConditionalControlName(string originalName)
    {
      string lower = this.MediaType.ToString().ToLower();
      return originalName + "_" + lower;
    }

    /// <summary>
    /// Shortcut for this.Container.GetControl(this.GetConditionalControlName(originalName), required)
    /// </summary>
    /// <typeparam name="T">Type of the control to load</typeparam>
    /// <param name="originalName">Original ID of the control</param>
    /// <param name="required">Throw exception if control is not found and this parameter is true</param>
    /// <returns>Loaded control</returns>
    protected T GetConditionalControl<T>(string originalName, bool required) => this.Container.GetControl<T>(this.GetConditionalControlName(originalName), required);

    /// <summary>Gets or sets the language toolbar.</summary>
    public LanguageToolBar LanguageToolbar { get; set; }

    /// <summary>Gets the main tool bar.</summary>
    /// <value>The main tool bar.</value>
    public virtual RadToolBar MainToolBar => this.Container.GetControl<RadToolBar>(nameof (MainToolBar), true);

    /// <summary>Gets the reference to the workflow menu.</summary>
    public virtual WorkflowMenu WorkflowMenu => this.Container.GetControl<WorkflowMenu>("workflowMenu", true);

    /// <summary>
    /// Gets the field that is responsible for showing the workflow status.
    /// </summary>
    /// <value>The workflow status field.</value>
    public virtual ContentWorkflowStatusInfoField WorkflowStatusField => this.Container.GetControl<ContentWorkflowStatusInfoField>("workflowStatusField", true);

    /// <summary>Gets the warnings field.</summary>
    public virtual WarningField Warnings => this.Container.GetControl<WarningField>("warningsField", true);

    /// <summary>Gets the reference to the NON workflow menu.</summary>
    public virtual Control NonWorkflowMenu => this.Container.GetControl<Control>("nonWorkflowMenu", false);

    /// <summary>Gets the left tool bar.</summary>
    /// <value>The left tool bar.</value>
    public virtual RadToolBar LeftToolBar => this.GetConditionalControl<RadToolBar>(nameof (LeftToolBar), true);

    /// <summary>Gets the title label.</summary>
    /// <value>The title label.</value>
    public virtual Label TitleControl => this.Container.GetControl<Label>("Title", true);

    /// <summary>Gets the back button.</summary>
    /// <value>The back button.</value>
    public virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the client id of LeftToolbar</summary>
    public virtual HiddenField LeftToolbarClientID => this.Container.GetControl<HiddenField>(nameof (LeftToolbarClientID), true);

    /// <summary>Gets the client id of ZoneEditor</summary>
    public virtual HiddenField ZoneEditorClientID => this.Container.GetControl<HiddenField>(nameof (ZoneEditorClientID), true);

    /// <summary>Gets the status label.</summary>
    /// <value>The status label.</value>
    public virtual Label StatusLabel
    {
      get
      {
        if (this.statusLabel == null && this.HasStatus)
        {
          foreach (Control control in this.MainToolBar.FindItemByValue("Status").Controls)
          {
            if (control.ID == nameof (StatusLabel))
            {
              this.statusLabel = (Label) control;
              break;
            }
          }
        }
        return this.statusLabel;
      }
    }

    /// <summary>
    /// Gets a reference to the window manager on the template.
    /// </summary>
    /// <value>The window manager.</value>
    private RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Gets a reference to the window manager on the template.
    /// </summary>
    /// <value>The window manager.</value>
    private ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the loading panel control.</summary>
    public RadAjaxLoadingPanel ToolbarLoadingPanel => this.Container.GetControl<RadAjaxLoadingPanel>("toolbarLoadingPanel", false);

    /// <summary>Gets the placeholder for the language toolbar.</summary>
    public PlaceHolder LanguageToolbarPlaceHolder => this.Container.GetControl<PlaceHolder>("languageToolbarPlaceHolder", true);

    /// <summary>Gets the toolbar wrapper.</summary>
    /// <value>The toolbar wrapper.</value>
    public HtmlGenericControl ToolbarWrapper => this.Container.GetControl<HtmlGenericControl>("toolbarWrapper", false);

    /// <summary>Gets the dialog for single template in use.</summary>
    public virtual PromptDialog SingleTemplateInUseDialog => this.Container.GetControl<PromptDialog>("singleTemplateInUseDialog", true);

    /// <summary>Gets the dialog for cannot modify page.</summary>
    public virtual PromptDialog CannotModifyPageDialog => this.Container.GetControl<PromptDialog>("cannotModifyPageDialog", true);

    public virtual PromptDialog ConfirmDiscardOverride => this.Container.GetControl<PromptDialog>("confirmDiscardOverride", true);

    /// <summary>
    /// Gets the confirmation dialog for deleting edited widget.
    /// </summary>
    public virtual PromptDialog DeleteEditedWidgetConfirmationDialog => this.Container.GetControl<PromptDialog>("deleteEditedWidgetConfirmationDialog", true);

    /// <summary>Gets the delete confirmation dialog</summary>
    public virtual PromptDialog DeleteConfirmationDialog => this.Container.GetControl<PromptDialog>("deleteConfirmationDialog", true);

    /// <summary>Gets the message pane</summary>
    internal virtual HtmlGenericControl MessagePane => this.Container.GetControl<HtmlGenericControl>("sfMessagePane", false);

    /// <summary>
    /// Gets the reference to the placeholder where personalization selector will be loaded
    /// if needed.
    /// </summary>
    protected virtual PlaceHolder PersonalizationSelectorPlaceholder => this.Container.GetControl<PlaceHolder>("personalizationSelectorPlaceholder", true);

    /// <summary>
    /// Gets the reference to the placeholder editor toolbar selectors will be loaded if needed.
    /// </summary>
    protected virtual PlaceHolder EditorToolbarSelectorsPlaceholder => this.Container.GetControl<PlaceHolder>(nameof (EditorToolbarSelectorsPlaceholder), true);

    internal LockingHandler LockingHandler { get; set; }

    internal DraftProxyBase Proxy { get; set; }

    internal string ViewUrl { get; set; }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.PluginsPreRender();
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      string s = this.PageTitle;
      if (this.PageTitle.Length > 20)
        s = this.PageTitle.Substring(0, 20) + "...";
      if (this.settings.Multilingual && (this.MediaType == DesignMediaType.Form || this.MediaType == DesignMediaType.Page || this.MediaType == DesignMediaType.Template))
      {
        CultureInfo currentObjectCulture = this.Proxy.CurrentObjectCulture;
        s = s + " (" + currentObjectCulture.Name + ")";
      }
      this.TitleControl.Text = HttpUtility.HtmlEncode(s);
      this.TitleControl.ToolTip = this.PageTitle;
      foreach (object obj in (StateManagedCollection) this.MainToolBar.Items)
      {
        if (obj is RadToolBarButton)
        {
          RadToolBarButton radToolBarButton = obj as RadToolBarButton;
          string str = radToolBarButton.Value;
          if (!(str == "Preview"))
          {
            if (!(str == "Cancel"))
            {
              if (!(str == "Publish"))
              {
                if (!(str == "Delete"))
                {
                  if (str == "SaveDraft")
                    radToolBarButton.Attributes.Add("onclick", "editorToolBar.saveDraft();");
                }
                else if (this.MediaType == DesignMediaType.Template)
                  radToolBarButton.Attributes.Add("onclick", "editorToolBar.deleteItem();");
              }
              else
                radToolBarButton.Attributes.Add("onclick", "editorToolBar.publishDraft();");
            }
            else
            {
              if (this.Proxy.ShowLocalizationStrategySelector)
                radToolBarButton.FindControl("orLabel").Visible = false;
              HyperLink control = (HyperLink) radToolBarButton.FindControl("CancelButton");
              if (control == null)
                throw new FormatException("Missing required control \"CancelButton\".");
              string cancelText = this.GetCancelText();
              control.Text = string.IsNullOrWhiteSpace(cancelText) ? "Back" : cancelText;
              control.Attributes.Add("onclick", "editorToolBar.cancelDraftEditing();");
            }
          }
          else
          {
            radToolBarButton.NavigateUrl = this.ViewUrl;
            radToolBarButton.Target = "_blank";
          }
        }
      }
      RadToolBarItem radToolBarItem = (RadToolBarItem) null;
      if (this.MediaType == DesignMediaType.Form)
        radToolBarItem = this.LeftToolBar.FindItemByValue("Themes");
      else if (this.MediaType == DesignMediaType.NewsletterTemplate || this.MediaType == DesignMediaType.NewsletterCampaign)
      {
        this.MainToolBar.FindItemByValue("SaveDraft").Visible = false;
        if (this.LanguageToolbar != null)
          this.LanguageToolbar.Visible = false;
        this.MainToolBar.FindItemByValue("Preview").Visible = false;
        RadToolBarButton itemByValue1 = (RadToolBarButton) this.LeftToolBar.FindItemByValue("Preview");
        itemByValue1.NavigateUrl = this.ViewUrl;
        itemByValue1.Target = "_blank";
        RadToolBarItem itemByValue2 = this.MainToolBar.FindItemByValue("Publish");
        if (this.MediaType == DesignMediaType.NewsletterCampaign)
        {
          itemByValue2.Visible = false;
          this.MainToolBar.FindItemByValue("SaveCampaignDraft").Visible = true;
          this.MainToolBar.FindItemByValue("SendCampaign").Visible = true;
          this.MainToolBar.FindItem((Predicate<RadToolBarItem>) (m => m.CssClass == "campaignsMoreActions")).Visible = true;
        }
        else if (this.MediaType == DesignMediaType.NewsletterTemplate)
          itemByValue2.Text = Res.Get<Labels>().Save;
      }
      else
        radToolBarItem = this.LeftToolBar.FindItemByValue("Settings");
      if (radToolBarItem != null)
        radToolBarItem.Visible = false;
      if (this.MediaType == DesignMediaType.Template)
      {
        if (this.Proxy is TemplateDraftProxy proxy && proxy.Framework == PageTemplateFramework.Mvc)
        {
          RadToolBarItem itemByValue3 = this.LeftToolBar.FindItemByValue("Themes");
          RadToolBarItem itemByValue4 = this.LeftToolBar.FindItemByValue("Layout");
          if (itemByValue3 != null)
          {
            itemByValue3.Visible = false;
            itemByValue4.CssClass += " sfTwoSwitchers";
          }
        }
        bool flag = new TemplateInitializer((PageManager) null).IsDefaultTemplate(this.ParentItemId);
        RadToolBarItem itemByValue5 = this.LeftToolBar.FindItemByValue("restoreTemplateToDefault");
        if (itemByValue5 != null)
          itemByValue5.Visible = flag;
        RadToolBarItem itemByValue6 = this.MainToolBar.FindItemByValue("Delete");
        if (itemByValue6 != null)
        {
          string str = (itemByValue6.CssClass ?? string.Empty).Replace("sfDisplayNoneImportant", string.Empty);
          itemByValue6.CssClass = str;
        }
      }
      if (this.MediaType == DesignMediaType.Template || this.MediaType == DesignMediaType.Form || this.MediaType == DesignMediaType.NewsletterCampaign || this.MediaType == DesignMediaType.NewsletterTemplate)
      {
        this.WorkflowMenu.Visible = false;
        this.NonWorkflowMenu.Visible = true;
      }
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.DialogManager).RegisterScriptControl<EditorToolBar>(this);
      this.LeftToolbarClientID.Value = this.LeftToolBar.ClientID;
      this.ZoneEditorClientID.Value = this.ZoneEditorID;
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The dialog container.</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.HandleLegacyControls();
      this.InitializeFormsEditTemplate();
      this.InitializePlugins();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null && multisiteContext.GetSites().Count<Telerik.Sitefinity.Multisite.ISite>() >= 2)
        this.Controls.Add((Control) new BackendMultisiteSessionControl());
      if (this.LanguageToolbar != null)
        this.LanguageToolbarPlaceHolder.Controls.Add((Control) this.LanguageToolbar);
      if (this.Proxy.ShowLocalizationStrategySelector)
      {
        this.LeftToolBar.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
        foreach (WebControl webControl in new List<RadToolBarItem>()
        {
          this.MainToolBar.FindItemByValue("Publish"),
          this.MainToolBar.FindItemByValue("SaveDraft"),
          this.MainToolBar.FindItemByValue("Preview"),
          this.MainToolBar.FindItemByValue("Status")
        })
          webControl.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none !important");
      }
      RadWindowManager control = this.Container.GetControl<RadWindowManager>("windowManager", true);
      for (int index = 0; index < control.Windows.Count; ++index)
      {
        Telerik.Web.UI.RadWindow window = control.Windows[index];
        string str1 = this.IsBackend ? "BackendPages" : "FrontendPages";
        string empty = string.Empty;
        string url = string.Empty;
        switch (window.ID)
        {
          case "changePageOwner":
            url = "~/Sitefinity/Dialog/ChangePageOwnerDialog";
            break;
          case "changeTemplate":
            url = "~/Sitefinity/Dialog/TemplateSelectorDialog";
            break;
          case "duplicate":
            string str2;
            if (this.ProxyType == DesignMediaType.Template)
            {
              str1 = this.IsBackend ? "BackendPageTemplates" : "FrontendPageTemplates";
              str2 = this.IsBackend ? "BackendPageTemplatesCreate" : "FrontendPageTemplatesCreate";
            }
            else
              str2 = this.IsBackend ? "BackendPagesCreate" : "FrontendPagesCreate";
            url = string.Format("~/Sitefinity/Dialog/ContentViewInsertDialog?ControlDefinitionName={0}&ViewName={1}", (object) str1, (object) str2);
            break;
          case "edit":
            string str3 = this.IsBackend ? "BackendPagesEdit" : "FrontendPagesEdit";
            url = string.Format("~/Sitefinity/Dialog/ContentViewEditDialog?ControlDefinitionName={0}&ViewName={1}", (object) str1, (object) str3);
            break;
          case "history":
            url = "~/Sitefinity/Dialog/PageVersionHistoryDialog?DisableComparisonForVersions=true";
            break;
          case "permissions":
            string str4 = (string) null;
            string str5 = string.Empty;
            string str6 = (string) null;
            string str7 = string.Empty;
            Guid guid;
            switch (this.ProxyType)
            {
              case DesignMediaType.Page:
                str4 = typeof (PageNode).AssemblyQualifiedName;
                str5 = typeof (PageManager).AssemblyQualifiedName;
                guid = this.PageNodeId;
                str6 = guid.ToString();
                str7 = this.PageTitle;
                break;
              case DesignMediaType.Template:
                str4 = typeof (PageTemplate).AssemblyQualifiedName;
                str5 = typeof (PageManager).AssemblyQualifiedName;
                guid = this.ParentItemId;
                str6 = guid.ToString();
                break;
              case DesignMediaType.Form:
                str4 = typeof (FormDescription).AssemblyQualifiedName;
                str5 = typeof (FormsManager).AssemblyQualifiedName;
                guid = this.ParentItemId;
                str6 = guid.ToString();
                break;
            }
            if (!string.IsNullOrWhiteSpace(str7))
            {
              url = string.Format("~/Sitefinity/Dialog/ModulePermissionsDialog?securedObjectID={0}&managerClassName={1}&securedObjectTypeName={2}&title={3}&showPermissionSetNameTitle={4}&backLabelText={5}", (object) str6, (object) str5, (object) str4, (object) this.PageTitle, (object) true, (object) Res.Get<Labels>().BackTo.Arrange((object) str7));
              break;
            }
            url = string.Format("~/Sitefinity/Dialog/ModulePermissionsDialog?securedObjectID={0}&managerClassName={1}&securedObjectTypeName={2}&title={3}&showPermissionSetNameTitle={4}", (object) str6, (object) str5, (object) str4, (object) this.PageTitle, (object) true);
            break;
          case "personalize":
            url = "~/Sitefinity/Dialog/PersonalizePageDialog";
            if (this.settings.Multilingual)
            {
              url = url + "?lang=" + (object) this.Proxy.CurrentObjectCulture;
              break;
            }
            break;
          case "versionPreview":
            url = string.Format("~/Sitefinity/Dialog/PageViewVersionDialog?IsTemplate={0}&IsFromEditor={1}", (object) (this.ProxyType == DesignMediaType.Template), (object) true);
            break;
        }
        window.NavigateUrl = RouteHelper.ResolveUrl(url, UrlResolveOptions.Rooted);
        string cancelText = this.GetCancelText();
        if (!string.IsNullOrWhiteSpace(cancelText))
          this.WorkflowMenu.CancelText = cancelText;
        RadToolBarItem itemByValue = this.MainToolBar.FindItemByValue("Status");
        if (itemByValue != null)
          itemByValue.Visible = this.HasStatus;
      }
      this.WorkflowMenu.CancelUrl = "javascript:editorToolBar.cancelDraftEditing();";
      this.WorkflowMenu.ReturnUrl = this.CancelUrl;
      this.WorkflowMenu.CancelText = Res.Get<PageResources>().BackToPages;
      if (this.MediaType == DesignMediaType.Page)
        this.WorkflowMenu.ShowCheckRelatingData = RelatedDataHelper.IsTypeSupportCheckRelatingData(typeof (PageNode));
      if (this.MediaType == DesignMediaType.NewsletterCampaign)
        dialogContainer.Controls.Add((Control) new ZoneEditorToolBarExtension(this));
      if (SystemManager.IsModuleAccessible("Personalization") && (this.MediaType == DesignMediaType.Page || this.MediaType == DesignMediaType.Template))
      {
        this.personlizationSelector = this.Page.LoadControl(TypeResolutionService.ResolveType("Telerik.Sitefinity.Personalization.Impl.Web.UI.Pages.PersonalizedPageSelector, Telerik.Sitefinity.Personalization.Impl"), (object[]) null);
        this.PersonalizationSelectorPlaceholder.Controls.Add(this.personlizationSelector);
      }
      foreach (EditorToolbarSelectorBase child in ObjectFactory.Container.ResolveAll(typeof (EditorToolbarSelectorBase)).Cast<EditorToolbarSelectorBase>())
      {
        if (!this.IsBackend && child.SupportedMediaTypes.Contains<DesignMediaType>(this.MediaType))
        {
          this.EditorToolbarSelectorsPlaceholder.Controls.Add((Control) child);
          this.editorToolbarSelectors.Add((Control) child);
        }
      }
      if (this.MediaType != DesignMediaType.Page || SystemManager.IsModuleAccessible("Personalization"))
        return;
      RadToolBar radToolBar = this.GetRadToolBar();
      if (radToolBar == null)
        return;
      RadToolBarButton radToolBarButton1 = (RadToolBarButton) null;
      foreach (object obj in (StateManagedCollection) radToolBar.Items)
      {
        if (obj is RadToolBarButton radToolBarButton2 && radToolBarButton2.Value == "personalize")
        {
          radToolBarButton1 = radToolBarButton2;
          break;
        }
      }
      if (radToolBarButton1 == null)
        return;
      radToolBarButton1.Visible = false;
    }

    private void InitializeFormsEditTemplate()
    {
      RadToolBar radToolBar = this.GetRadToolBar();
      if (radToolBar == null)
        return;
      RadToolBarItem itemByValue = radToolBar.FindItemByValue("editRules");
      if (itemByValue == null || this.Proxy.Framework == PageTemplateFramework.Mvc && !this.IsLdapUserWithSwtAuthenticationProtocol())
        return;
      itemByValue.Visible = false;
    }

    /// <summary>
    /// Overridden. Calls Evaluate on the conditional template container to correctly use the controls inside of the templates
    /// </summary>
    /// <param name="template">The template.</param>
    /// <returns>The container.</returns>
    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      container.GetControl<ConditionalTemplateContainer>("conditionalTemplateEditorToolbar", true).Evaluate((object) this);
      return container;
    }

    /// <summary>Gets the rad toolbar.</summary>
    /// <returns>The rad toolbar.</returns>
    internal RadToolBar GetRadToolBar()
    {
      radToolBar = (RadToolBar) null;
      ConditionalTemplateContainer control1 = this.Container.GetControl<ConditionalTemplateContainer>("conditionalTemplateEditorToolbar", false);
      if (control1 != null)
      {
        foreach (object control2 in control1.Controls)
        {
          if (control2 is ConditionalTemplate conditionalTemplate)
          {
            foreach (object control3 in conditionalTemplate.Controls)
            {
              if (control3 is RadToolBar radToolBar)
                break;
            }
          }
          if (radToolBar != null)
            break;
        }
      }
      return radToolBar;
    }

    private WcfPageData GetCurrentWcfPageData() => new WcfPageData(PageManager.GetManager(this.Proxy.PageProvider.Name).GetPageNode(this.PageNodeId));

    private PageTemplateViewModel GetCurrentWcfTemplateData() => new PageTemplateViewModel(PageManager.GetManager(this.Proxy.PageProvider.Name).GetTemplate(this.Proxy.ParentItemId));

    private string GetCancelText()
    {
      string cancelText;
      switch (this.ProxyType)
      {
        case DesignMediaType.Page:
          cancelText = Res.Get<PageResources>().BackToPages;
          break;
        case DesignMediaType.Template:
          cancelText = Res.Get<PageResources>().BackToTemplates;
          break;
        case DesignMediaType.Form:
          cancelText = Res.Get<FormsResources>().BackToForms;
          break;
        case DesignMediaType.NewsletterCampaign:
          cancelText = Res.Get<Labels>().Back;
          break;
        case DesignMediaType.NewsletterTemplate:
          cancelText = Res.Get<NewslettersResources>().BackToMessageTemplates;
          break;
        default:
          cancelText = Res.Get<Labels>().Cancel;
          break;
      }
      return cancelText;
    }

    private void InitializePlugins()
    {
      foreach (IEditorToolbarPlugin plugin in (IEnumerable<IEditorToolbarPlugin>) this.Plugins)
        plugin.Initialize(this);
    }

    private void PluginsPreRender()
    {
      foreach (IEditorToolbarPlugin plugin in (IEnumerable<IEditorToolbarPlugin>) this.Plugins)
        plugin.PreRender(this);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      bool flag1 = true;
      bool flag2 = true;
      behaviorDescriptor.AddProperty("cancelUrl", (object) this.CancelUrl);
      behaviorDescriptor.AddProperty("serviceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/ZoneEditorService.svc/"));
      behaviorDescriptor.AddProperty("draftId", (object) this.DraftId.ToString());
      behaviorDescriptor.AddProperty("mediaType", (object) this.ProxyType);
      behaviorDescriptor.AddProperty("_zoneEditorId", (object) this.ZoneEditorID);
      behaviorDescriptor.AddProperty("_lockingHandlerId", (object) this.LockingHandler.ClientID);
      behaviorDescriptor.AddProperty("_parentItemId", (object) this.ParentItemId.ToString());
      if (this.ProxyType == DesignMediaType.Form)
      {
        Guid parentItemId = this.ParentItemId;
        FormDraft formDraft = FormsManager.GetManager(this.Proxy.FormsProvider.Name).GetForm(parentItemId).Drafts.Where<FormDraft>((Func<FormDraft, bool>) (d => !d.IsTempDraft)).FirstOrDefault<FormDraft>();
        if (formDraft != null)
          behaviorDescriptor.AddProperty("_formMasterItemId", (object) formDraft.Id.ToString());
      }
      behaviorDescriptor.AddProperty("_personalizationMasterId", (object) this.PersonalizationMasterId.ToString());
      behaviorDescriptor.AddProperty("_pageTitle", (object) this.PageTitle);
      behaviorDescriptor.AddProperty("_localizationStrategy", (object) this.LocalizationStrategy);
      string str1 = this.Page.Request.Url.AbsolutePath;
      int length = str1.LastIndexOf("Action", StringComparison.InvariantCultureIgnoreCase);
      if (length != -1)
        str1 = str1.Substring(0, length);
      string str2 = str1.TrimEnd('/');
      behaviorDescriptor.AddProperty("pageUrl", (object) str2);
      behaviorDescriptor.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      behaviorDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      behaviorDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      if (this.personlizationSelector != null)
        behaviorDescriptor.AddComponentProperty("personalizationSelector", this.personlizationSelector.ClientID);
      if (this.editorToolbarSelectors != null)
        behaviorDescriptor.AddProperty("editorToolbarSelectors", (object) this.editorToolbarSelectors.Select<Control, string>((Func<Control, string>) (c => c.ClientID)));
      behaviorDescriptor.AddProperty("_hasStatus", (object) this.HasStatus);
      if (this.HasStatus)
      {
        behaviorDescriptor.AddProperty("_statusLabelId", (object) this.StatusLabel.ClientID);
        behaviorDescriptor.AddProperty("_wasPublished", (object) (this.ItemVersion > 0));
        behaviorDescriptor.AddProperty("_isPublished", (object) this.ItemVisible);
        behaviorDescriptor.AddProperty("_status", (object) this.PageStatusText);
      }
      if ((this.ProxyType == DesignMediaType.Page || this.ProxyType == DesignMediaType.Template) && this.Page.Items[(object) "theme"] is string themeName && themeName != "notheme" && ThemeController.GetThemeElement(themeName, this.Proxy.IsBackend, this.Page) == null)
        behaviorDescriptor.AddProperty("_missingThemeName", (object) themeName);
      switch (this.ProxyType)
      {
        case DesignMediaType.Page:
          behaviorDescriptor.AddProperty("baseItemServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/PagesService.svc/"));
          behaviorDescriptor.AddProperty("_takeOwnershipURL", (object) "Page/TakeOwnership/");
          behaviorDescriptor.AddProperty("_permissionsDialogUrl", (object) RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ModulePermissionsDialog", UrlResolveOptions.Rooted));
          behaviorDescriptor.AddProperty("_propertiesDialogUrl", (object) RouteHelper.ResolveUrl("~/Sitefinity/Dialog/PagePropertiesDialog", UrlResolveOptions.Rooted));
          behaviorDescriptor.AddProperty("_pageManagerType", (object) typeof (PageManager).AssemblyQualifiedName);
          behaviorDescriptor.AddProperty("_sectionManagerType", (object) typeof (TaxonomyManager).AssemblyQualifiedName);
          behaviorDescriptor.AddProperty("_pageDataSecuredObjectType", (object) typeof (PageData).AssemblyQualifiedName);
          behaviorDescriptor.AddProperty("_sectionSecuredObjectType", (object) typeof (HierarchicalTaxon).AssemblyQualifiedName);
          WcfPageData currentWcfPageData = this.GetCurrentWcfPageData();
          using (MemoryStream memoryStream = new MemoryStream())
          {
            new DataContractJsonSerializer(typeof (PageData), (IEnumerable<Type>) new Type[1]
            {
              currentWcfPageData.GetType()
            }).WriteObject((Stream) memoryStream, (object) currentWcfPageData);
            behaviorDescriptor.AddProperty("_dataItem", (object) Encoding.Default.GetString(memoryStream.ToArray()));
          }
          PageManager manager = PageManager.GetManager(this.Proxy.PageProvider.Name);
          PageNode node = (PageNode) null;
          if (this.Proxy is Telerik.Sitefinity.Modules.Pages.PageDraftProxy proxy1)
            node = proxy1.PageNode;
          if (node == null && this.PageNodeId != Guid.Empty)
            node = manager.GetPageNode(this.PageNodeId);
          bool flag3 = false;
          if (node != null)
          {
            behaviorDescriptor.AddProperty("_pageNodeId", (object) node.Id.ToString());
            flag1 = node.IsGranted("Pages", "Modify");
            flag2 = node.IsGranted("Pages", "EditContent");
            if (((IEnumerable<string>) node.AvailableLanguages).Count<string>() < 2)
              flag3 = SiteMapBase.GetSiteMapProviderForPageNode(node).FindSiteMapNodeFromKey(node.Id.ToString()).HasChildNodes;
          }
          behaviorDescriptor.AddProperty("_preventDeleteParentItem", (object) flag3);
          break;
        case DesignMediaType.Template:
          behaviorDescriptor.AddProperty("baseItemServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/PagesService.svc/"));
          behaviorDescriptor.AddProperty("_takeOwnershipURL", (object) "Template/TakeOwnership/");
          behaviorDescriptor.AddProperty("_templatesService", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/PageTemplatesService.svc/"));
          if (SystemManager.CurrentContext.AppSettings.Multilingual)
          {
            PageTemplateViewModel currentWcfTemplateData = this.GetCurrentWcfTemplateData();
            using (MemoryStream memoryStream = new MemoryStream())
            {
              new DataContractJsonSerializer(typeof (PageTemplate), (IEnumerable<Type>) new Type[1]
              {
                currentWcfTemplateData.GetType()
              }).WriteObject((Stream) memoryStream, (object) currentWcfTemplateData);
              behaviorDescriptor.AddProperty("_dataItem", (object) Encoding.Default.GetString(memoryStream.ToArray()));
            }
          }
          if (this.Proxy is TemplateDraftProxy proxy2)
          {
            behaviorDescriptor.AddProperty("_templatePagesCount", (object) proxy2.PagesCount);
            behaviorDescriptor.AddProperty("_templateTemplatesCount", (object) proxy2.TemplatesCount);
            behaviorDescriptor.AddProperty("_singleTemplateInUseDialogId", (object) this.SingleTemplateInUseDialog.ClientID);
            behaviorDescriptor.AddProperty("_deleteConfirmationDialogId", (object) this.DeleteConfirmationDialog.ClientID);
            break;
          }
          break;
        case DesignMediaType.Form:
          behaviorDescriptor.AddProperty("baseItemServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Forms/FormsService.svc/"));
          behaviorDescriptor.AddProperty("_takeOwnershipURL", (object) "Form/TakeOwnership/");
          break;
      }
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        behaviorDescriptor.AddProperty("currentLanguage", (object) this.Proxy.CurrentObjectCulture.Name);
        if (this.LanguageToolbar != null)
          behaviorDescriptor.AddComponentProperty("languageToolbar", this.LanguageToolbar.ClientID);
      }
      behaviorDescriptor.AddProperty("_isEditable", (object) flag1);
      behaviorDescriptor.AddProperty("_isContentEditable", (object) flag2);
      behaviorDescriptor.AddComponentProperty("toolbarLoadingPanel", this.ToolbarLoadingPanel.ClientID);
      behaviorDescriptor.AddComponentProperty("mainToolbar", this.MainToolBar.ClientID);
      behaviorDescriptor.AddElementProperty("toolbarWrapper", this.ToolbarWrapper.ClientID);
      behaviorDescriptor.AddProperty("_backToItemLabelTemplate", (object) Res.Get<Labels>().BackTo);
      foreach (string handler in (IEnumerable<string>) this.ExternalClientScripts.Values)
        behaviorDescriptor.AddEvent("viewLoaded", handler);
      string rootKey = (string) null;
      Type itemType;
      string name;
      if (this.MediaType == DesignMediaType.Form)
      {
        itemType = typeof (Form);
        name = this.Proxy.FormsProvider.Name;
      }
      else if (this.MediaType == DesignMediaType.Template)
      {
        itemType = typeof (PageTemplate);
        name = this.Proxy.PageProvider.Name;
      }
      else
      {
        itemType = typeof (PageNode);
        name = this.Proxy.PageProvider.Name;
        rootKey = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId.ToString();
      }
      if (this.WorkflowMenu.Visible)
      {
        behaviorDescriptor.AddComponentProperty("workflowMenu", this.WorkflowMenu.ClientID);
        behaviorDescriptor.AddComponentProperty("workflowStatusField", this.WorkflowStatusField.ClientID);
        behaviorDescriptor.AddProperty("workflowItemId", (object) this.Proxy.PageDraftId);
        behaviorDescriptor.AddProperty("workflowItemState", (object) this.WorkflowItemState);
        behaviorDescriptor.AddProperty("workflowItemType", (object) itemType.FullName);
      }
      behaviorDescriptor.AddProperty("_cannotModifyPageDialogId", (object) this.CannotModifyPageDialog.ClientID);
      behaviorDescriptor.AddComponentProperty("warningsField", this.Warnings.ClientID);
      Guid id = this.MediaType != DesignMediaType.Page ? (this.Proxy.ParentItemId != Guid.Empty ? this.Proxy.ParentItemId : this.Proxy.PageDraftId) : this.Proxy.PageNodeId;
      behaviorDescriptor.AddProperty("warnings", (object) SystemManager.StatusProviderRegistry.GetWarnings(id, itemType, name, rootKey, this.Proxy.CurrentObjectCulture));
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string str1 = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str1,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      });
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str1,
        Name = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.EditorToolBar.js"
      });
      foreach (string key in (IEnumerable<string>) this.ExternalClientScripts.Keys)
      {
        if (!string.IsNullOrEmpty(key))
        {
          if (key.IndexOf(',') > 0)
          {
            string[] strArray = key.Split(',');
            string str2 = strArray[0];
            string str3 = strArray[1];
            scriptReferences.Add(new ScriptReference()
            {
              Assembly = str3,
              Name = str2
            });
          }
          else if (key.StartsWith("~"))
          {
            List<ScriptReference> scriptReferenceList = scriptReferences;
            ScriptReference scriptReference = new ScriptReference();
            scriptReference.Path = key;
            scriptReferenceList.Add(scriptReference);
          }
          else
          {
            List<ScriptReference> scriptReferenceList = scriptReferences;
            ScriptReference scriptReference = new ScriptReference();
            scriptReference.Path = VirtualPathUtility.ToAppRelative(key);
            scriptReferenceList.Add(scriptReference);
          }
        }
      }
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private void HandleLegacyControls()
    {
      if (!this.HideLegacyControls)
        return;
      this.ToolbarWrapper.Style.Add("display", "none");
      if (this.LanguageToolbar != null)
        this.LanguageToolbar.Style.Add("display", "none");
      if (this.MessagePane != null)
      {
        foreach (Control control in this.MessagePane.Controls)
        {
          if (!(control is PlaceHolder))
            control.Visible = false;
        }
      }
      RadToolBar radToolBar = this.GetRadToolBar();
      if (radToolBar != null)
      {
        foreach (Control control in radToolBar.Controls)
        {
          if (!(control is RadToolBarButton radToolBarButton))
          {
            if (control != null)
              control.Visible = false;
          }
          else if (!(radToolBarButton.Value == "Content") && !(radToolBarButton.Value == "Layout"))
            radToolBarButton.Visible = false;
        }
      }
      this.MessagePane.Controls.Add((Control) radToolBar);
    }

    private bool IsLdapUserWithSwtAuthenticationProtocol()
    {
      string membershipProvider = ClaimsManager.GetCurrentIdentity().MembershipProvider;
      if (ClaimsManager.CurrentAuthenticationModule.AuthenticationProtocol.Equals("SimpleWebToken", StringComparison.OrdinalIgnoreCase))
      {
        try
        {
          return UserManager.GetManager(membershipProvider).Provider is ILdapProviderMarker;
        }
        catch (MissingProviderConfigurationException ex)
        {
        }
      }
      return false;
    }
  }
}
