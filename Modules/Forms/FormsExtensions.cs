// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms
{
  public static class FormsExtensions
  {
    private const string IdPropertyName = "ID";
    private const string SettingsPropertyName = "Settings";
    private const string ModelPropertyName = "Model";
    private const string MetaFieldPropertyName = "MetaField";
    private const string FieldNamePropertyName = "FieldName";
    private const string HiddenPropertyName = "Hidden";

    /// <summary>Gets the meta fields of the FormDescription controls.</summary>
    public static List<MetaField> GetMetaFields(this FormDraft form)
    {
      FormsManager manager = FormsManager.GetManager();
      List<MetaField> metaFields = new List<MetaField>();
      foreach (FormDraftControl control1 in (IEnumerable<FormDraftControl>) form.Controls)
      {
        Control control2 = manager.LoadControl((ObjectData) control1, (CultureInfo) null);
        if (control2 != null && control2 is IFormFieldControl formFieldControl && formFieldControl.MetaField != null)
          metaFields.Add(formFieldControl.MetaField as MetaField);
      }
      return metaFields;
    }

    /// <summary>Gets the hidden field ids.</summary>
    /// <param name="form">The form.</param>
    /// <returns></returns>
    internal static List<string> GetHiddenFieldIds(this FormDraft form) => FormsExtensions.GetHiddenFieldIdsInternal((IEnumerable<ControlData>) form.Controls);

    internal static Dictionary<string, string> GetFieldNames(this FormDraft form) => FormsExtensions.GetFieldNamesInternal((IEnumerable<ControlData>) form.Controls);

    /// <summary>Determines whether the field is hidden.</summary>
    /// <param name="formControl">The form control.</param>
    /// <returns>
    ///   <c>true</c> if the field is hidden; otherwise, <c>false</c>.
    /// </returns>
    internal static bool IsFieldHidden(this FormDraftControl formControl) => FormsExtensions.IsFieldHiddenInternal((ControlData) formControl, out string _);

    /// <summary>
    /// Gets the names of all field controls in the specified form.
    /// </summary>
    /// <param name="form">The form.</param>
    /// <returns></returns>
    public static List<FieldControl> GetFieldControls(string entryType)
    {
      List<FieldControl> fieldControls = (List<FieldControl>) null;
      FormsManager manager = FormsManager.GetManager();
      FormDescription formDescription = manager.GetItems<FormDescription>().ToList<FormDescription>().FirstOrDefault<FormDescription>((Func<FormDescription, bool>) (fd => fd.EntriesTypeName == entryType));
      if (formDescription != null)
      {
        fieldControls = new List<FieldControl>();
        foreach (FormControl control1 in (IEnumerable<FormControl>) formDescription.Controls)
        {
          Control control2 = manager.LoadControl((ObjectData) control1, (CultureInfo) null);
          if (control2 != null && control2 is IFormFieldControl formFieldControl)
          {
            FieldControl fieldControl = formFieldControl as FieldControl;
            fieldControls.Add(fieldControl);
          }
        }
      }
      return fieldControls;
    }

    /// <summary>
    /// Executes the provided predicates depending ot the type of the passed <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEvent" /> instance
    /// </summary>
    /// <typeparam name="T">The type of the result which will be return upon executing the provided predicates</typeparam>
    /// <param name="evt">The <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEvent" /> instance used to determine which predicate to execute</param>
    /// <param name="create">The predicate which will be execute if the form entry event is assignable from <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryCreatedEvent" /> </param>
    /// <param name="update">The predicate which will be execute if the form entry event is assignable from <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryUpdatedEvent" /> </param>
    /// <returns></returns>
    internal static T ExecuteByFormEntryEventType<T>(
      this IFormEntryEvent evt,
      Func<T> create,
      Func<T> update)
    {
      switch (evt)
      {
        case IFormEntryCreatedEvent _:
          return create();
        case IFormEntryUpdatedEvent _:
          return update();
        default:
          throw new ArgumentException("Invalid form entry event");
      }
    }

    /// <summary>
    /// Gets the system library associated with the form if it exists; otherwise new library is created.
    /// </summary>
    /// <param name="forum">Instance of the <see cref="T:Telerik.Sitefinity.Forms.Model.FormDescription" /> for which the library should be returned.</param>
    /// <param name="contentLinksProvider">Provider name of the <see cref="T:Telerik.Sitefinity.Data.ContentLinks.ContentLinksManager" />.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Libraries.Model.Library" /> if exists; otherwise null.</returns>
    internal static Library GetFormLibrary(
      this FormDescription formDescription,
      string contentLinksProvider = null,
      string transactionName = null)
    {
      bool flag = false;
      Library formLibrary = (Library) null;
      if (formDescription.LibraryId != Guid.Empty)
        formLibrary = (Library) FormsExtensions.GetSystemProviderLibrariesManager(transactionName).GetDocumentLibraries().Where<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, bool>>) (l => l.Id == formDescription.LibraryId)).SingleOrDefault<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>();
      if (formLibrary == null)
      {
        if (transactionName == null)
        {
          transactionName = Guid.NewGuid().ToString();
          flag = true;
        }
        LibrariesManager librariesManager = FormsExtensions.GetSystemProviderLibrariesManager(transactionName);
        using (new ElevatedModeRegion((IManager) librariesManager))
        {
          formLibrary = (Library) librariesManager.CreateDocumentLibrary();
          formLibrary.Title = (Lstring) (Res.Get<FormsResources>().FormFiles + ": " + (string) formDescription.Title);
          formLibrary.UrlName = (Lstring) ("form-files-" + formDescription.Name);
          librariesManager.BreakPermiossionsInheritance((ISecuredObject) formLibrary);
          TransactionManager.FlushTransaction(transactionName);
          Permission permission1 = librariesManager.GetPermission("Document", formLibrary.Id, SecurityManager.EveryoneRole.Id);
          if (permission1 != null)
            permission1.Grant = 0;
          Permission permission2 = librariesManager.GetPermission("DocumentLibrary", formLibrary.Id, SecurityManager.EveryoneRole.Id);
          if (permission2 != null)
            permission2.Grant = 0;
          Permission permission3 = librariesManager.GetPermission("Document", formLibrary.Id, SecurityManager.EditorsRole.Id);
          if (permission3 != null)
            permission3.GrantActions(true, "ViewDocument");
          Permission permission4 = librariesManager.GetPermission("DocumentLibrary", formLibrary.Id, SecurityManager.EditorsRole.Id);
          if (permission4 != null)
            permission4.GrantActions(true, "ViewDocumentLibrary");
          librariesManager.RecompileItemUrls<Library>(formLibrary);
          formDescription.LibraryId = formLibrary.Id;
          if (flag)
            TransactionManager.CommitTransaction(transactionName);
          else
            TransactionManager.FlushTransaction(transactionName);
        }
      }
      return formLibrary;
    }

    /// <summary>Adds a file to the form entry.</summary>
    /// <param name="post">Form entry to which the file should be added.</param>
    /// <param name="mediaContent">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" /> that should be attached to the forum post.
    /// </param>
    /// <param name="conentLinksProvider">
    /// Provider name of the <see cref="T:Telerik.Sitefinity.Data.ContentLinks.ContentLinksManager" />.
    /// </param>
    internal static void AddFile(
      this FormEntry entry,
      MediaContent mediaContent,
      string fieldName,
      bool isUpdateMode,
      bool isMultipleAttachments,
      string contentLinksProvider = null)
    {
      ContentLinksManager manager = ContentLinksManager.GetManager(contentLinksProvider);
      IDataProviderBase provider = ((IDataItem) entry).Provider as IDataProviderBase;
      ContentLink contentLink = new ContentLink();
      contentLink.Id = manager.Provider.GetNewGuid();
      contentLink.ParentItemId = entry.Id;
      contentLink.ParentItemProviderName = provider.Name;
      contentLink.ParentItemType = entry.GetType().ToString();
      contentLink.ChildItemId = mediaContent.Id;
      contentLink.ComponentPropertyName = fieldName;
      contentLink.ChildItemAdditionalInfo = (string) mediaContent.Title + mediaContent.Extension + "|" + mediaContent.ResolveMediaUrl(true, (CultureInfo) null) + "|" + mediaContent.FilePath;
      contentLink.ChildItemProviderName = (mediaContent.Provider as DataProviderBase).Name;
      contentLink.ChildItemType = mediaContent.GetType().FullName;
      contentLink.ApplicationName = manager.Provider.ApplicationName;
      if (!(entry.GetValue(fieldName) is ContentLink[] source))
        source = new ContentLink[0];
      List<ContentLink> assetsFieldList = ((IEnumerable<ContentLink>) source).ToList<ContentLink>();
      if (isUpdateMode && assetsFieldList.Count > 0)
      {
        int contentLinkIndex = FormsExtensions.GetContentLinkIndex((IList<ContentLink>) assetsFieldList, mediaContent);
        if (contentLinkIndex != -1)
          assetsFieldList[contentLinkIndex] = contentLink;
        else if (isMultipleAttachments)
          assetsFieldList.Insert(0, contentLink);
        else
          assetsFieldList = new List<ContentLink>()
          {
            contentLink
          };
      }
      else
        assetsFieldList.Insert(0, contentLink);
      ContentLink[] array = assetsFieldList.ToArray();
      entry.SetValue(fieldName, (object) array);
    }

    /// <summary>Gets libraries manager for system provider.</summary>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <exception cref="T:System.ArgumentNullException">Missing libraries provider with 'System' provider group. The library provider should be created with parameter key 'providerGroup' and value 'System'.</exception>
    internal static LibrariesManager GetSystemProviderLibrariesManager(
      string transactionName = null)
    {
      if (ManagerBase<LibrariesDataProvider>.StaticProvidersCollection == null)
        LibrariesManager.GetManager();
      return LibrariesManager.GetManager((ManagerBase<LibrariesDataProvider>.StaticProvidersCollection.Where<LibrariesDataProvider>((Func<LibrariesDataProvider, bool>) (p => p.ProviderGroup == "System")).FirstOrDefault<LibrariesDataProvider>() ?? throw new ArgumentNullException("Missing libraries provider with 'System' provider group. The library provider should be created with parameter key 'providerGroup' and value 'System'.")).Name, transactionName);
    }

    internal static void CreateSubscriptionList(
      FormDescription form,
      Guid subscriptionListId,
      IList<string> emails,
      Action<Guid> createSubscriptionAction)
    {
      ServiceContext serviceContext = FormsModule.GetServiceContext();
      INotificationService notificationService = SystemManager.GetNotificationService();
      if (subscriptionListId == Guid.Empty && emails != null && emails.Count != 0)
      {
        SubscriptionListRequestProxy subscriptionList = new SubscriptionListRequestProxy()
        {
          Description = (string) ((Content) form).Description
        };
        subscriptionListId = notificationService.CreateSubscriptionList(serviceContext, (ISubscriptionListRequest) subscriptionList);
        createSubscriptionAction(subscriptionListId);
      }
      IEnumerable<ISubscriberResponse> subscribers = notificationService.GetSubscribers(serviceContext, subscriptionListId, (QueryParameters) null);
      HashSet<string> stringSet = new HashSet<string>(subscribers.Select<ISubscriberResponse, string>((Func<ISubscriberResponse, string>) (s => s.ResolveKey)), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      HashSet<string> source = new HashSet<string>(subscribers.Where<ISubscriberResponse>((Func<ISubscriberResponse, bool>) (s => s.Email.ToLower() == s.ResolveKey.ToLower())).Select<ISubscriberResponse, string>((Func<ISubscriberResponse, string>) (s => s.Email)), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      if (emails != null)
      {
        foreach (string str in emails.Distinct<string>())
        {
          string mail = str;
          string resolveKey = source.FirstOrDefault<string>((Func<string, bool>) (e => e.ToLower() == mail.ToLower()));
          if (!string.IsNullOrEmpty(resolveKey))
          {
            if (resolveKey != mail)
              notificationService.UpdateSubscriber(serviceContext, resolveKey, (ISubscriberRequest) new SubscriberRequestProxy()
              {
                Email = mail
              });
            source.Remove(mail);
          }
          else if (!string.IsNullOrWhiteSpace(mail) && !stringSet.Contains(mail))
            notificationService.Subscribe(serviceContext, subscriptionListId, (ISubscriberRequest) new SubscriberRequestProxy()
            {
              Email = mail,
              ResolveKey = mail
            });
        }
      }
      foreach (string subscriberResolveKey in source)
        notificationService.Unsubscribe(serviceContext, subscriptionListId, subscriberResolveKey);
    }

    private static int GetContentLinkIndex(
      IList<ContentLink> assetsFieldList,
      MediaContent mediaContent)
    {
      for (int index = 0; index < assetsFieldList.Count; ++index)
      {
        if (assetsFieldList[index].GetLinkedItem() is MediaContent linkedItem && linkedItem.Extension.Equals(mediaContent.Extension, StringComparison.CurrentCultureIgnoreCase) && linkedItem.Title.Equals((string) mediaContent.Title, StringComparison.CurrentCultureIgnoreCase))
          return index;
      }
      return -1;
    }

    private static List<string> GetHiddenFieldIdsInternal(IEnumerable<ControlData> formControls)
    {
      List<string> fieldIdsInternal = new List<string>();
      foreach (ControlData formControl in formControls)
      {
        string fieldId;
        if (formControl.Properties != null && FormsExtensions.IsFieldHiddenInternal(formControl, out fieldId))
          fieldIdsInternal.Add(fieldId);
      }
      return fieldIdsInternal;
    }

    private static Dictionary<string, string> GetFieldNamesInternal(
      IEnumerable<ControlData> formControls)
    {
      Dictionary<string, string> fieldNamesInternal = new Dictionary<string, string>();
      foreach (ControlData formControl in formControls)
      {
        ControlProperty controlProperty1 = formControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Settings"));
        if (controlProperty1 != null && controlProperty1.ChildProperties != null)
        {
          ControlProperty controlProperty2 = controlProperty1.ChildProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Model"));
          if (controlProperty2 != null && controlProperty2.ChildProperties != null)
          {
            ControlProperty controlProperty3 = controlProperty2.ChildProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "MetaField"));
            if (controlProperty3 != null && controlProperty3.ChildProperties != null)
            {
              ControlProperty controlProperty4 = formControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID"));
              ControlProperty controlProperty5 = controlProperty3.ChildProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "FieldName"));
              if (controlProperty4 != null && controlProperty5 != null)
                fieldNamesInternal.Add(controlProperty4.Value, controlProperty5.Value);
            }
          }
        }
      }
      return fieldNamesInternal;
    }

    private static bool IsFieldHiddenInternal(ControlData formControl, out string fieldId)
    {
      fieldId = (string) null;
      ControlProperty controlProperty1 = formControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Settings"));
      if (controlProperty1 != null && controlProperty1.ChildProperties != null)
      {
        ControlProperty controlProperty2 = controlProperty1.ChildProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Model"));
        if (controlProperty2 != null && controlProperty2.ChildProperties != null)
        {
          ControlProperty controlProperty3 = controlProperty2.ChildProperties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Hidden"));
          bool result;
          if (((controlProperty3 == null ? 0 : (bool.TryParse(controlProperty3.Value, out result) ? 1 : 0)) & (result ? 1 : 0)) != 0)
          {
            ControlProperty controlProperty4 = formControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID"));
            if (controlProperty4 != null)
              fieldId = controlProperty4.Value;
          }
        }
      }
      return !string.IsNullOrEmpty(fieldId);
    }
  }
}
