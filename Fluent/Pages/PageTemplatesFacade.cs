// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Fluent.Pages
{
  /// <summary>
  /// Fluent API that provides most common functionality needed to work with a collection of Sitefinity page templates.
  /// </summary>
  public class PageTemplatesFacade : 
    BaseFacadeWithManager,
    ICollectionFacade<PageTemplatesFacade, PageTemplate>,
    IFacade<PageTemplatesFacade>
  {
    private IQueryable<PageTemplate> pageTemplates;
    private AppSettings appSettings;
    private PageManager pageManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public PageTemplatesFacade(AppSettings appSettings)
      : base(appSettings)
    {
      this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));
    }

    /// <summary>Needed for mocking with JustMock.</summary>
    internal PageTemplatesFacade()
    {
    }

    /// <summary>
    /// Gets an instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade.PageManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade.PageManager" /> class.</value>
    protected internal virtual PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager(this.appSettings.PagesProviderName, this.appSettings.TransactionName);
        return this.pageManager;
      }
    }

    /// <summary>
    /// Gets or sets the query of all page template taxa in the given taxonomy provider. This query is used
    /// by the fluent API and all methods are executed on this query.
    /// </summary>
    protected virtual IQueryable<PageTemplate> PageTemplates
    {
      get
      {
        if (this.pageTemplates == null)
          this.pageTemplates = this.PageManager.GetTemplates();
        return this.pageTemplates;
      }
      set => this.pageTemplates = value;
    }

    /// <summary>Prepares the fluent api provider to start work with.</summary>
    internal FluentSitefinity Fluent => App.Prepare(this.appSettings).WorkWith();

    /// <summary>
    /// Deletes all the page templates currently selected by this instance of the page templates fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" />.</returns>
    public virtual PageTemplatesFacade Delete()
    {
      FluentSitefinity fluent = this.Fluent;
      foreach (PageTemplate pageTemplate in (IEnumerable<PageTemplate>) this.PageTemplates)
      {
        fluent.PageTemplate(pageTemplate).Delete();
        this.appSettings.TrackDeletedItem((IDataItem) pageTemplate);
      }
      return this;
    }

    /// <summary>
    /// Performs a specified action on each page template selected by this instance of the page templates fluent API.
    /// </summary>
    /// <param name="action">An action to be performed on each page template.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" />.</returns>
    public virtual PageTemplatesFacade ForEach(Action<PageTemplate> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      foreach (PageTemplate pageTemplate in (IEnumerable<PageTemplate>) this.PageTemplates)
        action(pageTemplate);
      return this;
    }

    /// <summary>
    /// Gets the count of items in collection at current facade.
    /// </summary>
    /// <param name="result">The count of items.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade Count(out int result)
    {
      result = this.PageTemplates.Count<PageTemplate>();
      return this;
    }

    /// <summary>
    ///  Orders the items of collection in ascending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade OrderBy<TKey>(
      Expression<Func<PageTemplate, TKey>> keySelector)
    {
      this.PageTemplates = keySelector != null ? (IQueryable<PageTemplate>) this.PageTemplates.OrderBy<PageTemplate, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Orders the items of collection in descending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade OrderByDescending<TKey>(
      Expression<Func<PageTemplate, TKey>> keySelector)
    {
      this.PageTemplates = keySelector != null ? (IQueryable<PageTemplate>) this.PageTemplates.OrderBy<PageTemplate, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>Sets the specified query with page templates</summary>
    /// <param name="query">The query with page templates.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade Set(IQueryable<PageTemplate> query)
    {
      this.PageTemplates = query != null ? query : throw new ArgumentNullException(nameof (query));
      return this;
    }

    /// <summary>
    /// Bypasses a specified number of items of collection and then returns the remaining elements.
    /// </summary>
    /// <param name="count">The number of items to bypass.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade Skip(int count)
    {
      this.PageTemplates = count >= 0 ? this.PageTemplates.Skip<PageTemplate>(count).ToList<PageTemplate>().AsQueryable<PageTemplate>() : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Takes items from collection number of which is specified with the count parameter.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade Take(int count)
    {
      this.PageTemplates = count >= 0 ? this.PageTemplates.Take<PageTemplate>(count).ToList<PageTemplate>().AsQueryable<PageTemplate>() : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Filters items of the collection by specified where clause at predicate parameter.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade Where(Func<PageTemplate, bool> predicate)
    {
      this.PageTemplates = predicate != null ? this.PageTemplates.Where<PageTemplate>(predicate).AsQueryable<PageTemplate>() : throw new ArgumentNullException(nameof (predicate));
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade SaveChanges()
    {
      base.SaveChanges();
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade CancelChanges()
    {
      base.CancelChanges();
      return this;
    }

    /// <summary>
    /// Returns the query with page templates with all currently selected page templates.
    /// </summary>
    /// <returns>An instance of IQueryable[PageTemplateTaxon] objects.</returns>
    public virtual IQueryable<PageTemplate> Get() => this.PageTemplates;

    /// <summary>
    /// Filters the page templates to include only the templates that are owned by the specified user.
    /// </summary>
    /// <param name="userId">The id of the user to whom the page templates belong to.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object.</returns>
    public virtual PageTemplatesFacade ThatAreOwnedBy(Guid userId)
    {
      this.PageTemplates = this.PageTemplates.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (pt => pt.Owner == userId));
      return this;
    }

    /// <summary>
    /// Filter the page template to include only the templates that belong to the specified category
    /// </summary>
    /// <param name="category">The id of the category to which the templates belong.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object</returns>
    public virtual IQueryable<PageTemplate> GetByCategory(Guid category) => (IQueryable<PageTemplate>) this.PageTemplates.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category == category)).OrderBy<PageTemplate, short>((Expression<Func<PageTemplate, short>>) (t => t.Ordinal));

    /// <summary>
    /// Filter the page template to include only the templates that do not belong to the specified category
    /// </summary>
    /// <param name="category">The id of the category to which the templates does not belong.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object</returns>
    public virtual IQueryable<PageTemplate> GetNotInCategory(Guid category) => (IQueryable<PageTemplate>) this.PageTemplates.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category != category)).OrderBy<PageTemplate, short>((Expression<Func<PageTemplate, short>>) (t => t.Ordinal));

    /// <summary>
    /// Filter the page template to include only the templates that do belong to the specified site
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object</returns>
    internal virtual PageTemplatesFacade GetInSite(Guid siteId)
    {
      this.PageTemplates = this.PageTemplates.Join((IEnumerable<SiteItemLink>) this.PageManager.GetSiteTemplateLinks(), (Expression<Func<PageTemplate, Guid>>) (c => c.Id), (Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId), (c, l) => new
      {
        c = c,
        l = l
      }).Where(data => data.l.SiteId == siteId).Select(data => data.c);
      return this;
    }

    /// <summary>
    /// Filter the page templates to include only the templates that are not linked to any site.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageTemplatesFacade" /> object</returns>
    internal virtual PageTemplatesFacade GetNotShared()
    {
      this.PageTemplates = this.PageTemplates.Except<PageTemplate>((IEnumerable<PageTemplate>) this.PageTemplates.Join((IEnumerable<SiteItemLink>) this.PageManager.GetSiteTemplateLinks(), (Expression<Func<PageTemplate, Guid>>) (c => c.Id), (Expression<Func<SiteItemLink, Guid>>) (l => l.ItemId), (c, l) => new
      {
        c = c,
        l = l
      }).Where(data => data.l.SiteId != Guid.Empty).Select(data => data.c));
      return this;
    }

    internal PageTemplatesFacade InTemplates(IEnumerable<Guid> templateIds)
    {
      Guid[] templateIdsArray = templateIds.ToArray<Guid>();
      this.PageTemplates = this.PageTemplates.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (x => templateIdsArray.Contains<Guid>(x.Id)));
      return this;
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) this.PageManager;
  }
}
