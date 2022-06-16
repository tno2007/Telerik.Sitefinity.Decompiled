// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacadeWithLifeCycle`4
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Base class for facades that manage a set of items and support content lifecycle
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TSingularFacade">Type of the facade that manages single items of type <typeparamref name="TContent" /></typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  public abstract class BaseContentPluralFacadeWithLifeCycle<TCurrentFacade, TSingularFacade, TParentFacade, TContent> : 
    BaseContentPluralFacade<TCurrentFacade, TSingularFacade, TParentFacade, TContent>
    where TCurrentFacade : BaseContentPluralFacadeWithLifeCycle<TCurrentFacade, TSingularFacade, TParentFacade, TContent>
    where TSingularFacade : BaseFacadeWithParent<TCurrentFacade>
    where TParentFacade : BaseFacade
    where TContent : Content
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacadeWithLifeCycle`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentPluralFacadeWithLifeCycle(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacadeWithLifeCycle`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentPluralFacadeWithLifeCycle(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacadeWithLifeCycle`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="items">Initial set of items.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="items" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentPluralFacadeWithLifeCycle(
      AppSettings settings,
      TParentFacade parentFacade,
      IQueryable<TContent> items)
      : base(settings, parentFacade, items)
    {
    }

    /// <summary>
    /// Get a cached instance of this facade's content lifecycle manager
    /// </summary>
    /// <returns>Instance of this facade's content lifecycle manager</returns>
    /// <remarks>Hides BaseContentFacade.GetManager, so that returned manager is IContentLifecycleManager and not just IContentManager</remarks>
    public virtual IContentLifecycleManager<TContent> GetManager() => (IContentLifecycleManager<TContent>) base.GetManager();

    /// <summary>Get only draft items</summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade Drafts()
    {
      this.Items = this.Get().Where<TContent>((Expression<Func<TContent, bool>>) (item => (int) item.Status == 0));
      return this.GetCurrentFacade();
    }

    /// <summary>Get only items that are visible on the front end</summary>
    /// <returns>This facade</returns>
    [Obsolete("Use BaseContentPluralFacadeWithLifeCycle.Published method instead.")]
    public virtual TCurrentFacade Publihed() => this.Published();

    /// <summary>Get only items that are visible on the front end</summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade Published()
    {
      this.Items = this.Get().Where<TContent>(PredefinedFilters.PublishedItemsFilter<TContent>());
      return this.GetCurrentFacade();
    }

    /// <summary>Get drafts that have published state</summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade PublishedDrafts()
    {
      string filterName = !typeof (TContent).IsILifecycle() ? nameof (PublishedDrafts) : "LifecyclePublishedDrafts";
      this.Items = NamedFiltersHandler.ApplyFilter<TContent>(this.Get(), filterName, (CultureInfo) null, (string) null);
      return this.GetCurrentFacade();
    }

    /// <summary>Get drafts that have no published state</summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade NotPublishedDrafts()
    {
      string filterName = !typeof (TContent).IsILifecycle() ? nameof (NotPublishedDrafts) : "LifecycleNotPublishedDrafts";
      this.Items = NamedFiltersHandler.ApplyFilter<TContent>(this.Get(), filterName, (CultureInfo) null, (string) null);
      return this.GetCurrentFacade();
    }

    /// <summary>Get published and scheduled states</summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade PublishedOrScheduled()
    {
      this.Items = this.Get().Where<TContent>((Expression<Func<TContent, bool>>) (item => (int) item.Status == 2));
      return this.GetCurrentFacade();
    }

    /// <summary>Get drafts that have scheduled states</summary>
    /// <returns>This facade</returns>
    public virtual TCurrentFacade ScheduledDrafts()
    {
      this.Items = this.Get().Where<TContent>(PredefinedFilters.ScheduledDrafts<TContent>());
      return this.GetCurrentFacade();
    }
  }
}
