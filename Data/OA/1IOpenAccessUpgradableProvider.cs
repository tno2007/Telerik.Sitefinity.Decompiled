// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.IOpenAccessUpgradableProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>
  /// Implements additional upgrading mechanism to IOpenAccessMetadataProvider, which is handled by OpenAccessConnection instance.
  /// </summary>
  public interface IOpenAccessUpgradableProvider
  {
    /// <summary>
    /// Gets the current schema version number of the provider.
    /// </summary>
    /// <value>The current schema version number.</value>
    int CurrentSchemaVersionNumber { get; }

    /// <summary>
    /// Called on upgrading. Before the database scheme is upgraded.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="upgradingFromSchemaVersionNumber">The upgrading from schema version number.</param>
    void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber);

    /// <summary>
    /// Called when upgraded. After the database is upgraded.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="context">The context.</param>
    /// <param name="upgradedFromSchemaVersionNumber">The upgraded from schema version number.</param>
    void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber);
  }
}
