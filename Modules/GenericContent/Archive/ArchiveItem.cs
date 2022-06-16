// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Archive.ArchiveItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.GenericContent.Archive
{
  /// <summary>
  /// Represents a content item that will be displayed at ArchiveControl.
  /// </summary>
  public class ArchiveItem
  {
    /// <summary>Gets or sets the date by which items are grouped.</summary>
    /// <value>The date.</value>
    public DateTime Date { get; set; }

    /// <summary>Gets or sets the items count.</summary>
    /// <value>The items count.</value>
    public int ItemsCount { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Archive.ArchiveItem" /> class.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <param name="itemsCount">The items count.</param>
    public ArchiveItem(DateTime date, int itemsCount)
    {
      this.Date = date;
      this.ItemsCount = itemsCount;
    }
  }
}
