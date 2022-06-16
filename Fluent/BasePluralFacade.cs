// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.BasePluralFacade`4
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// Base class for facades that manage a multitude of data items
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TSingularFacade">Type of the facade that manages single items implementing the <typeparamref name="TDataItem" /> interface</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TDataItem">Type of data item managed by this facade, implementing the <see cref="T:Telerik.Sitefinity.Model.TDataItem" /> interface</typeparam>
  public abstract class BasePluralFacade<TCurrentFacade, TSingularFacade, TParentFacade, TDataItem> : 
    BaseFacadeWithParent<TParentFacade>
    where TCurrentFacade : BasePluralFacade<TCurrentFacade, TSingularFacade, TParentFacade, TDataItem>
    where TSingularFacade : BaseFacade
    where TParentFacade : BaseFacade
    where TDataItem : class, IDataItem
  {
    /// <summary>Do not use outside the Items property!</summary>
    private IQueryable<TDataItem> items;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BasePluralFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BasePluralFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BasePluralFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BasePluralFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BasePluralFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="items">Initial set of items.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="items" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BasePluralFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      IQueryable<TDataItem> items)
      : this(settings, parentFacade)
    {
      FacadeHelper.Assert(items != null, "items can not be null");
      this.Items = items;
    }

    /// <summary>
    /// Get the underlying state (IQueryable of TDataItem) of this facade
    /// </summary>
    /// <param name="currentItem">The instance of the item type currently loaded by the fluent API.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Get(out IQueryable<TDataItem> currentItem)
    {
      currentItem = this.Items;
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Get the underlying state (IQueryable of TDataItem) of this facade
    /// </summary>
    /// <returns>Underlying IQueryable for this facade</returns>
    public virtual IQueryable<TDataItem> Get() => this.Items;

    /// <summary>
    /// Makes the facade load all items of type <typeparamref name="TDataItem" /> with the current provider
    /// </summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade LoadAllItems()
    {
      this.items = (IQueryable<TDataItem>) null;
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Set the internal state of this facade (thus breaking any logical chains -&gt; e.g. Blog-&gt;Posts)
    /// </summary>
    /// <param name="query">New internal state of the facade</param>
    /// <returns>Current facade</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="query" /> is null.</exception>
    public virtual TCurrentFacade Set(IQueryable<TDataItem> query)
    {
      this.Items = query;
      return this.GetCurrentFacade();
    }

    /// <summary>Get the first element in this facade's set of items</summary>
    /// <returns>Facade that operates on the first element of this facade's set of items</returns>
    public virtual TSingularFacade First() => this.GetFacadeInstance<TSingularFacade>(this.Get().First<TDataItem>());

    /// <summary>
    /// Get the first element in this facade's set of items that satisfies a <paramref name="condition" />
    /// </summary>
    /// <param name="condition">An expression to test each element for a condition.</param>
    /// <returns>First element that satisfies <paramref name="condition" /></returns>
    public virtual TSingularFacade FirstThat(Expression<Func<TDataItem, bool>> condition) => this.GetFacadeInstance<TSingularFacade>(this.Get().Where<TDataItem>(condition).First<TDataItem>());

    /// <summary>Get the last element in this facade's set of items</summary>
    /// <returns>Facade that operates on the last element of this facade's set of items</returns>
    public virtual TSingularFacade Last() => this.GetFacadeInstance<TSingularFacade>(this.Get().Last<TDataItem>());

    /// <summary>
    /// Get the last element in this facade's set of items that satisfies a <paramref name="condition" />
    /// </summary>
    /// <param name="condition">An expression to test each element for a condition.</param>
    /// <returns>Last element that satisfies <paramref name="condition" /></returns>
    public virtual TSingularFacade LastThat(Expression<Func<TDataItem, bool>> condition) => this.GetFacadeInstance<TSingularFacade>(this.Get().Where<TDataItem>(condition).Last<TDataItem>());

    /// <summary>
    /// Delete all items in the underlying query of items. Warning: this will be done in memory and will execute the underlying query!
    /// </summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade Delete()
    {
      this.ForEach((Action<TDataItem>) (item => this.GetManager().DeleteItem((object) item)));
      return this.GetCurrentFacade();
    }

    /// <summary>Makes the underlying query skip a number of elements</summary>
    /// <param name="count">Number of items to skip</param>
    /// <returns>This facade</returns>
    /// <exception cref="T:System.ArgumentException">If count is negative</exception>
    public virtual TCurrentFacade Skip(int count)
    {
      FacadeHelper.Assert<ArgumentException>(count >= 0, "Skip count should not be negative");
      if (count > 0)
        this.Items = this.Get().Skip<TDataItem>(count);
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Specifies the maximum number of items that the query should return
    /// </summary>
    /// <param name="count">Maximum number of items to return</param>
    /// <returns>This facade</returns>
    /// <exception cref="T:System.ArgumentException">If count is negative</exception>
    public virtual TCurrentFacade Take(int count)
    {
      FacadeHelper.Assert<ArgumentException>(count >= 0, "Take count should not be negative");
      if (count > 0)
        this.Items = this.Get().Take<TDataItem>(count);
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Returns the number of items in the facade. This will execute the query and potentially make a call to the database.
    /// </summary>
    /// <param name="count">Variable to hold the number of items in the facade</param>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade Count(out int count)
    {
      count = this.Get().Count<TDataItem>();
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Executes an action over . Warning: this will be done in memory and will execute the underlying query!
    /// </summary>
    /// <param name="what">Action to execute over all items</param>
    /// <returns>This facade</returns>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="what" /> is null</exception>
    public virtual TCurrentFacade ForEach(Action<TDataItem> what)
    {
      FacadeHelper.AssertArgumentNotNull<Action<TDataItem>>(what, nameof (what));
      foreach (TDataItem dataItem in (IEnumerable<TDataItem>) this.Get())
        what(dataItem);
      return this.GetCurrentFacade();
    }

    /// <summary>Filters the facade based on a predicate.</summary>
    /// <param name="what">Predicate expression to apply to the facade items</param>
    /// <returns>This facade</returns>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="what" /> is null</exception>
    public virtual TCurrentFacade Where(Expression<Func<TDataItem, bool>> what)
    {
      FacadeHelper.AssertArgumentNotNull<Expression<Func<TDataItem, bool>>>(what, nameof (what));
      this.Items = this.Get().Where<TDataItem>(what);
      return this.GetCurrentFacade();
    }

    /// <summary>Filters the facade based on a predicate.</summary>
    /// <param name="expression">Predicate expression to apply to the facade items</param>
    /// <param name="values">The values.</param>
    /// <returns>This facade</returns>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="what" /> is null</exception>
    public virtual TCurrentFacade Where(string expression, params object[] values)
    {
      FacadeHelper.AssertArgumentNotNull<string>(expression, "what");
      this.Items = this.Get().Where<TDataItem>(expression, values);
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Sorts elements in the facade according to a key (in descending order)
    /// </summary>
    /// <typeparam name="TKey">Type of the key. This need not be specified and should be auto-detected by the compiler.</typeparam>
    /// <param name="keySelector">Expression that extracts a key from an element</param>
    /// <returns>This facade</returns>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="keySelector" /> is null</exception>
    public virtual TCurrentFacade OrderByDescending<TKey>(
      Expression<Func<TDataItem, TKey>> keySelector)
    {
      FacadeHelper.AssertArgumentNotNull<Expression<Func<TDataItem, TKey>>>(keySelector, nameof (keySelector));
      this.Items = (IQueryable<TDataItem>) this.Get().OrderByDescending<TDataItem, TKey>(keySelector);
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Sorts elements in the facade according to a key (in ascending order)
    /// </summary>
    /// <typeparam name="TKey">Type of the key. This need not be specified and should be auto-detected by the compiler.</typeparam>
    /// <param name="keySelector">Expression that extracts a key from an element</param>
    /// <returns>This facade</returns>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="keySelector" /> is null</exception>
    public virtual TCurrentFacade OrderBy<TKey>(Expression<Func<TDataItem, TKey>> keySelector)
    {
      FacadeHelper.AssertArgumentNotNull<Expression<Func<TDataItem, TKey>>>(keySelector, nameof (keySelector));
      this.Items = (IQueryable<TDataItem>) this.Get().OrderBy<TDataItem, TKey>(keySelector);
      return this.GetCurrentFacade();
    }

    /// <summary>Provides cached access to this facade's set of items</summary>
    /// <exception cref="T:System.ArgumentNullException">If setting a value that is null.</exception>
    protected virtual IQueryable<TDataItem> Items
    {
      get
      {
        if (this.items == null)
          this.items = this.LoadItems();
        return this.items;
      }
      set
      {
        FacadeHelper.AssertArgumentNotNull<IQueryable<TDataItem>>(value, nameof (Items));
        this.items = value;
      }
    }

    /// <summary>
    /// Called by <see cref="P:Telerik.Sitefinity.Fluent.BasePluralFacade`4.Items" /> if no items are loaded
    /// </summary>
    /// <returns>Queries the manager for all <typeparamref name="TDataItem" /> items</returns>
    protected abstract IQueryable<TDataItem> LoadItems();

    /// <summary>
    /// Casts this to <typeparamref name="TCurrentFacade" />
    /// </summary>
    /// <returns>This facade</returns>
    protected virtual TCurrentFacade GetCurrentFacade() => (TCurrentFacade) this;

    /// <summary>
    /// Create an instance of <typeparamref name="TInstance" /> by calling .ctor(AppSettings, this) -&gt; specifying settings and this as the parent facade
    /// </summary>
    /// <typeparam name="TInstance">Type of the facade to create</typeparam>
    /// <returns>Instance of the created facade</returns>
    protected virtual TInstance GetFacadeInstance<TInstance>() => (TInstance) Activator.CreateInstance(typeof (TInstance), (object) this.settings, (object) this.GetCurrentFacade());

    /// <summary>
    /// Create an instance of <typeparamref name="TInstance" /> by calling .ctor(AppSettings, this, item) -&gt; specifying settings,
    /// setting this as the parent facade and <paramref name="item" /> as the intial state
    /// </summary>
    /// <typeparam name="TInstance">Type of the facade to create</typeparam>
    /// <param name="item">Initial state of the facade that is going to be created</param>
    /// <returns>Instance of the created facade</returns>
    /// <remarks>This method can not create plural facades, as they accept IQueryable and not just TDataItem</remarks>
    protected virtual TInstance GetFacadeInstance<TInstance>(TDataItem item) => (TInstance) Activator.CreateInstance(typeof (TInstance), (object) this.settings, (object) this.GetCurrentFacade(), (object) item);

    /// <summary>
    /// Commit the changes and return the current facade for additional fluent calls
    /// </summary>
    public virtual TCurrentFacade SaveAndContinue()
    {
      this.SaveChanges();
      return this.GetCurrentFacade();
    }

    public virtual TCurrentFacade CancelAndContinue()
    {
      this.CancelChanges();
      return this.GetCurrentFacade();
    }
  }
}
