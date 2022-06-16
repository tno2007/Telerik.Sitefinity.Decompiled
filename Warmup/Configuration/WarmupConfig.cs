// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Warmup.Configuration.WarmupConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Warmup.Plugins;

namespace Telerik.Sitefinity.Warmup.Configuration
{
  /// <summary>
  /// This class contains the configurations for the <see cref="T:Telerik.Sitefinity.Warmup.WarmupModule" />.
  /// </summary>
  [ObjectInfo(typeof (WarmupResources), Description = "WarmupConfigDescription", Title = "WarmupConfigTitle")]
  internal class WarmupConfig : ConfigSection
  {
    /// <summary>
    /// The SiteMap <see cref="T:Telerik.Sitefinity.Warmup.Configuration.WarmupPluginElement" /> name.
    /// </summary>
    public const string SiteMapPluginName = "DefaultSiteMap";

    /// <summary>Gets the plug-ins.</summary>
    /// <value>The plug-ins.</value>
    [ConfigurationProperty("plugins")]
    [ObjectInfo(typeof (WarmupResources), Description = "PluginsDescription", Title = "PluginsTitle")]
    public virtual ConfigElementDictionary<string, WarmupPluginElement> Plugins => (ConfigElementDictionary<string, WarmupPluginElement>) this["plugins"];

    /// <summary>Gets list of user agents to warmup with.</summary>
    [ConfigurationProperty("userAgents")]
    [ObjectInfo(typeof (WarmupResources), Description = "UserAgentsDescription", Title = "UserAgentsTitle")]
    public virtual ConfigElementList<UserAgentElement> UserAgents => (ConfigElementList<UserAgentElement>) this["userAgents"];

    /// <summary>Gets or sets the request timeout in milliseconds.</summary>
    /// <value>The request timeout.</value>
    [ConfigurationProperty("requestTimeout", DefaultValue = 30000)]
    [ObjectInfo(typeof (WarmupResources), Description = "RequestTimeoutDescription", Title = "RequestTimeoutTitle")]
    public int RequestTimeout
    {
      get => (int) this["requestTimeout"];
      set => this["requestTimeout"] = (object) value;
    }

    /// <summary>Gets or sets the maximum number requests on startup.</summary>
    /// <value>The maximum number requests on startup.</value>
    [ConfigurationProperty("maxRequestsOnStartup", DefaultValue = 20)]
    [ObjectInfo(typeof (WarmupResources), Description = "MaxRequestsOnStartupDescription", Title = "MaxRequestsOnStartupTitle")]
    public int MaxRequestsOnStartup
    {
      get => (int) this["maxRequestsOnStartup"];
      set => this["maxRequestsOnStartup"] = (object) value;
    }

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized() => this.Plugins.Add(new WarmupPluginElement((ConfigElement) this.Plugins)
    {
      Enabled = true,
      Type = typeof (SitemapPlugin).FullName,
      Name = "DefaultSiteMap",
      Priority = WarmupPriority.High,
      Parameters = new NameValueCollection()
      {
        {
          "maxPagesOnStartupPerSite",
          10.ToString()
        },
        {
          "maxPagesAfterInitializationPerSite",
          50.ToString()
        }
      }
    });

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropNames
    {
      internal const string Plugins = "plugins";
      internal const string UserAgents = "userAgents";
      internal const string RequestTimeout = "requestTimeout";
      internal const string MaxRequestsOnStartup = "maxRequestsOnStartup";
    }
  }
}
