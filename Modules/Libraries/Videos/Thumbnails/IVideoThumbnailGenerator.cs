// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Videos.Thumbnails.IVideoThumbnailGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using Telerik.Sitefinity.Libraries.Model;

namespace Telerik.Sitefinity.Modules.Libraries.Videos.Thumbnails
{
  /// <summary>Defines interface for videos thumbnail generation.</summary>
  public interface IVideoThumbnailGenerator
  {
    /// <summary>Creates thumbnail for the specified video object.</summary>
    /// <param name="video">The video.</param>
    /// <param name="videoFile">The video file.</param>
    /// <param name="imageFilePath">File location for the thumbnail image</param>
    /// <returns>
    /// An instance of type <see cref="T:System.Drawing.Image" /> that contains the thumnbail image.
    /// </returns>
    System.Drawing.Image CreateThumbnail(
      Video video,
      FileInfo videoFile,
      string imageFilePath);

    /// <summary>
    /// Creates the thumbnail for windows media video format- .wmv.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <param name="videoFile">The video file.</param>
    /// <param name="imageFilePath">The image file path.</param>
    /// <returns>
    /// An instance of type <see cref="T:System.Drawing.Image" /> that contains the thumnbail image.
    /// </returns>
    System.Drawing.Image CreateWindowsMediaVideoThumbnail(
      Video video,
      FileInfo videoFile,
      string imageFilePath);

    /// <summary>
    /// Creates the thumbnail for Html5 videos formats- .mp4, .webM, .ogv.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <param name="videoFile">The video file.</param>
    /// <param name="imageFilePath">The image file path.</param>
    /// <returns>
    /// An instance of type <see cref="T:System.Drawing.Image" /> that contains the thumnbail image.
    /// </returns>
    System.Drawing.Image CreateHtml5VideoThumbnails(
      Video video,
      FileInfo videoFile,
      string imageFilePath);
  }
}
