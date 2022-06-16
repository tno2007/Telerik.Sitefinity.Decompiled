// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Extensions.TaxonomyPropertyDescriptorExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Extensions
{
  /// <summary>Extension methods over the property descriptor.</summary>
  public static class TaxonomyPropertyDescriptorExtensions
  {
    /// <summary>Gets the taxa text.</summary>
    /// <param name="property">The descriptor that contains information about the property.</param>
    /// <param name="item">The item.</param>
    /// <returns>All taxonomy titles concatenated as string.</returns>
    public static string GetTaxaText(this TaxonomyPropertyDescriptor property, object item)
    {
      object obj = property.GetValue(item);
      if (obj == null)
        return (string) null;
      TaxonomyManager manager = TaxonomyManager.GetManager(property.TaxonomyProvider);
      IList<Guid> taxons = obj as IList<Guid>;
      string title;
      if (taxons != null)
      {
        IOrderedQueryable<Taxon> orderedQueryable = manager.GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (t => taxons.Contains(t.Id))).OrderBy<Taxon, Lstring>((Expression<Func<Taxon, Lstring>>) (t => t.Title));
        StringBuilder stringBuilder = new StringBuilder();
        bool flag = false;
        foreach (Taxon taxon in (IEnumerable<Taxon>) orderedQueryable)
        {
          if (flag)
            stringBuilder.Append(',');
          else
            flag = true;
          stringBuilder.Append((string) taxon.Title);
        }
        title = stringBuilder.ToString();
      }
      else
      {
        Guid id = (Guid) obj;
        title = (string) manager.GetTaxon<Taxon>(id).Title;
      }
      manager.Dispose();
      return title;
    }
  }
}
