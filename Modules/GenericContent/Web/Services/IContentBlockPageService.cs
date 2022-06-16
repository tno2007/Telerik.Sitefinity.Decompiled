// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.IContentBlockPageService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services
{
  [ServiceContract]
  public interface IContentBlockPageService
  {
    /// <summary>Gets the content blocks by pages.</summary>
    /// <param name="cultureName">Name of the culture.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets the collection of all pages that contain shared content blocks.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/pages/?cultureName={cultureName}")]
    [OperationContract]
    CollectionContext<ContentBlockPageViewModel> GetContentBlocksByPages(
      string cultureName);
  }
}
