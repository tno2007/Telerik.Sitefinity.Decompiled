// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithLifeCycle`3
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Versioning;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Base class for facades that manage single content items that support content lifecycle
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  public abstract class BaseContentSingularFacadeWithLifeCycle<TCurrentFacade, TParentFacade, TContent> : 
    BaseContentSingularFacade<TCurrentFacade, TParentFacade, TContent>
    where TCurrentFacade : BaseContentSingularFacadeWithLifeCycle<TCurrentFacade, TParentFacade, TContent>
    where TParentFacade : BaseFacade
    where TContent : Content
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithLifeCycle`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentSingularFacadeWithLifeCycle(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithLifeCycle`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentSingularFacadeWithLifeCycle(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithLifeCycle`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public BaseContentSingularFacadeWithLifeCycle(
      AppSettings settings,
      TParentFacade parentFacade,
      Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentSingularFacadeWithLifeCycle`3" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentSingularFacadeWithLifeCycle(
      AppSettings settings,
      TParentFacade parentFacade,
      TContent item)
      : base(settings, parentFacade, item)
    {
    }

    /// <summary>
    /// Get a cached instance of this facade's content lifecycle manager
    /// </summary>
    /// <returns>Instance of this facade's content lifecycle manager</returns>
    /// <remarks>Hides BaseContentFacade.GetManager, so that returned manager is IContentLifecycleManager and not just IContentManager</remarks>
    public virtual IContentLifecycleManager<TContent> GetManager() => (IContentLifecycleManager<TContent>) base.GetManager();

    /// <summary>Creates the version.</summary>
    /// <param name="item">The item.</param>
    /// <param name="uiStatus">The UI status.</param>
    protected virtual void CreateVersion(TContent item, ContentUIStatus uiStatus)
    {
      if ((object) item == null)
        return;
      VersionManager.GetManager(this.settings.VersioningProviderName, this.settings.TransactionName).CreateVersion((object) item, item.OriginalContentId, uiStatus == ContentUIStatus.Published);
    }

    /// <summary>Copies the specified source.</summary>
    /// <param name="source">The source.</param>
    public virtual TCurrentFacade CloneFrom(TContent source)
    {
      this.GetManager().Copy(source, this.Get());
      CommonMethods.RecompileItemUrls((Content) this.Item, (IManager) this.GetManager());
      return this.GetCurrentFacade();
    }
  }
}
