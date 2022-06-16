// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Data.LoadContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration.Data
{
  internal class LoadContext
  {
    public LoadContext()
    {
    }

    public LoadContext(bool checkForUpgrade, ConfigSource source)
      : this()
    {
      this.CheckForUpgrade = checkForUpgrade;
      this.Source = source;
    }

    public bool CheckForUpgrade { get; set; }

    public ConfigSource Source { get; set; }

    public ConfigImportContext ImportContext { get; set; }

    public ConfigUpgradeContext UpgradeContext { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the section needs to be saved at the end of a LoadSection operation.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [save needed]; otherwise, <c>false</c>.
    /// </value>
    public bool SaveNeeded { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [load lazy elements].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [load lazy elements]; otherwise, <c>false</c>.
    /// </value>
    public bool LoadLazyElements { get; set; }

    public Guid SiteId { get; set; }
  }
}
