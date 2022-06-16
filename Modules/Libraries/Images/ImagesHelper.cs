// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Images.ImagesHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Telerik.Sitefinity.Modules.Libraries.Web;

namespace Telerik.Sitefinity.Modules.Libraries.Images
{
  /// <summary>
  /// Static class containing helper methods for images represented via IContent objects.
  /// </summary>
  public static class ImagesHelper
  {
    internal const int exifOrientationID = 274;
    internal const string DefaultThumbnailExtension = ".png";
    internal static HashSet<string> supportedTmbExtensions = new HashSet<string>((IEnumerable<string>) new string[4]
    {
      ".jpeg",
      ".jpg",
      ".png",
      ".gif"
    });

    /// <summary>
    /// Generates a thumbnail image from the given image, with the specified
    /// size by keeping the image dimension proportions.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="size">The size.</param>
    /// <returns>the thumbnail image</returns>
    public static Image GenerateThumbnail(Image image, int size) => ImagesHelper.Resize(image, size, true, false);

    /// <summary>
    /// Generates a thumbnail image from the given image, with the specified
    /// size by keeping the image dimension proportions.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="size">The size.</param>
    /// <param name="decreaseOnly">if set to <c>true</c> [decrease only].</param>
    /// <param name="resizeSmallerSize">if set to <c>true</c> [resize smaller size].</param>
    /// <returns></returns>
    public static Image GenerateThumbnail(
      Image image,
      int size,
      bool decreaseOnly,
      bool resizeSmallerSize)
    {
      return ImagesHelper.Resize(image, size, decreaseOnly, resizeSmallerSize);
    }

    /// <summary>
    /// Resizes the specified image by keeping the image dimension proportions.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="size">The size.</param>
    /// <param name="decreaseOnly">if set to <c>true</c> [decrease only].</param>
    /// <param name="resizeSmallerSize">if set to <c>true</c> [resize smaller size].</param>
    /// <returns></returns>
    public static Image Resize(
      Image image,
      int size,
      bool decreaseOnly,
      bool resizeSmallerSize)
    {
      Image thumbnail = (Image) null;
      return ImagesHelper.TryResizeImage(image, size, decreaseOnly, resizeSmallerSize, out thumbnail) ? thumbnail : ImagesHelper.CloneImage(image) ?? image;
    }

    /// <summary>Tries the resize image.</summary>
    /// <param name="image">The image.</param>
    /// <param name="size">The size.</param>
    /// <param name="keepOrDecreaseSides">if set to <c>true</c> it won't allow bigger than the initial image.</param>
    /// <param name="resizeSmallerSize">if set to <c>true</c> [resize smaller size].</param>
    /// <param name="thumbnail">The thumbnail.</param>
    /// <returns></returns>
    public static bool TryResizeImage(
      Image image,
      int size,
      bool keepOrDecreaseSides,
      bool resizeSmallerSize,
      out Image thumbnail,
      ImageQuality quality = ImageQuality.Medium)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if (size <= 0)
        throw new ArgumentOutOfRangeException(nameof (size));
      thumbnail = (Image) null;
      bool flag = resizeSmallerSize && image.Width < image.Height || !resizeSmallerSize && image.Width > image.Height;
      if (keepOrDecreaseSides && (flag && size > image.Width || !flag && size > image.Height))
        return false;
      double num = (double) image.Width / (double) image.Height;
      int newWidth;
      int newHeight;
      if (flag)
      {
        newWidth = size;
        newHeight = (int) ((double) newWidth / num);
      }
      else
      {
        newHeight = size;
        newWidth = (int) ((double) newHeight * num);
      }
      if (newWidth <= 0)
        newWidth = 1;
      if (newHeight <= 0)
        newHeight = 1;
      return ImagesHelper.TryResizeImage(image, newWidth, newHeight, out thumbnail, quality);
    }

