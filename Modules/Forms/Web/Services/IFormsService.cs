// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.Services.IFormsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Configuration.Web.ViewModels;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Forms.Web.Services
{
  /// <summary>
  ///  WCF interface for the Web services for forms management.
  /// </summary>
  [ServiceContract(Namespace = "FormsService")]
  [AllowDynamicFields]
  public interface IFormsService
  {
    /// <summary>
    /// Gets the collection of forms and returns the result in JSON format.
    /// </summary>
    /// <param name="sortExpression">The sort expression to be applied.</param>
    /// <param name="skip">Bypasses the number of specified items.</param>
    /// <param name="take">Number of items to be retrieved.</param>
    /// <returns>A collection context that contains the selected forms.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&notShared={notShared}")]
    CollectionContext<FormDescriptionViewModel> GetFormDescriptions(
      string filter,
      string sortExpression,
      int skip,
      int take,
      bool notShared);

    /// <summary>
    /// Gets the collection of form descriptions and returns the result in JSON format.
    /// </summary>
    /// <param name="sortExpression">The sort expression to be applied.</param>
    /// <param name="skip">Bypasses the number of specified items.</param>
    /// <param name="take">Number of items to be retrieved.</param>
    /// <returns>A collection context that contains the selected forms.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&notShared={notShared}")]
    [Obsolete]
    CollectionContext<FormDescriptionViewModel> GetFormDescriptionsInXml(
      string filter,
      string sortExpression,
      int skip,
      int take,
      bool notShared);

    /// <summary>
    /// Deletes an array of forms.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the forms to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the form.</param>
    /// <returns>true if the form has been deleted; otherwise false</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}&language={languageToDelete}")]
    bool BatchDeleteFormDescription(string[] ids, string providerName, string languageToDelete);

    /// <summary>
    /// Deletes an array of forms.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the forms to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the form.</param>
    /// <param name="languageToDelete">language translation to be deleted</param>
    /// <returns>true if the form has been deleted; otherwise false</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/?providerName={providerName}&language={languageToDelete}")]
    [Obsolete]
    bool BatchDeleteFormDescriptionInXml(
      string[] ids,
      string providerName,
      string languageToDelete);

    /// <summary>
    /// Deletes the form and returns true if the form has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="formId">Id of the form to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the formDescription.</param>
    /// <param name="languageToDelete">language translation to be deleted</param>
    /// <returns>true if the form has been deleted; otherwise false</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{formId}/?providerName={providerName}&language={languageToDelete}")]
    bool DeleteFormDescription(string formId, string providerName, string languageToDelete);

    /// <summary>
    /// Deletes the form and returns true if the form has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="formId">The form id.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the form.</param>
    /// <param name="languageToDelete">language translation to be deleted</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{formId}/?providerName={providerName}&language={languageToDelete}")]
    [Obsolete]
    bool DeleteFormDescriptionInXml(string formId, string providerName, string languageToDelete);

    /// <summary>
    /// Gets the single form description and returs it in JSON format.
    /// </summary>
    /// <param name="formId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{formId}/?providerName={providerName}&duplicate={duplicate}")]
    FormDescriptionViewModelContext GetFormDescription(
      string formId,
      string providerName,
      bool duplicate);

    /// <summary>
    /// Gets the single form description and returs it in XML format.
    /// </summary>
    /// <param name="formId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{formId}/?providerName={providerName}&duplicate={duplicate}")]
    [Obsolete]
    FormDescriptionViewModelContext GetFormDescriptionInXml(
      string formId,
      string providerName,
      bool duplicate);

    /// <summary>Saves the form description in JSON format.</summary>
    /// <param name="formContext">The form description context.</param>
    /// <param name="formId">The form id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns>An instance context of type <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FormDescriptionViewModelContext" /> that contains the form with specified id at formId parameter.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{formId}/?providerName={providerName}&duplicate={duplicate}")]
    FormDescriptionViewModelContext SaveFormDescription(
      FormDescriptionViewModelContext formContext,
      string formId,
      string providerName,
      bool duplicate);

    /// <summary>Saves the form description in XML format.</summary>
    /// <param name="formContext">The form description context.</param>
    /// <param name="formId">The form id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns>An instance context of type <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FormDescriptionViewModelContext" /> that contains the form with specified id at formId parameter.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{formId}/?providerName={providerName}&duplicate={duplicate}")]
    [Obsolete]
    FormDescriptionViewModelContext SaveFormDescriptionInXml(
      FormDescriptionViewModelContext formContext,
      string formId,
      string providerName,
      bool duplicate);

    /// <summary>Batch saving form description in XML format.</summary>
    /// <param name="formContext">The form description context.</param>
    /// <param name="providerName">Name of the provider.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}")]
    void BatchSaveFormDescription(FormDescriptionViewModel[] formContext, string providerName);

    /// <summary>Batch saving forms in XML.</summary>
    /// <param name="formContext">The form description context.</param>
    /// <param name="providerName">Name of the provider.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "batch/xml/?providerName={providerName}")]
    [Obsolete]
    void BatchSaveFormDescriptionInXml(FormDescriptionViewModel[] formContext, string providerName);

    /// <summary>
    /// Gets the collection of form entries for specified formId and returns the result in JSON format.
    /// </summary>
    /// <param name="formId">The id of form for which entries/responses will be retrieved.</param>
    /// <param name="sortExpression">The sort expression to be applied.</param>
    /// <param name="skip">Bypasses the number of specified items.</param>
    /// <param name="take">Number of items to be retrieved.</param>
    /// <param name="filter">The filter to be applied.</param>
    /// <returns>A collection context that contains the selected forms.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "entries/{formName}/?providerName={providerName}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&managerType={managerType}&siteId={siteId}")]
    CollectionContext<FormEntry> GetFormEntries(
      string formName,
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string managerType,
      Guid siteId);

    /// <summary>
    /// Gets the collection of form descriptions and returns the result in XML format.
    /// </summary>
    /// <param name="formId">The id of form for which entries/responses will be retrieved.</param>
    /// <param name="sortExpression">The sort expression to be applied.</param>
    /// <param name="skip">Bypasses the number of specified items.</param>
    /// <param name="take">Number of items to be retrieved.</param>
    /// <param name="filter">The filter to be applied.</param>
    /// <returns>A collection context that contains the selected forms.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "entries/xml/{formName}?providerName={providerName}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&managerType={managerType}&siteId={siteId}")]
    [Obsolete]
    CollectionContext<FormEntry> GetFormEntriesInXml(
      string formName,
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string managerType,
      Guid siteId);

    /// <summary>Gets a form entry.</summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">The id of the entry.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemType">Type of the entry.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/entry/{formName}/{entryId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    ContentItemContext<FormEntry> GetFormEntry(
      string formName,
      string entryId,
      string providerName,
      string itemType,
      string managerType);

    /// <summary>Gets the page index of the form entry.</summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">The id of the entry.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="pageSize">Type page size.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/entryindex/{formName}/{entryId}/?providerName={providerName}&sortExpression={sortExpression}&filter={filter}&pageSize={pageSize}")]
    int GetFormEntryPageIndex(
      string formName,
      string entryId,
      string providerName,
      string sortExpression,
      string filter,
      int pageSize);

    /// <summary>Gets a form entry in XML.</summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">The id of the entry.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemType">Type of the entry.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "entry/xml/{formName}/{entryId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    [Obsolete]
    ContentItemContext<FormEntry> GetFormEntryInXml(
      string formName,
      string entryId,
      string providerName,
      string itemType,
      string managerType);

    /// <summary>Gets fields of a form.</summary>
    /// <param name="formId">Id of the form.</param>
    /// <param name="providerName">Name of the provider.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/fields/{formId}/?providerName={providerName}")]
    CollectionContext<FieldViewModel> GetFormFields(
      string formId,
      string providerName);

    /// <summary>Gets rules of a form.</summary>
    /// <param name="formId">Id of the form.</param>
    /// <param name="providerName">Name of the provider.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/rules/{formId}/?providerName={providerName}")]
    string GetFormRules(string formId, string providerName);

    /// <inheritdoc />
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/autocomplete?extender={extenderKey}&term={term}")]
    IEnumerable<string> GetExtenderAutocompleteData(
      string[] paramValues,
      string extenderKey,
      string term);

    /// <summary>
    /// Deletes the form entry and returns true if the form entry has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">Id of the form entry to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the formDescription.</param>
    /// <returns>true if the form entry has been deleted; otherwise false</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/entry/{formName}/{entryId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    bool DeleteFormEntry(
      string formName,
      string entryId,
      string providerName,
      string itemType,
      string managerType);

    /// <summary>
    /// Deletes the form entry and returns true if the form entry has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">Id of the form entry to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the formDescription.</param>
    /// <returns>true if the form entry has been deleted; otherwise false</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/entry/xml/{formName}/{entryId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    [Obsolete]
    bool DeleteFormEntryInXml(
      string formName,
      string entryId,
      string providerName,
      string itemType,
      string managerType);

    /// <summary>
    /// Deletes an array of form entries.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the form entries to be deleted.</param>
    /// <param name="formName">Name of the form.</param>
    /// <param name="providerName">Name of the content provider to be used.</param>
    /// <returns>
    /// true if the form entry has been deleted; otherwise false
    /// </returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "entries/{formName}/batch/?providerName={providerName}")]
    bool BatchDeleteFormEntries(string[] ids, string formName, string providerName);

    /// <summary>
    /// Deletes an array of form entries.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the form entries to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used.</param>
    /// <returns>true if the form entry has been deleted; otherwise false</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "entries/{formName}/xml/batch/?providerName={providerName}")]
    [Obsolete]
    bool BatchDeleteFormEntriesInXml(string[] ids, string formName, string providerName);

    /// <summary>Publishes the the specified form in XML.</summary>
    /// <param name="formId">The form id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/publish/?providerName={providerName}")]
    [Obsolete]
    bool PublishDraftInXml(string formId, string providerName);

    /// <summary>Publishes the draft form.</summary>
    /// <param name="pageDraftId">Id of the form to be published.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/publish/?providerName={providerName}")]
    void PublishForm(string formDraftId, string providerName);

    /// <summary>Publishes the specified forms.</summary>
    /// <param name="formIds">The forms Ids.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/publish/?providerName={providerName}")]
    [WebHelp(Comment = "Publishes the specified forms by their Ids")]
    bool BatchPublishForm(string[] formIds, string providerName);

    /// <summary>Unpublishes a form.</summary>
    /// <param name="formId">Id of the form to be unpublished.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/unpublish/?providerName={providerName}")]
    void UnpublishForm(string formId, string providerName);

    /// <summary>Unpublishes the specified form.</summary>
    /// <param name="pageIDs">The form Ids.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/unpublish/?providerName={providerName}")]
    [WebHelp(Comment = "Unpublishes the specified pages (IDs)")]
    void BatchUnpublishForm(string[] formIds, string providerName);

    /// <summary>Saves a Form entry.</summary>
    /// <param name="entry">The entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/entry/{formName}/{entryId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    ContentItemContext<FormEntry> SaveFormEntry(
      ContentItemContext<FormEntry> entry,
      string formName,
      string entryId,
      string itemType,
      string providerName,
      string managerType);

    /// <summary>Saves a Form entry.</summary>
    /// <param name="entry">The entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/entry/{formName}/{entryId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    [Obsolete]
    ContentItemContext<FormEntry> SaveFormEntryInXml(
      ContentItemContext<FormEntry> entry,
      string formName,
      string entryId,
      string itemType,
      string providerName,
      string managerType);

    /// <summary>
    /// Adds the specified language to the list of supported languages of the specified form.
    /// </summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="languageCode">The language to add.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddLanguage/{languageCode}/?providerName={providerName}")]
    void AddFormLanguageVersion(string formId, string languageCode, string providerName);

    /// <summary>
    /// Adds the specified language to the list of supported languages of the specified form.
    /// </summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="languageCode">The language to add.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/AddLanguage/{languageCode}/?providerName={providerName}")]
    [Obsolete]
    void AddFormLanguageVersionInXml(string formId, string languageCode, string providerName);

    /// <summary>Subscribes a form for form entry event notification.</summary>
    /// <param name="formId">Id of the form to be subscribed.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/subscribe/?providerName={providerName}")]
    void SubscribeForm(string formId, string providerName);

    /// <summary>Subscribes a form for form entry event notification.</summary>
    /// <param name="formId">Id of the form to be subscribed.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/subscribe/?providerName={providerName}")]
    void SubscribeFormInXml(string formId, string providerName);

    /// <summary>Subscribes a form for form entry event notification.</summary>
    /// <param name="formId">Id of the form to be subscribed.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/unsubscribe/?providerName={providerName}")]
    void UnsubscribeForm(string formId, string providerName);

    /// <summary>Subscribes a form for form entry event notification.</summary>
    /// <param name="formId">Id of the form to be subscribed.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/unsubscribe/?providerName={providerName}")]
    void UnsubscribeFormInXml(string formId, string providerName);

    /// <summary>Shares a form across multisite.</summary>
    /// <param name="formId">Id of the form to be shared</param>
    /// <param name="providerName">Provider name of the form</param>
    /// <param name="selectedSites">Site Ids which will be sharing the form.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "share/?providerName={providerName}")]
    void ShareFormDescription(string formId, string providerName, string[] selectedSites);

    /// <summary>Gets the form message template</summary>
    /// <param name="formId">The form id</param>
    /// <param name="language">The language</param>
    /// <param name="providerName">The provider name</param>
    /// <param name="template">The template</param>
    /// <returns>Message template</returns>
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "templates/?formId={formId}&language={language}&providerName={providerName}&template={template}")]
    [OperationContract]
    SystemEmailsViewModel GetFormMessageTemplateViewModel(
      string formId,
      string language,
      string providerName,
      string template);
  }
}
