// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.ILibraryRelocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// Service contract for REST-full operations related to relocating the public Url or transferring the storage of a media library
  /// </summary>
  [ServiceContract]
  internal interface ILibraryRelocationService
  {
    /// <summary>Relocates the library.</summary>
    /// <param name="libraryId">The library id.</param>
    /// <param name="libraryName">The new name of the library.</param>
    /// <param name="libraryUrl">The new library URL.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "relocate/?libraryType={libraryType}&libraryProvider={libraryProvider}&libraryId={libraryId}&libraryUrl={libraryUrl}&blobProvider={blobProvider}")]
    Guid RelocateLibrary(
      string libraryType,
      string libraryProvider,
      string libraryId,
      string libraryUrl = null,
      string blobProvider = null);

    /// <summary>Relocates the folder.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="folderId">The folder id.</param>
    /// <param name="newFolderUrl">The new folder URL.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "relocateFolder/?provider={providerName}&folderId={folderId}&newFolderUrl={newFolderUrl}")]
    void RelocateFolder(string providerName, string folderId, string newFolderUrl);

    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "taskcommand/?taskId={taskId}&command={command}")]
    bool ExecuteTaskCommand(string taskId, string command = null);
  }
}
