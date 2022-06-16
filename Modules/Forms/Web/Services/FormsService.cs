// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.Services.FormsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration.Web.ViewModels;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.Forms;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Data;
using Telerik.Sitefinity.Modules.Forms.MessageTemplates;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Forms.Web.Services
{
  /// <summary>REST service for the Forms module</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class FormsService : IFormsService
  {
    internal const string WebServiceUrl = "~/Sitefinity/Services/Forms/FormsService.svc";

    /// <summary>
    /// Gets the collection of forms and returns the result in JSON format.
    /// </summary>
    /// <param name="formFilter">Filter expression to be applied.</param>
    /// <param name="sortExpression">The sort expression to be applied.</param>
    /// <param name="skip">Bypasses the number of specified items.</param>
    /// <param name="take">Number of items to be retrieved.</param>
    /// <param name="filter">The filter to be applied.</param>
    /// <returns>
    /// A collection context that contains the selected forms.
    /// </returns>
    public CollectionContext<FormDescriptionViewModel> GetFormDescriptions(
      string filter,
      string sortExpression,
      int skip,
      int take,
      bool notShared)
    {
      return this.GetFormDescriptionsInternal(filter, sortExpression, skip, take, notShared);
    }

    /// <summary>
    /// Gets the collection of form descriptions and returns the result in JSON format.
    /// </summary>
    /// <param name="sortExpression">The sort expression to be applied.</param>
    /// <param name="skip">Bypasses the number of specified items.</param>
    /// <param name="take">Number of items to be retrieved.</param>
    /// <param name="filter">The filter to be applied.</param>
    /// <returns>
    /// A collection context that contains the selected forms.
    /// </returns>
    public CollectionContext<FormDescriptionViewModel> GetFormDescriptionsInXml(
      string filter,
      string sortExpression,
      int skip,
      int take,
      bool notShared)
    {
      return this.GetFormDescriptionsInternal(filter, sortExpression, skip, take, notShared);
    }

    /// <summary>
    /// Deletes an array of forms.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the forms to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the form.</param>
    /// <returns>true if the form has been deleted; otherwise false</returns>
    public bool BatchDeleteFormDescription(
      string[] ids,
      string providerName,
      string languageToDelete)
    {
      return this.BatchDeleteContentInternal(ids, providerName, languageToDelete);
    }

    /// <summary>
    /// Deletes an array of forms.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the forms to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the form.</param>
    /// <returns>true if the form has been deleted; otherwise false</returns>
    public bool BatchDeleteFormDescriptionInXml(
      string[] ids,
      string providerName,
      string languageToDelete)
    {
      return this.BatchDeleteContentInternal(ids, providerName, languageToDelete);
    }

    /// <summary>
    /// Deletes the form and returns true if the form has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="formId">Id of the form to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the formDescription.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns>true if the form has been deleted; otherwise false</returns>
    public bool DeleteFormDescription(string formId, string providerName, string languageToDelete) => this.DeleteFormInternal(formId, providerName, languageToDelete);

    /// <summary>
    /// Deletes the form and returns true if the form has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="formId">The form id.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the form.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns></returns>
    public bool DeleteFormDescriptionInXml(
      string formId,
      string providerName,
      string languageToDelete)
    {
      return this.DeleteFormInternal(formId, providerName, languageToDelete);
    }

    /// <summary>
    /// Gets the single form description and returs it in JSON format.
    /// </summary>
    /// <param name="formId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content item to be retrieved.
    /// </returns>
    public FormDescriptionViewModelContext GetFormDescription(
      string formId,
      string providerName,
      bool duplicate)
    {
      return this.GetFormDescriptionInternal(formId, providerName);
    }

    /// <summary>
    /// Gets the single form description and returs it in XML format.
    /// </summary>
    /// <param name="formId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public FormDescriptionViewModelContext GetFormDescriptionInXml(
      string formId,
      string providerName,
      bool duplicate)
    {
      return this.GetFormDescriptionInternal(formId, providerName);
    }

    /// <summary>Saves the form description in JSON format.</summary>
    /// <param name="formContext">The form description context.</param>
    /// <param name="formId">The form id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns>
    /// An instance context of type <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FormDescriptionViewModelContext" /> that contains the form with specified id at formId parameter.
    /// </returns>
    public FormDescriptionViewModelContext SaveFormDescription(
      FormDescriptionViewModelContext formContext,
      string formId,
      string providerName,
      bool duplicate)
    {
      return this.SaveFormDescriptionInternal(formContext, formId, providerName, duplicate);
    }

    /// <summary>Saves the form description in XML format.</summary>
    /// <param name="formContext">The form description context.</param>
    /// <param name="formId">The form id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="duplicate">if set to <c>true</c> the form will be duplicated.</param>
    /// <returns>
    /// An instance context of type <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.Services.Model.FormDescriptionViewModelContext" /> that contains the form with specified id at formId parameter.
    /// </returns>
    public FormDescriptionViewModelContext SaveFormDescriptionInXml(
      FormDescriptionViewModelContext formContext,
      string formId,
      string providerName,
      bool duplicate)
    {
      return this.SaveFormDescriptionInternal(formContext, formId, providerName, duplicate);
    }

    /// <summary>Batch saving form description in XML format.</summary>
    /// <param name="formContext">The form description context.</param>
    /// <param name="providerName">Name of the provider.</param>
    public void BatchSaveFormDescription(
      FormDescriptionViewModel[] formContext,
      string providerName)
    {
      throw new NotImplementedException();
    }

    /// <summary>Batch saving forms in XML.</summary>
    /// <param name="formContext">The form description context.</param>
    /// <param name="providerName">Name of the provider.</param>
    public void BatchSaveFormDescriptionInXml(
      FormDescriptionViewModel[] formContext,
      string providerName)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the collection of form entries for specified formId and returns the result in JSON format.
    /// </summary>
    /// <param name="formId">The id of form for which entries/responses will be retrieved.</param>
    /// <param name="sortExpression">The sort expression to be applied.</param>
    /// <param name="skip">Bypasses the number of specified items.</param>
    /// <param name="take">Number of items to be retrieved.</param>
    /// <param name="filter">The filter to be applied.</param>
    /// <returns>
    /// A collection context that contains the selected forms.
    /// </returns>
    public CollectionContext<FormEntry> GetFormEntries(
      string formName,
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string managerType,
      Guid siteId)
    {
      return this.GetFormEntriesInternal(formName, providerName, sortExpression, skip, take, filter, siteId);
    }

    /// <summary>Gets the page index of the form entry.</summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">The id of the entry.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="pageSize">Type page size.</param>
    /// <returns></returns>
    public int GetFormEntryPageIndex(
      string formName,
      string entryId,
      string providerName,
      string sortExpression,
      string filter,
      int pageSize)
    {
      return this.GetFormEntryPageIndexInternal(formName, entryId, providerName, sortExpression, filter, pageSize);
    }

    /// <summary>
    /// Gets the collection of form descriptions and returns the result in XML format.
    /// </summary>
    /// <param name="formId">The id of form for which entries/responses will be retrieved.</param>
    /// <param name="sortExpression">The sort expression to be applied.</param>
    /// <param name="skip">Bypasses the number of specified items.</param>
    /// <param name="take">Number of items to be retrieved.</param>
    /// <param name="filter">The filter to be applied.</param>
    /// <returns>
    /// A collection context that contains the selected forms.
    /// </returns>
    public CollectionContext<FormEntry> GetFormEntriesInXml(
      string formName,
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string managerType,
      Guid siteId)
    {
      return this.GetFormEntriesInternal(formName, providerName, sortExpression, skip, take, filter, siteId);
    }

    /// <summary>Gets a form entry.</summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">The id of the entry.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemType">Type of the entry.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <returns></returns>
    public ContentItemContext<FormEntry> GetFormEntry(
      string formName,
      string entryId,
      string providerName,
      string itemType,
      string managerType)
    {
      return this.GetFormEntryInternal(formName, entryId, providerName, itemType, managerType);
    }

    /// <summary>Gets a form entry in XML.</summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">The id of the entry.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemType">Type of the entry.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <returns></returns>
    public ContentItemContext<FormEntry> GetFormEntryInXml(
      string formName,
      string entryId,
      string providerName,
      string itemType,
      string managerType)
    {
      return this.GetFormEntryInternal(formName, entryId, providerName, itemType, managerType);
    }

    /// <summary>Saves a Form entry.</summary>
    /// <param name="entry">The entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public ContentItemContext<FormEntry> SaveFormEntry(
      ContentItemContext<FormEntry> entry,
      string formName,
      string entryId,
      string itemType,
      string providerName,
      string managerType)
    {
      return this.SaveFormEntryInternal(entry, entryId, formName, itemType, providerName);
    }

    /// <summary>Saves a Form entry.</summary>
    /// <param name="entry">The entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public ContentItemContext<FormEntry> SaveFormEntryInXml(
      ContentItemContext<FormEntry> entry,
      string formName,
      string entryId,
      string itemType,
      string providerName,
      string managerType)
    {
      return this.SaveFormEntryInternal(entry, entryId, formName, itemType, providerName);
    }

    /// <summary>Gets fields of a form.</summary>
    /// <param name="formId">Id of the form.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public CollectionContext<FieldViewModel> GetFormFields(
      string formId,
      string providerName)
    {
      return this.GetFormFieldsInternal(formId, providerName);
    }

    /// <summary>Gets rules of a form.</summary>
    /// <param name="formId">Id of the form.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>Rules for this form.</returns>
    public string GetFormRules(string formId, string providerName)
    {
      FormsManager manager = FormsManager.GetManager(providerName);
      Guid formIdGuid = Guid.Parse(formId);
      FormDescription formDescription = manager.GetForms().FirstOrDefault<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Id == formIdGuid));
      if (formDescription != null)
        return formDescription.Rules;
      FormDraft formDraft = manager.GetDrafts().FirstOrDefault<FormDraft>((Expression<Func<FormDraft, bool>>) (d => d.Id == formIdGuid));
      return formDraft != null ? formDraft.Rules : string.Empty;
    }

    public IEnumerable<string> GetExtenderAutocompleteData(
      string[] paramValues,
      string extenderKey,
      string term)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      IEnumerable<string> autocompleteData = Enumerable.Empty<string>();
      ConnectorDataMappingExtender dataMappingExtender = ObjectFactory.Container.ResolveAll<ConnectorDataMappingExtender>().FirstOrDefault<ConnectorDataMappingExtender>((Func<ConnectorDataMappingExtender, bool>) (ext => ext.Key == extenderKey));
      if (dataMappingExtender != null)
        autocompleteData = dataMappingExtender.GetAutocompleteData(term, paramValues);
      ServiceUtility.DisableCache();
      return autocompleteData;
    }

    /// <summary>
    /// Deletes the form entry and returns true if the form entry has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">Id of the form entry to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the formDescription.</param>
    /// <param name="itemType"></param>
    /// <param name="managerType"></param>
    /// <returns>
    /// true if the form entry has been deleted; otherwise false
    /// </returns>
    public bool DeleteFormEntry(
      string formName,
      string entryId,
      string providerName,
      string itemType,
      string managerType)
    {
      return this.DeleteFormEntryInternal(formName, entryId, providerName);
    }

    /// <summary>
    /// Deletes the form entry and returns true if the form entry has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="entryId">Id of the form entry to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the formDescription.</param>
    /// <param name="itemType"></param>
    /// <param name="managerType"></param>
    /// <returns>
    /// true if the form entry has been deleted; otherwise false
    /// </returns>
    public bool DeleteFormEntryInXml(
      string formName,
      string entryId,
      string providerName,
      string itemType,
      string managerType)
    {
      return this.DeleteFormEntryInternal(formName, entryId, providerName);
    }

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
    public bool BatchDeleteFormEntries(string[] ids, string formName, string providerName) => this.BatchDeleteFormEntiesInternal(ids, formName, providerName);

    /// <summary>
    /// Deletes an array of form entries.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the form entries to be deleted.</param>
    /// <param name="formName"></param>
    /// <param name="providerName">Name of the content provider to be used.</param>
    /// <returns>
    /// true if the form entry has been deleted; otherwise false
    /// </returns>
    public bool BatchDeleteFormEntriesInXml(string[] ids, string formName, string providerName) => this.BatchDeleteFormEntiesInternal(ids, formName, providerName);

    /// <summary>
    /// Publishes the current draft of the specified form. The result is returned as JSON.
    /// </summary>
    /// <param name="pageNodeId">The page draft pageId.</param>
    public void PublishForm(string formId, string providerName) => this.PublishFormInternal(formId, providerName, true);

    /// <summary>
    /// Publishes the current draft of the specified form. The result is returned as XML.
    /// </summary>
    /// <param name="formId">The form id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public bool PublishDraftInXml(string formId, string providerName) => this.PublishFormInternal(formId, providerName, true);

    /// <summary>Unpublishes a form.</summary>
    /// <param name="formDescriptionId">The form id.</param>
    public void UnpublishForm(string formDescriptionId, string providerName) => this.UnpublishFormInternal(formDescriptionId, providerName, true);

    /// <summary>Unpublishes multiple pages at once.</summary>
    /// <param name="pageIDs">The page Ids.</param>
    /// <returns></returns>
    public void BatchUnpublishForm(string[] formIDs, string providerName) => this.BatchUnpublishFormInternal(formIDs, providerName);

    /// <summary>Publishes the specified forms.</summary>
    /// <param name="formIds">The forms Ids.</param>
    public bool BatchPublishForm(string[] formIds, string providerName) => this.BatchPublishFormInternal(formIds, providerName);

    /// <summary>
    /// Adds the specified language to the list of supported languages of the specified form.
    /// </summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="languageCode">The language to add.</param>
    /// <param name="providerName"></param>
    public void AddFormLanguageVersion(string formId, string languageCode, string providerName) => this.AddFormLanguageVersionInternal(formId, languageCode, providerName);

    /// <summary>
    /// Adds the specified language to the list of supported languages of the specified form.
    /// </summary>
    /// <param name="formName">Name of the form.</param>
    /// <param name="languageCode">The language to add.</param>
    /// <param name="providerName"></param>
    public void AddFormLanguageVersionInXml(
      string formId,
      string languageCode,
      string providerName)
    {
      this.AddFormLanguageVersionInternal(formId, languageCode, providerName);
    }

    public void ShareFormDescription(string formId, string providerName, string[] selectedSites) => this.ShareFormDescriptionInternal(formId, providerName, selectedSites);

    /// <summary>Gets the form message template</summary>
    /// <param name="formId">The form id</param>
    /// <param name="language">The language</param>
    /// <param name="providerName">The provider name</param>
    /// <param name="template">The template</param>
    /// <returns>Message template</returns>
    public SystemEmailsViewModel GetFormMessageTemplateViewModel(
      string formId,
      string language,
      string providerName,
      string template)
    {
      if (template == "NewFormResponseMessageTemplate")
        return this.ResolveMessageTemplateViewModel(formId, language, providerName, (IActionMessageTemplate) new NewFormResponseMessageTemplate());
      if (template == "ModifiedFormResponseMessageTemplate")
        return this.ResolveMessageTemplateViewModel(formId, language, providerName, (IActionMessageTemplate) new ModifiedFormResponseMessageTemplate());
      if (template == "FormConfirmationMessageTemplate")
        return this.ResolveMessageTemplateViewModel(formId, language, providerName, (IActionMessageTemplate) new FormConfirmationMessageTemplate());
      throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid template.", (Exception) null);
    }

    private static User GetCurrentUser()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return UserManager.GetManager(currentIdentity.MembershipProvider).GetUser(currentIdentity.Name);
    }

    private SystemEmailsViewModel ResolveMessageTemplateViewModel(
      string formId,
      string language,
      string providerName,
      IActionMessageTemplate actionMessageTemplate)
    {
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      INotificationService notificationService = SystemManager.GetNotificationService();
      actionMessageTemplate.ApplyVariations(("parentid", formId), (nameof (language), language));
      foreach (PlaceholderField placeholderField in this.GetFormPlaceholderFields(formId, providerName, language))
        actionMessageTemplate.AddDynamicPlaceholderField(placeholderField);
      IMessageTemplateResponse systemMessageTemplate1 = notificationService.GetSystemMessageTemplate(serviceContext, actionMessageTemplate.GetKey());
      SystemEmailsViewModel systemEmailsViewModel = new SystemEmailsViewModel(actionMessageTemplate, systemMessageTemplate1);
      actionMessageTemplate.ApplyVariations(("parentid", (string) null), (nameof (language), (string) null), ("siteid", SystemManager.CurrentContext.CurrentSite.Id.ToString()));
      IMessageTemplateRequest systemMessageTemplate2 = (IMessageTemplateRequest) notificationService.GetSystemMessageTemplate(serviceContext, actionMessageTemplate.GetKey());
      if (systemMessageTemplate2 == null)
      {
        actionMessageTemplate.ApplyVariations(("siteid", (string) null));
        systemMessageTemplate2 = (IMessageTemplateRequest) notificationService.GetSystemMessageTemplate(serviceContext, actionMessageTemplate.GetKey());
      }
      IMessageTemplateRequest messageTemplateRequest = systemMessageTemplate2 ?? actionMessageTemplate.GetDefaultMessageTemplate();
      systemEmailsViewModel.Subject = messageTemplateRequest.Subject;
      systemEmailsViewModel.BodyHtml = messageTemplateRequest.BodyHtml;
      if (!messageTemplateRequest.TemplateSenderEmailAddress.IsNullOrEmpty())
        systemEmailsViewModel.SenderEmailAddress = messageTemplateRequest.TemplateSenderEmailAddress;
      if (!messageTemplateRequest.TemplateSenderName.IsNullOrEmpty())
        systemEmailsViewModel.SenderName = messageTemplateRequest.TemplateSenderName;
      return systemEmailsViewModel;
    }

    private IEnumerable<PlaceholderField> GetFormPlaceholderFields(
      string formId,
      string providerName,
      string language)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      List<PlaceholderField> placeholderFields = new List<PlaceholderField>();
      FormsManager manager = FormsManager.GetManager(providerName);
      FormDraft formDraft = manager.GetForm(Guid.Parse(formId)).Drafts.Where<FormDraft>((Func<FormDraft, bool>) (p => p.IsTempDraft)).FirstOrDefault<FormDraft>();
      IControlBehaviorResolver behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
      foreach (Telerik.Sitefinity.Pages.Model.ControlData sortFormControl in OpenAccessFormsProvider.SortFormControls((IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>) formDraft.Controls))
      {
        Control control = manager.LoadControl((ObjectData) sortFormControl, !string.IsNullOrEmpty(language) ? CultureInfo.GetCultureInfo(language) : (CultureInfo) null);
        if (behaviorResolver.GetBehaviorObject(control) is IFormFieldControl behaviorObject)
        {
          IFieldControl fieldControl = behaviorObject as IFieldControl;
          string fieldName;
          string str = fieldName = sortFormControl.Caption + control.ID;
          ControlProperty controlProperty = sortFormControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Title"));
          if (controlProperty != null)
            str = controlProperty.Value;
          else if (!string.IsNullOrEmpty(behaviorObject.MetaField.Title))
            str = behaviorObject.MetaField.Title;
          else if (fieldControl != null && !string.IsNullOrEmpty(fieldControl.Title))
            str = fieldControl.Title;
          else if (!string.IsNullOrEmpty(behaviorObject.MetaField.FieldName))
            str = behaviorObject.MetaField.FieldName;
          string title = str;
          PlaceholderField placeholderField = FormsModule.GetControlPlaceholderField(fieldName, title);
          placeholderFields.Add(placeholderField);
        }
      }
      return (IEnumerable<PlaceholderField>) placeholderFields;
    }

    private FormDescriptionViewModelContext GetFormDescriptionInternal(
      string formId,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      FormDescriptionViewModel descriptionViewModel = new FormDescriptionViewModel(App.WorkWith().Forms().Form(new Guid(formId)).FormDescription);
      FormDescriptionViewModelContext descriptionInternal = new FormDescriptionViewModelContext();
      descriptionInternal.Item = descriptionViewModel;
      ServiceUtility.DisableCache();
      return descriptionInternal;
    }

    private CollectionContext<FormDescriptionViewModel> GetFormDescriptionsInternal(
      string filter,
      string sortExpression,
      int skip,
      int take,
      bool notShared)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      FormsManager manager = FormsManager.GetManager();
      int? totalCount = new int?(0);
      IQueryable<FormDescription> queryable1;
      if (filter == "[NotShared]")
      {
        queryable1 = manager.GetNotSharedForms();
        totalCount = new int?(queryable1.Count<FormDescription>());
      }
      else
      {
        IQueryable<FormDescription> queryable2 = manager.GetForms();
        CultureInfo culture = (CultureInfo) null;
        CommonMethods.MatchCultureFilter(ref filter, out culture);
        string filterName = (string) null;
        if (NamedFiltersHandler.TryParseFilterName(filter, out filterName))
        {
          queryable2 = NamedFiltersHandler.ApplyFilter<FormDescription>(queryable2, filterName, culture, (string) null);
          filter = (string) null;
        }
        sortExpression = !string.IsNullOrEmpty(sortExpression) ? sortExpression : "DateCreated";
        if (!notShared)
        {
          filter = ContentHelper.AdaptMultilingualFilterExpressionRaw(filter, culture);
          IQueryable<FormDescription> queryable3 = DataProviderBase.SetExpressions<FormDescription>(queryable2, filter, sortExpression, new int?(), new int?(), ref totalCount);
          IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
          if (multisiteContext != null)
            queryable3 = ((IMultisiteEnabledOAProvider) manager.Provider).FilterBySite<FormDescription>(queryable3, multisiteContext.CurrentSite.Id);
          queryable1 = DataProviderBase.SetExpressions<FormDescription>(queryable3, (string) null, (string) null, new int?(skip), new int?(take), ref totalCount);
          if (take == 0 && totalCount.HasValue)
            queryable1 = queryable1.Take<FormDescription>(totalCount.Value);
        }
        else
        {
          filter = ContentHelper.AdaptMultilingualFilterExpressionRaw(filter, culture);
          queryable1 = DataProviderBase.SetExpressions<FormDescription>(queryable2, filter, sortExpression, new int?(skip), new int?(take), ref totalCount);
        }
      }
      List<FormDescriptionViewModel> descriptionViewModelList = new List<FormDescriptionViewModel>();
      this.PopulateFormDescriptionsCount((IEnumerable<FormDescription>) queryable1.Include<FormDescription>((Expression<Func<FormDescription, object>>) (x => x.Permissions)), (IList<FormDescriptionViewModel>) descriptionViewModelList);
      ServiceUtility.DisableCache();
      return new CollectionContext<FormDescriptionViewModel>((IEnumerable<FormDescriptionViewModel>) descriptionViewModelList)
      {
        TotalCount = totalCount.Value
      };
    }

    private void PopulateFormDescriptionsCount(
      IEnumerable<FormDescription> forms,
      IList<FormDescriptionViewModel> formDescriptionsList)
    {
      FormsManager manager = FormsManager.GetManager();
      User currentUser = FormsService.GetCurrentUser();
      foreach (FormDescription form1 in forms)
      {
        FormDescription form = form1;
        FormDescriptionViewModel descriptionViewModel = new FormDescriptionViewModel(form);
        descriptionViewModel.HasSubscription = FormsManager.CheckUserIsSubscribed(currentUser, form.Id);
        descriptionViewModel.HasSubscriptionAfterFormUpdate = FormsManager.CheckUserIsSubscribed(currentUser, (Func<Guid>) (() => form.SubscriptionListIdAfterFormUpdate));
        if (form.Status == ContentLifecycleStatus.Live)
        {
          if (form.IsGranted("Forms", "ViewResponses"))
          {
            IQueryable<FormEntry> formEntries = manager.GetFormEntries(form);
            descriptionViewModel.EntriesCount = formEntries.Count<FormEntry>();
          }
        }
        formDescriptionsList.Add(descriptionViewModel);
      }
    }

    private FormDescriptionViewModelContext SaveFormDescriptionInternal(
      FormDescriptionViewModelContext formDescriptionContext,
      string formDescriptionId,
      string providerName,
      bool duplicate)
    {
      FormDescriptionViewModel formDescription = formDescriptionContext.Item;
      try
      {
        ServiceUtility.RequestBackendUserAuthentication();
        FormsManager manager = FormsManager.GetManager(providerName);
        string name = duplicate ? formDescription.DuplicateName : formDescription.Name;
        Guid id = formDescription.Id;
        if (id == Guid.Empty | duplicate)
          manager.ValidateConstraints(name, id, duplicate);
        if (duplicate)
          return this.DuplicateForm(formDescriptionContext);
        using (FluentSitefinity fluentSitefinity = App.WorkWith())
        {
          FormFacade formFacade = !(formDescription.Id == Guid.Empty) ? fluentSitefinity.Forms().Form(formDescription.Id) : fluentSitefinity.Forms().Form().CreateNew(formDescription.Name).SetFramework(formDescription.Framework);
          if (formDescription.SourceLanguageObjectId == Guid.Empty)
            formFacade.Do((System.Action<FormDescription>) (p => p.UrlName = (Lstring) formDescription.UrlName));
          formFacade.Do((System.Action<FormDescription>) (p =>
          {
            p.Title = (Lstring) formDescription.Title;
            if (formDescription.Attributes == null)
              return;
            foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) formDescription.Attributes.Dictionary)
              p.Attributes[keyValuePair.Key] = keyValuePair.Value;
          }));
          formFacade.SaveChanges();
          FormDescription formDescription1 = formFacade.FormDescription;
          ServiceUtility.DisableCache();
          return new FormDescriptionViewModelContext()
          {
            Item = new FormDescriptionViewModel(formDescription1)
          };
        }
      }
      catch (DuplicateKeyException ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<FormsResources>().DuplicateTitleException, (object) formDescription.Name), (Exception) null);
      }
    }

    private bool BatchDeleteContentInternal(
      string[] ids,
      string providerName,
      string languageToDelete)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      CultureInfo culture = (CultureInfo) null;
      if (!string.IsNullOrEmpty(languageToDelete) && SystemManager.CurrentContext.AppSettings.Multilingual)
        culture = CultureInfo.GetCultureInfo(languageToDelete);
      using (FluentSitefinity fluentSitefinity = App.WorkWith())
      {
        foreach (string id in ids)
          fluentSitefinity.Forms().Form(new Guid(id)).Delete(culture);
        fluentSitefinity.SaveChanges();
      }
      ServiceUtility.DisableCache();
      return true;
    }

    private bool DeleteFormInternal(string formId, string providerName, string languageToDelete)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      CultureInfo culture = (CultureInfo) null;
      if (!string.IsNullOrEmpty(languageToDelete) && SystemManager.CurrentContext.AppSettings.Multilingual)
        culture = CultureInfo.GetCultureInfo(languageToDelete);
      using (FluentSitefinity fluentSitefinity = App.WorkWith())
      {
        fluentSitefinity.Forms().Form(new Guid(formId)).Delete(culture);
        fluentSitefinity.SaveChanges();
      }
      ServiceUtility.DisableCache();
      return true;
    }

    private int GetFormEntryPageIndexInternal(
      string formName,
      string entryId,
      string providerName,
      string sortExpression,
      string filter,
      int pageSize)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid formEntryId = Guid.Parse(entryId);
      FormsManager manager = FormsManager.GetManager(providerName);
      string entryType = string.Format("{0}.{1}", (object) manager.Provider.FormsNamespace, (object) formName);
      int? nullable1 = new int?(0);
      bool flag = false;
      int pageIndexInternal = 0;
      if (pageSize == 0)
        pageSize = 50;
      try
      {
        IEnumerable<FormEntry> formEntries = (IEnumerable<FormEntry>) manager.GetFormEntries(entryType);
        IEnumerable<FormEntry> source = !(sortExpression == "SubmittedOn ASC") ? (IEnumerable<FormEntry>) formEntries.OrderByDescending<FormEntry, DateTime>((Func<FormEntry, DateTime>) (x => x.SubmittedOn)) : (IEnumerable<FormEntry>) formEntries.OrderBy<FormEntry, DateTime>((Func<FormEntry, DateTime>) (x => x.SubmittedOn));
        int? nullable2 = new int?(source.Count<FormEntry>());
        while (!flag)
        {
          flag = source.Skip<FormEntry>(pageIndexInternal * pageSize).Take<FormEntry>(pageSize).Any<FormEntry>((Func<FormEntry, bool>) (fe => fe.Id.Equals(formEntryId)));
          if (!flag)
          {
            ++pageIndexInternal;
            int num = pageIndexInternal * pageSize;
            int? nullable3 = nullable2;
            int valueOrDefault = nullable3.GetValueOrDefault();
            if (num > valueOrDefault & nullable3.HasValue)
            {
              pageIndexInternal = -1;
              break;
            }
          }
          else
            break;
        }
      }
      catch (Exception ex)
      {
        pageIndexInternal = -1;
      }
      ServiceUtility.DisableCache();
      return pageIndexInternal;
    }

    private CollectionContext<FormEntry> GetFormEntriesInternal(
      string formName,
      string providerName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      Guid siteId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      FormsManager formsManager = FormsManager.GetManager(providerName);
      string entryTypeName = string.Format("{0}.{1}", (object) formsManager.Provider.FormsNamespace, (object) formName);
      System.Type entryType = formsManager.GetEntryType(entryTypeName);
      int? count = new int?(0);
      IEnumerable<FormEntry> formEntries;
      try
      {
        if (sortExpression.IsNullOrEmpty())
          sortExpression = "SubmittedOn ASC";
        formEntries = formsManager.GetFormEntries(entryType, filter, sortExpression, skip, take, ref count).Cast<FormEntry>();
        if (siteId != Guid.Empty)
          formEntries = formEntries.Where<FormEntry>((Func<FormEntry, bool>) (item => item.SourceSiteId == siteId));
      }
      catch (Exception ex)
      {
        formEntries = (IEnumerable<FormEntry>) new List<FormEntry>();
      }
      ServiceUtility.DisableCache();
      formEntries.ToList<FormEntry>().ForEach((System.Action<FormEntry>) (entry => formsManager.ResolveFormEntrySourceSiteName(entry)));
      return new CollectionContext<FormEntry>(formEntries)
      {
        TotalCount = count.Value
      };
    }

    private ContentItemContext<FormEntry> GetFormEntryInternal(
      string formName,
      string entryId,
      string providerName,
      string itemType,
      string managerType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      FormsManager manager = FormsManager.GetManager(providerName);
      FormEntry formEntry = manager.GetFormEntry(string.Format("{0}.{1}", (object) manager.Provider.FormsNamespace, (object) formName), new Guid(entryId));
      manager.ResolveFormEntrySourceSiteName(formEntry);
      ServiceUtility.DisableCache();
      ContentItemContext<FormEntry> formEntryInternal = new ContentItemContext<FormEntry>();
      formEntryInternal.Item = formEntry;
      return formEntryInternal;
    }

    private CollectionContext<FieldViewModel> GetFormFieldsInternal(
      string formId,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      List<FieldViewModel> fieldViewModelList = new List<FieldViewModel>();
      FormsManager manager = FormsManager.GetManager(providerName);
      FormDescription form = manager.GetForm(new Guid(formId));
      IControlBehaviorResolver behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
      foreach (Telerik.Sitefinity.Pages.Model.ControlData sortFormControl in OpenAccessFormsProvider.SortFormControls((IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>) form.Controls))
      {
        Control control = manager.LoadControl((ObjectData) sortFormControl, (CultureInfo) null);
        if (behaviorResolver.GetBehaviorObject(control) is IFormFieldControl behaviorObject)
        {
          string fieldName = behaviorObject.MetaField.FieldName;
          string title = fieldName;
          ControlProperty controlProperty = sortFormControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Title"));
          if (controlProperty != null)
            title = controlProperty.Value;
          else if (!string.IsNullOrEmpty(behaviorObject.MetaField.Title))
            title = behaviorObject.MetaField.Title;
          FieldViewModel fieldViewModel = new FieldViewModel(title, fieldName);
          fieldViewModelList.Add(fieldViewModel);
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<FieldViewModel>((IEnumerable<FieldViewModel>) fieldViewModelList)
      {
        TotalCount = fieldViewModelList.Count<FieldViewModel>()
      };
    }

    private ContentItemContext<FormEntry> SaveFormEntryInternal(
      ContentItemContext<FormEntry> entry,
      string entryId,
      string formName,
      string itemType,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      FormsManager formsManager = FormsManager.GetManager(providerName);
      Guid result;
      if (Guid.TryParse(entryId, out result) && result == Guid.Empty)
      {
        using (FluentSitefinity fluentSitefinity = App.WorkWith())
        {
          fluentSitefinity.Forms().Form(formName).Do((System.Action<FormDescription>) (fd =>
          {
            entry.Item.ReferralCode = formsManager.Provider.GetNextReferralCode(entry.ItemType).ToString();
            entry.Item.SourceSiteDisplayName = formsManager.ResolveFormEntrySourceSiteName(entry.Item);
          }));
          fluentSitefinity.SaveChanges();
        }
        entry.Item.SubmittedOn = DateTime.UtcNow;
        entry.Item.UserId = ClaimsManager.GetCurrentIdentity().UserId;
        entry.Item.UserProvider = ClaimsManager.GetCurrentIdentity().MembershipProvider;
        this.SetEntryIpAddress(entry.Item);
      }
      else
      {
        FormDescription formByName = formsManager.GetFormByName(formName);
        if (formByName != null && !formsManager.Provider.SuppressSecurityChecks)
        {
          if (!formByName.IsGranted("Forms", "ManageResponses"))
            throw new UnauthorizedAccessException(string.Format("You are not authorized for '{0}' permissions!", (object) "ManageResponses"));
        }
      }
      formsManager.SaveChanges();
      ServiceUtility.DisableCache();
      ContentItemContext<FormEntry> contentItemContext = new ContentItemContext<FormEntry>();
      contentItemContext.Item = entry.Item;
      return contentItemContext;
    }

    private void SetEntryIpAddress(FormEntry entry)
    {
      if (!string.IsNullOrEmpty(entry.IpAddress))
        return;
      MessageProperties messageProperties = OperationContext.Current.IncomingMessageProperties;
      if (messageProperties == null || !(messageProperties[RemoteEndpointMessageProperty.Name] is RemoteEndpointMessageProperty endpointMessageProperty))
        return;
      entry.IpAddress = endpointMessageProperty.Address;
    }

    private bool DeleteFormEntryInternal(string formName, string entryId, string providerName) => this.BatchDeleteFormEntiesInternal(new string[1]
    {
      entryId
    }, formName, providerName);

    private bool BatchDeleteFormEntiesInternal(string[] ids, string formName, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (ids.Length == 0)
        return false;
      List<Guid> guidList = new List<Guid>(ids.Length);
      foreach (string id in ids)
        guidList.Add(new Guid(id));
      Guid[] formEntriesIdsArray = guidList.ToArray();
      FormsManager manager = FormsManager.GetManager(providerName);
      IQueryable<FormEntry> formEntries = manager.GetFormEntries(string.Format("{0}.{1}", (object) manager.Provider.FormsNamespace, (object) formName));
      Expression<Func<FormEntry, bool>> predicate = (Expression<Func<FormEntry, bool>>) (fe => formEntriesIdsArray.Contains<Guid>(fe.Id));
      foreach (FormEntry entry in (IEnumerable<FormEntry>) formEntries.Where<FormEntry>(predicate))
        manager.Delete(entry);
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private bool PublishFormInternal(string formId, string providerName, bool saveChanges)
    {
      FormsManager manager = FormsManager.GetManager(providerName);
      try
      {
        return this.PublishFormInternal(manager, formId, saveChanges);
      }
      catch (Exception ex)
      {
        manager.CancelChanges();
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
    }

    private bool PublishFormInternal(FormsManager manager, string formId, bool saveChanges)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid formId1 = formId != null ? new Guid(formId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid request format.", (Exception) null);
      FormDescription form = manager.GetForm(formId1);
      int num = manager.PublishForm(form, SystemManager.CurrentContext.Culture) ? 1 : 0;
      if (num == 0)
        return num != 0;
      if (!saveChanges)
        return num != 0;
      manager.SaveChanges(true);
      return num != 0;
    }

    private void UnpublishFormInternal(string formId, string providerName, bool saveChanges)
    {
      FormsManager manager = FormsManager.GetManager(providerName);
      try
      {
        this.UnpublishFormInternal(manager, formId, saveChanges);
      }
      catch (Exception ex)
      {
        manager.CancelChanges();
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
    }

    private void UnpublishFormInternal(
      FormsManager manager,
      string formDescriptionId,
      bool saveChanges)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid formId = formDescriptionId != null ? new Guid(formDescriptionId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid request format.", (Exception) null);
      manager.UnpublishForm(formId);
      if (!saveChanges)
        return;
      manager.SaveChanges();
    }

    private bool BatchPublishFormInternal(string[] formIds, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      FormsManager formManager = FormsManager.GetManager(providerName);
      ((IEnumerable<string>) formIds).ToList<string>().ForEach((System.Action<string>) (id => this.PublishFormInternal(formManager, id, false)));
      formManager.SaveChanges(true);
      return true;
    }

    private bool BatchUnpublishFormInternal(string[] formIds, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      FormsManager formManager = FormsManager.GetManager(providerName);
      ((IEnumerable<string>) formIds).ToList<string>().ForEach((System.Action<string>) (id => this.UnpublishFormInternal(formManager, id, false)));
      formManager.SaveChanges();
      return true;
    }

    private void AddFormLanguageVersionInternal(
      string formIdString,
      string languageCode,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      FormsManager manager = FormsManager.GetManager(providerName);
      FormDescription form = manager.GetForm(new Guid(formIdString));
      form.Title.SetString(new CultureInfo(languageCode), form.Title.GetString(CultureInfo.InvariantCulture, true));
      manager.SaveChanges();
    }

    private FormDescriptionViewModelContext DuplicateForm(
      FormDescriptionViewModelContext formContext)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      FormFacade formFacade = App.WorkWith().Forms().Form(formContext.Item.Id).Duplicate(formContext.Item.DuplicateName);
      FormDescription form = formFacade.Get();
      form.Title = (Lstring) formContext.Item.DuplicateTitle;
      formContext.Item.Attributes.CopyTo(form.Attributes);
      form.Framework = formContext.Item.Framework;
      formFacade.SaveChanges();
      FormDescriptionViewModel descriptionViewModel = new FormDescriptionViewModel(form);
      return new FormDescriptionViewModelContext()
      {
        Item = descriptionViewModel
      };
    }

    private void ShareFormDescriptionInternal(
      string formId,
      string providerName,
      string[] selectedSites)
    {
      IEnumerable<Guid> sites = ((IEnumerable<string>) selectedSites).Select<string, Guid>((Func<string, Guid>) (s => Guid.Parse(s)));
      Guid formDescriptionId = Guid.Parse(formId);
      App.WorkWith().Forms().Form(formDescriptionId).Share(sites).SaveChanges();
    }

    private bool IsSingleTaxon(System.Type type, string propertyName) => (TypeDescriptor.GetProperties(type)[propertyName] as TaxonomyPropertyDescriptor).MetaField.IsSingleTaxon;

    private IList<string> GetTaxaIds(FormDescription form, System.Type type, string propertyName)
    {
      List<string> taxaIds = new List<string>();
      if (this.IsSingleTaxon(type, propertyName))
      {
        taxaIds.Add(form.GetValue<Guid>(propertyName).ToString());
      }
      else
      {
        IList<Guid> source = form.GetValue<IList<Guid>>(propertyName);
        taxaIds.AddRange((IEnumerable<string>) source.Select<Guid, string>((Func<Guid, string>) (f => f.ToString())).ToArray<string>());
      }
      return (IList<string>) taxaIds;
    }

    /// <inheritdoc />
    public void SubscribeForm(string formId, string providerName) => this.SubscribeFormInternal(formId, providerName);

    /// <inheritdoc />
    public void SubscribeFormInXml(string formId, string providerName) => this.SubscribeFormInternal(formId, providerName);

    /// <inheritdoc />
    public void UnsubscribeForm(string formId, string providerName) => this.UnsubscribeFormInternal(formId, providerName);

    /// <inheritdoc />
    public void UnsubscribeFormInXml(string formId, string providerName) => this.UnsubscribeFormInternal(formId, providerName);

    private void SubscribeFormInternal(string formId, string providerName) => FormsManager.SubscribeUser(FormsService.GetCurrentUser(), new Guid(formId), providerName);

    private void UnsubscribeFormInternal(string formId, string providerName) => FormsManager.UnsubscribeUser(FormsService.GetCurrentUser(), new Guid(formId), providerName);
  }
}
