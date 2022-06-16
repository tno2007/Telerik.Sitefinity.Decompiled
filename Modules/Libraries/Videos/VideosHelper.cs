// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Videos.VideosHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Security.Permissions;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries.Videos.Thumbnails;

namespace Telerik.Sitefinity.Modules.Libraries.Videos
{
  /// <summary>
  /// Static class containing helper methods for images represented via IContent objects.
  /// </summary>
  public static class VideosHelper
  {
    /// <summary>Creates thumbnail for the specified video object</summary>
    /// <param name="video">The video.</param>
    /// <param name="videoFile">The video file.</param>
    /// <param name="imageFilePath">File location for the thumbnail image</param>
    /// <returns>Thumbnail Image</returns>
    [Obsolete("Use ObjectFactory.Resolve<IVideoThumbnailGenerator>().CreateThumbnail(video, videoFile, imageFilePath) instead.")]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Unrestricted)]
    public static System.Drawing.Image CreateThumbnail(
      this Video video,
      FileInfo videoFile,
      string imageFilePath)
    {
      return ObjectFactory.Resolve<IVideoThumbnailGenerator>().CreateThumbnail(video, videoFile, imageFilePath);
    }
  }
}
