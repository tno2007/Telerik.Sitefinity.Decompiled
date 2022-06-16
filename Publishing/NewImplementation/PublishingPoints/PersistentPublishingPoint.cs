// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PersistentPublishingPoint
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Translators;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Persistent Publishing Point</summary>
  public class PersistentPublishingPoint : PublishingPointBase
  {
    private const int DefaultNvarcharMaxLength = 255;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PersistentPublishingPoint" /> class.
    /// </summary>
    public PersistentPublishingPoint()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PersistentPublishingPoint" /> class.
    /// </summary>
    /// <param name="model">The model.</param>
    public PersistentPublishingPoint(PublishingPoint model)
      : base(model)
    {
    }

    /// <summary>Gets the publishing point items.</summary>
    /// <returns></returns>
    public override IQueryable<WrapperObject> GetPublishingPointItems() => Queryable.OfType<DynamicTypeBase>(this.ReadData()).OrderByDescending<DynamicTypeBase, DateTime>((Expression<Func<DynamicTypeBase, DateTime>>) (i => i.LastModified)).Select<DynamicTypeBase, WrapperObject>((Expression<Func<DynamicTypeBase, WrapperObject>>) (i => this.GetWrapperObjectWithLanguage(i)));

    private WrapperObject GetWrapperObjectWithLanguage(DynamicTypeBase item)
    {
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties((object) item).Find("LangId", true);
      WrapperObject objectWithLanguage = new WrapperObject((object) item);
      if (propertyDescriptor != null)
      {
        string str = propertyDescriptor.GetValue((object) item) as string;
        objectWithLanguage.Language = str;
      }
      return objectWithLanguage;
    }

    /// <summary>Adds the items.</summary>
    /// <param name="items">The items.</param>
    public override void AddItems(IList<WrapperObject> items)
    {
      PublishingPointDynamicTypeManager pointDataItemManager = this.GetPublishingPointDataItemManager();
      foreach (WrapperObject component in (IEnumerable<WrapperObject>) items.OrderBy<WrapperObject, object>((Func<WrapperObject, object>) (i => i.GetPropertyOrNull("LastModified"))))
      {
        object dataItem = pointDataItemManager.CreateDataItem((IPublishingPoint) this.Model);
        PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties(dataItem);
        PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties((object) component);
        properties1.Find("LangId", true)?.SetValue(dataItem, (object) component.Language);
        for (int index = 0; index < properties1.Count; ++index)
        {
          PropertyDescriptor propertyDescriptor1 = properties1[index];
          if (!propertyDescriptor1.IsReadOnly && propertyDescriptor1.Name.ToLower() != "id" && propertyDescriptor1.Name != "Provider" && propertyDescriptor1.Name != "ApplicationName" && propertyDescriptor1.Name != "Transaction")
          {
            PropertyDescriptor propertyDescriptor2 = properties2.Find(propertyDescriptor1.Name, false);
            if (propertyDescriptor2 != null)
            {
              object propertyOrNull = propertyDescriptor2.GetValue((object) component);
              if (propertyDescriptor1.Name == "OriginalItemId" && (propertyOrNull == null || (Guid) propertyOrNull == Guid.Empty))
                propertyOrNull = component.GetPropertyOrNull("ID");
              if (propertyOrNull != null)
              {
                if (this.IsSupportedPropertyType(propertyOrNull.GetType()))
                {
                  if (propertyDescriptor1.PropertyType.IsAssignableFrom(propertyOrNull.GetType()))
                  {
                    propertyDescriptor1.SetValue(dataItem, propertyOrNull);
                  }
                  else
                  {
                    TypeConverter converter = TypeDescriptor.GetConverter(propertyOrNull);
                    if (converter.CanConvertTo(propertyDescriptor1.PropertyType))
                      propertyDescriptor1.SetValue(dataItem, converter.ConvertTo(propertyOrNull, propertyDescriptor1.PropertyType));
                  }
                }
              }
              else
                propertyDescriptor1.SetValue(dataItem, propertyOrNull);
            }
          }
        }
      }
      pointDataItemManager.SaveChanges();
      base.AddItems(items);
    }

    /// <summary>Removes the items.</summary>
    /// <param name="items">The items.</param>
    public override void RemoveItems(IList<WrapperObject> items)
    {
      PublishingPointDynamicTypeManager pointDataItemManager = this.GetPublishingPointDataItemManager();
      foreach (WrapperObject component in (IEnumerable<WrapperObject>) items)
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) component);
        PropertyDescriptor propertyDescriptor1 = properties.Find("OriginalItemId", true);
        PropertyDescriptor propertyDescriptor2 = properties.Find("ItemHash", true);
        if (propertyDescriptor1 != null)
        {
          object itemId = propertyDescriptor1.GetValue((object) component);
          if (itemId != null && itemId is Guid guid && guid != Guid.Empty)
          {
            this.HandleItemDeleted((Guid) itemId, component.WrappedObject.GetType(), true);
            continue;
          }
        }
        if (propertyDescriptor2 != null)
        {
          object hash = propertyDescriptor2.GetValue((object) component);
          if (hash != null && hash is string && !string.IsNullOrEmpty((string) hash))
            this.HandleItemDeletedByHash((string) hash, true);
        }
      }
      pointDataItemManager.SaveChanges();
      base.RemoveItems(items);
    }

    /// <summary>Reads data from publishing point</summary>
    /// <param name="source"></param>
    /// <returns></returns>
    protected virtual IQueryable ReadData() => this.GetPublishingPointDataItemManager().GetDataItems((IPublishingPoint) this.Model);

    /// <summary>Handles the item deleted by hash.</summary>
    /// <param name="hash">The hash.</param>
    /// <param name="deletedChildren">if set to <c>true</c> [deleted children].</param>
    /// <returns></returns>
    protected virtual List<DynamicTypeBase> HandleItemDeletedByHash(
      string hash,
      bool deletedChildren)
    {
      List<DynamicTypeBase> dynamicTypeBaseList = new List<DynamicTypeBase>();
      this.GetPublishingManager(this.PublishingProviderName, Guid.NewGuid().ToString());
      PublishingPoint model = this.Model;
      foreach (DynamicTypeBase dynamicTypeBase in Queryable.OfType<DynamicTypeBase>(this.GetPublishingPointDataItemManager().GetDataItems((IPublishingPoint) model)).ToList<DynamicTypeBase>().Where<DynamicTypeBase>((Func<DynamicTypeBase, bool>) (item => item.GetValue<string>("ItemHash") == hash)))
      {
        this.GetPublishingPointDataItemManager().DeleteDataItem((object) dynamicTypeBase);
        dynamicTypeBaseList.Add(dynamicTypeBase);
      }
      return dynamicTypeBaseList;
    }

    /// <summary>
    /// Called when the item is modified and it needs to handle the action.
    /// Accepts an argument that defines whether the children of the item have to handle the action too.
    /// </summary>
    /// <param name="item">The data item.</param>
    /// <param name="deleteChildren">if set to <c>true</c> the children of the item handle the action.</param>
    protected virtual List<DynamicTypeBase> HandleItemDeleted(
      Guid itemId,
      Type itemType,
      bool deleteChildren)
    {
      PublishingPoint model = this.Model;
      PublishingManager publishingManager = this.GetPublishingManager(this.PublishingProviderName, Guid.NewGuid().ToString());
      IEnumerator enumerator = this.GetPublishingPointDataItemManager().GetDataItems((IPublishingPoint) model, itemId).GetEnumerator();
      object obj = (object) null;
      if (enumerator.MoveNext())
        obj = enumerator.Current;
      if (enumerator.MoveNext())
        throw new InvalidOperationException("There are more than one item in sequence");
      List<DynamicTypeBase> dynamicTypeBaseList = new List<DynamicTypeBase>();
      if (obj != null)
      {
        publishingManager.GetDynamicTypeManager((IPublishingPoint) model).DeleteDataItem(obj);
        dynamicTypeBaseList.Add((DynamicTypeBase) obj);
      }
      if (deleteChildren)
      {
        foreach (object dataItem in (IEnumerable) this.GetPublishingPointDataItemManager().GetDataItems((IPublishingPoint) model, itemId))
          this.GetPublishingPointDataItemManager().DeleteDataItem(dataItem);
        if (typeof (IHasContentChildren).IsAssignableFrom(itemType))
        {
          foreach (object childrenDataItem in (IEnumerable) this.GetPublishingPointDataItemManager().GetChildrenDataItems((IPublishingPoint) model, itemId))
          {
            this.GetPublishingPointDataItemManager().DeleteDataItem(childrenDataItem);
            dynamicTypeBaseList.Add((DynamicTypeBase) childrenDataItem);
          }
        }
      }
      return dynamicTypeBaseList;
    }

    /// <summary>
    /// Creates a publishing point item of the specific dynamic type.
    /// </summary>
    /// <returns></returns>
    protected virtual object CreatePublishingPointItem(Dictionary<string, object> itemDictionary)
    {
      PublishingManager publishingManager = this.GetPublishingManager();
      PublishingPoint model = this.Model;
      PublishingPoint publishingPoint = model;
      return publishingManager.GetDynamicTypeManager((IPublishingPoint) publishingPoint).CreateDataItem((IPublishingPoint) model);
    }

    /// <summary>
    /// Imports a single data item into the publishing point by performing the inbound mapping.
    /// </summary>
    /// <param name="item">The dictionary of values</param>
    public virtual object ImportItem(Dictionary<string, object> importData, WrapperObject wrapper)
    {
      object publishingPointItem = importData != null ? this.CreatePublishingPointItem(importData) : throw new ArgumentNullException("internalData");
      IList<MetaField> metaFields = (IList<MetaField>) null;
      MetaType metaType = MetadataManager.GetManager().GetMetaType(publishingPointItem.GetType());
      if (metaType != null)
        metaFields = metaType.Fields;
      if (wrapper.MappingSettings.Mappings == null || wrapper.MappingSettings.Mappings.Count == 0)
      {
        foreach (string key in importData.Keys)
          this.SetPublishingPointFieldValue(publishingPointItem, key, importData[key], metaFields);
      }
      else
      {
        IDictionary<string, object> dictionary = this.ApplyMapping((Func<string, object>) (propName => importData.ContainsKey(propName) ? importData[propName] : (object) null), wrapper);
        foreach (Mapping mapping in (IEnumerable<Mapping>) wrapper.MappingSettings.Mappings)
        {
          if (dictionary.ContainsKey(mapping.DestinationPropertyName))
            this.SetPublishingPointFieldValue(publishingPointItem, mapping.DestinationPropertyName, dictionary[mapping.DestinationPropertyName], metaFields);
        }
      }
      return publishingPointItem;
    }

    /// <summary>Sets the publishing point field value.</summary>
    /// <param name="publishingPointItem">The publishing point item.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="propertyValue">The property value.</param>
    protected virtual void SetPublishingPointFieldValue(
      object publishingPointItem,
      string propertyName,
      object propertyValue,
      IList<MetaField> metaFields)
    {
      PropertyDescriptor destProp = TypeDescriptor.GetProperties(publishingPointItem).Find(propertyName, true);
      if (destProp == null || destProp.PropertyType.IsValueType && propertyValue == null)
        return;
      bool flag = true;
      if (propertyValue != null)
      {
        Type type = propertyValue.GetType();
        if (!type.IsAssignableFrom(destProp.PropertyType))
        {
          flag = false;
          TypeConverter converter1 = TypeDescriptor.GetConverter(propertyValue);
          if (converter1 != null && converter1.CanConvertTo(destProp.PropertyType))
          {
            propertyValue = converter1.ConvertTo(propertyValue, destProp.PropertyType);
            flag = true;
          }
          else
          {
            TypeConverter converter2 = TypeDescriptor.GetConverter(destProp.PropertyType);
            if (converter2 != null && converter2.CanConvertFrom(type))
            {
              propertyValue = converter2.ConvertFrom(propertyValue);
              flag = true;
            }
          }
        }
      }
      if (!flag)
        return;
      propertyValue = this.ProcessPropertyValue(destProp, propertyName, propertyValue, metaFields);
      destProp.SetValue(publishingPointItem, propertyValue);
    }

    /// <summary>
    /// Processes the passed value before assigning it to the property.
    /// </summary>
    /// <param name="destProp">The destination property.</param>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="propertyValue">The property value.</param>
    /// <param name="metaFields">The meta fields of the component type.</param>
    /// <returns></returns>
    protected virtual object ProcessPropertyValue(
      PropertyDescriptor destProp,
      string propertyName,
      object propertyValue,
      IList<MetaField> metaFields)
    {
      if (metaFields != null && destProp.PropertyType == typeof (string))
      {
        MetaField metaField = metaFields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (mf => mf.FieldName == propertyName));
        if (metaField != null && metaField.DBSqlType != "NVARCHAR(MAX)")
        {
          int length = metaField.MaxLength;
          if (length == 0)
          {
            int result = 0;
            length = !int.TryParse(metaField.DBLength, out result) ? (int) byte.MaxValue : result;
          }
          string str = (string) propertyValue;
          if (str != null && str.Length > length)
            propertyValue = (object) str.Substring(0, length);
        }
      }
      return propertyValue;
    }

    /// <summary>
    /// Applies the fields mapping and translation for a single data item. Can be used in both directions
    /// </summary>
    /// <param name="sourceData">The source data dictionary.</param>
    /// <returns>The destination data dictionary - with all source fields translated and mapped into destination fields</returns>
    protected virtual IDictionary<string, object> ApplyMapping(
      Func<string, object> getSourceValue,
      WrapperObject wrapper)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (Mapping mapping in (IEnumerable<Mapping>) wrapper.MappingSettings.Mappings)
      {
        if (!dictionary.ContainsKey(mapping.DestinationPropertyName))
        {
          object[] sourcePropValues = new object[mapping.SourcePropertyNames.Length];
          for (int index = 0; index < mapping.SourcePropertyNames.Length; ++index)
          {
            string sourcePropertyName = mapping.SourcePropertyNames[index];
            sourcePropValues[index] = getSourceValue(sourcePropertyName);
          }
          if (sourcePropValues.Length != 0)
          {
            object obj = this.ExecuteTranslatorsChain(mapping, sourcePropValues);
            dictionary.Add(mapping.DestinationPropertyName, obj);
          }
        }
      }
      return (IDictionary<string, object>) dictionary;
    }

    /// <summary>Executes the translators chain.</summary>
    /// <param name="mapping">The mapping.</param>
    /// <param name="sourcePropValues">The source prop values.</param>
    /// <returns></returns>
    protected virtual object ExecuteTranslatorsChain(Mapping mapping, object[] sourcePropValues)
    {
      object[] valuesToTranslate = sourcePropValues;
      object obj = (object) null;
      if (mapping.Translations.Count > 0)
      {
        foreach (PipeMappingTranslation translation in (IEnumerable<PipeMappingTranslation>) mapping.Translations)
        {
          obj = PipeTranslatorFactory.ResolveTranslator(translation.TranslatorName).Translate(valuesToTranslate, (IDictionary<string, string>) translation.GetSettings());
          valuesToTranslate = new object[1]{ obj };
        }
        return obj;
      }
      return sourcePropValues.Length == 1 ? new TransparentTranslator().Translate(sourcePropValues, (IDictionary<string, string>) null) : new ConcatenationTranslator().Translate(sourcePropValues, (IDictionary<string, string>) null);
    }

    /// <summary>
    /// Converts the object currently imported into a dictionary of property values, suitable for mapping. The keys of the dictionary are the mapping source field names
    /// The default implementation retrieves the neccessary property values(according to the Mapping) using TypeDescriptor
    /// Specific Pipe implementations should do their own conversion to dictionary depending on the format of the data they receive
    /// which might be xml or some hierarchical object and not be suitable to query directly with TypeDescriptor
    /// </summary>
    /// <param name="item">The item from which the data is extracted.</param>
    /// <returns>A dictionary containing extracted values.</returns>
    protected virtual Dictionary<string, object> ConvertImportedItemForMapping(
      WrapperObject wrapper,
      object item)
    {
      List<string> source = new List<string>();
      foreach (Mapping mapping in (IEnumerable<Mapping>) wrapper.MappingSettings.Mappings)
      {
        foreach (string sourcePropertyName in mapping.SourcePropertyNames)
        {
          if (!source.Contains<string>(sourcePropertyName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
            source.Add(sourcePropertyName);
        }
      }
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(item);
      foreach (string str in source)
      {
        PropertyDescriptor propertyDescriptor = properties.Find(str, true);
        if (propertyDescriptor != null)
        {
          object obj = !(propertyDescriptor.Name == "Content") || !(propertyDescriptor.PropertyType == typeof (Lstring)) || !typeof (Content).IsAssignableFrom(propertyDescriptor.ComponentType) ? propertyDescriptor.GetValue(item) : (object) LinkParser.ResolveLinks((string) ((IDynamicFieldsContainer) item).GetString("Content"), new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, false, true);
          dictionary.Add(str, obj);
        }
      }
      return dictionary;
    }

    private bool IsSupportedPropertyType(Type type) => ((IEnumerable<Type>) new Type[17]
    {
      typeof (int?),
      typeof (Decimal?),
      typeof (float?),
      typeof (double?),
      typeof (char?),
      typeof (DateTime?),
      typeof (bool?),
      typeof (Lstring),
      typeof (int),
      typeof (Decimal),
      typeof (float),
      typeof (double),
      typeof (char),
      typeof (DateTime),
      typeof (bool),
      typeof (Guid),
      typeof (string)
    }).Any<Type>((Func<Type, bool>) (t => t == type));
  }
}
