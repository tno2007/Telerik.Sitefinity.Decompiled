// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.InlineEditingConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.Configuration
{
  /// <summary>
  /// Represents the configuration section which defines the configurations
  /// for Sitefinity Inline editing.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "InlineEditingConfig", Title = "InlineEditingTitle")]
  public class InlineEditingConfig : ConfigSection
  {
    /// <summary>Gets require js configuration paths.</summary>
    [ConfigurationProperty("requireJsModules")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RequireJsModulesDescription", Title = "RequireJsModulesTitle")]
    public ConfigValueDictionary RequireJsModules => (ConfigValueDictionary) this["requireJsModules"];
  }
}
