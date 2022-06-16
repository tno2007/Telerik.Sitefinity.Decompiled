// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.RestrictionLevel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Defines the level of rights for the configuration access.
  /// </summary>
  [Flags]
  public enum RestrictionLevel
  {
    /// <summary>Read and modify mode.</summary>
    [Description("Read and modify mode")] Default = 0,
    /// <summary>Read only mode.</summary>
    [Description("Read only mode")] ReadOnlyConfigFile = 1,
  }
}
