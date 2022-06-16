// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.VideoLibraryFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Fluent.Content
{
  [Obsolete]
  public class VideoLibraryFacade : 
    ContentFacade<VideoLibraryFacade, VideoLibrary, VideoLibraryFacade>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.VideoLibraryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public VideoLibraryFacade(AppSettings appSettings)
      : base(appSettings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.VideoLibraryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The video library id.</param>
    public VideoLibraryFacade(AppSettings appSettings, Guid itemId)
      : base(appSettings, itemId)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.VideoLibraryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public VideoLibraryFacade(AppSettings appSettings, VideoLibraryFacade parentFacade)
      : base(appSettings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.VideoLibraryFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="itemId">The video library id.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public VideoLibraryFacade(
      AppSettings appSettings,
      Guid itemId,
      VideoLibraryFacade parentFacade)
      : base(appSettings, itemId, parentFacade)
    {
    }

    /// <summary>
    /// Creates a facade of the videos in the library with a parent facade the current facade.
    /// </summary>
    /// <returns>
    /// The child facade of type <see cref="T:Telerik.Sitefinity.Fluent.Content.VideosFacade" />.
    /// </returns>
    public VideosFacade Videos()
    {
      this.EnsureExistence(true);
      return new VideosFacade(this.AppSettings, this.ContentItem.Videos(), this);
    }

    /// <summary>
    /// Creates a facade of a video in the library with a parent facade the current facade.
    /// </summary>
    /// <returns>
    /// The child facade of type <see cref="T:Telerik.Sitefinity.Fluent.Content.VideoFacade" />.
    /// </returns>
    public VideoFacade Video() => new VideoFacade(this.AppSettings, this);

    /// <summary>
    /// Creates a facade of the given video in the library with a parent facade the current facade.
    /// </summary>
    /// <param name="itemId">The id of the video.</param>
    /// <returns>
    /// The child facade of type <see cref="T:Telerik.Sitefinity.Fluent.Content.VideoFacade" />.
    /// </returns>
    public VideoFacade Video(Guid itemId) => new VideoFacade(this.AppSettings, itemId, this);
  }
}
