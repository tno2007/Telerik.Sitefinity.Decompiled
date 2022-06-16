// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.FilterItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Filters
{
  /// <summary>
  /// Represents a class that describes the operation interface
  /// </summary>
  [DataContract]
  internal class FilterItem
  {
    /// <summary>Gets or sets the name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the title</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the title</summary>
    [DataMember]
    public FilterParameters Parameters { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// Filters will be grouped/separated by categories.
    /// </summary>
    [DataMember]
    public string Category { get; set; }

    /// <summary>Gets or sets the items count applied by the filter</summary>
    [DataMember]
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the filter is dynamic.
    /// It will change dynamically based on the content items and should be rendered in specific way.
    /// </summary>
    [DataMember]
    public bool IsDynamicFilter { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this filter is also a status.
    /// </summary>
    public bool IsStatus { get; set; }

    /// <summary>
    /// Gets or sets the ordinal of the filter.
    /// Filters will be ordered by ordinal.
    /// </summary>
    public int Ordinal { get; set; }
  }
}
