// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Validation.PropertyValidation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.Services.Contracts.Validation
{
  internal static class PropertyValidation
  {
    private static string urlNameField = LinqHelper.MemberName<ILocatable>((Expression<Func<ILocatable, object>>) (x => x.UrlName));
    private static string additionalUrlsField = LinqHelper.MemberName<ILocatable>((Expression<Func<ILocatable, object>>) (x => x.Urls));
    private static string nameField = LinqHelper.MemberName<ITaxonomy>((Expression<Func<ITaxonomy, object>>) (x => x.Name));
    private static string defaultVARCHARDbLength = "255";

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method requires more than 65 lines.")]
    internal static BaseValidator GetFieldValidator(
      this PropertyDescriptor descriptor,
      string typeFullName)
    {
      NameValueCollection parameters = new NameValueCollection();
      bool flag1 = descriptor.IsRequired(typeFullName);
      bool flag2 = true;
      bool flag3 = true;
      int? nullable1 = new int?();
      int? nullable2 = new int?();
      int? nullable3 = new int?();
      DateTime date = DateTime.MaxValue.Date;
      DateTime min = SqlDateTime.MinValue.Value;
      string str1 = (string) null;
      string str2 = (string) null;
      MetaField metaField = descriptor.GetMetaField();
      IDynamicModuleField dynamicField = descriptor.GetDynamicField(typeFullName);
      if (dynamicField != null)
      {
        if (dynamicField.FieldType == FieldType.Number)
        {
          int result1 = 0;
          if (int.TryParse(dynamicField.MaxNumberRange, out result1))
            nullable1 = new int?(result1);
          int result2 = 0;
          if (int.TryParse(dynamicField.MinNumberRange, out result2))
            nullable2 = new int?(result2);
        }
        else if (dynamicField.FieldType == FieldType.DateTime)
        {
          PropertyValidation.SetDateRanges(dynamicField.MinNumberRange, dynamicField.MaxNumberRange, ref min, ref date);
        }
        else
        {
          nullable1 = PropertyValidation.GetFieldMaxLimitations(PropertyValidation.GetFieldDbLength(dynamicField), dynamicField.MaxLength.ToString());
          nullable2 = new int?(dynamicField.MinLength);
          nullable3 = dynamicField.RecommendedCharactersCount;
          if (dynamicField.FieldType == FieldType.ShortText)
          {
            int? nullable4 = nullable1;
            int num = 0;
            if (nullable4.GetValueOrDefault() == num & nullable4.HasValue)
              nullable1 = new int?();
          }
        }
        flag3 = dynamicField.CanSelectMultipleItems;
        str1 = dynamicField.RegularExpression;
        if (dynamicField.FieldType == FieldType.Guid)
        {
          if (flag1)
            str1 = str1.Replace("^", "^(?!00000000-0000-0000-0000-000000000000$)");
          else if (!string.IsNullOrEmpty(str1))
            flag1 = true;
        }
      }
      else
      {
        switch (descriptor)
        {
          case MetafieldPropertyDescriptor _:
          case DynamicLstringPropertyDescriptor _:
            if (metaField != null)
            {
              if (descriptor.IsDateAndTime())
              {
                PropertyValidation.SetDateRanges(metaField.MinValue, metaField.MaxValue, ref min, ref date);
              }
              else
              {
                nullable3 = metaField.RecommendedCharactersCount;
                nullable1 = PropertyValidation.GetFieldMaxLimitations(PropertyValidation.GetMetaFieldDbLength(metaField), metaField.MaxValue);
                if (!string.IsNullOrEmpty(metaField.MinValue))
                  nullable2 = new int?(int.Parse(metaField.MinValue));
              }
              str1 = metaField.RegularExpression;
              break;
            }
            break;
          default:
            RangeAttribute rangeAttribute = descriptor.Attributes.OfType<RangeAttribute>().FirstOrDefault<RangeAttribute>();
            if (rangeAttribute != null)
            {
              nullable1 = new int?(int.Parse(rangeAttribute.Maximum.ToString()));
              nullable2 = new int?(int.Parse(rangeAttribute.Minimum.ToString()));
            }
            StringLengthAttribute stringLengthAttribute = descriptor.Attributes.OfType<StringLengthAttribute>().FirstOrDefault<StringLengthAttribute>();
            if (stringLengthAttribute != null)
            {
              nullable1 = new int?(stringLengthAttribute.MaximumLength);
              nullable2 = new int?(stringLengthAttribute.MinimumLength);
            }
            RegexValidationAttribute validationAttribute1 = descriptor.Attributes.OfType<RegexValidationAttribute>().Where<RegexValidationAttribute>((Func<RegexValidationAttribute, bool>) (x => x.RegexPurpose == RegexPurpose.Validate)).FirstOrDefault<RegexValidationAttribute>();
            if (validationAttribute1 != null)
              str1 = validationAttribute1.Pattern;
            EditableAttribute editableAttribute = descriptor.Attributes.OfType<EditableAttribute>().FirstOrDefault<EditableAttribute>();
            if (editableAttribute != null)
              flag2 = editableAttribute.AllowEdit;
            MetadataMappingAttribute mappingAttribute = descriptor.Attributes.OfType<MetadataMappingAttribute>().FirstOrDefault<MetadataMappingAttribute>();
            if (mappingAttribute != null && !mappingAttribute.IsLong && descriptor is LstringPropertyDescriptor)
            {
              if (nullable1.HasValue)
              {
                int? nullable5 = nullable1;
                int dbLength = mappingAttribute.DBLength;
                if (!(nullable5.GetValueOrDefault() < dbLength & nullable5.HasValue))
                  break;
              }
              nullable1 = new int?(mappingAttribute.DBLength);
              break;
            }
            break;
        }
      }
      if (metaField != null)
      {
        int? nullable6 = new int?();
        if (metaField.MaxLength > 0)
        {
          nullable6 = new int?(metaField.MaxLength);
        }
        else
        {
          int result;
          if (int.TryParse(metaField.MaxValue, out result))
            nullable6 = new int?(result);
        }
        int? nullable7 = nullable1 ?? nullable6;
        nullable1 = PropertyValidation.GetFieldMaxLimitations(metaField.DBLength, nullable7.ToString());
      }
      DatabaseMappingsElement databaseMappingsElement;
      if (!nullable1.HasValue && Config.Get<MetadataConfig>().DatabaseMappings.TryGetValue(descriptor.GetFieldType().ToString(), out databaseMappingsElement) && databaseMappingsElement.DbLength != null)
        nullable1 = new int?(int.Parse(databaseMappingsElement.DbLength));
      BaseValidator fieldValidator = new BaseValidator();
      parameters.Add("IsRequired", flag1.ToString());
      parameters.Add("Updatable", flag2.ToString());
      if (nullable2.HasValue)
        parameters.Add("MinValue", nullable2.ToString());
      if (nullable1.HasValue)
        parameters.Add("MaxValue", nullable1.ToString());
      if (!string.IsNullOrEmpty(str1))
        parameters.Add("RegularExpression", str1);
      if (descriptor.IsLongText() || descriptor.IsShortText())
      {
        fieldValidator = (BaseValidator) new StringValidator();
        if (nullable3.HasValue)
          parameters.Add("RecommendedCharacters", nullable3.ToString());
      }
      else if (descriptor.IsNumeric())
      {
        fieldValidator = (BaseValidator) new NumericValidator();
        int decimalPlacesCount = descriptor.GetDecimalPlacesCount(typeFullName);
        parameters.Add("DecimalPlaces", decimalPlacesCount.ToString());
      }
      else if (descriptor.IsTaxonomy())
      {
        parameters.Add("AllowMultiple", flag3.ToString());
        fieldValidator = (BaseValidator) new TaxaValidator();
      }
      else if (descriptor.IsDateAndTime())
      {
        parameters.Add("MaxValue", date.ToIsoFormat());
        parameters.Add("MinValue", min.ToIsoFormat());
        fieldValidator = (BaseValidator) new DateTimeValidator();
      }
      Type componentType = descriptor.ComponentType;
      if (descriptor.Name == PropertyValidation.urlNameField)
      {
        if (!bool.Parse(parameters["IsRequired"]))
          parameters.Set("IsRequired", "true");
        fieldValidator = !typeof (ITaxon).IsAssignableFrom(TypeResolutionService.ResolveType(typeFullName)) ? (!typeof (Folder).IsAssignableFrom(TypeResolutionService.ResolveType(typeFullName)) ? (!typeof (PageNode).IsAssignableFrom(TypeResolutionService.ResolveType(typeFullName)) ? (BaseValidator) new UrlValidator() : (BaseValidator) new PageUrlValidator()) : (BaseValidator) new FolderUrlValidator()) : (BaseValidator) new TaxonUrlValidator();
        string str3 = Config.Get<SystemConfig>().SiteUrlSettings.ClientUrlTransformations.RegularExpressionFilter;
        if (string.IsNullOrEmpty(str3))
        {
          RegexValidationAttribute validationAttribute2 = descriptor.Attributes.OfType<RegexValidationAttribute>().Where<RegexValidationAttribute>((Func<RegexValidationAttribute, bool>) (x => x.RegexPurpose == RegexPurpose.Sanitize)).FirstOrDefault<RegexValidationAttribute>();
          if (validationAttribute2 != null)
            str3 = validationAttribute2.Pattern;
        }
        string str4 = Config.Get<SystemConfig>().SiteUrlSettings.ClientUrlTransformations.ReplaceWith;
        if (string.IsNullOrEmpty(str4))
          str4 = "-";
        if (str3 != null)
          str2 = str3;
        if (!string.IsNullOrEmpty(str2))
        {
          parameters.Add("SanitizeRegex", str2);
          parameters.Add("RegularExpressionReplaceCharacter", str4);
        }
        if (str1 == null || str1 == "[^\\w\\-\\!\\$\\'\\(\\)\\=\\@\\d_]+")
        {
          string str5 = "^([\\.]?[\\p{L}-_!'()@\\d])+$";
          parameters.Remove("RegularExpression");
          parameters.Add("RegularExpression", str5);
        }
      }
      else if (descriptor.Name == PropertyValidation.additionalUrlsField && typeof (ILocatable).IsAssignableFrom(componentType))
      {
        parameters.Add("ValidateRegex", "^(\\/|\\~\\/)?([\\.]?[\\p{L}-_!'()@\\d)][\\/]{0,1})+$");
        fieldValidator = (BaseValidator) new AdditionalUrlsValidator();
      }
      else if (descriptor.Name == PropertyValidation.nameField && (typeof (ITaxonomy).IsAssignableFrom(componentType) || typeof (ITaxon).IsAssignableFrom(componentType)))
      {
        if (typeof (ITaxonomy).IsAssignableFrom(componentType))
          fieldValidator = (BaseValidator) new TaxonomyNameValidator();
        RegexValidationAttribute validationAttribute3 = descriptor.Attributes.OfType<RegexValidationAttribute>().Where<RegexValidationAttribute>((Func<RegexValidationAttribute, bool>) (x => x.RegexPurpose == RegexPurpose.Sanitize)).FirstOrDefault<RegexValidationAttribute>();
        if (validationAttribute3 != null)
          str2 = validationAttribute3.Pattern;
        if (!string.IsNullOrEmpty(str2))
          parameters.Add("SanitizeRegex", str2);
      }
      if (!((IEnumerable<string>) parameters.AllKeys).Any<string>())
        fieldValidator = (BaseValidator) null;
      fieldValidator?.Init(parameters);
      return fieldValidator;
    }

    private static string GetFieldDbLength(IDynamicModuleField field)
    {
      if (field == null)
        return (string) null;
      return string.IsNullOrWhiteSpace(field.DBLength) && field.DBType == "VARCHAR" ? PropertyValidation.defaultVARCHARDbLength : field.DBLength;
    }

    private static string GetMetaFieldDbLength(MetaField metaField)
    {
      if (metaField == null)
        return (string) null;
      return string.IsNullOrWhiteSpace(metaField.DBLength) && metaField.DBType == "VARCHAR" ? PropertyValidation.defaultVARCHARDbLength : metaField.DBLength;
    }

    internal static int? GetFieldMaxLimitations(string databaseLength, string maxLength)
    {
      if (!string.IsNullOrWhiteSpace(databaseLength) && !int.TryParse(databaseLength, out int _))
        databaseLength = PropertyValidation.defaultVARCHARDbLength;
      if (string.IsNullOrEmpty(databaseLength) && !string.IsNullOrEmpty(maxLength))
        return new int?(int.Parse(maxLength));
      if (!string.IsNullOrEmpty(databaseLength) && string.IsNullOrEmpty(maxLength))
        return new int?(int.Parse(databaseLength));
      if (string.IsNullOrEmpty(databaseLength) || string.IsNullOrEmpty(maxLength))
        return new int?();
      int num1 = int.Parse(databaseLength);
      int num2 = int.Parse(maxLength);
      if (num1 == 0 && num2 > 0)
        return new int?(num2);
      if (num2 == 0 && num1 > 0)
        return new int?(num1);
      return num1 < num2 ? new int?(num1) : new int?(num2);
    }

    private static void SetDateRanges(
      string dateMin,
      string dateMax,
      ref DateTime min,
      ref DateTime max)
    {
      DateTime result;
      if (DateTime.TryParseExact(dateMax, "d/M/yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        max = result;
      if (!DateTime.TryParseExact(dateMin, "d/M/yyyy", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        return;
      min = result;
    }
  }
}
