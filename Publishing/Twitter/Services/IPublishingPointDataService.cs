// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Services.IPublishingPointDataService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Publishing.Twitter.Services.Data;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Publishing.Twitter.Services
{
  [ServiceContract(Namespace = "PublishingAdminService")]
  public interface IPublishingPointDataService
  {
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{pointId}/?providerName={providerName}&take={take}")]
    CollectionContext<SerializableDynamicObject> GetPublishingPointDataItems(
      string providerName,
      string pointId,
      int take);
  }
}
