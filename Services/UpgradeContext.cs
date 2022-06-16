// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.UpgradeContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>Contains information for the upgrade of the System.</summary>
  internal class UpgradeContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.UpgradeContext" /> class.
    /// </summary>
    public UpgradeContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.UpgradeContext" /> class.
    /// </summary>
    /// <param name="fromBuild">From build.</param>
    /// <param name="toBuild">To build.</param>
    public UpgradeContext(int fromBuild, int toBuild)
    {
      this.UpgradeFromBuild = fromBuild;
      this.UpgradeToBuild = toBuild;
    }

    /// <summary>
    /// Gets or sets the build number the system is upgrading from.
    /// </summary>
    /// <value>The upgrade from build.</value>
    public int UpgradeFromBuild { get; set; }

    /// <summary>
    /// Gets or sets the build number the system is upgrading to.
    /// </summary>
    /// <value>The upgrade to build.</value>
    public int UpgradeToBuild { get; set; }
  }
}
