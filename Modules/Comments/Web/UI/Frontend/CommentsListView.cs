// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsListView
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
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Services.Comments.DTO;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend
{
  /// <summary>
  /// Represents list of the comments items. Used in CommentsWidget
  /// </summary>
  [ControlTemplateInfo("CommentsResources", "CommentsListViewTemplateName", "CommentsTitle")]
  public class CommentsListView : SimpleScriptView
  {
    private int initalCommentsCount;
    private int loadMoreCommentsSize;
    private bool enablePaging;
    private List<string> visibleCommentsStatuses = new List<string>();
    internal static readonly string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Comments.CommentsListView.ascx";
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsListView.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath(CommentsListView.layoutTemplateName);
    private DateTime limitDate;
    private CommentsSettingsElement threadConfig;

    /// <summary>Gets or sets the thread key.</summary>
    /// <value>The thread key.</value>
    public string ThreadKey { set; get; }

    /// <summary>Gets the Comments Settings element</summary>
    private CommentsSettingsElement ThreadConfig
    {
      get
      {
        if (this.threadConfig == null)
        {
          string threadType = (string) null;
          if (!this.ThreadKey.IsNullOrWhitespace() && !this.ThreadKey.IsNullOrEmpty())
          {
            IThread thread = SystemManager.GetCommentsService().GetThread(this.ThreadKey);
            if (thread != null)
              threadType = thread.Type;
          }
          this.threadConfig = CommentsUtilities.GetThreadConfigByType(threadType);
        }
        return this.threadConfig;
      }
    }

    /// <summary>Gets or sets the current comments count.</summary>
    public int CurrentCommentsCount { get; set; }

    /// <summary>
    /// Gets or sets the number of links that should show when the link is clicked (used for the paging functionality).
    /// </summary>
    /// <value>The size of the load more comments.</value>
    public int LoadMoreCommentsSize
    {
      get => this.loadMoreCommentsSize;
      set => this.loadMoreCommentsSize = value;
    }

    /// <summary>Gets the client label manager control.</summary>
    protected virtual ClientLabelManager ClientLabelManagerControl => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Obsolete. Use LayoutTemplatePath instead.</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Gets the layout template's relative or virtual path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsListView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the load more comments anchor.</summary>
    protected virtual HtmlAnchor LoadMoreCommentsAnchor => this.Container.GetControl<HtmlAnchor>("loadMoreComments", true);

    /// <summary>Gets the comments count label.</summary>
    protected virtual Label CommentsCountLabel => this.Container.GetControl<Label>("commentsCount", true);

    /// <summary>Gets the leave comment anchor.</summary>
    protected virtual HtmlAnchor LeaveCommentAnchor => this.Container.GetControl<HtmlAnchor>("leaveCommentLink", true);

    /// <summary>Gets the leave comment label.</summary>
    protected virtual Literal LeaveCommentLabel => this.Container.GetControl<Literal>("leaveComment", false);

    /// <summary>Gets the oldest comments on top anchor.</summary>
    protected virtual HtmlAnchor OldestOnTopAnchor => this.Container.GetControl<HtmlAnchor>("oldestOnTop", true);

    /// <summary>Gets the newest comments on top anchor.</summary>
    protected virtual HtmlAnchor NewestOnTopAnchor => this.Container.GetControl<HtmlAnchor>("newestOnTop", true);

    /// <summary>Gets the comments commentsList ListView.</summary>
    protected virtual Repeater CommentsList => this.Container.GetControl<Repeater>("commentsList", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.Visible)
        return;
      if (!SystemManager.IsModuleEnabled("Comments"))
      {
        this.Visible = false;
      }
      else
      {
        if (this.IsDesignMode())
          this.LoadMoreCommentsAnchor.Visible = false;
        this.visibleCommentsStatuses.Add("Published");
        this.loadMoreCommentsSize = Config.Get<CommentsModuleConfig>().CommentsPerPage;
        this.enablePaging = Config.Get<CommentsModuleConfig>().EnablePaging;
        if (!this.ThreadKey.IsNullOrWhitespace() && !this.ThreadKey.IsNullOrEmpty())
          this.CommentsList.DataSource = (object) this.GetComments();
        if (this.ThreadConfig.EnableRatings)
        {
          if (this.initalCommentsCount > 1)
            this.CommentsCountLabel.Text = string.Format(Res.Get<CommentsResources>("ReviewsCount"), (object) this.initalCommentsCount);
          else if (this.initalCommentsCount == 1)
            this.CommentsCountLabel.Text = string.Format(Res.Get<CommentsResources>("ReviewCount"), (object) this.initalCommentsCount);
        }
        else if (this.initalCommentsCount > 1)
          this.CommentsCountLabel.Text = string.Format(Res.Get<CommentsResources>("CommentsCount"), (object) this.initalCommentsCount);
        else if (this.initalCommentsCount == 1)
          this.CommentsCountLabel.Text = string.Format(Res.Get<CommentsResources>("CommentCount"), (object) this.initalCommentsCount);
        this.CurrentCommentsCount = !this.enablePaging ? this.initalCommentsCount : this.LoadMoreCommentsSize;
        if (!this.ThreadConfig.EnableRatings || this.LeaveCommentLabel == null)
          return;
        this.LeaveCommentLabel.Text = Res.Get<CommentsResources>().WriteAReview;
      }
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private IEnumerable<CommentResponse> GetComments()
    {
      ICommentService commentsService = SystemManager.GetCommentsService();
      int? nullable = !this.enablePaging ? new int?() : new int?(this.LoadMoreCommentsSize);
      string threadKey = this.ThreadKey;
      IEnumerable<IComment> source = commentsService.GetComments(threadKey).Where<IComment>((Func<IComment, bool>) (c => c.Status == "Published"));
      IEnumerable<IComment> comments = !Config.Get<CommentsModuleConfig>().AreNewestOnTop ? (IEnumerable<IComment>) source.OrderBy<IComment, DateTime>((Func<IComment, DateTime>) (c => c.DateCreated)) : (IEnumerable<IComment>) source.OrderByDescending<IComment, DateTime>((Func<IComment, DateTime>) (c => c.DateCreated));
      this.initalCommentsCount = comments.Count<IComment>();
      if (nullable.HasValue)
        comments = comments.Take<IComment>(nullable.Value);
      IEnumerable<CommentResponse> commentResponses = CommentsUtilities.GetCommentResponses(comments);
      if (this.enablePaging && commentResponses.Count<CommentResponse>() > 0)
        this.limitDate = commentResponses.Last<CommentResponse>().DateCreated;
      return commentResponses;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.CommentsList.DataBind();
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      if (this.enablePaging)
      {
        controlDescriptor.AddElementProperty("loadMoreCommentsAnchor", this.LoadMoreCommentsAnchor.ClientID);
        controlDescriptor.AddProperty("loadMoreCommentsSize", (object) this.LoadMoreCommentsSize);
        if (Config.Get<CommentsModuleConfig>().AreNewestOnTop)
          controlDescriptor.AddProperty("oldestDate", (object) this.limitDate);
        else
          controlDescriptor.AddProperty("latestDate", (object) this.limitDate);
      }
      controlDescriptor.AddProperty("alwaysUseUtc", (object) Config.Get<CommentsModuleConfig>().AlwaysUseUTC);
      controlDescriptor.AddProperty("visibleCommentsStatuses", (object) this.visibleCommentsStatuses);
      controlDescriptor.AddElementProperty("leaveCommentAnchor", this.LeaveCommentAnchor.ClientID);
      controlDescriptor.AddElementProperty("oldestOnTopAnchor", this.OldestOnTopAnchor.ClientID);
      controlDescriptor.AddElementProperty("newestOnTopAnchor", this.NewestOnTopAnchor.ClientID);
      controlDescriptor.AddElementProperty("commentsCountLabel", this.CommentsCountLabel.ClientID);
      string absolute = VirtualPathUtility.ToAbsolute("~/RestApi/comments-api");
      controlDescriptor.AddProperty("serviceUrl", (object) absolute);
      controlDescriptor.AddProperty("currentCommentsCount", (object) this.CurrentCommentsCount);
      controlDescriptor.AddProperty("initalCommentsCount", (object) this.initalCommentsCount);
      controlDescriptor.AddProperty("sortAscDate", (object) !Config.Get<CommentsModuleConfig>().AreNewestOnTop);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManagerControl.ClientID);
      controlDescriptor.AddProperty("threadKey", (object) this.ThreadKey);
      controlDescriptor.AddProperty("currentLanguage", (object) CommentsUtilities.GetCurrentLanguage());
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
      string assembly = typeof (CommentsListView).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Rating.rating.js", "Telerik.Sitefinity.Resources"));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Scripts.CommentsListView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.KendoAll;
  }
}
