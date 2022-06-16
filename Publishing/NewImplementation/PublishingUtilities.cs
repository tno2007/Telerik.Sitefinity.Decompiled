// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.NewImplementation.PublishingUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Publishing.NewImplementation
{
  /// <summary>Utilities for publishing</summary>
  public static class PublishingUtilities
  {
    private static readonly string[] AdditionalFieldsForContentUsages = new string[2]
    {
      "Content",
      "PersonalizedContent"
    };

    /// <summary>
    /// Builds the categories field for the given content item and adds the value to the wrapper object.
    /// </summary>
    /// <param name="item">The wrapper object.</param>
    /// <param name="contentItem">The content item.</param>
    public static void AddItemCategories(WrapperObject item, IDataItem contentItem)
    {
      if (item == null)
        return;
      CultureInfo ci = (CultureInfo) null;
      if (!item.Language.IsNullOrEmpty())
        ci = new CultureInfo(item.Language);
      IEnumerable<TaxonomyPropertyDescriptor> propertyDescriptors = TypeDescriptor.GetProperties((object) contentItem).OfType<TaxonomyPropertyDescriptor>();
      Dictionary<string, object> additionalProperties = item.AdditionalProperties;
      foreach (TaxonomyPropertyDescriptor descriptor in propertyDescriptors)
      {
        string name = descriptor.Name;
        string[] taxonNames = PublishingUtilities.GetTaxonNames((object) contentItem, descriptor, ci);
        if (name != "Category")
        {
          if (!additionalProperties.ContainsKey(name))
            additionalProperties.Add(name, (object) taxonNames);
          else
            additionalProperties[name] = (object) taxonNames;
        }
        else
          item.SetOrAddProperty("Categories", (object) taxonNames);
      }
    }

    /// <summary>Gets the search index additional fields from settings</summary>
    /// <param name="settings">The settings.</param>
    /// <returns>Search index additional fields.</returns>
    public static IEnumerable<string> SearchIndexAdditionalFields(PipeSettings settings)
    {
      string str1;
      if (!settings.AdditionalData.TryGetValue(nameof (SearchIndexAdditionalFields), out str1) || string.IsNullOrEmpty(str1))
        return (IEnumerable<string>) null;
      string[] strArray = str1.Split(new string[2]
      {
        ",",
        Environment.NewLine
      }, StringSplitOptions.RemoveEmptyEntries);
      List<string> stringList = new List<string>(strArray.Length);
      foreach (string str2 in strArray)
      {
        string str3 = str2.Trim();
        if (!string.IsNullOrEmpty(str3))
          stringList.Add(str3);
      }
      return (IEnumerable<string>) stringList;
    }

    /// <summary>
    /// Builds the content usages field for the given content item and adds the value to the wrapper object.
    /// </summary>
    /// <param name="item">The wrapper object.</param>
    /// <param name="contentItem">The content item.</param>
    internal static void AddContentUsages(WrapperObject wrapperObject, IDataItem dataItem)
    {
      try
      {
        List<string> source = new List<string>();
        List<string> list1 = TypeDescriptor.GetProperties((object) dataItem).OfType<PropertyDescriptor>().Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.IsLongText())).Select<PropertyDescriptor, string>((Func<PropertyDescriptor, string>) (x => x.Name)).ToList<string>();
        foreach (string fieldsForContentUsage in PublishingUtilities.AdditionalFieldsForContentUsages)
        {
          if (!list1.Contains(fieldsForContentUsage))
            list1.Add(fieldsForContentUsage);
        }
        foreach (string str in list1)
        {
          string propertyName = str;
          object obj = (object) null;
          if (wrapperObject.TryGetProperty(propertyName, out obj) && obj != null)
          {
            string html = obj.ToString();
            if (!string.IsNullOrEmpty(html))
            {
              if (((IEnumerable<string>) PublishingUtilities.AdditionalFieldsForContentUsages).Any<string>((Func<string, bool>) (f => propertyName.Equals(f))) && wrapperObject.GetPropertyOrDefault<bool>("UnresolveContentField"))
                source.AddRange((IEnumerable<string>) LinkParser.GetDynamicLinks(html, true));
              source.AddRange((IEnumerable<string>) LinkParser.GetDynamicLinks(html, false));
            }
          }
        }
        List<string> list2 = source.Select<string, string>((Func<string, string>) (a => PublishingUtilities.SanitizeGuid(a))).ToList<string>();
        wrapperObject.SetOrAddProperty("ContentUsages", (object) string.Join(Environment.NewLine, (IEnumerable<string>) list2));
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
      }
    }

    internal static void AddContentLifecycleStatus(WrapperObject item, IDataItem contentItem)
    {
      if (!(contentItem is ILifecycleDataItem lifecycleDataItem))
        return;
      item.SetOrAddProperty("LifecycleStatus", (object) lifecycleDataItem.Status);
    }

    /// <summary>
    /// Transforms the value of the language field to a string that can be tokenized.
    /// </summary>
    /// <param name="language">The language field value.</param>
    /// <returns>The transformed value</returns>
    internal static string TransformLanguageFieldValue(string language)
    {
      switch (language)
      {
        case "":
          return language;
        case null:
          return "nullvalue";
        default:
          return language.ToLowerInvariant().Replace("-", string.Empty);
      }
    }

    /// <summary>Gets the taxon/taxa names for the given object.</summary>
    /// <param name="item">The object which taxa are examined.</param>
    /// <param name="descriptor">The property descriptor describing the taxa property.</param>
    /// <returns>A collection of names.</returns>
    private static string[] GetTaxonNames(
      object item,
      TaxonomyPropertyDescriptor descriptor,
      CultureInfo ci = null)
    {
      string taxonomyProvider = descriptor.MetaField.TaxonomyProvider;
      Guid id = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver().ResolveSiteTaxonomyId(descriptor.MetaField.TaxonomyId);
      ITaxonomy taxonomy = TaxonomyManager.GetManager(taxonomyProvider).GetTaxonomy(id);
      if (descriptor.MetaField.IsSingleTaxon)
        return new string[1]
        {
          PublishingUtilities.GetTaxonName((Guid) descriptor.GetValue(item), taxonomy, ci)
        };
      List<Guid> source = new List<Guid>();
      object collection = descriptor.GetValue(item);
      if (collection != null)
        source.AddRange((IEnumerable<Guid>) collection);
      return source.Select<Guid, string>((Func<Guid, string>) (t => PublishingUtilities.GetTaxonName(t, taxonomy, ci))).Where<string>((Func<string, bool>) (taxonName => !string.IsNullOrEmpty(taxonName))).ToArray<string>();
    }

    /// <summary>Gets the name of the taxon.</summary>
    /// <param name="taxonId">The taxon id.</param>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns>The name of the taxon.</returns>
    private static string GetTaxonName(Guid taxonId, ITaxonomy taxonomy, CultureInfo ci = null)
    {
      string taxonName = string.Empty;
      ITaxon taxon = taxonomy.Taxa.Where<ITaxon>((Func<ITaxon, bool>) (tx => tx.Id == taxonId)).FirstOrDefault<ITaxon>();
      if (taxon != null)
        taxonName = taxon.Title.GetStringDefaultFallback(ci);
      return taxonName;
    }

    /// <summary>
    /// Removes the dashesh from guid in dynamic link
    /// valid format: "[images|OpenAccessDataProvider]6f90060b-cf29-46e8-965a-47d40b4e72c0"
    /// </summary>
    /// <param name="dynamicLink">String in valid format.</param>
    /// <returns>Same string but with stripped Guid.</returns>
    private static string SanitizeGuid(string dynamicLink)
    {
      if (string.IsNullOrWhiteSpace(dynamicLink))
        return dynamicLink;
      int num = dynamicLink.IndexOf("]");
      Guid result;
      if (num < 0 || !Guid.TryParse(dynamicLink.Sub(num + 1, dynamicLink.Length - 1), out result))
        return dynamicLink;
      dynamicLink = dynamicLink.Replace(result.ToString(), result.ToString("N"));
      return dynamicLink;
    }
  }
}
