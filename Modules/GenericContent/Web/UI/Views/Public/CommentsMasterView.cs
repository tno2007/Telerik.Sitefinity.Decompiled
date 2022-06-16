// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views.CommentsMasterView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views
{
  /// <summary>Displays a list of comments on the public side</summary>
  [Obsolete("This frontend UI is replaced by the CommentsWidget.")]
  public class CommentsMasterView : MasterViewBase
  {
    private CommentsSettingsWrapper settingsWrappper;
    private int commentsCount;
    private IQueryable<Comment> query;
    private string sortExpression;
    private IManager manager;
    private ContentDataProviderBase provider;
    private string authorCommentCssClass;
    private IContentViewDefinition definition;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.GenericContent.CommentsMasterView.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsMasterView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the comments settings.</summary>
    /// <value>The comments settings.</value>
    protected CommentsSettingsWrapper CommentsSettings
    {
      get => this.settingsWrappper;
      set => this.settingsWrappper = value;
    }

    /// <summary>
    /// Gets or sets the CSS class applying to the comment container if the auhtor himself made the comment.
    /// </summary>
    public string AuthorCommentCssClass
    {
      get
      {
        if (this.authorCommentCssClass == null)
          this.authorCommentCssClass = "sfcommentOfTheAuthor";
        return this.authorCommentCssClass;
      }
      set => this.authorCommentCssClass = value;
    }

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected IManager Manager
    {
      get
      {
        if (this.manager == null && this.Host.ControlDefinition.ManagerType != (Type) null)
          this.manager = ManagerBase.GetManager(this.Host.ControlDefinition.ManagerType.FullName, this.Host.ControlDefinition.ProviderName);
        return this.manager;
      }
    }

    /// <summary>Gets the provider.</summary>
    /// <value>The provider.</value>
    protected ContentDataProviderBase Provider
    {
      get
      {
        if (this.provider == null && this.Manager != null)
          this.provider = (ContentDataProviderBase) this.Manager.Provider;
        return this.provider;
      }
    }

    /// <summary>
    /// Reference to the control in the template that is responsible for listing all comments for a given content item.
    /// </summary>
    protected virtual RadListView CommentsList => this.Container.GetControl<RadListView>("commentsList", true);

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
      if (this.Host.DetailItem == null)
        return;
      this.definition = definition;
      this.InitializeListView((IQueryable<Comment>) null);
    }

    /// <summary>
    /// Initializes the list view control with the items specified in the query.
    /// </summary>
    /// <param name="query">The query.</param>
    protected virtual void InitializeListView(IQueryable<Comment> query)
    {
      this.query = query;
      this.CommentsList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(this.CommentsList_ItemDataBound);
      this.CommentsList.PreRender += new EventHandler(this.CommentsList_PreRender);
    }

    /// <summary>Gets the items list.</summary>
    /// <returns></returns>
    protected virtual IQueryable<Comment> GetItemsList(
      IDataItem commentedItem,
      IContentViewDefinition definition)
    {
      this.CommentsSettings = new CommentsSettingsWrapper(commentedItem, definition.CommentsSettingsDefinition);
      ICommentService commentsService = SystemManager.GetCommentsService();
      string localizedKey = ControlUtilities.GetLocalizedKey((object) commentedItem.Id);
      CommentFilter filter = new CommentFilter()
      {
        Status = {
          "Published"
        },
        ThreadKey = {
          localizedKey
        }
      };
      IEnumerable<IComment> source1 = commentsService.GetComments(filter);
      this.sortExpression = this.SortExpression;
      if (this.CommentsSettings.HideCommentsAfterNumberOfDays)
      {
        TimeSpan duration = new TimeSpan(this.CommentsSettings.NumberOfDaysToHideComments, 0, 0, 0);
        source1 = source1.Where<IComment>((Func<IComment, bool>) (c => c.DateCreated > DateTime.UtcNow.Subtract(duration)));
      }
      List<Comment> commentList = new List<Comment>();
      Guid empty1 = Guid.Empty;
      Guid empty2 = Guid.Empty;
      foreach (IComment comment in source1)
      {
        Comment compatibleComment = CommentsUtilities.CreateBackwardCompatibleComment("BackwardCompatibilityTransaction");
        string threadType = this.CommentsSettings == null || this.CommentsSettings.DataItem == null ? (string) null : this.CommentsSettings.DataItem.GetType().FullName;
        CommentsUtilities.Populate(ref compatibleComment, comment, threadType, this.Provider.Name);
        commentList.Add(compatibleComment);
      }
      IEnumerable<Comment> source2 = (IEnumerable<Comment>) commentList;
      if (!this.FilterExpression.IsNullOrEmpty())
        source2 = (IEnumerable<Comment>) source2.AsQueryable<Comment>().Where<Comment>(this.FilterExpression);
      if (!this.sortExpression.IsNullOrEmpty())
        source2 = (IEnumerable<Comment>) source2.AsQueryable<Comment>().OrderBy<Comment>(this.sortExpression);
      this.commentsCount = source2.Count<Comment>();
      int pageNumber = this.GetPageNumber(this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
      if (pageNumber > 0)
        source2 = source2.Skip<Comment>(pageNumber);
      if (this.ItemsPerPage.HasValue && this.ItemsPerPage.Value > 0)
        source2 = source2.Take<Comment>(this.ItemsPerPage.Value);
      return source2.AsQueryable<Comment>();
    }

    /// <summary>Checks if the author himself made the comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <param name="commentedItem">The commented item.</param>
    /// <returns>
    /// 	<c>true</c> if the author himself made the comment; otherwise, <c>false</c>.
    /// </returns>
    protected virtual bool IsAuthorComment(Comment comment) => comment != null && comment.Owner == this.CommentsSettings.DataItem.Owner;

    /// <summary>
    /// Determines whether is a specific comment action is allowed according to current user's permissions.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns>True if allowed; False otherwise.</returns>
    private bool IsCommentActionAllowed(string action) => true;

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
    /// Handles the ItemDataBound event of the CommentsList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadListViewItemEventArgs" /> instance containing the event data.</param>
    private void CommentsList_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
        return;
      RadListViewDataItem listViewDataItem = e.Item as RadListViewDataItem;
      Control control1 = e.Item.FindControl("itemIndex");
      if (control1 != null)
      {
        int num = this.sortExpression.ToLower().Equals("datecreated desc") || this.sortExpression.ToLower().Equals("publicationdate desc") ? this.commentsCount - listViewDataItem.DisplayIndex : listViewDataItem.DisplayIndex + 1;
        ((ITextControl) control1).Text = num.ToString();
      }
      if (!(listViewDataItem.DataItem is Comment dataItem))
        return;
      Control control2;
      if (dataItem.Website.IsNullOrWhitespace())
      {
        control2 = e.Item.FindControl("website");
        if (!this.CommentsSettings.DisplayWebSiteField)
          control2.Visible = false;
      }
      else
      {
        control2 = e.Item.FindControl("authorName");
        if (!this.CommentsSettings.DisplayNameField)
          control2.Visible = false;
      }
      if (control2 != null)
        control2.Visible = false;
      if (!this.IsAuthorComment(dataItem))
        return;
      Control control3 = e.Item.FindControl("commentContainer");
      if (control3 == null)
        return;
      HtmlGenericControl htmlGenericControl = (HtmlGenericControl) control3;
      htmlGenericControl.Attributes.Add("class", htmlGenericControl.Attributes["class"] + " " + this.AuthorCommentCssClass);
    }

    /// <summary>
    /// Handles the PreRender event of the CommentsList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void CommentsList_PreRender(object sender, EventArgs e)
    {
      this.query = this.GetItemsList(this.Host.DetailItem, this.definition);
      if (this.Page != null && this.Page.IsPostBack)
        this.commentsCount = this.query.Count<Comment>();
      if (this.commentsCount > 0)
      {
        if (this.CommentsList.FindControl("comments") is ITextControl control)
        {
          string empty = string.Empty;
          string str = this.commentsCount != 1 ? Res.Get<ContentResources>().Comments : Res.Get<ContentResources>().Comment;
          control.Text = this.commentsCount.ToString() + " " + str;
        }
        this.CommentsList.DataSource = (object) this.query.ToList<Comment>();
        this.CommentsList.Rebind();
      }
      else
        this.CommentsList.Visible = false;
    }
  }
}
