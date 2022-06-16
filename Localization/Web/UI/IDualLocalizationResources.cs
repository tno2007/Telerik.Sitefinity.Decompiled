// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.IDualLocalizationResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Localization.Web
{
  /// <summary>
  /// Mandates the members to be implemented by any WCF service implementation for
  /// dual localization resources service. This service works with resources for two (display and edit)
  /// cultures at the same time.
  /// </summary>
  [ServiceContract]
  public interface IDualLocalizationResources
  {
    /// <summary>Gets the all the resources.</summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets a collection of all resources, with an option to retrieve all items for given culture or for given culture and class id. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{displayUICulture=null}/{editUICulture=null}/{displayClassId=null}/{editClassId=null}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<DualResourceEntry> GetResources(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the resources in XML.</summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets a collection of all resources, with an option to retrieve all items for given culture or for given culture and class id. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{displayUICulture=null}/{editUICulture=null}/{displayClassId=null}/{editClassId=null}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<DualResourceEntry> GetResourcesInXml(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the resource.</summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets a single resource entry in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{displayUICulture}/{editUICulture}/{displayClassId}/{editClassId}/{displayKey}/{editKey}/?provider={provider}")]
    [OperationContract]
    DualResourceEntry GetResource(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider);

    /// <summary>Gets the resource in XML.</summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets a single resource entry in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{displayUICulture}/{editUICulture}/{displayClassId}/{editClassId}/{displayKey}/{editKey}/?provider={provider}")]
    [OperationContract]
    DualResourceEntry GetResourceInXml(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider);

    /// <summary>Saves the resource.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Adds a new resource entry or updates an existing one and then returns the resource in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{displayUICulture}/{editUICulture}/{displayClassId}/{editClassId}/{displayKey}/{editKey}/?provider={provider}")]
    [OperationContract]
    DualResourceEntry SaveResource(
      string[][] propertyBag,
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider);

    /// <summary>Saves the resource in XML.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Adds a new resource entry or updates an existing one and then returns the resource in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{displayUICulture}/{editUICulture}/{displayClassId}/{editClassId}/{displayKey}/{editKey}/?provider={provider}")]
    [OperationContract]
    DualResourceEntry SaveResourceInXml(
      string[][] propertyBag,
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider);

    /// <summary>
    /// Deletes the resource and returns true if the resource has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    [WebHelp(Comment = "Deletes a resource entry.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{displayUICulture}/{editUICulture}/{displayClassId}/{editClassId}/{displayKey}/{editKey}/?provider={provider}")]
    [OperationContract]
    bool DeleteResource(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider);

    /// <summary>
    /// Deletes the resource and returns true if the resource has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    [WebHelp(Comment = "Deletes a resource entry.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{displayUICulture}/{editUICulture}/{displayClassId}/{editClassId}/{displayKey}/{editKey}/?provider={provider}")]
    [OperationContract]
    bool DeleteResourceInXml(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider);
  }
}
