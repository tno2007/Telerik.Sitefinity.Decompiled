// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.FileSystemModeRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Defines a code block where configurations will be saved in the file system
  /// </summary>
  public class FileSystemModeRegion : IDisposable
  {
    private readonly bool prevMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.FileSystemModeRegion" /> class.
    /// </summary>
    public FileSystemModeRegion()
    {
      this.prevMode = Config.FileSystemMode;
      Config.FileSystemMode = true;
    }

    void IDisposable.Dispose() => Config.FileSystemMode = this.prevMode;
  }
}
