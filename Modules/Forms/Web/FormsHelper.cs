// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.FormsHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Data;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>Represents a helper class used by the Forms Module.</summary>
  internal class FormsHelper
  {
    /// <summary>Saves the forms entry.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <param name="formsData">The forms data.</param>
    /// <param name="files">The files.</param>
    /// <param name="userIpAddress">The user ip address.</param>
    /// <param name="formLanguage">The form language.</param>
    /// <returns>Returns the id of saved form entry.</returns>
    public static Guid SaveFormsEntry(
      Guid formDescriptionId,
      IEnumerable<KeyValuePair<string, object>> postedData,
      IDictionary<string, List<FormHttpPostedFile>> files,
      string userIpAddress,
      string formLanguage,
      IEnumerable<string> notificationEmails = null)
    {
      Guid entryId = Guid.Empty;
      FormsManager manager = FormsManager.GetManager((string) null, Guid.NewGuid().ToString("N"));
      manager.ExecuteAndCommitWithRetries((Action) (() => entryId = FormsHelper.SaveFormEntryInternal(manager, formDescriptionId, postedData, files, userIpAddress, formLanguage, notificationEmails)), 5);
      return entryId;
    }

    /// <summary>
    /// Validates the form against the preset submit restrictions.
    /// </summary>
    /// <param name="formDescription">The form description.</param>
    /// <param name="userId">The user identifier.</param>
    /// <param name="userIP">The user IP address.</param>
    /// <param name="restrictionsError">The restrictions error.</param>
    /// <returns></returns>
    /// <exception cref="T:System.NotImplementedException"></exception>
    public static bool ValidateFormSubmissionRestrictions(
      FormDescription formDescription,
      Guid userId,
      string userIP,
      out string restrictionsError)
    {
      FormsManager manager = FormsManager.GetManager();
      switch (formDescription.SubmitRestriction)
      {
        case SubmitRestriction.OnePerCookie:
          throw new NotImplementedException();
        case SubmitRestriction.OnePerIp:
          using (new ElevatedModeRegion((IManager) manager))
          {
            if (manager.GetFormEntries(formDescription).Any<FormEntry>((Expression<Func<FormEntry, bool>>) (f => f.IpAddress == userIP)))
            {
              restrictionsError = Res.Get<FormsResources>().YouHaveAlreadySubmittedThisForm;
              return false;
            }
            break;
          }
        case SubmitRestriction.OnePerUser:
          using (new ElevatedModeRegion((IManager) manager))
          {
            if (manager.GetFormEntries(formDescription).Any<FormEntry>((Expression<Func<FormEntry, bool>>) (f => f.UserId == userId)))
            {
              restrictionsError = Res.Get<FormsResources>().YouHaveAlreadySubmittedThisForm;
              return false;
            }
            break;
          }
      }
      restrictionsError = string.Empty;
      return true;
    }

    /// <summary>Gets the form data.</summary>
    /// <param name="name">The name.</param>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public static FormDescription GetFormData(string name, Guid id)
    {
      FormsManager manager = FormsManager.GetManager();
      FormDescription formDescription;
      try
      {
        formDescription = !(id != Guid.Empty) ? manager.GetFormByName(name) : manager.GetForm(id);
      }
      catch (ItemNotFoundException ex)
      {
        return (FormDescription) null;
      }
      return !formDescription.Visible ? (FormDescription) null : formDescription;
    }

    /// <summary>Creates the before form action event.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <param name="postedData">The posted data.</param>
    /// <param name="ipAddress">The ip address.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    /// <exception cref="T:System.NotImplementedException"></exception>
    public static BeforeFormActionEvent CreateBeforeFormActionEvent(
      FormDescription formDescription,
      FormPostedData postedData,
      string ipAddress,
      Guid userId,
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate,
      string mode)
    {
      BeforeFormActionEvent beforeFormActionEvent = new BeforeFormActionEvent()
      {
        FormId = formDescription.Id,
        FormName = formDescription.Name,
        UserId = userId,
        IpAddress = ipAddress,
        FormTitle = (string) formDescription.Title,
        FormEntryResponseEditContext = formEntryResponseEditContext,
        IsEditMode = formEntryResponseEditContext.EntryId != Guid.Empty
      };
      FormsManager manager = FormsManager.GetManager(formDescription.GetProviderName());
      using (new ElevatedModeRegion((IManager) manager))
      {
        beforeFormActionEvent.Controls = FormsHelper.GetFormControlsData(manager, formDescription, postedData, controlsSate, mode, formEntryResponseEditContext.EntryId);
        return beforeFormActionEvent;
      }
    }

    /// <summary>Creates the form validating event.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <param name="postedData">The posted data.</param>
    /// <param name="ipAddress">The ip address.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public static FormValidatingEvent CreateFormValidatingEvent(
      FormDescription formDescription,
      FormPostedData postedData,
      string ipAddress,
      Guid userId,
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate,
      string mode)
    {
      FormValidatingEvent formValidatingEvent = new FormValidatingEvent()
      {
        FormId = formDescription.Id,
        FormName = formDescription.Name,
        UserId = userId,
        IpAddress = ipAddress,
        FormTitle = (string) formDescription.Title,
        FormEntryResponseEditContext = formEntryResponseEditContext,
        IsEditMode = formEntryResponseEditContext.EntryId != Guid.Empty
      };
      FormsManager manager = FormsManager.GetManager(formDescription.GetProviderName());
      using (new ElevatedModeRegion((IManager) manager))
      {
        formValidatingEvent.Controls = FormsHelper.GetFormControlsData(manager, formDescription, postedData, controlsSate, mode, formEntryResponseEditContext.EntryId);
        return formValidatingEvent;
      }
    }

    /// <summary>Creates the form saving event.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <param name="postedData">The posted data.</param>
    /// <param name="ipAddress">The ip address.</param>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    public static FormSavingEvent CreateFormSavingEvent(
      FormDescription formDescription,
      FormPostedData postedData,
      string ipAddress,
      Guid userId,
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate,
      string mode)
    {
      FormSavingEvent formSavingEvent = new FormSavingEvent()
      {
        FormId = formDescription.Id,
        FormName = formDescription.Name,
        UserId = userId,
        IpAddress = ipAddress,
        FormTitle = (string) formDescription.Title,
        FormEntryResponseEditContext = formEntryResponseEditContext,
        IsEditMode = formEntryResponseEditContext.EntryId != Guid.Empty
      };
      FormsManager manager = FormsManager.GetManager(formDescription.GetProviderName());
      using (new ElevatedModeRegion((IManager) manager))
      {
        formSavingEvent.Controls = FormsHelper.GetFormControlsData(manager, formDescription, postedData, controlsSate, mode, formEntryResponseEditContext.EntryId);
        return formSavingEvent;
      }
    }

    /// <summary>Creates the form saved event.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <param name="formPostedData">The form posted data.</param>
    /// <param name="p">The p.</param>
    /// <param name="guid">The unique identifier.</param>
    /// <returns></returns>
    public static FormSavedEvent CreateFormSavedEvent(
      FormDescription formDescription,
      FormPostedData postedData,
      string ipAddress,
      Guid userId,
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate,
      string mode)
    {
      FormSavedEvent formSavedEvent = new FormSavedEvent()
      {
        FormId = formDescription.Id,
        FormName = formDescription.Name,
        UserId = userId,
        IpAddress = ipAddress,
        FormTitle = (string) formDescription.Title,
        FormEntryResponseEditContext = formEntryResponseEditContext,
        IsEditMode = mode == FormsControl.FormsDefaultModes.Update,
        FormSubscriptionListId = formDescription.SubscriptionListIdAfterFormUpdate
      };
      FormsManager manager = FormsManager.GetManager(formDescription.GetProviderName());
      using (new ElevatedModeRegion((IManager) manager))
      {
        formSavedEvent.Controls = FormsHelper.GetFormControlsData(manager, formDescription, postedData, controlsSate, mode, formEntryResponseEditContext.EntryId);
        return formSavedEvent;
      }
    }

    /// <summary>Generates the response edit context.</summary>
    /// <param name="isEditMode">The is in edit mode.</param>
    /// <param name="entryId">The entry id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="entryType">Type of the entry.</param>
    /// <returns></returns>
    public static IFormEntryResponseEditContext GenerateReponseEditContext(
      Guid entryId,
      FormDescription formDescription)
    {
      return entryId != Guid.Empty ? (IFormEntryResponseEditContext) new FormEntryResponseEditContext(entryId) : (IFormEntryResponseEditContext) new FormEntryResponseEditContext();
    }

    /// <summary>Gets the sorted controls data.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <returns></returns>
    public static List<ControlData> GetSortedControlsData(
      FormDescription formDescription)
    {
      List<IControlsContainer> controlsContainerList = new List<IControlsContainer>();
      controlsContainerList.Add((IControlsContainer) formDescription);
      PageHelper.ProcessControls((IList<ControlData>) new List<ControlData>(), (IList<ControlData>) new List<ControlData>(), (IList<IControlsContainer>) controlsContainerList);
      return PageHelper.SortControls(controlsContainerList.AsEnumerable<IControlsContainer>().Reverse<IControlsContainer>(), controlsContainerList.Count);
    }

    /// <summary>Gets the form field controls.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public static IEnumerable<IFormFieldControl> GetFormFieldControls(
      FormDescription formDescription,
      string providerName)
    {
      List<ControlData> sortedControlsData = FormsHelper.GetSortedControlsData(formDescription);
      FormsManager formsManager = FormsManager.GetManager(providerName);
      IControlBehaviorResolver behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
      foreach (ObjectData controlData in sortedControlsData)
      {
        if (behaviorResolver.GetBehaviorObject(formsManager.LoadControl(controlData, (CultureInfo) null)) is IFormFieldControl behaviorObject)
          yield return behaviorObject;
      }
    }

    /// <summary>Updates the form entry.</summary>
    /// <param name="providername">The provider name.</param>
    /// <param name="entryId">The entry id.</param>
    /// <param name="formDescription">The form description.</param>
    /// <param name="formPostedData">The form posted data.</param>
    /// <param name="userIpAddress">The user ip address.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="userPovider">The user provider.</param>
    public static void UpdateFormEntry(
      string providername,
      Guid entryId,
      FormDescription formDescription,
      IEnumerable<KeyValuePair<string, object>> postedData,
      IDictionary<string, List<FormHttpPostedFile>> files,
      string userIpAddress,
      Guid userId,
      string userPovider)
    {
      FormsManager manager = FormsManager.GetManager(providername);
      FormEntry formEntry = manager.GetFormEntry(formDescription.EntriesTypeName, entryId);
      foreach (KeyValuePair<string, object> keyValuePair in postedData)
        formEntry.SetValue(keyValuePair.Key, keyValuePair.Value);
      FormsHelper.SaveFiles(files, formDescription, formEntry, true);
      formEntry.IpAddress = userIpAddress;
      formEntry.UserId = userId == Guid.Empty ? formEntry.UserId : userId;
      formEntry.UserProvider = userPovider;
      formEntry.LastModified = DateTime.UtcNow;
      manager.SaveChanges();
    }

    /// <summary>Creates the form HTTP files data.</summary>
    /// <param name="httpFileCollectionBase">The HTTP file collection base.</param>
    /// <returns></returns>
    public static IDictionary<string, List<FormHttpPostedFile>> CreateFormHttpFilesData(
      HttpFileCollectionBase httpFileCollectionBase)
    {
      Dictionary<string, List<FormHttpPostedFile>> formHttpFilesData = new Dictionary<string, List<FormHttpPostedFile>>();
      for (int index1 = 0; index1 < httpFileCollectionBase.Keys.Count; ++index1)
      {
        string key = httpFileCollectionBase.Keys[index1];
        HttpPostedFileBase httpPostedFileBase = httpFileCollectionBase[index1];
        if (!formHttpFilesData.Keys.Contains<string>(key))
        {
          List<FormHttpPostedFile> formHttpPostedFileList = new List<FormHttpPostedFile>();
          formHttpPostedFileList.Add(new FormHttpPostedFile()
          {
            FileName = httpFileCollectionBase[index1].FileName,
            ContentLength = (long) httpFileCollectionBase[index1].ContentLength,
            ContentType = httpFileCollectionBase[index1].ContentType,
            InputStream = httpFileCollectionBase[index1].InputStream
          });
          for (int index2 = index1 + 1; index2 < httpFileCollectionBase.Keys.Count; ++index2)
          {
            if (key.Equals(httpFileCollectionBase.Keys[index2], StringComparison.InvariantCultureIgnoreCase))
              formHttpPostedFileList.Add(new FormHttpPostedFile()
              {
                FileName = httpFileCollectionBase[index2].FileName,
                ContentLength = (long) httpFileCollectionBase[index2].ContentLength,
                ContentType = httpFileCollectionBase[index2].ContentType,
                InputStream = httpFileCollectionBase[index2].InputStream
              });
          }
          formHttpFilesData.Add(key, formHttpPostedFileList);
        }
      }
      return (IDictionary<string, List<FormHttpPostedFile>>) formHttpFilesData;
    }

    /// <summary>Gets the only valid for update controls.</summary>
    /// <param name="fieldControls">The field controls.</param>
    /// <param name="readOnlyMode">The read only mode.</param>
    /// <param name="hiddenMode">The hidden mode.</param>
    /// <returns></returns>
    public static IEnumerable<IFormFieldControl> ProcessValidForUpdateFieldControls(
      IEnumerable<IFormFieldControl> fieldControls,
      string currentMode)
    {
      foreach (IFormFieldControl fieldControl in fieldControls)
      {
        if (fieldControl is IMultiDisplayModesSupport formFieldControl)
        {
          if ((formFieldControl.GetFieldReadOnlyByMode(currentMode) ? 1 : 0) == 0 & formFieldControl.GetFieldVisibleByMode(currentMode))
            yield return fieldControl;
        }
        else
          yield return fieldControl;
      }
    }

    /// <summary>Gets the form posted data.</summary>
    /// <returns></returns>
    public static FormPostedData GetFormPostedData(
      IEnumerable<IFormFieldControl> controls)
    {
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, List<FormHttpPostedFile>> dictionary2 = new Dictionary<string, List<FormHttpPostedFile>>();
      foreach (IFormFieldControl control in controls)
      {
        if (control is FieldControl fieldControl)
        {
          if (fieldControl is FormFileUpload)
          {
            if (fieldControl.Value is UploadedFileCollection uploadedFileCollection)
            {
              List<FormHttpPostedFile> formHttpPostedFileList = new List<FormHttpPostedFile>();
              foreach (object obj in (CollectionBase) uploadedFileCollection)
              {
                if (obj is UploadedFile uploadedFile)
                  formHttpPostedFileList.Add(new FormHttpPostedFile()
                  {
                    FileName = uploadedFile.FileName,
                    InputStream = uploadedFile.InputStream,
                    ContentType = uploadedFile.ContentType,
                    ContentLength = uploadedFile.ContentLength
                  });
              }
              dictionary2.Add(control.MetaField.FieldName, formHttpPostedFileList);
            }
            else
              dictionary1.Add(control.MetaField.FieldName, fieldControl.Value);
          }
          else if (fieldControl.Value is List<string>)
          {
            string str = (string) new StringArrayConverter().ConvertTo((object) (fieldControl.Value as List<string>).ToArray(), typeof (string));
            dictionary1.Add(control.MetaField.FieldName, (object) str);
          }
          else
            dictionary1.Add(control.MetaField.FieldName, fieldControl.Value);
        }
      }
      return new FormPostedData()
      {
        FormsData = (IDictionary<string, object>) dictionary1,
        Files = (IDictionary<string, List<FormHttpPostedFile>>) dictionary2
      };
    }

    /// <summary>Gets the state of the current controls.</summary>
    /// <param name="isUpdate">The is update.</param>
    /// <param name="form">The form.</param>
    /// <param name="entryId">The entry id.</param>
    /// <returns></returns>
    public static IList<IFormEntryEventControl> GetCurrentControlsState(
      bool isUpdate,
      FormDescription form,
      Guid entryId)
    {
      return isUpdate ? (IList<IFormEntryEventControl>) OpenAccessFormsProvider.GetEntryControls(FormsManager.GetManager(form.GetProviderName()).GetFormEntry(form.EntriesTypeName, entryId), form, form.GetProviderName()).ToList<IFormEntryEventControl>() : (IList<IFormEntryEventControl>) new List<IFormEntryEventControl>();
    }

    private static Guid SaveFormEntryInternal(
      FormsManager manager,
      Guid formDescriptionId,
      IEnumerable<KeyValuePair<string, object>> postedData,
      IDictionary<string, List<FormHttpPostedFile>> files,
      string userIpAddress,
      string formLanguage,
      IEnumerable<string> notificationEmails = null)
    {
      FormDescription form = manager.GetForm(formDescriptionId);
      string entryType = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}.{1}", (object) manager.Provider.FormsNamespace, (object) form.Name);
      FormEntry formEntry = manager.CreateFormEntry(entryType);
      foreach (KeyValuePair<string, object> data in postedData)
        FormsHelper.SetValue(formEntry, data);
      FormsHelper.SaveFiles(files, form, formEntry);
      formEntry.IpAddress = userIpAddress;
      formEntry.SubmittedOn = DateTime.UtcNow;
      formEntry.UserId = ClaimsManager.GetCurrentUserId();
      formEntry.UserProvider = ClaimsManager.GetCurrentIdentity().MembershipProvider;
      if (formLanguage != null)
        formEntry.Language = SystemManager.CurrentContext.Culture.Name;
      formEntry.ReferralCode = manager.Provider.GetNextReferralCode(entryType).ToString();
      formEntry.NotificationEmails = notificationEmails;
      return formEntry.Id;
    }

    private static void SetValue(FormEntry entry, KeyValuePair<string, object> data)
    {
      if (data.Value != null)
      {
        if (data.Value is string)
          entry.SetValue(data.Key, (object) data.Value.ToString());
        else if ((object) (data.Value as Lstring) != null)
        {
          Lstring lstring = data.Value as Lstring;
          lstring.Value = lstring.Value.ToString();
          entry.SetValue(data.Key, (object) lstring);
        }
        else
          entry.SetValue(data.Key, data.Value);
      }
      else
        entry.SetValue(data.Key, (object) null);
    }

    /// <summary>Saves the files.</summary>
    /// <param name="files">The files.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="formDescription">The form description.</param>
    /// <param name="entry">The entry.</param>
    private static void SaveFiles(
      IDictionary<string, List<FormHttpPostedFile>> files,
      FormDescription formDescription,
      FormEntry entry,
      bool isUpdateMode = false)
    {
      if (files == null || files.Count <= 0)
        return;
      foreach (string key in (IEnumerable<string>) files.Keys)
      {
        List<FormHttpPostedFile> file = files[key];
        string fieldName = key;
        bool isMultipleAttachments = isUpdateMode && FormsHelper.IsMultipleFileUploadFiledAttachments(formDescription, fieldName);
        string transactionName = Guid.NewGuid().ToString();
        LibrariesManager librariesManager = FormsExtensions.GetSystemProviderLibrariesManager(transactionName);
        foreach (FormHttpPostedFile formHttpPostedFile in file)
        {
          using (new ElevatedModeRegion((IManager) librariesManager))
          {
            string withoutExtension = Path.GetFileNameWithoutExtension(formHttpPostedFile.FileName);
            DocumentLibrary formLibrary = (DocumentLibrary) formDescription.GetFormLibrary(transactionName: transactionName);
            Document document = librariesManager.CreateDocument();
            document.Title = (Lstring) withoutExtension;
            document.UrlName = (Lstring) (CommonMethods.TitleToUrl((string) document.Title) + "-" + entry.Id.ToString().Replace("-", string.Empty));
            document.Library = formLibrary;
            document.Parent = (Library) formLibrary;
            CommonMethods.RecompileItemUrls((Telerik.Sitefinity.GenericContent.Model.Content) document, (IManager) librariesManager, new List<string>(), false);
            librariesManager.Upload((MediaContent) document, formHttpPostedFile.InputStream, Path.GetExtension(formHttpPostedFile.FileName));
            document.ApprovalWorkflowState = (Lstring) "Published";
            ILifecycleDataItem lifecycleDataItem = librariesManager.Lifecycle.Publish((ILifecycleDataItem) document);
            LibrariesDataProvider provider = librariesManager.Provider;
            document.CreateApprovalTrackingRecord(provider.ApplicationName, "Published", provider.GetNewGuid());
            TransactionManager.CommitTransaction(transactionName);
            entry.AddFile((MediaContent) (lifecycleDataItem as Document), fieldName, isUpdateMode, isMultipleAttachments);
          }
        }
      }
    }

    private static bool IsMultipleFileUploadFiledAttachments(
      FormDescription formDescription,
      string fieldName)
    {
      return FormsHelper.GetFormFieldControls(formDescription, formDescription.GetProviderName()).FirstOrDefault<IFormFieldControl>((Func<IFormFieldControl, bool>) (f => f is FormFileUpload && f.MetaField != null && !string.IsNullOrWhiteSpace(f.MetaField.FieldName) && f.MetaField.FieldName == fieldName)) is FormFileUpload formFileUpload && formFileUpload.AllowMultipleAttachments;
    }

    /// <summary>Gets the form controls data.</summary>
    /// <param name="postedData">The posted data.</param>
    /// <returns></returns>
    private static IEnumerable<IFormEntryValidationEventControl> GetFormControlsData(
      FormPostedData postedData)
    {
      List<FormEntryValidationEventControl> formControlsData = new List<FormEntryValidationEventControl>();
      foreach (string key in (IEnumerable<string>) postedData.FormsData.Keys)
        formControlsData.Add(new FormEntryValidationEventControl()
        {
          FieldName = key,
          Value = postedData.FormsData[key]
        });
      foreach (string key in (IEnumerable<string>) postedData.Files.Keys)
        formControlsData.Add(new FormEntryValidationEventControl()
        {
          FieldName = key,
          Value = (object) postedData.Files[key]
        });
      return (IEnumerable<IFormEntryValidationEventControl>) formControlsData;
    }

    /// <summary>Gets the form controls data.</summary>
    /// <param name="formDescription">The form description.</param>
    /// <param name="postedData">The posted data.</param>
    /// <param name="entryId">The entry id.</param>
    /// <returns></returns>
    private static IEnumerable<IFormEntryValidationEventControl> GetFormControlsData(
      FormsManager formsManager,
      FormDescription formDescription,
      FormPostedData postedData,
      IList<IFormEntryEventControl> controlsSate,
      string mode,
      Guid entryId)
    {
      if (entryId == Guid.Empty)
        return FormsHelper.GetFormControlsData(postedData);
      List<FormEntryValidationEventControl> formControlsData = new List<FormEntryValidationEventControl>();
      if (!controlsSate.Any<IFormEntryEventControl>())
        return (IEnumerable<IFormEntryValidationEventControl>) formControlsData;
      FormEntry formEntry = formsManager.GetFormEntry(formDescription.EntriesTypeName, entryId);
      Dictionary<string, IFormFieldControl> dictionary = FormsHelper.GetSortedControlsData(formDescription).Select<ControlData, Control>((Func<ControlData, Control>) (f => formsManager.LoadControl((ObjectData) f, (CultureInfo) null))).OfType<IFormFieldControl>().ToDictionary<IFormFieldControl, string, IFormFieldControl>((Func<IFormFieldControl, string>) (field => field.MetaField.FieldName), (Func<IFormFieldControl, IFormFieldControl>) (field => field));
      foreach (IFormEntryEventControl entryEventControl in (IEnumerable<IFormEntryEventControl>) controlsSate)
      {
        bool flag1 = false;
        bool flag2 = true;
        if (!string.IsNullOrWhiteSpace(entryEventControl.FieldName) && formEntry.DoesFieldExist(entryEventControl.FieldName) && postedData.FormsData.ContainsKey(entryEventControl.FieldName))
        {
          if (dictionary.ContainsKey(entryEventControl.FieldName) && dictionary[entryEventControl.FieldName] is IMultiDisplayModesSupport formFieldControl)
          {
            flag1 = formFieldControl.GetFieldReadOnlyByMode(mode);
            flag2 = formFieldControl.GetFieldVisibleByMode(mode);
          }
          formControlsData.Add(new FormEntryValidationEventControl()
          {
            FieldName = entryEventControl.FieldName,
            OldValue = entryEventControl.Value,
            Title = entryEventControl.Title,
            Type = entryEventControl.Type,
            Value = flag1 || !flag2 ? entryEventControl.Value : postedData.FormsData[entryEventControl.FieldName]
          });
        }
      }
      return (IEnumerable<IFormEntryValidationEventControl>) formControlsData;
    }
  }
}
