// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BaseTempFacade`4
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Base class for facades that manage the temp state of a content item
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  /// <typeparam name="TPublicFacade">Type of the public facade.</typeparam>
  public abstract class BaseTempFacade<TCurrentFacade, TParentFacade, TContent, TPublicFacade> : 
    BaseContentSingularFacadeWithLifeCycle<TCurrentFacade, TParentFacade, TContent>
    where TCurrentFacade : BaseContentSingularFacadeWithLifeCycle<TCurrentFacade, TParentFacade, TContent>
    where TParentFacade : BaseFacade, IHasPublicAndTempFacade<TPublicFacade, TCurrentFacade, TParentFacade, TContent>
    where TContent : Content
    where TPublicFacade : BaseFacade
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseTempFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseTempFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseTempFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseTempFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseTempFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public BaseTempFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseTempFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseTempFacade(AppSettings settings, TParentFacade parentFacade, TContent item)
      : base(settings, parentFacade, item)
    {
    }

    /// <summary>
    /// Returns the instance of the content type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the content type.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// Wrapped item does not support content lifecycle or is not a temp
    /// </exception>
    public override TContent Get()
    {
      TContent content = base.Get();
      if ((object) content != null)
      {
        FacadeHelper.Assert((content.SupportsContentLifecycle ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) typeof (TContent), (object) content.Id));
        FacadeHelper.Assert((content.Status == ContentLifecycleStatus.Temp ? 1 : (content.Status == ContentLifecycleStatus.PartialTemp ? 1 : 0)) != 0, "{0} with id {1} is not temp".Arrange((object) typeof (TContent), (object) content.Id));
      }
      return content;
    }

    /// <summary>
    /// Sets an instance of the content type to currently loaded fluent API.
    /// </summary>
    /// <param name="item">Item to set as the new internal state of the facade</param>
    /// <returns>An instance of the current facade type.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="item" /> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// Wrapped item does not support content lifecycle or is not a temp
    /// </exception>
    public override TCurrentFacade Set(TContent item)
    {
      FacadeHelper.AssertArgumentNotNull<TContent>(item, nameof (item));
      FacadeHelper.Assert((item.SupportsContentLifecycle ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) typeof (TContent), (object) item.Id));
      FacadeHelper.Assert((item.Status == ContentLifecycleStatus.Temp ? 1 : (item.Status == ContentLifecycleStatus.PartialTemp ? 1 : 0)) != 0, "{0} with id {1} is not temp".Arrange((object) typeof (TContent), (object) item.Id));
      return base.Set(item);
    }

    /// <summary>
    /// Makes the changes in the temp permanent by copying it to the draft. The item is no longer locked and the temp is deleted.
    /// </summary>
    /// <returns>Parent (draft) facade</returns>
    public virtual TParentFacade CheckIn() => this.CheckIn(false);

    public virtual TParentFacade CheckIn(bool excludeVersioning)
    {
      TContent content1 = this.Get();
      TContent content2 = this.GetManager().CheckIn(content1);
      ++content2.Version;
      this.Done().Set(content2);
      if (!excludeVersioning)
        this.CreateVersion(this.Get(), ContentUIStatus.Draft);
      return this.Done();
    }

    /// <summary>
    /// Copies temp to the draft and live versions, thus unlocking and deleting the temp state itself.
    /// </summary>
    /// <returns></returns>
    public virtual TPublicFacade CheckInAndPublish() => this.CheckIn().Publish(false);

    /// <summary>
    /// Copies temp to the draft and live versions, thus unlocking and deleting the temp state itself.
    /// </summary>
    /// <returns></returns>
    public virtual TPublicFacade CheckInAndPublish(bool excludeVersioning) => this.CheckIn(excludeVersioning).Publish(excludeVersioning);

    /// <summary>
    /// Copies the current temp to the master, but leaves the temp undeleted, so the content item is kept locked.
    /// </summary>
    /// <returns></returns>
    public virtual TParentFacade CopyToMaster() => this.CopyToMaster(false);

    public virtual TParentFacade CopyToMaster(bool excludeVersioning)
    {
      TContent destination = this.Done().Get();
      this.GetManager().Copy(this.Get(), destination);
      ++destination.Version;
      if (!excludeVersioning)
        this.CreateVersion(this.Get(), ContentUIStatus.Draft);
      return this.Done();
    }
  }
}
