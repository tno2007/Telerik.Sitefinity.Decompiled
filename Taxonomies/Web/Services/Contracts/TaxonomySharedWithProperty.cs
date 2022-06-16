// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.Contracts.TaxonomySharedWithProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Taxonomies.Web.Services.Contracts
{
  /// <summary>
  /// A calculated property for retrieving the shared sites for given taxonomy.
  /// </summary>
  internal class TaxonomySharedWithProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (int);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null)
        return (IDictionary<object, object>) values;
      List<Taxonomy> list = items.Cast<Taxonomy>().ToList<Taxonomy>();
      TaxonomyManager manager1 = manager as TaxonomyManager;
      foreach (Taxonomy key in list)
      {
        Taxonomy taxonomy1 = key;
        if (!manager1.ShouldIgnoreSiteContext())
        {
          Taxonomy taxonomy2 = key.RootTaxonomy != null ? key.RootTaxonomy : key;
          taxonomy1 = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(manager1).ResolveSiteTaxonomy<Taxonomy>(taxonomy2);
        }
        int num = 1;
        if (SystemManager.CurrentContext.IsMultisiteMode)
          num = manager1.GetRelatedSites((ITaxonomy) taxonomy1).Count<ISite>();
        values.Add((object) key, (object) num);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
