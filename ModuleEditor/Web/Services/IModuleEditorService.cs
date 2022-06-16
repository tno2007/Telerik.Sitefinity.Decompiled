// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.IModuleEditorService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services
{
  /// <summary>
  /// Interface that defines all the members to be implemented by the module editor service.
  /// </summary>
  [ServiceContract(Namespace = "ModuleEditorService")]
  [ServiceKnownType(typeof (FieldViewModel))]
  public interface IModuleEditorService
  {
    /// <summary>
    /// Gets the collection of default field definitions in JSON format.
    /// </summary>
    /// <param name="sortExpression">The sortExpression expression used to sort the localization classes.</param>
    /// <param name="skip">The number of classes to skip before taking the subset.</param>
    /// <param name="take">The number of classes to take in the subset.</param>
    /// <param name="filter">The filter expression in dynamic LINQ form used to filter the classes.</param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with the collection of field definitions.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of default field definitions in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "default/?contentType={contentType}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<FieldViewModel> GetDefaultFields(
      string contentType,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets the collection of default field definitions in XML format.
    /// </summary>
    /// <param name="sortExpression">The sortExpression expression used to sort the localization classes.</param>
    /// <param name="skip">The number of classes to skip before taking the subset.</param>
    /// <param name="take">The number of classes to take in the subset.</param>
    /// <param name="filter">The filter expression in dynamic LINQ form used to filter the classes.</param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with the collection of field definitions.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of default field definitions in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "default/xml/?contentType={contentType}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<FieldViewModel> GetDefaultFieldsInXml(
      string contentType,
      string sortExpression,
      int skip,
      int take,
      string filter);

    [WebHelp(Comment = "Gets the collection of custom field definitions in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "custom/xml/?contentType={contentType}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={provider}")]
    [OperationContract]
    CollectionContext<FieldViewModel> GetCustomFieldsInXml(
      string contentType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider);

    [WebHelp(Comment = "Gets the collection of custom field definitions in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "custom/?contentType={contentType}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={provider}")]
    [OperationContract]
    CollectionContext<FieldViewModel> GetCustomFields(
      string contentType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider);

    /// <summary>
    /// Applies the changes made on the fields collection parameter in JSON format.
    /// </summary>
    /// <param name="fields">The collection of fields that are changed.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebHelp(Comment = "Applies the changes made on the fields collection parameter in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "applyChanges/?providerName={providerName}")]
    void ApplyChanges(ModuleEditorContext context, string providerName);

    /// <summary>
    /// Applies the changes made on the fields collection parameter in XML format.
    /// </summary>
    /// <param name="fields">The collection of fields that are changed.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebHelp(Comment = "Applies the changes made on the fields collection parameter in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "applyChanges/xml/?providerName={providerName}")]
    void ApplyChangesInXml(ModuleEditorContext context, string providerName);

    /// <summary>
    /// Gets the collection of detail form view names for a specified content type in JSON format.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns>The collection of detail form view names.</returns>
    [WebHelp(Comment = "Gets the collection of detail form view names for a specified content type in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "views/?itemType={itemType}")]
    [OperationContract]
    CollectionContext<WcfDetailFormViewData> GetDetailFormViewNames(
      string itemType);

    /// <summary>
    /// Gets the collection of detail form view names for a specified content type in XML format.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns>The collection of detail form view names.</returns>
    [WebHelp(Comment = "Gets the collection of detail form view names for a specified content type in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "views/xml/?itemType={itemType}")]
    [OperationContract]
    CollectionContext<WcfDetailFormViewData> GetDetailFormViewNamesInXml(
      string itemType);
  }
}
