// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Base facade for facades that manage <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />-based items
  /// </summary>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this content facade</typeparam>
  /// <typeparam name="TContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /></typeparam>
  public abstract class BaseContentFacade<TParentFacade, TContent> : 
    BaseFacadeWithParent<TParentFacade>
    where TParentFacade : BaseFacade
    where TContent : Content
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentFacade`2" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="!:ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="!:ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.BaseContentFacade`2" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="!:ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="!:ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseContentFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }
  }
}
