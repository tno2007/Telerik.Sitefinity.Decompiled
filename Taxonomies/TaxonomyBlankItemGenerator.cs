// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyWebServiceBlankItemGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.Services;

namespace Telerik.Sitefinity.Taxonomies
{
  public class TaxonomyWebServiceBlankItemGenerator : IWebServiceBlankItemGenerator
  {
    public object CreateBlankItem(Type taxonOrTaxonomyType, string providerName)
    {
      int num = typeof (Taxon).IsAssignableFrom(taxonOrTaxonomyType) ? 1 : 0;
      bool flag = typeof (Taxonomy).IsAssignableFrom(taxonOrTaxonomyType);
      if (num != 0)
        return (object) this.CreateBlankTaxon(taxonOrTaxonomyType);
      if (flag)
        return (object) this.CreateBlankTaxonomy(taxonOrTaxonomyType);
      throw new InvalidOperationException("TaxonomyWebServiceBlankItemGenerator supports only taxa and taxonomies");
    }

    public IWcfTaxon CreateBlankTaxon(Type taxonType)
    {
      if (typeof (FlatTaxon).IsAssignableFrom(taxonType))
        return (IWcfTaxon) new WcfFlatTaxon();
      if (typeof (HierarchicalTaxon).IsAssignableFrom(taxonType))
        return (IWcfTaxon) new WcfHierarchicalTaxon();
      throw new NotImplementedException();
    }

    public WcfTaxonomy CreateBlankTaxonomy(Type taxonomyType) => new WcfTaxonomy();
  }
}
