// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.IAllCulturesResource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Localization.Web
{
  /// <summary>
  /// Mandates the members to be implemented by any WCF service implementation for
  /// LocalizationResources service.
  /// </summary>
  [ServiceContract]
  [ServiceKnownType(typeof (XmlResourceEntry))]
  public interface IAllCulturesResource
  {
    /// <summary>Gets the resources.</summary>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets a collection of all resources, with an option to retrieve all items for given culture or for given culture and class id. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{classId=null}/{key=null}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ResourceEntry> GetResources(
      string classId,
      string key,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the resources in XML.</summary>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets a collection of all resources, with an option to retrieve all items for given culture or for given culture and class id. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{classId=null}/{key=null}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ResourceEntry> GetResourcesInXml(
      string classId,
      string key,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Saves the resource.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="uiCulture">The UI culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Adds a new resource entry or updates an existing one and then returns the resource in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{uiCulture}/{classId}/{key}/?provider={provider}")]
    [OperationContract]
    ResourceEntry SaveResource(
      string[][] propertyBag,
      string uiCulture,
      string classId,
      string key,
      string provider);

    /// <summary>Saves the resource in XML.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="cultureName">The UI culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Adds a new resource entry or updates an existing one and then returns the resource in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{cultureName}/{classId}/{key}/?provider={provider}")]
    [OperationContract]
    ResourceEntry SaveResourceInXml(
      string[][] propertyBag,
      string cultureName,
      string classId,
      string key,
      string provider);
  }
}
