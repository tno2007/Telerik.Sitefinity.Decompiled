// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.DownloadMasterViewBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents
{
  /// <summary>
  /// Base class for all master views of DownloadListView control.
  /// </summary>
  public abstract class DownloadMasterViewBase : DownloadListViewBase
  {
    private object[] dataSource;
    private bool? includeChildLibraryItems;

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

    /// <summary>
    /// Gets or sets a value indicating whether to include child library items.
    /// </summary>
    protected virtual bool? IncludeChildLibraryItems
    {
      get
      {
        if (!this.includeChildLibraryItems.HasValue && this.Definition is IContentViewMasterDefinition definition)
          this.includeChildLibraryItems = new bool?(((int) definition.IncludeDescendantItems ?? 1) != 0);
        return this.includeChildLibraryItems;
      }
      set => this.includeChildLibraryItems = value;
    }

    /// <summary>Gets the pager.</summary>
    /// <value>The pager.</value>
    protected virtual Pager Pager => this.Container.GetControl<Pager>("pager", true);

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
      base.InitializeControls(container, definition);
      if (!(definition is IContentViewMasterDefinition masterDefinition))
        return;
      this.ConfigurePager(this.TotalCount, masterDefinition);
    }

    /// <summary>Configures the documents query.</summary>
    /// <param name="definition">The content view definition for DowloadListView control.</param>
    protected override void ConfigureDocumentsQuery(IContentViewDefinition definition)
    {
      IDownloadListViewMasterDefinition masterDefinition = definition as IDownloadListViewMasterDefinition;
      this.ThumbnailType = masterDefinition.ThumbnailType;
      int? totalCount = new int?(0);
      int? skip = new int?(0);
      bool? nullable;
      if (masterDefinition.AllowPaging.HasValue)
      {
        nullable = masterDefinition.AllowPaging;
        if (nullable.Value)
          skip = new int?(this.GetItemsToSkipCount(masterDefinition.ItemsPerPage, this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix));
      }
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (this.DataSource != null)
      {
        this.Query = this.DataSource.OfType<Document>().AsQueryable<Document>();
        this.Query = DataProviderBase.SetExpressions<Document>(this.Query, empty1, empty2, skip, masterDefinition.ItemsPerPage, ref totalCount);
      }
      else if (this.Host.DisplayRelatedData())
      {
        this.Query = Queryable.OfType<Document>(this.Host.GetRelatedItems(this.MasterViewDefinition.FilterExpression, this.MasterViewDefinition.SortExpression, this.MasterViewDefinition.ItemsPerPage, ref totalCount)).AsQueryable<Document>();
      }
      else
      {
        this.Query = this.Manager.GetDocuments();
        nullable = masterDefinition.AllowUrlQueries;
        if (nullable.HasValue)
        {
          nullable = masterDefinition.AllowUrlQueries;
          if (nullable.Value)
          {
            this.Query = this.EvaluateUrl<Document>(this.Query, "Date", "PublicationDate", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
            this.Query = this.EvaluateUrl<Document>(this.Query, "Author", "Owner", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
            this.Query = this.EvaluateUrl<Document>(this.Query, "Taxonomy", "", typeof (Document), this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
          }
        }
        masterDefinition.FilterExpression = ContentHelper.AdaptMultilingualFilterExpression(masterDefinition.FilterExpression);
        this.Query = DataProviderBase.SetExpressions<Document>(this.Query, DefinitionsHelper.GetFilterExpression(masterDefinition.FilterExpression, masterDefinition.AdditionalFilter), masterDefinition.SortExpression, new int?(), new int?(), ref totalCount);
        Guid parentId = masterDefinition.ItemsParentId;
        if (parentId != Guid.Empty)
        {
          Folder folder = this.Manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == parentId));
          if (folder == null)
          {
            nullable = this.IncludeChildLibraryItems;
            if (nullable.HasValue)
            {
              nullable = this.IncludeChildLibraryItems;
              if (!nullable.Value)
              {
                this.Query = this.Query.Where<Document>((Expression<Func<Document, bool>>) (p => p.Parent.Id == parentId && (p.FolderId == new Guid?() || p.FolderId == (Guid?) Guid.Empty)));
                goto label_20;
              }
            }
            this.Query = this.Query.Where<Document>((Expression<Func<Document, bool>>) (p => p.Parent.Id == parentId));
          }
          else
          {
            nullable = this.IncludeChildLibraryItems;
            if (nullable.HasValue)
            {
              nullable = this.IncludeChildLibraryItems;
              if (!nullable.Value)
              {
                this.Query = this.Query.Where<Document>((Expression<Func<Document, bool>>) (m => m.FolderId == (Guid?) folder.Id));
                goto label_20;
              }
            }
            this.Query = this.Manager.GetDescendantsFromQuery<Document>(this.Query, (IFolder) folder);
          }
        }
label_20:
        this.Query = DataProviderBase.SetExpressions<Document>(this.Query, (string) null, (string) null, skip, masterDefinition.ItemsPerPage, ref totalCount);
      }
      this.TotalCount = totalCount.Value;
    }

    /// <summary>Configures the pager.</summary>
    /// <param name="vrtualItemCount">The vrtual item count.</param>
    /// <param name="masterDefinition">The master definition.</param>
    protected virtual void ConfigurePager(
      int vrtualItemCount,
      IContentViewMasterDefinition masterDefinition)
    {
      if (masterDefinition.AllowPaging.HasValue && masterDefinition.AllowPaging.Value && masterDefinition.ItemsPerPage.GetValueOrDefault() > 0)
      {
        this.Pager.VirtualItemCount = vrtualItemCount;
        this.Pager.PageSize = masterDefinition.ItemsPerPage.Value;
        this.Pager.QueryParamKey = this.Host.UrlKeyPrefix;
        bool? setPaginationUrls = this.Host.SetPaginationUrls;
        if (setPaginationUrls.HasValue)
        {
          Pager pager = this.Pager;
          setPaginationUrls = this.Host.SetPaginationUrls;
          int num = setPaginationUrls.Value ? 1 : 0;
          pager.SetPaginationUrls = num != 0;
        }
        IRelatedDataView host = (IRelatedDataView) this.Host;
        if (host == null || !host.DisplayRelatedData())
          return;
        this.Pager.BaseUrl = host.GetPagerBaseUrl();
        this.Pager.UrlEvaluationMode = UrlEvaluationMode.QueryString;
      }
      else
        this.Pager.Visible = false;
    }
  }
}
