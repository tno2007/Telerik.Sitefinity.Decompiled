// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.AppStatus.SchemaMigrationType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.AppStatus
{
  internal enum SchemaMigrationType
  {
    /// <summary>
    /// The generated script contains schema artifacts without influencing the existing schema.
    /// </summary>
    Trivial,
    /// <summary>
    /// The generated script contains schema artifacts that extend the existing schema.
    /// </summary>
    Extending,
    /// <summary>
    /// The generated script contains schema artifacts with structural changes.
    /// </summary>
    Complex,
  }
}
