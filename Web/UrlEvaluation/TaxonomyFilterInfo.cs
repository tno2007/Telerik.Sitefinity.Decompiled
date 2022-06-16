// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.TaxonomyFilterInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Object containing the basic information about a taxon objects from a given taxonomy
  /// </summary>
  [DataContract]
  public class TaxonomyFilterInfo
  {
    private List<TaxonInfo> taxons;

    /// <summary>Gets or sets the name of the taxonomy.</summary>
    /// <value>The name of the taxonomy.</value>
    [DataMember]
    public string TaxonomyName { get; set; }

    /// <summary>Gets or sets the name of the single taxon.</summary>
    /// <value>The name of the single taxon.</value>
    [DataMember]
    public string SingleTaxonName { get; set; }

    [DataMember]
    public TaxonBuildOptions TaxonType { get; set; }

    /// <summary>Gets or sets the taxons.</summary>
    /// <value>The taxons.</value>
    [DataMember]
    public List<TaxonInfo> Taxons
    {
      get
      {
        if (this.taxons == null)
          this.taxons = new List<TaxonInfo>();
        return this.taxons;
      }
      set => this.taxons = value;
    }
  }
}
