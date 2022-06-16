// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views.CommentsDetailsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views
{
  /// <summary>Displays a comment create form on the frontend.</summary>
  [Obsolete("This frontend UI is replaced by the CommentsWidget.")]
  public class CommentsDetailsView : ViewBase
  {
    private CommentsSettingsWrapper settingsWrappper;
    private List<CommentsDetailsView.FieldControlData> fieldControls;
    private bool isAuthenticated;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.GenericContent.CommentsDetailsView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsDetailsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the comments settings.</summary>
    /// <value>The comments settings.</value>
    protected CommentsSettingsWrapper CommentsSettings
    {
      get
      {
        if (this.settingsWrappper == null)
        {
          IDataItem detailItem = this.Host.DetailItem;
          if (detailItem != null)
            this.settingsWrappper = new CommentsSettingsWrapper(detailItem, this.ContentViewDefinition.CommentsSettingsDefinition);
        }
        return this.settingsWrappper;
      }
      set => this.settingsWrappper = value;
    }

    /// <summary>Gets or sets the content view definition.</summary>
    /// <value>The content view definition.</value>
    private IContentViewDefinition ContentViewDefinition { get; set; }

    /// <summary>
    /// Gets or sets the field controls to be validated on submit.
    /// </summary>
    /// <value>The field controls.</value>
    private List<CommentsDetailsView.FieldControlData> FieldControls
    {
      get
      {
        if (this.fieldControls == null)
          this.fieldControls = new List<CommentsDetailsView.FieldControlData>();
        return this.fieldControls;
      }
      set => this.fieldControls = value;
    }

    /// <summary>
    /// Gets a value that indicates whether the user has been authenticated.
    /// </summary>
    /// <returns>true if the user was authenticated; otherwise, false.</returns>
    private bool IsAuthenticated
    {
      get
      {
        if (SystemManager.CurrentHttpContext != null)
          this.isAuthenticated = SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated;
        return this.isAuthenticated;
      }
    }

    /// <summary>
    /// Gets a value indicating whether posting comments is allowed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if posting comments is allowed; otherwise, <c>false</c>.
    /// </value>
    private bool AllowPostingComments
    {
      get
      {
        if (!this.CommentsSettings.AllowComments.GetValueOrDefault())
          return false;
        if (this.CommentsSettings.PostRights == PostRights.Everyone)
          return true;
        return this.CommentsSettings.PostRights == PostRights.RegisteredUsers && this.IsAuthenticated;
      }
    }

    /// <summary>
    /// Gets or sets the group key that will be used for association of the comment.
    /// </summary>
    private string GroupKey { set; get; }

    /// <summary>
    /// Gets or sets the thread key that will be used for association of the comment.
    /// </summary>
    private string ThreadKey { set; get; }

    /// <summary>
    /// Gets or sets the thread key that will be used for association of the comment.
    /// </summary>
    private string ThreadType { set; get; }

    /// <summary>Gets or sets the title.</summary>
    private string Title { set; get; }

    /// <summary>
    /// A text field for displaying the author name of the comment.
    /// </summary>
    protected virtual Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl AuthorNameControl => this.Container.GetControl<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>("authorName", true);

    /// <summary>A text field for displaying the email of the comment.</summary>
    protected virtual Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl EmailControl => this.Container.GetControl<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>("email", true);

    /// <summary>
    /// A text field for displaying the website of the author posting the comment.
    /// </summary>
    protected virtual Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl WebsiteControl => this.Container.GetControl<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>("website", true);

    /// <summary>An html field for displaying the comment text.</summary>
    protected virtual Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl ContentControl => this.Container.GetControl<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>("content", true);

    protected virtual ITextControl InvalidCommentMessage => this.Container.GetControl<ITextControl>("invalidCommentMessage", false);

    /// <summary>A button for submitting the comment.</summary>
    protected virtual IButtonControl SubmitButton => this.Container.GetControl<IButtonControl>("submitBtn", true);

    /// <summary>
    /// A control for protection against automated form submissions.
    /// </summary>
    protected virtual RadCaptcha Captcha => this.Container.GetControl<RadCaptcha>("captcha", true);

    /// <summary>Gets the moderation holder.</summary>
    /// <value>The moderation holder.</value>
    protected virtual Panel ModerationContainer => this.Container.GetControl<Panel>("moderationContainer", true);

    internal virtual Panel ClosedThreadContainer => this.Container.GetControl<Panel>("closedThreadContainer", true);

    /// <summary>Gets the authenticated user container.</summary>
    /// <value>The authenticated user container.</value>
    protected virtual Panel AuthenticatedUserContainer => this.Container.GetControl<Panel>("authenticatedUserContainer", true);

    /// <summary>Gets the anonymous user container.</summary>
    /// <value>The anonymous user container.</value>
    protected virtual Panel AnonymousUserContainer => this.Container.GetControl<Panel>(nameof (AnonymousUserContainer), true);

    /// <summary>
    /// Gets the reference to the li element wrappering AuthorNameControl.
    /// </summary>
    protected virtual HtmlGenericControl AuthorNameControlWrapper => this.Container.GetControl<HtmlGenericControl>("authorNameControlWrapper", true);

    /// <summary>
    /// Gets the reference to the li element wrappering EmailControl.
    /// </summary>
    protected virtual HtmlGenericControl EmailControlWrapper => this.Container.GetControl<HtmlGenericControl>("emailControlWrapper", true);

    /// <summary>
    /// Gets the reference to the li element wrappering WebsiteControl.
    /// </summary>
    protected virtual HtmlGenericControl WebsiteControlWrapper => this.Container.GetControl<HtmlGenericControl>("websiteControlWrapper", true);

    /// <summary>
    /// Gets the reference to the li element wrappering ContentControl.
    /// </summary>
    protected virtual HtmlGenericControl ContentControlWrapper => this.Container.GetControl<HtmlGenericControl>("contentControlWrapper", true);

    /// <summary>
    /// Gets the reference to the li element wrappering Captcha.
    /// </summary>
    protected virtual HtmlGenericControl CaptchaWrapper => this.Container.GetControl<HtmlGenericControl>("captchaWrapper", true);

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.Empty;

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
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (FormManager.GetCurrent(this.Page) == null)
        this.Controls.Add((Control) new FormManager());
      base.OnInit(e);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (this.Host.DetailItem != null && !(this.Host.DetailItem is ICommentable))
        throw new NotSupportedException("The detail item must be implementing the ICommentable interface.");
      this.ContentViewDefinition = definition;
      this.SubmitButton.Command += new CommandEventHandler(this.SubmitButton_Command);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.CommentsSettings != null)
      {
        if (this.AllowPostingComments)
        {
          this.InitFieldControls();
          this.FieldControls.ForEach((Action<CommentsDetailsView.FieldControlData>) (f => f.CofigureVisibility()));
          this.InitCaptcha(false);
        }
        else
        {
          this.AuthenticatedUserContainer.Visible = false;
          this.AnonymousUserContainer.Visible = this.CommentsSettings.AllowComments.GetValueOrDefault();
        }
      }
      if (this.Host.DetailItem == null)
        return;
      this.ThreadKey = ControlUtilities.GetLocalizedKey((object) this.Host.DetailItem.Id);
      ICommentService commentsService = SystemManager.GetCommentsService();
      CommentsUtilities.GetCurrentLanguage();
      string threadKey = this.ThreadKey;
      IThread thread = commentsService.GetThread(threadKey);
      if (thread == null || !thread.IsClosed)
        return;
      this.AuthenticatedUserContainer.Visible = false;
      this.AnonymousUserContainer.Visible = false;
      this.ClosedThreadContainer.Visible = true;
    }

    private void InitFieldControls()
    {
      if (this.FieldControls.Count != 0)
        return;
      this.FieldControls.Add(new CommentsDetailsView.FieldControlData()
      {
        FieldControl = this.EmailControl,
        FieldControlWrapper = this.EmailControlWrapper,
        DisplayFieldControl = this.CommentsSettings.DisplayEmailField,
        IsRequired = this.CommentsSettings.IsEmailFieldRequired
      });
      this.FieldControls.Add(new CommentsDetailsView.FieldControlData()
      {
        FieldControl = this.ContentControl,
        FieldControlWrapper = this.ContentControlWrapper,
        DisplayFieldControl = this.CommentsSettings.DisplayMessageField,
        IsRequired = this.CommentsSettings.IsMessageFieldRequired
      });
      this.FieldControls.Add(new CommentsDetailsView.FieldControlData()
      {
        FieldControl = this.AuthorNameControl,
        FieldControlWrapper = this.AuthorNameControlWrapper,
        DisplayFieldControl = this.CommentsSettings.DisplayNameField,
        IsRequired = this.CommentsSettings.IsNameFieldRequired
      });
      this.FieldControls.Add(new CommentsDetailsView.FieldControlData()
      {
        FieldControl = this.WebsiteControl,
        FieldControlWrapper = this.WebsiteControlWrapper,
        DisplayFieldControl = this.CommentsSettings.DisplayWebSiteField,
        IsRequired = this.CommentsSettings.IsWebSiteFieldRequired
      });
    }

    private void InitCaptcha(bool validate)
    {
      bool flag = this.CommentsSettings.UseSpamProtectionImage && !this.IsAuthenticated;
      this.Captcha.Enabled = flag;
      this.Captcha.Visible = flag;
      this.CaptchaWrapper.Visible = flag;
      if (!(flag & validate))
        return;
      this.Captcha.Validate();
    }

    /// <summary>Validates the fields input</summary>
    private bool ValidateInput()
    {
      bool flag = true;
      foreach (CommentsDetailsView.FieldControlData fieldControl1 in this.FieldControls)
      {
        Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl fieldControl2 = fieldControl1.FieldControl;
        if (fieldControl1.IsRequired)
          fieldControl2.ValidatorDefinition.Required = new bool?(true);
        if (!fieldControl2.Validator.IsValid(fieldControl2.Value))
          flag = false;
      }
      return flag;
    }

    /// <summary>Submits the comment.</summary>
    protected virtual void SubmitComment()
    {
      if (this.CommentsSettings == null || !this.AllowPostingComments)
        return;
      this.InitCaptcha(true);
      if (!this.Page.IsValid || this.Host.DetailItem == null || !(this.Host.ControlDefinition.ManagerType != (Type) null))
        return;
      string providerName = this.Host.ControlDefinition.ProviderName;
      string fullName = this.Host.ControlDefinition.ManagerType.FullName;
      ICommentable detailItem1 = (ICommentable) this.Host.DetailItem;
      if (string.IsNullOrEmpty(providerName))
        providerName = ManagerBase.GetManager(fullName, providerName).Provider.Name;
      this.GroupKey = CommentsUtilities.GetGroupKey(fullName, providerName);
      this.ThreadKey = ControlUtilities.GetLocalizedKey((object) detailItem1.Id);
      this.ThreadType = detailItem1.GetType().FullName;
      if (this.Host.DetailItem is IHasTitle detailItem2)
        this.Title = detailItem2.GetTitle();
      this.InitFieldControls();
      if (!this.ValidateInput())
        return;
      string message = HtmlStripper.StripHtml(this.ContentControl.Value.ToString(), HtmlStripper.StrippingOptions);
      string email = this.EmailControl.Value.ToString();
      string name = this.Page.Server.HtmlEncode(this.AuthorNameControl.Value.ToString());
      string currentLanguage = CommentsUtilities.GetCurrentLanguage();
      string dataSource = (string) null;
      if (this.Host.DetailItem.Provider is IDataProviderBase provider)
        dataSource = provider.Name;
      try
      {
        CommentsUtilities.CreateCommentViaWebService(email, name, message, this.ThreadKey, this.ThreadType, this.Title, dataSource, currentLanguage, this.GroupKey, skipCaptcha: true);
      }
      catch (InvalidOperationException ex)
      {
      }
      if (this.CommentsSettings.ApproveComments.Value)
        this.ModerationContainer.Visible = true;
      this.FieldControls.ForEach((Action<CommentsDetailsView.FieldControlData>) (f => f.CleanUp()));
    }

    private void SubmitButton_Command(object sender, CommandEventArgs e)
    {
      if (!(e.CommandName == "submitComment"))
        return;
      this.SubmitComment();
    }

    protected internal class FieldControlData
    {
      private bool isRequired;

      /// <summary>Gets or sets the field control.</summary>
      /// <value>The field control.</value>
      public Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl FieldControl { get; set; }

      /// <summary>Gets or sets the li element wrapping field control.</summary>
      /// <value>The field control wrapper.</value>
      public HtmlGenericControl FieldControlWrapper { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is required.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if this instance is required; otherwise, <c>false</c>.
      /// </value>
      public bool IsRequired
      {
        get => this.DisplayFieldControl && this.isRequired;
        set => this.isRequired = value;
      }

      /// <summary>
      /// Gets or sets a value indicating whether [display field control].
      /// </summary>
      /// <value><c>true</c> if [display field control]; otherwise, <c>false</c>.</value>
      public bool DisplayFieldControl { get; set; }

      /// <summary>Shows or hides the specified control.</summary>
      public void CofigureVisibility()
      {
        if (this.FieldControl is Control fieldControl)
          fieldControl.Visible = this.DisplayFieldControl;
        this.FieldControlWrapper.Visible = this.DisplayFieldControl;
      }

      /// <summary>Cleans up.</summary>
      public void CleanUp() => this.FieldControl.Value = (object) null;
    }
  }
}
