﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.MediaDraftFacade`5
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
  /// Manages the draft state of media items (e.g. image, video, document)
  /// </summary>
  /// <typeparam name="TCurrentFacade">Type of the facade that implements this facade</typeparam>
  /// <typeparam name="TParentFacade">Type of the facade that hosts this facade</typeparam>
  /// <typeparam name="TMediaContent">Type of content managed by this facade, inheriting from <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContentt" /></typeparam>
  /// <typeparam name="TTempFacade">Type of the temp facade.</typeparam>
  /// <typeparam name="TPublicFacade">Type of the public facade.</typeparam>
  public abstract class MediaDraftFacade<TCurrentFacade, TParentFacade, TMediaContent, TTempFacade, TPublicFacade> : 
    BaseDraftFacade<TCurrentFacade, TParentFacade, TMediaContent, TTempFacade, TPublicFacade>,
    IDownloadable<TCurrentFacade>
    where TCurrentFacade : BaseDraftFacade<TCurrentFacade, TParentFacade, TMediaContent, TTempFacade, TPublicFacade>
    where TParentFacade : BaseFacade
    where TMediaContent : MediaContent
    where TTempFacade : BaseFacade
    where TPublicFacade : BaseFacade
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.MediaDraftFacade`5" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public MediaDraftFacade(AppSettings settings)
      : base(settings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.MediaDraftFacade`5" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public MediaDraftFacade(AppSettings settings, TParentFacade parentFacade)
      : base(settings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.MediaDraftFacade`5" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="itemID">ID of the content item that is to be initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public MediaDraftFacade(AppSettings settings, TParentFacade parentFacade, Guid itemID)
      : base(settings, parentFacade, itemID)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.MediaDraftFacade`5" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="item">Content item to be the initial state of the facade.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="item" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public MediaDraftFacade(AppSettings settings, TParentFacade parentFacade, TMediaContent item)
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

    /// <summary>
    /// Returns the instance of the content type currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the content type.</returns>
    public override TMediaContent Get() => this.Item;
  }
}
