// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.DynamicPermissionHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  internal static class DynamicPermissionHelper
  {
    private static readonly DateTime defaultDateTimedefaultDateTime = new DateTime(1900, 1, 1);
    private static object secRootLockObject = new object();

    /// <summary>
    /// Applies permissions per field to the specified data item.
    /// If the current user has no view permission for a specific field of the data item, then this field is set to the default value for its type.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    public static void ApplyViewPermissions(this DynamicContent dataItem)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      Type itemType = dataItem.GetType();
      if (!manager.ShouldCheckPermissionsPerField(itemType.FullName))
        return;
      if (ClaimsManager.GetCurrentIdentity() == null)
        throw new InvalidOperationException(Res.Get<SecurityResources>().MissingCurrentPrincipal);
      PropertyDescriptorCollection properties = new DynamicFieldsTypeDescriptionProvider().GetTypeDescriptor(itemType, (object) dataItem).GetProperties();
      string[] fieldNames = DynamicPermissionHelper.GetFieldsNames(properties).ToArray();
      string providerName = dataItem.GetProviderName();
      List<DynamicModuleField> list = manager.GetDynamicModuleFields().Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.FieldNamespace == itemType.FullName && fieldNames.Contains<string>(f.Name) && !f.IsTransient)).ToList<DynamicModuleField>();
      IEnumerable<ISecuredObject> securedObjects = manager.GetSecuredObjects(((IEnumerable<ISecuredObject>) list).AsEnumerable<ISecuredObject>(), providerName);
      foreach (DynamicModuleField dynamicModuleField in list)
      {
        DynamicModuleField field = dynamicModuleField;
        field.Owner = dataItem.Owner;
        ISecuredObject securedObject = securedObjects.Single<ISecuredObject>((Func<ISecuredObject, bool>) (s =>
        {
          if (s.Id == field.Id)
            return true;
          return s is DynamicContentProvider && ((DynamicContentProvider) s).ParentSecuredObjectId == field.Id;
        }));
        PropertyDescriptor property = properties[field.Name];
        string permissionSetName = field.GetPermissionSetName();
        string[] strArray = new string[1]{ "View" };
        if (!securedObject.IsGranted(permissionSetName, strArray))
          DynamicPermissionHelper.SetDefaultValueToProperty(dataItem, property);
      }
    }

    /// <summary>
    /// Determines whether the specified action that must be of the General set name is granted for the specified <paramref name="dataItem" />.
    /// Also checks if the specified <paramref name="action" /> is granted for all fields of the <paramref name="dataItem" />.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="context">The context.</param>
    [Obsolete("Use the IsFieldPermissionsGranted extension method of DynamicContent when you wish to demand field permissions and IsGranted extension method on ISecuredObject to demand actions on the DynamicContent item itself. This method was used when DynamicContent didn't implement ISecuredObject and permissions were evaluated on the DynamicModuleType of the Dynamic content.")]
    public static bool IsPermissionGranted(
      this DynamicContent dataItem,
      string action,
      bool resetDefaultValues = true,
      CultureInfo culture = null)
    {
      return dataItem.IsGranted("General", action) && dataItem.IsFieldPermissionsGranted(action, resetDefaultValues, culture);
    }

    /// <summary>
    /// Determines whether specified <paramref name="action" /> is granted for all fields.
    /// of the specified <paramref name="dataItem" />.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="context">The context.</param>
    public static bool IsFieldPermissionsGranted(
      this DynamicContent dataItem,
      string action,
      bool resetDefaultValues = true,
      CultureInfo culture = null)
    {
      if (ClaimsManager.GetCurrentIdentity() == null)
        throw new InvalidOperationException(Res.Get<SecurityResources>().MissingCurrentPrincipal);
      ModuleBuilderManager manager1 = ModuleBuilderManager.GetManager();
      Type itemType = dataItem.GetType();
      if (!manager1.GetDynamicModuleType(itemType).CheckFieldPermissions)
        return true;
      PropertyDescriptorCollection properties = new DynamicFieldsTypeDescriptionProvider().GetTypeDescriptor(itemType, (object) dataItem).GetProperties();
      string[] fieldNames = DynamicPermissionHelper.GetFieldsNames(properties).ToArray();
      string providerName = dataItem.GetProviderName();
      List<DynamicModuleField> list = manager1.GetDynamicModuleFields().Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (f => f.FieldNamespace == itemType.FullName && fieldNames.Contains<string>(f.Name) && !f.IsTransient)).ToList<DynamicModuleField>();
      IEnumerable<ISecuredObject> securedObjects = manager1.GetSecuredObjects(((IEnumerable<ISecuredObject>) list).AsEnumerable<ISecuredObject>(), providerName);
      List<ContentLink> allDBLinks = (List<ContentLink>) null;
      ContentLinksManager manager2 = ContentLinksManager.GetManager();
      foreach (DynamicModuleField dynamicModuleField in list)
      {
        DynamicModuleField field = dynamicModuleField;
        field.Owner = dataItem.Owner;
        ISecuredObject securedObject = securedObjects.Single<ISecuredObject>((Func<ISecuredObject, bool>) (s =>
        {
          if (s.Id == field.Id)
            return true;
          return s is DynamicContentProvider && ((DynamicContentProvider) s).ParentSecuredObjectId == field.Id;
        }));
        if (action == "Modify")
        {
          if (!DynamicPermissionHelper.IsModifyPermissionGranted(properties, field, securedObject, dataItem, manager2, resetDefaultValues, allDBLinks, culture))
            return false;
        }
        else if (!DynamicPermissionHelper.IsCreatePermissionGranted(properties, field, securedObject, dataItem))
          return false;
      }
      return true;
    }

    /// <summary>
    /// Gets the secured object for the specified dynamic module provider.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="mainSecuredObject">The main secured object.</param>
    /// <param name="dynamicDataProviderName">Name of the dynamic data provider.</param>
    internal static ISecuredObject GetSecuredObject(
      IDynamicModuleSecurityManager manager,
      ISecuredObject mainSecuredObject,
      string dynamicDataProviderName = null)
    {
      ISecuredObject securedObject = manager.GetSecuredObject(mainSecuredObject, dynamicDataProviderName);
      if (securedObject == null)
      {
        lock (DynamicPermissionHelper.secRootLockObject)
        {
          securedObject = manager.GetSecuredObject(mainSecuredObject, dynamicDataProviderName);
          if (securedObject == null)
          {
            securedObject = manager.CreateSecuredObject(mainSecuredObject, dynamicDataProviderName);
            if (manager.TransactionName.IsNullOrEmpty())
              manager.SaveChanges();
          }
        }
      }
      if (mainSecuredObject is IOwnership && securedObject is IOwnership)
        ((IOwnership) securedObject).Owner = ((IOwnership) mainSecuredObject).Owner;
      return securedObject;
    }

    private static bool IsModifyPermissionGranted(
      PropertyDescriptorCollection properties,
      DynamicModuleField field,
      ISecuredObject securedObject,
      DynamicContent dataItem,
      ContentLinksManager contentLinksManager,
      bool resetDefaultValues,
      List<ContentLink> allDBLinks,
      CultureInfo culture)
    {
      culture = culture.GetLstring();
      PropertyDescriptor property = properties[field.Name];
      string permissionSetName = field.GetPermissionSetName();
      object currentValue = property.GetValue((object) dataItem);
      if (property.PropertyType == typeof (ContentLink[]))
      {
        if (allDBLinks == null)
        {
          Guid parentId = dataItem.Id;
          if (dataItem.Status != ContentLifecycleStatus.Master)
            parentId = dataItem.OriginalContentId;
          allDBLinks = contentLinksManager.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentId)).ToList<ContentLink>();
        }
        ContentLink[] originalContentLinks;
        if (DynamicPermissionHelper.HaveContentLinksChanged(dataItem, property, allDBLinks, out originalContentLinks))
        {
          if (!securedObject.IsGranted(permissionSetName, "Modify"))
          {
            if (!DynamicPermissionHelper.IsDefaultPropertyValue(property, currentValue, culture))
              return false;
            if (securedObject.IsGranted(permissionSetName, "View") || !resetDefaultValues)
              return false;
            List<ContentLink> contentLinkList = new List<ContentLink>();
            foreach (ContentLink target in originalContentLinks)
            {
              ContentLink source = new ContentLink();
              source.CopyContentLinkProperties(target);
              contentLinkList.Add(source);
            }
            property.SetValue((object) dataItem, (object) contentLinkList.ToArray());
          }
        }
      }
      else
      {
        DynamicModuleDataProvider provider = dataItem.Provider as DynamicModuleDataProvider;
        string name = property.Name;
        if (culture != CultureInfo.InvariantCulture)
        {
          switch (property)
          {
            case DynamicLstringPropertyDescriptor propertyDescriptor:
              property = propertyDescriptor.GetPropertyDescriptor((object) dataItem, culture);
              break;
            case LocalizableDescriptorBase localizableDescriptorBase:
              property = localizableDescriptorBase.GetPropertyDescriptor((object) dataItem, culture);
              break;
          }
        }
        object originalValue = DynamicPermissionHelper.GetOriginalValue(provider, dataItem, property);
        if (DynamicPermissionHelper.HasPropertyValueChanged(property, originalValue, currentValue))
        {
          if (!securedObject.IsGranted(permissionSetName, "Modify"))
          {
            if (!DynamicPermissionHelper.IsDefaultPropertyValue(property, currentValue, culture))
              return false;
            if (securedObject.IsGranted(permissionSetName, "View") || !resetDefaultValues)
              return false;
            DynamicPermissionHelper.SetOriginalValueBack(provider, property, dataItem, originalValue);
          }
        }
      }
      return true;
    }

    private static bool IsCreatePermissionGranted(
      PropertyDescriptorCollection properties,
      DynamicModuleField field,
      ISecuredObject securedObject,
      DynamicContent dataItem)
    {
      PropertyDescriptor property = properties[field.Name];
      string permissionSetName = field.GetPermissionSetName();
      if (securedObject.IsGranted(permissionSetName, "Modify"))
        return true;
      if (field.IsRequired)
        return false;
      if (field.FieldType == FieldType.DateTime || field.FieldType == FieldType.MultipleChoice || field.FieldType == FieldType.Choices)
        property.SetValue((object) dataItem, (object) null);
      else
        DynamicPermissionHelper.SetDefaultValueToProperty(dataItem, property);
      return true;
    }

    private static void SetOriginalValueBack(
      DynamicModuleDataProvider provider,
      PropertyDescriptor property,
      DynamicContent dataItem,
      object originalValue)
    {
      if (property.PropertyType == typeof (Address))
        AddressFieldPermissionsHelper.SetOriginalValueBack(provider, property, dataItem, originalValue);
      else
        property.SetValue((object) dataItem, originalValue);
    }

    private static bool HasPropertyValueChanged(
      PropertyDescriptor property,
      object originalValue,
      object currentValue)
    {
      return property.PropertyType == typeof (Address) ? AddressFieldPermissionsHelper.HasPropertyValueChanged(property, originalValue, currentValue) : !originalValue.EqualsTo(currentValue);
    }

    private static object GetOriginalValue(
      DynamicModuleDataProvider provider,
      DynamicContent dataItem,
      PropertyDescriptor property)
    {
      return property.PropertyType == typeof (Address) ? AddressFieldPermissionsHelper.GetOriginalValue(provider, dataItem, property) : provider.GetOriginalValue<object>(dataItem, property.Name);
    }

    private static bool IsDefaultPropertyValue(
      PropertyDescriptor property,
      object currentValue,
      CultureInfo culture)
    {
      return property.PropertyType == typeof (Address) ? AddressFieldPermissionsHelper.IsDefaultPropertyValue(property, currentValue, culture) : property.IsDefaultValue(currentValue, culture);
    }

    private static bool EqualsTo(this object originalValue, object currentValue)
    {
      switch (currentValue)
      {
        case string[] _:
          if (currentValue == null)
            return originalValue == null;
          return originalValue != null && string.Join(",", (string[]) originalValue) == string.Join(",", (string[]) currentValue);
        case Guid[] _ when currentValue != null:
          Guid[] guidArray1 = (Guid[]) currentValue;
          Guid[] guidArray2 = (Guid[]) originalValue;
          if (guidArray2 == null || guidArray1.Length != guidArray2.Length)
            return false;
          for (int index = 0; index < guidArray1.Length; ++index)
          {
            if (guidArray1[index] != guidArray2[index])
              return false;
          }
          return true;
        case null:
          return originalValue == null;
        default:
          return currentValue.Equals(originalValue);
      }
    }

    private static bool HaveContentLinksChanged(
      DynamicContent item,
      PropertyDescriptor property,
      List<ContentLink> allDBLinks,
      out ContentLink[] originalContentLinks)
    {
      originalContentLinks = (ContentLink[]) null;
      if (property.PropertyType == typeof (ContentLink[]) && property.GetValue((object) item) is ContentLink[] source)
      {
        originalContentLinks = allDBLinks.Where<ContentLink>((Func<ContentLink, bool>) (cl => cl.ComponentPropertyName == property.Name)).ToArray<ContentLink>();
        foreach (ContentLink contentLink in originalContentLinks)
        {
          ContentLink dbLink = contentLink;
          if (!((IEnumerable<ContentLink>) source).Any<ContentLink>((Func<ContentLink, bool>) (cl => cl.ChildItemId == dbLink.ChildItemId)))
            return true;
        }
        foreach (ContentLink contentLink in source)
        {
          ContentLink dbLink = contentLink;
          if (!((IEnumerable<ContentLink>) originalContentLinks).Any<ContentLink>((Func<ContentLink, bool>) (cl => cl.ChildItemId == dbLink.ChildItemId)))
            return true;
        }
      }
      return false;
    }

    private static List<string> GetFieldsNames(PropertyDescriptorCollection properties)
    {
      List<string> fieldsNames = new List<string>();
      foreach (PropertyDescriptor property in properties)
      {
        switch (property)
        {
          case TaxonomyPropertyDescriptor _:
          case MetafieldPropertyDescriptor _:
          case DynamicLstringPropertyDescriptor _:
            fieldsNames.Add(property.Name);
            continue;
          default:
            continue;
        }
      }
      return fieldsNames;
    }

    private static void SetDefaultValueToProperty(
      DynamicContent dataItem,
      PropertyDescriptor property)
    {
      if (property.PropertyType == typeof (Address))
      {
        Address address = (Address) property.GetValue((object) dataItem);
        if (address != null)
        {
          address.Street = (string) null;
          address.Zip = (string) null;
          address.StateCode = (string) null;
          address.CountryCode = (string) null;
          address.City = (string) null;
          address.Latitude = new double?();
          address.Longitude = new double?();
          address.MapZoomLevel = new int?();
        }
        property.SetValue((object) dataItem, (object) address);
      }
      else
      {
        object defaultPropertyValue = property.GetDefaultPropertyValue();
        property.SetValue((object) dataItem, defaultPropertyValue);
      }
    }

    private static object GetDefaultPropertyValue(this PropertyDescriptor property)
    {
      if (property is TaxonomyPropertyDescriptor)
        return (object) new TrackedList<Guid>();
      if (property.PropertyType == typeof (ContentLink[]))
        return (object) new ContentLink[0];
      if (property.PropertyType == typeof (Lstring))
        return (object) new Lstring();
      Type type = property.GetType();
      object defaultPropertyValue = type.IsValueType ? Activator.CreateInstance(type) : (object) null;
      if (property.PropertyType == typeof (string) && defaultPropertyValue == null)
        defaultPropertyValue = (object) string.Empty;
      if (property.PropertyType == typeof (DateTime))
        defaultPropertyValue = (object) DynamicPermissionHelper.defaultDateTimedefaultDateTime.ToUniversalTime();
      return defaultPropertyValue;
    }

    private static bool IsDefaultValue(
      this PropertyDescriptor property,
      object value,
      CultureInfo culture)
    {
      if (value is TrackedList<Guid>)
        return ((TrackedList<Guid>) value).Count == 0;
      if (value is ContentLink[])
        return ((ContentLink[]) value).Length == 0;
      if ((object) (value as Lstring) != null)
        return ((Lstring) value).GetStringNoFallback(culture) == string.Empty;
      object originalValue = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : (object) null;
      if (property.PropertyType == typeof (string))
        return (string) value == string.Empty;
      return property.PropertyType == typeof (DateTime) ? (DateTime) value == DynamicPermissionHelper.defaultDateTimedefaultDateTime.ToUniversalTime() : originalValue.EqualsTo(value);
    }
  }
}
