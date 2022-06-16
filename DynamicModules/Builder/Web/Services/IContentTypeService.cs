// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.Services.IContentTypeService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.DynamicModules.Builder.Data;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.Services
{
  /// <summary>Defines the members of the content type service.</summary>
  [ServiceContract]
  internal interface IContentTypeService
  {
    /// <summary>
    /// Gets a module with the specified id from the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the module ought to be retrieved.</param>
    /// <param name="contentTypeId">Id of the desired module.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> object.</returns>
    [WebHelp(Comment = "Gets a module with the specified id from the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{moduleId}/?provider={provider}")]
    [OperationContract]
    ContentTypeContext GetModule(string moduleId, string provider);

    /// <summary>
    /// Gets a module with the specified id from the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the module ought to be retrieved.</param>
    /// <param name="contentTypeId">Id of the desired module.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> object.</returns>
    [WebHelp(Comment = "Gets a module with the specified id from the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{moduleId}/?provider={provider}")]
    [OperationContract]
    ContentTypeContext GetModuleInXml(string moduleId, string provider);

    /// <summary>
    /// Gets all modules for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the module ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the modules.</param>
    /// <param name="skip">Number of modules to skip in result set. (used for paging)</param>
    /// <param name="take">Number of modules to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.</returns>
    [WebHelp(Comment = "Gets all the content types for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ContentTypeContext> GetModules(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all modules for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the module ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the modules.</param>
    /// <param name="skip">Number of modules to skip in result set. (used for paging)</param>
    /// <param name="take">Number of modules to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.</returns>
    [WebHelp(Comment = "Gets all modules for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ContentTypeContext> GetModulesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all the content types registered in the specified module with the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content types.</param>
    /// <param name="skip">Number of content types to skip in result set. (used for paging)</param>
    /// <param name="take">Number of content types to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets all the content types registered in the specified module with the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{moduleId}/types/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ContentTypeContext> GetContentTypes(
      string moduleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all the content types registered in the specified module with the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content types.</param>
    /// <param name="skip">Number of content types to skip in result set. (used for paging)</param>
    /// <param name="take">Number of content types to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets all the content types registered in the specified module with the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{moduleId}/types/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ContentTypeContext> GetContentTypesInXml(
      string moduleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all content types available to be set as parent to the desired content type. The results are returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="contentTypeId">Id of the desired content type.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets all content types available to be set as parent to the desired content type. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{moduleId}/types/{contentTypeId}/availableparents/?provider={provider}")]
    [OperationContract]
    CollectionContext<ContentTypeContext> GetContentTypeAvailableParents(
      string moduleId,
      string contentTypeId,
      string provider);

    /// <summary>
    /// Gets all content types available to be set as parent to the desired content type. The results are returned in XML format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="contentTypeId">Id of the desired content type.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets all content types available to be set as parent to the desired content type. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{moduleId}/types/{contentTypeId}/availableparents/?provider={provider}")]
    [OperationContract]
    CollectionContext<ContentTypeContext> GetContentTypeAvailableParentsInXml(
      string moduleId,
      string contentTypeId,
      string provider);

    /// <summary>
    /// Gets the tree of the content types registered in the specified module with the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="!:ContentTypeTreeItem" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets the tree of the content types registered in the specified module with the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{moduleId}/types/tree/?provider={provider}&filter={filter}")]
    [OperationContract]
    CollectionContext<ContentTypeTreeItemContext> GetContentTypesTree(
      string moduleId,
      string provider,
      string filter);

    /// <summary>
    /// Gets the tree of the content types registered in the specified module with the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="provider">Name of the provider from which the content types ought to be retrieved.</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="!:ContentTypeTreeItem" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets the tree of the content types registered in the specified module with the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{moduleId}/types/tree/?provider={provider}&filter={filter}")]
    [OperationContract]
    CollectionContext<ContentTypeTreeItemContext> GetContentTypesTreeInXml(
      string moduleId,
      string provider,
      string filter);

    /// <summary>
    /// Gets a content type with the specified id from the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the content type ought to be retrieved.</param>
    /// <param name="contentTypeId">Id of the desired content type.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> object.</returns>
    [WebHelp(Comment = "Gets a content type with the specified id from the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{moduleId}/types/{contentTypeId}/?provider={provider}")]
    [OperationContract]
    ContentTypeContext GetContentType(
      string moduleId,
      string contentTypeId,
      string provider);

    /// <summary>
    /// Gets a content type with the specified id from the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the content type ought to be retrieved.</param>
    /// <param name="contentTypeId">Id of the desired content type.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext" /> object.</returns>
    [WebHelp(Comment = "Gets a content type with the specified id from the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{moduleId}/types/{contentTypeId}/?provider={provider}")]
    [OperationContract]
    ContentTypeContext GetContentTypeInXml(
      string moduleId,
      string contentTypeId,
      string provider);

    /// <summary>
    /// Saves a content types. If the content type with specified id exists that content type will be updated; otherwise new content type will be created.
    /// The saved content type is returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The Id of the module to which the content type belongs to.</param>
    /// <param name="contentTypeContext">The content type context that is going to be saved.</param>
    /// <param name="provider">The provider through which the product ought to be saved.</param>
    /// <param name="updateWidgetTemplates">Specify whether widget templates will be regenerated.</param>
    /// <returns>The content type context.</returns>
    [WebHelp(Comment = "Saves a content type. If the content type with specified id exists that content type will be updated; otherwise new content type will be created. The saved content type is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{moduleId}/?provider={provider}&updateWidgetTemplates={updateWidgetTemplates}")]
    [OperationContract]
    ContentTypeContext SaveContentType(
      string moduleId,
      ContentTypeContext contentType,
      string provider,
      bool updateWidgetTemplates);

    /// <summary>
    /// Saves a content types. If the content type with specified id exists that content type will be updated; otherwise new content type will be created.
    /// The saved content type is returned in XML format.
    /// </summary>
    /// <param name="contentTypeId">Id of the content type to be saved.</param>
    /// <param name="contentTypeContext">The content type context that is going to be saved.</param>
    /// <param name="provider">The provider through which the product ought to be saved.</param>
    /// <param name="updateWidgetTemplates">Specify whether widget templates will be regenerated.</param>
    /// <returns>The content type context.</returns>
    [WebHelp(Comment = "Saves a content type. If the content type with specified id exists that content type will be updated; otherwise new content type will be created. The saved content type is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{moduleId}/?provider={provider}&updateWidgetTemplates={updateWidgetTemplates}")]
    [OperationContract]
    ContentTypeContext SaveContentTypeInXml(
      string moduleId,
      ContentTypeContext contentType,
      string provider,
      bool updateWidgetTemplates);

    /// <summary>
    /// Gets all the content types under specific content type registered in the specified module with the given provider. The saved content type is returned in JSON format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="contentTypeId">The content type id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets all the content types under specific content type registered in the specified module with the given provider. The saved content type is returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/{moduleId}/types/{contentTypeId}/types/?provider={provider}")]
    [OperationContract]
    CollectionContext<ContentTypeContext> GetContentTypeChildren(
      string moduleId,
      string contentTypeId,
      string provider);

    /// <summary>
    /// Gets all the content types under specific content type registered in the specified module with the given provider. The saved content type is returned in XML format.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="contentTypeId">The content type id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets all the content types under specific content type registered in the specified module with the given provider. The saved content type is returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{moduleId}/types/{contentTypeId}/types/?provider={provider}")]
    [OperationContract]
    CollectionContext<ContentTypeContext> GetContentTypeChildrenInXml(
      string moduleId,
      string contentTypeId,
      string provider);

    /// <summary>
    /// Activates the module. The module is returned in JSON format.
    ///  </summary>
    /// <param name="contentTypeId">Id of the module to be activated.</param>
    /// <param name="provider">The provider through which the module ought to be activated.</param>
    /// <returns>The module context.</returns>
    [WebHelp(Comment = "Activates the module. The module is returned in JSON format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/activate/?provider={provider}")]
    [OperationContract]
    ContentTypeContext ActivateModule(string moduleId, string provider);

    /// <summary>Activates the module.</summary>
    /// <param name="contentTypeId">Id of the module to be activated.</param>
    /// <param name="provider">The provider through which the module ought to be activated.</param>
    /// <returns>The module context.</returns>
    [WebHelp(Comment = "Activates the module. The module is returned in XML format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/activate/?provider={provider}")]
    [OperationContract]
    ContentTypeContext ActivateContentTypeInXml(string moduleId, string provider);

    /// <summary>Deactivates the module.</summary>
    /// <param name="contentTypeId">Id of the module to be deactivated.</param>
    /// <param name="provider">The provider through which the module ought to be deactivated.</param>
    /// <returns>The module context.</returns>
    [WebHelp(Comment = "Deactivates the module. The module is returned in JSON format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/deactivate/?provider={provider}")]
    [OperationContract]
    ContentTypeContext DeactivateModule(string moduleId, string provider);

    /// <summary>Deactivates the module.</summary>
    /// <param name="contentTypeId">Id of the module to be deactivated.</param>
    /// <param name="provider">The provider through which the module ought to be deactivated.</param>
    /// <returns>The module context.</returns>
    [WebHelp(Comment = "Deactivates the module. The module is returned in XML format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/deactivate/?provider={provider}")]
    [OperationContract]
    ContentTypeContext DeactivateModuleInXml(string moduleId, string provider);

    /// <summary>Deletes the specified module.</summary>
    /// <param name="contentTypeId">The module id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="deleteData">Determines whether the module data should be deleted</param>
    [WebHelp(Comment = "Deletes the module.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{moduleId}/?provider={provider}&deleteData={deleteData}")]
    [OperationContract]
    DeleteContentTypeContext DeleteModule(
      string moduleId,
      string provider,
      bool deleteData);

    /// <summary>Deletes the specified module.</summary>
    /// <param name="contentTypeId">The module id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="deleteData">Determines whether the module data should be deleted</param>
    [WebHelp(Comment = "Deletes the content type in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{moduleId}/?provider={provider}&deleteData={deleteData}")]
    [OperationContract]
    DeleteContentTypeContext DeleteModuleInXml(
      string moduleId,
      string provider,
      bool deleteData);

    /// <summary>
    /// Checks if module with the specified name already exist
    /// </summary>
    /// <param name="moduleName">Name of the module, which should be checked.</param>
    /// <returns>Json data representing the result of checking if the specified module name already exists.</returns>
    [WebHelp(Comment = "Checks if module with the specified name already exist")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/checkModuleName/?moduleName={moduleName}")]
    [OperationContract]
    ModuleNameContext CheckIfModuleWithSpecifiedNameAlreadyExists(string moduleName);

    /// <summary>Updates content type name and description.</summary>
    /// <param name="contentTypeId">Id of the content type to be updated.</param>
    /// <param name="contentTypeContext">The content type context that is going to be saved.</param>
    /// <returns>The saved content type name and description is returned in JSON format.</returns>
    [WebHelp(Comment = "Updates module name and description")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{moduleId}/updateModuleName/")]
    [OperationContract]
    ContentTypeSimpleContext UpdateModuleNameAndDescription(
      string moduleId,
      ContentTypeSimpleContext contentTypeContext);

    /// <summary>Deletes the specified dynamic content type.</summary>
    /// <param name="contentTypeId">The dynamic content type id.</param>
    /// <param name="provider">The provider.</param>
    [WebHelp(Comment = "Deletes the content type.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/content-type/{contentTypeId}/?moduleId={moduleId}&provider={provider}")]
    [OperationContract]
    DeleteContentTypeContext DeleteDynamicContentType(
      string contentTypeId,
      string moduleId,
      string provider);
  }
}
