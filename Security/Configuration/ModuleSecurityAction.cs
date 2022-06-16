// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.ModuleSecurityAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security.Configuration
{
  internal class ModuleSecurityAction : SecurityAction, IModuleDependentItem
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ModuleSecurityAction(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <param name="value">Optionally explicitly set the configuration numeric value of this security action. Use with caution!</param>
    public ModuleSecurityAction(ConfigElement parent, int value)
      : base(parent, value)
    {
    }

    /// <summary>
    /// Gets or sets the module name the security action depends on.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("moduleName")]
    [Browsable(false)]
    public string ModuleName
    {
      get => (string) this["moduleName"];
      set => this["moduleName"] = (object) value;
    }
  }
}
