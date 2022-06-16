// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Videos.Thumbnails.VideoThumbnailGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using DirectShowLib;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Videos.Thumbnails
{
  /// <summary>Default sitefinity video thumbnail generation.</summary>
  public class VideoThumbnailGenerator : IVideoThumbnailGenerator
  {
    /// <summary>Creates thumbnail for the specified video object</summary>
    /// <param name="video">The video.</param>
    /// <param name="videoFile">The video file.</param>
    /// <param name="imageFilePath">File location for the thumbnail image</param>
    /// <returns>
    /// An instance of type <see cref="T:System.Drawing.Image" /> that contains the thumnbail image.
    /// </returns>
    public virtual System.Drawing.Image CreateThumbnail(
      Video video,
      FileInfo videoFile,
      string imageFilePath)
    {
      return videoFile.Extension == ".wmv" ? this.CreateWindowsMediaVideoThumbnail(video, videoFile, imageFilePath) : this.CreateHtml5VideoThumbnails(video, videoFile, imageFilePath);
    }

    /// <summary>
    /// Creates the thumbnail for windows media video format- .wmv.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <param name="videoFile">The video file.</param>
    /// <param name="imageFilePath">The image file path.</param>
    /// <returns>
    /// An instance of type <see cref="T:System.Drawing.Image" /> that contains the thumnbail image.
    /// </returns>
    /// <exception cref="T:System.ApplicationException">Cannot process video. The specified module could be found (8007007e).</exception>
    public virtual System.Drawing.Image CreateWindowsMediaVideoThumbnail(
      Video video,
      FileInfo videoFile,
      string imageFilePath)
    {
      if (!this.ValidateFilePath(imageFilePath))
        throw new ApplicationException("Cannot save file to specified folder. The folder needs to be explicitly configured in Settings->Advanced->Libraries->Videos->Allowed folders for storing temp files.");
      IGraphBuilder o = (IGraphBuilder) new FilterGraph();
      ISampleGrabber sampleGrabber = (ISampleGrabber) new SampleGrabber();
      o.AddFilter((IBaseFilter) sampleGrabber, "samplegrabber");
      sampleGrabber.SetMediaType(new AMMediaType()
      {
        majorType = MediaType.Video,
        subType = MediaSubType.RGB24,
        formatType = FormatType.VideoInfo
      });
      if (o.RenderFile(videoFile.FullName, (string) null) == -2147024770)
        throw new ApplicationException("Cannot process video. The specified module could be found (8007007e).");
      IMediaEventEx mediaEventEx = (IMediaEventEx) o;
      IMediaSeeking mediaSeeking = (IMediaSeeking) o;
      IMediaControl mediaControl = (IMediaControl) o;
      IBasicAudio basicAudio = (IBasicAudio) o;
      IVideoWindow videoWindow = (IVideoWindow) o;
      basicAudio.put_Volume(-10000);
      videoWindow.put_AutoShow(OABool.False);
      sampleGrabber.SetOneShot(true);
      sampleGrabber.SetBufferSamples(true);
      long pDuration = 0;
      mediaSeeking.GetDuration(out pDuration);
      DsLong pCurrent = new DsLong((long) ((double) (pDuration / 10000000L) * 0.100000001490116) * 10000000L);
      DsLong pStop = pCurrent;
      mediaSeeking.SetPositions(pCurrent, AMSeekingSeekingFlags.AbsolutePositioning, pStop, AMSeekingSeekingFlags.AbsolutePositioning);
      mediaControl.Run();
      mediaEventEx.WaitForCompletion(-1, out EventCode _);
      VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
      AMMediaType pmt = new AMMediaType();
      sampleGrabber.GetConnectedMediaType(pmt);
      VideoInfoHeader structure = (VideoInfoHeader) Marshal.PtrToStructure(pmt.formatPtr, typeof (VideoInfoHeader));
      int right = structure.SrcRect.right;
      int bottom = structure.SrcRect.bottom;
      using (Bitmap bitmap = new Bitmap(right, bottom, PixelFormat.Format24bppRgb))
      {
        uint num1 = 3;
        uint num2 = (uint) right * num1 % 4U;
        uint num3 = num1 * ((uint) right + num2);
        int num4 = bottom * (int) num3;
        BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, right, bottom), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
        int pBufferSize = num4;
        sampleGrabber.GetCurrentBuffer(ref pBufferSize, bitmapdata.Scan0);
        bitmap.UnlockBits(bitmapdata);
        bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
        bitmap.Save(imageFilePath, ImageFormat.Jpeg);
        Marshal.ReleaseComObject((object) o);
        Marshal.ReleaseComObject((object) sampleGrabber);
      }
      video.Width = right;
      video.Height = bottom;
      using (FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
        return System.Drawing.Image.FromStream((Stream) fileStream);
    }

    /// <summary>
    /// Creates the thumbnail for Html5 videos formats- .mp4, .webM, .ogv.
    /// </summary>
    /// <param name="video">The video.</param>
    /// <param name="videoFile">The video file.</param>
    /// <param name="imageFilePath">The image file path.</param>
    /// <returns>
    /// An instance of type <see cref="T:System.Drawing.Image" /> that contains the thumnbail image.
    /// </returns>
    public virtual System.Drawing.Image CreateHtml5VideoThumbnails(
      Video video,
      FileInfo videoFile,
      string imageFilePath)
    {
      return (System.Drawing.Image) null;
    }

    internal bool ValidateFilePath(string path)
    {
      string str1 = LibrariesManager.LibrariesTempFolder + (object) Path.DirectorySeparatorChar;
      string fullPath = Path.GetFullPath(path);
      if (fullPath.StartsWith(LibrariesManager.LibrariesTempFolder, StringComparison.OrdinalIgnoreCase))
        return true;
      string fileFoldersSettings = Config.Get<LibrariesConfig>().Videos.AllowedTempFileFoldersSettings;
      if (fileFoldersSettings.IsNullOrEmpty())
        return false;
      string str2 = fileFoldersSettings;
      char[] chArray = new char[1]{ ',' };
      foreach (string str3 in str2.Split(chArray))
      {
        string str4 = Path.GetFullPath(str3.Trim()) + (object) Path.DirectorySeparatorChar;
        if (fullPath.StartsWith(str4, StringComparison.OrdinalIgnoreCase))
          return true;
      }
      return false;
    }
  }
}
