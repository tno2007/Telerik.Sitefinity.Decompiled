// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.ModuleEditorService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services
{
  /// <summary>
  /// A web service which provides methods for the module editor feature.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class ModuleEditorService : IModuleEditorService
  {
    public CollectionContext<FieldViewModel> GetDefaultFields(
      string contentType,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ServiceUtility.DisableCache();
      return ServiceUtility.HandleCollectionParameters<FieldViewModel>(this.GetDefaultFields(this.ResolveContentType(contentType)), sortExpression, skip, take, filter);
    }

    public CollectionContext<FieldViewModel> GetDefaultFieldsInXml(
      string contentType,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ServiceUtility.DisableCache();
      return ServiceUtility.HandleCollectionParameters<FieldViewModel>(this.GetDefaultFields(this.ResolveContentType(contentType)), sortExpression, skip, take, filter);
    }

    public CollectionContext<FieldViewModel> GetCustomFields(
      string contentType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ServiceUtility.DisableCache();
      return ServiceUtility.HandleCollectionParameters<FieldViewModel>((IEnumerable<FieldViewModel>) this.GetCustomFields(this.ResolveContentType(contentType)).OrderBy<FieldViewModel, string>((Func<FieldViewModel, string>) (x => x.Name)), sortExpression, skip, take, filter);
    }

    public CollectionContext<FieldViewModel> GetCustomFieldsInXml(
      string contentType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ServiceUtility.DisableCache();
      return ServiceUtility.HandleCollectionParameters<FieldViewModel>(this.GetCustomFields(this.ResolveContentType(contentType)), sortExpression, skip, take, filter);
    }

    public CollectionContext<WcfDetailFormViewData> GetDetailFormViewNames(
      string itemType)
    {
      return this.GetViewsInternal(this.ResolveContentType(itemType));
    }

    public CollectionContext<WcfDetailFormViewData> GetDetailFormViewNamesInXml(
      string itemType)
    {
      return this.GetViewsInternal(this.ResolveContentType(itemType));
    }

    public void ApplyChanges(ModuleEditorContext context, string providerName) => this.ApplyChangesInternal(context, providerName);

    public void ApplyChangesInXml(ModuleEditorContext context, string providerName) => this.ApplyChangesInternal(context, providerName);

    private IEnumerable<FieldViewModel> GetFields(
      Type type,
      Func<PropertyDescriptor, bool> predicate)
    {
      return FieldHelper.GetFields(type, true).Where<PropertyDescriptor>(predicate).Select<PropertyDescriptor, FieldViewModel>((Func<PropertyDescriptor, FieldViewModel>) (prop => new FieldViewModel(prop)
      {
        Name = prop.Name,
        IsCustom = false,
        ContentType = UserFriendlyTypeResolver.ResolveTypeToString(prop),
        FieldTypeName = UserFriendlyTypeResolver.ResolveType(prop).ToString()
      }));
    }

    private static bool IsCustomContentLinksProperty(PropertyDescriptor prop) => prop is ContentLinksPropertyDescriptor propertyDescriptor && propertyDescriptor.MetaField != null;

    private static bool IsHiddenProperty(PropertyDescriptor prop) => prop.Attributes[typeof (HiddenPropertyAttribute)] is HiddenPropertyAttribute attribute && attribute.Hidden;

    private IEnumerable<FieldViewModel> GetDefaultFields(Type type) => this.GetFields(type, (Func<PropertyDescriptor, bool>) (propDesc =>
    {
      switch (propDesc)
      {
        case MetafieldPropertyDescriptor _:
        case TaxonomyPropertyDescriptor _:
        case DynamicLstringPropertyDescriptor _:
label_3:
          return false;
        default:
          if (!ModuleEditorService.IsCustomContentLinksProperty(propDesc) && !(propDesc is RelatedDataPropertyDescriptor))
            return !ModuleEditorService.IsHiddenProperty(propDesc);
          goto label_3;
      }
    }));

    private IEnumerable<FieldViewModel> GetCustomFields(Type type) => this.GetFields(type, (Func<PropertyDescriptor, bool>) (propDesc =>
    {
      switch (propDesc)
      {
        case MetafieldPropertyDescriptor _:
        case TaxonomyPropertyDescriptor _:
        case DynamicLstringPropertyDescriptor _:
        case RelatedDataPropertyDescriptor _:
          return true;
        default:
          return ModuleEditorService.IsCustomContentLinksProperty(propDesc);
      }
    }));

    private void ApplyChangesInternal(ModuleEditorContext context, string providerName)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity == null || !currentIdentity.IsUnrestricted)
        throw new InvalidOperationException(Res.Get<ModuleEditorResources>().CannotSaveChanges);
      Type contentType = TypeResolutionService.ResolveType(context.ContentType);
      CustomFieldsContext customFieldsContext = new CustomFieldsContext(contentType);
      IDictionary<string, WcfField> addFields = context.AddFields;
      if (addFields.Count > 0)
        customFieldsContext.AddOrUpdateCustomFields(addFields, contentType.Name);
      IList<string> removeFields = context.RemoveFields;
      if (removeFields.Count > 0)
        customFieldsContext.RemoveCustomFields(removeFields, contentType.Name);
      customFieldsContext.SaveChanges();
    }

    private CollectionContext<WcfDetailFormViewData> GetViewsInternal(
      Type itemType)
    {
      List<DetailFormViewElement> views = CustomFieldsContext.GetViews(itemType.FullName);
      List<WcfDetailFormViewData> items = new List<WcfDetailFormViewData>();
      foreach (DetailFormViewElement detailFormViewElement in views)
      {
        string str1 = detailFormViewElement.ControlDefinitionName + " > " + detailFormViewElement.ViewName;
        string str2 = !string.IsNullOrEmpty(detailFormViewElement.Title) ? Res.ResolveLocalizedValue(detailFormViewElement.ResourceClassId, detailFormViewElement.Title) : str1;
        if (detailFormViewElement.ViewName.Contains("Frontend"))
          str2 = "'" + str2 + "' " + Res.ResolveLocalizedValue(typeof (Labels).Name, "InTheFrontend");
        else if (detailFormViewElement.ViewName.Contains("Backend"))
          str2 = "'" + str2 + "' " + Res.ResolveLocalizedValue(typeof (Labels).Name, "InTheBackend");
        items.Add(new WcfDetailFormViewData()
        {
          Name = str1,
          Title = str2
        });
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<WcfDetailFormViewData>((IEnumerable<WcfDetailFormViewData>) items);
    }

    private Type ResolveContentType(string typeName)
    {
      Type type = (Type) null;
      int num = 0;
      for (bool flag = false; !flag && num <= 30; ++num)
      {
        type = TypeResolutionService.ResolveType(typeName, false);
        if (type == (Type) null)
          Thread.Sleep(1000);
        else
          break;
      }
      if (type == (Type) null)
        type = TypeResolutionService.ResolveType(typeName);
      return type;
    }
  }
}
