// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.DynamicFieldsDataContractSurrogate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>
  /// Provides the methods needed to substitute one type for another by the System.Runtime.Serialization.DataContractSerializer
  /// during serialization, deserialization, and export and import of XML schema
  /// documents (XSD).
  /// This type will try to export types suitable for serialization, whenever a type that contains
  /// dynamic links is located.
  /// </summary>
  public class DynamicFieldsDataContractSurrogate : IDataContractSurrogate
  {
    private static Dictionary<Type, Type> abstractClassesCache = new Dictionary<Type, Type>();

    /// <summary>
    /// During schema export operations, inserts annotations into the schema for
    ///     non-null return values.
    /// </summary>
    /// <param name="clrType">The CLR type to be replaced.</param>
    /// <param name="dataContractType">The data contract type to be annotated.</param>
    /// <returns>An object that represents the annotation to be inserted into the XML schema definition.</returns>
    public object GetCustomDataToExport(Type clrType, Type dataContractType) => (object) null;

    /// <summary>
    /// During schema export operations, inserts annotations into the schema for
    ///     non-null return values.
    /// </summary>
    /// <param name="memberInfo">A System.Reflection.MemberInfo that describes the member.</param>
    /// <param name="dataContractType">A System.Type.</param>
    /// <returns>An object that represents the annotation to be inserted into the XML schema definition.</returns>
    public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType) => (object) null;

    /// <summary>
    /// During serialization, deserialization, and schema import and export, returns
    /// a data contract type that substitutes the specified type.
    /// </summary>
    /// <param name="type">The CLR type System.Type to substitute.</param>
    /// <returns>The System.Type to substitute for the type value. This type must be serializable
    /// by the System.Runtime.Serialization.DataContractSerializer. For example,
    /// it must be marked with the System.Runtime.Serialization.DataContractAttribute
    /// attribute or other mechanisms that the serializer recognizes.</returns>
    public Type GetDataContractType(Type type)
    {
      Type dataContractType = type;
      Type itemType = WcfContext.ItemType;
      if (itemType != (Type) null && type.IsAssignableFrom(itemType))
      {
        dataContractType = itemType;
      }
      else
      {
        Type substitution = WcfContext.GetSubstitution(type);
        if (substitution != (Type) null && type.IsAssignableFrom(substitution))
          dataContractType = substitution;
      }
      if (type == typeof (WcfItemBase))
        dataContractType = TypeResolutionService.ResolveType(SystemManager.CurrentHttpContext.Request.QueryString["itemSurrogateType"]);
      if (dataContractType != type && type.IsAbstract)
        DynamicFieldsDataContractSurrogate.AddAbstract(type, dataContractType);
      if (TypeSurrogateFactory.Instance.ShouldCreateSurrogateForType(dataContractType))
        dataContractType = TypeSurrogateFactory.Instance.GetSurrogateType(dataContractType);
      return dataContractType;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private static void AddAbstract(Type abstractType, Type substitution) => DynamicFieldsDataContractSurrogate.abstractClassesCache[abstractType] = substitution;

    [MethodImpl(MethodImplOptions.Synchronized)]
    private static Type GetAbstractFromReplacement(Type replacementType)
    {
      foreach (KeyValuePair<Type, Type> keyValuePair in DynamicFieldsDataContractSurrogate.abstractClassesCache)
      {
        if (keyValuePair.Value == replacementType)
          return keyValuePair.Key;
      }
      return (Type) null;
    }

    /// <summary>
    /// During deserialization, returns an object that is a substitute for the specified
    ///     object.
    /// </summary>
    /// <param name="obj">The deserialized object to be substituted.</param>
    /// <param name="targetType">The System.Type that the substituted object should be assigned to.</param>
    /// <returns>The substituted deserialized object. This object must be of a type that is
    /// serializable by the System.Runtime.Serialization.DataContractSerializer.
    /// For example, it must be marked with the System.Runtime.Serialization.DataContractAttribute
    /// attribute or other mechanisms that the serializer recognizes</returns>
    public object GetDeserializedObject(object obj, Type targetType)
    {
      Type type = obj != null ? obj.GetType() : throw new ArgumentNullException(nameof (obj));
      bool flag = false;
      if (TypeSurrogateFactory.Instance.IsSurrogate(type))
        flag = true;
      else if (DynamicFieldsDataContractSurrogate.GetAbstractFromReplacement(type) != (Type) null)
        flag = true;
      return flag ? ReflectionHelper.DeepCopy(obj, type, (CreateInstanceDelegate) ((t, propPath, obj1) => TypeSurrogateFactory.Instance.CreateInstance(t, obj1, propPath)), (CanCopyPropertyDelegate) (p => p.Attributes[typeof (DataMemberAttribute)] != null || typeof (FormEntry).IsAssignableFrom(p.ComponentType) || typeof (DynamicTypeBase).IsAssignableFrom(p.ComponentType)), string.Empty) : obj;
    }

    /// <summary>
    /// Sets the collection of known types to use for serialization and deserialization of the custom data objects.
    /// </summary>
    /// <param name="customDataTypes">A <see cref="T:System.Collections.ObjectModel.Collection`1" />  of <see cref="T:System.Type" /> to add known types to.</param>
    public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
    {
      foreach (Type knownType in (IEnumerable<Type>) TypeSurrogateFactory.Instance.KnownTypes)
      {
        if (!customDataTypes.Contains(knownType))
          customDataTypes.Add(knownType);
      }
    }

    /// <summary>
    /// During serialization, returns an object that substitutes the specified object.
    /// </summary>
    /// <param name="obj">The object to substitute.</param>
    /// <param name="targetType">The System.Type that the substituted object should be assigned to.</param>
    /// <returns>The substituted object that will be serialized. The object must be serializable
    /// by the System.Runtime.Serialization.DataContractSerializer. For example,
    /// it must be marked with the System.Runtime.Serialization.DataContractAttribute
    /// attribute or other mechanisms that the serializer recognizes.</returns>
    public object GetObjectToSerialize(object obj, Type targetType)
    {
      if (!(obj is IDynamicFieldsContainer))
        return obj;
      IDynamicFieldsContainer dynamicFieldsContainer = (IDynamicFieldsContainer) obj;
      object surrogate = TypeSurrogateFactory.Instance.GetSurrogate(dynamicFieldsContainer);
      if (surrogate.GetType() == obj.GetType())
        return obj;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(surrogate);
      foreach (PropertyDescriptor property1 in TypeDescriptor.GetProperties((object) dynamicFieldsContainer))
      {
        PropertyDescriptor property2 = properties[property1.Name];
        if (property2 != null)
        {
          object obj1 = property1.GetValue((object) dynamicFieldsContainer);
          object obj2 = this.ProcessSpecialValueCases(property1, obj1, dynamicFieldsContainer);
          this.SetPropertyValue(property2, surrogate, obj2);
        }
      }
      if (!TypeSurrogateFactory.Instance.IsBaseType(dynamicFieldsContainer.GetType()))
      {
        Type baseType = TypeSurrogateFactory.Instance.GetBaseType(dynamicFieldsContainer.GetType());
        TypeConverter converter = TypeDescriptor.GetConverter(surrogate);
        if (converter != null && converter.CanConvertTo(baseType))
          return converter.ConvertTo(surrogate, baseType);
      }
      return surrogate;
    }

    private void SetPropertyValue(PropertyDescriptor property, object item, object value)
    {
      if (property.PropertyType.FullName == typeof (DateTime).FullName && (value == null || value is DateTime dateTime && dateTime == DateTime.MinValue))
        property.SetValue(item, (object) DateTime.UtcNow);
      else
        property.SetValue(item, value);
    }

    /// <summary>Processes the special value cases such IList[Guid]</summary>
    /// <param name="dynamicProperty">The dynamic property.</param>
    /// <param name="value">The value.</param>
    /// <param name="dynamicObj">The dynamic object.</param>
    /// <returns>Processed value.</returns>
    private object ProcessSpecialValueCases(
      PropertyDescriptor dynamicProperty,
      object value,
      IDynamicFieldsContainer dynamicObj)
    {
      if (typeof (IList<Guid>).Equals(dynamicProperty.PropertyType))
      {
        List<Guid> guidList = new List<Guid>();
        foreach (Guid guid in (IEnumerable<Guid>) (value as IList<Guid>))
          guidList.Add(guid);
        value = (object) guidList;
      }
      else if (dynamicProperty.IsLongText() && value != null)
      {
        switch (dynamicObj)
        {
          case Content _:
            value = (object) new Lstring((dynamicObj as Content).ApplyContentFilters(value.ToString()));
            break;
          case IDataItem _:
            IDataItem dataItem = (IDataItem) dynamicObj;
            if (dataItem.Provider is IHtmlFilterProvider)
            {
              value = (object) new Lstring(((IHtmlFilterProvider) dataItem.Provider).ApplyFilters(value.ToString()));
              break;
            }
            break;
        }
      }
      return value;
    }

    /// <summary>
    /// During schema import, returns the type referenced by the schema.
    /// </summary>
    /// <param name="typeName">The name of the type in schema.</param>
    /// <param name="typeNamespace">The namespace of the type in schema.</param>
    /// <param name="customData">The object that represents the annotation inserted into the XML schema definition,
    /// which is data that can be used for finding the referenced type.</param>
    /// <returns>The System.Type to use for the referenced type.</returns>
    public Type GetReferencedTypeOnImport(
      string typeName,
      string typeNamespace,
      object customData)
    {
      return TypeSurrogateFactory.Instance.GetSurrogate(typeName, typeNamespace);
    }

    /// <summary>
    /// Processes the type that has been generated from the imported schema.
    /// </summary>
    /// <param name="typeDeclaration">A System.CodeDom.CodeTypeDeclaration to process that represents the type
    /// declaration generated during schema import.</param>
    /// <param name="compileUnit">The System.CodeDom.CodeCompileUnit that contains the other code generated
    /// during schema import.</param>
    /// <returns>A System.CodeDom.CodeTypeDeclaration that contains the processed type.</returns>
    public CodeTypeDeclaration ProcessImportedType(
      CodeTypeDeclaration typeDeclaration,
      CodeCompileUnit compileUnit)
    {
      return typeDeclaration;
    }
  }
}
