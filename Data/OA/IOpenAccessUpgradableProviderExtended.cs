// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.IOpenAccessUpgradableProviderExtended
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.OpenAccess;

namespace Telerik.Sitefinity.Data.OA
{
  internal interface IOpenAccessUpgradableProviderExtended : IOpenAccessUpgradableProvider
  {
    /// <summary>
    /// Called when after database schema upgrade.
    /// Can be used when need to execute custom DDL script
    /// </summary>
    /// <param name="schemaHandler">The schema handler.</param>
    /// <param name="oaConnection">The open access connection.</param>
    /// <param name="upgradedFromSchemaVersionNumber">The upgraded from schema version number.</param>
    void OnSchemaUpgrade(
      OpenAccessConnection oaConnection,
      ISchemaHandler schemaHandler,
      int upgradedFromSchemaVersionNumber);
  }
}
