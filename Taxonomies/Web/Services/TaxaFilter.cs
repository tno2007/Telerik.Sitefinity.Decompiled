// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.TaxaFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>
  /// Represents request data that is required to retrieve taxons.
  /// </summary>
  [DataContract]
  public class TaxaFilter
  {
    /// <summary>Gets or sets the provider.</summary>
    /// <value>
    /// Name of the provider to be used when retrieving the taxons.
    /// </value>
    [DataMember]
    public string Provider { get; set; }

    /// <summary>Gets or sets the sort expression.</summary>
    /// <value>
    /// Sort expression used to order the taxons in the collection.
    /// </value>
    [DataMember]
    public string SortExpression { get; set; }

    /// <summary>Gets or sets the skip.</summary>
    /// <value>
    /// Number of items to skip before starting to take results in the result set.
    /// </value>
    [DataMember]
    public int Skip { get; set; }

    /// <summary>Gets or sets the take.</summary>
    /// <value>Number of items to take in the result set.</value>
    [DataMember]
    public int Take { get; set; }

    /// <summary>Gets or sets the filter.</summary>
    /// <value>
    /// Filter expression used to filter the items that ought to be part of the result set.
    /// </value>
    [DataMember]
    public string FilterExpression { get; set; }

    /// <summary>Gets or sets the type of the item.</summary>
    /// <value>Type of the item.</value>
    [DataMember]
    public string ItemType { get; set; }

    /// <summary>Gets or sets the mode.</summary>
    /// <value>The mode.</value>
    [DataMember]
    public string Mode { get; set; }

    /// <summary>Gets or sets the site context mode.</summary>
    /// <value>The mode in which the taxa should be obtained (ex. current site context, no site context, all sites context).</value>
    [DataMember]
    public string SiteContextMode { get; set; }
  }
}
