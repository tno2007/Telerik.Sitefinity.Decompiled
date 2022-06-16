// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Proxy.TaxonomyProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Proxy
{
  internal class TaxonomyProxy : ITaxonomyProxy
  {
    public TaxonomyProxy(Taxonomy taxonomy)
    {
      this.Id = taxonomy.Id;
      this.Name = taxonomy.Name;
      this.Title = (ILstring) new LstringProxy(taxonomy.Title);
      this.Description = (ILstring) new LstringProxy(taxonomy.Description);
      this.TaxonName = (ILstring) new LstringProxy(taxonomy.TaxonName);
      if (taxonomy.RootTaxonomy == null)
        return;
      this.RootTaxonomy = (ITaxonomyProxy) new TaxonomyProxy(taxonomy.RootTaxonomy);
      this.RootTaxonomyId = taxonomy.RootTaxonomyId;
    }

    public string Name { get; set; }

    public ILstring Title { get; set; }

    public ILstring Description { get; set; }

    public ILstring TaxonName { get; set; }

    public Guid? RootTaxonomyId { get; set; }

    public ITaxonomyProxy RootTaxonomy { get; set; }

    public virtual Guid Id { get; set; }
  }
}
