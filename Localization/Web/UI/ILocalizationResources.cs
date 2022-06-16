// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.ILocalizationResources
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
  public interface ILocalizationResources
  {
    /// <summary>
    /// Gets the collection of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> in JSON format.
    /// </summary>
    /// <param name="cultureName">
    /// Name of the culture for which the <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> should be retrieved.
    /// </param>
    /// <param name="classId">
    /// The pageId of the class for which the <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> should be retrieved.
    /// </param>
    /// <param name="provider">
    /// The name of the resource provider from which the resources should be retrived.
    /// </param>
    /// <param name="sortExpression">
    /// The sort expression used to order the retrieved resources.
    /// </param>
    /// <param name="skip">
    /// The number of resources to skip before populating the collection (used primarily for paging).
    /// </param>
    /// <param name="take">
    /// The maximum number of resources to take in the collection (used primarily for paging).
    /// </param>
    /// <param name="filter">
    /// The filter expression in dynamic LINQ format used to filter the retrieved resources.
    /// </param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with resource entry items and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets a collection of all resources, with an option to retrieve all items for given culture or for given culture and class id. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{cultureName=null}/{classId=null}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ResourceEntry> GetResources(
      string cultureName,
      string classId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets the collection of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> in XML format.
    /// </summary>
    /// <param name="cultureName">
    /// Name of the culture for which the <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> should be retrieved.
    /// </param>
    /// <param name="classId">
    /// The pageId of the class for which the <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> should be retrieved.
    /// </param>
    /// <param name="provider">
    /// The name of the resource provider from which the resources should be retrived.
    /// </param>
    /// <param name="sortExpression">
    /// The sort expression used to order the retrieved resources.
    /// </param>
    /// <param name="skip">
    /// The number of resources to skip before populating the collection (used primarily for paging).
    /// </param>
    /// <param name="take">
    /// The maximum number of resources to take in the collection (used primarily for paging).
    /// </param>
    /// <param name="filter">
    /// The filter expression in dynamic LINQ format used to filter the retrieved resources.
    /// </param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with resource entry items and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets a collection of all resources, with an option to retrieve all items for given culture or for given culture and class id. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{cultureName=null}/{classId=null}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ResourceEntry> GetResourcesInXml(
      string cultureName,
      string classId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the single resource entry in JSON format.</summary>
    /// <param name="cultureName">
    /// Name of the culture to which the resource is defined for.
    /// </param>
    /// <param name="classId">
    /// The pageId of the class to which the resource belongs to.
    /// </param>
    /// <param name="key">The key of the resource.</param>
    /// <param name="provider">
    /// The name of the resource provider from which the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> should be retrieved.
    /// </param>
    /// <returns>ResourceEntry object.</returns>
    [WebHelp(Comment = "Gets a single resource entry in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{cultureName}/{classId}/{key}/?provider={provider}")]
    [OperationContract]
    ResourceEntry GetResource(
      string cultureName,
      string classId,
      string key,
      string provider);

    /// <summary>Gets the single resource entry in XML format.</summary>
    /// <param name="cultureName">
    /// Name of the culture to which the resource is defined for.
    /// </param>
    /// <param name="classId">
    /// The pageId of the class to which the resource belongs to.
    /// </param>
    /// <param name="key">The key of the resource.</param>
    /// <param name="provider">
    /// The name of the resource provider from which the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> should be retrieved.
    /// </param>
    /// <returns>ResourceEntry object.</returns>
    [WebHelp(Comment = "Gets a single resource entry in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{cultureName}/{classId}/{key}/?provider={provider}")]
    [OperationContract]
    ResourceEntry GetResourceInXml(
      string cultureName,
      string classId,
      string key,
      string provider);

    /// <summary>
    /// Saves the resource and returns the saved version of the resources in JSON format.
    /// </summary>
    /// <remarks>
    /// If the resource to be saved does not exist, new resource will be created. If the resource,
    /// however, does exist the existing resource will be update.
    /// </remarks>
    /// <param name="propertyBag">
    /// The array of ResourceEntry properties that should be persisted. The first array contains
    /// properties, while the second array holds property name in its first dimension and
    /// property value in its second dimension.
    /// </param>
    /// <param name="cultureName">
    /// Name of the culture for which the resource should be saved.
    /// </param>
    /// <param name="classId">
    /// The pageId of the class for which the resource should be saved.
    /// </param>
    /// <param name="key">
    /// The key of the resource for which the resource should be saved.
    /// </param>
    /// <param name="provider">
    /// The name of the resource provider on which the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> should be saved.
    /// </param>
    /// <returns>
    /// Newly created or updated <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object in JSON format.
    /// </returns>
    [WebHelp(Comment = "Adds a new resource entry or updates an existing one and then returns the resource in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{cultureName}/{classId}/{key}/?provider={provider}")]
    [OperationContract]
    ResourceEntry SaveResource(
      string[][] propertyBag,
      string cultureName,
      string classId,
      string key,
      string provider);

    /// <summary>
    /// Saves the resource and returns the saved version of the resources in XML format.
    /// </summary>
    /// <remarks>
    /// If the resource to be saved does not exist, new resource will be created. If the resource,
    /// however, does exist the existing resource will be update.
    /// </remarks>
    /// <param name="propertyBag">
    /// The array of ResourceEntry properties that should be persisted. The first array contains
    /// properties, while the second array holds property name in its first dimension and
    /// property value in its second dimension.
    /// </param>
    /// <param name="cultureName">
    /// Name of the culture for which the resource should be saved.
    /// </param>
    /// <param name="classId">
    /// The pageId of the class for which the resource should be saved.
    /// </param>
    /// <param name="key">
    /// The key of the resource for which the resource should be saved.
    /// </param>
    /// <param name="provider">
    /// The name of the resource provider on which the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> should be saved.
    /// </param>
    /// <returns>
    /// Newly created or updated ResourceEntry object in XML format.
    /// </returns>
    [WebHelp(Comment = "Adds a new resource entry or updates an existing one and then returns the resource in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{cultureName}/{classId}/{key}/?provider={provider}")]
    [OperationContract]
    ResourceEntry SaveResourceInXml(
      string[][] propertyBag,
      string cultureName,
      string classId,
      string key,
      string provider);

    /// <summary>
    /// Deletes the resource entry and returns true if the resource has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="cultureName">Name of the culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">
    /// The name of the resource provider from which the resource entry should be deleted.
    /// </param>
    [WebHelp(Comment = "Deletes a resource entry.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{cultureName}/{classId}/{key}/?provider={provider}")]
    [OperationContract]
    bool DeleteResource(string cultureName, string classId, string key, string provider);

    /// <summary>
    /// Deletes the resource entry and returns true if the resource has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="cultureName">Name of the culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">
    /// The name of the resource provider from which the resource entry should be deleted.
    /// </param>
    [WebHelp(Comment = "Deletes a resource entry.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{cultureName}/{classId}/{key}/?provider={provider}")]
    [OperationContract]
    bool DeleteResourceInXml(string cultureName, string classId, string key, string provider);
  }
}
