// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Data.IMembershipProviderService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Data
{
  /// <summary>
  /// Provides information about registered membership providers.
  /// </summary>
  [ServiceContract]
  public interface IMembershipProviderService
  {
    /// <summary>
    /// Gets registered role providers. The results are returned in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the providers.</param>
    /// <param name="skip">Number of providers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of providers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Services.Data.DataProviderViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets registered role providers. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/roleProviders/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<DataProviderViewModel> GetRoleProviders(
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets registered user providers. The results are returned in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the providers.</param>
    /// <param name="skip">Number of providers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of providers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Services.Data.DataProviderViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets registered user providers. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/userProviders/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<DataProviderViewModel> GetUserProviders(
      string sortExpression,
      int skip,
      int take,
      string filter);
  }
}
