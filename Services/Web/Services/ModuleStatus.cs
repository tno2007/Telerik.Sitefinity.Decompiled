// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Web.Services.ModuleStatus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.Web.Services
{
  /// <summary>Defines the status of the module.</summary>
  internal enum ModuleStatus
  {
    /// <summary>The module is not installed and can't be run</summary>
    NotInstalled,
    /// <summary>The module is installed but will not run</summary>
    Inactive,
    /// <summary>The module is installed and will operate</summary>
    Active,
    /// <summary>The module has failed to install/activate</summary>
    Failed,
  }
}
