// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.NeeedsManagerTypeEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data
{
  /// <summary>Information about the NeedsManagerType event</summary>
  public class NeeedsManagerTypeEventArgs : EventArgs
  {
    /// <summary>
    /// Initializes the event arguments with item type and manager type
    /// </summary>
    /// <param name="itemType">Type of the content item that needs a manager. Read-only.</param>
    /// <param name="managerType">Type of the manager. You have to set this when handling the NeedsManagerType event</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="itemType" /> is null.</exception>
    public NeeedsManagerTypeEventArgs(Type itemType, Type managerType)
    {
      this.ItemType = !(itemType == (Type) null) ? itemType : throw new ArgumentNullException(nameof (itemType));
      this.ManagerType = managerType;
    }

    /// <summary>
    /// Type of the content item that needs a manager. Read-only.
    /// </summary>
    public Type ItemType { get; private set; }

    /// <summary>
    /// Type of the manager. You have to set this when handling the NeedsManagerType event
    /// </summary>
    public Type ManagerType { get; set; }
  }
}
