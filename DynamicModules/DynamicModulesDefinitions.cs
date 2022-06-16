// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.DynamicModulesDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.DynamicModules
{
  internal static class DynamicModulesDefinitions
  {
    internal static bool TryDefineContentViewControls(
      ConfigElement parent,
      out IEnumerable<ContentViewControlElement> items)
    {
      if (!SystemManager.IsModuleEnabled("ModuleBuilder") || SystemManager.DelayedDatabaseInit)
      {
        items = (IEnumerable<ContentViewControlElement>) null;
        return false;
      }
      List<ContentViewControlElement> viewControlElementList = new List<ContentViewControlElement>();
      foreach (IDynamicModule module in ModuleBuilderManager.GetModules())
      {
        foreach (IDynamicModuleType type in module.Types)
          viewControlElementList.Add(DynamicModulesDefinitions.DefineContentViewControl(parent, module.Name, type.Id, type.GetFullTypeName(), type.DisplayName));
      }
      items = (IEnumerable<ContentViewControlElement>) viewControlElementList;
      return true;
    }

    internal static ContentViewControlElement DefineContentViewControl(
      ConfigElement parent,
      string moduleName,
      Guid dynamicTypeId,
      string typeFullName,
      string typeDisplayName)
    {
      string viewDefinitionName = ModuleNamesGenerator.GenerateContentViewDefinitionName(typeFullName);
      ContentViewControlElement viewControlElement = new ContentViewControlElement(parent);
      viewControlElement.Source = ConfigSource.Database;
      viewControlElement.ContentTypeName = typeFullName;
      viewControlElement.ControlDefinitionName = viewDefinitionName;
      viewControlElement.LinkModuleName = moduleName;
      viewControlElement.ManagerType = typeof (DynamicModuleManager);
      ContentViewControlElement contentView = viewControlElement;
      contentView.ViewsConfig.AddLazy((object) ModuleNamesGenerator.GenerateListViewName(typeDisplayName), (Func<ConfigElement>) (() => DynamicModulesDefinitions.DefineMasterListView(contentView, dynamicTypeId)));
      contentView.ViewsConfig.AddLazy((object) ModuleNamesGenerator.GenerateBackendInsertViewName(typeDisplayName), (Func<ConfigElement>) (() => DynamicModulesDefinitions.DefineInsertDetailsView(contentView, dynamicTypeId)));
      contentView.ViewsConfig.AddLazy((object) ModuleNamesGenerator.GenerateBackendPreviewViewName(typeDisplayName), (Func<ConfigElement>) (() => DynamicModulesDefinitions.DefinePreviewDetailsView(contentView, dynamicTypeId)));
      contentView.ViewsConfig.AddLazy((object) ModuleNamesGenerator.GenerateBackendEditViewName(typeDisplayName), (Func<ConfigElement>) (() => DynamicModulesDefinitions.DefineEditDetailsView(contentView, dynamicTypeId)));
      contentView.ViewsConfig.AddLazy((object) ModuleNamesGenerator.GenerateBackendDuplicateViewName(typeDisplayName), (Func<ConfigElement>) (() => DynamicModulesDefinitions.DefineInsertDetailsView(contentView, dynamicTypeId, true)));
      contentView.ViewsConfig.AddLazy((object) ModuleNamesGenerator.GenerateBackendVersionPreview(typeDisplayName), (Func<ConfigElement>) (() => DynamicModulesDefinitions.DefineVersionPreviewView(contentView, dynamicTypeId)));
      contentView.ViewsConfig.AddLazy((object) ModuleNamesGenerator.GenerateBackendVersionComparerPreview(typeDisplayName), (Func<ConfigElement>) (() => DynamicModulesDefinitions.DefineBackendVersioningComparisonView(contentView, dynamicTypeId)));
      return contentView;
    }

    private static ConfigElement DefineMasterListView(
      ContentViewControlElement contentView,
      Guid dynamicTypeId)
    {
      IDynamicModuleType moduleType = DynamicModulesDefinitions.GetDynamicType(dynamicTypeId);
      IDynamicModule module = ModuleBuilderManager.GetModules().Single<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Id == moduleType.ModuleId));
      return (ConfigElement) BackendDefinitionInstaller.GetMasterViewDefinition(moduleType, contentView, moduleType.MainShortTextFieldName, module);
    }

    private static ConfigElement DefineInsertDetailsView(
      ContentViewControlElement contentView,
      Guid dynamicTypeId,
      bool isDuplicate = false)
    {
      return (ConfigElement) DetailsViewGenerator.GenerateInsertDetailsView(DynamicModulesDefinitions.GetDynamicType(dynamicTypeId), contentView, isDuplicate);
    }

    private static ConfigElement DefinePreviewDetailsView(
      ContentViewControlElement contentView,
      Guid dynamicTypeId)
    {
      return (ConfigElement) DetailsViewGenerator.GeneratePreviewDetailsView(DynamicModulesDefinitions.GetDynamicType(dynamicTypeId), contentView);
    }

    private static ConfigElement DefineEditDetailsView(
      ContentViewControlElement contentView,
      Guid dynamicTypeId)
    {
      return (ConfigElement) DetailsViewGenerator.GenerateEditDetailsView(DynamicModulesDefinitions.GetDynamicType(dynamicTypeId), contentView);
    }

    private static ConfigElement DefineVersionPreviewView(
      ContentViewControlElement contentView,
      Guid dynamicTypeId)
    {
      return (ConfigElement) DetailsViewGenerator.GenerateHistoryVersionDetailsView(DynamicModulesDefinitions.GetDynamicType(dynamicTypeId), contentView);
    }

    private static ConfigElement DefineBackendVersioningComparisonView(
      ContentViewControlElement contentView,
      Guid dynamicTypeId)
    {
      return (ConfigElement) DetailsViewGenerator.DefineBackendVersioningComparisonView(DynamicModulesDefinitions.GetDynamicType(dynamicTypeId), contentView);
    }

    private static IDynamicModuleType GetDynamicType(Guid typeId) => ModuleBuilderManager.GetModules().GetTypeById(typeId) ?? throw new ItemNotFoundException("Dynamic type with ID='{0}' does not exist.".Arrange((object) typeId));
  }
}
