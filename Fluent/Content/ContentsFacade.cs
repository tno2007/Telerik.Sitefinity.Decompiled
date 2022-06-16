// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.ContentsFacade`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Fluent.Content
{
  [Obsolete]
  public class ContentsFacade<TCurrentFacade, TContent, TParentFacade> : 
    ICollectionFacade<TCurrentFacade, TContent>,
    IFacade<TCurrentFacade>
    where TCurrentFacade : class
    where TContent : Telerik.Sitefinity.GenericContent.Model.Content
    where TParentFacade : class
  {
    private AppSettings appSettings;
    private IContentManager contentManager;
    private TParentFacade parentFacade;
    private IQueryable<TContent> items;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentsFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public ContentsFacade(AppSettings appSettings) => this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentsFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ContentsFacade(AppSettings appSettings, TParentFacade parentFacade)
      : this(appSettings)
    {
      this.parentFacade = (object) parentFacade != null ? parentFacade : throw new ArgumentNullException(nameof (parentFacade));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentsFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="items">The items that the facade will work with.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ContentsFacade(
      AppSettings appSettings,
      IQueryable<TContent> items,
      TParentFacade parentFacade)
      : this(appSettings, parentFacade)
    {
      this.items = items != null ? items : throw new ArgumentNullException(nameof (items));
    }

    internal ContentsFacade()
    {
    }

    /// <summary>
    /// Gets the count of items in collection at current facade.
    /// </summary>
    /// <param name="result">The count of items.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Count(out int result)
    {
      this.EnsureExistence(true);
      result = this.ContentItems.Count<TContent>();
      return this.CurrentFacade();
    }

    /// <summary>
    /// Performs an arbitrary action for each item in the collection of the facade.
    /// </summary>
    /// <param name="action">An action to be performed for each item of collection.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade ForEach(Action<TContent> action)
    {
      this.EnsureExistence(true);
      foreach (TContent contentItem in (IEnumerable<TContent>) this.ContentItems)
      {
        action(contentItem);
        CommonMethods.RecompileItemUrls((Telerik.Sitefinity.GenericContent.Model.Content) contentItem, (IManager) this.ContentManager);
      }
      return this.CurrentFacade();
    }

    /// <summary>Gets query with instances of the content type.</summary>
    /// <returns>An instance of IQueryable object with the content items. </returns>
    public IQueryable<TContent> Get() => this.ContentItems;

    /// <summary>
    /// Orders the items in the collection in ascending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade OrderBy<TKey>(Expression<Func<TContent, TKey>> keySelector)
    {
      this.EnsureExistence(true);
      this.ContentItems = (IQueryable<TContent>) this.ContentItems.OrderBy<TContent, TKey>(keySelector);
      return this.CurrentFacade();
    }

    /// <summary>
    /// Orders the items in the collection in descending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade OrderByDescending<TKey>(
      Expression<Func<TContent, TKey>> keySelector)
    {
      this.EnsureExistence(true);
      this.ContentItems = (IQueryable<TContent>) this.ContentItems.OrderByDescending<TContent, TKey>(keySelector);
      return this.CurrentFacade();
    }

    /// <summary>Sets the specified query.</summary>
    /// <param name="query">The query to be set.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Set(IQueryable<TContent> query)
    {
      this.ContentItems = query != null ? query : throw new ArgumentNullException(nameof (query));
      return this.CurrentFacade();
    }

    /// <summary>
    /// Skips the specified count of items from the collection.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Skip(int count)
    {
      this.EnsureExistence(true);
      this.ContentItems = this.ContentItems.Skip<TContent>(count).ToList<TContent>().AsQueryable<TContent>();
      return this.CurrentFacade();
    }

    /// <summary>
    /// Takes the specified count of items from the collection.
    /// </summary>
    /// <param name="count">The count of item to be taken.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Take(int count)
    {
      this.EnsureExistence(true);
      this.ContentItems = this.ContentItems.Take<TContent>(count).ToList<TContent>().AsQueryable<TContent>();
      return this.CurrentFacade();
    }

    /// <summary>
    /// Filters items of the collection by the specified <paramref name="predicate" /> parameter.
    /// </summary>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Where(Func<TContent, bool> predicate)
    {
      this.EnsureExistence(true);
      this.ContentItems = this.ContentItems.Where<TContent>(predicate).AsQueryable<TContent>();
      return this.CurrentFacade();
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade SaveChanges()
    {
      TransactionManager.CommitTransaction(this.appSettings.TransactionName);
      return this.CurrentFacade();
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.ContentManager.TransactionName);
      return this.CurrentFacade();
    }

    /// <summary>Deletes the items in the collection.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor or Set() method.
    /// </exception>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Delete()
    {
      this.EnsureExistence(true);
      foreach (TContent contentItem in (IEnumerable<TContent>) this.ContentItems)
        this.ContentManager.DeleteItem((object) contentItem);
      return this.CurrentFacade();
    }

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if method is called and parentFacade is null; meaning that facade is not a child facade in this context.
    /// </exception>
    /// <returns>An instance of the parent facade type that initialized this facade as a child facade.</returns>
    public TParentFacade Done() => (object) this.parentFacade != null ? this.parentFacade : throw new InvalidOperationException("The facade instance is not set a parent facade instance.");

    /// <summary>
    /// Gets or sets the query for this content facade .This query is used
    /// by the fluent API and all methods are executed on this query.
    /// </summary>
    protected internal virtual IQueryable<TContent> ContentItems
    {
      get
      {
        if (this.items == null)
          this.items = this.ContentManager.GetItems<TContent>();
        return this.items;
      }
      set => this.items = value;
    }

    /// <summary>
    /// Gets an instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.IContentManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.IContentManager" /> class.</value>
    protected internal virtual IContentManager ContentManager
    {
      get
      {
        if (this.contentManager == null)
        {
          this.contentManager = ManagerBase.GetMappedManagerInTransaction(typeof (TContent), this.appSettings.PagesProviderName, this.appSettings.TransactionName) as IContentManager;
          if (!(this.contentManager is ICommentsManager))
            throw new Exception("The manages does not implement ICommentsManager interface");
        }
        return this.contentManager;
      }
      internal set => this.contentManager = value;
    }

    /// <summary>
    /// This method is called to ensure that a collection of items has been loaded (either from the contstructor or by the Set() method).
    /// </summary>
    /// <param name="throwExceptionIfNot">
    /// If this parameter is set to true, an exception will be thrown; otherwise method can be used in
    /// a less obtrusive way when it only returns the boolean value indicating whether a page has been loaded.
    /// </param>
    protected internal virtual void EnsureExistence(bool throwExceptionIfNot)
    {
      if (throwExceptionIfNot && this.ContentItems == null)
        throw new InvalidOperationException("This method must not be called before the content object has been loaded either through the constructor or by calling the CreateNew method.");
    }

    /// <summary>
    /// Gets or sets the parent facade that initialized this facade.
    /// </summary>
    protected internal virtual TParentFacade ParentFacade
    {
      get => this.parentFacade;
      set => this.parentFacade = value;
    }

    /// <summary>
    /// Casts the current instance to the type parameter of the current facade.
    /// </summary>
    /// <returns>An instance of the current facade type.</returns>
    /// <exception cref="T:System.InvalidCastException">
    /// thrown if the current instance cannot be cast to the type parameter of the current facade
    /// </exception>
    protected TCurrentFacade CurrentFacade() => this is TCurrentFacade currentFacade ? currentFacade : throw new InvalidCastException(string.Format("Cannot cast {0} to {1}", (object) this.GetType().FullName, (object) typeof (TCurrentFacade).FullName));
  }
}
