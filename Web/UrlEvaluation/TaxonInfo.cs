// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.TaxonInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Object containing the basic information about a taxon.
  /// </summary>
  [DataContract]
  public class TaxonInfo
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UrlEvaluation.TaxonInfo" /> class.
    /// </summary>
    public TaxonInfo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UrlEvaluation.TaxonInfo" /> class.
    /// </summary>
    /// <param name="taxon">The taxon.</param>
    public TaxonInfo(Taxon taxon) => this.SetupTaxonInfo(taxon);

    /// <summary>Gets or sets the name of the taxon.</summary>
    /// <value>The taxon name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the id of the taxon.</summary>
    /// <value>The taxon id.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }

    private void SetupTaxonInfo(Taxon taxon)
    {
      this.Name = taxon != null ? taxon.Name : throw new ArgumentNullException(nameof (taxon));
      this.Id = taxon.Id;
      this.Title = (string) taxon.Title;
    }
  }
}
