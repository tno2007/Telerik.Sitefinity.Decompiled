// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Warmup.Configuration.WarmupPluginElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Warmup.Configuration
{
  /// <summary>Defines the plug-in configuration element</summary>
  internal class WarmupPluginElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Warmup.Configuration.WarmupPluginElement" /> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    public WarmupPluginElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the name of the plug-in configured by this class.
    /// </summary>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the type or alias of the plug-in.</summary>
    [ConfigurationProperty("type", IsRequired = true)]
    [ObjectInfo(typeof (WarmupResources), Description = "PluginTypeDescription", Title = "PluginTypeTitle")]
    public string Type
    {
      get => (string) this["type"];
      set => this["type"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the warmup plug-in is enabled.
    /// </summary>
    /// <value>True if the plug-in is enabled, otherwise - false.</value>
    [ConfigurationProperty("enabled", DefaultValue = true)]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a priority of the warmup plug-in compared to the other plug-ins
    /// </summary>
    /// <value>The priority.</value>
    [ConfigurationProperty("priority", DefaultValue = WarmupPriority.Normal)]
    [ObjectInfo(typeof (WarmupResources), Description = "PluginPriorityDescription", Title = "PluginPriorityTitle")]
    public WarmupPriority Priority
    {
      get => (WarmupPriority) this["priority"];
      set => this["priority"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a collection of user-defined parameters for the warmup plug-in.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    private class PropNames
    {
      internal const string Enabled = "enabled";
      internal const string Priority = "priority";
      internal const string Type = "type";
      internal const string Name = "name";
    }
  }
}
