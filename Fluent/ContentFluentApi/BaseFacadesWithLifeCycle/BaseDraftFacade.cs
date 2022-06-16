// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BaseDraftFacade`5
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Base class for facades that manage the draft lifecycle state of a content item
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  /// <typeparam name="TTempFacade">Type of the temp facade.</typeparam>
  /// <typeparam name="TPublicFacade">Type of the public facade.</typeparam>
  public abstract class BaseDraftFacade<TCurrentFacade, TParentFacade, TContent, TTempFacade, TPublicFacade> : 
    BaseContentSingularFacadeWithLifeCycle<TCurrentFacade, TParentFacade, TContent>,
    IHasPublicAndTempFacade<TPublicFacade, TTempFacade, TCurrentFacade, TContent>
    where TCurrentFacade : BaseDraftFacade<TCurrentFacade, TParentFacade, TContent, TTempFacade, TPublicFacade>
    where TParentFacade : BaseFacade
    where TContent : Content
    where TTempFacade : BaseFacade
    where TPublicFacade : BaseFacade
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseDraftFacade`5" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseDraftFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseDraftFacade`5" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseDraftFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseDraftFacade`5" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseDraftFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseDraftFacade`5" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseDraftFacade(AppSettings settings, TParentFacade parentFacade, TContent item)
      : base(settings, parentFacade, item)
    {
    }

    /// <summary>
    /// Returns the instance of the content type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the content type.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// Wrapped item does not support content lifecycle or is not a master
    /// </exception>
    public override TContent Get()
    {
      TContent content = base.Get();
      FacadeHelper.Assert((content.SupportsContentLifecycle ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) typeof (TContent), (object) content.Id));
      FacadeHelper.Assert((content.Status == ContentLifecycleStatus.Master ? 1 : 0) != 0, "{0} with id {1} is not a draft.".Arrange((object) typeof (TContent), (object) content.Id));
      return content;
    }

    /// <summary>
    /// Sets an instance of the content type to currently loaded fluent API.
    /// </summary>
    /// <param name="item">Item to set as the new internal state of the facade</param>
    /// <returns>An instance of the current facade type.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="item" /> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// Wrapped item does not support content lifecycle or is not a master
    /// </exception>
    public override TCurrentFacade Set(TContent item)
    {
      FacadeHelper.AssertArgumentNotNull<TContent>(item, nameof (item));
      FacadeHelper.Assert((AllFacadesHelper.SupportsContentLifecycle((Content) item) ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) item.GetType(), (object) item.Id));
      FacadeHelper.Assert((item.Status == ContentLifecycleStatus.Master ? 1 : 0) != 0, "{0} with id {1} is not a draft.".Arrange((object) typeof (TContent), (object) item.Id));
      return base.Set(item);
    }

    /// <summary>Create a new content item with random ID</summary>
    /// <returns>This (Draft) facade</returns>
    public virtual TCurrentFacade CreateNew()
    {
      this.Item = (TContent) this.GetManager().CreateItem(typeof (TContent));
      return this.GetCurrentFacade();
    }

    /// <summary>Create a new content item with specific ID</summary>
    /// <param name="itemId">ID of the content item to create</param>
    /// <returns>This (Draft) facade</returns>
    /// <exception cref="T:System.ArgumentException"><paramref name="itemID" /> is empty Guid</exception>
    public virtual TCurrentFacade CreateNew(Guid itemId)
    {
      FacadeHelper.Assert<ArgumentException>(itemId != Guid.Empty, "Item ID can not be empty GUID when creating a new item");
      this.Item = (TContent) this.GetManager().CreateItem(typeof (TContent), itemId);
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Unlocks the content item. If it is not locked, this has no effect.
    /// </summary>
    /// <returns>This (Draft) facade</returns>
    public virtual TCurrentFacade Unlock()
    {
      TContent cnt = this.Get();
      TContent temp = this.GetManager().GetTemp(cnt);
      if ((object) temp != null && temp.Owner != Guid.Empty)
        temp.Owner = Guid.Empty;
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Gets the live(published) item.
    /// Throws an exception if the item is not in database
    /// </summary>
    /// <returns></returns>
    public virtual TPublicFacade GetLive() => this.GetFacadeInstance<TPublicFacade>(this.GetManager().GetLive(this.Get()));

    /// <summary>
    /// Gets the temp item.
    /// Throws an exception if the item is not in database
    /// </summary>
    /// <returns></returns>
    public virtual TTempFacade GetTemp() => this.GetFacadeInstance<TTempFacade>(this.GetManager().GetTemp(this.Get()));

    /// <summary>
    /// Checks out the content item and return a temp item that is an identical copy if this draft. The item becomes locked.
    /// </summary>
    /// <returns>Temp facade</returns>
    public virtual TTempFacade CheckOut()
    {
      TContent content = this.Get();
      return this.GetFacadeInstance<TTempFacade>(this.GetManager().CheckOut(content));
    }

    /// <summary>
    /// Publish the content item by making the item visible on the frontend an identical copy of this draft
    /// </summary>
    /// <returns>Public facade</returns>
    public virtual TPublicFacade Publish() => this.Publish(false);

    /// <summary>Publishes the specified exclude pipe invocation.</summary>
    /// <param name="excludePipeInvocation">if set to <c>true</c> [exclude pipe invocation].</param>
    /// <returns></returns>
    public TPublicFacade Publish(bool excludeVersioning)
    {
      TContent content1 = this.Get();
      if (content1 is IApprovalWorkflowItem approvalWorkflowItem)
        approvalWorkflowItem.ApprovalWorkflowState = (Lstring) "Published";
      TContent content2 = this.GetManager().Publish(content1);
      if (!excludeVersioning)
        this.CreateVersion(content2, ContentUIStatus.Published);
      this.settings.TrackPublishedItem((IDataItem) content2);
      return this.GetFacadeInstance<TPublicFacade>(content2);
    }

    /// <summary>
    /// Create a public version for an item that will be visible for a period of time
    /// </summary>
    /// <param name="pubDate">Date from which the item will be visible on the public side</param>
    /// <param name="expDate">Date untill which the item will be visible on the public side. Use null if the item should not expire</param>
    /// <param name="excludePipeInvocation">if set to <c>true</c> [exclude pipe invocation].</param>
    /// <returns>Public facade</returns>
    /// <exception cref="T:System.ArgumentException">When <paramref name="pubDate" /> is either DateTime.Min or DateTime.Max</exception>
    public virtual TPublicFacade Schedule(
      DateTime pubDate,
      DateTime? expDate,
      bool excludeVersioning)
    {
      FacadeHelper.Assert<ArgumentException>(pubDate != DateTime.MinValue && pubDate != DateTime.MaxValue, "Publication date should not be DateTime.Max or DateTime.Min");
      TContent content1 = this.Get();
      TContent content2 = this.GetManager().Schedule(content1, pubDate, expDate);
      if (!excludeVersioning)
        this.CreateVersion(content2, ContentUIStatus.Published);
      this.settings.TrackPublishedItem((IDataItem) content2);
      return this.GetFacadeInstance<TPublicFacade>(content2);
    }

    /// <summary>
    /// Create a public version for an item that will be visible for a period of time
    /// </summary>
    /// <param name="pubDate">Date from which the item will be visible on the public side</param>
    /// <param name="expDate">Date untill which the item will be visible on the public side. Use null if the item should not expire</param>
    /// <returns>Public facade</returns>
    /// <exception cref="T:System.ArgumentException">When <paramref name="pubDate" /> is either DateTime.Min or DateTime.Max</exception>
    public virtual TPublicFacade Schedule(DateTime pubDate, DateTime? expDate) => this.Schedule(pubDate, expDate, false);

    /// <summary>
    /// Create a public version for an item that will be visible for a period of time
    /// </summary>
    /// <param name="excludePipeInvocation">if set to <c>true</c> [exclude pipe invocation].</param>
    /// <returns>Public facade</returns>
    /// <exception cref="T:System.ArgumentException">When <paramref name="pubDate" /> is either DateTime.Min or DateTime.Max</exception>
    public virtual TPublicFacade Schedule(bool excludeVersioning) => this.Schedule(this.Get().PublicationDate, this.Get().ExpirationDate, excludeVersioning);

    /// <summary>
    /// Create a public version for an item that will be visible for a period of time
    /// </summary>
    /// <returns>Public facade</returns>
    /// <exception cref="T:System.ArgumentException">When <paramref name="pubDate" /> is either DateTime.Min or DateTime.Max</exception>
    public virtual TPublicFacade Schedule() => this.Schedule(false);
  }
}
