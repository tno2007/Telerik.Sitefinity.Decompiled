// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentViewMasterDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of the respective master view control.
  /// </summary>
  public class ContentViewMasterDefinition : 
    ContentViewDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private bool? allowPaging;
    private bool? allowUrlQueries;
    private bool? disableSorting;
    private string filterExpression;
    private QueryData additionalFilter;
    private int? itemsPerPage;
    private string sortExpression;
    private Guid detailsPageId;
    private string webServiceBaseUrl;
    private TemplateEvalutionMode templateEvaluationMode;
    private Guid itemsParentId;
    private ICollection<Guid> itemsParentsIds;
    private bool? renderLinksInMasterView;
    private bool? itemLanguageFallback;
    private bool? canUsersSetItemsPerPage;
    private string providersGroups;
    private bool? includeDescendantItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewMasterDefinition" /> class.
    /// </summary>
    public ContentViewMasterDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewMasterDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ContentViewMasterDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ContentViewMasterDefinition GetDefinition() => this;

    /// <summary>
    /// Specifies whether the master view allows paging of the list of items
    /// </summary>
    /// <value>A Boolean value. True if paging is allowed</value>
    public bool? AllowPaging
    {
      get => this.ResolveProperty<bool?>(nameof (AllowPaging), this.allowPaging);
      set => this.allowPaging = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether URL queries are allowed.
    /// The default value is true.
    /// </summary>
    /// <value><c>true</c> if URL queries are allowed; otherwise, <c>false</c>.</value>
    public bool? AllowUrlQueries
    {
      get => this.ResolveProperty<bool?>(nameof (AllowUrlQueries), this.allowUrlQueries);
      set => this.allowUrlQueries = value;
    }

    /// <summary>If true sorting for the master view is disabled.</summary>
    /// <value></value>
    public bool? DisableSorting
    {
      get => this.ResolveProperty<bool?>(nameof (DisableSorting), this.disableSorting);
      set => this.disableSorting = value;
    }

    /// <summary>
    /// Gets or sets the filter expression for the list of items in the view
    /// </summary>
    /// <value>The filter expression.</value>
    public string FilterExpression
    {
      get => this.ResolveProperty<string>(nameof (FilterExpression), this.filterExpression);
      set => this.filterExpression = value;
    }

    /// <summary>
    /// Gets or sets the filter query for the list of items in the view
    /// </summary>
    /// <value>The filter query.</value>
    [TypeConverter(typeof (JsonTypeConverter<QueryData>))]
    public QueryData AdditionalFilter
    {
      get => this.ResolveProperty<QueryData>(nameof (AdditionalFilter), this.additionalFilter);
      set => this.additionalFilter = value;
    }

    /// <summary>
    /// When paging is enabled through the AllowPaging property, how many items per page are displayed.
    /// </summary>
    /// <value>The number of items per page.</value>
    public int? ItemsPerPage
    {
      get => this.ResolveProperty<int?>(nameof (ItemsPerPage), this.itemsPerPage);
      set => this.itemsPerPage = value;
    }

    /// <summary>
    /// Whether or not users can select the number of items to display per page.
    /// </summary>
    /// <value>true if users should have this option, false if not.</value>
    public bool? CanUsersSetItemsPerPage
    {
      get => this.ResolveProperty<bool?>(nameof (CanUsersSetItemsPerPage), this.canUsersSetItemsPerPage);
      set => this.canUsersSetItemsPerPage = value;
    }

    /// <summary>
    /// Gets or sets the sort expression for the list of items
    /// </summary>
    /// <value>The sort expression.</value>
    public string SortExpression
    {
      get => this.ResolveProperty<string>(nameof (SortExpression), this.sortExpression);
      set => this.sortExpression = value;
    }

    /// <summary>
    /// Gets or sets the ID of the page that should display the details view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId.</value>
    public Guid DetailsPageId
    {
      get => this.ResolveProperty<Guid>(nameof (DetailsPageId), this.detailsPageId);
      set => this.detailsPageId = value;
    }

    /// <summary>Gets or sets the base url of the web service.</summary>
    /// <value></value>
    public string WebServiceBaseUrl
    {
      get => this.ResolveProperty<string>(nameof (WebServiceBaseUrl), this.webServiceBaseUrl);
      set => this.webServiceBaseUrl = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the template ought to be evaluated on the
    /// client or on the server.
    /// </summary>
    /// <value></value>
    public TemplateEvalutionMode TemplateEvaluationMode
    {
      get => this.ResolveProperty<TemplateEvalutionMode>(nameof (TemplateEvaluationMode), this.templateEvaluationMode);
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the ID of the page that should display the details view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId.</value>
    public Guid ItemsParentId
    {
      get => this.ResolveProperty<Guid>(nameof (ItemsParentId), this.itemsParentId);
      set => this.itemsParentId = value;
    }

    /// <summary>Gets or sets the items parents IDs.</summary>
    [TypeConverter(typeof (CollectionJsonTypeConverter<Guid>))]
    public ICollection<Guid> ItemsParentsIds
    {
      get => this.ResolveProperty<ICollection<Guid>>(nameof (ItemsParentsIds), this.itemsParentsIds, (ICollection<Guid>) new Collection<Guid>());
      set => this.itemsParentsIds = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to render links to detail view in the master view.
    /// </summary>
    /// <value></value>
    public bool? RenderLinksInMasterView
    {
      get => this.ResolveProperty<bool?>(nameof (RenderLinksInMasterView), this.renderLinksInMasterView);
      set => this.renderLinksInMasterView = value;
    }

    /// <summary>
    /// When in multilingual mode gets or sets whether items with no translation for the current language will be shown.
    /// </summary>
    /// <value></value>
    public bool? ItemLanguageFallback
    {
      get => this.ResolveProperty<bool?>(nameof (ItemLanguageFallback), this.itemLanguageFallback);
      set => this.itemLanguageFallback = value;
    }

    /// <summary>
    /// Gets or sets a comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers)
    /// </summary>
    /// <value>A comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).</value>
    public string ProvidersGroups
    {
      get => this.ResolveProperty<string>(nameof (ProvidersGroups), this.providersGroups);
      set => this.providersGroups = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include all descendant items of the parent id, instead of just the children.
    /// </summary>
    public bool? IncludeDescendantItems
    {
      get => this.ResolveProperty<bool?>(nameof (IncludeDescendantItems), this.includeDescendantItems);
      set => this.includeDescendantItems = value;
    }
  }
}
