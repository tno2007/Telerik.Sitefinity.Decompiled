// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewMasterDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Contracts
{
  /// <summary>
  /// Declares the contract for the base ContentMasterView. Implemented by each master view and the configuration element for the view
  /// </summary>
  public interface IContentViewMasterDefinition : IContentViewDefinition, IDefinition
  {
    /// <summary>If true sorting for the master view is disabled.</summary>
    bool? DisableSorting { get; set; }

    /// <summary>
    /// Specifies whether the master view allows paging of the list of items
    /// </summary>
    /// <value>A Boolean value. True if paging is allowed</value>
    bool? AllowPaging { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether URL queries are allowed.
    /// The default value is true.
    /// </summary>
    /// <value><c>true</c> if URL queries are allowed; otherwise, <c>false</c>.</value>
    bool? AllowUrlQueries { get; set; }

    /// <summary>
    /// When paging is enabled through the AllowPaging property, how many items per page are displayed.
    /// </summary>
    /// <value>The number of items per page.</value>
    int? ItemsPerPage { get; set; }

    /// <summary>
    /// Whether users can set the number of items to display per page
    /// </summary>
    bool? CanUsersSetItemsPerPage { get; set; }

    /// <summary>
    /// Gets or sets a constant filter expression for the list of items in the view
    /// </summary>
    /// <value>The filter expression.</value>
    string FilterExpression { get; set; }

    /// <summary>
    /// Gets or sets an additional filter for the list of items in the view
    /// </summary>
    /// <value>The filter query.</value>
    QueryData AdditionalFilter { get; set; }

    /// <summary>
    /// Gets or sets the sort expression for the list of items
    /// </summary>
    /// <value>The sort expression.</value>
    string SortExpression { get; set; }

    /// <summary>
    /// Gets or sets the ID of the page that should display the details view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId.</value>
    Guid DetailsPageId { get; set; }

    /// <summary>Gets or sets the base url of the web service.</summary>
    string WebServiceBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the template ought to be evaluated on the
    /// client or on the server.
    /// </summary>
    TemplateEvalutionMode TemplateEvaluationMode { get; set; }

    /// <summary>Gets or sets the items parent ID.</summary>
    /// <value>The items view parent.</value>
    Guid ItemsParentId { get; set; }

    /// <summary>Gets or sets the items parents IDs.</summary>
    ICollection<Guid> ItemsParentsIds { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to render links to detail view in the master view.
    /// </summary>
    /// <value>A Boolean value. True if the links are to be rendered.</value>
    bool? RenderLinksInMasterView { get; set; }

    /// <summary>
    /// When in multilingual mode gets or sets whether items with no translation for the current language will be shown.
    /// </summary>
    bool? ItemLanguageFallback { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include all descendant items of the parent id, instead of just the children.
    /// </summary>
    bool? IncludeDescendantItems { get; set; }
  }
}
