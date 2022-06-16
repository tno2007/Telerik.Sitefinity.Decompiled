// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Utils.ModuleResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Utils
{
  /// <summary>
  /// Helper class to resolve Sitefinity modules <see cref="T:Telerik.Sitefinity.Services.IModule" />
  /// </summary>
  internal class ModuleResolver
  {
    /// <summary>
    /// Resolves Sitefinity module <see cref="T:Telerik.Sitefinity.Services.IModule" /> by given Sitefinity content type full CLR name or returns null if the type is not supported.
    /// </summary>
    /// <param name="typeName">CLR full type name</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Services.IModule" /> or null.</returns>
    public IModule ResolveModule(string typeName)
    {
      IManager mappedManager = ManagerBase.GetMappedManager(typeName);
      IModuleManager moduleManager = (IModuleManager) null;
      if (mappedManager != null)
        moduleManager = mappedManager as IModuleManager;
      if (moduleManager == null)
        return (IModule) null;
      if (!(moduleManager is DynamicModuleManager))
        return SystemManager.GetModule(moduleManager.ModuleName);
      DynamicModuleManager dynamicModuleManager = moduleManager as DynamicModuleManager;
      DynamicModuleType dynamicModuleType = dynamicModuleManager.ModuleBuilderMgr.GetDynamicModuleType(typeName);
      if (dynamicModuleType == null)
        return (IModule) null;
      Guid parentModuleId = dynamicModuleType.ParentModuleId;
      return SystemManager.GetModule(dynamicModuleManager.ModuleBuilderMgr.GetDynamicModule(parentModuleId).Name);
    }
  }
}
