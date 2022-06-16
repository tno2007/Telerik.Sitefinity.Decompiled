// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.TaxaDefinitionsHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  internal static class TaxaDefinitionsHelper
  {
    public static void GetTaxonTypeDlgDefNameAndViewNameFromTaxonomy(
      ITaxonomy taxonomy,
      out Type taxonType,
      out string defName,
      out string viewName,
      bool insert)
    {
      if (typeof (HierarchicalTaxonomy).IsAssignableFrom(taxonomy.GetType()))
      {
        taxonType = typeof (HierarchicalTaxon);
        defName = "HierarchicalTaxonBackend";
        viewName = insert ? "HierarchicalTaxonBackendInsert" : "HierarchicalTaxonBackendEdit";
      }
      else if (typeof (FlatTaxonomy).IsAssignableFrom(taxonomy.GetType()))
      {
        taxonType = typeof (FlatTaxon);
        defName = "FlatTaxonBackend";
        viewName = insert ? "FlatTaxonBackendInsert" : "FlatTaxonBackendEdit";
      }
      else
      {
        if (typeof (NetworkTaxonomy).IsAssignableFrom(taxonomy.GetType()))
        {
          taxonType = typeof (NetworkTaxon);
          throw new NotImplementedException("Network taxonomy form is not implemented");
        }
        if (typeof (FacetTaxonomy).IsAssignableFrom(taxonomy.GetType()))
        {
          taxonType = typeof (FacetTaxon);
          throw new NotImplementedException("Facet taxonomy form is not implemented");
        }
        throw new NotImplementedException("The only supported taxonomies are: Hierarchical, Flat, Network and Facet");
      }
    }
  }
}
