// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithoutLifeCycle`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Base class for facades that manage single content items that do not support content lifecycle
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  public abstract class BaseContentSingularFacadeWithoutLifeCycle<TCurrentFacade, TParentFacade, TContent> : 
    BaseContentSingularFacade<TCurrentFacade, TParentFacade, TContent>
    where TCurrentFacade : BaseContentSingularFacade<TCurrentFacade, TParentFacade, TContent>
    where TParentFacade : BaseFacade
    where TContent : Content
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithoutLifeCycle`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentSingularFacadeWithoutLifeCycle(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithoutLifeCycle`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentSingularFacadeWithoutLifeCycle(
      AppSettings settings,
      TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithoutLifeCycle`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When itemId is emtpy Guid</exception>
    public BaseContentSingularFacadeWithoutLifeCycle(
      AppSettings settings,
      TParentFacade parentFacade,
      Guid itemID)
      : base(settings, parentFacade)
    {
      FacadeHelper.Assert<ArgumentOutOfRangeException>(itemID != Guid.Empty, "itemID can not be empty GUID");
      this.Item = (TContent) this.GetManager().GetItem(typeof (TContent), itemID);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithoutLifeCycle`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentSingularFacadeWithoutLifeCycle(
      AppSettings settings,
      TParentFacade parentFacade,
      TContent item)
      : base(settings, parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<TContent>(item, nameof (item));
      this.Item = item;
    }

    /// <summary>Create a new item with random ID</summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade CreateNew()
    {
      this.Item = (TContent) this.GetManager().CreateItem(typeof (TContent));
      return this.GetCurrentFacade();
    }

    /// <summary>Create a new item with specific ID</summary>
    /// <param name="itemId">ID of the item to create</param>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade CreateNew(Guid itemId)
    {
      FacadeHelper.Assert<ArgumentException>(itemId != Guid.Empty, "Item ID can not be empty GUID when creating a new item");
      this.Item = (TContent) this.GetManager().CreateItem(typeof (TContent), itemId);
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Provides a cached access to this facade's internal state
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">If while trying to load the item itemID turns out to be empty GUID</exception>
    /// <exception cref="T:System.ArgumentNullException">If setting and proposed value is null</exception>
    protected override TContent Item
    {
      get => base.Item;
      set
      {
        FacadeHelper.AssertArgumentNotNull<TContent>(value, nameof (Item));
        base.Item = value;
      }
    }
  }
}
