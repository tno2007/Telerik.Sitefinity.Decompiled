// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.ControlsConfigManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration
{
  /// <summary>
  /// Represents a wrapper for <see cref="T:Telerik.Sitefinity.Web.Configuration.ControlsConfig" /> configuration section.
  /// </summary>
  public static class ControlsConfigManager
  {
    private static ControlsConfig config = Config.Get<ControlsConfig>();

    /// <summary>
    /// Gets the default resource assembly.
    /// Resource assembly is an assembly that contains all Sitefinity resources such as
    /// embedded templates, images, CSS files and etc.
    /// By default this is Telerik.Sitefinity.Resources.dll.
    /// </summary>
    public static Type ResourcesAssemblyInfo => ControlsConfigManager.config.ResourcesAssemblyInfo;

    /// <summary>
    /// Specifies how view and templates are mapped to controls.
    /// </summary>
    public static ConfigElementDictionary<Type, ViewModeControlSettings> ViewMap => ControlsConfigManager.config.ViewMap;
  }
}
