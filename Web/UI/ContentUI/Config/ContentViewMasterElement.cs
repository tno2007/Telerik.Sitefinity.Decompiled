// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewMasterElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// The configuration element for ContentViewMasterDefinition
  /// </summary>
  public class ContentViewMasterElement : 
    ContentViewDefinitionElement,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewMasterElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public ContentViewMasterElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ContentViewMasterDefinition((ConfigElement) this);

    /// <summary>
    /// Specifies whether the master view allows paging of the list of items
    /// </summary>
    /// <value>A Boolean value. True if paging is allowed</value>
    [ConfigurationProperty("allowPaging", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewMasterAllowPagingDescription", Title = "ContentViewMasterAllowPagingCaption")]
    public bool? AllowPaging
    {
      get => (bool?) this["allowPaging"];
      set => this["allowPaging"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether URL queries are allowed.
    /// The default value is true.
    /// </summary>
    /// <value><c>true</c> if URL queries are allowed; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("allowUrlQueries", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowUrlQueriesDescription", Title = "AllowUrlQueriesCaption")]
    public bool? AllowUrlQueries
    {
      get => (bool?) this["allowUrlQueries"];
      set => this["allowUrlQueries"] = (object) value;
    }

    /// <summary>If true sorting for the master view is disabled.</summary>
    /// <value></value>
    [ConfigurationProperty("disableSorting", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewMasterDisableSortingDescription", Title = "ContentViewMasterDisableSortingCaption")]
    public bool? DisableSorting
    {
      get => (bool?) this["disableSorting"];
      set => this["disableSorting"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the filter expression for the list of items in the view
    /// </summary>
    /// <value>The filter expression.</value>
    [ConfigurationProperty("filterExpression", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewMasterFilterExpressionDescription", Title = "ContentViewMasterFilterExpressionCaption")]
    public string FilterExpression
    {
      get => (string) this["filterExpression"];
      set => this["filterExpression"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the filter query for the list of items in the view
    /// </summary>
    /// <value>The filter query.</value>
    [ConfigurationProperty("additionalFilter")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AdditionalFilterDescription", Title = "AdditionalFilterCaption")]
    [TypeConverter(typeof (JsonTypeConverter<QueryData>))]
    public QueryData AdditionalFilter
    {
      get => (QueryData) this["additionalFilter"];
      set => this["additionalFilter"] = (object) value;
    }

    /// <summary>
    /// When paging is enabled through the AllowPaging property, how many items per page are displayed.
    /// </summary>
    /// <value>The number of items per page.</value>
    [ConfigurationProperty("itemsPerPage", DefaultValue = 20)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewMasterItemsPerPageDescription", Title = "ContentViewMasterItemsPerPageCaption")]
    public int? ItemsPerPage
    {
      get => (int?) this["itemsPerPage"];
      set => this["itemsPerPage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether users can set the number of items to display per page.
    /// </summary>
    [ConfigurationProperty("canUsersSetItemsPerPage", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewMasterItemsPerPageDescription", Title = "CanUsersSetItemsPerPageCaption")]
    public bool? CanUsersSetItemsPerPage
    {
      get => (bool?) this["canUsersSetItemsPerPage"];
      set => this["canUsersSetItemsPerPage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the sort expression for the list of items
    /// </summary>
    /// <value>The sort expression.</value>
    [ConfigurationProperty("sortExpression", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewMasterSortExpressionDescription", Title = "ContentViewMasterSortExpressionCaption")]
    public string SortExpression
    {
      get => (string) this["sortExpression"];
      set => this["sortExpression"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the ID of the page that should display the details view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId.</value>
    [ConfigurationProperty("detailsPageId", DefaultValue = "00000000-0000-0000-0000-000000000000", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DetailsPageIdDescription", Title = "DetailsPageIdCaption")]
    public Guid DetailsPageId
    {
      get => (Guid) this["detailsPageId"];
      set => this["detailsPageId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the ID of the page that should display the details view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId.</value>
    [ConfigurationProperty("webServiceBaseUrl", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WebServiceBaseUrlDescription", Title = "WebServiceBaseUrlCaption")]
    public string WebServiceBaseUrl
    {
      get => (string) this["webServiceBaseUrl"];
      set => this["webServiceBaseUrl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the template ought to be evaluated on the
    /// client or on the server.
    /// </summary>
    [ConfigurationProperty("templateEvaluationMode", DefaultValue = TemplateEvalutionMode.None)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TemplateEvaluationModeDescription", Title = "TemplateEvaluationModeCaption")]
    public TemplateEvalutionMode TemplateEvaluationMode
    {
      get => (TemplateEvalutionMode) this["templateEvaluationMode"];
      set => this["templateEvaluationMode"] = (object) value;
    }

    [ConfigurationProperty("itemsParentId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemsParentIdDescription", Title = "ItemsParentIdCaption")]
    public Guid ItemsParentId
    {
      get => (Guid) this["itemsParentId"];
      set => this["itemsParentId"] = (object) value;
    }

    /// <summary>Gets or sets the items parents IDs.</summary>
    [ConfigurationProperty("itemsParentsIds")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemsParentsIdsDescription", Title = "ItemsParentsIdsCaption")]
    public ICollection<Guid> ItemsParentsIds
    {
      get => (ICollection<Guid>) this["itemsParentsIds"];
      set => this["itemsParentsIds"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to render links to detail view in the master view.
    /// </summary>
    [ConfigurationProperty("renderLinksInMasterView", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RenderLinksInMasterViewDescription", Title = "RenderLinksInMasterViewCaption")]
    public bool? RenderLinksInMasterView
    {
      get => (bool?) this["renderLinksInMasterView"];
      set => this["renderLinksInMasterView"] = (object) value;
    }

    /// <summary>
    /// When in multilingual mode gets or sets whether items with no translation for the current language will be shown.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("itemLanguageFallback")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemLanguageFallbackDescription", Title = "ItemLanguageFallbackTitle")]
    public bool? ItemLanguageFallback
    {
      get => (bool?) this["itemLanguageFallback"];
      set => this["itemLanguageFallback"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).
    /// </summary>
    /// <value>A comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).</value>
    [ConfigurationProperty("ProvidersGroups", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProvidersGroupsDescription", Title = "ProvidersGroupsMessage")]
    public string ProvidersGroups
    {
      get => (string) this[nameof (ProvidersGroups)];
      set => this[nameof (ProvidersGroups)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include all descendant items of the parent id, instead of just the children.
    /// </summary>
    [ConfigurationProperty("includeDescendantItems")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IncludeDescendantItemsDescription", Title = "IncludeDescendantItemsCaption")]
    public bool? IncludeDescendantItems
    {
      get => (bool?) this["includeDescendantItems"];
      set => this["includeDescendantItems"] = (object) value;
    }

    /// <summary>
    /// Constants to hold the string keys for configuration properties of ContentViewMasterElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ContentViewMasterProps
    {
      public const string AllowPaging = "allowPaging";
      public const string DisableSorting = "disableSorting";
      public const string SortExpression = "sortExpression";
      public const string FilterExpression = "filterExpression";
      public const string AdditionalFilter = "additionalFilter";
      public const string ItemsPerPage = "itemsPerPage";
      public const string CanUsersSetItemsPerPage = "canUsersSetItemsPerPage";
      public const string DetailsPageId = "detailsPageId";
      public const string AllowUrlQueries = "allowUrlQueries";
      public const string WebServiceBaseUrl = "webServiceBaseUrl";
      public const string TemplateEvaluationMode = "templateEvaluationMode";
      public const string ItemsParentId = "itemsParentId";
      public const string ItemsParentsIds = "itemsParentsIds";
      public const string RenderLinksInMasterView = "renderLinksInMasterView";
      public const string ItemLanguageFallback = "itemLanguageFallback";
      public const string providersGroups = "ProvidersGroups";
      public const string IncludeDescendantItems = "includeDescendantItems";
    }
  }
}
