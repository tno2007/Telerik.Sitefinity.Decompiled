// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentUpgradePermissionsStrategyTo73
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.DynamicModules.Builder.Upgrade
{
  internal class DynamicContentUpgradePermissionsStrategyTo73
  {
    private ProvidersCollection<DynamicModuleDataProvider> dynamicModuleProviders;
    private const string UpgradePermissionsTransactionName = "sftran_upgrade_dynamic_content_permissions";
    private int numberOfBatchesPerTypeProcessed;
    private int totalNumberOfBatchesProcessed;

    internal void Upgrade() => this.UpgradeDynamicContentItemsPermissions();

    private void UpgradeDynamicContentItemsPermissions()
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      this.totalNumberOfBatchesProcessed = 0;
      using (new ElevatedModeRegion((IManager) manager))
      {
        foreach (DynamicModuleType dynamicType in manager.Provider.GetDynamicModuleTypes().ToArray<DynamicModuleType>())
          this.UpgradePermissionsPerDynamicType(manager, dynamicType);
      }
    }

    private void UpgradePermissionsPerDynamicType(
      ModuleBuilderManager moduleBuilderManager,
      DynamicModuleType dynamicType)
    {
      Type dynamicContentClrType = TypeResolutionService.ResolveType(dynamicType.GetFullTypeName(), false);
      if (dynamicContentClrType == (Type) null)
        return;
      this.numberOfBatchesPerTypeProcessed = 0;
      Log.Write((object) string.Format("Start upgrading DynamicContent items permissions for type : {0}", (object) dynamicContentClrType.FullName), ConfigurationPolicy.UpgradeTrace);
      foreach (DynamicModuleDataProvider moduleStaticProvider in (Collection<DynamicModuleDataProvider>) this.DynamicModuleStaticProviders)
      {
        ISecuredObject securedObject = DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) moduleBuilderManager, (ISecuredObject) dynamicType, moduleStaticProvider.Name);
        DynamicModuleManager manager = DynamicModuleManager.GetManager(moduleStaticProvider.Name, "sftran_upgrade_dynamic_content_permissions");
        List<Permission> permissionsInProvider = this.GetPermissionHolderPermissionsInProvider(manager.Provider, securedObject);
        using (new ManagerSettingsRegion((IManager) manager).SuppressSecurityChecks().SuppressEvents().SuppressNotifications().SuppressFilterQueriesByViewPermissions().SuppressOpenAccessFieldsAutoCalculation())
        {
          using (new CultureRegion(CultureInfo.InvariantCulture))
            this.AddPermissionToDynamicContentItems(permissionsInProvider, securedObject.Id, dynamicContentClrType, manager);
        }
      }
      Log.Write((object) string.Format("End upgrading DynamicContent items permissions for type : {0}.", (object) dynamicContentClrType.FullName), ConfigurationPolicy.UpgradeTrace);
      Log.Write((object) string.Format("For all types - total number of batches processed : {0}.", (object) this.totalNumberOfBatchesProcessed), ConfigurationPolicy.UpgradeTrace);
    }

    private void AddPermissionToDynamicContentItems(
      List<Permission> permissions,
      Guid permissionHolderId,
      Type dynamicContentClrType,
      DynamicModuleManager manager)
    {
      string childObjectTypeName = manager.Provider.ConvertClrTypeVoaClass(dynamicContentClrType);
      IQueryable<DynamicContent> source1 = manager.GetDataItems(dynamicContentClrType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (x => x.CanInheritPermissions == false));
      bool flag1 = permissions.Count<Permission>() > 0;
      int count = 500;
      IQueryable<DynamicContent> source2 = source1.Take<DynamicContent>(count);
      for (bool flag2 = source2.Any<DynamicContent>(); flag2; flag2 = source2.Any<DynamicContent>())
      {
        foreach (DynamicContent dynamicContentItem in (IEnumerable<DynamicContent>) source2)
        {
          dynamicContentItem.Permissions.Clear();
          dynamicContentItem.CanInheritPermissions = true;
          dynamicContentItem.InheritsPermissions = true;
          manager.Provider.CreatePermissionsInheritanceMap(permissionHolderId, dynamicContentItem.Id, childObjectTypeName);
          if (flag1)
            this.SetPermissionsToItem(dynamicContentItem, permissions);
        }
        manager.Provider.CommitTransaction();
        ++this.numberOfBatchesPerTypeProcessed;
        ++this.totalNumberOfBatchesProcessed;
        Log.Write((object) string.Format("DynamicContent items for type {0} - batch number {1} processed", (object) dynamicContentClrType.FullName, (object) this.numberOfBatchesPerTypeProcessed), ConfigurationPolicy.UpgradeTrace);
        source2 = source1.Take<DynamicContent>(count);
      }
    }

    private void SetPermissionsToItem(
      DynamicContent dynamicContentItem,
      List<Permission> permissions)
    {
      foreach (Permission permission in permissions)
        dynamicContentItem.Permissions.Add(permission);
    }

    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable", Justification = "The ToList() is deliberate.")]
    private List<Permission> GetPermissionHolderPermissionsInProvider(
      DynamicModuleDataProvider dynamicModuleProvider,
      ISecuredObject permissionHolder)
    {
      if (permissionHolder.Permissions.Count<Permission>() == 0)
        return new List<Permission>();
      if (!(dynamicModuleProvider is OpenAccessDynamicModuleProvider dynamicModuleProvider1))
        return new List<Permission>();
      Guid[] permissionHolderPermissionIds = permissionHolder.Permissions.Select<Permission, Guid>((Func<Permission, Guid>) (p => p.Id)).ToArray<Guid>();
      IQueryable<Permission> source1 = dynamicModuleProvider1.GetPermissionsInApplication(((IDataItem) permissionHolder).ApplicationName).Where<Permission>((Expression<Func<Permission, bool>>) (p => p.SetName == "General" && permissionHolderPermissionIds.Contains<Guid>(p.Id)));
      IQueryable<Permission> source2;
      if (permissionHolder.InheritsPermissions)
        source2 = source1.Where<Permission>((Expression<Func<Permission, bool>>) (p => p.ObjectId != permissionHolder.Id));
      else
        source2 = source1.Where<Permission>((Expression<Func<Permission, bool>>) (p => p.ObjectId == permissionHolder.Id));
      return source2.ToList<Permission>();
    }

    private ProvidersCollection<DynamicModuleDataProvider> DynamicModuleStaticProviders
    {
      get
      {
        if (this.dynamicModuleProviders == null)
        {
          this.dynamicModuleProviders = ManagerBase<DynamicModuleDataProvider>.StaticProvidersCollection;
          if (this.dynamicModuleProviders == null)
            this.dynamicModuleProviders = DynamicModuleManager.GetManager().StaticProviders;
        }
        return this.dynamicModuleProviders;
      }
    }
  }
}
