// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Determines the source of the loaded element, e.g. Database means that the element is persisted in the database
  /// </summary>
  public enum ConfigSource
  {
    /// <summary>Not persisted yet - inherited from the parent</summary>
    NotSet,
    /// <summary>Default in-memory</summary>
    Default,
    /// <summary>File system</summary>
    FileSystem,
    /// <summary>Database source</summary>
    Database,
    /// <summary>Imported by import method - not persisted yet</summary>
    Import,
  }
}
