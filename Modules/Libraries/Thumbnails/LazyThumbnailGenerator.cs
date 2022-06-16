// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Thumbnails.LazyThumbnailGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Web;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Modules.Libraries.Thumbnails
{
  /// <summary>Singleton used for lazy thumbnail generation.</summary>
  public class LazyThumbnailGenerator
  {
    private static Semaphore semaphore;
    private static readonly ConcurrentProperty<LazyThumbnailGenerator> instance = new ConcurrentProperty<LazyThumbnailGenerator>((Func<LazyThumbnailGenerator>) (() => new LazyThumbnailGenerator()));

    private LazyThumbnailGenerator()
    {
      int num = Config.Get<LibrariesConfig>().Images.DynamicResizingThreadsCount == 0 ? Environment.ProcessorCount : Config.Get<LibrariesConfig>().Images.DynamicResizingThreadsCount;
      LazyThumbnailGenerator.semaphore = new Semaphore(num, num);
    }

    /// <summary>Returns an instance of LazyThumbnailGenerator</summary>
    public static LazyThumbnailGenerator Instance => LazyThumbnailGenerator.instance.Value;

    private IImageProcessor ImageProcessor => ObjectFactory.Resolve<IImageProcessor>();

    /// <summary>
    /// Creates a thumbnail by finding the thumbnail profile from already registered profiles or by specifying image method and method parameters
    /// </summary>
    /// <param name="liveMediaContent"></param>
    /// <param name="thumbnailName"></param>
    /// <param name="imageMethod"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public Thumbnail CreateThumbnail(
      MediaContent liveMediaContent,
      string thumbnailName,
      string imageMethod = null,
      NameValueCollection parameters = null)
    {
      if (!Config.Get<LibrariesConfig>().Images.AllowDynamicResizing)
        return (Thumbnail) null;
      if (liveMediaContent.IsVectorGraphics())
        return (Thumbnail) null;
      try
      {
        LazyThumbnailGenerator.semaphore.WaitOne();
        return !thumbnailName.IsNullOrEmpty() ? this.CreateThumbnailByThumbnailName(liveMediaContent, thumbnailName) : this.CreateThumbnailByImageMethod(liveMediaContent, imageMethod, parameters);
      }
      finally
      {
        LazyThumbnailGenerator.semaphore.Release();
      }
    }

    /// <summary>
    /// Creates a thumbnail by specified thumbnailName. It finds the thumbnail profile for this thumbnail and creates an images
    /// </summary>
    /// <param name="liveMediaContent"></param>
    /// <param name="selectedThumbnailName">the name of the thumbnail</param>
    /// <returns></returns>
    private Thumbnail CreateThumbnailByThumbnailName(
      MediaContent liveMediaContent,
      string selectedThumbnailName)
    {
      ThumbnailProfileConfigElement profileConfigElement = Config.Get<LibrariesConfig>().GetThumbnailProfiles(liveMediaContent.Parent.GetType()).Values.Where<ThumbnailProfileConfigElement>((Func<ThumbnailProfileConfigElement, bool>) (tp => tp.Name == selectedThumbnailName)).SingleOrDefault<ThumbnailProfileConfigElement>();
      return profileConfigElement != null ? this.SaveOrGenerateThumbnail(liveMediaContent, selectedThumbnailName, profileConfigElement.Method, profileConfigElement.MethodArgument) : (Thumbnail) null;
    }

    /// <summary>
    /// Creates a thumbnail by specified image method.
    /// The parameters collection should contain all parameters that this image method needs in order to process the image.
    /// </summary>
    /// <param name="liveMediaContent"></param>
    /// <param name="imageMethod">the name of the image method to be used</param>
    /// <param name="parameters"> contain all parameters that this image method needs in order to process the image</param>
    /// <returns></returns>
    private Thumbnail CreateThumbnailByImageMethod(
      MediaContent liveMediaContent,
      string imageMethod,
      NameValueCollection parameters)
    {
      ImageProcessingMethod method = (ImageProcessingMethod) null;
      if (imageMethod.IsNullOrEmpty() || !this.ImageProcessor.Methods.TryGetValue(imageMethod, out method))
        return (Thumbnail) null;
      string thumbnailNameFromParams = LazyThumbnailGenerator.GetThumbnailNameFromParams(parameters, method);
      object methodArgument = method.CreateArgumentInstance(parameters);
      if (Config.Get<LibrariesConfig>().Images.EnableImageUrlSignature)
      {
        string parameter = parameters["signature"];
        if (parameter.IsNullOrEmpty())
          return (Thumbnail) null;
        Dictionary<string, string> dictionary = ((IEnumerable<PropertyInfo>) methodArgument.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)).ToDictionary<PropertyInfo, string, string>((Func<PropertyInfo, string>) (prop => prop.Name), (Func<PropertyInfo, string>) (prop => prop.GetValue(methodArgument, (object[]) null).ToString()));
        dictionary.Add("method", imageMethod);
        if (MediaContentExtensions.GenerateUrlParamSignature(dictionary, liveMediaContent) != parameter)
        {
          if (!Config.Get<LibrariesConfig>().Images.EnableMultipleHashAlgorithmsSupport)
            return (Thumbnail) null;
          bool flag = false;
          foreach (object obj in Enum.GetValues(typeof (ImageUrlSignatureHashAlgorithm)))
          {
            if ((ImageUrlSignatureHashAlgorithm) obj != Config.Get<LibrariesConfig>().Images.ImageUrlSignatureHashAlgorithm && MediaContentExtensions.GenerateUrlParamSignature(dictionary, liveMediaContent, new ImageUrlSignatureHashAlgorithm?((ImageUrlSignatureHashAlgorithm) obj)) == parameter)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            return (Thumbnail) null;
        }
      }
      return this.SaveOrGenerateThumbnail(liveMediaContent, thumbnailNameFromParams, imageMethod, methodArgument);
    }

    /// <summary>
    /// Creates a new thumbnail name based no the passed parameters and the name of the image method.
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    private static string GetThumbnailNameFromParams(
      NameValueCollection parameters,
      ImageProcessingMethod method)
    {
      string thumbnailNameFromParams = method.MethodName;
      foreach (string parameter in (NameObjectCollectionBase) parameters)
      {
        if (parameter.ToLower().Contains("width") || parameter.ToLower().Contains("height"))
          thumbnailNameFromParams = thumbnailNameFromParams + "-" + parameters[parameter];
      }
      if (thumbnailNameFromParams.Length > 9)
        thumbnailNameFromParams = thumbnailNameFromParams.Substring(thumbnailNameFromParams.Length - 10, 10);
      return thumbnailNameFromParams;
    }

    /// <summary>
    /// Method used for storing or just generating thumbnail on the fly. The mode is configured via LibrariesConfig.Images.StoreDynamicResizedImagesAsThumbnails
    /// </summary>
    /// <param name="liveMediaContent"></param>
    /// <param name="newThumbnailName"></param>
    /// <param name="method"></param>
    /// <param name="methodArgument"></param>
    /// <returns></returns>
    private Thumbnail SaveOrGenerateThumbnail(
      MediaContent liveMediaContent,
      string newThumbnailName,
      string method,
      object methodArgument)
    {
      if (Config.Get<LibrariesConfig>().Images.StoreDynamicResizedImagesAsThumbnails)
      {
        Thumbnail thumbnail = liveMediaContent.GetThumbnails().FirstOrDefault<Thumbnail>((Func<Thumbnail, bool>) (t => t.Name == newThumbnailName));
        if (thumbnail != null)
          return thumbnail;
      }
      LibrariesManager manager = LibrariesManager.GetManager(liveMediaContent.GetProviderName());
      using (new ElevatedModeRegion((IManager) manager))
      {
        string mimeType = liveMediaContent is Video ? MimeMapping.GetMimeMapping(".jpg") : liveMediaContent.MimeType;
        IBlobContent blobContent = (IBlobContent) liveMediaContent;
        if (liveMediaContent is Video)
        {
          Thumbnail thumbnail = liveMediaContent.GetThumbnails().FirstOrDefault<Thumbnail>();
          if (thumbnail == null)
            return (Thumbnail) null;
          blobContent = (IBlobContent) thumbnail;
        }
        using (Stream stream = manager.Download(blobContent))
        {
          using (MemoryStream destination = new MemoryStream())
          {
            stream.CopyTo((Stream) destination);
            using (System.Drawing.Image sourceImage = System.Drawing.Image.FromStream((Stream) destination))
            {
              if (Config.Get<LibrariesConfig>().Images.StoreDynamicResizedImagesAsThumbnails)
              {
                manager.Provider.RegenerateThumbnail(liveMediaContent, sourceImage, newThumbnailName, method, methodArgument, mimeType);
                MediaContent master = manager.Lifecycle.GetMaster((ILifecycleDataItem) liveMediaContent) as MediaContent;
                if (master.FileId == liveMediaContent.FileId)
                  manager.CopyThumbnails(liveMediaContent, master);
                manager.Provider.CommitTransaction();
                return liveMediaContent.GetThumbnails().FirstOrDefault<Thumbnail>((Func<Thumbnail, bool>) (t => t.Name == newThumbnailName));
              }
              using (System.Drawing.Image image = this.ImageProcessor.ProcessImage(sourceImage, method, methodArgument))
              {
                Thumbnail thumbnail = new Thumbnail();
                thumbnail.Width = image.Width;
                thumbnail.Height = image.Height;
                thumbnail.Parent = liveMediaContent;
                thumbnail.Type = ThumbnailTypes.Custom;
                thumbnail.Culture = SystemManager.CurrentContext.Culture.Name;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                  thumbnail.MimeType = ImagesHelper.SaveImageToStream(image, (Stream) memoryStream, mimeType, true);
                  thumbnail.Data = memoryStream.ToArray();
                }
                return thumbnail;
              }
            }
          }
        }
      }
    }
  }
}
