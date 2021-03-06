// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigSiteContextRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Defines a code block where configurations marked as having values that can be specific for different sites will be saved for all sites.
  /// </summary>
  internal class ConfigSiteContextRegion : IDisposable
  {
    private ConfigSiteContext prevMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.ConfigSiteContextRegion" /> class.
    /// </summary>
    /// <param name="siteId">The id of the site for which config marked as site specific will be persisted.</param>
    public ConfigSiteContextRegion(Guid siteId)
    {
      ConfigSiteContext configSiteContext = new ConfigSiteContext();
      configSiteContext.SiteId = siteId;
      this.prevMode = Config.SiteContext;
      Config.SiteContext = configSiteContext;
    }

    void IDisposable.Dispose() => Config.SiteContext = this.prevMode;
  }
}
