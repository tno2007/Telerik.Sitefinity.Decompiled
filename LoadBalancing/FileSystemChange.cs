// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.FileSystemChange
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>Represents a change on the file system.</summary>
  public class FileSystemChange
  {
    /// <summary>The type of the change.</summary>
    public FileSystemChangeType Type { get; set; }

    /// <summary>The type of the file system item changed.</summary>
    public FileSystemItemType ItemType { get; set; }

    /// <summary>The file system path related to the change.</summary>
    public string Path { get; set; }

    /// <summary>The time when the change occured.</summary>
    public DateTime Timestamp { get; set; }

    /// <summary>Creates a new instance.</summary>
    public FileSystemChange()
    {
    }

    /// <summary>
    /// Creates a new instance with its corresponding properties set to the values of the parameters.
    /// </summary>
    public FileSystemChange(
      FileSystemChangeType type,
      FileSystemItemType itemType,
      string path,
      DateTime? timestamp = null)
    {
      this.Type = type;
      this.ItemType = itemType;
      this.Path = path;
      this.Timestamp = timestamp ?? DateTime.UtcNow;
    }
  }
}
