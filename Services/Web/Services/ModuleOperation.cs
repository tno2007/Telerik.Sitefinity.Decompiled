// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Web.Services.ModuleOperation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.Web.Services
{
  /// <summary>Contains the available manipulations for modules</summary>
  internal enum ModuleOperation
  {
    /// <summary>
    /// Registers the module in the system and performs the needed configurations
    /// </summary>
    Install,
    /// <summary>
    /// The content and settings will be deleted. The module can be installed later, but content and settings will be lost.
    /// </summary>
    Uninstall,
    /// <summary>
    /// The module will be made operational and it's code will be executed.
    /// </summary>
    Activate,
    /// <summary>
    /// The content and settings will be kept, but hidden. The module can be activated later.
    /// </summary>
    Deactivate,
    /// <summary>
    /// Delete this module from the list of uninstalled modules
    /// </summary>
    Delete,
    /// <summary>Updates the module information</summary>
    Edit,
  }
}
