// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleProviderAssociation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Modules.Selectors
{
  /// <summary>Associates a listed module link to a set of providers</summary>
  [DataContract]
  public class ModuleProviderAssociation
  {
    internal ModuleProvider[] moduleProviders;

    /// <summary>Client ID of the module link</summary>
    [DataMember]
    public string LinkClientID { get; set; }

    /// <summary>Title of the module</summary>
    [DataMember]
    public string ModuleTitle { get; set; }

    /// <summary>Gets or sets the name of the module.</summary>
    [DataMember]
    public string ModuleName { get; set; }

    /// <summary>Indicates whether this module is selected</summary>
    [DataMember]
    public bool IsSelectedModule { get; set; }

    /// <summary>
    /// Gets or sets the name of the module manager type.
    /// (optional: if Module name is not provided the permissions can be loaded by manager type).
    /// </summary>
    [DataMember]
    public string ModuleManagerTypeName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is dynamic module.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is dynamic module; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsDynamicModule { get; set; }

    /// <summary>
    /// Gets or sets a specific secured object id to load.
    /// (optional: if not specified, the secured object id is the security root of the providers given
    /// If specified, SecuredObjectTypeName must be also given).
    /// </summary>
    [DataMember]
    public string SecuredObjectId { get; set; }

    /// <summary>
    /// Gets or sets the name of the secured object type.
    /// (optional: must be specified if a specific SecuredObjectId is given).
    /// </summary>
    [DataMember]
    public string SecuredObjectTypeName { get; set; }

    /// <summary>An array of data for providers set for this module</summary>
    [DataMember]
    public virtual ModuleProvider[] ModuleProviders
    {
      get
      {
        if (this.moduleProviders == null)
          this.moduleProviders = this.getModuleProviders();
        return this.moduleProviders;
      }
      set => this.moduleProviders = value;
    }

    internal virtual ModuleProvider[] getModuleProviders()
    {
      List<ModuleProvider> moduleProviderList = new List<ModuleProvider>();
      string moduleName = this.ModuleName;
      if (this.ModuleName == "Backend")
      {
        IManager manager = ManagerBase.GetManager(AppPermission.Root.ManagerType, AppPermission.Root.DataProviderName);
        moduleProviderList.Add(new ModuleProvider("Backend", AppPermission.Root.ManagerType.AssemblyQualifiedName, manager.Provider.Title, AppPermission.Root.DataProviderName, AppPermission.Root.Id.ToString(), false, typeof (SecurityRoot).AssemblyQualifiedName));
      }
      else if (!string.IsNullOrWhiteSpace(this.ModuleManagerTypeName))
      {
        IManager manager = ManagerBase.GetManager(this.ModuleManagerTypeName);
        IEnumerable<DataProviderBase> dataProviderBases;
        switch (manager)
        {
          case null:
            goto label_25;
          case DynamicModuleManager _:
            dataProviderBases = ((DynamicModuleManager) manager).GetContextProviders(this.ModuleName);
            break;
          case null:
            dataProviderBases = manager.GetAllProviders();
            break;
          default:
            dataProviderBases = manager.GetContextProviders();
            break;
        }
        foreach (DataProviderBase dataProviderBase in dataProviderBases)
          moduleProviderList.Add(new ModuleProvider(string.Empty, this.ModuleManagerTypeName, dataProviderBase.Title, dataProviderBase.Name, dataProviderBase.SecurityRoot.Id.ToString(), false, dataProviderBase.SecurityRoot.GetType().AssemblyQualifiedName));
      }
      else
      {
        IModule dynamicModule;
        if (!SystemManager.ApplicationModules.TryGetValue(moduleName, out dynamicModule) && !SystemManager.ServiceModules.TryGetValue(moduleName, out dynamicModule))
          dynamicModule = SystemManager.GetDynamicModule(moduleName);
        if (dynamicModule != null && dynamicModule is ISecuredModule)
        {
          foreach (SecurityRoot securityRoot in (IEnumerable<SecurityRoot>) ((ISecuredModule) dynamicModule).GetSecurityRoots())
          {
            if (securityRoot != null)
            {
              IManager manager = ManagerBase.GetManager(securityRoot.ManagerType, securityRoot.DataProviderName);
              moduleProviderList.Add(new ModuleProvider(string.Empty, securityRoot.ManagerType.AssemblyQualifiedName, manager.Provider.Title, securityRoot.DataProviderName, securityRoot.Id.ToString(), false, securityRoot.GetType().AssemblyQualifiedName));
            }
          }
        }
      }
label_25:
      return moduleProviderList.ToArray();
    }
  }
}
