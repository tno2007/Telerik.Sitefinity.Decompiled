// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicModulesUpgradeStrategyTo73
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;

namespace Telerik.Sitefinity.DynamicModules.Builder.Upgrade
{
  internal class DynamicModulesUpgradeStrategyTo73
  {
    private readonly SiteInitializer initializer;
    private readonly ModuleBuilderManager moduleBuilderManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicModulesUpgradeStrategyTo73" /> class.
    /// </summary>
    /// <param name="initializer">The site initializer which contains various managers in transaction.</param>
    public DynamicModulesUpgradeStrategyTo73(SiteInitializer initializer)
    {
      this.initializer = initializer;
      this.moduleBuilderManager = initializer.GetManagerInTransaction<ModuleBuilderManager>();
    }

    internal void Upgrade()
    {
      this.UpgradeData();
      this.UpgradeConfig();
    }

    internal void UpgradeData()
    {
      if (!this.moduleBuilderManager.GetDynamicModules().Any<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.CanInheritPermissions == false)))
        return;
      this.UpgradeDynamicModuleInheritsPermissions();
      this.UpgradeModuleDynamicContentProvidersPermissions();
      this.UpgradeTypeDynamicContentProvidersPermissions();
      this.UpgradeDynamicModuleTypesPermissions();
    }

    internal void UpgradeConfig() => this.UpdatePermissionsDialogs();

    /// <summary>Upgrades the dynamic module inherits permissions.</summary>
    private void UpgradeDynamicModuleInheritsPermissions()
    {
      IQueryable<DynamicModule> dynamicModules = this.moduleBuilderManager.GetDynamicModules();
      if (dynamicModules.Count<DynamicModule>() == 0)
        return;
      IEnumerable<Permission> ownPermissions = this.moduleBuilderManager.GetDefaultContextProvider().GetOwnPermissions();
      using (new ManagerSettingsRegion((IManager) this.moduleBuilderManager).SuppressOpenAccessFieldsAutoCalculation())
      {
        foreach (DynamicModule securedObject in (IEnumerable<DynamicModule>) dynamicModules)
        {
          if (!securedObject.CanInheritPermissions)
            securedObject.CanInheritPermissions = true;
          if (!securedObject.InheritsPermissions && securedObject.GetOwnPermissions().Count<Permission>() <= 0 && PermissionComparer.ArePermissionCollectionsEqual(securedObject.GetInheritedPermissions(), ownPermissions))
            securedObject.InheritsPermissions = true;
        }
      }
    }

    /// <summary>
    /// Upgrades the module dynamic content providers permissions. The goal of the upgrade is to make the ModuleBuilderProvider a permission parent of the permission holders at
    /// DynamicModule level (the same as the actual DynamicModule objects). After the upgrade the Break/restore inheritance button will be available for the DynamicModule permission holders.
    /// In order for the restore inheritance button to work for already existing permission holders we need to link them to the ModuleBuilderProvider permissions.
    /// </summary>
    private void UpgradeModuleDynamicContentProvidersPermissions()
    {
      Guid[] moduleBuilderProviderPermissionsIds = this.moduleBuilderManager.SecurityRoot.Permissions.Select<Permission, Guid>((Func<Permission, Guid>) (p => p.Id)).ToArray<Guid>();
      Permission[] array = this.moduleBuilderManager.GetPermissions().Where<Permission>((Expression<Func<Permission, bool>>) (p => moduleBuilderProviderPermissionsIds.Contains<Guid>(p.Id))).ToArray<Permission>();
      IQueryable<DynamicContentProvider> queryable = this.moduleBuilderManager.GetDynamicContentProviders().Where<DynamicContentProvider>((Expression<Func<DynamicContentProvider, bool>>) (p => p.ParentSecuredObjectType.Equals(typeof (DynamicModule).FullName) && p.CanInheritPermissions == false));
      using (new ManagerSettingsRegion((IManager) this.moduleBuilderManager).SuppressOpenAccessFieldsAutoCalculation())
      {
        foreach (DynamicContentProvider dynamicContentProvider in (IEnumerable<DynamicContentProvider>) queryable)
        {
          dynamicContentProvider.CanInheritPermissions = true;
          foreach (Permission permission in array)
            dynamicContentProvider.Permissions.Add(permission);
        }
      }
    }

    /// <summary>
    /// Upgrades the type dynamic content providers permissions.
    /// </summary>
    private void UpgradeTypeDynamicContentProvidersPermissions()
    {
      IQueryable<DynamicContentProvider> queryable = this.moduleBuilderManager.GetDynamicContentProviders().Where<DynamicContentProvider>((Expression<Func<DynamicContentProvider, bool>>) (p => p.ParentSecuredObjectType.Equals(typeof (DynamicModuleType).FullName)));
      using (new ManagerSettingsRegion((IManager) this.moduleBuilderManager).SuppressOpenAccessFieldsAutoCalculation())
      {
        foreach (ISecuredObject securedObject in (IEnumerable<DynamicContentProvider>) queryable)
          this.GrantViewBackEndLinkAction(securedObject);
      }
    }

    /// <summary>Upgrades the dynamic module types permissions.</summary>
    private void UpgradeDynamicModuleTypesPermissions()
    {
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) this.moduleBuilderManager.GetDynamicModules())
      {
        DynamicModule module = dynamicModule;
        IEnumerable<Permission> permissionsToCopy = module.InheritsPermissions ? module.GetInheritedPermissions() : module.GetOwnPermissions();
        IQueryable<DynamicModuleType> queryable = this.moduleBuilderManager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == module.Id));
        using (new ManagerSettingsRegion((IManager) this.moduleBuilderManager).SuppressOpenAccessFieldsAutoCalculation())
        {
          foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) queryable)
          {
            if (dynamicModuleType.InheritsPermissions && dynamicModuleType.GetInheritedPermissions().Count<Permission>() == 0)
              dynamicModuleType.InheritsPermissions = false;
            this.CopyMissingParentPermissions(dynamicModuleType, permissionsToCopy);
            this.GrantViewBackEndLinkAction((ISecuredObject) dynamicModuleType);
          }
        }
      }
    }

    private void CopyMissingParentPermissions(
      DynamicModuleType moduleType,
      IEnumerable<Permission> permissionsToCopy)
    {
      foreach (Permission permission in permissionsToCopy)
      {
        if (!moduleType.Permissions.Contains(permission))
          moduleType.Permissions.Add(permission);
      }
    }

    /// <summary>
    /// Grants ViewBackEndLink permission for <paramref name="securedObject" /> with view permissions in previous version (before 7.3).
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable", Justification = "The ToList here prevents two queries to be fired - first for count and then for the items.")]
    private void GrantViewBackEndLinkAction(ISecuredObject securedObject)
    {
      List<Permission> list = securedObject.GetActivePermissions().Where<Permission>((Expression<Func<Permission, bool>>) (p => (p.Grant & 1) == 1)).ToList<Permission>();
      if (list.Count<Permission>() == 0)
        return;
      if (securedObject.InheritsPermissions)
      {
        securedObject.InheritsPermissions = false;
        this.CloneParentPermissionsAsOwnForItem(securedObject);
      }
      foreach (Permission permission1 in list)
      {
        Permission permission = permission1;
        if (!securedObject.Permissions.Any<Permission>((Func<Permission, bool>) (p => p.PrincipalId == permission.PrincipalId && p.ObjectId == securedObject.Id && p.SetName.Equals("SitemapGeneration", StringComparison.OrdinalIgnoreCase))))
        {
          Permission permission2 = this.moduleBuilderManager.CreatePermission("SitemapGeneration", securedObject.Id, permission.PrincipalId);
          permission2.GrantActions(true, "ViewBackendLink");
          securedObject.Permissions.Add(permission2);
        }
      }
    }

    /// <summary>
    /// Clones the parent permissions as own for item.
    /// Performed on item's level only, without further recursive calls for any child items.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    private void CloneParentPermissionsAsOwnForItem(ISecuredObject securedObject)
    {
      List<Permission> permissionList = new List<Permission>();
      foreach (Permission inheritedPermission1 in securedObject.GetInheritedPermissions())
      {
        Permission inheritedPermission = inheritedPermission1;
        Permission permission1 = securedObject.Permissions.SingleOrDefault<Permission>((Func<Permission, bool>) (p => p.SetName.Equals(inheritedPermission.SetName, StringComparison.OrdinalIgnoreCase) && p.PrincipalId == inheritedPermission.PrincipalId && p.ObjectId == securedObject.Id));
        if (permission1 != null)
        {
          permission1.Grant = inheritedPermission.Grant;
          permission1.Deny = inheritedPermission.Deny;
        }
        else
        {
          Permission permission2 = this.moduleBuilderManager.Provider.CreatePermission(inheritedPermission.SetName, securedObject.Id, inheritedPermission.PrincipalId);
          permission2.Grant = inheritedPermission.Grant;
          permission2.Deny = inheritedPermission.Deny;
          permissionList.Add(permission2);
        }
      }
      foreach (Permission permission in permissionList)
        securedObject.Permissions.Add(permission);
    }

    private void UpdatePermissionsDialogs()
    {
      IQueryable<DynamicModule> dynamicModules = this.moduleBuilderManager.GetDynamicModules();
      if (dynamicModules.Count<DynamicModule>() == 0)
        return;
      DynamicModulesConfig config = this.initializer.Context.GetConfig<DynamicModulesConfig>();
      foreach (DynamicModule dynamicModule1 in (IEnumerable<DynamicModule>) dynamicModules)
      {
        DynamicModule dynamicModule = dynamicModule1;
        if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
        {
          IQueryable<DynamicModuleType> dynamicModuleTypes = this.moduleBuilderManager.GetDynamicModuleTypes();
          Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == dynamicModule.Id);
          foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
          {
            ContentViewControlElement contentViewControl = config.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(dynamicModuleType.GetFullTypeName())];
            if (contentViewControl != null && contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateListViewName(dynamicModuleType.DisplayName)] is MasterGridViewElement masterGridViewElement)
            {
              this.moduleBuilderManager.LoadDynamicModuleTypeGraph(dynamicModuleType, false);
              if (MasterListViewGenerator.RemoveDialog(masterGridViewElement, "ModulePermissionsDialog", "permissions"))
                MasterListViewGenerator.AddPermissionsDialogForDynamicModuleType((IDynamicModule) dynamicModule, (IDynamicModuleType) dynamicModuleType, masterGridViewElement);
              MasterListViewGenerator.AddPermissionsDialogForDynamicContent((IDynamicModule) dynamicModule, (IDynamicModuleType) dynamicModuleType, masterGridViewElement);
              this.AddPermissionsDialogForDynamicContentMenuCommand(masterGridViewElement);
            }
          }
        }
      }
    }

    private void AddPermissionsDialogForDynamicContentMenuCommand(
      MasterGridViewElement masterGridView)
    {
      if (!(masterGridView.ViewModesConfig.Elements.FirstOrDefault<ViewModeElement>((Func<ViewModeElement, bool>) (e => e.Name == "Grid")) is GridViewModeElement gridViewModeElement))
        return;
      foreach (IColumnDefinition column in gridViewModeElement.Columns)
      {
        if (column is ActionMenuColumnElement menuColumnElement && !(menuColumnElement.MenuItems.Elements.FirstOrDefault<WidgetElement>((Func<WidgetElement, bool>) (e => e.Name == "permissionsDynamicContent")) is CommandWidgetElement))
        {
          ConfigElementList<WidgetElement> menuItems = menuColumnElement.MenuItems;
          CommandWidgetElement element = new CommandWidgetElement((ConfigElement) menuColumnElement.MenuItems);
          element.Name = "Permissions";
          element.WrapperTagKey = HtmlTextWriterTag.Li;
          element.CommandName = "permissionsDynamicContent";
          element.Text = "Permissions";
          element.ResourceClassId = typeof (ModuleBuilderResources).Name;
          element.CssClass = "sfPermissionsItm";
          element.WidgetType = typeof (CommandWidget);
          menuItems.Add((WidgetElement) element);
          break;
        }
      }
    }
  }
}
