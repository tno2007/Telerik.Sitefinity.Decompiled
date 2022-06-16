// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.DynamicModuleNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.DynamicModules.Web
{
  internal class DynamicModuleNodeFilter : ISitemapNodeFilter
  {
    private static readonly Dictionary<string, string[]> dicSecuritySetsWithActions = new Dictionary<string, string[]>()
    {
      {
        "General",
        new string[4]
        {
          "Create",
          "Modify",
          "Delete",
          "ChangePermissions"
        }
      },
      {
        "SitemapGeneration",
        new string[1]{ "ViewBackendLink" }
      }
    };

    public bool IsNodeAccessPrevented(PageSiteNode pageNode)
    {
      if (!pageNode.IsBackend)
        return false;
      if (!((IEnumerable<string>) pageNode.Attributes.AllKeys).Contains<string>("IsDynamicModulePage"))
        return this.IsFilterEnabled("ModuleBuilder") && (pageNode.Id == ModuleBuilderModule.moduleBuilderNodeId || pageNode.IsModulePage("ModuleBuilder")) && !ClaimsManager.IsUnrestricted();
      bool flag = false;
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.PageId == pageNode.Id)).SingleOrDefault<DynamicModuleType>();
      if (dynamicModuleType != null)
        flag = !DynamicModuleNodeFilter.IsNodeAccessible(dynamicModuleType, dynamicModuleType.ModuleName, manager);
      return flag;
    }

    private static bool IsNodeAccessible(
      DynamicModuleType dynamicModuleType,
      string moduleName,
      ModuleBuilderManager moduleBuilderManager)
    {
      foreach (string dynamicDataProviderName in DynamicModuleManager.GetManager().GetContextProviders(moduleName).Select<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Name)))
      {
        ISecuredObject securedObject = DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) moduleBuilderManager, (ISecuredObject) dynamicModuleType, dynamicDataProviderName);
        foreach (KeyValuePair<string, string[]> securitySetsWithAction in DynamicModuleNodeFilter.dicSecuritySetsWithActions)
        {
          foreach (string str in securitySetsWithAction.Value)
          {
            if (securedObject.IsGranted(securitySetsWithAction.Key, str))
              return true;
          }
        }
      }
      return false;
    }
  }
}
