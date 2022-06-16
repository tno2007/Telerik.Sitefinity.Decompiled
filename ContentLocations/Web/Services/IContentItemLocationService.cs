// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Web.Services.IContentItemLocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.ContentLocations.Web.Services
{
  /// <summary>
  /// Defines the members of the content item locations service.
  /// </summary>
  [ServiceContract]
  public interface IContentItemLocationService
  {
    /// <summary>
    /// Gets the content locations and returns them in JSON format.
    /// </summary>
    /// <param name="itemType">The type of the item which content locations will be loaded.</param>
    /// <param name="provider">The provider of the item which content locations will be loaded.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content locations.
    /// </returns>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?itemType={itemType}&provider={provider}")]
    [OperationContract]
    CollectionContext<WcfContentLocation> GetLocations(
      string itemType,
      string provider);

    /// <summary>
    /// Gets the content locations and returns them in JSON format.
    /// </summary>
    /// <param name="itemType">The type of the item which content locations will be loaded.</param>
    /// <param name="provider">The provider of the item which content locations will be loaded.</param>
    /// <param name="itemId">The id of the item which content locations will be loaded.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content locations.
    /// </returns>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{itemId}/?itemType={itemType}&provider={provider}")]
    [OperationContract]
    CollectionContext<WcfItemContentLocation> GetItemLocations(
      string itemType,
      string provider,
      string itemId);

    /// <summary>
    /// Changes the content location's priority using the given pattern.
    /// </summary>
    /// <param name="id">The id of the content location which priority will be changed.</param>
    /// <returns>
    /// True if the content location's priority has changed; otherwise false.
    /// </returns>
    [WebHelp(Comment = "Changes the content location's priority using the given pattern.")]
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?id={id}&direction={direction}")]
    bool ChangeContentLocationPriority(string id, MovingDirection direction);
  }
}
