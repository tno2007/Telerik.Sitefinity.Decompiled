// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.ContentComparator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Versioning.Serialization.Attributes;
using Telerik.Sitefinity.Versioning.Web.UI.Contracts;

namespace Telerik.Sitefinity.Versioning.Comparison
{
  /// <summary>
  /// A class used to generate differences and compare objects.
  /// </summary>
  public class ContentComparator
  {
    private IEnumerable<IComparisonFieldDefinition> compareFields;
    private DiffEngine diff;
    private static List<Guid> _baseTypes;

    public ContentComparator()
    {
      this.diff = new DiffEngine();
      this.Settings = new ContentComparatorSettings();
    }

    public ContentComparator(
      IEnumerable<IComparisonFieldDefinition> fieldDefinitions)
    {
      this.diff = new DiffEngine();
      this.Settings = new ContentComparatorSettings();
      this.compareFields = fieldDefinitions;
    }

    public ContentComparatorSettings Settings { get; set; }

    internal void ApplySettings()
    {
      this.diff.BeginTag = this.Settings.BeginTagFormat;
      this.diff.EndTag = this.Settings.EndTagFormat;
    }

    /// <summary>
    /// Compares two object instances and returns the difference object.
    /// </summary>
    /// <param name="origVersion">The original version of the object.</param>
    /// <param name="newVersion">The new version of the object.</param>
    /// <returns>Object representing the difference between the two objects.</returns>
    public IList<CompareResult> Compare(object origVersion, object newVersion) => this.CompareObjects(origVersion, newVersion, SystemManager.CurrentContext.Culture);

    /// <summary>
    /// Compares two object instances and returns the difference object.
    /// </summary>
    /// <param name="origVersion">The original version of the object.</param>
    /// <param name="newVersion">The new version of the object.</param>
    /// <returns>Object representing the difference between the two objects.</returns>
    public IList<CompareResult> Compare(
      object origVersion,
      object newVersion,
      string culture)
    {
      CultureInfo culture1 = new CultureInfo(culture);
      return this.CompareObjects(origVersion, newVersion, culture1);
    }

    /// <summary>
    /// Compares two object instances and returns the difference object.
    /// </summary>
    /// <param name="origVersion">The original version of the object.</param>
    /// <param name="newVersion">The new version of the object.</param>
    /// <param name="culture">The culture</param>
    /// <returns>Object representing the difference between the two objects.</returns>
    public IList<CompareResult> Compare(
      object origVersion,
      object newVersion,
      CultureInfo culture)
    {
      return this.CompareObjects(origVersion, newVersion, culture);
    }

    public IList<CompareResult> CompareBaseTypes(
      object origVersion,
      object newVersion)
    {
      List<CompareResult> compareResultList = new List<CompareResult>();
      if (origVersion.GetType().Equals(typeof (string)))
      {
        CompareResult compareResult = this.CompareStrings((string) origVersion, (string) newVersion);
        compareResultList.Add(compareResult);
        return (IList<CompareResult>) compareResultList;
      }
      CompareResult compareResult1 = this.CompareValues(origVersion, newVersion);
      compareResultList.Add(compareResult1);
      return (IList<CompareResult>) compareResultList;
    }

