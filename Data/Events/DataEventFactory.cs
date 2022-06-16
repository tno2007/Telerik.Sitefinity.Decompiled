// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.DataEventFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Data.Events
{
  internal class DataEventFactory
  {
    public static IDataEvent CreateEvent(IDataItem dataItem, string action) => DataEventFactory.CreateEvent(dataItem, action, (string) null);

    public static IDataEvent CreateEvent(
      IDataItem dataItem,
      string action,
      string language)
    {
      if (language == null)
        language = SystemManager.CurrentContext.Culture.Name;
      CultureInfo cultureInfo = CultureInfo.GetCultureInfo(language);
      string actualItemAction = DataEventFactory.GetActualItemAction(dataItem, action, cultureInfo);
      DataEvent eventInternal = DataEventFactory.CreateEventInternal(dataItem, actualItemAction, cultureInfo);
      string itemTitle = DataEventFactory.GetItemTitle(dataItem, action, cultureInfo);
      eventInternal.Title = itemTitle;
      eventInternal.SetIApprovalWorkflowEventPropertiesFromTrackingContext(dataItem);
      eventInternal.SetIRecyclableEventPropertiesFromTrackingContext(dataItem);
      if (actualItemAction == SecurityConstants.TransactionActionType.Updated.ToString())
        DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) eventInternal, dataItem, cultureInfo);
      return (IDataEvent) eventInternal;
    }

    internal static DataEvent CreateEventInternal(
      IDataItem dataItem,
      string action,
      CultureInfo culture)
    {
      DataEvent eventInternal = new DataEvent()
      {
        ItemId = dataItem.Id,
        ItemType = dataItem.GetType(),
        Action = action,
        TransactionName = dataItem.Transaction == null ? (string) null : dataItem.Transaction.ToString(),
        Status = (string) null,
        Visible = false,
        OriginalContentId = Guid.Empty
      };
      if (culture != null && culture != CultureInfo.InvariantCulture)
        eventInternal.Language = culture.Name;
      eventInternal.ProviderName = DataEventFactory.GetProviderName(dataItem.Provider);
      if (dataItem is ILifecycleDataItem lifecycleDataItem)
      {
        eventInternal.Visible = lifecycleDataItem.Visible;
        eventInternal.Status = lifecycleDataItem.Status.ToString();
      }
      if (dataItem is IApprovalWorkflowItem approvalWorkflowItem)
        eventInternal.ApprovalWorkflowState = approvalWorkflowItem.ApprovalWorkflowState[culture];
      if (dataItem is ILifecycleDataItemGeneric lifecycleDataItemGeneric && lifecycleDataItemGeneric.OriginalContentId != Guid.Empty)
        eventInternal.OriginalContentId = lifecycleDataItemGeneric.OriginalContentId;
      return eventInternal;
    }

    /// <summary>Gets the name of a provider</summary>
    /// <param name="provider">The provider</param>
    /// <returns>The name of the provider</returns>
    public static string GetProviderName(object provider) => ObjectFactory.Resolve<IProviderNameResolver>().GetProviderName(provider);

    /// <summary>
    /// Retrieves and sets the changed properties for IPropertyChangeDataEvent
    /// </summary>
    /// <param name="evt">The event</param>
    /// <param name="dataItem">The data item from which the changed properties will be retrieved</param>
    /// <param name="culture">The culture where the item was modified</param>
    public static void SetChangedProperties(
      IPropertyChangeDataEvent evt,
      IDataItem dataItem,
      CultureInfo culture)
    {
      DataEventFactory.GetChangedProperties(dataItem, culture).ForEach<string, PropertyChange>((System.Action<string, PropertyChange>) ((key, prop) => evt.ChangedProperties.Add(key, prop)));
    }

    /// <summary>Retrieves the changed properties for an IDataItem</summary>
    /// <param name="dataItem">The data item from which the changed properties will be retrieved.</param>
    /// <param name="culture">The culture where the item was modified.</param>
    /// <returns>Returns a dictionary containing all changed properties.</returns>
    public static Dictionary<string, PropertyChange> GetChangedProperties(
      IDataItem dataItem,
      CultureInfo culture)
    {
      Dictionary<string, PropertyChange> changedProperties = new Dictionary<string, PropertyChange>();
      DataProviderBase itemProvider = DataEventFactory.GetItemProvider(dataItem);
      if (itemProvider != null)
      {
        SitefinityOAContext transaction = (SitefinityOAContext) itemProvider.GetTransaction();
        IEnumerable<string> memberNames = transaction.GetMemberNames((object) dataItem, ObjectState.Dirty);
        Dictionary<string, PropertyDescriptor> dictionary = new Dictionary<string, PropertyDescriptor>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
        foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties((object) dataItem).Cast<PropertyDescriptor>())
        {
          if (!dictionary.ContainsKey(propertyDescriptor.Name))
            dictionary.Add(propertyDescriptor.Name, propertyDescriptor);
        }
        foreach (string str in memberNames)
        {
          string actualPropertyName = DataEventFactory.GetActualPropertyName(str, culture);
          if (dictionary.ContainsKey(actualPropertyName))
          {
            PropertyDescriptor realProp = dictionary[actualPropertyName];
            object currentValue = DataEventFactory.GetCurrentValue(realProp, dataItem);
            object originalValue = DataEventFactory.GetOriginalValue(transaction, dataItem, str);
            object obj = !(realProp.PropertyType == typeof (Lstring)) || currentValue == null ? currentValue : (object) currentValue.ToString();
            if ((originalValue != null && !originalValue.Equals(obj) || originalValue == null && obj != null) && !changedProperties.ContainsKey(actualPropertyName))
              changedProperties.Add(actualPropertyName, new PropertyChange()
              {
                PropertyName = actualPropertyName,
                OldValue = originalValue,
                NewValue = obj
              });
          }
        }
      }
      return changedProperties;
    }

    /// <summary>
    /// ML uses only one record for localizable items, only the first language created is considered New by OA.
    /// Any new language added after that will be considered as update.
    /// This method gets the actual action for the item.
    /// Basically if the "required" property for some item (in most cases the "Title" property) has old value "null", the action should be created.
    /// There is only one case, when this is not true - when the user switches from mono to ML - however, the "New" status will be logged only once per item.
    /// There might be a way to check if this is the case and filter those items in the future.
    /// </summary>
    /// <param name="dataItem">The data item</param>
    /// <param name="action">The action</param>
    /// <param name="culture">The culture</param>
    /// <returns>The item state from Sitefinity perspective</returns>
    public static string GetActualItemAction(
      IDataItem dataItem,
      string action,
      CultureInfo culture)
    {
      if (action != SecurityConstants.TransactionActionType.Updated.ToString())
        return action;
      DataProviderBase itemProvider = DataEventFactory.GetItemProvider(dataItem);
      if (itemProvider != null)
      {
        if (dataItem is PageNode && itemProvider.GetDirtyItemStatus((object) dataItem) == SecurityConstants.TransactionActionType.New)
          return DataEventAction.Created;
        string requiredPropertyName = DataEventFactory.GetItemTitleProperty(dataItem);
        if (!string.IsNullOrEmpty(requiredPropertyName))
        {
          PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties((object) dataItem).Cast<PropertyDescriptor>().FirstOrDefault<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (p => p.Name == requiredPropertyName));
          if (propertyDescriptor != null && propertyDescriptor.GetValue((object) dataItem) as Lstring != (Lstring) null)
          {
            string propertyName = DataEventFactory.GetOAPropertyName(requiredPropertyName, culture);
            SitefinityOAContext transaction = (SitefinityOAContext) itemProvider.GetTransaction();
            if (transaction.GetMemberNames((object) dataItem, ObjectState.Dirty).Any<string>((Func<string, bool>) (p => p == propertyName)) && DataEventFactory.GetOriginalValue(transaction, dataItem, propertyName) == null)
              return DataEventAction.Created;
          }
        }
      }
      return action;
    }

    /// <summary>Gets the title of an item</summary>
    /// <param name="dataItem">The data item</param>
    /// <param name="action">The action</param>
    /// <param name="culture">The culture</param>
    /// <returns>The title of the item</returns>
    public static string GetItemTitle(IDataItem dataItem, string action, CultureInfo culture)
    {
      string itemTitle = string.Empty;
      if (dataItem is IHasTitle hasTitle)
      {
        string str = SecurityConstants.TransactionActionType.Deleted.ToString();
        if (action == str && SystemManager.CurrentContext.AppSettings.Multilingual)
          itemTitle = DataEventFactory.GetTitleOfDeletedItem(dataItem, culture);
        if (action != str || string.IsNullOrEmpty(itemTitle))
          itemTitle = hasTitle.GetTitle(culture);
      }
      return itemTitle;
    }

    private static DataProviderBase GetItemProvider(IDataItem dataItem)
    {
      if (!(dataItem.Provider is DataProviderBase provider1) && dataItem.Provider is string provider2)
      {
        bool flag = typeof (Folder).IsAssignableFrom(dataItem.GetType());
        string transaction = dataItem.Transaction as string;
        IManager manager = !string.IsNullOrEmpty(transaction) ? (flag ? (IManager) LibrariesManager.GetManager(provider2, transaction) : ManagerBase.GetMappedManagerInTransaction(dataItem.GetType(), provider2, transaction)) : (flag ? (IManager) LibrariesManager.GetManager(provider2) : ManagerBase.GetMappedManager(dataItem.GetType(), provider2));
        if (manager == null)
          return (DataProviderBase) null;
        provider1 = manager.Provider;
      }
      return provider1;
    }

    private static object GetOriginalValue(
      SitefinityOAContext context,
      IDataItem dataItem,
      string propertyName)
    {
      object originalValue = (object) null;
      try
      {
        originalValue = context.GetOriginalValue<object>((object) dataItem, propertyName);
      }
      catch (Exception ex)
      {
      }
      return originalValue;
    }

    private static object GetCurrentValue(PropertyDescriptor realProp, IDataItem dataItem)
    {
      object currentValue = (object) null;
      try
      {
        currentValue = realProp.GetValue((object) dataItem);
      }
      catch (Exception ex)
      {
      }
      return currentValue;
    }

    /// <summary>
    /// Gets the OA property name for the given property. For example "Title" as a Lstring property will have an actual name "Title_EN" in EN culture according to OA.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    /// <param name="culture">The culture. If the culture is null, the current culture will be used</param>
    /// <returns>The OA property name</returns>
    private static string GetOAPropertyName(string propertyName, CultureInfo culture)
    {
      if (SystemManager.CurrentContext.AppSettings.Multilingual && culture == null)
        culture = SystemManager.CurrentContext.Culture;
      string str = "_" + LstringPropertyDescriptor.GetCultureSuffix(culture ?? CultureInfo.InvariantCulture);
      return propertyName + str;
    }

    /// <summary>
    /// Gets the title of an item that is deleted in the current transaction.
    /// </summary>
    /// <param name="dataItem">The data item</param>
    /// <param name="culture">The culture. If null, the current culture will be used.</param>
    /// <returns>The title of the item</returns>
    private static string GetTitleOfDeletedItem(IDataItem dataItem, CultureInfo culture)
    {
      string titleOfDeletedItem = (string) null;
      string itemTitleProperty = DataEventFactory.GetItemTitleProperty(dataItem);
      if (!string.IsNullOrEmpty(itemTitleProperty))
      {
        string oaPropertyName = DataEventFactory.GetOAPropertyName(itemTitleProperty, culture);
        if (dataItem.Provider is DataProviderBase provider)
          titleOfDeletedItem = DataEventFactory.GetOriginalValue((SitefinityOAContext) provider.GetTransaction(), dataItem, oaPropertyName) as string;
      }
      return titleOfDeletedItem;
    }

    /// <summary>
    /// Gets the title property for a data item. This is a required property for the item.
    /// </summary>
    /// <param name="dataItem">The data item</param>
    /// <returns>The title property for the item</returns>
    private static string GetItemTitleProperty(IDataItem dataItem)
    {
      string itemTitleProperty = (string) null;
      if (dataItem is ILocalizable localizable)
      {
        object[] customAttributes = localizable.GetType().GetCustomAttributes(typeof (RequiredLocalizablePropertyAttribute), true);
        if (customAttributes != null && customAttributes.Length != 0)
          itemTitleProperty = ((RequiredLocalizablePropertyAttribute) customAttributes[0]).RequiredPropertyName;
        else if (dataItem is DynamicContent)
        {
          PropertyDescriptor typeMainProperty = ModuleBuilderManager.GetTypeMainProperty(dataItem.GetType());
          itemTitleProperty = typeMainProperty != null ? typeMainProperty.Name : string.Empty;
        }
      }
      return itemTitleProperty;
    }

    /// <summary>
    /// Lstring properties have artificial names (i.e. "Title_EN") according to OA. This method gets the actual property names.
    /// </summary>
    /// <param name="prop">The property name according to OA</param>
    /// <param name="culture">The culture</param>
    /// <returns>The real actual name</returns>
    private static string GetActualPropertyName(string prop, CultureInfo culture)
    {
      string actualPropertyName = prop;
      string str = "_" + LstringPropertyDescriptor.GetCultureSuffix(culture != null ? culture : CultureInfo.InvariantCulture);
      int startIndex = actualPropertyName.IndexOf(str);
      if (startIndex > 0)
        actualPropertyName = actualPropertyName.Remove(startIndex);
      return actualPropertyName;
    }
  }
}