    /// <summary>
    /// Tries to resize the image by keeping the ratio between with and height and not overflowing the max limits specified.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="maxWidth">The maximum width of the result image.</param>
    /// <param name="maxHeight">The maximum height of the result image.</param>
    /// <param name="keepOrDecreaseSides">If set to true only transformation to a smaller or equal image will be performed.</param>
    /// <param name="resizedImage">The resized image.</param>
    /// <returns></returns>
    internal static bool TryResizeImage(
      Image image,
      int maxWidth,
      int maxHeight,
      bool keepOrDecreaseSides,
      out Image resizedImage,
      ImageQuality quality = ImageQuality.Medium)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if (maxWidth < 0)
        throw new ArgumentOutOfRangeException(nameof (maxWidth));
      if (maxHeight < 0)
        throw new ArgumentOutOfRangeException(nameof (maxHeight));
      if (maxWidth == 0 && maxHeight == 0)
        throw new ArgumentOutOfRangeException("maxHeight and maxWidth can't be both zero.");
      bool? nullable = new bool?();
      if (maxWidth == 0)
      {
        maxWidth = image.Width;
        nullable = new bool?(false);
      }
      if (maxHeight == 0)
      {
        maxHeight = image.Height;
        nullable = new bool?(true);
      }
      double num1 = (double) image.Height / (double) maxHeight;
      double num2 = (double) image.Width / (double) maxWidth;
      if (!nullable.HasValue)
        nullable = new bool?(num2 > num1);
      resizedImage = (Image) null;
      if (keepOrDecreaseSides && (nullable.Value && num2 < 1.0 || !nullable.Value && num1 < 1.0))
        return false;
      double num3 = (double) image.Width / (double) image.Height;
      int newWidth;
      int newHeight;
      if (nullable.Value)
      {
        newWidth = maxWidth;
        newHeight = (int) ((double) newWidth / num3);
      }
      else
      {
        newHeight = maxHeight;
        newWidth = (int) ((double) newHeight * num3);
      }
      if (newWidth <= 0)
        newWidth = 1;
      if (newHeight <= 0)
        newHeight = 1;
      return ImagesHelper.TryResizeImage(image, newWidth, newHeight, out resizedImage, quality);
    }

    /// <summary>
    /// Tries to resize the image by explicitly setting its width and height. It does not necessarily preserve the ratio.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="newWidth">The new width.</param>
    /// <param name="newHeight">The new height.</param>
    /// <param name="resultImage">The result image.</param>
    /// <param name="quality">The desired output image quality.</param>
    /// <returns></returns>
    public static bool TryResizeImage(
      Image image,
      int newWidth,
      int newHeight,
      out Image resultImage,
      ImageQuality quality = ImageQuality.Medium,
      int x = 0,
      int y = 0)
    {
      if (newWidth <= 0)
        throw new ArgumentOutOfRangeException(nameof (newWidth));
      if (newHeight <= 0)
        throw new ArgumentOutOfRangeException(nameof (newHeight));
      resultImage = (Image) null;
      CompositingQuality compositingQuality;
      SmoothingMode smoothingMode;
      InterpolationMode interpolationMode;
      PixelOffsetMode pxOffsetMode;
      ImagesHelper.GetImageParameters(quality, out compositingQuality, out smoothingMode, out interpolationMode, out pxOffsetMode);
      PixelFormat format = image.PixelFormat;
      if (format.ToString().Contains("Indexed"))
        format = PixelFormat.Format32bppArgb;
      try
      {
        Bitmap bitmap = new Bitmap(newWidth, newHeight, format);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        bitmap.MakeTransparent(Color.Transparent);
        resultImage = (Image) bitmap;
        using (Graphics graphics = Graphics.FromImage(resultImage))
        {
          graphics.Clear(Color.Transparent);
          graphics.CompositingQuality = compositingQuality;
          graphics.SmoothingMode = smoothingMode;
          graphics.InterpolationMode = interpolationMode;
          graphics.PixelOffsetMode = pxOffsetMode;
          Rectangle destRect = new Rectangle(0, 0, newWidth, newHeight);
          using (ImageAttributes imageAttr = new ImageAttributes())
          {
            imageAttr.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, x, y, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
          }
        }
      }
      catch
      {
        try
        {
          resultImage = image.GetThumbnailImage(newWidth, newHeight, (Image.GetThumbnailImageAbort) (() => true), IntPtr.Zero);
        }
        catch
        {
        }
      }
      return resultImage != null;
    }

    private static void GetImageParameters(
      ImageQuality quality,
      out CompositingQuality compositingQuality,
      out SmoothingMode smoothingMode,
      out InterpolationMode interpolationMode,
      out PixelOffsetMode pxOffsetMode)
    {
      switch (quality)
      {
        case ImageQuality.High:
          compositingQuality = CompositingQuality.HighQuality;
          smoothingMode = SmoothingMode.HighQuality;
          interpolationMode = InterpolationMode.HighQualityBicubic;
          pxOffsetMode = PixelOffsetMode.HighQuality;
          break;
        case ImageQuality.Low:
          compositingQuality = CompositingQuality.HighSpeed;
          smoothingMode = SmoothingMode.HighSpeed;
          interpolationMode = InterpolationMode.Low;
          pxOffsetMode = PixelOffsetMode.HighSpeed;
          break;
        default:
          compositingQuality = CompositingQuality.AssumeLinear;
          smoothingMode = SmoothingMode.AntiAlias;
          interpolationMode = InterpolationMode.Bicubic;
          pxOffsetMode = PixelOffsetMode.None;
          break;
      }
    }

    /// <summary>
    /// Tries to crop an image using a specified rectangle using the x,y,width and height arguments
    /// </summary>
    /// <param name="image">the source image </param>
    /// <param name="resultImage">the cropped resulting image</param>
    /// <param name="x">specifies the upper left corner x coordinate of the cropping rectangle</param>
    /// <param name="y">specifies the upper left corner y coordinate of the cropping rectangle</param>
    /// <param name="width">specifies the width of the cropping rectangle</param>
    /// <param name="height">specifies the height of the cropping rectangle</param>
    /// <param name="quality">specifies the quality of the resulting image</param>
    /// <returns></returns>
    public static bool TryCrop(
      Image image,
      out Image resultImage,
      int x,
      int y,
      int width,
      int height,
      ImageQuality quality = ImageQuality.Medium)
    {
      resultImage = (Image) null;
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if (width <= 0)
        throw new ArgumentOutOfRangeException(nameof (width));
      if (height <= 0)
        throw new ArgumentOutOfRangeException(nameof (height));
      if (x < 0)
        throw new ArgumentOutOfRangeException(nameof (x));
      if (y < 0)
        throw new ArgumentOutOfRangeException(nameof (y));
      CompositingQuality compositingQuality;
      SmoothingMode smoothingMode;
      InterpolationMode interpolationMode;
      PixelOffsetMode pxOffsetMode;
      ImagesHelper.GetImageParameters(quality, out compositingQuality, out smoothingMode, out interpolationMode, out pxOffsetMode);
      PixelFormat format = image.PixelFormat;
      if (format.ToString().Contains("Indexed"))
        format = PixelFormat.Format24bppRgb;
      try
      {
        Bitmap bitmap = new Bitmap(width, height, format);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.CompositingQuality = compositingQuality;
          graphics.SmoothingMode = smoothingMode;
          graphics.InterpolationMode = interpolationMode;
          graphics.PixelOffsetMode = pxOffsetMode;
          Rectangle srcRect = new Rectangle(x, y, width, height);
          graphics.DrawImage(image, new Rectangle(0, 0, width, height), srcRect, GraphicsUnit.Pixel);
          resultImage = (Image) bitmap;
          return true;
        }
      }
      catch
      {
      }
      return false;
    }

    /// <summary>
    /// Tries to resize the side with smaller change and to cut the other side from both sides (to preserve the center of the image).
    /// </summary>
    /// <param name="image">The source image.</param>
    /// <param name="width">The desired image width.</param>
    /// <param name="height">The desired image height.</param>
    /// <param name="keepOrDecreaseSides">If set to true only smaller side images will be returned. Otherwise a scale up operation may occur to return a bigger image.</param>
    /// <param name="quality">The image quality.</param>
    /// <param name="resultImage">The cropped image.</param>
    /// <returns>True if the crop operation was successful otherwise false.</returns>
    internal static bool TryCrop(
      Image image,
      int width,
      int height,
      out Image resultImage,
      bool keepOrDecreaseSides = false,
      ImageQuality quality = ImageQuality.Medium)
    {
      resultImage = (Image) null;
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      if (width <= 0)
        throw new ArgumentOutOfRangeException(nameof (width));
      if (height <= 0)
        throw new ArgumentOutOfRangeException(nameof (height));
      double num1 = (double) image.Width / (double) image.Height;
      double num2 = (double) width / (double) height;
      bool flag = num1 < num2;
      if (keepOrDecreaseSides && (flag && width > image.Width || !flag && height > image.Height))
        return false;
      int newWidth;
      int newHeight;
      if (flag)
      {
        newWidth = width;
        newHeight = (int) ((double) newWidth / num1);
      }
      else
      {
        newHeight = height;
        newWidth = (int) ((double) newHeight * num1);
      }
      if (newWidth <= 0)
        newWidth = 1;
      if (newHeight <= 0)
        newHeight = 1;
      if (!ImagesHelper.TryResizeImage(image, newWidth, newHeight, out resultImage, quality))
        return false;
      int x = 0;
      int y = 0;
      if (flag)
        y = (newHeight - height) / 2;
      else
        x = (newWidth - width) / 2;
      CompositingQuality compositingQuality;
      SmoothingMode smoothingMode;
      InterpolationMode interpolationMode;
      PixelOffsetMode pxOffsetMode;
      ImagesHelper.GetImageParameters(quality, out compositingQuality, out smoothingMode, out interpolationMode, out pxOffsetMode);
      PixelFormat format = resultImage.PixelFormat;
      if (format.ToString().Contains("Indexed"))
        format = PixelFormat.Format24bppRgb;
      try
      {
        Bitmap bitmap = new Bitmap(width, height, format);
        bitmap.SetResolution(resultImage.HorizontalResolution, resultImage.VerticalResolution);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.CompositingQuality = compositingQuality;
          graphics.SmoothingMode = smoothingMode;
          graphics.InterpolationMode = interpolationMode;
          graphics.PixelOffsetMode = pxOffsetMode;
          Rectangle srcRect = new Rectangle(x, y, width, height);
          graphics.DrawImage(resultImage, new Rectangle(0, 0, width, height), srcRect, GraphicsUnit.Pixel);
          resultImage = (Image) bitmap;
          return true;
        }
      }
      catch
      {
      }
      return false;
    }

    /// <summary>Saves given image to given stream.</summary>
    /// <param name="image">The image to be saved.</param>
    /// <param name="stream">The stream which would contain the image information.</param>
    /// <param name="mimeType">MIME type of the image.</param>
    /// <param name="isThumbnail">Specifies if a thumbnail image is being saved</param>
    /// <returns>The mime/type of the saved image</returns>
    public static string SaveImageToStream(
      Image image,
      Stream stream,
      string mimeType,
      bool isThumbnail)
    {
      string lower = mimeType.Substring(mimeType.IndexOf('/') + 1).ToLower();
      ImageFormat jpeg = ImageFormat.Jpeg;
      ImageFormat format;
      if (!(lower == "bmp"))
      {
        if (!(lower == "gif"))
        {
          if (!(lower == "png"))
          {
            if (lower == "jpeg")
              format = ImageFormat.Jpeg;
            else if (isThumbnail)
            {
              format = ImageFormat.Png;
              mimeType = MimeMapping.GetMimeMapping(".png");
            }
            else
            {
              format = ImageFormat.Jpeg;
              mimeType = MimeMapping.GetMimeMapping(".jpeg");
            }
          }
          else
            format = ImageFormat.Png;
        }
        else
          format = ImageFormat.Gif;
      }
      else if (isThumbnail)
      {
        format = ImageFormat.Png;
        mimeType = MimeMapping.GetMimeMapping(".png");
      }
      else
        format = ImageFormat.Bmp;
      ImageCodecInfo encoder = ((IEnumerable<ImageCodecInfo>) ImageCodecInfo.GetImageEncoders()).FirstOrDefault<ImageCodecInfo>((Func<ImageCodecInfo, bool>) (t => t.MimeType == mimeType));
      if (encoder != null)
      {
        EncoderParameters encoderParams = new EncoderParameters(1);
        encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 95L);
        image.Save(stream, encoder, encoderParams);
      }
      else
        image.Save(stream, format);
      return mimeType;
    }

    /// <summary>Saves given image to given stream.</summary>
    /// <param name="image">The image to be saved.</param>
    /// <param name="stream">The stream which would contain the image information.</param>
    /// <param name="mimeType">MIME type of the image.</param>
    /// <returns>The mime/type of the saved image</returns>
    public static string SaveImageToStream(Image image, Stream stream, string mimeType) => ImagesHelper.SaveImageToStream(image, stream, mimeType, false);

    /// <summary>
    /// Generates an instance of type <see cref="T:System.Drawing.Image" /> from array of binary information.
    /// </summary>
    /// <param name="byteArray">The byte array.</param>
    /// <returns></returns>
    public static Image ByteArrayToImage(byte[] byteArray) => Image.FromStream((Stream) new MemoryStream(byteArray), true, true);

    /// <summary>Creates a clone of an image - draws it in a new one</summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public static Image CloneImage(Image image)
    {
      PixelFormat format = image.PixelFormat;
      if (format.ToString().Contains("Indexed"))
        format = PixelFormat.Format24bppRgb;
      try
      {
        Bitmap bitmap = new Bitmap(image.Width, image.Height, format);
        bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.SmoothingMode = SmoothingMode.HighQuality;
          graphics.InterpolationMode = InterpolationMode.Bicubic;
          graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
          graphics.DrawImage(image, 0, 0, image.Width, image.Height);
        }
        return (Image) bitmap;
      }
      catch (Exception ex)
      {
        return image.GetThumbnailImage(image.Width, image.Height, (Image.GetThumbnailImageAbort) (() => true), IntPtr.Zero);
      }
    }

    /// <summary>
    /// Rotates image based on exif orientation meta field if exists
    /// </summary>
    /// <param name="img">The given image</param>
    /// <returns>true if image is rotated, false otherwise</returns>
    internal static bool ExifRotate(Image img)
    {
      if (!((IEnumerable<int>) img.PropertyIdList).Contains<int>(274))
        return false;
      int uint16 = (int) BitConverter.ToUInt16(img.GetPropertyItem(274).Value, 0);
      RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone;
      if (uint16 == 3 || uint16 == 4)
        rotateFlipType = RotateFlipType.Rotate180FlipNone;
      else if (uint16 == 5 || uint16 == 6)
        rotateFlipType = RotateFlipType.Rotate90FlipNone;
      else if (uint16 == 7 || uint16 == 8)
        rotateFlipType = RotateFlipType.Rotate270FlipNone;
      if (uint16 == 2 || uint16 == 4 || uint16 == 5 || uint16 == 7)
        rotateFlipType |= RotateFlipType.RotateNoneFlipX;
      if (rotateFlipType == RotateFlipType.RotateNoneFlipNone)
        return false;
      img.RotateFlip(rotateFlipType);
      img.RemovePropertyItem(274);
      return true;
    }
  }
}
