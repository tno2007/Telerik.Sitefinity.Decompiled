// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.IPermissionsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  public interface IPermissionsConfig
  {
    /// <summary>A collection of defined permission sets.</summary>
    ConfigElementDictionary<string, Permission> Permissions { get; }

    /// <summary>
    /// A collection of customized actions defined per secured object type and specific permission sets.
    /// </summary>
    ConfigElementDictionary<string, CustomPermissionsDisplaySettingsConfig> CustomPermissionsDisplaySettings { get; }
  }
}
