// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.Web.Services.IControlTemplateService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.ControlTemplates.Web.Services
{
  /// <summary>
  /// The WCF web service that is used to work with control templates
  /// </summary>
  [ServiceContract]
  [AllowDynamicFields]
  public interface IControlTemplateService
  {
    /// <summary>
    /// Gets the single control template and returs it in JSON format.
    /// </summary>
    /// <param name="id">Id of the control template that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the control template.</param>
    /// <returns>An instance of ContentItemContext that contains the control template to be retrieved.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{id}/?providerName={providerName}&VersionId={VersionId}")]
    ItemContext<ControlPresentation> GetControlTemplate(
      string id,
      string providerName,
      string VersionId);

    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/versions/{id}/?providerName={providerName}&VersionId={VersionId}")]
    ItemContext<ControlTemplateVersionInfo> GetTemplateVersionInfo(
      string id,
      string providerName,
      string VersionId);

    /// <summary>
    /// Gets the single control template and returs it in XML format.
    /// </summary>
    /// <param name="id">Id of the control template that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the control template.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the control template to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{id}/?providerName={providerName}&VersionId={VersionId}")]
    ItemContext<ControlPresentation> GetControlTemplateInXml(
      string id,
      string providerName,
      string VersionId);

    /// <summary>
    /// Gets the collection of all control templates and returns the result in JSON format.
    /// </summary>
    /// <param name="providerName">Name of the provider to be used to get the content items.</param>
    /// <param name="sortExpression">Sort expression used to order the control templates in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="templateFilter">Specific filter for widget templates.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "?providerName={providerName}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&templateFilter={templateFilter}")]
    CollectionContext<ControlTemplateViewModel> GetControlTemplates(
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string templateFilter);

    /// <summary>
    /// Gets the collection of all control templates and returns the result in XML format.
    /// </summary>
    /// <param name="providerName">Name of the provider to be used to get the content items.</param>
    /// <param name="sortExpression">Sort expression used to order the control templates in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="templateFilter">Specific filter for widget templates.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/?providerName={providerName}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&templatesFilter={templateFilter}")]
    CollectionContext<ControlTemplateViewModel> GetControlTemplatesInXml(
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string templateFilter);

    /// <summary>
    /// Saves the control template and returns the saved control template in JSON format. If a control template
    /// with the specified id exists the it will be updated; otherwise new control template will
    /// be created.
    /// </summary>
    /// <param name="context">The control template to be saved.</param>
    /// <param name="id">The id of the control template to be saved.</param>
    /// <param name="providerName">Name of the provider that is to be used to save the control template.</param>
    /// <returns>An instance of ItemContext that contains the control template that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{id}/?providerName={providerName}")]
    ItemContext<ControlPresentation> SaveControlTemplate(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName);

    /// <summary>
    /// Saves the control template and returns the saved control template in XML format. If a control template
    /// with the specified id exists the it will be updated; otherwise new control template will
    /// be created.
    /// </summary>
    /// <param name="context">The control template to be saved.</param>
    /// <param name="id">The id of the control template to be saved.</param>
    /// <param name="providerName">Name of the provider that is to be used to save the control template.</param>
    /// <returns>An instance of ItemContext that contains the control template that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{id}/?providerName={providerName}")]
    ItemContext<ControlPresentation> SaveControlTemplateInXml(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName);

    /// <summary>
    /// Deletes the control template and returns true if the control template has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="id">Id of the control template to be deleted.</param>
    /// <param name="providerName">Name of the provider to be used when deleting the control template.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{id}/?providerName={providerName}")]
    bool DeleteControlTemplate(string id, string providerName);

    /// <summary>
    /// Deletes the control template and returns true if the control template has been deleted; otherwise false.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="id">Id of the control template to be deleted.</param>
    /// <param name="providerName">Name of the provider to be used when deleting the control template.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/{id}/?providerName={providerName}")]
    bool DeleteControlTemplateInXml(string id, string providerName);

    /// <summary>
    /// Restores the control template to its default markup from the embedded resource.
    /// </summary>
    /// <param name="context">The control template to be restored.</param>
    /// <param name="id">The id of the control template.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>An instance of the control template that was restored to the default one</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "restore/{id}/?providerName={providerName}")]
    ItemContext<ControlPresentation> RestoreControlTemplate(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName);

    /// <summary>
    /// Restores the control template to its default markup from the embedded resource.
    /// </summary>
    /// <param name="context">The control template to be restored.</param>
    /// <param name="id">The id of the control template.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>An instance of the control template that was restored to the default one</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/restore/{id}/?providerName={providerName}")]
    ItemContext<ControlPresentation> RestoreControlTemplateInXml(
      ItemContext<ControlPresentation> context,
      string id,
      string providerName);

    /// <summary>
    /// Deletes an array of control templates.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="ids">An array containing the ids of the control templates to be deleted.</param>
    /// <param name="providerName">Name of the provider to be used when deleting the control templates.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}")]
    bool BatchDeleteControlTemplates(string[] ids, string providerName);

    /// <summary>
    /// Deletes an array of control templates.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="ids">An array containing the ids of the control templates to be deleted.</param>
    /// <param name="providerName">Name of the provider to be used when deleting the control templates.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/batch/?providerName={providerName}")]
    bool BatchDeleteControlTemplatesInXml(string[] ids, string providerName);

    /// <summary>
    /// Gets common properties of the data item in JSON format.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "commonProperties/?controlType={controlType}")]
    CollectionContext<DataItemPropertyViewModel> GetCommonProperties(
      string controlType);

    /// <summary>
    /// Gets common properties of the data item in Xml format.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/commonProperties/?controlType={controlType}")]
    CollectionContext<DataItemPropertyViewModel> GetCommonPropertiesInXml(
      string controlType);

    /// <summary>
    /// Gets non-common properties of the data item in JSON format.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "otherProperties/?controlType={controlType}")]
    CollectionContext<DataItemPropertyViewModel> GetOtherProperties(
      string controlType);

    /// <summary>
    /// Gets non-common properties of the data item in Xml format.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/otherProperties/?controlType={controlType}")]
    CollectionContext<DataItemPropertyViewModel> GetOtherPropertiesInXml(
      string controlType);

    /// <summary>
    /// Gets information about the sites which share a specific control template in JSON format.
    /// </summary>
    /// <param name="templateId">The shared control template id.</param>
    /// <param name="sortExpression">The sort expression for the result.</param>
    /// <param name="skip">The skip for the result.</param>
    /// <param name="take">The take for the result.</param>
    /// <param name="filter">The filter for the result.</param>
    /// <returns>Information about the sites which share a specific control template in JSON format.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/sitelinks/{templateId}/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<SiteItemLinkViewModel> GetSharedSites(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets information about the sites which share a specific control template in XML format.
    /// </summary>
    /// <param name="templateId">The shared control template id.</param>
    /// <param name="sortExpression">The sort expression for the result.</param>
    /// <param name="skip">The skip for the result.</param>
    /// <param name="take">The take for the result.</param>
    /// <param name="filter">The filter for the result.</param>
    /// <returns>Information about the sites which share a specific control template in XML format.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/sitelinks/{templateId}/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<SiteItemLinkViewModel> GetSharedSitesInXml(
      string templateId,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Shares a control template on multiple sites. The result of the saving process is returned in JSON format.
    /// </summary>
    /// <param name="templateId">The template id to share.</param>
    /// <param name="sharedSiteIDs">The IDs of the sites to share the template on.</param>
    /// <returns>True, if the saving was successful. False otherwise, in JSON format.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/savesitelinks/{templateId}/")]
    bool SaveSharedSites(string templateId, string[] sharedSiteIDs);

    /// <summary>
    /// Shares a control template on multiple sites. The result of the saving process is returned in XML format.
    /// </summary>
    /// <param name="templateId">The template id to share.</param>
    /// <param name="sharedSiteIDs">The IDs of the sites to share the template on.</param>
    /// <returns>True, if the saving was successful. False otherwise, in XML format.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/savesitelinks/{templateId}/")]
    bool SaveSharedSitesInXml(string templateId, string[] sharedSiteIDs);
  }
}
