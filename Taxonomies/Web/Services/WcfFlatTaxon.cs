// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.WcfFlatTaxon
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>WCF representation of the flat taxon type</summary>
  [DataContract]
  public class WcfFlatTaxon : IWcfTaxon
  {
    private DictionaryObjectViewModel attributes;

    /// <summary>Gets or sets the pageId of the flat taxon.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the pageId of the taxonomy to which the taxon belongs to.
    /// </summary>
    [DataMember]
    public Guid TaxonomyId { get; set; }

    /// <inheritdoc />
    [DataMember]
    public Guid RootTaxonomyId { get; set; }

    /// <summary>
    /// Gets or sets the name of the taxonomy to which the taxon belongs to.
    /// </summary>
    [DataMember]
    public string TaxonomyName { get; set; }

    /// <summary>Gets or sets the title of the flat taxon.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the name of the flat taxon.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the url name of the flat taxon.</summary>
    [DataMember]
    public string UrlName { get; set; }

    /// <summary>Gets or sets the description of the flat taxon.</summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the number of items marked with this flat taxon.
    /// </summary>
    [DataMember]
    public uint ItemsCount { get; set; }

    /// <summary>Gets or sets the synonyms for this flat taxon.</summary>
    [DataMember]
    public string Synonyms { get; set; }

    /// <summary>Gets or sets the path relative to taxonomy</summary>
    /// <value></value>
    [DataMember]
    public string UrlPath { get; set; }

    /// <summary>Gets languages available for this item.</summary>
    /// <value>The available languages.</value>
    [DataMember]
    public string[] AvailableLanguages { get; set; }

    /// <summary>
    /// Gets taxon attributes collection (can be used to store custom meta-data with the taxonomy)
    /// </summary>
    [DataMember]
    public DictionaryObjectViewModel Attributes
    {
      get
      {
        if (this.attributes == null)
          this.attributes = new DictionaryObjectViewModel();
        return this.attributes;
      }
      set => this.attributes = value;
    }

    /// <summary>Gets or sets the last modified.</summary>
    /// <value>The last modified.</value>
    public DateTime LastModified { get; set; }

    /// <summary>Gets or sets the status text.</summary>
    /// <value>The status text.</value>
    [DataMember]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Status AdditionalStatus { get; set; }
  }
}
