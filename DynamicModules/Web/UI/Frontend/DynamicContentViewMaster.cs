// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentViewMaster
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.InlineEditing;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Extensions;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend
{
  /// <summary>
  /// Control that displays the master view of the dynamic content view.
  /// </summary>
  public class DynamicContentViewMaster : DynamicContentViewBase
  {
    private IQueryable<DynamicContent> dataSource;
    private string filterExpression;
    private QueryData additionalFilter;
    internal static readonly string defaultLayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.DynamicModules.DynamicContentViewMaster.ascx");
    private bool? itemLanguageFallback;
    private bool? allowUrlQueries;
    private bool? bindAddressFields;
    private bool? bindAssetsFields;

    public DynamicContentViewMaster(DynamicModuleManager dynamicModuleManager = null)
      : base(dynamicModuleManager)
    {
    }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
        {
          if (this.MasterViewDefinition != null)
            base.LayoutTemplatePath = this.MasterViewDefinition.TemplatePath;
          if (string.IsNullOrEmpty(base.LayoutTemplatePath) && string.IsNullOrEmpty(this.LayoutTemplateName))
            base.LayoutTemplatePath = DynamicContentViewMaster.defaultLayoutTemplatePath;
        }
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the parent item. If set filtering by parent item will be applied to the data source. Supports multiple parent type levels.
    /// </summary>
    public DynamicContent ParentItem { get; set; }

    /// <summary>
    /// Gets or sets the currently selected item. On ItemDataBound event of the DynamicContentListView, item will be marked as selected.
    /// </summary>
    public DynamicContent SelectedItem { get; set; }

    /// <summary>
    /// Gets or sets the definition which specifies the way this control should behave.
    /// </summary>
    public IContentViewMasterDefinition MasterViewDefinition { get; set; }

    public UrlEvaluationMode UrlEvaluationMode { get; set; }

    public string UrlKeyPrefix { get; set; }

    /// <summary>Gets or sets the source data items ids</summary>
    public Guid[] SourceItemsIds { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets an additional filter for the list of items in the view
    /// </summary>
    /// <value>The filter query.</value>
    public virtual QueryData AdditionalFilter
    {
      get => this.additionalFilter == null && this.MasterViewDefinition != null ? this.MasterViewDefinition.AdditionalFilter : this.additionalFilter;
      set => this.additionalFilter = value;
    }

    /// <summary>
    /// Gets or sets a constant filter expression for the list of items in the view
    /// </summary>
    /// <value>The filter expression.</value>
    public virtual string FilterExpression
    {
      get => this.filterExpression.IsNullOrEmpty() && this.MasterViewDefinition != null ? this.MasterViewDefinition.FilterExpression : this.filterExpression;
      set => this.filterExpression = value;
    }

    /// <summary>
    /// Gets or sets the data source.
    /// The items are not paged and sorted.
    /// </summary>
    /// <value>The data source.</value>
    public IQueryable<DynamicContent> DataSource
    {
      get
      {
        if (this.dataSource == null)
        {
          if (this.ParentItem != null)
            this.dataSource = this.DynamicManager.GetItemSuccessors(this.ParentItem, this.DynamicContentType);
          else if (this.MasterViewDefinition != null && this.MasterViewDefinition.ItemsParentsIds != null && this.MasterViewDefinition.ItemsParentsIds.Count > 0)
          {
            this.dataSource = Enumerable.Empty<DynamicContent>().AsQueryable<DynamicContent>();
            Type parentItemType = DynamicContentExtensions.GetParentItemType(this.DynamicContentType);
            this.dataSource = this.DynamicManager.GetItemSuccessors(this.MasterViewDefinition.ItemsParentsIds.ToList<Guid>(), this.DynamicContentType, parentItemType);
          }
          else
          {
            this.dataSource = this.GetNonPagedDataSource();
            if (this.SourceItemsIds != null)
              this.dataSource = this.dataSource.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (n => this.SourceItemsIds.Contains<Guid>(n.OriginalContentId)));
          }
        }
        return this.dataSource;
      }
      set => this.dataSource = value;
    }

    /// <summary>Gets or sets the pager BaseUrl.</summary>
    /// <value>The pager BaseUrl.</value>
    public string PagerBaseUrl { get; set; }

    /// <summary>
    /// When in multilingual mode gets or sets whether items with no translation for the current language will be shown.
    /// </summary>
    public virtual bool? ItemLanguageFallback
    {
      get => !this.itemLanguageFallback.HasValue && this.MasterViewDefinition != null ? this.MasterViewDefinition.ItemLanguageFallback : this.itemLanguageFallback;
      set => this.itemLanguageFallback = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether URL queries are allowed.
    /// The default value is true.
    /// </summary>
    /// <value><c>true</c> if URL queries are allowed; otherwise, <c>false</c>.</value>
    public virtual bool? AllowUrlQueries
    {
      get => !this.allowUrlQueries.HasValue && this.MasterViewDefinition != null ? this.MasterViewDefinition.AllowUrlQueries : this.allowUrlQueries;
      set => this.allowUrlQueries = value;
    }

    /// <summary>
    /// Gets the reference to the <see cref="!:ListView" /> control that displays the list of
    /// all dynamic content.
    /// </summary>
    protected virtual RadListView DynamicContentListView => this.Container.GetControl<RadListView>("dynamicContentListView", true);

    /// <summary>
    /// Gets the reference to the control that represents the container of
    /// the parent item details.
    /// </summary>
    protected virtual DynamicDetailContainer ParentDetailContainer => this.Container.GetControl<DynamicDetailContainer>("parentDetailContainer", false);

    /// <summary>Gets the pager.</summary>
    /// <value>The pager.</value>
    protected virtual Pager Pager => this.Container.GetControl<Pager>("pager", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the
    /// event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.SelectedItem != null)
      {
        Guid selectedItemId = this.SelectedItem.Status == ContentLifecycleStatus.Master ? this.SelectedItem.Id : this.SelectedItem.OriginalContentId;
        this.DynamicContentListView.Items.FirstOrDefault<RadListViewDataItem>((Func<RadListViewDataItem, bool>) (i => ((DynamicContent) i.DataItem).Status != ContentLifecycleStatus.Master ? ((DynamicContent) i.DataItem).OriginalContentId == selectedItemId : ((DynamicContent) i.DataItem).Id == selectedItemId))?.FireCommandEvent("Select", (object) string.Empty);
      }
      if (this.ParentDetailContainer == null)
        return;
      bool flag = this.ParentItem != null;
      this.ParentDetailContainer.Visible = flag;
      if (!flag)
        return;
      this.ParentDetailContainer.DataBind();
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.DynamicContentListView.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(this.DynamicContentListView_ItemDataBound);
      this.DynamicContentListView.NeedDataSource += new EventHandler<RadListViewNeedDataSourceEventArgs>(this.DynamicContentListView_NeedDataSource);
      if (this.ParentDetailContainer == null || this.ParentItem == null)
        return;
      this.ParentDetailContainer.DataSource = (object) new DynamicContent[1]
      {
        this.ParentItem
      };
      this.ParentDetailContainer.DataBound += new EventHandler(this.ParentDetailContainer_DataBound);
    }

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Handles the data bound event of the parent detail container.
    /// </summary>
    protected virtual void ParentDetailContainer_DataBound(object sender, EventArgs e)
    {
      AssetsFieldBinder.BindAllAssetsFields((Control) this.ParentDetailContainer, (IDataItem) this.ParentItem);
      AddressFieldBinder.BindAllAddressFields((Control) this.ParentDetailContainer, (IDataItem) this.ParentItem);
    }

    /// <summary>
    /// Handles the NeedDataSource event of the DynamicContentListView
    /// </summary>
    protected virtual void DynamicContentListView_NeedDataSource(
      object sender,
      RadListViewNeedDataSourceEventArgs e)
    {
      int? totalCount = new int?();
      bool? allowPaging = this.MasterViewDefinition.AllowPaging;
      bool flag = true;
      if (allowPaging.GetValueOrDefault() == flag & allowPaging.HasValue && this.MasterViewDefinition.ItemsPerPage.HasValue)
        totalCount = new int?(0);
      List<DynamicContent> items = InlineEditingHelper.ApplyItemFilter<DynamicContent>((IManager) this.DynamicManager, (this.Host == null || !this.Host.DisplayRelatedData() ? (IEnumerable<DynamicContent>) this.GetDataSource(ref totalCount) : (IEnumerable<DynamicContent>) Queryable.OfType<DynamicContent>(this.Host.GetRelatedItems(this.MasterViewDefinition.FilterExpression, this.MasterViewDefinition.SortExpression, this.MasterViewDefinition.ItemsPerPage, ref totalCount)).AsQueryable<DynamicContent>()).ToList<DynamicContent>());
      this.DynamicContentListView.DataSource = (object) items;
      if (this.TitleControl != null && items.Count > 0)
        this.TitleControl.Text = this.Title;
      this.ConfigurePager(totalCount);
      ((IEnumerable<IDataItem>) items).SetRelatedDataSourceContext();
    }

    private IQueryable<DynamicContent> GetNonPagedDataSource() => this.DynamicManager.GetDataItems(TypeResolutionService.ResolveType(this.DynamicContentType.FullName));

    private IQueryable<DynamicContent> GetDataSource(ref int? totalCount)
    {
      IQueryable<DynamicContent> queryable = this.DataSource;
      if (!this.AllowUrlQueries.HasValue || this.AllowUrlQueries.Value)
      {
        queryable = this.EvaluateUrl<DynamicContent>(queryable, "Date", "PublicationDate", this.UrlEvaluationMode, this.UrlKeyPrefix);
        if (this.HasClassificationFields())
          queryable = this.EvaluateUrl<DynamicContent>(queryable, "Taxonomy", "", this.DynamicContentType, this.UrlEvaluationMode, this.UrlKeyPrefix);
      }
      if (!string.IsNullOrEmpty(this.MasterViewDefinition.SortExpression))
        queryable = queryable.OrderBy<DynamicContent>(this.MasterViewDefinition.SortExpression);
      int? skip = new int?(0);
      if (this.MasterViewDefinition.AllowPaging.HasValue && this.MasterViewDefinition.AllowPaging.Value)
        skip = new int?(this.GetItemsToSkipCount(this.MasterViewDefinition.ItemsPerPage, this.UrlEvaluationMode, this.UrlKeyPrefix));
      if (this.FilterExpression.IsNullOrEmpty())
        this.FilterExpression = "Visible = true AND Status = Live";
      if (DynamicTypesHelper.IsTypeLocalizable(TypeResolutionService.ResolveType(this.DynamicContentType.FullName)))
        this.MasterViewDefinition.AdaptMultilingualFilterExpression();
      string filterExpression = DefinitionsHelper.GetFilterExpression(this.FilterExpression, this.AdditionalFilter);
      return this.SetExpressions(queryable, filterExpression, this.MasterViewDefinition.SortExpression, skip, this.MasterViewDefinition.ItemsPerPage, ref totalCount);
    }

    private bool HasClassificationFields()
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this.DynamicContentType))
      {
        if (property is TaxonomyPropertyDescriptor)
          return true;
      }
      return false;
    }

    protected virtual IQueryable<DynamicContent> SetExpressions(
      IQueryable<DynamicContent> query,
      string filterExpression,
      string sortExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      query = DataProviderBase.SetExpressions<DynamicContent>(query, filterExpression, sortExpression, skip, new int?(take ?? 0), ref totalCount);
      return query;
    }

    /// <summary>Configures the pager.</summary>
    /// <param name="vrtualItemCount">The virtual item count.</param>
    /// <param name="masterDefinition">The master definition.</param>
    private void ConfigurePager(int? virtualItemCount)
    {
      if (virtualItemCount.HasValue)
      {
        bool? nullable = this.MasterViewDefinition.AllowPaging;
        bool flag = true;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue && this.MasterViewDefinition.ItemsPerPage.HasValue && this.MasterViewDefinition.ItemsPerPage.Value > 0)
        {
          this.Pager.VirtualItemCount = virtualItemCount.Value;
          this.Pager.PageSize = this.MasterViewDefinition.ItemsPerPage.Value;
          this.Pager.QueryParamKey = this.UrlKeyPrefix;
          this.Pager.BaseUrl = this.PagerBaseUrl;
          this.Pager.UrlEvaluationMode = this.UrlEvaluationMode;
          if (this.Host != null)
          {
            nullable = this.Host.SetPaginationUrls;
            if (nullable.HasValue)
            {
              Pager pager = this.Pager;
              nullable = this.Host.SetPaginationUrls;
              int num = nullable.Value ? 1 : 0;
              pager.SetPaginationUrls = num != 0;
            }
          }
          if (this.Host == null || !this.Host.DisplayRelatedData())
            return;
          this.Pager.BaseUrl = this.Host.GetPagerBaseUrl();
          this.Pager.UrlEvaluationMode = UrlEvaluationMode.QueryString;
          return;
        }
      }
      this.Pager.Visible = false;
    }

    /// <summary>
    /// Handles the item data bound event of the dynamic content list view.
    /// </summary>
    /// <param name="sender">
    /// The instance of the control that invoked the event.
    /// </param>
    /// <param name="e">The event arguments associated with this event.</param>
    protected virtual void DynamicContentListView_ItemDataBound(
      object sender,
      RadListViewItemEventArgs e)
    {
      if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
        return;
      RadListViewDataItem listViewDataItem = (RadListViewDataItem) e.Item;
      if (this.BindAddressFields((Control) e.Item))
        AddressFieldBinder.BindAllAddressFields((Control) e.Item, (IDataItem) listViewDataItem.DataItem);
      if (this.BindAssetsFields((Control) e.Item))
        AssetsFieldBinder.BindAllAssetsFields((Control) e.Item, (IDataItem) listViewDataItem.DataItem);
      foreach (object control in e.Item.Controls)
      {
        if (control is IRelatedDataView relatedDataView && relatedDataView.DisplayRelatedData() && listViewDataItem.DataItem is ILocatable dataItem)
          relatedDataView.UrlKeyPrefix += (string) dataItem.UrlName;
      }
    }

    private bool BindAddressFields(Control container)
    {
      if (!this.bindAddressFields.HasValue)
      {
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(TypeResolutionService.ResolveType(this.DynamicContentType.FullName)))
        {
          if (property.PropertyType == typeof (Address) && DynamicContentViewMaster.HasControl(container, typeof (AddressField)))
          {
            this.bindAddressFields = new bool?(true);
            return this.bindAddressFields.Value;
          }
        }
        this.bindAddressFields = new bool?(false);
      }
      return this.bindAddressFields.Value;
    }

    private bool BindAssetsFields(Control container)
    {
      if (!this.bindAssetsFields.HasValue)
      {
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(TypeResolutionService.ResolveType(this.DynamicContentType.FullName)))
        {
          if (property.PropertyType == typeof (ContentLink[]) && DynamicContentViewMaster.HasControl(container, typeof (AssetsField)))
          {
            this.bindAssetsFields = new bool?(true);
            return this.bindAssetsFields.Value;
          }
        }
        this.bindAssetsFields = new bool?(false);
      }
      return this.bindAssetsFields.Value;
    }

    private static bool HasControl(Control container, Type controlType)
    {
      foreach (Control control in container.Controls)
      {
        if (controlType.IsAssignableFrom(control.GetType()) || DynamicContentViewMaster.HasControl(control, controlType))
          return true;
      }
      return false;
    }
  }
}
