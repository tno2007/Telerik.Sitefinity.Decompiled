// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.IControlPropertyService
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
  /// WCF service interface for working with control properties
  /// </summary>
  [ServiceContract]
  public interface IControlPropertyService
  {
    /// <summary>Gets the collection of control properties.</summary>
    /// <param name="controlId">Id of the control for which the properties ought to be retrieved.</param>
    /// <param name="propertyName">Name of the parent property; null if the first level properties ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider from which the persisted properties ought to be retrieved.</param>
    /// <param name="skip">Number of properties to skip before fetching the properties in the collection (used for paging).</param>
    /// <param name="take">Number of properties to take in the collection of the properties (used for paging).</param>
    /// <param name="sortExpression">Sort expression used to order the properties.</param>
    /// <returns>Collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" /> objects.</returns>
    [WebHelp(Comment = "")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{controlId}/{propertyName=null}/?providerName={providerName}&skip={skip}&take={take}&sortExpression={sortExpression}")]
    [OperationContract]
    CollectionContext<WcfControlProperty> GetProperties(
      string controlId,
      string propertyName,
      string providerName,
      string skip,
      string take,
      string sortExpression);

    /// <summary>Saves the properties.</summary>
    /// <param name="properties">Array of properties to be saved.</param>
    /// <param name="controlId">Id of the control for which the properties ought to be saved.</param>
    /// <param name="providerName">Name of the provider that should be used to save the properties.</param>
    /// <param name="skip">Here only because of WCF URI template issues. Ignore.</param>
    /// <param name="take">Here only because of WCF URI template issues. Ignore.</param>
    /// <param name="sortExpression">Here only because of WCF URI template issues. Ignore.</param>
    [WebHelp(Comment = "")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batch/{controlId}/?providerName={providerName}&pageId={pageId}&mediaType={mediaType}&checkLiveVersion={checkLiveVersion}&upgradePageVersion={upgradePageVersion}&propertyLocalization={propertyLocalization}&isOpenedByBrowseAndEdit={isOpenedByBrowseAndEdit}")]
    [OperationContract]
    void SaveProperties(
      WcfControlProperty[] properties,
      string controlId,
      string providerName,
      string pageId,
      string mediaType,
      bool checkLiveVersion = false,
      bool upgradePageVersion = false,
      PropertyLocalizationType propertyLocalization = PropertyLocalizationType.Default,
      bool isOpenedByBrowseAndEdit = false);
  }
}
