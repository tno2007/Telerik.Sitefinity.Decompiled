// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.InvalidateDynamicModuleCacheHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.LoadBalancing
{
  internal class InvalidateDynamicModuleCacheHandler : ISystemMessageHandler
  {
    public bool CanProcessSystemMessage(SystemMessageBase message) => message.Key == "InvalidateDynamicModuleCacheKey";

    /// <summary>Processes the system message.</summary>
    /// <param name="message">The message.</param>
    public void ProcessSystemMessage(SystemMessageBase message)
    {
      string messageData = message.MessageData;
      SiteMapBase.cache.Flush();
      ModuleBuilderManager.ClearModulesCache();
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      manager.InvalidateFieldPermissionsCheckCache();
      KeyValuePair<string, string> keyValuePair = message.AdditionalInfo.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => x.Key == "Operation"));
      if (keyValuePair.Value == null)
        return;
      string str1 = keyValuePair.Value;
      if (!(str1 == "DynamicModuleDelete") && !(str1 == "DynamicModuleUninstall"))
      {
        if (!(str1 == "DynamicModuleInstall") && !(str1 == "DynamicModuleTypeInstall"))
          return;
        Guid id = Guid.Parse(messageData);
        DynamicModule dynamicModule = ModuleBuilderManager.GetManager().GetDynamicModule(id);
        manager.LoadDynamicModuleGraph(dynamicModule);
        ModuleBuilderModule.RegisterDynamicModule((IDynamicModule) dynamicModule);
        ModuleBuilderModule.LoadModule((IDynamicModule) dynamicModule);
        ModuleInstaller.RaiseModuleInstalled(dynamicModule);
      }
      else
      {
        string str2 = messageData;
        List<string> stringList = JsonConvert.DeserializeObject<List<string>>(message.AdditionalInfo.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => x.Key == "TypesNames")).Value);
        SystemManager.RemoveDynamicModule(str2);
        SystemManager.DataSourceRegistry.UnregisterDataSource(str2);
        ModuleBuilderModule.UnloadModule(str2, stringList);
        ModuleInstallerHelper.UninstallSearch(stringList);
        SiteMapBase.cache.Flush();
      }
    }
  }
}
