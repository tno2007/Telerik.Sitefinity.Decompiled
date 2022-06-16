// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>
  /// WCF representation of the <see cref="T:Telerik.Sitefinity.Taxonomies.Model.Taxonomy" /> type.
  /// </summary>
  [DataContract]
  public class WcfTaxonomy
  {
    /// <summary>Gets or sets the pageId of the taxonomy.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title of the taxonomy.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the name of the taxonomy.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the url of the edit form for the taxonomy.
    /// </summary>
    [DataMember]
    public string EditUrl { get; set; }

    /// <summary>Gets or sets the description of the taxonomy.</summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the user friendly type name of the taxonomy.
    /// </summary>
    [DataMember]
    public string UserFriendlyType { get; set; }

    /// <summary>Gets or sets the type of the taxonomy.</summary>
    [DataMember]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the css class what should be applied to the
    /// element representing the taxonomy.
    /// </summary>
    [DataMember]
    public string CssClass { get; set; }

    /// <summary>
    /// Gets or sets the titles of the first two taxons of the taxonomy.
    /// </summary>
    [DataMember]
    public string[] FirstTwoTaxons { get; set; }

    /// <summary>
    /// Gets or sets the total number of taxons in the taxonomy.
    /// </summary>
    [DataMember]
    public int TotalTaxaCount { get; set; }

    /// <summary>Gets or sets the name of the single taxon.</summary>
    /// <remarks>
    /// If the name of the taxonomy is "Categories", the name of the single item
    /// would be "Category".
    /// </remarks>
    [DataMember]
    public string SingleItemName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this taxonomy is built in.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this taxonomy is built in; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsBuiltIn { get; set; }

    /// <summary>Gets or sets the full name of the taxonomy type.</summary>
    [DataMember]
    public string FullTypeName { get; set; }

    /// <summary>Gets languages available for this item.</summary>
    /// <value>The available languages.</value>
    [DataMember]
    public string[] AvailableLanguages { get; set; }

    /// <summary>Gets or sets the full name of the taxonomy type.</summary>
    [DataMember]
    public string FullName { get; set; }

    /// <summary>
    /// Gets or sets the count of the sites which share this taxonomy.
    /// </summary>
    /// <value>The count of the sites.</value>
    [DataMember]
    public int SharedSitesCount { get; set; }

    /// <summary>
    /// Gets or sets id of the taxonomy used in the current site.
    /// </summary>
    /// <value>The id of the taxonomy used in the current site.</value>
    [DataMember]
    public Guid CurrentSiteTaxonomyId { get; set; }

    /// <summary>Gets or sets the additional status.</summary>
    [DataMember]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Status AdditionalStatus { get; set; }
  }
}
