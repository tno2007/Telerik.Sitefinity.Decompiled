// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.RecycleBinAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// Specifies what type of Recycle Bin related operation has occurred.
  /// </summary>
  public enum RecycleBinAction
  {
    /// <summary>
    /// The default value specifying that no Recycle Bin related operation occurred.
    /// </summary>
    None,
    /// <summary>
    /// Specifies that an item was moved to the Recycle Bin, meaning that it was marked as deleted.
    /// </summary>
    MoveToRecycleBin,
    /// <summary>Specifies that an item was permanently deleted.</summary>
    PermanentDelete,
    /// <summary>
    /// Specifies that an item was restored from the Recycle Bin.
    /// </summary>
    RestoreFromRecycleBin,
  }
}
