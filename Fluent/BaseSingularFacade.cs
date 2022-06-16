// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.BaseSingularFacade`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>Base class for facades that manage a single item.</summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade.</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade.</typeparam>
  /// <typeparam name="TDataItem">Type of the data item managed by this facade, implementing the <see cref="T:Telerik.Sitefinity.Model.IDataItem" /> interface.</typeparam>
  public abstract class BaseSingularFacade<TCurrentFacade, TParentFacade, TDataItem> : 
    BaseFacadeWithParent<TParentFacade>,
    IBaseSingularFacade<TCurrentFacade, TDataItem>
    where TCurrentFacade : BaseSingularFacade<TCurrentFacade, TParentFacade, TDataItem>
    where TParentFacade : BaseFacade
    where TDataItem : class, IDataItem
  {
    /// <summary>Do not use outside Item property and constructors</summary>
    private TDataItem item;
    private Guid itemID;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BaseSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseSingularFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BaseSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="itemID">ID of the data item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public BaseSingularFacade(AppSettings settings, Guid itemID)
      : base(settings)
    {
      FacadeHelper.Assert<ArgumentOutOfRangeException>(itemID != Guid.Empty, "itemID can not be empty GUID");
      this.item = default (TDataItem);
      this.itemID = itemID;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BaseSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="item">Data item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="item" />'s transaction name is null</exception>
    public BaseSingularFacade(AppSettings settings, TDataItem item)
      : base(settings)
    {
      FacadeHelper.Assert<ArgumentOutOfRangeException>(this.itemID != Guid.Empty, "itemID can not be empty GUID");
      FacadeHelper.AssertArgumentNotNull<TDataItem>(item, nameof (item));
      this.item = item;
      this.itemID = item.Id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BaseSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseSingularFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BaseSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the data item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public BaseSingularFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<AppSettings>(settings, nameof (settings));
      FacadeHelper.Assert<ArgumentOutOfRangeException>(itemID != Guid.Empty, "itemID can not be empty GUID");
      this.item = default (TDataItem);
      this.itemID = itemID;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BaseSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Data item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="item" />'s transaction name is null</exception>
    public BaseSingularFacade(AppSettings settings, TParentFacade parentFacade, TDataItem item)
      : base(settings, parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<TDataItem>(item, nameof (item));
      this.item = item;
      this.itemID = item.Id;
    }

    /// <summary>
    /// Provides a cached access to this facade's internal state
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">If while trying to load the item itemID turns out to be empty GUID</exception>
    /// <exception cref="T:System.ArgumentNullException">If setting and proposed value is null</exception>
    protected virtual TDataItem Item
    {
      get
      {
        if ((object) this.item == null && this.itemID != Guid.Empty)
        {
          FacadeHelper.Assert<InvalidOperationException>(this.itemID != Guid.Empty, "Item can not be loaded when ID is set to empty Guid");
          this.item = (TDataItem) this.GetManager().GetItem(typeof (TDataItem), this.itemID);
        }
        return this.item;
      }
      set
      {
        FacadeHelper.AssertArgumentNotNull<TDataItem>(value, nameof (Item));
        this.item = value;
        this.itemID = this.item.Id;
      }
    }

    /// <summary>
    /// An instance of the data item type currently loaded by the fluent API.
    /// </summary>
    /// <param name="currentItem">The instance of the data item type currently loaded by the fluent API.</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Get(out TDataItem currentItem)
    {
      currentItem = this.Item;
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Returns the instance of the item type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the item type.</returns>
    public virtual TDataItem Get() => this.Item;

    /// <summary>
    /// Sets an instance of the item type to currently loaded fluent API.
    /// </summary>
    /// <param name="item">Item to set as the new internal state of the facade</param>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Set(TDataItem item)
    {
      this.Item = item;
      return this.GetCurrentFacade();
    }

    /// <summary>Performs an arbitrary action on the data item object.</summary>
    /// <param name="setAction">An action to be performed on the data item object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the data item object has not been initialized either through constructor, CreateNew() or Set() method.
    /// </exception>
    /// <returns>An instance of the current facade type.</returns>
    public virtual TCurrentFacade Do(Action<TDataItem> setAction)
    {
      FacadeHelper.Assert((object) this.Item != null, "Not initialize item");
      setAction(this.Item);
      return this.GetCurrentFacade();
    }

    /// <summary>Delete the currently loaded item</summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade Delete()
    {
      this.GetManager().DeleteItem((object) this.Item);
      return this.GetCurrentFacade();
    }

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
    /// Casts this to <typeparamref name="TCurrentFacade" />
    /// </summary>
    /// <returns>This facade</returns>
    protected virtual TCurrentFacade GetCurrentFacade() => (TCurrentFacade) this;

    /// <summary>
    /// Commit the changes and return true on success. This method breaks the fluent API sequence.
    /// </summary>
    public override bool SaveChanges() => base.SaveChanges();

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
