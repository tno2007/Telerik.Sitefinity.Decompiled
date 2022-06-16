// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacade`4
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Base class for facades that manage a multitude of content items
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TSingularFacade">Type of the facade that manages single items of type <typeparamref name="TContent" /></typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  public abstract class BaseContentPluralFacade<TCurrentFacade, TSingularFacade, TParentFacade, TContent> : 
    BasePluralFacade<TCurrentFacade, TSingularFacade, TParentFacade, TContent>
    where TCurrentFacade : BaseContentPluralFacade<TCurrentFacade, TSingularFacade, TParentFacade, TContent>
    where TSingularFacade : BaseFacade
    where TParentFacade : BaseFacade
    where TContent : Content
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentPluralFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentPluralFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="items">Initial set of items.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="items" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentPluralFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      IQueryable<TContent> items)
      : this(settings, parentFacade)
    {
      FacadeHelper.Assert(items != null, "items can not be null");
      this.Items = items;
    }

    /// <summary>
    /// Executes an action over . Warning: this will be done in memory and will execute the underlying query!
    /// </summary>
    /// <param name="what">Action to execute over all items</param>
    /// <returns>This facade</returns>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="what" /> is null</exception>
    public override TCurrentFacade ForEach(Action<TContent> what)
    {
      FacadeHelper.AssertArgumentNotNull<Action<TContent>>(what, nameof (what));
      IQueryable<TContent> queryable = this.Get();
      IContentManager manager = this.GetManager();
      foreach (TContent content in (IEnumerable<TContent>) queryable)
      {
        what(content);
        CommonMethods.RecompileItemUrls((Content) content, (IManager) manager);
      }
      return this.GetCurrentFacade();
    }

    /// <summary>
    /// Called by <see cref="!:Items" /> if no items are loaded
    /// </summary>
    /// <returns>Queries the manager for all <typeparamref name="TContent" /> items</returns>
    protected override IQueryable<TContent> LoadItems() => this.GetManager().GetItems<TContent>();

    /// <summary>
    /// Commit the changes and return true on success. This method breaks the fluent API sequence.
    /// </summary>
    public override bool SaveChanges()
    {
      foreach (TContent content in (IEnumerable<TContent>) this.Items)
        CommonMethods.RecompileItemUrls((Content) content, (IManager) this.GetManager());
      return base.SaveChanges();
    }

    public virtual IContentManager GetManager() => (IContentManager) base.GetManager();
  }
}
