// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.PageDynamicFieldsDataContractSurrogate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>
  /// Provides the methods needed to substitute one type for another by the System.Runtime.Serialization.DataContractSerializer
  /// during serialization, deserialization, and export and import of XML schema
  /// documents (XSD).
  /// This type will try to export types suitable for serialization, whenever a type that contains
  /// dynamic links is located.
  /// </summary>
  public class PageDynamicFieldsDataContractSurrogate : IDataContractSurrogate
  {
    /// <summary>
    /// During schema export operations, inserts annotations into the schema for
    ///     non-null return values.
    /// </summary>
    /// <param name="clrType">The CLR type to be replaced.</param>
    /// <param name="dataContractType">The data contract type to be annotated.</param>
    /// <returns>An object that represents the annotation to be inserted into the XML schema
    /// definition.</returns>
    public object GetCustomDataToExport(Type clrType, Type dataContractType) => (object) null;

    /// <summary>
    /// During schema export operations, inserts annotations into the schema for
    ///     non-null return values.
    /// </summary>
    /// <param name="memberInfo">A System.Reflection.MemberInfo that describes the member.</param>
    /// <param name="dataContractType">A System.Type.</param>
    /// <returns>An object that represents the annotation to be inserted into the XML schema
    /// definition.</returns>
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
    public Type GetDataContractType(Type type) => type.IsAssignableFrom(typeof (PageCustomFieldsViewModel)) ? TypeSurrogateFactory.Instance.GetSurrogateType(type) : type;

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
    public object GetDeserializedObject(object obj, Type targetType) => obj;

    public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
    {
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
      if (!(obj is PageCustomFieldsViewModel customFieldsViewModel))
        return obj;
      PageNode node = customFieldsViewModel.GetNode();
      object instance = Activator.CreateInstance(TypeSurrogateFactory.Instance.GetSurrogateType(obj.GetType()));
      PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties(instance);
      PropertyDescriptorCollection properties2 = TypeDescriptor.GetProperties((object) node);
      foreach (PropertyDescriptor property in properties1)
      {
        PropertyDescriptor dynamicProperty = properties2[property.Name];
        if (dynamicProperty != null)
        {
          object obj1 = dynamicProperty.GetValue((object) node);
          object obj2 = this.ProcessSpecialValueCases(dynamicProperty, obj1);
          this.SetPropertyValue(property, instance, obj2);
        }
      }
      return instance;
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

    /// <summary>Processes the special value cases such IList[Guid]</summary>
    /// <param name="dynamicProperty">The dynamic property.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    private object ProcessSpecialValueCases(PropertyDescriptor dynamicProperty, object value)
    {
      if (typeof (IList<Guid>).Equals(dynamicProperty.PropertyType))
      {
        List<Guid> guidList = new List<Guid>();
        foreach (Guid guid in (IEnumerable<Guid>) (value as IList<Guid>))
          guidList.Add(guid);
        value = (object) guidList;
      }
      else if (dynamicProperty.IsLongText() && value != null)
        value = (object) new Lstring(HtmlFilterProvider.ApplyFilters(value.ToString()));
      return value;
    }

    private void SetPropertyValue(PropertyDescriptor property, object item, object value)
    {
      if (property.PropertyType.FullName == typeof (DateTime).FullName && (value == null || value is DateTime dateTime && dateTime == DateTime.MinValue))
        property.SetValue(item, (object) DateTime.UtcNow);
      else
        property.SetValue(item, value);
    }
  }
}
