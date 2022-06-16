// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Forms.Configuration;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Operations;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>
  /// Provides API for managing form definitions and working corresponding form data types
  /// </summary>
  [ModuleId("A64410F7-2F1E-4068-81D0-E28D864DE323")]
  public class FormsManager : 
    ControlManager<FormsDataProvider>,
    ILifecycleManagerForms,
    ILifecycleManager<FormDescription, FormDraft>,
    IManager,
    IDisposable,
    IProviderResolver,
    ILanguageDataManager,
    IMultisiteEnabledManager
  {
    [Obsolete("Default transaction name is no longer used.")]
    public const string DefaultTransactionName = "FormsTransaction";
    internal const string SystemLibrariesToBeDelete = "libraries-to-be-deleted";
    internal const string ContentLinksToBeDelete = "content-links-to-be-deleted";
    internal const string UpdatedFormEntries = "updated-form-entries";
    internal const string FormIdLabel = "formId";
    internal const string FormEntryIdLabel = "responseEntryId";
    internal const string ProviderNameLabel = "providerName";
    internal const string FormExpirationTokenLabel = "expiration";
    internal const string FormEditDataLabel = "data";
    private const string TablePrefix = "sf_fm";

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Modules.Forms.FormsManager" /> class with the default provider.
    /// </summary>
    public FormsManager()
      : base((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Modules.Forms.FormsManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public FormsManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.FormsManager" /> class and sets the specified provider and distributed transaction.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public FormsManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    static FormsManager()
    {
      ManagerBase<FormsDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(FormsManager.Provider_Executing);
      ManagerBase<FormsDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(FormsManager.Provider_Executed);
    }

    /// <summary>
    /// Get an instance of MetadataManager associated with the current transaction.
    /// </summary>
    public MetadataManager GetMetadataManager() => this.Provider.GetRelatedManager<MetadataManager>((string) null);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object.
    /// </summary>
    /// <param name="formName">Name of the form. This name is used to construct the actual CLR type of the form entry
    /// and therefore the name should comply to CLR naming rules.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object
    /// </returns>
    public FormDescription CreateForm(string formName)
    {
      FormDescription form = this.Provider.CreateForm(formName);
      this.LinkFormToSite(form, SystemManager.CurrentContext.CurrentSite.Id);
      return form;
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object with the given id.
    /// </summary>
    /// <param name="formName">Name of the form. This name is used to construct the actual CLR type of the form entry
    /// and therefore the name should comply to CLR naming rules.</param>
    /// <param name="id">The id.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object
    /// </returns>
    public FormDescription CreateForm(string formName, Guid id)
    {
      FormDescription form = this.Provider.CreateForm(formName, id);
      this.LinkFormToSite(form, SystemManager.CurrentContext.CurrentSite.Id);
      return form;
    }

    /// <summary>Links the form to a specified site.</summary>
    /// <param name="form">The form.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal SiteItemLink LinkFormToSite(FormDescription form, Guid siteId) => this.Provider.LinkFormToSite(form, siteId);

    /// <summary>Gets the links for all forms.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    internal IQueryable<SiteItemLink> GetSiteFormLinks() => this.Provider.GetSiteFormLinks();

    /// <summary>
    /// Deletes the specified <see cref="T:Telerik.Sitefinity.Forms.Model.FormDraft" /> object.
    /// </summary>
    /// <param name="form">The form description.</param>
    public void Delete(FormDescription form)
    {
      string[] availableLanguages = this.GetAvailableLanguages((object) form, false);
      form.RegisterDeletedOperation(availableLanguages);
      this.Provider.Delete(form);
    }

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> object by ID.
    /// </summary>
    /// <param name="formId">The form description id.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Forms.Model.FormDraft" /> object with the given ID.</returns>
    public FormDescription GetForm(Guid formId) => this.Provider.GetForm(formId);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> with the specified form name.
    /// </summary>
    /// <param name="formName">Name of the form.</param>
    /// <returns></returns>
    public FormDescription GetFormByName(string formName) => this.Provider.GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Name == formName)).SingleOrDefault<FormDescription>();

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDraft" /> objects.
    /// </summary>
    /// <returns>An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDraft" /> objects.</returns>
    public IQueryable<FormDescription> GetForms() => this.Provider.GetForms();

    /// <summary>Gets the forms of a specified site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> objects.</returns>
    internal IQueryable<FormDescription> GetForms(Guid siteId) => this.Provider.GetForms(siteId);

    internal IQueryable<FormDescription> GetNotSharedForms() => this.Provider.GetNotSharedForms();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> with the given type.
    /// </summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <returns></returns>
    public FormEntry CreateFormEntry(string entryType) => this.Provider.CreateFormEntry(entryType);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> with the given type and id.
    /// </summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <returns></returns>
    public FormEntry CreateFormEntry(string entryType, Guid id) => this.Provider.CreateFormEntry(entryType, id);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> for removal.
    /// </summary>
    /// <param name="formEntry">The form entry.</param>
    public void Delete(FormEntry entry) => this.Provider.Delete(entry);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> object with the given type and id.
    /// </summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <param name="entryId">The entry id.</param>
    /// <returns></returns>
    public FormEntry GetFormEntry(string entryType, Guid entryId) => this.Provider.GetFormEntry(entryType, entryId);

    /// <summary>Gets the form entries of the given type.</summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <returns>An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> objects</returns>
    public IQueryable<FormEntry> GetFormEntries(FormDescription formDescription) => this.Provider.GetFormEntries(formDescription);

    /// <summary>Gets the form entries of the given type.</summary>
    /// <param name="entryType">Type of the entry.</param>
    /// <returns>An <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Forms.Model.FormEntry" /> objects</returns>
    public IQueryable<FormEntry> GetFormEntries(string entryType) => this.Provider.GetFormEntries(entryType);

    public IEnumerable GetFormEntries(
      Type entryType,
      string filter,
      string orderBy,
      int skip,
      int take,
      ref int? count)
    {
      return this.GetItems(entryType, filter, orderBy, skip, take, ref count);
    }

    /// <summary>Gets the type of the entry.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <returns></returns>
    public Type GetEntryType(FormDraft formDescription) => this.Provider.GetEntryType(formDescription);

    /// <summary>Gets the type of the entry.</summary>
    /// <param name="entryTypeName">Name of the entry type.</param>
    /// <returns></returns>
    public Type GetEntryType(string entryTypeName) => this.Provider.GetEntryType(entryTypeName);

    public void BuildDynamicType(MetadataManager metaMan, FormDescription form)
    {
      MetaType metaType = metaMan.GetMetaType(this.Provider.FormsNamespace, form.Name);
      if (metaType == null)
      {
        metaType = metaMan.CreateMetaType(this.Provider.FormsNamespace, form.Name);
        metaType.BaseClassName = typeof (FormEntry).FullName;
        metaType.DatabaseInheritance = DatabaseInheritanceType.vertical;
        metaType.IsDynamic = true;
        IList<MetaTypeAttribute> metaAttributes = metaType.MetaAttributes;
        MetaTypeAttribute metaTypeAttribute = new MetaTypeAttribute();
        metaTypeAttribute.Name = "moduleName";
        metaTypeAttribute.Value = "sf_fm";
        metaAttributes.Add(metaTypeAttribute);
      }
      IList<FormControl> controls = form.Controls;
      CultureInfo[] availableCultures = form.AvailableCultures;
      string propertyName1 = "MetaField";
      string propertyName2 = "FormId";
      string propertyName3 = "FormsProviderName";
      FormDraft formDraft = form.Drafts.FirstOrDefault<FormDraft>((Func<FormDraft, bool>) (d => !d.IsTempDraft));
      foreach (FormControl formControl in (IEnumerable<FormControl>) controls)
      {
        Type formControlType = FormsManager.GetFormControlType((ControlData) formControl);
        if (typeof (IFormFieldControl).IsAssignableFrom(formControlType))
        {
          if (!formControl.Published)
          {
            Guid id = formControl.Id;
            FormDraftControl draftControl = formDraft.Controls.SingleOrDefault<FormDraftControl>((Func<FormDraftControl, bool>) (c => c.OriginalControlId == id));
            IControlBehaviorResolver controlBehaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
            Control control = this.LoadControl((ObjectData) formControl, (CultureInfo) null);
            IFormFieldControl behaviorObject = (IFormFieldControl) controlBehaviorResolver.GetBehaviorObject(control);
            IMetaField metaField = behaviorObject.MetaField;
            string fieldName = metaField.FieldName;
            if (behaviorObject is FormFileUpload)
            {
              FormFileUpload formFileUpload = behaviorObject as FormFileUpload;
              formFileUpload.FormId = form.Id;
              formFileUpload.FormsProviderName = this.Provider.Name;
              this.ReadProperty((object) behaviorObject, (ObjectData) formControl, (ObjectData) draftControl, propertyName2);
              this.ReadProperty((object) behaviorObject, (ObjectData) formControl, (ObjectData) draftControl, propertyName3);
            }
            string str = !string.IsNullOrEmpty(metaField.FieldName) ? metaType.GetValidUniqueMetaFieldName(metaField.FieldName) : metaType.GetValidUniqueMetaFieldName(formControlType.Name);
            if (string.Compare(metaField.FieldName, str, StringComparison.OrdinalIgnoreCase) != 0)
            {
              metaField.FieldName = str;
              if (!typeof (MvcProxyBase).IsAssignableFrom(ControlManager<FormsDataProvider>.GetControlType((ObjectData) formControl)))
                this.ReadProperty((object) control, (ObjectData) formControl, (ObjectData) draftControl, propertyName1);
              else if (SystemManager.CurrentContext.AppSettings.Multilingual)
              {
                this.SetFieldNameForAllLanguages(availableCultures, formControl, draftControl, controlBehaviorResolver, str);
              }
              else
              {
                this.ReadProperties((object) control, (ObjectData) formControl);
                this.ReadProperties((object) control, (ObjectData) draftControl);
              }
            }
            else if (typeof (MvcProxyBase).IsAssignableFrom(ControlManager<FormsDataProvider>.GetControlType((ObjectData) formControl)) && !string.IsNullOrEmpty(metaField.FieldName))
            {
              if (SystemManager.CurrentContext.AppSettings.Multilingual)
              {
                this.SetFieldNameForAllLanguages(availableCultures, formControl, draftControl, controlBehaviorResolver, str);
              }
              else
              {
                this.ReadProperties((object) control, (ObjectData) formControl);
                this.ReadProperties((object) control, (ObjectData) draftControl);
              }
            }
            MetaField metafield = metaMan.CreateMetafield(metaField.FieldName);
            MetadataManager.CopyMetafield(metaField, metafield);
            metaType.Fields.Add(metafield);
          }
          else
            continue;
        }
        if (!formControl.Published)
          formControl.Published = true;
      }
      this.RemoveDeletedControlsMetaFields(metaType, controls);
    }

    private static bool RequiresLibraryCreation(FormDescription formDescription) => formDescription.Framework == FormFramework.WebForms ? formDescription.Controls.Any<FormControl>((Func<FormControl, bool>) (c => ControlManager<FormsDataProvider>.GetControlType((ObjectData) c).ImplementsInterface(typeof (IRequireLibrary)))) : formDescription.Controls.Any<FormControl>((Func<FormControl, bool>) (c =>
    {
      string name = c.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && p.Value != null)).Select<ControlProperty, string>((Func<ControlProperty, string>) (p => p.Value)).FirstOrDefault<string>();
      return !name.IsNullOrEmpty() && TypeResolutionService.ResolveType(name, false).ImplementsInterface(typeof (IRequireLibrary));
    }));

    private void SetFieldNameForAllLanguages(
      CultureInfo[] availableLanguages,
      FormControl control,
      FormDraftControl draftControl,
      IControlBehaviorResolver controlBehaviorResolver,
      string actualFieldName)
    {
      foreach (CultureInfo availableLanguage in availableLanguages)
      {
        Control control1 = this.LoadControl((ObjectData) control, availableLanguage);
        ((IFormFieldControl) controlBehaviorResolver.GetBehaviorObject(control1)).MetaField.FieldName = actualFieldName;
        this.ReadProperties((object) control1, (ObjectData) control, availableLanguage, (object) null);
        this.ReadProperties((object) control1, (ObjectData) draftControl, availableLanguage, (object) null);
      }
    }

    /// <summary>
    /// Marks all meta fields as deleted for controls that are no longer in the controls collections
    /// </summary>
    private void RemoveDeletedControlsMetaFields(MetaType metaType, IList<FormControl> controls)
    {
      IEnumerable<FormControl> formControls = controls.Where<FormControl>((Func<FormControl, bool>) (c => typeof (IFormFieldControl).IsAssignableFrom(FormsManager.GetFormControlType((ControlData) c))));
      List<string> formFieldControlFieldNames = new List<string>();
      IControlBehaviorResolver behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
      foreach (ObjectData controlData in formControls)
      {
        Control control = this.LoadControl(controlData, (CultureInfo) null);
        IFormFieldControl behaviorObject = (IFormFieldControl) behaviorResolver.GetBehaviorObject(control);
        if (behaviorObject.MetaField != null)
          formFieldControlFieldNames.Add(behaviorObject.MetaField.FieldName);
      }
      IEnumerable<string> deletedFields = metaType.Fields.Select<MetaField, string>((Func<MetaField, string>) (f => f.FieldName)).ToList<string>().Where<string>((Func<string, bool>) (mf => !formFieldControlFieldNames.Contains(mf)));
      foreach (MetaField metaField in metaType.Fields.Where<MetaField>((Func<MetaField, bool>) (f => deletedFields.Contains<string>(f.FieldName))))
        metaField.IsDeleted = true;
    }

    /// <summary>Builds the dynamic metaType of form description.</summary>
    /// <param name="formDescription">The form description for which dynamic metatype will be build.</param>
    public void BuildDynamicType(FormDescription formDescription) => this.BuildDynamicType(this.GetMetadataManager(), formDescription);

    /// <summary>
    /// Gets the encrypted query string which will serves for form response edit.
    /// </summary>
    /// <param name="formId">The form id.</param>
    /// <param name="formEntryId">The form entry id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="startWithQuestionMark">The start with question mark.</param>
    /// <returns>Gets the encrypted query string for the form response edit mode.</returns>
    public static string GetEncryptedQueryStringFormResponseEdit(
      Guid formId,
      Guid formEntryId,
      string providerName,
      bool startWithQuestionMark = false)
    {
      StringBuilder stringBuilder = new StringBuilder(4);
      stringBuilder.Append(string.Format("{0}={1}", (object) nameof (formId), (object) formId)).Append(string.Format("&{0}={1}", (object) "responseEntryId", (object) formEntryId)).Append(string.Format("&{0}={1}", (object) nameof (providerName), (object) providerName)).Append(string.Format("&{0}={1}", (object) "expiration", (object) DateTime.UtcNow.AddDays(1.0).ToLongDateTimeString()));
      string str = SecurityManager.EncryptData(stringBuilder.ToString());
      return string.Format("{0}{1}={2}", startWithQuestionMark ? (object) "?" : (object) string.Empty, (object) "data", (object) HttpUtility.UrlEncode(str));
    }

    /// <summary>Creates the form draft.</summary>
    /// <param name="form">The form.</param>
    /// <returns></returns>
    internal FormDraft CreateDraftForForm(FormDescription form, CultureInfo culture = null) => this.Lifecycle.Edit(form, culture);

    /// <summary>Copies the data from one form draft to another.</summary>
    /// <param name="formDraftFrom">The source draft object.</param>
    /// <param name="formDraftTo">The target draft object.</param>
    [Obsolete]
    internal void CopyFormDraft(FormDraft formDraftFrom, FormDraft formDraftTo) => this.CopyFormCommonData<FormDraftControl, FormDraftControl>((IFormData<FormDraftControl>) formDraftFrom, (IFormData<FormDraftControl>) formDraftTo, CopyDirection.Unspecified);

    /// <summary>
    /// Copies the data from one IFormData object to another. Note: this method only copies common draft and non-draft
    /// data.
    /// </summary>
    /// <param name="formFrom">The source object.</param>
    /// <param name="formTo">The target object.</param>
    public void CopyFormCommonData<TControlA, TControlB>(
      IFormData<TControlA> formFrom,
      IFormData<TControlB> formTo,
      CopyDirection copyDirection,
      CultureInfo sourceCulture = null,
      CultureInfo targetCulture = null)
      where TControlA : ControlData
      where TControlB : ControlData
    {
      formTo.LastControlId = formFrom.LastControlId;
      formTo.CssClass = formFrom.CssClass;
      formTo.FormLabelPlacement = formFrom.FormLabelPlacement;
      formTo.RedirectPageUrl = formFrom.RedirectPageUrl;
      formTo.SubmitAction = formFrom.SubmitAction;
      formTo.SubmitRestriction = formFrom.SubmitRestriction;
      formTo.SubmitActionAfterUpdate = formFrom.SubmitActionAfterUpdate;
      formTo.RedirectPageUrlAfterUpdate = formFrom.RedirectPageUrlAfterUpdate;
      formTo.SendConfirmationEmail = formFrom.SendConfirmationEmail;
      formTo.Rules = formFrom.Rules;
      formTo.SuccessMessage.SetString(targetCulture.GetLstring(), formFrom.SuccessMessage.GetString(sourceCulture.GetLstring(), true));
      formTo.SuccessMessageAfterFormUpdate.SetString(targetCulture.GetLstring(), formFrom.SuccessMessageAfterFormUpdate.GetString(sourceCulture.GetLstring(), true));
      this.CopyControls<TControlA, TControlB>((IEnumerable<TControlA>) formFrom.Controls, formTo.Controls, sourceCulture, targetCulture, copyDirection);
      this.CopyPresentation<FormPresentation, FormPresentation>((IEnumerable<FormPresentation>) formFrom.Presentation, formTo.Presentation);
    }

    /// <summary>
    /// Copies the data from a form ViewModel object to an IFormData object.
    /// </summary>
    /// <param name="formViewModel">The source ViewModel object.</param>
    /// <param name="formTo">The target object.</param>
    internal void CopyFormCommonData<TControl>(
      FormDescriptionViewModel formViewModel,
      IFormData<TControl> formTo)
      where TControl : ControlData
    {
      formTo.CssClass = formViewModel.CssClass;
      formTo.FormLabelPlacement = formViewModel.FormLabelPlacement;
      formTo.RedirectPageUrl = formViewModel.RedirectPageUrl;
      formTo.SubmitAction = formViewModel.SubmitAction;
      formTo.SubmitRestriction = formViewModel.SubmitRestriction;
      formTo.SuccessMessage = (Lstring) formViewModel.SuccessMessage;
      formTo.SubmitActionAfterUpdate = formViewModel.SubmitActionAfterUpdate;
      formTo.RedirectPageUrlAfterUpdate = formViewModel.RedirectPageUrlAfterUpdate;
      formTo.SuccessMessageAfterFormUpdate = (Lstring) formViewModel.SuccessMessageAfterFormUpdate;
      formTo.SendConfirmationEmail = formViewModel.SendConfirmationEmail;
    }

    /// <summary>Edits the form.</summary>
    /// <param name="pageId">The id of the template (PageTempalte object).</param>
    /// <param name="lockIt">if set to <c>true</c> locks the template and prevents other users from editing it.</param>
    /// <returns></returns>
    [Obsolete("The boolean lockIt parameter is now obsolete. Use the EditForm(Guid, CultureInfo) instead.")]
    public virtual FormDraft EditForm(Guid id, bool lockIt) => this.EditForm(id, (CultureInfo) null);

    /// <summary>Edits the form.</summary>
    /// <param name="pageId">The id of the template (PageTempalte object).</param>
    /// <param name="lockIt">if set to <c>true</c> locks the template and prevents other users from editing it.</param>
    /// <returns></returns>
    [Obsolete("The boolean lockIt parameter is now obsolete. Use the EditForm(Guid, CultureInfo) instead.")]
    public virtual FormDraft EditForm(Guid id, bool lockIt, CultureInfo culture = null) => this.EditForm(id, culture);

    /// <summary>Creates (if necessary) and returns a temp item</summary>
    /// <param name="pageId">The id of the template (PageTempalte object).</param>
    /// <param name="lockIt">if set to <c>true</c> locks the template and prevents other users from editing it.</param>
    /// <returns></returns>
    public virtual FormDraft EditForm(Guid liveItemId) => this.EditForm(liveItemId, (CultureInfo) null);

    /// <summary>Creates (if necessary) and returns a temp item</summary>
    /// <param name="pageId">The id of the template (PageTempalte object).</param>
    /// <param name="lockIt">if set to <c>true</c> locks the template and prevents other users from editing it.</param>
    /// <returns></returns>
    public virtual FormDraft EditForm(Guid liveItemId, CultureInfo culture)
    {
      FormDescription form = this.GetForm(liveItemId);
      return this.Lifecycle.CheckOut(this.Lifecycle.GetMaster(form) ?? this.Lifecycle.Edit(form, culture), culture);
    }

    /// <summary>Gets the a draft used for preview of the form.</summary>
    /// <param name="id">The id of the form (FormData id).</param>
    /// <returns></returns>
    public virtual FormDraft GetPreview(Guid id)
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      FormDraft formTo = this.GetDrafts().Where<FormDraft>((Expression<Func<FormDraft, bool>>) (d => d.ParentForm.Id == id && d.Owner == currentUserId && d.IsTempDraft == true)).SingleOrDefault<FormDraft>();
      if (formTo == null)
      {
        FormDescription form = this.GetForm(id);
        FormDraft formFrom = this.GetDrafts().Where<FormDraft>((Expression<Func<FormDraft, bool>>) (d => d.ParentForm.Id == id && d.IsTempDraft == false)).SingleOrDefault<FormDraft>();
        bool suppressSecurityChecks = this.Provider.SuppressSecurityChecks;
        this.Provider.SuppressSecurityChecks = true;
        if (formFrom == null)
        {
          formFrom = this.CreateDraftForForm(form);
          form.Drafts.Add(formFrom);
        }
        formTo = this.CreateDraft(form.Name);
        this.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        formTo.IsTempDraft = true;
        this.CopyFormCommonData<FormDraftControl, FormDraftControl>((IFormData<FormDraftControl>) formFrom, (IFormData<FormDraftControl>) formTo, CopyDirection.Unspecified);
        form.Drafts.Add(formTo);
      }
      return formTo;
    }

    /// <summary>Unlocks a form.</summary>
    /// <param name="formId">The form id.</param>
    /// <param name="takeOwnership">if set to <c>true</c> the form will be locked to the current user.</param>
    public void UnlockForm(Guid formId, bool takeOwnership) => this.UnlockForm(formId, takeOwnership, (CultureInfo) null);

    /// <summary>Unlocks a form.</summary>
    /// <param name="formId">The form id.</param>
    /// <param name="takeOwnership">if set to <c>true</c> the form will be locked to the current user.</param>
    public void UnlockForm(Guid formId, bool takeOwnership, CultureInfo culture)
    {
      FormDescription form = this.GetForm(formId);
      if (form != null)
      {
        if (form.IsGranted("Forms", "Unlock"))
        {
          Guid guid = Guid.Empty;
          if (takeOwnership)
            guid = SecurityManager.CurrentUserId;
          this.Lifecycle.DiscardTemp(form, culture);
          form.LockedBy = guid;
          return;
        }
      }
      throw new InvalidOperationException("The form cannot be unlocked.");
    }

    /// <summary>Deletes all temp drafts for the specified form.</summary>
    /// <param name="form">The specified form.</param>
    public virtual void DeleteTempDrafts(FormDescription form) => this.DeleteTempDrafts(form, (CultureInfo) null);

    /// <summary>Deletes all temp drafts for the specified form.</summary>
    /// <param name="form">The specified form.</param>
    public virtual void DeleteTempDrafts(FormDescription form, CultureInfo culture) => this.Lifecycle.DiscardTemp(form, culture);

    /// <summary>Publishes the form draft.</summary>
    /// <param name="draftId">The draft id.</param>
    /// <param name="makeVisible">if set to <c>true</c> form will be made visible.</param>
    [Obsolete]
    public virtual void PublishFormDraft(Guid draftId, bool makeVisible) => this.PublishFormDraft(this.GetDraft(draftId), (CultureInfo) null);

    /// <summary>Publishes a form.</summary>
    /// <param name="draft">The draft.</param>
    /// <param name="makeVisible">if set to <c>true</c> [make visible].</param>
    [Obsolete]
    public virtual void PublishFormDraft(FormDraft draft, bool makeVisible) => this.PublishFormDraft(draft, (CultureInfo) null);

    /// <summary>Publishes a form.</summary>
    /// <param name="draft">The draft.</param>
    public virtual void PublishFormDraft(FormDraft draft) => this.PublishFormDraft(draft, (CultureInfo) null);

    /// <summary>Publishes a form.</summary>
    /// <param name="draft">The draft.</param>
    /// <param name="makeVisible">if set to <c>true</c> [make visible].</param>
    public virtual void PublishFormDraft(FormDraft draft, CultureInfo culture) => this.Lifecycle.Publish(!draft.IsTempDraft ? draft : this.Lifecycle.CheckIn(draft, culture, false), culture);

    public virtual bool PublishForm(FormDescription form, CultureInfo culture = null)
    {
      bool multilingual = SystemManager.CurrentContext.AppSettings.Multilingual;
      CultureInfo sitefinityCulture = culture.GetSitefinityCulture();
      if (form.Status == ContentLifecycleStatus.Live && form.Visible && multilingual && form.PublishedTranslations.Contains(sitefinityCulture.Name))
        return false;
      this.PublishFormDraft(this.Lifecycle.GetMaster(form) ?? this.Lifecycle.Edit(form, sitefinityCulture), sitefinityCulture);
      return true;
    }

    /// <summary>Unpublishes a form.</summary>
    /// <param name="formId">The form id.</param>
    public void UnpublishForm(Guid formId) => this.UnpublishForm(formId, (CultureInfo) null);

    /// <summary>Unpublishes a form.</summary>
    /// <param name="formId">The form id.</param>
    public void UnpublishForm(Guid formId, CultureInfo culture) => this.Lifecycle.Unpublish(this.GetForm(formId), culture);

    /// <summary>Cancels the draft.</summary>
    /// <param name="draftId">The draft pageId.</param>
    public virtual void DiscardFormDraft(Guid draftId) => this.Lifecycle.DiscardTemp(this.GetDraft(draftId));

    /// <summary>Merges the template changes.</summary>
    /// <param name="draftId">The draft pageId.</param>
    /// <param name="publish">if set to <c>true</c> [publish].</param>
    public virtual void MergeFormChanges(Guid draftId, bool publish)
    {
      FormDraft draft = this.GetDraft(draftId);
      if (draft.ParentForm.Version == draft.Version)
      {
        if (!publish)
          return;
        this.PublishFormDraft(draftId, true);
      }
      throw new NotImplementedException();
    }

    /// <summary>Creates new draft page or template.</summary>
    /// <returns>The new control.</returns>
    public virtual FormDraft CreateDraft(string formName) => this.Provider.CreateDraft(formName);

    /// <summary>Creates new draft or page with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    public virtual FormDraft CreateDraft(string formName, Guid id) => this.Provider.CreateDraft(formName, id);

    /// <summary>
    /// Gets the draft page or template with the specified ID.
    /// </summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public virtual FormDraft GetDraft(Guid id) => this.Provider.GetDraft(id);

    /// <summary>Gets a query for draft pages or templates.</summary>
    /// <returns>The query for controls.</returns>
    public virtual IQueryable<FormDraft> GetDrafts() => this.Provider.GetDrafts();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public virtual void Delete(DraftData item) => this.Provider.Delete(item);

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public virtual void Delete(FormDraft item) => this.Delete((DraftData) item);

    /// <summary>
    /// Retrieves the draft with the given Id and copies it in the latest non-temp draft.
    /// </summary>
    /// <param name="id">The id.</param>
    public FormDraft SaveFormDraft(Guid id) => this.SaveFormDraft(this.GetDraft(id));

    /// <summary>
    /// Retrieves the draft with the given Id and copies it in the latest non-temp draft.
    /// </summary>
    /// <param name="id">The id.</param>
    public FormDraft SaveFormDraft(FormDraft tempDraft) => this.SaveFormDraft(tempDraft, (CultureInfo) null);

    /// <summary>
    /// Retrieves the draft with the given Id and copies it in the latest non-temp draft.
    /// </summary>
    /// <param name="id">The id.</param>
    public FormDraft SaveFormDraft(FormDraft tempDraft, CultureInfo culture) => this.Lifecycle.CheckIn(tempDraft, culture, false);

    /// <summary>
    /// Determines whether the specified target control is part of a draft form.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <returns></returns>
    private bool HasFieldTempForm(ControlData target) => target is FormDraftControl formDraftControl && formDraftControl.Form != null && formDraftControl.Form.IsTempDraft;

    /// <summary>Creates new control.</summary>
    /// <returns>The new control.</returns>
    public override T CreateControl<T>(bool isBackendObject = false) => this.Provider.CreateControl<T>(isBackendObject);

    /// <summary>Creates new control with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    public override T CreateControl<T>(Guid id, bool isBackendObject = false) => this.Provider.CreateControl<T>(id, isBackendObject);

    /// <summary>Gets the control with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public override T GetControl<T>(Guid id) => this.Provider.GetControl<T>(id);

    /// <summary>Gets a query for controls.</summary>
    /// <returns>The query for controls.</returns>
    public override IQueryable<T> GetControls<T>() => this.Provider.GetControls<T>();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public override void Delete(ControlData item) => this.Provider.Delete((ObjectData) item);

    /// <summary>Copies the control.</summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="sourceLanguage">The source language.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <param name="ignorePersonalization">If set true control's personalized versions will not be copied</param>
    public override void CopyControl(
      ControlData source,
      ControlData target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool ignorePersonalization = false)
    {
      base.CopyControl(source, target, sourceLanguage, targetLanguage, ignorePersonalization);
      if (this.HasFieldTempForm(target))
        return;
      (target as IFormControl).Published = (source as IFormControl).Published;
    }

    private void ReadProperty(
      object instance,
      ObjectData control,
      ObjectData draftControl,
      string propertyName)
    {
      this.ReadProperty(instance, control, propertyName, (CultureInfo) null);
      if (draftControl == null)
        return;
      this.ReadProperty(instance, draftControl, propertyName, (CultureInfo) null);
    }

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public override ControlProperty CreateProperty() => this.Provider.CreateProperty();

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public override ControlProperty CreateProperty(Guid id) => this.Provider.CreateProperty(id);

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>A page template.</returns>
    public override ControlProperty GetProperty(Guid id) => this.Provider.GetProperty(id);

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    public override IQueryable<ControlProperty> GetProperties() => this.Provider.GetProperties();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page template to delete.</param>
    public override void Delete(ControlProperty item) => this.Provider.Delete(item);

    /// <summary>
    /// Creates new object for storing presentation information.
    /// </summary>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T CreatePresentationItem<T>() => this.Provider.CreatePresentationItem<T>();

    /// <summary>
    /// Creates new object for storing presentation information with the specified ID.
    /// </summary>
    /// <param name="pageId">The pageId of the new item.</param>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T CreatePresentationItem<T>(Guid id) => this.Provider.CreatePresentationItem<T>(id);

    /// <summary>Links the presentation item to site.</summary>
    /// <param name="presentationData">The presentation item.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal override SiteItemLink LinkPresentationItemToSite(
      PresentationData presentationData,
      Guid siteId)
    {
      return this.Provider.LinkPresentationItemToSite(presentationData, siteId);
    }

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items in a specified site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>The query for page templates.</returns>
    internal override IQueryable<T> GetPresentationItems<T>(Guid siteId) => this.Provider.GetPresentationItems<T>(siteId);

    /// <summary>Gets the links for all presentation items.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    internal override IQueryable<SiteItemLink> GetSitePresentationItemLinks<T>() => this.Provider.GetSitePresentationItemLinks<T>();

    /// <summary>Gets the item with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public override T GetPresentationItem<T>(Guid id) => this.Provider.GetPresentationItem<T>(id);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <returns>The query for page templates.</returns>
    public override IQueryable<T> GetPresentationItems<T>() => this.Provider.GetPresentationItems<T>();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(PresentationData item) => this.Provider.Delete(item);

    /// <summary>Gets the default provider for this manager.</summary>
    /// <value></value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<FormsConfig>().DefaultProvider);

    /// <summary>The name of the module that this manager belongs to.</summary>
    /// <value></value>
    public override string ModuleName => "Forms";

    /// <summary>Collection of data provider settings.</summary>
    /// <value></value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<FormsConfig>().Providers;

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static FormsManager GetManager() => ManagerBase<FormsDataProvider>.GetManager<FormsManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static FormsManager GetManager(string providerName) => ManagerBase<FormsDataProvider>.GetManager<FormsManager>(providerName);

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <typeparam name="T">The type of the manager.</typeparam>
    /// <param name="providerName">The name of the data provider.</param>
    /// <param name="transactionName">Name of a named global transaction.</param>
    /// <returns>The manager instance.</returns>
    public static FormsManager GetManager(string providerName, string transactionName) => ManagerBase<FormsDataProvider>.GetManager<FormsManager>(providerName, transactionName);

    /// <summary>
    /// Saves any changes to objects retrieved with this manager.
    /// </summary>
    /// <param name="updateSchema">if set to <c>true</c> the database schema will be updated ant the application will be restarted.</param>
    [Obsolete("Use SaveChanges() without parameter. The schema will be updated automatically.")]
    public virtual void SaveChanges(bool updateSchema) => this.SaveChanges();

    /// <summary>Creates the item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType)
    {
      if (typeof (FormDraft).IsAssignableFrom(itemType))
        return (object) this.Provider.CreateDraft("Name");
      if (typeof (FormDescription).IsAssignableFrom(itemType))
        return (object) this.Provider.CreateForm("Name");
      return typeof (FormEntry).IsAssignableFrom(itemType) ? (object) this.Provider.CreateFormEntry(itemType.FullName) : base.CreateItem(itemType);
    }

    /// <summary>Creates the item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (typeof (FormDescription).IsAssignableFrom(itemType))
        return (object) this.Provider.CreateForm("Name", id);
      if (typeof (FormDraft).IsAssignableFrom(itemType))
        return (object) this.Provider.CreateDraft("Name", id);
      return typeof (FormEntry).IsAssignableFrom(itemType) ? (object) this.Provider.CreateFormEntry(itemType.FullName, id) : base.CreateItem(itemType, id);
    }

    /// <summary>Gets the item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (typeof (FormDescription).IsAssignableFrom(itemType))
        return (object) this.Provider.GetForm(id);
      if (typeof (FormDraft).IsAssignableFrom(itemType))
        return (object) this.Provider.GetDraft(id);
      return typeof (FormEntry).IsAssignableFrom(itemType) ? (object) this.Provider.GetFormEntry(itemType.FullName, id) : base.GetItem(itemType, id);
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take)
    {
      return typeof (FormDescription).IsAssignableFrom(itemType) || typeof (FormEntry).IsAssignableFrom(itemType) || typeof (FormDraft).IsAssignableFrom(itemType) ? this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take) : base.GetItems(itemType, filterExpression, orderExpression, skip, take);
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return typeof (FormDescription).IsAssignableFrom(itemType) ? this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount) : base.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>Deletes the item.</summary>
    /// <param name="item">The item.</param>
    public override void DeleteItem(object item)
    {
      Type type = item.GetType();
      if (typeof (FormDescription).IsAssignableFrom(type))
        this.Provider.Delete((FormDescription) item);
      if (typeof (FormDraft).IsAssignableFrom(type))
        this.Provider.Delete((DraftData) item);
      if (typeof (FormEntry).IsAssignableFrom(type))
        this.Provider.Delete((FormEntry) item);
      base.DeleteItem(item);
    }

    /// <summary>Manages the user subscription.</summary>
    /// <param name="subscribe">if set to <c>true</c> [subscribe]. otherwise unsubscribe</param>
    /// <param name="user">The user.</param>
    /// <param name="formId">The form id.</param>
    /// <param name="formsProviderName">Name of the forms provider.</param>
    private static void ManageUserSubscription(
      bool subscribe,
      User user,
      Guid formId,
      string formsProviderName = null)
    {
      ISubscriberRequest subscriberObject = FormsManager.GetSubscriberObject(user);
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      FormsManager manager = FormsManager.GetManager(formsProviderName);
      FormDescription formDescription = manager.GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Id == formId)).FirstOrDefault<FormDescription>();
      Guid subscriptionListId = formDescription.SubscriptionListId;
      bool flag;
      if (subscriptionListId == Guid.Empty)
      {
        if (!subscribe)
          return;
        SubscriptionListRequestProxy subscriptionList = new SubscriptionListRequestProxy()
        {
          Description = (string) ((Content) formDescription).Description
        };
        subscriptionListId = notificationService.CreateSubscriptionList(serviceContext, (ISubscriptionListRequest) subscriptionList);
        bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
        manager.Provider.SuppressSecurityChecks = true;
        formDescription.SubscriptionListId = subscriptionListId;
        manager.SaveChanges();
        manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        flag = false;
      }
      else
        flag = notificationService.IsSubscribed(serviceContext, subscriptionListId, subscriberObject.ResolveKey);
      if (subscribe & !flag)
        notificationService.Subscribe(serviceContext, subscriptionListId, subscriberObject);
      else
        notificationService.Unsubscribe(serviceContext, subscriptionListId, subscriberObject.ResolveKey);
    }

    /// <summary>Manages the user subscription after form update.</summary>
    /// <param name="subscribe">The subscribe.</param>
    /// <param name="user">The user.</param>
    /// <param name="formId">The form id.</param>
    /// <param name="formsProviderName">Name of the forms provider.</param>
    internal static void ManageUserSubscriptionAfterFormUpdate(
      bool subscribe,
      User user,
      Guid formId,
      string formsProviderName = null)
    {
      ISubscriberRequest subscriberObject = FormsManager.GetSubscriberObject(user);
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      FormsManager manager = FormsManager.GetManager(formsProviderName);
      FormDescription formDescription = manager.GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Id == formId)).FirstOrDefault<FormDescription>();
      Guid subscriptionListId = formDescription.SubscriptionListIdAfterFormUpdate;
      bool flag;
      if (subscriptionListId == Guid.Empty)
      {
        if (!subscribe)
          return;
        SubscriptionListRequestProxy subscriptionList = new SubscriptionListRequestProxy()
        {
          Description = (string) ((Content) formDescription).Description
        };
        subscriptionListId = notificationService.CreateSubscriptionList(serviceContext, (ISubscriptionListRequest) subscriptionList);
        bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
        manager.Provider.SuppressSecurityChecks = true;
        formDescription.SubscriptionListIdAfterFormUpdate = subscriptionListId;
        manager.SaveChanges();
        manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        flag = false;
      }
      else
        flag = notificationService.IsSubscribed(serviceContext, subscriptionListId, subscriberObject.ResolveKey);
      if (subscribe & !flag)
        notificationService.Subscribe(serviceContext, subscriptionListId, subscriberObject);
      else
        notificationService.Unsubscribe(serviceContext, subscriptionListId, subscriberObject.ResolveKey);
    }

    /// <summary>
    /// Un-subscribes the user for submitted form entry notifications.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="formId">The form id.</param>
    /// <param name="formsProviderName">Name of the forms data provider where the form is stored. This is null by default and takes the default data provider.</param>
    public static void UnsubscribeUser(User user, Guid formId, string formsProviderName = null) => FormsManager.ManageUserSubscription(false, user, formId, formsProviderName);

    /// <summary>Subscribes the user for forum notifications.</summary>
    /// <param name="user">The user to subscribe. The email and names are taken from the user to be used in the subscription notifications</param>
    /// <param name="formId">The form id.</param>
    /// <param name="formsProviderName">Name of the forms data provider where the form is stored. This is null by default and takes the default data provider.</param>
    public static void SubscribeUser(User user, Guid formId, string formsProviderName = null) => FormsManager.ManageUserSubscription(true, user, formId, formsProviderName);

    /// <summary>
    /// Checks the user is subscribed for a specific forum or thread.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="formId">The form id.</param>
    /// <param name="formsProviderName">Name of the forms data provider where the form is stored. This is null by default and takes the default data provider.</param>
    /// <returns>true if the user is subscribed for the form with specified form id.</returns>
    public static bool CheckUserIsSubscribed(User user, Guid formId, string formsProviderName = null)
    {
      Guid subscriptionListId = FormsManager.GetManager(formsProviderName).GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Id == formId)).FirstOrDefault<FormDescription>().SubscriptionListId;
      if (!(subscriptionListId != Guid.Empty))
        return false;
      ISubscriberRequest subscriberObject = FormsManager.GetSubscriberObject(user);
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      return SystemManager.GetNotificationService().IsSubscribed(serviceContext, subscriptionListId, subscriberObject.ResolveKey);
    }

    internal static bool CheckUserIsSubscribed(User user, Func<Guid> getSubscriptionId)
    {
      Guid subscriptionListId = getSubscriptionId();
      if (!(subscriptionListId != Guid.Empty))
        return false;
      ISubscriberRequest subscriberObject = FormsManager.GetSubscriberObject(user);
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      return SystemManager.GetNotificationService().IsSubscribed(serviceContext, subscriptionListId, subscriberObject.ResolveKey);
    }

    internal static ISubscriberRequest GetSubscriberObject(User user)
    {
      ISubscriberRequest subscriberObject = SecurityManager.GetSubscriberObject(user);
      subscriberObject.ResolveKey = subscriberObject.Email;
      return subscriberObject;
    }

    /// <summary>Gets the items.</summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <returns></returns>
    public override IQueryable<TItem> GetItems<TItem>()
    {
      if (typeof (TItem) == typeof (FormDescription))
        return (IQueryable<TItem>) this.GetForms();
      if (typeof (TItem) == typeof (FormDraft))
        return (IQueryable<TItem>) this.GetDrafts();
      throw new ArgumentException(nameof (TItem));
    }

    public LifecycleDecorator<FormDescription, FormDraft> Lifecycle => (LifecycleDecorator<FormDescription, FormDraft>) new LifecycleDecoratorForms(this);

    LifecycleDecorator<FormDescription, FormDraft> ILifecycleManager<FormDescription, FormDraft>.Lifecycle => this.Lifecycle;

    public LanguageData CreateLanguageData() => this.Provider.CreateItem<LanguageData>();

    public LanguageData CreateLanguageData(Guid id) => this.Provider.CreateItem<LanguageData>(id);

    public LanguageData GetLanguageData(Guid id) => this.Provider.GetItem<LanguageData>(id) as LanguageData;

    public FormDraft CreateDraft() => this.CreateDraft((string) null);

    /// <summary>
    /// Deletes a translation of a form description if a culture is specified
    /// If no culture is given the whole form description is deleted
    /// </summary>
    /// <param name="formDescription">form description</param>
    /// <param name="language">Language translation to be deleted, if not specified the whole item will be deleted</param>
    /// <returns>True if the whole form is deleted, not just the tranlsation</returns>
    public bool Delete(FormDescription formDescription, CultureInfo language)
    {
      if (language == null)
      {
        this.Delete(formDescription);
        return true;
      }
      this.Provider.FetchAllLanguagesData();
      List<CultureInfo> list = formDescription.GetAvailableLanguagesIgnoringContext().Where<CultureInfo>((Func<CultureInfo, bool>) (ci => ci.LCID != CultureInfo.InvariantCulture.LCID)).ToList<CultureInfo>();
      if (list.Count<CultureInfo>() <= 1 && list.Contains(language))
      {
        this.Delete(formDescription);
        return true;
      }
      this.DeleteLanguageVersion((object) formDescription, language);
      LocalizationHelper.ClearLstringPropertiesForLanguage((object) formDescription, language);
      list.Remove(language);
      CultureInfo lastLanguageLeft = (CultureInfo) null;
      if (list.Count == 1)
        lastLanguageLeft = list[0];
      this.ClearPropertiesForLiveSyncedContainer<FormDraft>((IContentWithDrafts<FormDraft>) formDescription, language, lastLanguageLeft);
      formDescription.RegisterDeletedOperation(new string[1]
      {
        language.GetLanguageKey()
      });
      return false;
    }

    public string ResolveFormEntrySourceSiteName(FormEntry entry)
    {
      if (entry.SourceSiteId == Guid.Empty)
        entry.SourceSiteId = SystemManager.CurrentContext.CurrentSite.Id;
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      return multisiteContext == null ? (!(SystemManager.CurrentContext.CurrentSite.Id == entry.SourceSiteId) ? (entry.SourceSiteDisplayName = entry.SourceSiteName) : (entry.SourceSiteName = entry.SourceSiteDisplayName = SystemManager.CurrentContext.CurrentSite.Name)) : (!multisiteContext.GetSites().Any<Telerik.Sitefinity.Multisite.ISite>((Func<Telerik.Sitefinity.Multisite.ISite, bool>) (s => s.Id == entry.SourceSiteId)) ? (entry.SourceSiteDisplayName = string.Format("{0} ({1})", (object) entry.SourceSiteName, (object) Res.Get<FormsResources>().SiteIsDeleted)) : (entry.SourceSiteName = entry.SourceSiteDisplayName = SystemManager.CurrentContext.MultisiteContext.GetSiteById(entry.SourceSiteId).Name));
    }

    internal void ValidateConstraints(string name, Guid id, bool duplicate = false)
    {
      if (this.GetForms().Where<FormDescription>((Expression<Func<FormDescription, bool>>) (f => f.Name == name && (f.Id != id || duplicate))).Count<FormDescription>() <= 0)
        return;
      CommonMethods.ThrowDuplicateNameException(name);
    }

    /// <summary>Gets the shareable types.</summary>
    Type[] IMultisiteEnabledManager.GetShareableTypes() => new Type[0];

    /// <summary>Gets the site specific types.</summary>
    Type[] IMultisiteEnabledManager.GetSiteSpecificTypes() => new Type[1]
    {
      typeof (FormDescription)
    };

    /// <summary>
    /// Executes before flushing or committing a Forums transactions and records which forums and threads statistics(counters) should be updated
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    internal static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      FormsDataProvider provider = sender as FormsDataProvider;
      IList dirtyItems = provider.GetDirtyItems();
      List<Guid> guidList1 = (List<Guid>) null;
      List<Guid> guidList2 = (List<Guid>) null;
      Dictionary<string, List<Guid>> dictionary = (Dictionary<string, List<Guid>>) null;
      ContentLinksManager contentLinksManager = (ContentLinksManager) null;
      for (int index1 = 0; index1 < dirtyItems.Count; ++index1)
      {
        object obj = dirtyItems[index1];
        SecurityConstants.TransactionActionType dirtyItemStatus = provider.GetDirtyItemStatus(obj);
        if (dirtyItemStatus == SecurityConstants.TransactionActionType.New)
        {
          if (obj is FormEntry component1)
          {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) component1))
            {
              if (property.PropertyType == typeof (ContentLink[]) && property.GetValue((object) component1) is ContentLink[] contentLinkArray)
              {
                for (int index2 = 0; index2 < contentLinkArray.Length; ++index2)
                  contentLinkArray[index2].ParentItemId = component1.Id;
              }
            }
          }
          else if (obj is FormDescription)
          {
            FormDescription formDescription = obj as FormDescription;
            if (FormsManager.RequiresLibraryCreation(formDescription))
              formDescription.GetFormLibrary();
          }
        }
        if (dirtyItemStatus == SecurityConstants.TransactionActionType.Updated)
        {
          if (obj is FormEntry component2)
          {
            if (dictionary == null)
              dictionary = new Dictionary<string, List<Guid>>();
            string fullName = component2.GetType().FullName;
            if (dictionary.ContainsKey(fullName))
              dictionary[fullName].Add(component2.Id);
            else
              dictionary.Add(fullName, new List<Guid>()
              {
                component2.Id
              });
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) component2))
            {
              if (property.PropertyType == typeof (ContentLink[]) && property.GetValue((object) component2) is ContentLink[] contentLinkArray)
              {
                if (contentLinksManager == null)
                  contentLinksManager = ContentLinksManager.GetManager();
                for (int index3 = 0; index3 < contentLinkArray.Length; ++index3)
                {
                  ContentLink contentLink = contentLinkArray[index3];
                  if (contentLink.Id == Guid.Empty)
                    contentLink.Id = contentLinksManager.Provider.GetNewGuid();
                  contentLink.ParentItemId = component2.Id;
                }
              }
            }
          }
          else if (obj is FormDescription)
          {
            FormDescription formDescription = obj as FormDescription;
            if (FormsManager.RequiresLibraryCreation(formDescription))
              formDescription.GetFormLibrary();
          }
        }
        if (dirtyItemStatus == SecurityConstants.TransactionActionType.Deleted)
        {
          switch (obj)
          {
            case FormDescription formDescription1:
              if (guidList1 == null)
                guidList1 = new List<Guid>();
              if (formDescription1.LibraryId != Guid.Empty && formDescription1.Status != ContentLifecycleStatus.Temp)
                guidList1.Add(formDescription1.LibraryId);
              PackagingOperations.DeleteAddonLinks(formDescription1.Id, formDescription1.GetType().FullName);
              continue;
            case FormEntry _:
              if (guidList2 == null)
                guidList2 = new List<Guid>();
              IEnumerator enumerator = TypeDescriptor.GetProperties(obj).GetEnumerator();
              try
              {
                while (enumerator.MoveNext())
                {
                  PropertyDescriptor current = (PropertyDescriptor) enumerator.Current;
                  if (current.PropertyType == typeof (ContentLink[]) && current.GetValue(obj) is ContentLink[] source)
                    guidList2.AddRange(((IEnumerable<ContentLink>) source).Select<ContentLink, Guid>((Func<ContentLink, Guid>) (cl => cl.Id)));
                }
                continue;
              }
              finally
              {
                if (enumerator is IDisposable disposable)
                  disposable.Dispose();
              }
            case FormDraftControl _:
              FormsManager.UpdateFormRulesOnDeleteField(obj as FormDraftControl, (IEnumerable) dirtyItems, provider);
              continue;
            default:
              continue;
          }
        }
      }
      if (guidList1 != null && guidList1.Any<Guid>())
        provider.SetExecutionStateData("libraries-to-be-deleted", (object) guidList1);
      if (guidList2 != null && guidList2.Any<Guid>())
        provider.SetExecutionStateData("content-links-to-be-deleted", (object) guidList2);
      if (dictionary == null || !dictionary.Any<KeyValuePair<string, List<Guid>>>())
        return;
      provider.SetExecutionStateData("updated-form-entries", (object) dictionary);
    }

    /// <summary>
    /// Handles the post commit event of the Forums provider and updates the forum and thread statistics if necessary
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    internal static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      FormsDataProvider formsDataProvider = sender as FormsDataProvider;
      bool? executionStateData1 = formsDataProvider.GetExecutionStateData("IsRollbacked") as bool?;
      bool flag = true;
      if (executionStateData1.GetValueOrDefault() == flag & executionStateData1.HasValue)
        return;
      List<Guid> executionStateData2 = formsDataProvider.GetExecutionStateData("libraries-to-be-deleted") as List<Guid>;
      List<Guid> executionStateData3 = formsDataProvider.GetExecutionStateData("content-links-to-be-deleted") as List<Guid>;
      Dictionary<string, List<Guid>> executionStateData4 = formsDataProvider.GetExecutionStateData("updated-form-entries") as Dictionary<string, List<Guid>>;
      ContentLinksManager contentLinksManager = (ContentLinksManager) null;
      if (executionStateData2 != null && executionStateData2.Any<Guid>())
      {
        LibrariesManager librariesManager = FormsExtensions.GetSystemProviderLibrariesManager();
        using (new ElevatedModeRegion((IManager) librariesManager))
        {
          foreach (Guid guid in executionStateData2)
          {
            Guid id = guid;
            Telerik.Sitefinity.Libraries.Model.DocumentLibrary libraryToDelete = librariesManager.GetDocumentLibraries().SingleOrDefault<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, bool>>) (l => l.Id == id));
            if (libraryToDelete != null)
              librariesManager.DeleteDocumentLibrary(libraryToDelete);
          }
          librariesManager.SaveChanges();
        }
      }
      if (executionStateData3 != null && executionStateData3.Any<Guid>())
      {
        if (contentLinksManager == null)
          contentLinksManager = ContentLinksManager.GetManager();
        foreach (Guid guid in executionStateData3)
        {
          Guid id = guid;
          ContentLink contentLink = contentLinksManager.GetContentLinks().SingleOrDefault<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.Id == id));
          if (contentLink != null)
            contentLinksManager.Delete(contentLink);
        }
      }
      if (executionStateData4 != null && executionStateData4.Any<KeyValuePair<string, List<Guid>>>())
      {
        if (contentLinksManager == null)
          contentLinksManager = ContentLinksManager.GetManager();
        foreach (KeyValuePair<string, List<Guid>> keyValuePair in executionStateData4)
        {
          foreach (Guid entryId in keyValuePair.Value)
          {
            FormEntry formEntry = formsDataProvider.GetFormEntry(keyValuePair.Key, entryId);
            contentLinksManager.RemoveRedundantContentLinks((IContent) formEntry);
          }
        }
      }
      contentLinksManager?.SaveChanges();
      formsDataProvider.SetExecutionStateData("libraries-to-be-deleted", (object) null);
      formsDataProvider.SetExecutionStateData("content-links-to-be-deleted", (object) null);
      formsDataProvider.SetExecutionStateData("updated-form-entries", (object) null);
    }

    internal static Type GetFormControlType(ControlData control)
    {
      IControlBehaviorResolver behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
      return !control.ObjectType.StartsWith("~/") ? TypeResolutionService.ResolveType(behaviorResolver.GetBehaviorObjectType(control), true) : ControlManager<FormsDataProvider>.GetControlType((ObjectData) control);
    }

    private static void UpdateFormRulesOnDeleteField(
      FormDraftControl deletedField,
      IEnumerable dirtyItems,
      FormsDataProvider provider)
    {
      if (deletedField == null)
        return;
      FormDraft itemInTransaction = dirtyItems.OfType<FormDraft>().FirstOrDefault<FormDraft>((Func<FormDraft, bool>) (f => f.Id == deletedField.ContainerId));
      if (itemInTransaction == null || string.IsNullOrWhiteSpace(itemInTransaction.Rules) || provider.GetDirtyItemStatus((object) itemInTransaction) == SecurityConstants.TransactionActionType.Deleted)
        return;
      List<FormRule> source = JsonConvert.DeserializeObject<List<FormRule>>(itemInTransaction.Rules);
      ControlProperty controlProperty = deletedField.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID"));
      if (controlProperty != null && source != null && source.Count > 0)
      {
        string str = controlProperty.Value;
        for (int index1 = source.Count - 1; index1 >= 0; --index1)
        {
          for (int index2 = source[index1].Conditions.Count - 1; index2 >= 0; --index2)
          {
            if (source[index1].Conditions[index2].Id == str)
              source[index1].Conditions.RemoveAt(index2);
          }
          for (int index3 = source[index1].Actions.Count - 1; index3 >= 0; --index3)
          {
            if (source[index1].Actions[index3].Target == str)
              source[index1].Actions.RemoveAt(index3);
          }
          if (source[index1].Conditions.Count == 0 || source[index1].Actions.Count == 0)
            source.RemoveAt(index1);
        }
      }
      itemInTransaction.Rules = source.Any<FormRule>() ? JsonConvert.SerializeObject((object) source) : (string) null;
    }

    public bool DeleteTempAfterPublish => true;
  }
}
