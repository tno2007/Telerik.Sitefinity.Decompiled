// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonStatistic
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>
  /// WCF type that provides the statistical information
  /// about a single taxon.
  /// </summary>
  [DataContract]
  public class WcfTaxonStatistic
  {
    /// <summary>
    /// Gets or sets the name of the taxon for which the
    /// statistical information is kept.
    /// </summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the item for which the statistical
    /// information is kept.
    /// </summary>
    [DataMember]
    public string ItemType { get; set; }

    /// <summary>Gets or sets the item provider.</summary>
    /// <value>The item provider.</value>
    [DataMember]
    public string ItemProvider { get; set; }

    /// <summary>
    /// Gets or sets the number of items of type ItemType that
    /// are marked with the taxon of a specified name.
    /// </summary>
    [DataMember]
    public uint Count { get; set; }
  }
}
