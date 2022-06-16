// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BasePublicFacade`4
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
  /// Base class for facades that manage the public state of a content item
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  /// <typeparam name="TTempFacade">Type of the temp facade</typeparam>
  public abstract class BasePublicFacade<TCurrentFacade, TParentFacade, TContent, TTempFacade> : 
    BaseContentSingularFacadeWithLifeCycle<TCurrentFacade, TParentFacade, TContent>
    where TCurrentFacade : BaseContentSingularFacadeWithLifeCycle<TCurrentFacade, TParentFacade, TContent>
    where TParentFacade : BaseFacade, IHasPublicAndTempFacade<TCurrentFacade, TTempFacade, TParentFacade, TContent>
    where TContent : Content
    where TTempFacade : BaseFacade
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BasePublicFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BasePublicFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BasePublicFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BasePublicFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BasePublicFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public BasePublicFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BasePublicFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BasePublicFacade(AppSettings settings, TParentFacade parentFacade, TContent item)
      : base(settings, parentFacade, item)
    {
    }

    /// <summary>
    /// Returns the instance of the content type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the content type.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// Wrapped item does not support content lifecycle or is not a live
    /// </exception>
    public override TContent Get()
    {
      TContent content = base.Get();
      if ((object) content != null)
      {
        FacadeHelper.Assert((AllFacadesHelper.SupportsContentLifecycle((Content) content) ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) content.GetType(), (object) content.Id));
        FacadeHelper.Assert((content.Status == ContentLifecycleStatus.Live ? 1 : 0) != 0, "{0} with id {1} is not public".Arrange((object) typeof (TContent), (object) content.Id));
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
    /// Wrapped item does not support content lifecycle or is not a live
    /// </exception>
    public override TCurrentFacade Set(TContent item)
    {
      FacadeHelper.AssertArgumentNotNull<TContent>(item, nameof (item));
      FacadeHelper.Assert((item.SupportsContentLifecycle ? 1 : 0) != 0, "{0} with id {1} does not support content lifecycle".Arrange((object) typeof (TContent), (object) item.Id));
      FacadeHelper.Assert((item.Status == ContentLifecycleStatus.Live ? 1 : 0) != 0, "{0} with id {1} is not public".Arrange((object) typeof (TContent), (object) item.Id));
      return base.Set(item);
    }

    /// <summary>
    /// Copies the public state to the draft state of the content item. Warning: discards any changes in the draft
    /// </summary>
    /// <returns>Parent (draft) facade</returns>
    public virtual TParentFacade Edit()
    {
      TContent content1 = this.Get();
      TContent content2 = this.GetManager().Edit(content1);
      this.Done().Set(content2);
      return this.Done();
    }

    /// <summary>
    /// Copies the public state over the draft and then the temp states of the content item. Any changes in the draft and temp are discarded.
    /// </summary>
    /// <returns>Temp facade</returns>
    public virtual TTempFacade EditAndCheckout()
    {
      TContent content1 = this.Get();
      TContent content2 = this.GetManager().Edit(content1);
      this.Done().Set(content2);
      return this.Done().CheckOut();
    }

    /// <summary>
    /// Makes the public state of the content item inaccessible
    /// </summary>
    /// <returns>Parent (draft) facade</returns>
    public virtual TParentFacade Unpublish()
    {
      TContent content1 = this.Get();
      TContent content2 = this.GetManager().Unpublish(content1);
      if ((object) content2 != null)
        this.Done().Set(content2);
      this.settings.TrackDeletedItem((IDataItem) content2);
      return this.Done();
    }
  }
}
