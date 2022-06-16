// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.FileSystemChangeType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.LoadBalancing
{
  [Flags]
  public enum FileSystemChangeType
  {
    /// <summary>A new file system item was created.</summary>
    Created = 1,
    /// <summary>A file system item was changed.</summary>
    Changed = 2,
    /// <summary>A file system item was deleted.</summary>
    Removed = 4,
    /// <summary>
    /// A file system item was either created or overwritten.
    /// 
    /// This is used to indicate operations like copying and moving, during which either a new item may be created,
    /// or an old one overwritten and the infrastructure cannot or does not want to spend time to determine exactly
    /// which of these has happend.
    /// </summary>
    CreatedOrChanged = Changed | Created, // 0x00000003
  }
}
