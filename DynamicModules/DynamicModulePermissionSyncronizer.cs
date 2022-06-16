// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.DynamicModulePermissionSyncronizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.FetchOptimization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.DynamicModules
{
  internal class DynamicModulePermissionSyncronizer
  {
    /// <summary>
    /// Resets the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> children permissions when inheritance is changed or permission is added
    /// for the parent <see cref="!:DynamicContentType" /> or the equivalent permission holder <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </summary>
    /// <param name="parent">The parent.</param>
    /// <param name="permissionsToAssignIds">The list of permissions to be assigned to the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> children.</param>
    /// <param name="unassignOldInheritedPermissions">
    /// A flag that indicates if the existing link to the inherited permissions for the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> children should be removed.
    /// The default value is <c>True</c>
    /// </param>
    internal virtual void ResetChildPermissions(
      ISecuredObject parent,
      IEnumerable<Guid> permissionsToAssignIds,
      bool unassignOldInheritedPermissions = true)
    {
      ModuleBuilderDataProvider provider = ModuleBuilderManager.GetManager().Provider;
      Type dynamicContentClrType = this.GetDynamicContentClrType(parent, provider);
      string dynamicContentVoaClass = provider.ConvertClrTypeVoaClass(dynamicContentClrType);
      IQueryable<PermissionsInheritanceMap> source = provider.GetPermissionChildren(parent.Id).Where<PermissionsInheritanceMap>((Expression<Func<PermissionsInheritanceMap, bool>>) (pc => pc.ChildObjectTypeName == dynamicContentVoaClass));
      DynamicModuleManager manager = DynamicModuleManager.GetManager(this.GetDynamicContentProviderName(parent));
      SitefinityOAContext context = (manager.Provider as OpenAccessDynamicModuleProvider).GetContext();
      FetchStrategy fetchStrategy1 = context.FetchStrategy;
      FetchStrategy fetchStrategy2 = fetchStrategy1 != null ? FetchStrategyResolver.Clone(fetchStrategy1) : new FetchStrategy();
      fetchStrategy2.LoadWith<DynamicContent>((Expression<Func<DynamicContent, object>>) (i => i.Permissions));
      context.FetchStrategy = fetchStrategy2;
      Permission[] array = this.GetParentPermissionsFromChildProvider(manager.Provider, ((IDataItem) parent).ApplicationName, permissionsToAssignIds).ToArray<Permission>();
      Guid[] itemIds = source.Select<PermissionsInheritanceMap, Guid>((Expression<Func<PermissionsInheritanceMap, Guid>>) (ch => ch.ChildObjectId)).ToArray<Guid>();
      IQueryable<DynamicContent> dataItems = manager.GetDataItems(dynamicContentClrType);
      Expression<Func<DynamicContent, bool>> predicate = (Expression<Func<DynamicContent, bool>>) (i => itemIds.Contains<Guid>(i.Id));
      foreach (DynamicContent dynamicContent in dataItems.Where<DynamicContent>(predicate).ToList<DynamicContent>())
      {
        if (unassignOldInheritedPermissions)
          this.UnassignInheritedPermissions((ISecuredObject) dynamicContent);
        this.AssignPermissions((ISecuredObject) dynamicContent, (IEnumerable<Permission>) array);
      }
      manager.SaveChanges();
    }

    private string GetDynamicContentProviderName(ISecuredObject parent)
    {
      Type type = parent.GetType();
      if (type.IsAssignableFrom(typeof (DynamicContentProvider)))
        return ((DynamicContentProvider) parent).Name;
      if (type.IsAssignableFrom(typeof (DynamicModuleType)))
        return ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName();
      throw new ArgumentException("Unknown DynamicContent permission holder.");
    }

    private Type GetDynamicContentClrType(
      ISecuredObject parent,
      ModuleBuilderDataProvider provider)
    {
      Type type = parent.GetType();
      DynamicModuleType dynamicModuleType;
      if (type.IsAssignableFrom(typeof (DynamicContentProvider)))
      {
        DynamicContentProvider dynamicContentProvider = (DynamicContentProvider) parent;
        dynamicModuleType = provider.GetDynamicModuleType(dynamicContentProvider.ParentSecuredObjectId);
      }
      else
      {
        if (!type.IsAssignableFrom(typeof (DynamicModuleType)))
          throw new ArgumentException("Unknown DynamicContent permission holder.");
        dynamicModuleType = (DynamicModuleType) parent;
      }
      return TypeResolutionService.ResolveType(dynamicModuleType.GetFullTypeName());
    }

    private IQueryable<Permission> GetParentPermissionsFromChildProvider(
      DynamicModuleDataProvider dynamicModuleProvider,
      string parentApplicationName,
      IEnumerable<Guid> permissionsRetrieved)
    {
      if (!(dynamicModuleProvider is OpenAccessDynamicModuleProvider dynamicModuleProvider1))
        throw new NotSupportedException("Unsupported provider type. Currently supporting only " + typeof (OpenAccessDynamicModuleProvider).FullName.ToString());
      return dynamicModuleProvider1.GetPermissionsInApplication(parentApplicationName).Where<Permission>((Expression<Func<Permission, bool>>) (p => permissionsRetrieved.Contains<Guid>(p.Id)));
    }

    private void AssignPermissions(
      ISecuredObject securedObject,
      IEnumerable<Permission> permissions)
    {
      foreach (Permission permission in permissions)
      {
        if (securedObject.IsPermissionSetSupported(permission.SetName))
          securedObject.Permissions.Add(permission);
      }
    }

    private void UnassignInheritedPermissions(ISecuredObject securedObject)
    {
      foreach (Permission permission in securedObject.GetInheritedPermissions().ToArray<Permission>())
        securedObject.Permissions.Remove(permission);
    }

    private DynamicContent TryGetChildDynamicContent(
      DynamicModuleManager manager,
      Type childType,
      Guid childId)
    {
      DynamicContent childDynamicContent = (DynamicContent) null;
      try
      {
        childDynamicContent = manager.GetDataItem(childType, childId);
      }
      catch (ItemNotFoundException ex)
      {
      }
      return childDynamicContent;
    }
  }
}
