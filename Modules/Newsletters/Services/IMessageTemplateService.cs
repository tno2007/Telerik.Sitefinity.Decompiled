// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.IMessageTemplateService
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
  /// Interface that mandates all the members of any class that acts as a service for message templates.
  /// </summary>
  [ServiceContract]
  public interface IMessageTemplateService
  {
    /// <summary>
    /// Saves a template. If the template with specified id exists that template will be updated; otherwise new template will be created.
    /// The saved template is returned in JSON format.
    /// </summary>
    /// <param name="templateId">Id of the template to be saved.</param>
    /// <param name="template">The view model class that represents the message template.</param>
    /// <param name="provider">The provider through which the template ought to be saved.</param>
    /// <param name="isPageBased">Determines weather the template is based on the sitefinity page.</param>
    /// <returns>The saved template.</returns>
    [WebHelp(Comment = "Saves a template. If the template with specified id exists that template will be updated; otherwise new template will be created. The saved template is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{templateId}/?provider={provider}&isPageBased={isPageBased}")]
    [OperationContract]
    MessageBodyViewModel SaveTemplate(
      string templateId,
      MessageBodyViewModel template,
      string provider,
      bool isPageBased);

    /// <summary>
    /// Saves a template. If the template with specified id exists that template will be updated; otherwise new template will be created.
    /// The saved template is returned in XML format.
    /// </summary>
    /// <param name="templateId">The id of the template.</param>
    /// <param name="template">The view model class that represents the message template.</param>
    /// <param name="provider">The provider through which the template ought to be saved.</param>
    /// <param name="isPageBased">Determines weather the template is based on the sitefinity page.</param>
    /// <returns>The saved template.</returns>
    [WebHelp(Comment = "Saves a template. If the template with specified id exists that template will be updated; otherwise new template will be created. The saved template is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{templateId}/?provider={provider}&isPageBased={isPageBased}")]
    [OperationContract]
    MessageBodyViewModel SaveTemplateInXml(
      string templateId,
      MessageBodyViewModel template,
      string provider,
      bool isPageBased);

    /// <summary>
    /// Gets all message templates of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the message templates ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the message templates.</param>
    /// <param name="skip">Number of message templates to skip in result set. (used for paging)</param>
    /// <param name="take">Number of message templates to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MessageBodyViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all message templates of the newsletter module for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<MessageBodyViewModel> GetTemplates(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all message templates of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the message templates ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the message templates.</param>
    /// <param name="skip">Number of message templates to skip in result set. (used for paging)</param>
    /// <param name="take">Number of message templates to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MessageBodyViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all message templates of the newsletter module for the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<MessageBodyViewModel> GetTemplatesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Deletes the message template by id and returns true if the message template has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="templateId">Id of the message template to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes message template for given provider and supplied id. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{templateId}/?provider={provider}")]
    [OperationContract]
    bool DeleteTemplate(string templateId, string provider);

    /// <summary>
    /// Deletes the message template by id and returns true if the message template has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="templateId">Id of the message template to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes message template for given provider and supplied id. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{templateId}/?provider={provider}")]
    [OperationContract]
    bool DeleteTemplateInXml(string templateId, string provider);

    /// <summary>
    /// Deletes a collection of message templates. Result is returned in JSON format.
    /// </summary>
    /// <param name="templateIds">An array of the ids of the message templates to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all message templates have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple message templates.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteTemplates(string[] templateIds, string provider);

    /// <summary>
    /// Deletes a collection of message templates. Result is returned in XML format.
    /// </summary>
    /// <param name="templateIds">An array of the ids of the message templates to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all message templates have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple message templates.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteTemplatesInXml(string[] templateIds, string provider);

    /// <summary>
    /// Gets the message template and returns it in JSON format.
    /// </summary>
    /// <param name="templateId">Id of the message template that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the message template.</param>
    /// <returns>An instance of MessageBodyViewModel.</returns>
    [WebHelp(Comment = "Gets the message template and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{templateId}/?provider={provider}")]
    [OperationContract]
    MessageBodyViewModel GetTemplate(string templateId, string provider);

    /// <summary>
    /// Gets the message template and returns it in XML format.
    /// </summary>
    /// <param name="templateId">Id of the message template that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the message template.</param>
    /// <returns>An instance of MessageBodyViewModel.</returns>
    [WebHelp(Comment = "Gets the message template and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{templateId}/?provider={provider}")]
    [OperationContract]
    MessageBodyViewModel GetTemplateInXml(string templateId, string provider);
  }
}
