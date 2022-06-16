// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacadeWithoutLifeCycle`4
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Base class for facades that manage a set of content items that do not support content lifecycle
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TSingularFacade">Type of the facade that manages single items of type <typeparamref name="TContent" /></typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  public abstract class BaseContentPluralFacadeWithoutLifeCycle<TCurrentFacade, TSingularFacade, TParentFacade, TContent> : 
    BaseContentPluralFacade<TCurrentFacade, TSingularFacade, TParentFacade, TContent>
    where TCurrentFacade : BaseContentPluralFacadeWithoutLifeCycle<TCurrentFacade, TSingularFacade, TParentFacade, TContent>
    where TSingularFacade : BaseFacade
    where TParentFacade : BaseFacade
    where TContent : Content
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacadeWithoutLifeCycle`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="!:ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="!:ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentPluralFacadeWithoutLifeCycle(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacadeWithoutLifeCycle`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="!:ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="!:ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentPluralFacadeWithoutLifeCycle(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentPluralFacadeWithoutLifeCycle`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="items">Initial set of items.</param>
    /// <exception cref="!:ArgumentNullException">When <paramref name="settings" /> or <paramref name="items" /> is null</exception>
    /// <exception cref="!:ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentPluralFacadeWithoutLifeCycle(
      AppSettings settings,
      TParentFacade parentFacade,
      IQueryable<TContent> items)
      : base(settings, parentFacade, items)
    {
    }
  }
}
