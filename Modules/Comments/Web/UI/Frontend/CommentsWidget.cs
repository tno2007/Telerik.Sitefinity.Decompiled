// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Services.Comments.Proxies;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend
{
  /// <summary>
  /// Comments Widget control. Used in for managing the comment items on the frontend.
  /// </summary>
  [ControlDesigner(typeof (CommentsWidgetDesigner))]
  [PropertyEditorTitle(typeof (CommentsResources), "PageComments")]
  [ControlTemplateInfo("CommentsResources", "CommentsTitle", "CommentsTitle")]
  public class CommentsWidget : SimpleScriptView, IHasCacheDependency
  {
    internal static readonly string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Comments.CommentsWidget.ascx";
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsWidget.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath(CommentsWidget.layoutTemplateName);
    private CommentsSettingsElement threadConfig;

    /// <summary>Obsolete. Use LayoutTemplatePath instead.</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Gets the layout template's relative or virtual path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsWidget.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the comments list view template key</summary>
    public string ListViewTemplateKey { get; set; }

    /// <summary>Gets or sets the comments view template key</summary>
    public string ViewTemplateKey { get; set; }

    /// <summary>
    /// Gets or sets the thread key that will be used for association of the comment.
    /// </summary>
    public string ThreadKey { set; get; }

    /// <summary>Gets the Comments Settings element</summary>
    private CommentsSettingsElement ThreadConfig
    {
      get
      {
        if (this.threadConfig == null)
          this.threadConfig = CommentsUtilities.GetThreadConfigByType(this.ThreadType);
        return this.threadConfig;
      }
    }

    /// <summary>
    /// Gets or sets the thread key that will be used for association of the comment.
    /// </summary>
    public string ThreadType { set; get; }

    /// <summary>
    /// Gets or sets the group key that will be used for association of the comment.
    /// </summary>
    public string GroupKey { set; get; }

    /// <summary>Gets or sets the title.</summary>
    public string ThreadTitle { set; get; }

    /// <summary>Gets or sets the data source.</summary>
    public string DataSource { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to restrict to authenticated users.
    /// </summary>
    public bool RestrictToAuthenticated { set; get; }

    /// <summary>
    /// Gets or sets a value indicating whether [thread is closed].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [thread is closed]; otherwise, <c>false</c>.
    /// </value>
    private bool ThreadIsClosed { get; set; }

    /// <summary>Gets or sets the allow comments.</summary>
    /// <value>The allow comments.</value>
    public bool? AllowComments { get; set; }

    /// <summary>Gets the label that will show if the thread is closed</summary>
    protected virtual Label ClosedThreadMessageLabel => this.Container.GetControl<Label>("closedThreadMessage", true);

    /// <summary>
    /// Gets the CommentsAverageRatingControl that will show the thread average rating
    /// </summary>
    protected virtual CommentsAverageRatingControl CommentsAverageRating => this.Container.GetControl<CommentsAverageRatingControl>("commentsAverageRating", true);

    /// <summary>Gets the required approval message.</summary>
    protected virtual Label RequiredApprovalMessageLabel => this.Container.GetControl<Label>("requiredApprovalMessage", true);

    /// <summary>Gets the required approval message.</summary>
    protected virtual Label AlreadySubmittedReviewMessageLabel => this.Container.GetControl<Label>("alreadySubmittedReviewMessage", true);

    /// <summary>Gets the required approval message.</summary>
    protected virtual Label ReviewSubmittedSuccessfullyMessageLabel => this.Container.GetControl<Label>("reviewSubmittedSuccessfullyMessage", true);

    /// <summary>Gets the commentsListView form user control.</summary>
    protected virtual CommentsListView CommentsListViewForm => this.Container.GetControl<CommentsListView>("listView", true);

    /// <summary>Gets the submit comment form.</summary>
    protected virtual CommentsSubmitForm SubmitForm => this.Container.GetControl<CommentsSubmitForm>("submitForm", false);

    /// <summary>Gets the client label manager control.</summary>
    protected virtual ClientLabelManager ClientLabelManagerControl => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    protected virtual CommentsNotificationSubscription CommentsNotificationSubscriptionControl => this.Container.GetControl<CommentsNotificationSubscription>("notificationSubscription", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ThreadType.IsNullOrEmpty())
        this.ThreadType = typeof (PageNode).ToString();
      if (SystemManager.IsModuleEnabled("Comments"))
      {
        bool? allowComments = this.AllowComments;
        int num;
        if (!allowComments.HasValue)
        {
          num = !this.ThreadConfig.AllowComments ? 1 : 0;
        }
        else
        {
          allowComments = this.AllowComments;
          num = !allowComments.Value ? 1 : 0;
        }
        if (num == 0)
        {
          this.ThreadIsClosed = false;
          this.ClosedThreadMessageLabel.Visible = false;
          SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
          if (this.ThreadKey.IsNullOrEmpty() && SiteMapBase.GetActualCurrentNode() != null)
            this.ThreadKey = ControlUtilities.GetLocalizedKey((object) SiteMapBase.GetActualCurrentNode().Id, (string) null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(this.ThreadType));
          this.CommentsListViewForm.ThreadKey = this.ThreadKey;
          this.CommentsListViewForm.TemplateKey = this.ListViewTemplateKey;
          this.SubmitForm.TemplateKey = this.ViewTemplateKey;
          ICommentService commentsService = SystemManager.GetCommentsService();
          IThread thread = (IThread) null;
          if (!this.ThreadKey.IsNullOrEmpty())
            thread = commentsService.GetThread(this.ThreadKey);
          if (thread != null)
          {
            this.ThreadIsClosed = thread.IsClosed;
            this.ClosedThreadMessageLabel.Visible = thread.IsClosed;
            if (this.SubmitForm != null)
            {
              this.SubmitForm.CreateThread = false;
              this.SubmitForm.Thread = thread;
            }
          }
          else if (this.SubmitForm != null)
          {
            this.SubmitForm.CreateThread = true;
            this.PopulateThreadTitleAndGroupKey();
            ThreadProxy threadProxy = new ThreadProxy(this.ThreadTitle, this.ThreadType, this.GroupKey, (IAuthor) new AuthorProxy(currentIdentity.UserId.ToString()), SystemManager.CurrentContext.Culture)
            {
              IsClosed = false,
              Key = this.ThreadKey,
              DataSource = this.DataSource
            };
            if (this.ThreadConfig.EnableRatings)
              threadProxy.Behavior = "review";
            this.SubmitForm.Thread = (IThread) threadProxy;
          }
          if (this.ThreadConfig.EnableRatings)
          {
            this.RequiredApprovalMessageLabel.Text = Res.Get<CommentsResources>("RequiredApprovalMessageReviews");
            if (this.CheckIfAuthorAlreadyCommented())
              this.AlreadySubmittedReviewMessageLabel.Style.Remove("display");
          }
          this.SetupNotificationSubscrption();
          this.SubsribeCacheDependency();
          this.CommentsAverageRating.ThreadKey = this.ThreadKey;
          this.CommentsAverageRating.ThreadType = this.ThreadType;
          return;
        }
      }
      this.Visible = false;
      container.Visible = false;
    }

    private void SetupNotificationSubscrption()
    {
      if (this.CommentsNotificationSubscriptionControl != null && this.ThreadConfig.AllowSubscription && !this.ThreadIsClosed)
      {
        if (this.CommentsNotificationSubscriptionControl.SubscriptionItemKey.IsNullOrWhitespace())
          this.CommentsNotificationSubscriptionControl.SubscriptionItemKey = this.ThreadKey;
        if (!this.CommentsNotificationSubscriptionControl.ThreadType.IsNullOrWhitespace())
          return;
        this.CommentsNotificationSubscriptionControl.ThreadType = this.ThreadType;
      }
      else
        this.CommentsNotificationSubscriptionControl.Visible = false;
    }

    private void PopulateThreadTitleAndGroupKey()
    {
      if (this.ThreadTitle.IsNullOrEmpty() && SiteMapBase.GetActualCurrentNode() != null)
        this.ThreadTitle = SiteMapBase.GetActualCurrentNode().Title;
      if (!this.GroupKey.IsNullOrEmpty())
        return;
      this.GroupKey = SystemManager.CurrentContext.CurrentSite.Id.ToString();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected virtual void SubsribeCacheDependency()
    {
      if (this.IsBackend())
        return;
      IList<CacheDependencyKey> dependencyObjects = this.GetCacheDependencyObjects();
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "DataItemTypeCacheDependencyName"))
        SystemManager.CurrentHttpContext.Items.Add((object) "DataItemTypeCacheDependencyName", (object) new List<CacheDependencyKey>());
      ((List<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) "DataItemTypeCacheDependencyName"]).AddRange((IEnumerable<CacheDependencyKey>) dependencyObjects);
    }

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
      controlDescriptor.AddComponentProperty("submitCommentForm", this.SubmitForm.ClientID);
      controlDescriptor.AddComponentProperty("commentsListViewForm", this.CommentsListViewForm.ClientID);
      controlDescriptor.AddElementProperty("requiredApprovalMessageLabel", this.RequiredApprovalMessageLabel.ClientID);
      controlDescriptor.AddElementProperty("reviewSubmittedSuccessfullyMessageLabel", this.ReviewSubmittedSuccessfullyMessageLabel.ClientID);
      controlDescriptor.AddProperty("requiresApprovalSetting", (object) this.threadConfig.RequiresApproval);
      controlDescriptor.AddProperty("requiresCaptchaSetting", (object) Config.Get<CommentsModuleConfig>().UseSpamProtectionImage);
      string absolute1 = VirtualPathUtility.ToAbsolute("~/RestApi/comments-api");
      controlDescriptor.AddProperty("serviceUrl", (object) absolute1);
      string absolute2 = VirtualPathUtility.ToAbsolute("~/RestApi/session");
      controlDescriptor.AddProperty("loginServiceUrl", (object) absolute2);
      controlDescriptor.AddProperty("threadKey", (object) this.ThreadKey);
      controlDescriptor.AddProperty("threadIsClosed", (object) this.ThreadIsClosed);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManagerControl.ClientID);
      controlDescriptor.AddProperty("language", (object) CommentsUtilities.GetCurrentLanguage());
      controlDescriptor.AddProperty("enableRatings", (object) this.ThreadConfig.EnableRatings);
      if (this.CommentsNotificationSubscriptionControl != null && this.CommentsNotificationSubscriptionControl.Visible)
        controlDescriptor.AddComponentProperty("commentsNotificationSubscriptionControl", this.CommentsNotificationSubscriptionControl.ClientID);
      if (this.Page.Items.Contains((object) typeof (CommentsAverageRatingControlBinder).FullName))
      {
        CommentsAverageRatingControlBinder ratingControlBinder = this.Page.Items[(object) typeof (CommentsAverageRatingControlBinder).FullName] as CommentsAverageRatingControlBinder;
        controlDescriptor.AddComponentProperty("commentsAverageRatingControlBinder", ratingControlBinder.ClientID);
      }
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
      string assembly = typeof (CommentsWidget).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsRestClient.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsWidget.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <returns></returns>
    /// <value>An instance of type <see cref="!:CacheDependencyNotifiedObjects" />.</value>
    public IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      if (this.ThreadKey.IsNullOrEmpty())
        return (IList<CacheDependencyKey>) new List<CacheDependencyKey>();
      return (IList<CacheDependencyKey>) new List<CacheDependencyKey>()
      {
        new CacheDependencyKey()
        {
          Key = this.ThreadKey,
          Type = typeof (IThread)
        }
      };
    }

    private bool CheckIfAuthorAlreadyCommented() => CommentsUtilities.GetCommentsByThreadForCurrentAuthorWithRating(this.ThreadKey, SystemManager.GetCommentsService()).Count<IComment>() > 0;
  }
}
