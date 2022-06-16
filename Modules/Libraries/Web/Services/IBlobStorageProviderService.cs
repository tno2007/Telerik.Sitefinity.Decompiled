// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.IBlobStorageProviderService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Configuration.Basic;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// The WCF web service interface for blob storage management.
  /// </summary>
  [ServiceContract]
  internal interface IBlobStorageProviderService
  {
    /// <summary>Gets the providers.</summary>
    /// <returns>The providers.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "providers/")]
    CollectionContext<DataProviderSettingsViewModel> GetProviders();

    /// <summary>
    /// Deletes the provided providers in a single transaction.
    /// </summary>
    /// <param name="providers">The providers.</param>
    /// <returns>True if the operation was successfull.</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "providers/batch/")]
    bool BatchDeleteProviders(string[] providers);

    /// <summary>Sets the default.</summary>
    /// <param name="newDefaultProvider">The new default provider.</param>
    /// <returns>True if the operation was successfull.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "setdefault/")]
    bool SetDefault(string newDefaultProvider);

    /// <summary>
    /// Gets the collection of blob storage providers and returns the result in JSON format.
    /// </summary>
    /// <param name="libraryType">The libhrary type</param>
    /// <param name="provider">Provider name</param>
    /// <returns>A CollectionContext instance that contains the collection of WcfBlobStorageProvider objects.</returns>
    [WebHelp(Comment = "Gets the collection of blob storage providers and returns the result in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "providerstats/?libraryType={libraryType}&provider={provider}")]
    [OperationContract]
    CollectionContext<WcfBlobStorageProvider> GetBlobStorageProviderStats(
      string libraryType,
      string provider);

    /// <summary>Gets blob storage provider settings.</summary>
    /// <param name="blobStorageProvider">Blob storage provider name</param>
    /// <returns>Blob storage provider type settings.</returns>
    [WebHelp(Comment = "Gets blob storage provider settings.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "provider-settings/?blobStorageProviderName={blobStorageProviderName}")]
    [OperationContract]
    BlobStorageProviderSettingsViewModel GetBlobStorageProviderSettings(
      string blobStorageProviderName);
  }
}
