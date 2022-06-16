// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.MediaPublicFacade`4
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Manages the public state of media items (e.g. image, video, document)
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TMediaContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContentt" /></typeparam>
  /// <typeparam name="TTempFacade">Type of the temp facade.</typeparam>
  public abstract class MediaPublicFacade<TCurrentFacade, TParentFacade, TMediaContent, TTempFacade> : 
    BasePublicFacade<TCurrentFacade, TParentFacade, TMediaContent, TTempFacade>,
    IDownloadable<TCurrentFacade>
    where TCurrentFacade : BaseContentSingularFacadeWithLifeCycle<TCurrentFacade, TParentFacade, TMediaContent>
    where TParentFacade : BaseFacade, IHasPublicAndTempFacade<TCurrentFacade, TTempFacade, TParentFacade, TMediaContent>
    where TMediaContent : MediaContent
    where TTempFacade : BaseFacade
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.MediaPublicFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public MediaPublicFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.MediaPublicFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public MediaPublicFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.MediaPublicFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="itemID" /> is empty Guid</exception>
    public MediaPublicFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.MediaPublicFacade`4" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public MediaPublicFacade(AppSettings settings, TParentFacade parentFacade, TMediaContent item)
      : base(settings, parentFacade, item)
    {
    }

    /// <summary>
    /// Provides a streaming download of the content's binary data
    /// </summary>
    /// <param name="storage">Stream that can be used to retrieve the binary content of the content item</param>
    /// <returns>Currently chained facade</returns>
    public virtual TCurrentFacade DownloadContent(out Stream storage)
    {
      storage = UploadDownloadHelper.DownlaodContent(this.GetManager() as LibrariesManager, (MediaContent) this.Get());
      return this.GetCurrentFacade();
    }
  }
}
