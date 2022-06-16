// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsSubmitForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend
{
  /// <summary>
  /// Submit form for the comment items. Used in CommentsWidget
  /// </summary>
  [ControlTemplateInfo("CommentsResources", "CommentsForm", "CommentsTitle")]
  public class CommentsSubmitForm : SimpleScriptView
  {
    internal static readonly string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Comments.CommentsSubmitForm.ascx";
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsSubmitForm.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath(CommentsSubmitForm.layoutTemplateName);
    private CommentsSettingsElement threadConfig;

    /// <summary>Obsolete. Use LayoutTemplatePath instead.</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Gets the layout template's relative or virtual path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsSubmitForm.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the thread will be used for association of the comment.
    /// </summary>
    public IThread Thread { set; get; }

    /// <summary>
    /// Gets or sets a value indicating whether [create thread].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [create thread]; otherwise, <c>false</c>.
    /// </value>
    public bool CreateThread { get; set; }

    /// <summary>Gets the Comments Settings element</summary>
    private CommentsSettingsElement ThreadConfig
    {
      get
      {
        if (this.threadConfig == null)
          this.threadConfig = CommentsUtilities.GetThreadConfigByType(this.Thread != null ? this.Thread.Type : (string) null);
        return this.threadConfig;
      }
    }

    /// <summary>Gets the label that will show for authenticated users</summary>
    protected virtual Label RestrictedToAuthenticatedLabel => this.Container.GetControl<Label>("restrictedToAuthenticated", true);

    /// <summary>Gets the submit form list.</summary>
    /// <value>The submit form list.</value>
    protected virtual HtmlGenericControl SubmitCommentForm => this.Container.GetControl<HtmlGenericControl>("submitCommentForm", true);

    /// <summary>
    /// Reference to the TextArea control that shows the comment text.
    /// </summary>
    protected virtual HtmlTextArea CommentEditor => this.Container.GetControl<HtmlTextArea>("commentEditor", true);

    /// <summary>Gets the validation message.</summary>
    protected virtual Label ValidationMessage => this.Container.GetControl<Label>("validationMessage", true);

    /// <summary>Gets the validation message.</summary>
    protected virtual Label ValidationMessageRating => this.Container.GetControl<Label>("validationMessageRating", true);

    /// <summary>Gets the div data contains the rating component.</summary>
    protected virtual HtmlGenericControl RatingSection => this.Container.GetControl<HtmlGenericControl>("ratingSection", false);

    /// <summary>Gets the leave comment label.</summary>
    protected virtual SitefinityLabel LeaveCommentLabel => this.Container.GetControl<SitefinityLabel>("leaveComment", true);

    /// <summary>
    /// Reference to the TextField control that shows the name of the user who submits the comment.
    /// </summary>
    protected virtual TextField NameTextField => this.Container.GetControl<TextField>("nameTextField", true);

    /// <summary>
    /// Reference to the TextField control that shows the email.
    /// </summary>
    protected virtual TextField EmailTextField => this.Container.GetControl<TextField>("emailTextField", true);

    /// <summary>Reference to the submit button</summary>
    protected virtual Button SubmitCommentButton => this.Container.GetControl<Button>("submitCommentButton", true);

    /// <summary>Gets the client label manager control.</summary>
    protected virtual ClientLabelManager ClientLabelManagerControl => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets a reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the loading view.</summary>
    protected virtual HtmlGenericControl LoadingView => this.Container.GetControl<HtmlGenericControl>("loadingView", true);

    /// <summary>Gets the avatar image.</summary>
    protected virtual System.Web.UI.WebControls.Image AvatarImage => this.Container.GetControl<System.Web.UI.WebControls.Image>("avatarImage", true);

    /// <summary>Gets the restful captcha.</summary>
    /// <value>The restful captcha.</value>
    protected virtual RestfulCaptcha RestfulCaptcha => this.Container.GetControl<RestfulCaptcha>("restfulCaptcha", true);

    /// <summary>Gets the restful captcha.</summary>
    /// <value>The restful captcha.</value>
    protected virtual SitefinityLabel CaptchaError => this.Container.GetControl<SitefinityLabel>("captchaError", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!SystemManager.IsModuleEnabled("Comments") || this.ShouldHideCommentsForm())
      {
        this.Visible = false;
      }
      else
      {
        if (this.IsDesignMode())
        {
          this.SubmitCommentButton.Enabled = false;
        }
        else
        {
          this.NameTextField.Attributes["style"] += "display:none;";
          this.EmailTextField.Attributes["style"] += "display:none;";
        }
        this.ConfigureAvatarImage(ClaimsManager.GetCurrentIdentity());
        string str = LoginUtils.BuildLoginPageUrl(LoginUtils.GetDefaultLoginUrl(), SystemManager.CurrentHttpContext.Request.Url.AbsoluteUri);
        if (this.ThreadConfig.EnableRatings)
        {
          this.ValidationMessage.Text = Res.Get<CommentsResources>().EmptyReviewErrorMessage;
          this.LeaveCommentLabel.Text = Res.Get<CommentsResources>().WriteAReview;
          this.RestrictedToAuthenticatedLabel.Text = string.Format(Res.Get<CommentsResources>().RestrictToAuthenticatedReviewText, (object) str);
        }
        else
          this.RestrictedToAuthenticatedLabel.Text = string.Format(Res.Get<CommentsResources>().RestrictToAuthenticatedText, (object) str);
      }
    }

    private bool ShouldHideCommentsForm() => this.Thread == null || this.ThreadConfig.EnableRatings && this.CheckIfAuthorAlreadyCommented() || this.Thread.IsClosed;

    private bool CheckIfAuthorAlreadyCommented() => CommentsUtilities.GetCommentsByThreadForCurrentAuthorWithRating(this.Thread.Key, SystemManager.GetCommentsService()).Count<IComment>() > 0;

    /// <summary>Configures Avatar image</summary>
    private void ConfigureAvatarImage(SitefinityIdentity currentUser)
    {
      if (!currentUser.IsAuthenticated)
      {
        this.AvatarImage.ImageUrl = UserProfilesHelper.GetAvatarImageUrl(Guid.Empty);
      }
      else
      {
        Telerik.Sitefinity.Libraries.Model.Image image;
        UserProfilesHelper.GetAvatarImageUrl(currentUser.UserId, out image);
        if (image != null && !string.IsNullOrEmpty(image.MediaUrl))
          this.AvatarImage.ImageUrl = image.ThumbnailUrl;
        else
          this.AvatarImage.ImageUrl = RouteHelper.ResolveUrl("~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", UrlResolveOptions.Rooted);
      }
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.TelerikSitefinity;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      string absolute = VirtualPathUtility.ToAbsolute("~/RestApi/comments-api");
      controlDescriptor.AddElementProperty("submitCommentForm", this.SubmitCommentForm.ClientID);
      controlDescriptor.AddElementProperty("commentEditor", this.CommentEditor.ClientID);
      controlDescriptor.AddElementProperty("validationMessage", this.ValidationMessage.ClientID);
      controlDescriptor.AddElementProperty("validationMessageRating", this.ValidationMessageRating.ClientID);
      controlDescriptor.AddElementProperty("leaveCommentLabel", this.LeaveCommentLabel.ClientID);
      controlDescriptor.AddComponentProperty("nameTextField", this.NameTextField.ClientID);
      controlDescriptor.AddComponentProperty("emailTextField", this.EmailTextField.ClientID);
      controlDescriptor.AddElementProperty("submitCommentButton", this.SubmitCommentButton.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManagerControl.ClientID);
      if (this.Thread != null)
      {
        controlDescriptor.AddProperty("threadKey", (object) this.Thread.Key);
        controlDescriptor.AddProperty("threadType", (object) this.Thread.Type);
        controlDescriptor.AddProperty("threadBehavior", (object) this.Thread.Behavior);
        controlDescriptor.AddProperty("threadTitle", (object) this.Thread.Title);
        controlDescriptor.AddProperty("groupKey", (object) this.Thread.GroupKey);
        controlDescriptor.AddProperty("dataSource", (object) this.Thread.DataSource);
        controlDescriptor.AddProperty("language", (object) CommentsUtilities.GetCurrentLanguage());
      }
      controlDescriptor.AddElementProperty("captchaError", this.CaptchaError.ClientID);
      controlDescriptor.AddComponentProperty("restfulCaptcha", this.RestfulCaptcha.ClientID);
      controlDescriptor.AddProperty("createThread", (object) this.CreateThread);
      controlDescriptor.AddProperty("serviceUrl", (object) absolute);
      controlDescriptor.AddProperty("loginText", (object) Res.Get<Labels>("Login"));
      controlDescriptor.AddProperty("requireAuthentication", (object) this.ThreadConfig.RequiresAuthentication);
      controlDescriptor.AddElementProperty("loginLink", this.RestrictedToAuthenticatedLabel.ClientID);
      controlDescriptor.AddProperty("enableRatings", (object) this.ThreadConfig.EnableRatings);
      controlDescriptor.AddProperty("ratingsLabel", (object) Res.Get<CommentsResources>().Get("SubmitFormRatingLabel"));
      controlDescriptor.AddElementProperty("ratingSection", this.RatingSection.ClientID);
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
      string assembly = typeof (CommentsSubmitForm).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.WatermarkField.js", typeof (Telerik.Sitefinity.Constants).Assembly.FullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsRestClient.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Rating.rating.js", "Telerik.Sitefinity.Resources"));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsSubmitForm.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
