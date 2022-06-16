// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.DescriptorExtensons
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>Class providing property descriptor extension methods</summary>
  public static class DescriptorExtensons
  {
    internal static bool IsLongText(this PropertyDescriptor descriptor) => descriptor.IsFieldType(UserFriendlyDataType.LongText);

    internal static bool IsShortText(this PropertyDescriptor descriptor) => descriptor.IsFieldType(UserFriendlyDataType.ShortText);

    internal static bool IsNumeric(this PropertyDescriptor descriptor) => descriptor.IsFieldType(UserFriendlyDataType.Number) || descriptor.IsFieldType(UserFriendlyDataType.Currency);

    internal static bool IsGuid(this PropertyDescriptor descriptor) => descriptor.IsFieldType(UserFriendlyDataType.Guid);

    internal static bool IsTaxonomy(this PropertyDescriptor descriptor) => descriptor.IsFieldType(UserFriendlyDataType.Classification) || descriptor is TaxonomyPropertyDescriptor;

    internal static bool IsChoices(this PropertyDescriptor descriptor) => descriptor.IsFieldType(UserFriendlyDataType.Choices) || descriptor is SingleChoicePropertyDescriptor;

    internal static bool IsMultipleChoice(this PropertyDescriptor descriptor) => descriptor.IsFieldType(UserFriendlyDataType.MultipleChoice) || descriptor is MultipleChoicesPropertyDescriptor;

    internal static bool IsDateAndTime(this PropertyDescriptor descriptor) => descriptor.IsFieldType(UserFriendlyDataType.DateAndTime) || descriptor.IsFieldType(UserFriendlyDataType.Date);

    internal static bool IsUnknown(this PropertyDescriptor descriptor) => descriptor.IsFieldType(UserFriendlyDataType.Unknown);

    internal static int GetDecimalPlacesCount(
      this PropertyDescriptor descriptor,
      string typeFullName)
    {
      if (!descriptor.IsNumeric())
        throw new ArgumentException("The provided descriptor is not of type number");
      int result = 0;
      if (typeof (DynamicContent).IsAssignableFrom(descriptor.ComponentType))
      {
        IDynamicModuleField dynamicField = descriptor.GetDynamicField(typeFullName);
        if (dynamicField != null)
          result = dynamicField.DecimalPlacesCount;
      }
      else if (descriptor is MetafieldPropertyDescriptor propertyDescriptor)
        int.TryParse(propertyDescriptor.MetaField.DBScale, out result);
      return result;
    }

    internal static bool IsRequired(this PropertyDescriptor descriptor, string typeFullName)
    {
      IDynamicModuleField dynamicField = descriptor.GetDynamicField(typeFullName);
      if (dynamicField != null)
        return dynamicField.IsRequired;
      if (descriptor is MetafieldPropertyDescriptor || descriptor is DynamicLstringPropertyDescriptor)
      {
        MetaField metaField = descriptor.GetMetaField();
        if (metaField != null)
          return metaField.Required;
      }
      else if (descriptor.Attributes.OfType<RequiredAttribute>().Any<RequiredAttribute>())
        return true;
      return false;
    }

    internal static string GetUnits(this PropertyDescriptor descriptor, string typeFullName)
    {
      if (!descriptor.IsNumeric())
        throw new ArgumentException("The provided descriptor is not of type number");
      if (typeof (DynamicContent).IsAssignableFrom(descriptor.ComponentType))
      {
        IDynamicModuleField dynamicField = descriptor.GetDynamicField(typeFullName);
        if (dynamicField != null)
          return dynamicField.NumberUnit;
      }
      return (string) null;
    }

    internal static bool IsHtmlField(this PropertyDescriptor descriptor, string typeFullName)
    {
      IDynamicModuleField dynamicField = descriptor.GetDynamicField(typeFullName);
      return dynamicField != null && dynamicField.WidgetTypeName == typeof (HtmlField).FullName;
    }

    internal static string FileExtensions(this PropertyDescriptor descriptor, string typeFullName)
    {
      if (typeof (DynamicContent).IsAssignableFrom(descriptor.ComponentType))
      {
        IDynamicModuleField dynamicField = descriptor.GetDynamicField(typeFullName);
        if (dynamicField != null)
          return dynamicField.FileExtensions;
      }
      return (string) null;
    }

    internal static int FileMaxSize(this PropertyDescriptor descriptor, string typeFullName)
    {
      if (typeof (DynamicContent).IsAssignableFrom(descriptor.ComponentType))
      {
        IDynamicModuleField dynamicField = descriptor.GetDynamicField(typeFullName);
        if (dynamicField != null)
          return dynamicField.FileMaxSize;
      }
      return 0;
    }

    internal static string GetFieldDescription(
      this PropertyDescriptor descriptor,
      string typeFullName)
    {
      IDynamicModuleField dynamicField = descriptor.GetDynamicField(typeFullName);
      if (dynamicField != null && !string.IsNullOrEmpty(dynamicField.InstructionalText))
        return dynamicField.InstructionalText;
      switch (descriptor)
      {
        case MetafieldPropertyDescriptor propertyDescriptor1:
          return propertyDescriptor1.Description;
        case DataPropertyDescriptor propertyDescriptor2:
          return propertyDescriptor2.Description;
        default:
          return (string) null;
      }
    }

    internal static IDynamicModuleField GetDynamicField(
      this PropertyDescriptor descriptor,
      string typeFullName)
    {
      if (typeof (DynamicContent).IsAssignableFrom(descriptor.ComponentType))
      {
        IDynamicModuleType dynamicModuleType = ModuleBuilderManager.GetModules().Active().SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (x => x.Types)).Where<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (x => x.GetFullTypeName() == typeFullName)).FirstOrDefault<IDynamicModuleType>();
        if (dynamicModuleType != null)
        {
          IDynamicModuleField dynamicField = dynamicModuleType.Fields.Where<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (x => x.Name == descriptor.Name)).FirstOrDefault<IDynamicModuleField>();
          if (dynamicField != null)
            return dynamicField;
        }
      }
      return (IDynamicModuleField) null;
    }

    internal static int? GetDbLengthFromMetaField(this PropertyDescriptor descriptor)
    {
      MetaField metaField = descriptor.GetMetaField();
      if (metaField == null)
        return new int?();
      return !string.IsNullOrEmpty(metaField.DBLength) ? new int?(int.Parse(metaField.DBLength, (IFormatProvider) CultureInfo.CurrentCulture)) : new int?();
    }

    internal static MetaField GetMetaField(this PropertyDescriptor descriptor)
    {
      switch (descriptor)
      {
        case DynamicLstringPropertyDescriptor propertyDescriptor1:
          return propertyDescriptor1.MetaField;
        case MetafieldPropertyDescriptor propertyDescriptor2:
          return propertyDescriptor2.MetaField;
        default:
          return (MetaField) null;
      }
    }

    /// <summary>Gets the field type of the descriptor</summary>
    /// <param name="descriptor">The descriptor</param>
    /// <returns>The field data type</returns>
    public static UserFriendlyDataType? GetFieldType(
      this PropertyDescriptor descriptor)
    {
      UserFriendlyDataType? fieldTypeParsed = new UserFriendlyDataType?();
      UserFriendlyDataTypeAttribute dataTypeAttribute = descriptor.Attributes.OfType<UserFriendlyDataTypeAttribute>().FirstOrDefault<UserFriendlyDataTypeAttribute>();
      if (dataTypeAttribute != null)
        fieldTypeParsed = new UserFriendlyDataType?(dataTypeAttribute.DataType);
      if (!fieldTypeParsed.HasValue)
      {
        MetaFieldAttributeAttribute attributeAttribute = descriptor.Attributes.OfType<MetaFieldAttributeAttribute>().FirstOrDefault<MetaFieldAttributeAttribute>();
        string str = (string) null;
        UserFriendlyDataType result;
        if (attributeAttribute != null && attributeAttribute.Attributes.TryGetValue(typeof (UserFriendlyDataType).Name, out str) && Enum.TryParse<UserFriendlyDataType>(str, out result))
          fieldTypeParsed = new UserFriendlyDataType?(result);
      }
      if (!fieldTypeParsed.HasValue && typeof (DynamicContent).IsAssignableFrom(descriptor.ComponentType))
      {
        IDynamicModuleType dynamicModuleType = ModuleBuilderManager.GetModules().Active().SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (x => x.Types)).Where<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (x => x.GetFullTypeName() == descriptor.ComponentType.FullName)).FirstOrDefault<IDynamicModuleType>();
        if (dynamicModuleType != null)
        {
          IDynamicModuleField field = dynamicModuleType.Fields.Where<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (x => x.Name == descriptor.Name)).FirstOrDefault<IDynamicModuleField>();
          if (field != null)
            fieldTypeParsed = DescriptorExtensons.GetDynamicFieldType(fieldTypeParsed, field);
        }
      }
      return fieldTypeParsed;
    }

    private static UserFriendlyDataType? GetDynamicFieldType(
      UserFriendlyDataType? fieldTypeParsed,
      IDynamicModuleField field)
    {
      switch (field.FieldType)
      {
        case FieldType.ShortText:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.ShortText);
          break;
        case FieldType.LongText:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.LongText);
          break;
        case FieldType.MultipleChoice:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.MultipleChoice);
          break;
        case FieldType.YesNo:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.YesNo);
          break;
        case FieldType.Currency:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.Currency);
          break;
        case FieldType.DateTime:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.DateAndTime);
          break;
        case FieldType.Number:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.Number);
          break;
        case FieldType.Classification:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.Classification);
          break;
        case FieldType.Guid:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.Guid);
          break;
        case FieldType.Choices:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.Choices);
          break;
        case FieldType.RelatedMedia:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.RelatedMedia);
          break;
        case FieldType.RelatedData:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.RelatedData);
          break;
        default:
          fieldTypeParsed = new UserFriendlyDataType?(UserFriendlyDataType.Unknown);
          break;
      }
      return fieldTypeParsed;
    }

    private static bool IsFieldType(
      this PropertyDescriptor descriptor,
      UserFriendlyDataType userFriendlyDataType)
    {
      UserFriendlyDataType? fieldType = descriptor.GetFieldType();
      if (fieldType.HasValue)
      {
        UserFriendlyDataType? nullable = fieldType;
        UserFriendlyDataType friendlyDataType = userFriendlyDataType;
        if (nullable.GetValueOrDefault() == friendlyDataType & nullable.HasValue)
          return true;
      }
      return false;
    }
  }
}
