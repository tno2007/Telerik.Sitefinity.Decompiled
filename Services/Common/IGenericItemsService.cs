// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Common.IGenericItemsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Common
{
  /// <summary>
  /// WCF interface for the service used by generic item selectors.
  /// </summary>
  [ServiceContract]
  [AllowDynamicFields]
  public interface IGenericItemsService
  {
    /// <summary>Method used to get the collection of generic items.</summary>
    /// <param name="itemType">The type of items that ought to be returned.</param>
    /// <param name="itemSurrogateType">The surrogate type, such as WCF representation of the original type, that ought to be returned.</param>
    /// <param name="sortExpression">Sort expression used to order the items.</param>
    /// <param name="skip">Number of items to skip before fetching the collection (used for paging)</param>
    /// <param name="take">Number of items to fetch in the collection (used for paging)</param>
    /// <param name="filter">Filer expression in Dynamic LINQ format used to filter through the items</param>
    /// <param name="providerName">Name of the provider from which the items ought to be fetched.</param>
    /// <param name="ignoreAdminUsers">Condition to ignore or not admin users.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Web.Services.SerializedCollectionContext" /> with items serialized in the JSON format.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?itemType={itemType}&itemSurrogateType={itemSurrogateType}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={providerName}&allProviders={allProviders}&ignoreAdminUsers={ignoreAdminUsers}")]
    CollectionContext<WcfItemBase> GetGenericItems(
      string itemType,
      string itemSurrogateType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool allProviders,
      bool ignoreAdminUsers);
  }
}
