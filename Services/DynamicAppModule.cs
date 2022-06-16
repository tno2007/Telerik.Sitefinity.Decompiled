// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.DynamicAppModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Services
{
  internal class DynamicAppModule : IModule, ISecuredModule, IStatisticSupportModule
  {
    private string name;
    private string title;
    private string description;
    private Guid moduleId;
    private IEnumerable<DynamicTypeInfo> typeInfos;

    public DynamicAppModule(
      IDynamicModule dynamicModule,
      IEnumerable<IDynamicModuleType> dynamicModuleTypes)
    {
      this.moduleId = dynamicModule.Id;
      this.name = dynamicModule.Name;
      this.title = dynamicModule.Title;
      this.description = dynamicModule.Description;
      this.LandingPageId = dynamicModule.PageId;
      this.typeInfos = (IEnumerable<DynamicTypeInfo>) dynamicModuleTypes.Select<IDynamicModuleType, DynamicTypeInfo>((Func<IDynamicModuleType, DynamicTypeInfo>) (dt => new DynamicTypeInfo(dt))).ToArray<DynamicTypeInfo>();
    }

    public string Name => this.name;

    public string Title => this.title;

    public string Description => this.description;

    public string ClassId => string.Empty;

    public StartupType Startup
    {
      get => StartupType.OnApplicationStart;
      set
      {
      }
    }

    public bool IsApplicationModule => false;

    public Guid LandingPageId { get; private set; }

    public void Initialize(ModuleSettings settings)
    {
    }

    public void Install(SiteInitializer initializer, Version upgradeFrom)
    {
    }

    public IControlPanel GetControlPanel() => (IControlPanel) null;

    public Type[] Managers => new Type[1]
    {
      typeof (DynamicModuleManager)
    };

    public Guid ModuleId => this.moduleId;

    /// <inheritdoc />
    public void Load()
    {
    }

    /// <inheritdoc />
    public void Unload()
    {
    }

    /// <inheritdoc />
    public void Uninstall(SiteInitializer initializer)
    {
    }

    /// <summary>Gets the security roots for this module.</summary>
    /// <param name="getContextRootsOnly">If set to true, returns only the security roots relevant explicitly to the current site (not including system providers).</param>
    /// <returns>The list of security roots for the module</returns>
    public IList<SecurityRoot> GetSecurityRoots(bool getContextRootsOnly = true)
    {
      List<SecurityRoot> securityRoots = new List<SecurityRoot>();
      DynamicModuleManager manager = DynamicModuleManager.GetManager();
      foreach (DataProviderBase contextProvider in manager.GetContextProviders(this.name))
      {
        if (contextProvider.GetSecurityRoot(true) is SecurityRoot securityRoot)
        {
          securityRoot.DataProviderName = contextProvider.Name;
          securityRoot.ManagerType = manager.GetType();
          securityRoots.Add(securityRoot);
        }
      }
      return (IList<SecurityRoot>) securityRoots;
    }

    IEnumerable<IStatisticSupportTypeInfo> IStatisticSupportModule.GetTypeInfos() => (IEnumerable<IStatisticSupportTypeInfo>) this.typeInfos.Where<DynamicTypeInfo>((Func<DynamicTypeInfo, bool>) (t => t.SupportedStatistics.Any<string>()));

    public IEnumerable<DynamicTypeInfo> GetTypes() => this.typeInfos;
  }
}
