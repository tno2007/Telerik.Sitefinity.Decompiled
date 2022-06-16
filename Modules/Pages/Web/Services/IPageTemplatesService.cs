// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.IPageTemplatesService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>Web services for page templates management.</summary>
  [ServiceContract]
  public interface IPageTemplatesService
  {
    /// <summary>
    /// Gets the collection of all page templates and returns the result in JSON format. Returns the templates in ascedning order by their title.
    /// </summary>
    /// <returns>A collection context that contains page templates.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "?pageFilter={pageFilter}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&root={root}")]
    CollectionContext<PageTemplateViewModel> GetPageTemlatesInCondition(
      string pageFilter,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root);

    /// <summary>
    /// Gets the collection of all page templates returns the result in XML format. Returns the templates in ascedning order by their title.
    /// </summary>
    /// <returns>A collection context that contains page templates.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/?pageFilter={pageFilter}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&root={root}")]
    CollectionContext<PageTemplateViewModel> GetPageTemlatesInConditionInXml(
      string pageFilter,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root);

    /// <summary>
    /// Gets the single page template and returs it in JSON format.
    /// </summary>
    /// <param name="templateId">Id of the template item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the template.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{templateId}/?providerName={providerName}")]
    WcfPageTemplateContext GetPageTemplate(
      string templateId,
      string providerName);

    /// <summary>
    /// Gets the single page template and returs it in XML format.
    /// </summary>
    /// <param name="templateId">Id of the template item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the template.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{templateId}/?providerName={providerName}")]
    WcfPageTemplateContext GetPageTemplateInXml(
      string templateId,
      string providerName);

    /// <summary>
    /// Saves the page template and returns the result in JSON format.
    /// </summary>
    /// <param name="templateContext">The template context.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{templateId}/?itemType={itemType}&providerName={providerName}&managerType={managerType}&duplicate={duplicate}")]
    WcfPageTemplateContext SavePageTemplate(
      WcfPageTemplateContext templateContext,
      string templateId,
      string itemType,
      string providerName,
      string managerType,
      bool duplicate);

    /// <summary>
    /// Saves the page template and returns the result in XML format.
    /// </summary>
    /// <param name="templateContext">The template context.</param>
    /// <returns>A context that contains saved page template.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{templateId}/?itemType={itemType}&providerName={providerName}&managerType={managerType}&duplicate={duplicate}")]
    WcfPageTemplateContext SavePageTemplateInXml(
      WcfPageTemplateContext templateContext,
      string templateId,
      string itemType,
      string providerName,
      string managerType,
      bool duplicate);

    /// <summary>
    /// Deletes the page template and returns true if the page has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="templateId">The template id.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page template.</param>
    /// <returns>true if the page template has been deleted; otherwise false.</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{templateId}/?providerName={providerName}&language={deletedLanguage}")]
    bool DeletePageTemplate(string templateId, string providerName, string deletedLanguage);

    /// <summary>
    /// Deletes the page template and returns true if the page template has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="templateId">The template id.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page template.</param>
    /// <returns>true if the page template has been deleted; otherwise false.</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{templateId}/?providerName={providerName}&language={deletedLanguage}")]
    bool DeletePageTemplateInXml(string templateId, string providerName, string deletedLanguage);

    /// <summary>
    /// Deletes an array of pages templates.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the page templates to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}&language={deletedLanguage}")]
    bool BatchDeletePageTemplates(string[] Ids, string providerName, string deletedLanguage);

    /// <summary>
    /// Deletes an array of pages templates.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the page templates to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page templates.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/?providerName={providerName}&language={deletedLanguage}")]
    bool BatchDeletePageTemplatesInXml(string[] Ids, string providerName, string deletedLanguage);

    /// <summary>
    /// Changes the parent template.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="templateId">The template id whoose parent is going to be changed.</param>
    /// <param name="newTemplateId">The new template id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "changeTemplate/{templateId}/?newTemplateId={newTemplateId}")]
    [WebHelp(Comment = "Changes the parent template of the specified template")]
    bool ChangeTemplate(string templateId, string newTemplateId);

    /// <summary>
    /// Changes the parent template.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="templateId">The template id whoose parent is going to be changed.</param>
    /// <param name="newTemplateId">The new template id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/changeTemplate/{templateId}/?newTemplateId={newTemplateId}")]
    [WebHelp(Comment = "Changes the parent template of the specified template")]
    bool ChangeTemplateInXml(string templateId, string newTemplateId);

    /// <summary>Publishes the specified templates.</summary>
    /// <param name="pageIDs">The template IDs.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batchPublishDraft/")]
    [WebHelp(Comment = "Publishes the specified templates (IDs)")]
    void BatchPublishDraft(string[] templateIDs);

    /// <summary>Publishes the specified templates.</summary>
    /// <param name="pageIDs">The template IDs.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/batchPublishDraft/")]
    [WebHelp(Comment = "Publishes the specified templates (IDs)")]
    void BatchPublishDraftInXml(string[] templateIDs);

    /// <summary>
    /// Gets information about the sites which share a specific page template in JSON format.
    /// </summary>
    /// <param name="templateId">The shared page template id.</param>
    /// <param name="sortExpression">The sort expression for the result.</param>
    /// <param name="skip">The skip for the result.</param>
    /// <param name="take">The take for the result.</param>
    /// <param name="filter">The filter for the result.</param>
    /// <returns>Information about the sites which share a specific page template in JSON format.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sitelinks/{templateId}/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<SiteItemLinkViewModel> GetSharedSites(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets information about the sites which share a specific page template in XML format.
    /// </summary>
    /// <param name="templateId">The shared page template id.</param>
    /// <param name="sortExpression">The sort expression for the result.</param>
    /// <param name="skip">The skip for the result.</param>
    /// <param name="take">The take for the result.</param>
    /// <param name="filter">The filter for the result.</param>
    /// <returns>Information about the sites which share a specific page template in XML format.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/sitelinks/{templateId}/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<SiteItemLinkViewModel> GetSharedSitesInXml(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Shares a page template on multiple sites. The result of the saving process is returned in JSON format.
    /// </summary>
    /// <param name="templateId">The template id to share.</param>
    /// <param name="sharedSiteIDs">The IDs of the sites to share the template on.</param>
    /// <returns>True, if the saving was successful. False otherwise, in JSON format.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/savesitelinks/{templateId}/")]
    bool SaveSharedSites(string templateId, string[] sharedSiteIDs);

    /// <summary>
    /// Shares a page template on multiple sites. The result of the saving process is returned in XML format.
    /// </summary>
    /// <param name="templateId">The template id to share.</param>
    /// <param name="sharedSiteIDs">The IDs of the sites to share the template on.</param>
    /// <returns>True, if the saving was successful. False otherwise, in XML format.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/savesitelinks/{templateId}/")]
    bool SaveSharedSitesInXml(string templateId, string[] sharedSiteIDs);
  }
}
