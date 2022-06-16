// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.IDynamicListService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Interface that defines the members of the service working with dynamic lists.
  /// </summary>
  [ServiceContract]
  public interface IDynamicListService
  {
    /// <summary>
    /// Gets a collection of dynamic lists information from a given provider.
    /// </summary>
    /// <param name="dynamicListProviderName">The name of the dynamic list provider.</param>
    /// <returns>A collection context of the <see cref="!:DynamiclistSettingsViewModel" /> type.</returns>
    [WebHelp(Comment = "Gets a collection of dynamic lists from a given provider.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/info/{dynamicListProviderName}/")]
    [OperationContract]
    CollectionContext<DynamicListInfoViewModel> GetDynamicListsInfo(
      string dynamicListProviderName);

    /// <summary>
    /// Gets a collection of dynamic lists information from a given provider.
    /// </summary>
    /// <param name="dynamicListProviderName">The name of the dynamic list provider.</param>
    /// <returns>A collection context of the <see cref="!:DynamiclistSettingsViewModel" /> type.</returns>
    [WebHelp(Comment = "Gets a collection of dynamic lists from a given provider.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/info/{dynamicListProviderName}/")]
    [OperationContract]
    CollectionContext<DynamicListInfoViewModel> GetDynamicListsInfoInXml(
      string dynamicListProviderName);
  }
}
