// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SecuredModuleBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Represents base class for Sitefinity modules.</summary>
  public abstract class SecuredModuleBase : ModuleBase, ISecuredModule
  {
    /// <summary>Gets the security roots for this module.</summary>
    /// <param name="getContextRootsOnly">If set to true, returns only the security roots relevant explicitly to the current site (not including system providers).</param>
    /// <returns>The list of security roots for the module</returns>
    public virtual IList<SecurityRoot> GetSecurityRoots(bool getContextRootsOnly = true)
    {
      List<SecurityRoot> securityRoots = new List<SecurityRoot>();
      if (this.Managers != null)
      {
        foreach (Type manager1 in this.Managers)
        {
          IManager manager2 = ManagerBase.GetManager(manager1);
          if (manager2 != null)
          {
            foreach (DataProviderBase dataProviderBase in !getContextRootsOnly ? manager2.GetAllProviders() : manager2.GetContextProviders())
            {
              if (dataProviderBase.GetSecurityRoot(true) is SecurityRoot securityRoot)
              {
                securityRoot.DataProviderName = dataProviderBase.Name;
                securityRoot.ManagerType = manager2.GetType();
                securityRoots.Add(securityRoot);
              }
            }
          }
        }
      }
      return (IList<SecurityRoot>) securityRoots;
    }

    /// <summary>Get all security roots for the a module</summary>
    /// <param name="moduleName">Name of the module</param>
    /// <returns>Sequence of all security roots</returns>
    public static IEnumerable<ISecuredObject> GetSecurityRoots(
      string moduleName)
    {
      if (SystemManager.GetModule(moduleName) is ModuleBase module)
      {
        Type[] typeArray = module.Managers;
        for (int index = 0; index < typeArray.Length; ++index)
        {
          foreach (ISecuredObject securityRoot in SecuredModuleBase.GetSecurityRoots(typeArray[index]))
            yield return securityRoot;
        }
        typeArray = (Type[]) null;
      }
    }

    /// <summary>Get all security roots for a manager</summary>
    /// <param name="managerType">Type of the manager</param>
    /// <returns>Sequence of all security roots</returns>
    public static IEnumerable<ISecuredObject> GetSecurityRoots(
      Type managerType)
    {
      foreach (DataProviderBase contextProvider in ManagerBase.GetManager(managerType).GetContextProviders())
        yield return ManagerBase.GetManager(managerType, contextProvider.Name).GetSecurityRoot();
    }
  }
}
