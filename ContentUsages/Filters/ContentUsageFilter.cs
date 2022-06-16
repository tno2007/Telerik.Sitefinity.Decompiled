// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentUsages.Filters.ContentUsageFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.ContentUsages.Model;

namespace Telerik.Sitefinity.ContentUsages.Filters
{
  /// <summary>Content usage filter</summary>
  internal class ContentUsageFilter
  {
    /// <summary>Gets or sets ItemId</summary>
    public Guid ItemId { get; set; }

    /// <summary>Gets or sets ItemType</summary>
    public string ItemType { get; set; }

    /// <summary>Gets or sets ItemProvider</summary>
    public string ItemProvider { get; set; }

    /// <summary>Gets or sets Culture</summary>
    public CultureInfo Culture { get; set; }

    /// <summary>Gets or sets the ItemTypeFilter</summary>
    public ItemTypeFilter ItemTypeFilter { get; set; }

    /// <summary>Gets or sets the Skip</summary>
    public int Skip { get; set; }

    /// <summary>Gets or sets the Take</summary>
    public int Take { get; set; }
  }
}
