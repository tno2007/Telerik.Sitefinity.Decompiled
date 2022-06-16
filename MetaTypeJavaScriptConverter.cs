// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.MetaTypeJavaScriptConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Utilities
{
  /// <summary>
  /// A custom converter for JavaScriptSerializer used to serialize objects using type descriptors instead of simple reflection.
  /// </summary>
  public class MetaTypeJavaScriptConverter : JavaScriptConverter
  {
    /// <summary>
    /// This delegate is used to check whether a property must be serialized. If null, default chack is performed.
    /// If you implement such delegate, you can invoke the static MustSerializeProperty method to use the default checking.
    /// </summary>
    public MustSerializePropertyDelegate MustSerializeDelegate { get; set; }

    /// <summary>
    /// Gets or sets the custom supported types. Those are the types that will be handled by this converter.
    /// </summary>
    /// <value>The custom supported types.</value>
    public IEnumerable<Type> CustomSupportedTypes { get; set; }

    public SerializationEnhancementDelegate EnhancementDelegate { get; set; }

    public CreateObjectInstanceDelegate CreateInstanceDelegate { get; set; }

    public DeserializeValueEnhancementDelegate DeserializeValueEnhancementDelegate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Utilities.MetaTypeJavaScriptConverter" /> class using <see cref="T:System.Object" /> as supported type.
    /// </summary>
    public MetaTypeJavaScriptConverter()
      : this((IEnumerable<Type>) new Type[1]
      {
        typeof (object)
      })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Utilities.MetaTypeJavaScriptConverter" /> class and configure it to handle the specified type.
    /// </summary>
    /// <param name="supportedType">The type that will be handled by the converter.</param>
    public MetaTypeJavaScriptConverter(Type supportedType)
      : this((IEnumerable<Type>) new Type[1]
      {
        supportedType
      })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Utilities.MetaTypeJavaScriptConverter" /> class and configure it to handle the specified types.
    /// </summary>
    /// <param name="supportedTypes">The supported types.</param>
    public MetaTypeJavaScriptConverter(IEnumerable<Type> supportedTypes) => this.CustomSupportedTypes = supportedTypes;

    /// <summary>Gets the supported types.</summary>
    /// <value>The supported types.</value>
    public override IEnumerable<Type> SupportedTypes => (IEnumerable<Type>) new ReadOnlyCollection<Type>((IList<Type>) new List<Type>()
    {
      typeof (UserProfile)
    });

    /// <summary>Serializes the specified obj.</summary>
    /// <param name="obj">The obj.</param>
    /// <param name="serializer">The serializer.</param>
    /// <returns>
    /// An object that contains key/value pairs that represent the object’s data.
    /// </returns>
    public override IDictionary<string, object> Serialize(
      object obj,
      JavaScriptSerializer serializer)
    {
      IDynamicFieldsContainer dynamicFieldsContainer = obj as IDynamicFieldsContainer;
      Dictionary<string, object> result = new Dictionary<string, object>();
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj.GetType()))
      {
        if (this.MustSerializePropertyInternal(property, true))
        {
          object obj1 = this.SerializeProperty(property, (object) dynamicFieldsContainer, serializer);
          result[property.Name] = obj1;
        }
      }
      if (this.EnhancementDelegate != null)
        this.EnhancementDelegate(obj, result, serializer);
      if (obj is IDataItem dataItem && dataItem.Provider is DataProviderBase provider)
        result["__providerName"] = (object) provider.Name;
      return (IDictionary<string, object>) result;
    }

    protected virtual object SerializeProperty(
      PropertyDescriptor propertyDescriptor,
      object obj,
      JavaScriptSerializer serializer)
    {
      object obj1 = propertyDescriptor.GetValue(obj);
      object obj2;
      if (propertyDescriptor is LstringPropertyDescriptor)
      {
        Lstring lstring = obj1 as Lstring;
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        if (lstring != (Lstring) null)
        {
          dictionary.Add("Value", (object) lstring.Value);
          dictionary.Add("PersistedValue", (object) lstring.PersistedValue);
        }
        obj2 = (object) dictionary;
      }
      else
        obj2 = obj1;
      return obj2;
    }

    public override object Deserialize(
      IDictionary<string, object> dictionary,
      Type type,
      JavaScriptSerializer serializer)
    {
      if (dictionary == null)
        throw new ArgumentNullException(nameof (dictionary));
      if (!typeof (IDynamicFieldsContainer).IsAssignableFrom(type))
        return (object) null;
      IDynamicFieldsContainer objectInstance = this.CreateObjectInstance(type, dictionary, serializer);
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(type))
      {
        if (this.MustSerializePropertyInternal(property, false))
        {
          object deserializedValue = this.DeserializeProperty(property, type, dictionary, (object) objectInstance, serializer);
          if (this.DeserializeValueEnhancementDelegate != null)
            deserializedValue = this.DeserializeValueEnhancementDelegate(property, type, deserializedValue, objectInstance);
          objectInstance.SetValue(property.Name, deserializedValue);
        }
      }
      return (object) objectInstance;
    }

    protected virtual object DeserializeProperty(
      PropertyDescriptor propertyDescriptor,
      Type objectType,
      IDictionary<string, object> dictionary,
      object instance,
      JavaScriptSerializer serializer)
    {
      object obj1;
      dictionary.TryGetValue(propertyDescriptor.Name, out obj1);
      object obj2;
      if (propertyDescriptor is LstringPropertyDescriptor)
      {
        if (obj1 != null)
        {
          if (dictionary[propertyDescriptor.Name] is Dictionary<string, object> dictionary1)
          {
            obj1 = (object) ((IDynamicFieldsContainer) instance).GetString(propertyDescriptor.Name);
            ((Lstring) obj1).Value = (string) dictionary1["PersistedValue"];
          }
          else
            obj1 = (object) null;
        }
        obj2 = obj1;
      }
      else
      {
        if (obj1 != null)
          obj1 = serializer.ConvertToType(obj1, propertyDescriptor.PropertyType);
        obj2 = obj1;
      }
      return obj2;
    }

    protected virtual IDynamicFieldsContainer CreateObjectInstance(
      Type objectType,
      IDictionary<string, object> properties,
      JavaScriptSerializer serializer)
    {
      IDynamicFieldsContainer objectInstance = (IDynamicFieldsContainer) null;
      if (this.CreateInstanceDelegate != null)
        objectInstance = this.CreateInstanceDelegate(objectType, properties, serializer);
      if (objectInstance == null)
        objectInstance = Activator.CreateInstance(objectType) as IDynamicFieldsContainer;
      return objectInstance;
    }

    protected virtual bool MustSerializePropertyInternal(
      PropertyDescriptor propertyDescriptor,
      bool serialize)
    {
      return this.MustSerializeDelegate != null ? this.MustSerializeDelegate(propertyDescriptor, serialize) : MetaTypeJavaScriptConverter.MustSerializeProperty(propertyDescriptor, checkReadOnly: (!serialize));
    }

    /// <summary>
    /// This is the default implementation of the method used to test whether to serialize/deserialize a property. It can be used from within custom handler delegates.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="checkDataMember">Whether to return true only for properties that have DataMember attribute.</param>
    /// <param name="checkScriptIgnore">Whether to return true only for properties that do not have ScriptIgnore attribute.</param>
    /// <param name="checkReadOnly">Whether to return true only for properties that are not read-only.</param>
    /// <returns></returns>
    public static bool MustSerializeProperty(
      PropertyDescriptor propertyDescriptor,
      bool checkDataMember = true,
      bool checkScriptIgnore = true,
      bool checkReadOnly = true)
    {
      bool flag1 = false;
      if (checkDataMember)
      {
        bool flag2 = propertyDescriptor.Attributes[typeof (DataMemberAttribute)] != null;
        flag1 |= flag2;
      }
      if (checkScriptIgnore)
      {
        bool flag3 = propertyDescriptor.Attributes[typeof (ScriptIgnoreAttribute)] != null;
        flag1 = flag1 && !flag3;
      }
      if (checkReadOnly)
      {
        bool flag4 = propertyDescriptor.Attributes[typeof (ReadOnlyAttribute)] is ReadOnlyAttribute attribute && attribute.IsReadOnly || propertyDescriptor.IsReadOnly;
        flag1 = flag1 && !flag4;
      }
      return flag1;
    }
  }
}
