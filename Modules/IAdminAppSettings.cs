// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.IAdminAppSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules
{
  /// <summary>Provides access to admin app related settings</summary>
  internal interface IAdminAppSettings
  {
    /// <summary>Gets if the admin app is enabled for current user.</summary>
    /// <returns>Whether the adminApp is enabled</returns>
    bool GetIsEnabledForCurrentUser();

    /// <summary>
    /// Gets whether module name is whitelisted in admin app config
    /// </summary>
    /// <param name="moduleName">Module name</param>
    /// <returns>Whether the module name is whitelisted</returns>
    bool IsWhiteListedModule(string moduleName);
  }
}
