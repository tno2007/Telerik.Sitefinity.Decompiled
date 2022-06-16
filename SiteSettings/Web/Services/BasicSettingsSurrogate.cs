// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Web.Services.BasicSettingsSurrogate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data.WcfHelpers;

namespace Telerik.Sitefinity.SiteSettings.Web.Services
{
  /// <summary>
  /// Provides the methods needed to substitute one type for another by the System.Runtime.Serialization.DataContractSerializer
  /// during serialization, deserialization, and export and import of XML schema
  /// documents (XSD).
  /// This type will try to export types suitable for serialization, whenever a type is ISettingsDataContract.
  /// The substitute type should be provided as a query string parameter 'itemType'.
  /// </summary>
  public class BasicSettingsSurrogate : IDataContractSurrogate
  {
    /// <inheritdoc />
    public object GetCustomDataToExport(Type clrType, Type dataContractType) => (object) null;

    /// <inheritdoc />
    public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType) => (object) null;

    /// <inheritdoc />
    public Type GetDataContractType(Type type)
    {
      if (type.Equals(typeof (ISettingsDataContract)))
      {
        Type itemType = WcfContext.ItemType;
        if (itemType != (Type) null && type.IsAssignableFrom(itemType))
          return itemType;
      }
      return type;
    }

    /// <inheritdoc />
    public object GetDeserializedObject(object obj, Type targetType) => obj;

    /// <inheritdoc />
    public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
    {
    }

    /// <inheritdoc />
    public object GetObjectToSerialize(object obj, Type targetType) => obj;

    /// <inheritdoc />
    public Type GetReferencedTypeOnImport(
      string typeName,
      string typeNamespace,
      object customData)
    {
      return (Type) null;
    }

    /// <inheritdoc />
    public CodeTypeDeclaration ProcessImportedType(
      CodeTypeDeclaration typeDeclaration,
      CodeCompileUnit compileUnit)
    {
      return typeDeclaration;
    }
  }
}
