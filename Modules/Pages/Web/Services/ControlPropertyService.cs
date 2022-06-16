// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.ControlPropertyService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Pages.Model.PropertyLoaders;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>
  /// The WCF service used for working with properties of Sitefinity controls.
  /// </summary>
  /// <remarks>
  /// This service is predominantly used by Sitefinity's property editor.
  /// </remarks>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class ControlPropertyService : IControlPropertyService
  {
    /// <summary>Gets the collection of control properties.</summary>
    /// <param name="controlId">Id of the control for which the properties ought to be retrieved.</param>
    /// <param name="propertyName">Name of the parent property; null if the first level properties ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider from which the persisted properties ought to be retrieved.</param>
    /// <param name="skip">Number of properties to skip before fetching the properties in the collection (used for paging).</param>
    /// <param name="take">Number of properties to take in the collection of the properties (used for paging).</param>
    /// <param name="sortExpression">Sort expression used to order the properties.</param>
    /// <returns>
    /// Collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty" /> objects.
    /// </returns>
    public CollectionContext<WcfControlProperty> GetProperties(
      string controlId,
      string propertyName,
      string providerName,
      string skip,
      string take,
      string sortExpression)
    {
      string pageId = HttpContext.Current.Request.QueryString["pageId"];
      string mediaType = HttpContext.Current.Request.QueryString["mediaType"];
      return this.GetProperties(controlId, propertyName, providerName, skip, take, sortExpression, pageId, mediaType: mediaType);
    }

    /// <summary>Saves the properties.</summary>
    /// <param name="properties">Array of properties to be saved.</param>
    /// <param name="controlId">Id of the control for which the properties ought to be saved.</param>
    /// <param name="providerName">Name of the provider that should be used to save the properties.</param>
    /// <param name="skip">Here only because of WCF URI template issues. Ignore.</param>
    /// <param name="take">Here only because of WCF URI template issues. Ignore.</param>
    /// <param name="sortExpression">Here only because of WCF URI template issues. Ignore.</param>
    public void SaveProperties(
      WcfControlProperty[] properties,
      string controlId,
      string providerName,
      string pageId,
      string mediaType,
      bool checkLiveVersion = false,
      bool upgradePageVersion = false,
      PropertyLocalizationType propertyLocalization = PropertyLocalizationType.Default,
      bool isOpenedByBrowseAndEdit = false)
    {
      this.SaveProperties(true, properties, controlId, providerName, pageId, mediaType, checkLiveVersion, upgradePageVersion, propertyLocalization, isOpenedByBrowseAndEdit);
    }

    internal CollectionContext<WcfControlProperty> GetProperties(
      string controlId,
      string propertyName,
      string providerName,
      string skip,
      string take,
      string sortExpression,
      string pageId,
      WcfPropertyManager propertyManager = null,
      string mediaType = null)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetPropertiesInternal(controlId, propertyName, providerName, skip, take, sortExpression, pageId, propertyManager, mediaType);
    }

    internal CollectionContext<WcfControlProperty> GetPropertiesInternal(
      string controlId,
      string propertyName,
      string providerName,
      string skip,
      string take,
      string sortExpression,
      string pageId,
      WcfPropertyManager propManager = null,
      string mediaType = null)
    {
      propManager = propManager ?? new WcfPropertyManager();
      DesignMediaType result;
      if (!System.Enum.TryParse<DesignMediaType>(mediaType, out result))
        result = DesignMediaType.Page;
      IControlManager manager = result != DesignMediaType.Form ? (IControlManager) PageManager.GetManager(providerName) : (IControlManager) FormsManager.GetManager(providerName);
      Guid pageId1 = !string.IsNullOrEmpty(pageId) ? new Guid(pageId) : Guid.Empty;
      Telerik.Sitefinity.Pages.Model.ControlData controlData;
      try
      {
        controlData = this.GetControlData(new Guid(controlId), pageId1, manager);
      }
      catch (ItemNotFoundException ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "The control was not found. If you are editing a page it might have been unlocked by another user and your changes cannot be saved.", (Exception) null);
      }
      object instance = (object) manager.LoadControl((ObjectData) controlData);
      IList<WcfControlProperty> wcfControlPropertyList1 = (IList<WcfControlProperty>) new List<WcfControlProperty>();
      IList<WcfControlProperty> wcfControlPropertyList2;
      try
      {
        wcfControlPropertyList2 = propManager.GetProperties(instance, controlData, -1, propertyName);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      if (sortExpression == "Category")
        wcfControlPropertyList2 = (IList<WcfControlProperty>) wcfControlPropertyList2.OrderBy<WcfControlProperty, string>((Func<WcfControlProperty, string>) (p => p.CategoryName)).ThenBy<WcfControlProperty, int>((Func<WcfControlProperty, int>) (p => p.InCategoryOrdinal)).ToList<WcfControlProperty>();
      else if (sortExpression == "Name")
        wcfControlPropertyList2 = (IList<WcfControlProperty>) wcfControlPropertyList2.OrderBy<WcfControlProperty, string>((Func<WcfControlProperty, string>) (p => p.PropertyName)).ToList<WcfControlProperty>();
      ServiceUtility.DisableCache();
      return new CollectionContext<WcfControlProperty>((IEnumerable<WcfControlProperty>) wcfControlPropertyList2)
      {
        IsGeneric = false,
        TotalCount = wcfControlPropertyList2.Count
      };
    }

    internal void SaveProperties(
      bool validateChange,
      WcfControlProperty[] properties,
      string controlId,
      string providerName,
      string pageId,
      string mediaType,
      bool checkLiveVersion = false,
      bool upgradePageVersion = false,
      PropertyLocalizationType propertyLocalization = PropertyLocalizationType.Default,
      bool isOpenedByBrowseAndEdit = false)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (!ControlUtilities.IsGuid(controlId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid control ID format.", (Exception) null);
      if (!ControlUtilities.IsGuid(pageId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid page ID format.", (Exception) null);
      WcfPropertyManager wcfPropertyManager = new WcfPropertyManager();
      Guid pageIdGuid = Guid.Parse(pageId);
      ZoneEditorValidationExtensions.ValidateChange(pageIdGuid, (DesignMediaType) System.Enum.Parse(typeof (DesignMediaType), mediaType), nameof (SaveProperties), checkLiveVersion, validateChange);
      DesignMediaType result;
      if (!System.Enum.TryParse<DesignMediaType>(mediaType, out result))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid media type", (Exception) null);
      if (result != DesignMediaType.Form)
      {
        PageManager manager = PageManager.GetManager(providerName);
        Telerik.Sitefinity.Pages.Model.ControlData controlData = manager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(new Guid(controlId));
        if (this.ShouldControlDataBeOverridden(controlData, pageIdGuid, manager))
          controlData = this.GetOverridingControl(controlData, pageIdGuid, manager);
        Guid pageId1 = Guid.Empty;
        if (isOpenedByBrowseAndEdit && result == DesignMediaType.Page)
        {
          Guid publicControlId = controlData.Id;
          pageId1 = ((PageControl) controlData).Page.NavigationNode.Id;
          PageDraft pageDraft = manager.EditPage(((PageControl) controlData).Page.Id);
          manager.Provider.FlushTransaction();
          controlData = (Telerik.Sitefinity.Pages.Model.ControlData) pageDraft.Controls.Where<PageDraftControl>((Func<PageDraftControl, bool>) (c => c.OriginalControlId == publicControlId)).Single<PageDraftControl>();
          pageId = pageDraft.Id.ToString();
          controlId = controlData.Id.ToString();
        }
        object obj = (object) manager.LoadControl((ObjectData) controlData, (CultureInfo) null);
        try
        {
          wcfPropertyManager.SetProperties((IEnumerable<WcfControlProperty>) properties, obj, controlData);
        }
        catch (Exception ex)
        {
          throw;
        }
        if (propertyLocalization == PropertyLocalizationType.AllTranslations || !controlData.IsTranslatable)
          controlData.Strategy = PropertyPersistenceStrategy.NotTranslatable;
        else if (result == DesignMediaType.Page)
        {
          if (manager.GetTemplates().Any<PageTemplate>((Expression<Func<PageTemplate, bool>>) (x => x.Id == pageIdGuid)))
          {
            controlData.Strategy = PropertyPersistenceStrategy.Translatable;
          }
          else
          {
            PageData pageData = checkLiveVersion ? manager.GetPageData(pageIdGuid) : manager.GetDraft<PageDraft>(pageIdGuid).ParentPage;
            if (pageData.NavigationNode.LocalizationStrategy == LocalizationStrategy.Synced && ((IEnumerable<CultureInfo>) pageData.NavigationNode.AvailableCultures).Count<CultureInfo>() > 1)
              controlData.Strategy = PropertyPersistenceStrategy.Translatable;
            else
              controlData.Strategy = PropertyPersistenceStrategy.NotTranslatable;
          }
        }
        else
          controlData.SetPersistanceStrategy();
        manager.ReadProperties(obj, (ObjectData) controlData, SystemManager.CurrentContext.Culture, (object) null);
        if (controlData is PageDraftControl pageDraftControl)
        {
          PageManager pageManager = manager;
          object control = obj;
          IEnumerable<KeyValuePair<string, string>> properties1 = ((IEnumerable<WcfControlProperty>) properties).Select<WcfControlProperty, KeyValuePair<string, string>>((Func<WcfControlProperty, KeyValuePair<string, string>>) (x => new KeyValuePair<string, string>(x.PropertyName, x.PropertyValue)));
          pageManager.UpdatePropertiesInPage(control, pageDraftControl, properties1);
        }
        this.IncreaseDraftVersion(controlData, mediaType, pageId, (IControlManager) manager);
        controlData.IncreaseMultilingualVersion();
        manager.SaveChanges();
        if (!(checkLiveVersion & upgradePageVersion))
          return;
        Guid pageGuid = Guid.Parse(pageId);
        if (!isOpenedByBrowseAndEdit || result != DesignMediaType.Page)
          pageId1 = App.WorkWith().Pages().Get().SelectMany<PageNode, PageData>((Expression<Func<PageNode, IEnumerable<PageData>>>) (x => x.PageDataList)).Where<PageData>((Expression<Func<PageData, bool>>) (x => x.Id == pageGuid)).Select<PageData, Guid>((Expression<Func<PageData, Guid>>) (x => x.NavigationNode.Id)).First<Guid>();
        App.WorkWith().Page(pageId1).AsStandardPage().CheckOut().Publish().SaveChanges();
      }
      else
      {
        FormsManager manager = FormsManager.GetManager(providerName);
        Telerik.Sitefinity.Pages.Model.ControlData control = manager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(new Guid(controlId));
        this.VerifyControlPropertyMetaFieldNamesAreUnique(properties, manager, pageIdGuid, Guid.Parse(controlId));
        object obj = (object) manager.LoadControl((ObjectData) control, (CultureInfo) null);
        try
        {
          wcfPropertyManager.SetProperties((IEnumerable<WcfControlProperty>) properties, obj, control);
        }
        catch (Exception ex)
        {
          throw;
        }
        if (propertyLocalization == PropertyLocalizationType.AllTranslations || !control.IsTranslatable)
          control.Strategy = PropertyPersistenceStrategy.NotTranslatable;
        else
          control.SetPersistanceStrategy();
        manager.ReadProperties(obj, (ObjectData) control, SystemManager.CurrentContext.Culture, (object) null);
        this.IncreaseDraftVersion(control, mediaType, pageId, (IControlManager) manager);
        control.IncreaseMultilingualVersion();
        manager.SaveChanges();
      }
    }

    private Telerik.Sitefinity.Pages.Model.ControlData GetControlData(
      Guid baseControlId,
      Guid pageId,
      IControlManager manager)
    {
      return new WcfPropertyManager().GetOverridingControl(baseControlId, pageId) ?? manager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(baseControlId);
    }

    /// <summary>
    /// Checks if the base control data should be overridden.
    /// The base control data should be overridden if the changed parameters come for a page different from the base control data own page.
    /// </summary>
    /// <param name="baseControl">The base control data object.</param>
    /// <param name="pageId">The Id of the page for which the control parameters are changed.</param>
    /// <returns></returns>
    private bool ShouldControlDataBeOverridden(
      Telerik.Sitefinity.Pages.Model.ControlData baseControl,
      Guid pageId,
      PageManager pageManager)
    {
      if (baseControl is TemplateDraftControl templateDraftControl)
      {
        if (templateDraftControl.PersonalizationMasterId != Guid.Empty)
          templateDraftControl = pageManager.GetControl<TemplateDraftControl>(templateDraftControl.PersonalizationMasterId);
        if (templateDraftControl.Page.Id != pageId)
          return true;
      }
      if (baseControl is TemplateControl templateControl)
      {
        if (templateControl.PersonalizationMasterId != Guid.Empty)
          templateControl = pageManager.GetControl<TemplateControl>(templateControl.PersonalizationMasterId);
        if (templateControl.Page.Id != pageId)
          return true;
      }
      return false;
    }

    /// <summary>Gets the overriding control for specific page</summary>
    /// <param name="baseControl">The base control.</param>
    /// <param name="pageId">The id of the page.</param>
    /// <param name="pageManager">PageManager instance</param>
    /// <returns>The overriding control</returns>
    internal Telerik.Sitefinity.Pages.Model.ControlData GetOverridingControl(
      Telerik.Sitefinity.Pages.Model.ControlData baseControl,
      Guid pageId,
      PageManager pageManager)
    {
      DraftData draftData = pageManager.GetDrafts<DraftData>().Where<DraftData>((Expression<Func<DraftData, bool>>) (d => d.Id == pageId)).FirstOrDefault<DraftData>();
      switch (draftData)
      {
        case PageDraft _:
        case TemplateDraft _:
          Guid segmentId = Guid.Empty;
          if (baseControl.PersonalizationMasterId != Guid.Empty)
          {
            segmentId = baseControl.PersonalizationSegmentId;
            baseControl = pageManager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(baseControl.PersonalizationMasterId);
            if (baseControl.BaseControlId != Guid.Empty)
              baseControl = pageManager.GetControl<Telerik.Sitefinity.Pages.Model.ControlData>(baseControl.BaseControlId);
          }
          Telerik.Sitefinity.Pages.Model.ControlData overridingControl = this.GetExistingOverridingControlOrNull(baseControl.Id, draftData, pageManager) ?? this.CreateOverridingControl(baseControl, draftData, pageId, pageManager);
          if (segmentId != Guid.Empty)
            overridingControl = overridingControl.PersonalizedControls.Where<Telerik.Sitefinity.Pages.Model.ControlData>((Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>) (c => c.PersonalizationSegmentId == segmentId)).FirstOrDefault<Telerik.Sitefinity.Pages.Model.ControlData>();
          return overridingControl;
        default:
          return baseControl;
      }
    }

    private Telerik.Sitefinity.Pages.Model.ControlData GetExistingOverridingControlOrNull(
      Guid baseControlId,
      DraftData draftData,
      PageManager pageManager)
    {
      Telerik.Sitefinity.Pages.Model.ControlData overridingControlOrNull = (Telerik.Sitefinity.Pages.Model.ControlData) null;
      if (draftData is PageDraft)
        overridingControlOrNull = (Telerik.Sitefinity.Pages.Model.ControlData) pageManager.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (prc => prc.IsOverridedControl == true && prc.Page.Id == draftData.Id && prc.BaseControlId == baseControlId)).FirstOrDefault<PageDraftControl>();
      else if (draftData is TemplateDraft)
        overridingControlOrNull = (Telerik.Sitefinity.Pages.Model.ControlData) pageManager.GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (prc => prc.IsOverridedControl == true && prc.Page.Id == draftData.Id && prc.BaseControlId == baseControlId)).FirstOrDefault<TemplateDraftControl>();
      return overridingControlOrNull;
    }

    private Telerik.Sitefinity.Pages.Model.ControlData CreateOverridingControl(
      Telerik.Sitefinity.Pages.Model.ControlData baseControl,
      DraftData draftData,
      Guid pageId,
      PageManager pageManager)
    {
      Telerik.Sitefinity.Pages.Model.ControlData control;
      switch (draftData)
      {
        case PageDraft _:
          control = (Telerik.Sitefinity.Pages.Model.ControlData) pageManager.CreateControl<PageDraftControl>(false);
          break;
        case TemplateDraft _:
          control = (Telerik.Sitefinity.Pages.Model.ControlData) pageManager.CreateControl<TemplateDraftControl>(false);
          break;
        default:
          throw new ArgumentException("Unsupported page type:" + draftData.GetType().FullName);
      }
      Telerik.Sitefinity.Pages.Model.ControlData overridingControl = this.GetClosestOverridingControl(baseControl, pageId);
      this.InitializeOverridingControlFromParent(control, overridingControl, pageManager);
      this.SetControlDataPage(control, draftData);
      return control;
    }

    private Telerik.Sitefinity.Pages.Model.ControlData GetClosestOverridingControl(
      Telerik.Sitefinity.Pages.Model.ControlData baseControl,
      Guid pageId)
    {
      return new WcfPropertyManager().GetOverridingControl(baseControl.Id, pageId) ?? baseControl;
    }

    private void InitializeOverridingControlFromParent(
      Telerik.Sitefinity.Pages.Model.ControlData overridingControl,
      Telerik.Sitefinity.Pages.Model.ControlData parentControl,
      PageManager pageManager)
    {
      overridingControl.IsOverridedControl = true;
      overridingControl.BaseControlId = !(parentControl.BaseControlId != Guid.Empty) ? parentControl.Id : parentControl.BaseControlId;
      pageManager.CopyObject((ObjectData) parentControl, (ObjectData) overridingControl);
      overridingControl.InheritsPermissions = parentControl.InheritsPermissions;
      overridingControl.SupportedPermissionSets = parentControl.SupportedPermissionSets;
      overridingControl.ObjectType = parentControl.ObjectType;
      pageManager.Provider.CopyPermissions((IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) parentControl.Permissions, (IList) overridingControl.Permissions, parentControl.Id, overridingControl.Id, true);
    }

    private void SetControlDataPage(Telerik.Sitefinity.Pages.Model.ControlData overridingControl, DraftData draftData)
    {
      switch (overridingControl)
      {
        case TemplateDraftControl _:
          ((TemplateDraftControl) overridingControl).Page = draftData as TemplateDraft;
          break;
        case PageDraftControl _:
          ((PageDraftControl) overridingControl).Page = draftData as PageDraft;
          break;
      }
    }

    private void IncreaseDraftVersion(
      Telerik.Sitefinity.Pages.Model.ControlData controlData,
      string mediaType,
      string pageId,
      IControlManager manager)
    {
      switch (controlData)
      {
        case PageDraftControl _:
        case TemplateDraftControl _:
        case FormDraftControl _:
          DesignMediaType result;
          if (!System.Enum.TryParse<DesignMediaType>(mediaType, out result))
            break;
          switch (result)
          {
            case DesignMediaType.Page:
            case DesignMediaType.Template:
              Guid id1 = new Guid(pageId);
              ++(manager as PageManager).GetDraft<DraftData>(id1).Version;
              return;
            case DesignMediaType.Form:
              Guid id2 = new Guid(pageId);
              ++(manager as FormsManager).GetDraft(id2).Version;
              return;
            default:
              return;
          }
      }
    }

    private void VerifyControlPropertyMetaFieldNamesAreUnique(
      WcfControlProperty[] properties,
      FormsManager formsManager,
      Guid pageId,
      Guid controlId)
    {
      WcfControlProperty controlProperty1 = ((IEnumerable<WcfControlProperty>) properties).Where<WcfControlProperty>((Func<WcfControlProperty, bool>) (e => e.PropertyName == LinqHelper.MemberName<IMetaField>((Expression<Func<IMetaField, object>>) (x => x.ColumnName)))).FirstOrDefault<WcfControlProperty>();
      WcfControlProperty controlProperty2 = ((IEnumerable<WcfControlProperty>) properties).Where<WcfControlProperty>((Func<WcfControlProperty, bool>) (e => e.PropertyName == LinqHelper.MemberName<IMetaField>((Expression<Func<IMetaField, object>>) (x => x.FieldName)))).FirstOrDefault<WcfControlProperty>();
      if ((controlProperty1 == null || string.IsNullOrEmpty(controlProperty1.PropertyValue)) && (controlProperty2 == null || string.IsNullOrEmpty(controlProperty2.PropertyValue)))
        return;
      FormDraft draft = formsManager.GetDraft(pageId);
      FormDescription form = formsManager.GetForm(draft.ParentForm.Id);
      PropertyDescriptorCollection internalProperties = (PropertyDescriptorCollection) null;
      if (controlProperty2 != null && !string.IsNullOrEmpty(controlProperty2.PropertyValue))
      {
        Type componentType = TypeResolutionService.ResolveType(form.EntriesTypeName, false);
        if (componentType == (Type) null)
          componentType = typeof (FormEntry);
        internalProperties = TypeDescriptor.GetProperties(componentType);
      }
      foreach (FormDraftControl control in (IEnumerable<FormDraftControl>) draft.Controls)
      {
        IMetaField controlMetaField = this.GetControlMetaField(formsManager, (Telerik.Sitefinity.Pages.Model.ControlData) control);
        if (controlMetaField != null)
        {
          this.ValidateColumnNameField(controlMetaField, (Telerik.Sitefinity.Pages.Model.ControlData) control, controlProperty1, controlId, draft.Status);
          this.ValidateFieldNameField((Telerik.Sitefinity.Pages.Model.ControlData) control, controlProperty2, controlId, internalProperties);
        }
      }
      foreach (FormControl control in (IEnumerable<FormControl>) form.Controls)
      {
        IMetaField controlMetaField = this.GetControlMetaField(formsManager, (Telerik.Sitefinity.Pages.Model.ControlData) control);
        if (controlMetaField != null)
        {
          this.ValidateColumnNameField(controlMetaField, (Telerik.Sitefinity.Pages.Model.ControlData) control, controlProperty1, controlId, form.Status);
          this.ValidateFieldNameField((Telerik.Sitefinity.Pages.Model.ControlData) control, controlProperty2, controlId, internalProperties);
        }
      }
    }

    private void ValidateColumnNameField(
      IMetaField field,
      Telerik.Sitefinity.Pages.Model.ControlData control,
      WcfControlProperty controlProperty,
      Guid controlId,
      ContentLifecycleStatus status)
    {
      if (!string.IsNullOrEmpty(field.ColumnName) && controlId != control.Id && field.ColumnName == controlProperty.PropertyValue)
        throw new ArgumentException(status == ContentLifecycleStatus.Temp ? "The ColumnName value is used by another widget in the form. Can't have two widgets with same ColumnName value!" : "Can't have two widgets with same ColumnName value! You have to click on Publish button so that the removed wigets to be permanently deleted!");
    }

    private void ValidateFieldNameField(
      Telerik.Sitefinity.Pages.Model.ControlData control,
      WcfControlProperty controlProperty,
      Guid controlId,
      PropertyDescriptorCollection internalProperties)
    {
      if (internalProperties != null && !string.IsNullOrEmpty(controlProperty.PropertyValue) && control.Id == controlId && internalProperties.Find(controlProperty.PropertyValue, true) != null)
        throw new ArgumentException("Field name " + controlProperty.PropertyValue + " cannot be used. It is reserved by the system.");
    }

    private IMetaField GetControlMetaField(FormsManager formsManager, Telerik.Sitefinity.Pages.Model.ControlData control) => typeof (IFormFieldControl).IsAssignableFrom(FormsManager.GetFormControlType(control)) ? ((IFormFieldControl) ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObject(formsManager.LoadControl((ObjectData) control, (CultureInfo) null))).MetaField : (IMetaField) null;
  }
}
