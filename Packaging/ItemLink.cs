// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.ItemLink
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Packaging
{
  /// <summary>
  /// Represents a link for an item from a package that contains type and id.
  /// </summary>
  internal class ItemLink
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.ItemLink" /> class.
    /// </summary>
    /// <param name="itemType">The item type.</param>
    /// <param name="itemId">The item id.</param>
    public ItemLink(string itemType, Guid itemId)
    {
      this.ItemType = itemType;
      this.ItemId = itemId;
    }

    /// <summary>Gets or sets item type.</summary>
    public string ItemType { get; set; }

    /// <summary>Gets or sets item id.</summary>
    public Guid ItemId { get; set; }
  }
}