    private IList<CompareResult> CompareObjects(
      object origVersion,
      object newVersion,
      CultureInfo culture)
    {
      Type type = origVersion.GetType();
      if (!type.Equals(newVersion.GetType()))
        throw new ArgumentException("Objects must be of the same type");
      List<CompareResult> compareResultList = new List<CompareResult>();
      if (ContentComparator.IsBaseType(type))
        return this.CompareBaseTypes(origVersion, newVersion);
      if (type.IsEnum)
      {
        CompareResult compareResult = this.CompareEnums((Enum) origVersion, (Enum) newVersion);
        compareResultList.Add(compareResult);
        return (IList<CompareResult>) compareResultList;
      }
      if (type.Equals(typeof (Lstring)))
      {
        CompareResult compareResult = this.CompareLStrings((Lstring) origVersion, (Lstring) newVersion, culture);
        compareResultList.Add(compareResult);
        return (IList<CompareResult>) compareResultList;
      }
      if (type.Equals(typeof (string)))
      {
        CompareResult compareResult = this.CompareStrings((string) origVersion, (string) newVersion);
        compareResultList.Add(compareResult);
        return (IList<CompareResult>) compareResultList;
      }
      PropertyDescriptorCollection descriptorsForObject = this.GetPropertyDescriptorsForObject(origVersion);
      PropertyDescriptor[] source1 = new PropertyDescriptor[descriptorsForObject.Count];
      descriptorsForObject.CopyTo((Array) source1, 0);
      foreach (IComparisonFieldDefinition compareField1 in this.compareFields)
      {
        IComparisonFieldDefinition compareField = compareField1;
        PropertyDescriptor propertyDescriptor = ((IEnumerable<PropertyDescriptor>) source1).Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Name == compareField.FieldName)).FirstOrDefault<PropertyDescriptor>();
        if (propertyDescriptor != null && (propertyDescriptor.Attributes[typeof (NonSerializableProperty)] == null || propertyDescriptor.Attributes[typeof (PropertyComparerAttribute)] != null))
        {
          if (propertyDescriptor.Attributes[typeof (PropertyComparerAttribute)] != null)
          {
            PropertyComparerAttribute attr = propertyDescriptor.Attributes[typeof (PropertyComparerAttribute)] as PropertyComparerAttribute;
            PropertyDescriptor[] relatedPropertiesDescriptors = attr.RelatedFields == null ? new PropertyDescriptor[0] : ((IEnumerable<PropertyDescriptor>) source1).Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => ((IEnumerable<string>) attr.RelatedFields).Contains<string>(x.Name))).ToArray<PropertyDescriptor>();
            CompareResult compareResult = this.CompareProperty(propertyDescriptor, origVersion, newVersion, relatedPropertiesDescriptors, attr.CompareType);
            compareResultList.Add(compareResult);
          }
          else if (propertyDescriptor.PropertyType.Equals(typeof (string)))
          {
            CompareResult compareResult = this.CompareStrings((string) propertyDescriptor.GetValue(origVersion) ?? string.Empty, (string) propertyDescriptor.GetValue(newVersion) ?? string.Empty);
            compareResult.PropertyName = propertyDescriptor.Name;
            compareResultList.Add(compareResult);
          }
          else if (propertyDescriptor.PropertyType.Equals(typeof (Lstring)))
          {
            CompareResult compareResult = this.CompareLStrings((Lstring) propertyDescriptor.GetValue(origVersion), (Lstring) propertyDescriptor.GetValue(newVersion), culture);
            compareResult.PropertyName = propertyDescriptor.Name;
            compareResultList.Add(compareResult);
          }
          else if (propertyDescriptor.PropertyType.IsEnum)
          {
            CompareResult compareResult = this.CompareEnums((Enum) propertyDescriptor.GetValue(origVersion), (Enum) propertyDescriptor.GetValue(newVersion));
            compareResult.PropertyName = propertyDescriptor.Name;
            compareResultList.Add(compareResult);
          }
          else if (propertyDescriptor.GetType().IsAssignableFrom(typeof (TaxonomyPropertyDescriptor)))
          {
            CompareResult compareResult = this.CompareTaxonomies(((IEnumerable<Guid>) propertyDescriptor.GetValue(origVersion)).ToList<Guid>(), ((IEnumerable<Guid>) propertyDescriptor.GetValue(newVersion)).ToList<Guid>(), TaxonomyManager.GetManager(((TaxonomyPropertyDescriptor) propertyDescriptor).TaxonomyProvider), culture);
            compareResult.PropertyName = propertyDescriptor.Name;
            compareResultList.Add(compareResult);
          }
          else if (propertyDescriptor.PropertyType.Equals(typeof (Address)))
          {
            Address address1 = (Address) propertyDescriptor.GetValue(origVersion);
            Address address2 = (Address) propertyDescriptor.GetValue(newVersion);
            CompareResult compareResult = this.CompareStrings(address1 != null ? address1.ToString() : string.Empty, address2 != null ? address2.ToString() : string.Empty);
            compareResult.PropertyName = propertyDescriptor.Name;
            compareResultList.Add(compareResult);
          }
          else if (propertyDescriptor.PropertyType.Equals(typeof (ChoiceOption)))
          {
            ChoiceOption choiceOption1 = (ChoiceOption) propertyDescriptor.GetValue(origVersion);
            ChoiceOption choiceOption2 = (ChoiceOption) propertyDescriptor.GetValue(newVersion);
            CompareResult compareResult = this.CompareStrings(choiceOption1 != null ? choiceOption1.Text : string.Empty, choiceOption2 != null ? choiceOption2.Text : string.Empty);
            compareResult.PropertyName = propertyDescriptor.Name;
            compareResultList.Add(compareResult);
          }
          else if (propertyDescriptor.PropertyType.IsArray && propertyDescriptor.PropertyType.GetElementType().Equals(typeof (ChoiceOption)))
          {
            ChoiceOption[] source2 = (ChoiceOption[]) propertyDescriptor.GetValue(origVersion);
            ChoiceOption[] source3 = (ChoiceOption[]) propertyDescriptor.GetValue(newVersion);
            CompareResult compareResult = this.CompareStrings(string.Join("; ", ((IEnumerable<ChoiceOption>) source2).Select<ChoiceOption, string>((Func<ChoiceOption, string>) (c => c.Text))), string.Join("; ", ((IEnumerable<ChoiceOption>) source3).Select<ChoiceOption, string>((Func<ChoiceOption, string>) (c => c.Text))));
            compareResult.PropertyName = propertyDescriptor.Name;
            compareResultList.Add(compareResult);
          }
          else if (!propertyDescriptor.PropertyType.IsArray || !propertyDescriptor.PropertyType.GetElementType().Equals(typeof (Guid)))
          {
            object origValue = propertyDescriptor.GetValue(origVersion);
            object newValue = propertyDescriptor.GetValue(newVersion);
            if (origValue != null && newValue != null)
            {
              CompareResult compareResult = this.CompareValues(origValue, newValue);
              compareResult.PropertyName = propertyDescriptor.Name;
              compareResultList.Add(compareResult);
            }
            else
            {
              CompareResult compareResult = new CompareResult()
              {
                AreDifferent = origValue != newValue,
                PropertyName = propertyDescriptor.Name
              };
              string usingSpecificFormat1 = this.ConvertObjectToStringUsingSpecificFormat(origValue);
              string usingSpecificFormat2 = this.ConvertObjectToStringUsingSpecificFormat(newValue);
              compareResult.DiffHtml = this.GetDiffHtml(usingSpecificFormat1, usingSpecificFormat2, compareResult.AreDifferent);
              compareResultList.Add(compareResult);
            }
          }
        }
      }
      return (IList<CompareResult>) compareResultList;
    }

    /// <summary>Compares the content links.</summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    /// <returns></returns>
    internal virtual PropertyDescriptorCollection GetPropertyDescriptorsForObject(
      object obj)
    {
      return TypeDescriptor.GetProperties(obj);
    }

    public CompareResult CompareTaxonomies(
      List<Guid> oldValue,
      List<Guid> newValue,
      TaxonomyManager managerInstance,
      CultureInfo culture)
    {
      CompareResult compareResult = new CompareResult();
      List<string> first1 = new List<string>();
      List<string> first2 = new List<string>();
      List<string> second = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Guid id in oldValue)
      {
        ITaxon taxon = managerInstance.GetTaxon(id);
        if (taxon != null)
        {
          if (newValue.Contains(id))
          {
            second.Add(HttpUtility.HtmlEncode(taxon.Title[culture]));
            if (stringBuilder.Length > 0)
              stringBuilder.Append(",");
            stringBuilder.Append(string.Format("\n<span>{0}</span>\n", (object) HttpUtility.HtmlEncode(taxon.Title[culture])));
          }
          else
          {
            first2.Add(HttpUtility.HtmlEncode(taxon.Title[culture]));
            if (stringBuilder.Length > 0)
              stringBuilder.Append(",");
            stringBuilder.Append(string.Format("\n<span class='{0}'>{1}</span>\n", (object) this.Settings.DeletedCSSClassName, (object) HttpUtility.HtmlEncode(taxon.Title[culture])));
          }
        }
      }
      foreach (Guid id in newValue)
      {
        ITaxon taxon = managerInstance.GetTaxon(id);
        if (!oldValue.Contains(id) && taxon != null)
        {
          first1.Add(taxon.Title[culture]);
          if (stringBuilder.Length > 0)
            stringBuilder.Append(",");
          stringBuilder.Append(string.Format("\n<span class='{0}'>{1}</span>\n", (object) this.Settings.AddedCSSClassName, (object) HttpUtility.HtmlEncode(taxon.Title[culture])));
        }
      }
      compareResult.AreDifferent = first1.Count > 0 || first2.Count < 0;
      compareResult.OldValue = string.Join(",", first2.Concat<string>((IEnumerable<string>) second).ToArray<string>());
      compareResult.NewValue = string.Join(",", first1.Concat<string>((IEnumerable<string>) second).ToArray<string>());
      compareResult.DiffHtml = stringBuilder.ToString();
      return compareResult;
    }

    public CompareResult CompareProperty(
      PropertyDescriptor propertyDescriptor,
      object origVersion,
      object newVersion,
      PropertyDescriptor[] relatedPropertiesDescriptors,
      string compareType)
    {
      string str1 = propertyDescriptor != null ? propertyDescriptor.GetValue(origVersion).ToString() : throw new ArgumentNullException(nameof (propertyDescriptor));
      string str2 = propertyDescriptor.GetValue(newVersion).ToString();
      CompareResult compareResult = new CompareResult();
      compareResult.CompareType = compareType;
      compareResult.OldValueRelatedProperties = new Dictionary<string, object>();
      compareResult.NewValueRelatedProperties = new Dictionary<string, object>();
      if (relatedPropertiesDescriptors != null)
      {
        foreach (PropertyDescriptor propertiesDescriptor in relatedPropertiesDescriptors)
        {
          compareResult.OldValueRelatedProperties.Add(propertiesDescriptor.Name, propertiesDescriptor.GetValue(origVersion));
          compareResult.NewValueRelatedProperties.Add(propertiesDescriptor.Name, propertiesDescriptor.GetValue(newVersion));
        }
      }
      compareResult.OldValue = str1;
      compareResult.NewValue = str2;
      compareResult.PropertyName = propertyDescriptor.Name;
      return compareResult;
    }

    public CompareResult CompareLStrings(
      Lstring origValue,
      Lstring newValue,
      CultureInfo culture)
    {
      return this.CompareStrings(origValue[culture], newValue[culture]);
    }

    public CompareResult CompareStrings(string origValue, string newValue)
    {
      CompareResult compareResult = new CompareResult();
      origValue = origValue == null ? string.Empty : origValue;
      newValue = newValue == null ? string.Empty : newValue;
      origValue = HttpUtility.HtmlEncode(origValue);
      newValue = HttpUtility.HtmlEncode(newValue);
      if (origValue.CompareTo(newValue) != 0)
      {
        compareResult.AreDifferent = true;
        compareResult.DiffHtml = this.diff.GetDiffs(newValue, origValue);
      }
      else
      {
        compareResult.AreDifferent = false;
        compareResult.DiffHtml = newValue;
      }
      compareResult.OldValue = origValue;
      compareResult.NewValue = newValue;
      return compareResult;
    }

    private CompareResult CompareValues(object origValue, object newValue)
    {
      CompareResult compareResult = new CompareResult();
      Type type = origValue.GetType();
      if (type.Equals(typeof (bool)))
      {
        bool flag1 = (bool) origValue;
        bool flag2 = (bool) newValue;
        compareResult.AreDifferent = (uint) flag1.CompareTo(flag2) > 0U;
        compareResult.NewValue = flag2.ToString(this.Settings.BoolDisplayFormat);
        compareResult.OldValue = flag1.ToString(this.Settings.BoolDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (DateTime)))
      {
        DateTime dateTime1 = (DateTime) origValue;
        DateTime dateTime2 = (DateTime) newValue;
        compareResult.AreDifferent = (uint) dateTime1.CompareTo(dateTime2) > 0U;
        compareResult.NewValue = dateTime2.ToString(this.Settings.DateTimeDisplayFormat);
        compareResult.OldValue = dateTime1.ToString(this.Settings.DateTimeDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (int)))
      {
        int num1 = (int) origValue;
        int num2 = (int) newValue;
        compareResult.AreDifferent = (uint) num1.CompareTo(num2) > 0U;
        compareResult.NewValue = num2.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.OldValue = num1.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (double)))
      {
        double num3 = (double) origValue;
        double num4 = (double) newValue;
        compareResult.AreDifferent = (uint) num3.CompareTo(num4) > 0U;
        compareResult.NewValue = num4.ToString(this.Settings.DoubleDisplayFormat);
        compareResult.OldValue = num3.ToString(this.Settings.DoubleDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (Decimal)))
      {
        Decimal num5 = (Decimal) origValue;
        Decimal num6 = (Decimal) newValue;
        compareResult.AreDifferent = (uint) num5.CompareTo(num6) > 0U;
        compareResult.NewValue = num6.ToString(this.Settings.DecimalDisplayFormat);
        compareResult.OldValue = num5.ToString(this.Settings.DecimalDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (byte)))
      {
        byte num7 = (byte) origValue;
        byte num8 = (byte) newValue;
        compareResult.AreDifferent = (uint) num7.CompareTo(num8) > 0U;
        compareResult.NewValue = num8.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.OldValue = num7.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (Guid)))
      {
        Guid guid1 = (Guid) origValue;
        Guid guid2 = (Guid) newValue;
        compareResult.AreDifferent = (uint) guid1.CompareTo(guid2) > 0U;
        compareResult.NewValue = guid2.ToString();
        compareResult.OldValue = guid1.ToString();
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (float)))
      {
        float num9 = (float) origValue;
        float num10 = (float) newValue;
        compareResult.AreDifferent = (uint) num9.CompareTo(num10) > 0U;
        compareResult.NewValue = num10.ToString(this.Settings.DoubleDisplayFormat);
        compareResult.OldValue = num9.ToString(this.Settings.DoubleDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (short)))
      {
        short num11 = (short) origValue;
        short num12 = (short) newValue;
        compareResult.AreDifferent = (uint) num11.CompareTo(num12) > 0U;
        compareResult.NewValue = num12.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.OldValue = num11.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (long)))
      {
        long num13 = (long) origValue;
        long num14 = (long) newValue;
        compareResult.AreDifferent = (uint) num13.CompareTo(num14) > 0U;
        compareResult.NewValue = num14.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.OldValue = num13.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (ushort)))
      {
        ushort num15 = (ushort) origValue;
        ushort num16 = (ushort) newValue;
        compareResult.AreDifferent = (uint) num15.CompareTo(num16) > 0U;
        compareResult.NewValue = num16.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.OldValue = num15.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (type.Equals(typeof (uint)))
      {
        uint num17 = (uint) origValue;
        uint num18 = (uint) newValue;
        compareResult.AreDifferent = (uint) num17.CompareTo(num18) > 0U;
        compareResult.NewValue = num18.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.OldValue = num17.ToString(this.Settings.IntegerDisplayFormat);
        compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
        return compareResult;
      }
      if (!type.Equals(typeof (ulong)))
        return compareResult;
      ulong num19 = (ulong) origValue;
      ulong num20 = (ulong) newValue;
      compareResult.AreDifferent = (uint) num19.CompareTo(num20) > 0U;
      compareResult.NewValue = num20.ToString(this.Settings.IntegerDisplayFormat);
      compareResult.OldValue = num19.ToString(this.Settings.IntegerDisplayFormat);
      compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
      return compareResult;
    }

    public CompareResult CompareEnums(Enum origValue, Enum newValue)
    {
      CompareResult compareResult = new CompareResult()
      {
        AreDifferent = (uint) origValue.CompareTo((object) newValue) > 0U,
        NewValue = newValue.ToString(),
        OldValue = origValue.ToString()
      };
      compareResult.DiffHtml = this.GetDiffHtml(compareResult.OldValue, compareResult.NewValue, compareResult.AreDifferent);
      return compareResult;
    }

    private string GetDiffHtml(string origValue, string newValue, bool areDifferent)
    {
      if (!areDifferent)
        return newValue;
      return string.Format("<span class='{2}'>{0}</span>&nbsp;<span class='{3}'>{1}</span>", (object) origValue, (object) newValue, (object) this.Settings.DeletedCSSClassName, (object) this.Settings.AddedCSSClassName);
    }

    /// <summary>
    /// Convert opbject to string using specific format, defined in the settings object.
    /// </summary>
    /// <param name="param"></param>
    private string ConvertObjectToStringUsingSpecificFormat(object param)
    {
      if (param == null)
        return string.Empty;
      Type type = param.GetType();
      if (type.Equals(typeof (bool)))
        return ((bool) param).ToString(this.Settings.BoolDisplayFormat);
      if (type.Equals(typeof (DateTime)))
        return ((DateTime) param).ToString(this.Settings.DateTimeDisplayFormat);
      if (type.Equals(typeof (int)))
        return ((int) param).ToString(this.Settings.IntegerDisplayFormat);
      if (type.Equals(typeof (double)))
        return ((double) param).ToString(this.Settings.DoubleDisplayFormat);
      if (type.Equals(typeof (Decimal)))
        return ((Decimal) param).ToString(this.Settings.DecimalDisplayFormat);
      if (type.Equals(typeof (byte)))
        return ((byte) param).ToString(this.Settings.IntegerDisplayFormat);
      if (type.Equals(typeof (Guid)))
        return param.ToString();
      if (type.Equals(typeof (float)))
        return ((double) param).ToString(this.Settings.DoubleDisplayFormat);
      if (type.Equals(typeof (short)))
        return ((short) param).ToString(this.Settings.IntegerDisplayFormat);
      if (type.Equals(typeof (long)))
        return ((long) param).ToString(this.Settings.IntegerDisplayFormat);
      if (type.Equals(typeof (ushort)))
        return ((ushort) param).ToString(this.Settings.IntegerDisplayFormat);
      if (type.Equals(typeof (uint)))
        return ((uint) param).ToString(this.Settings.IntegerDisplayFormat);
      return type.Equals(typeof (ulong)) ? ((ulong) param).ToString(this.Settings.IntegerDisplayFormat) : string.Empty;
    }

    public static bool IsBaseType(Type type)
    {
      if (ContentComparator._baseTypes == null)
        ContentComparator.RegisterBaseTypes();
      return ContentComparator._baseTypes.Contains(type.GUID);
    }

    private static void RegisterBaseTypes()
    {
      ContentComparator._baseTypes = new List<Guid>();
      ContentComparator._baseTypes.Add(typeof (byte).GUID);
      ContentComparator._baseTypes.Add(typeof (byte?).GUID);
      ContentComparator._baseTypes.Add(typeof (sbyte).GUID);
      ContentComparator._baseTypes.Add(typeof (sbyte?).GUID);
      ContentComparator._baseTypes.Add(typeof (bool).GUID);
      ContentComparator._baseTypes.Add(typeof (bool?).GUID);
      ContentComparator._baseTypes.Add(typeof (DateTime).GUID);
      ContentComparator._baseTypes.Add(typeof (DateTime?).GUID);
      ContentComparator._baseTypes.Add(typeof (Guid).GUID);
      ContentComparator._baseTypes.Add(typeof (Guid?).GUID);
      ContentComparator._baseTypes.Add(typeof (short).GUID);
      ContentComparator._baseTypes.Add(typeof (short?).GUID);
      ContentComparator._baseTypes.Add(typeof (ushort).GUID);
      ContentComparator._baseTypes.Add(typeof (ushort?).GUID);
      ContentComparator._baseTypes.Add(typeof (int).GUID);
      ContentComparator._baseTypes.Add(typeof (int?).GUID);
      ContentComparator._baseTypes.Add(typeof (uint).GUID);
      ContentComparator._baseTypes.Add(typeof (uint?).GUID);
      ContentComparator._baseTypes.Add(typeof (long).GUID);
      ContentComparator._baseTypes.Add(typeof (long?).GUID);
      ContentComparator._baseTypes.Add(typeof (ulong).GUID);
      ContentComparator._baseTypes.Add(typeof (ulong?).GUID);
      ContentComparator._baseTypes.Add(typeof (Decimal).GUID);
      ContentComparator._baseTypes.Add(typeof (Decimal?).GUID);
      ContentComparator._baseTypes.Add(typeof (double).GUID);
      ContentComparator._baseTypes.Add(typeof (double?).GUID);
      ContentComparator._baseTypes.Add(typeof (float).GUID);
      ContentComparator._baseTypes.Add(typeof (float?).GUID);
    }
  }
}
