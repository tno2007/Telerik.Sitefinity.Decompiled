// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Base fluent API facade that defines a definition for content view master element.
  /// </summary>
  /// <typeparam name="TElement">The type of the element.</typeparam>
  /// <typeparam name="TActualFacade">The type of the actual facade.</typeparam>
  public abstract class BaseMasterDefinitionFacade<TElement, TActualFacade> : 
    ContentViewDefinitionFacade<TElement, TActualFacade>
    where TElement : ContentViewMasterElement
    where TActualFacade : BaseMasterDefinitionFacade<TElement, TActualFacade>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public BaseMasterDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName,
      ContentViewControlDefinitionFacade parentFacade)
      : base(moduleName, definitionName, contentType, parentElement, viewName, parentFacade)
    {
    }

    /// <summary>Sets the base service url of the master view.</summary>
    /// <param name="serviceUrl">Service Url.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetServiceBaseUrl(string serviceUrl)
    {
      this.ContentView.WebServiceBaseUrl = !string.IsNullOrEmpty(serviceUrl) ? serviceUrl : throw new ArgumentNullException(nameof (serviceUrl));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets a comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers)
    /// </summary>
    /// <param name="providersGroups">A comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers)</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetProvidersGroups(string providersGroups)
    {
      this.ContentView.ProvidersGroups = !string.IsNullOrEmpty(providersGroups) ? providersGroups : throw new ArgumentNullException(nameof (providersGroups));
      return this as TActualFacade;
    }

    /// <summary>The master view allows paging of the list of items.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade EnablePaging()
    {
      this.ContentView.AllowPaging = new bool?(true);
      return this as TActualFacade;
    }

    /// <summary>
    /// The master view does not allow paging of the list of items.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade DisablePaging()
    {
      this.ContentView.AllowPaging = new bool?(false);
      return this as TActualFacade;
    }

    /// <summary>Enables sorting of the master view.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade EnableSorting()
    {
      this.ContentView.DisableSorting = new bool?(false);
      return this as TActualFacade;
    }

    /// <summary>Disables sorting of the master view.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade DisableSorting()
    {
      this.ContentView.DisableSorting = new bool?(true);
      return this as TActualFacade;
    }

    /// <summary>Allows URL queries.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade AllowUrlQueries()
    {
      this.ContentView.AllowUrlQueries = new bool?(true);
      return this as TActualFacade;
    }

    /// <summary>Forbids URL queries.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade ForbidUrlQueries()
    {
      this.ContentView.AllowUrlQueries = new bool?(false);
      return this as TActualFacade;
    }

    /// <summary>
    /// When paging is enabled through the AllowPaging property, how many items per page are displayed.
    /// </summary>
    /// <param name="itemsPerPage">Items per page.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetItemsPerPage(int itemsPerPage)
    {
      this.ContentView.ItemsPerPage = new int?(itemsPerPage);
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets a constant filter expression for the list of items in the view.
    /// </summary>
    /// <param name="filter">The filter expression.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetFilterExpression(string filter)
    {
      this.ContentView.FilterExpression = !string.IsNullOrEmpty(filter) ? filter : throw new ArgumentNullException(nameof (filter));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets an additional filter for the list of items in the view.
    /// </summary>
    /// <param name="filter">The additional filter.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetAdditionalFilter(QueryData filter)
    {
      this.ContentView.AdditionalFilter = filter != null ? filter : throw new ArgumentNullException(nameof (filter));
      return this as TActualFacade;
    }

    /// <summary>Sets the sort expression for the list of items</summary>
    /// <param name="sortExpression">The sort expression.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetSortExpression(string sortExpression)
    {
      this.ContentView.SortExpression = !string.IsNullOrEmpty(sortExpression) ? sortExpression : throw new ArgumentNullException(nameof (sortExpression));
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets the ID of the page that should display the details view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <param name="id">The details page ID.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetDetailsPageId(Guid id)
    {
      this.ContentView.DetailsPageId = id;
      return this as TActualFacade;
    }

    /// <summary>
    /// Sets a value indicating whether the template ought to be evaluated on the
    /// client or on the server.
    /// </summary>
    /// <param name="mode">Template evaluation mode.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetTemplateEvaluationMode(TemplateEvalutionMode mode)
    {
      this.ContentView.TemplateEvaluationMode = mode;
      return this as TActualFacade;
    }

    /// <summary>Sets the items parent ID.</summary>
    /// <param name="id">The items parent ID.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetItemsParentId(Guid id)
    {
      this.ContentView.ItemsParentId = id;
      return this as TActualFacade;
    }

    /// <summary>Sets the items parents IDs.</summary>
    /// <param name="ids">The items parents IDs.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade SetItemsParentIds(ICollection<Guid> ids)
    {
      this.ContentView.ItemsParentsIds = ids != null ? ids : throw new ArgumentNullException(nameof (ids));
      return this as TActualFacade;
    }

    /// <summary>Renders links in master view.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade RenderLinksInMasterView()
    {
      this.ContentView.RenderLinksInMasterView = new bool?(true);
      return this as TActualFacade;
    }

    /// <summary>Does not render links in master view.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade DoNotRenderLinksInMasterView()
    {
      this.ContentView.RenderLinksInMasterView = new bool?(false);
      return this as TActualFacade;
    }

    /// <summary>
    /// In multilingual mode, shows items with no translation for the current language as well.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade EnableLanguageFallback()
    {
      this.ContentView.ItemLanguageFallback = new bool?(true);
      return this as TActualFacade;
    }

    /// <summary>
    /// In multilingual mode, shows only translated items for the current language.
    /// </summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.BaseMasterDefinitionFacade`2" />.</returns>
    public TActualFacade DisableLanguageFallback()
    {
      this.ContentView.ItemLanguageFallback = new bool?(false);
      return this as TActualFacade;
    }
  }
}
