// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.BackendDefinitionInstaller
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  /// <summary>
  /// This class provides logic for installing backend definitions for the dynamic module.
  /// </summary>
  internal class BackendDefinitionInstaller
  {
    private ModuleBuilderManager moduleBuilderManager;

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Install.BackendDefinitionInstaller" />.
    /// </summary>
    /// <param name="moduleBuilderManager">
    /// The instance of the <see cref="!:IModuleBuilderManager" /> to be used when
    /// writing the definitions.
    /// </param>
    public BackendDefinitionInstaller(ModuleBuilderManager moduleBuilderManager) => this.moduleBuilderManager = moduleBuilderManager != null ? moduleBuilderManager : throw new ArgumentNullException(nameof (moduleBuilderManager));

    /// <summary>
    /// Creates the backend type definitions, if definitions are not already existing.
    /// </summary>
    /// <param name="module">The module.</param>
    public void CreateDefaultBackendTypeDefinitionsIfNotExisting(DynamicModule module)
    {
      IQueryable<DynamicModuleType> queryable = this.moduleBuilderManager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (mt => mt.ParentModuleId == module.Id));
      this.moduleBuilderManager.LoadDynamicModuleGraph(module);
      foreach (DynamicModuleType moduleType in (IEnumerable<DynamicModuleType>) queryable)
        BackendDefinitionInstaller.GenerateDefaultTypeViewDefinition(moduleType);
    }

    /// <summary>
    /// Creates the backend type definitions, if definitions are not already existing.
    /// </summary>
    /// <param name="module">The module.</param>
    /// <param name="moduleType">Type of the module.</param>
    public void CreateDefaultBackendTypeDefinitions(
      DynamicModule module,
      DynamicModuleType moduleType)
    {
      BackendDefinitionInstaller.GenerateDefaultTypeViewDefinition(moduleType);
    }

    /// <summary>Reinstalls the backend grid definitions.</summary>
    /// <param name="module">The module.</param>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="moduleTypeDisplayName">Display name of the module type.</param>
    public void ReinstallBackendGridDefinitions(
      DynamicModule module,
      DynamicModuleType moduleType,
      string moduleTypeDisplayName = null)
    {
      BackendDefinitionInstaller.InvalidateBackendDefinitions();
    }

    /// <summary>Reinstalls the backend detail definitions.</summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="moduleTypeDisplayName">Display name of the module type.</param>
    public void ReinstallBackendDetailDefinitions(
      DynamicModuleType moduleType,
      string moduleTypeDisplayName = null)
    {
      BackendDefinitionInstaller.InvalidateBackendDefinitions();
    }

    /// <summary>Deletes the back-end type definitions.</summary>
    /// <param name="module">The module.</param>
    public void DeleteBackendTypeDefinitions(string contentTypeName)
    {
      ContentViewConfig contentViewConfig = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>();
      string viewDefinitionName = ModuleNamesGenerator.GenerateContentViewDefinitionName(contentTypeName);
      if (!contentViewConfig.ContentViewControls.ContainsKey(viewDefinitionName))
        return;
      ConfigManager manager = ConfigManager.GetManager();
      ContentViewConfig section = manager.GetSection<ContentViewConfig>();
      section.ContentViewControls.Remove(viewDefinitionName);
      manager.SaveSection((ConfigSection) section);
    }

    internal static MasterGridViewElement GetMasterViewDefinition(
      IDynamicModuleType moduleType,
      ContentViewControlElement contentView,
      string mainFieldName,
      IDynamicModule module)
    {
      MasterGridViewElement masterViewDefinition;
      if (moduleType.IsSelfReferencing)
        masterViewDefinition = MasterListViewGenerator.GenerateSelfreferencingHierarchy(moduleType, (ConfigElement) contentView.ViewsConfig, mainFieldName, module);
      else if (!moduleType.IsSelfReferencing && module.Types.Any<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.ParentTypeId == moduleType.Id)))
      {
        List<IDynamicModuleType> list = module.Types.Where<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.ParentTypeId == moduleType.Id)).ToList<IDynamicModuleType>();
        masterViewDefinition = MasterListViewGenerator.GenerateHierarchy(moduleType, (IEnumerable<IDynamicModuleType>) list, (ConfigElement) contentView.ViewsConfig, mainFieldName, module);
      }
      else
        masterViewDefinition = MasterListViewGenerator.Generate(moduleType, (ConfigElement) contentView.ViewsConfig, mainFieldName, module);
      return masterViewDefinition;
    }

    internal static void InvalidateBackendDefinitions() => CacheDependency.Notify(new object[1]
    {
      (object) typeof (DynamicModulesConfig)
    });

    internal void GenerateDefaultContentViewDefinition(
      DynamicModule module,
      IDynamicModuleType moduleType)
    {
      DynamicModulesConfig dynamicModulesConfig = Telerik.Sitefinity.Configuration.Config.Get<DynamicModulesConfig>();
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls = dynamicModulesConfig.ContentViewControls;
      if (contentViewControls.Values.Any<ContentViewControlElement>((Func<ContentViewControlElement, bool>) (c => c.ContentTypeName == moduleType.GetFullTypeName())))
        return;
      ConfigManager manager = ConfigManager.GetManager();
      ContentViewControlElement element = DynamicModulesDefinitions.DefineContentViewControl((ConfigElement) contentViewControls, module.Name, moduleType.Id, moduleType.GetFullTypeName(), moduleType.DisplayName);
      contentViewControls.Add(element);
      DynamicModulesConfig section = dynamicModulesConfig;
      manager.SaveSection((ConfigSection) section);
    }

    /// <summary>Adds default content view controls if not existing.</summary>
    private static void GenerateDefaultTypeViewDefinition(DynamicModuleType moduleType)
    {
      ContentViewConfig contentViewConfig = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>();
      string viewDefinitionName = ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName());
      if (contentViewConfig.ContentViewControls.ContainsKey(viewDefinitionName))
        return;
      ConfigManager manager = ConfigManager.GetManager();
      ContentViewConfig section = manager.GetSection<ContentViewConfig>();
      string childPathForKey = Telerik.Sitefinity.Configuration.Config.Get<DynamicModulesConfig>().ContentViewControls.GenerateChildPathForKey(viewDefinitionName);
      section.ContentViewControls.AddLinkedElement(viewDefinitionName, childPathForKey, "ModuleBuilder");
      manager.SaveSection((ConfigSection) section);
    }
  }
}
