// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Structure.TaxonomyExportObject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Packaging.Structure
{
  internal class TaxonomyExportObject
  {
    public TaxonomyExportObject()
    {
    }

    public TaxonomyExportObject(Taxonomy taxonomy)
    {
      this.Id = taxonomy.Id;
      this.Name = taxonomy.Name;
      this.Type = taxonomy.GetType().FullName;
      this.LastModified = taxonomy.LastModified;
      this.Title = TaxonomyExportObject.GetLstringValues(taxonomy.Title);
      this.Description = TaxonomyExportObject.GetLstringValues(taxonomy.Description);
      this.TaxonName = TaxonomyExportObject.GetLstringValues(taxonomy.TaxonName);
    }

    public static IEnumerable<KeyValuePair<string, string>> GetLstringValues(
      Lstring lstring)
    {
      CultureInfo[] availableLanguages = lstring.GetAvailableLanguages();
      List<KeyValuePair<string, string>> lstringValues = new List<KeyValuePair<string, string>>();
      foreach (CultureInfo culture in availableLanguages)
      {
        string str;
        lstring.TryGetValue(out str, culture);
        if (str != null)
          lstringValues.Add(new KeyValuePair<string, string>(culture.Name, str));
      }
      return (IEnumerable<KeyValuePair<string, string>>) lstringValues;
    }

    public static void CopyLstringValuesToLstringObject(
      IEnumerable<KeyValuePair<string, string>> source,
      Lstring target)
    {
      IEnumerable<string> source1 = source.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (kv => kv.Key)).Where<string>((Func<string, bool>) (c => string.IsNullOrEmpty(c) || SystemManager.CurrentContext.AppSettings.AllLanguages.Any<KeyValuePair<int, CultureInfo>>((Func<KeyValuePair<int, CultureInfo>, bool>) (l => l.Value.Name.Equals(c, StringComparison.Ordinal)))));
      bool flag = source1.Count<string>() == 1 && CultureInfo.InvariantCulture.Name.Equals(source1.First<string>());
      foreach (string str in source1)
      {
        string language = str;
        CultureInfo cultureInfo = CultureInfo.GetCultureInfo(language);
        if (!string.IsNullOrEmpty(language) | flag)
          target.SetString(cultureInfo, source.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (kv => kv.Key.Equals(language))).Value);
      }
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public DateTime LastModified { get; set; }

    public IEnumerable<KeyValuePair<string, string>> Title { get; set; }

    public IEnumerable<KeyValuePair<string, string>> Description { get; set; }

    public IEnumerable<KeyValuePair<string, string>> TaxonName { get; set; }
  }
}
