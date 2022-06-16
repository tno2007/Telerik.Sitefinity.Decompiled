// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.MasterViewBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Web.UI.Comments;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views
{
  public abstract class MasterViewBase : ViewBase
  {
    private string sortExpression;
    private string filterExpression;
    private QueryData additionalFilter;
    private int? itemsPerPage;
    private bool? allowUrlQueries;
    private bool? allowPaging;
    private bool? itemLanguageFallback;
    private Guid itemsParentId;
    private ICollection<Guid> itemsParentIds;
    private object[] dataSource;

    /// <summary>Gets the master definition.</summary>
    /// <value>The master definition.</value>
    protected virtual IContentViewMasterDefinition MasterDefinition => this.Definition as IContentViewMasterDefinition;

    /// <summary>
    /// Gets or sets the sort expression for the list of items
    /// </summary>
    /// <value>The sort expression.</value>
    public virtual string SortExpression
    {
      get => this.sortExpression.IsNullOrEmpty() && this.MasterDefinition != null ? this.MasterDefinition.SortExpression : this.sortExpression;
      set => this.sortExpression = value;
    }

    /// <summary>
    /// Gets or sets a constant filter expression for the list of items in the view
    /// </summary>
    /// <value>The filter expression.</value>
    public virtual string FilterExpression
    {
      get => this.filterExpression.IsNullOrEmpty() && this.MasterDefinition != null ? this.MasterDefinition.FilterExpression : this.filterExpression;
      set => this.filterExpression = value;
    }

    /// <summary>
    /// Gets or sets an additional filter for the list of items in the view
    /// </summary>
    /// <value>The filter query.</value>
    public virtual QueryData AdditionalFilter
    {
      get => this.additionalFilter == null && this.MasterDefinition != null ? this.MasterDefinition.AdditionalFilter : this.additionalFilter;
      set => this.additionalFilter = value;
    }

    /// <summary>
    /// Gets or sets the sort expression for the list of items
    /// </summary>
    /// <value>The sort expression.</value>
    public virtual int? ItemsPerPage
    {
      get => !this.itemsPerPage.HasValue && this.MasterDefinition != null ? this.MasterDefinition.ItemsPerPage : this.itemsPerPage;
      set => this.itemsPerPage = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether URL queries are allowed.
    /// The default value is true.
    /// </summary>
    /// <value><c>true</c> if URL queries are allowed; otherwise, <c>false</c>.</value>
    public virtual bool? AllowUrlQueries
    {
      get => !this.allowUrlQueries.HasValue && this.MasterDefinition != null ? this.MasterDefinition.AllowUrlQueries : this.allowUrlQueries;
      set => this.allowUrlQueries = value;
    }

    /// <summary>
    /// Specifies whether the master view allows paging of the list of items
    /// </summary>
    /// <value>A Boolean value. True if paging is allowed</value>
    public virtual bool? AllowPaging
    {
      get => !this.allowPaging.HasValue && this.MasterDefinition != null ? this.MasterDefinition.AllowPaging : this.allowPaging;
      set => this.allowPaging = value;
    }

    /// <summary>
    /// When in multilingual mode gets or sets whether items with no translation for the current language will be shown.
    /// </summary>
    public virtual bool? ItemLanguageFallback
    {
      get => !this.itemLanguageFallback.HasValue && this.MasterDefinition != null ? this.MasterDefinition.ItemLanguageFallback : this.itemLanguageFallback;
      set => this.itemLanguageFallback = value;
    }

    /// <summary>Gets or sets the items parent ID.</summary>
    /// <value>The items view parent.</value>
    public Guid ItemsParentId
    {
      get => this.itemsParentId == Guid.Empty && this.MasterDefinition != null ? this.MasterDefinition.ItemsParentId : this.itemsParentId;
      set => this.itemsParentId = value;
    }

    /// <summary>Gets or sets the items parents IDs.</summary>
    public ICollection<Guid> ItemsParentsIds
    {
      get => this.itemsParentIds == null && this.MasterDefinition != null ? this.MasterDefinition.ItemsParentsIds : this.itemsParentIds;
      set => this.itemsParentIds = value;
    }

    /// <summary>
    /// Gets or sets the data source for the MasterView. When this
    /// property is set, all other filtering and sorting options will be
    /// disregarded.
    /// </summary>
    public object[] DataSource
    {
      get => this.dataSource;
      set
      {
        this.dataSource = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Configures the pager.</summary>
    /// <param name="vrtualItemCount">The virtual item count.</param>
    /// <param name="masterDefinition">The master definition.</param>
    protected virtual void ConfigurePager(Pager pager, int vrtualItemCount)
    {
      bool? allowPaging = this.AllowPaging;
      bool flag = true;
      if (allowPaging.GetValueOrDefault() == flag & allowPaging.HasValue && this.ItemsPerPage.HasValue && this.ItemsPerPage.Value > 0)
      {
        pager.VirtualItemCount = vrtualItemCount;
        pager.PageSize = this.ItemsPerPage.Value;
        pager.QueryParamKey = this.Host.UrlKeyPrefix;
        if (this.Host.SetPaginationUrls.HasValue)
          pager.SetPaginationUrls = this.Host.SetPaginationUrls.Value;
        if (!this.Host.DisplayRelatedData())
          return;
        pager.BaseUrl = this.Host.GetPagerBaseUrl();
        pager.UrlEvaluationMode = UrlEvaluationMode.QueryString;
      }
      else
        pager.Visible = false;
    }

    /// <summary>
    /// Sets the <see cref="P:Telerik.Sitefinity.Web.UI.Comments.CommentsBox.CommentsCount" /> property for all items in the list view.
    /// </summary>
    /// <param name="commentsManager">The manager, which should implement <see cref="T:Telerik.Sitefinity.Model.ICommentsManager" />.</param>
    /// <param name="listView">The <see cref="T:Telerik.Web.UI.RadListView" /> containing the <see cref="T:Telerik.Sitefinity.Web.UI.Comments.CommentsBox" />-es.</param>
    [Obsolete("Comments counts are set by CommentsCountControlBinder.")]
    protected virtual void SetCommentCounts(ICommentsManager commentsManager, RadListView listView) => this.SetCommentCounts<CommentsBox>(commentsManager, listView, (Action<CommentsBox, int>) ((commentsBox, count) => commentsBox.CommentsCount = count));

    /// <summary>
    /// Sets the <see cref="P:Telerik.Sitefinity.Web.UI.Comments.CommentsBox.CommentsCount" /> property for all items in the list view.
    /// </summary>
    /// <param name="commentsManager">The manager, which should implement <see cref="T:Telerik.Sitefinity.Model.ICommentsManager" />.</param>
    /// <param name="listView">The <see cref="T:Telerik.Web.UI.RadListView" /> containing the <see cref="T:Telerik.Sitefinity.Web.UI.Comments.CommentsBox" />-es.</param>
    [Obsolete("Comments counts are set by CommentsCountControlBinder.")]
    protected virtual void SetCommentCounts<TCommentsControl>(
      ICommentsManager commentsManager,
      RadListView listView,
      Action<TCommentsControl, int> commentsControlSetter,
      string commentsControlName = "itemCommentsLink")
      where TCommentsControl : Control
    {
      LinkedList<Tuple<Content, TCommentsControl>> source = new LinkedList<Tuple<Content, TCommentsControl>>();
      foreach (RadListViewDataItem listViewDataItem in (List<RadListViewDataItem>) listView.Items)
      {
        if (listViewDataItem.ItemType == RadListViewItemType.DataItem || listViewDataItem.ItemType == RadListViewItemType.AlternatingItem)
        {
          if (!(listViewDataItem.FindControl(commentsControlName) is TCommentsControl control))
            return;
          if (listViewDataItem.DataItem is Content dataItem)
            source.AddLast(Tuple.Create<Content, TCommentsControl>(dataItem, control));
        }
      }
      if (source.Count == 0)
        return;
      IEnumerable<Guid> liveIds = source.Select<Tuple<Content, TCommentsControl>, Guid>((Func<Tuple<Content, TCommentsControl>, Guid>) (cl => cl.Item1.Id));
      IDictionary<Guid, int> commentCounts = commentsManager.GetCommentCounts(liveIds, true);
      foreach (Tuple<Content, TCommentsControl> tuple in source)
      {
        Content content = tuple.Item1;
        TCommentsControl commentsControl = tuple.Item2;
        int valueOrDefault = commentCounts.GetValueOrDefault<Guid, int>(content.Id);
        int num = (int) content.AllowComments ?? 0;
        commentsControlSetter(commentsControl, valueOrDefault);
        if (num == 0 && valueOrDefault <= 0)
          commentsControl.Visible = false;
      }
    }
  }
}
