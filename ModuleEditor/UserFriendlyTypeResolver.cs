// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.UserFriendlyTypeResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.ModuleEditor
{
  /// <summary>
  /// This class is used to resolve and unresolve user friendly types to actual CLR types.
  /// </summary>
  public static class UserFriendlyTypeResolver
  {
    /// <summary>
    /// Resolves a type from the property descriptor to the user friendly type.
    /// </summary>
    /// <param name="propertyDescriptor">Property descriptor from which the user friendly type will be resolved.</param>
    /// <returns>Value of the <see cref="T:Telerik.Sitefinity.Model.UserFriendlyDataType" /> to which property descriptor was resolved to.</returns>
    public static UserFriendlyDataType ResolveType(
      PropertyDescriptor propertyDescriptor)
    {
      MetaFieldAttributeAttribute attributeAttribute = propertyDescriptor != null ? propertyDescriptor.Attributes.OfType<MetaFieldAttributeAttribute>().SingleOrDefault<MetaFieldAttributeAttribute>() : throw new ArgumentNullException(nameof (propertyDescriptor));
      string str;
      UserFriendlyDataType result;
      if (attributeAttribute != null && attributeAttribute.Attributes.TryGetValue("UserFriendlyDataType", out str) && Enum.TryParse<UserFriendlyDataType>(str, out result))
        return result;
      UserFriendlyDataTypeAttribute dataTypeAttribute = propertyDescriptor.Attributes.OfType<UserFriendlyDataTypeAttribute>().SingleOrDefault<UserFriendlyDataTypeAttribute>();
      if (dataTypeAttribute != null)
        return dataTypeAttribute.DataType;
      Type nullableType = propertyDescriptor.PropertyType;
      Type underlyingType = Nullable.GetUnderlyingType(nullableType);
      if (underlyingType != (Type) null)
        nullableType = underlyingType;
      if (nullableType == typeof (bool))
        return UserFriendlyDataType.YesNo;
      if (nullableType == typeof (DateTime))
        return UserFriendlyDataType.DateAndTime;
      if (nullableType == typeof (Guid) && propertyDescriptor.Name == "Id")
        return UserFriendlyDataType.Identifier;
      if (propertyDescriptor.GetType() == typeof (TaxonomyPropertyDescriptor))
        return UserFriendlyDataType.Classification;
      int num1 = nullableType == typeof (sbyte) || nullableType == typeof (short) || nullableType == typeof (int) ? 1 : (nullableType == typeof (long) ? 1 : 0);
      bool flag1 = nullableType == typeof (byte) || nullableType == typeof (ushort) || nullableType == typeof (uint) || nullableType == typeof (ulong);
      bool flag2 = nullableType == typeof (float) || nullableType == typeof (double) || nullableType == typeof (Decimal);
      int num2 = flag1 ? 1 : 0;
      return (num1 | num2 | (flag2 ? 1 : 0)) != 0 ? UserFriendlyDataType.Number : UserFriendlyDataType.Unknown;
    }

    /// <summary>
    /// Resolves the type to a localized string representation of the user friendly type.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <returns>A localized string representation of the user friendly type.</returns>
    public static string ResolveTypeToString(PropertyDescriptor propertyDescriptor)
    {
      UserFriendlyDataType friendlyDataType = UserFriendlyTypeResolver.ResolveType(propertyDescriptor);
      try
      {
        return Res.Get<ModuleEditorResources>().Get(friendlyDataType.ToString());
      }
      catch (Exception ex)
      {
        return friendlyDataType.ToString();
      }
    }
  }
}
