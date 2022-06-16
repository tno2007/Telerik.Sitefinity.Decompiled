// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.IThumbnailService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing.Models;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// The WCF web service interface for thumbnails management.
  /// </summary>
  [ServiceContract]
  public interface IThumbnailService
  {
    /// <summary>Gets the thumbnail profiles.</summary>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "thumbnail-profiles/?libraryType={libraryType}&filter={filter}&viewType={viewType}")]
    CollectionContext<WcfThumbnailProfile> GetThumbnailProfiles(
      string libraryType,
      string filter,
      string viewType);

    /// <summary>
    /// Gets the thumbnail profiles for the specified library.
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{libraryId}/profiles?provider={provider}&viewType={viewType}")]
    CollectionContext<WcfThumbnailProfile> GetProfileNames(
      string libraryId,
      string provider,
      string viewType);

    /// <summary>Deletes the profiles that are passed to the service.</summary>
    /// <param name="profileKeys">The profile keys.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "thumbnail-profiles/batch/?libraryType={libraryType}")]
    CollectionContext<WcfThumbnailProfile> BatchDeleteProfiles(
      string[] profileKeys,
      string libraryType);

    /// <summary>Regenerates the thumbnails for the selected library.</summary>
    /// <param name="libraryType">Type of the library.</param>
    /// <param name="libraryProvider">The library provider.</param>
    /// <param name="libraryId">The library id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "regenerate?libraryType={libraryType}&libraryProvider={libraryProvider}&libraryId={libraryId}")]
    Guid RegenerateThumbnails(
      string libraryType,
      string libraryProvider,
      string libraryId,
      string[] thumbnailProfiles);

    /// <summary>Gets the thumbnails.</summary>
    /// <param name="mediaContentId">The media content id.</param>
    /// <param name="libraryProvider">The library provider.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "?mediaContentId={mediaContentId}&libraryProvider={libraryProvider}")]
    CollectionContext<ThumbnailViewModel> GetThumbnails(
      string mediaContentId,
      string libraryProvider);

    /// <summary>
    /// Gets the properties needed for a given image processing method.
    /// </summary>
    /// <param name="methodName">The name of the method.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "processing-method/properties?methodName={methodName}")]
    CollectionContext<MethodPropertyData> GetProcessingMethodProperties(
      string methodName);

    /// <summary>
    /// Gets the url for a given custom image thumbnail by its parameters.
    /// </summary>
    /// <param name="imageId">The Id of the image.</param>
    /// <param name="customUrlParameters">JSON serialized custom image thumbnail parameters.</param>
    /// <param name="libraryProvider">The library provider.</param>
    /// <remarks>
    ///  If incorrect image Id the method returns empty string.
    ///  If incorrect customUrlParameters the method returns the original image URL
    /// </remarks>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "custom-image-thumbnail/url?imageId={imageId}&customUrlParameters={customUrlParameters}&libraryProvider={libraryProvider}")]
    string GetCustomImageThumbnailUrl(
      string imageId,
      string customUrlParameters,
      string libraryProvider);

    /// <summary>
    /// Checks for a given custom image thumbnail if the parameters are correct.
    /// </summary>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="parameters">JSON serialized custom image thumbnail parameters.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "custom-image-thumbnail/checkParameters?methodName={methodName}&parameters={parameters}")]
    string CheckCustomImageThumbnailParameters(string methodName, string parameters);
  }
}
