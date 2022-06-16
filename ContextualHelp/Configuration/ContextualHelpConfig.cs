// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContextualHelp.Configuration.ContextualHelpConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.ContextualHelp.Configuration
{
  /// <summary>
  /// Configuration for the <see cref="T:Telerik.Sitefinity.ContextualHelp.ContextualHelpModule" />.
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Configuration.ConfigSection" />
  [DescriptionResource(typeof (ContextualHelpResources), "ContextualHelpConfig")]
  internal class ContextualHelpConfig : ConfigSection
  {
    /// <summary>
    /// Gets or sets a value indicating whether the contextual help is enabled.
    /// </summary>
    /// <value>The contextual help enabled value.</value>
    [ConfigurationProperty("enabled", DefaultValue = true)]
    [ObjectInfo(typeof (ContextualHelpResources), Description = "EnabledDescription", Title = "EnabledTitle")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    private class PropNames
    {
      internal const string Enabled = "enabled";
    }
  }
}
