// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacade`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Base class for facades that manage single content items
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  public abstract class BaseContentSingularFacade<TCurrentFacade, TParentFacade, TContent> : 
    BaseSingularFacade<TCurrentFacade, TParentFacade, TContent>,
    IBaseSingularFacade<TCurrentFacade, TContent>
    where TCurrentFacade : BaseContentSingularFacade<TCurrentFacade, TParentFacade, TContent>
    where TParentFacade : BaseFacade
    where TContent : Content
  {
    /// <summary>Do not use outside Item property and constructors</summary>
    private TContent item;
    private Guid itemID;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentSingularFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public BaseContentSingularFacade(AppSettings settings, Guid itemID)
      : base(settings)
    {
      FacadeHelper.Assert<ArgumentOutOfRangeException>(itemID != Guid.Empty, "itemID can not be empty GUID");
      this.item = default (TContent);
      this.itemID = itemID;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="item" />'s transaction name is null</exception>
    public BaseContentSingularFacade(AppSettings settings, TContent item)
      : base(settings)
    {
      FacadeHelper.Assert<ArgumentOutOfRangeException>(this.itemID != Guid.Empty, "itemID can not be empty GUID");
      FacadeHelper.AssertArgumentNotNull<TContent>(item, nameof (item));
      this.item = item;
      this.itemID = item.Id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentSingularFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public BaseContentSingularFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<AppSettings>(settings, nameof (settings));
      FacadeHelper.Assert<ArgumentOutOfRangeException>(itemID != Guid.Empty, "itemID can not be empty GUID");
      this.item = default (TContent);
      this.itemID = itemID;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacade`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="item" />'s transaction name is null</exception>
    public BaseContentSingularFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      TContent item)
      : base(settings, parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<TContent>(item, nameof (item));
      this.item = item;
      this.itemID = item.Id;
    }

    /// <summary>
    /// Provides a cached access to this facade's internal state
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">If while trying to load the item itemID turns out to be empty GUID</exception>
    /// <exception cref="T:System.ArgumentNullException">If setting and proposed value is null</exception>
    protected override TContent Item
    {
      get
      {
        if ((object) this.item == null && this.itemID != Guid.Empty)
        {
          FacadeHelper.Assert<InvalidOperationException>(this.itemID != Guid.Empty, "Item can not be loaded when ID is set to empty Guid");
          this.item = (TContent) this.GetManager().GetItem(typeof (TContent), this.itemID);
        }
        return this.item;
      }
      set
      {
        FacadeHelper.AssertArgumentNotNull<TContent>(value, nameof (Item));
        this.item = value;
        this.itemID = this.item.Id;
      }
    }

    /// <summary>
    /// Returns the instance of the content type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the content type.</returns>
    public new virtual TContent Get() => this.Item;

    /// <summary>
    /// Sets an instance of the content type to currently loaded fluent API.
    /// </summary>
    /// <param name="item">Item to set as the new internal state of the facade</param>
    /// <returns>An instance of the current facade type.</returns>
    public override TCurrentFacade Set(TContent item)
    {
      this.Item = item;
      return this.GetCurrentFacade();
    }

    /// <summary>Performs an arbitrary action on the content object.</summary>
    /// <param name="setAction">An action to be performed on the content object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the content object has not been initialized either through constructor, CreateNew() or Set() method.
    /// </exception>
    /// <returns>An instance of the current facade type.</returns>
    public override TCurrentFacade Do(Action<TContent> setAction)
    {
      FacadeHelper.Assert((object) this.Item != null, "Not initialize item");
      setAction(this.Item);
      CommonMethods.RecompileItemUrls((Content) this.Item, (IManager) this.GetManager());
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Get a cached instance of this facade's content manager
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>Hides BaseFacade.GetManager, so that returned manager is IContentManager and not just IManager</remarks>
    public virtual IContentManager GetManager() => (IContentManager) base.GetManager();

    /// <summary>
    /// Commit the changes and return true on success. This method breaks the fluent API sequence.
    /// </summary>
    public override bool SaveChanges()
    {
      if (this.GetManager().Provider.GetDirtyItemStatus((object) this.Item) != SecurityConstants.TransactionActionType.Deleted && (this.itemID != Guid.Empty || (object) this.Item != null))
        CommonMethods.RecompileItemUrls((Content) this.Item, (IManager) this.GetManager());
      return base.SaveChanges();
    }

    /// <summary>Manage tags, categories and other taxonomies</summary>
    /// <returns>Facade that manages tags, categories and other taxonomies</returns>
    public virtual OrganizationFacade<TCurrentFacade> Organization() => new OrganizationFacade<TCurrentFacade>(this.settings, (TCurrentFacade) this, (IOrganizable) this.Get());
  }
}
