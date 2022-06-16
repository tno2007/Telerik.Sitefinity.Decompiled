// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.IControlPropertyCategories
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>
  /// WCF service for working with the categories of control properties.
  /// </summary>
  [ServiceContract]
  public interface IControlPropertyCategories
  {
    /// <summary>
    /// Gets the collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlPropertyCategory" /> objects.
    /// </summary>
    /// <param name="controlId">Id of the control for which the categories ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider which ought to be used to retrieved the categories.</param>
    /// <param name="skip">Number of items to skip when fetching the categories into the collection (used for paging).</param>
    /// <param name="take">Number of items to take when fetching the categories into the collection (used for paging).</param>
    /// <returns>Collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlPropertyCategory" /> objects.</returns>
    [WebHelp(Comment = "")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{controlId}/?providerName={providerName}&skip={skip}&take={take}")]
    [OperationContract]
    CollectionContext<WcfControlPropertyCategory> GetCategories(
      string controlId,
      string providerName,
      string skip,
      string take);
  }
}
