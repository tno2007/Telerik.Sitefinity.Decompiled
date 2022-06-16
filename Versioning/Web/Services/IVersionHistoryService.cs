// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.Services.IVersionHistoryService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Versioning.ColumnProviders;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Versioning.Web.Services
{
  [ServiceContract(Namespace = "VersionHistoryService")]
  public interface IVersionHistoryService
  {
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/columns/?itemType={itemType}&itemId={itemId}&provider={provider}")]
    IEnumerable<VersionHistoryColumn> GetColumns(
      string itemType,
      string itemId,
      string provider);

    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?itemType={itemType}&itemId={itemId}&skip={skip}&take={take}")]
    CollectionContext<WcfChange> GetItemVersionHistory(
      string itemType,
      string itemId,
      int skip,
      int take);

    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{changeId}/")]
    bool DeleteChange(string changeId);

    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{changeId}/")]
    bool SaveChangeComment(string comment, string changeId);
  }
}
