// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.PermissionsInstaller
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  internal class PermissionsInstaller
  {
    private ModuleBuilderManager moduleBuilderManager;

    private PermissionsInstaller()
    {
    }

    public PermissionsInstaller(ModuleBuilderManager moduleBuilderManager) => this.moduleBuilderManager = moduleBuilderManager != null ? moduleBuilderManager : throw new ArgumentNullException(nameof (moduleBuilderManager));

    /// <summary>
    /// Installs the default permissions for the specified module.
    /// </summary>
    /// <param name="module">The module.</param>
    public void InstallModulePermissions(DynamicModule module)
    {
      foreach (DynamicModuleType type in module.Types)
        this.InstallModuleTypePermissions(module, type);
    }

    /// <summary>
    /// Installs the permissions for the specified module type.
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="module">The module.</param>
    public void InstallModuleTypePermissions(DynamicModule module, DynamicModuleType moduleType)
    {
      if (!moduleType.CheckFieldPermissions)
        return;
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        if (!field.IsTransient && (field.Permissions == null || field.Permissions.Count == 0))
          this.InstallModuleTypeFieldPermissions(field);
      }
    }

    public void InstallModuleTypeFieldPermissions(DynamicModuleField field)
    {
      ISecuredObject secured = (ISecuredObject) field;
      string permissionSetName = field.GetPermissionSetName();
      this.AddInitialPermissionsFor(secured, permissionSetName, SecurityManager.EveryoneRole.Id, "View");
      this.AddInitialPermissionsFor(secured, permissionSetName, SecurityManager.EditorsRole.Id, "Modify");
      this.AddInitialPermissionsFor(secured, permissionSetName, SecurityManager.OwnerRole.Id, "Modify");
    }

    internal static void AddPermissionSet(
      string securedItemName,
      string permissionSetName,
      ConfigElementDictionary<string, Telerik.Sitefinity.Security.Configuration.Permission> permissions)
    {
      ModuleBuilderResources builderResources = Res.Get<ModuleBuilderResources>();
      string plural = PluralsResolver.Instance.ToPlural(securedItemName);
      if (permissions.TryGetValue(permissionSetName, out Telerik.Sitefinity.Security.Configuration.Permission _))
        return;
      Telerik.Sitefinity.Security.Configuration.Permission element = new Telerik.Sitefinity.Security.Configuration.Permission((ConfigElement) permissions)
      {
        Name = permissionSetName,
        Title = string.Format(builderResources.TypePermissions, (object) plural),
        Description = builderResources.TypePermissionsDescription
      };
      permissions.Add(element);
      ConfigElementDictionary<string, SecurityAction> actions = element.Actions;
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "View",
        Type = SecurityActionTypes.View,
        Title = string.Format(builderResources.ViewItemAction, (object) plural),
        Description = string.Format(builderResources.ViewItemDescription, (object) plural)
      });
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "Create",
        Type = SecurityActionTypes.Create,
        Title = string.Format(builderResources.CreateItemAction, (object) plural),
        Description = string.Format(builderResources.CreateItemDescription, (object) plural)
      });
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "Modify",
        Type = SecurityActionTypes.Modify,
        Title = string.Format(builderResources.ModifyItemAction, (object) plural),
        Description = string.Format(builderResources.ModifyItemDescription, (object) plural)
      });
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "Delete",
        Type = SecurityActionTypes.Delete,
        Title = string.Format(builderResources.DeleteItemAction, (object) plural),
        Description = string.Format(builderResources.DeleteItemDescription, (object) plural)
      });
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = string.Format(builderResources.ChangeItemPermissions, (object) plural),
        Description = string.Format(builderResources.ChangeItemPermissionsDescription, (object) plural)
      });
    }

    internal static void AddFieldPermissionSet(
      string securedItemName,
      string permissionSetName,
      ConfigElementDictionary<string, Telerik.Sitefinity.Security.Configuration.Permission> permissions)
    {
      ModuleBuilderResources builderResources = Res.Get<ModuleBuilderResources>();
      if (permissions.TryGetValue(permissionSetName, out Telerik.Sitefinity.Security.Configuration.Permission _))
        return;
      Telerik.Sitefinity.Security.Configuration.Permission element = new Telerik.Sitefinity.Security.Configuration.Permission((ConfigElement) permissions)
      {
        Name = permissionSetName,
        Title = string.Format(builderResources.TypePermissions, (object) securedItemName),
        Description = builderResources.TypePermissionsDescription
      };
      permissions.Add(element);
      ConfigElementDictionary<string, SecurityAction> actions = element.Actions;
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "View",
        Type = SecurityActionTypes.View,
        Title = string.Format(builderResources.ViewItemAction, (object) securedItemName),
        Description = string.Format(builderResources.ViewItemDescription, (object) securedItemName)
      });
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "Modify",
        Type = SecurityActionTypes.Modify,
        Title = string.Format(builderResources.ModifyItemAction, (object) securedItemName),
        Description = string.Format(builderResources.ModifyItemDescription, (object) securedItemName)
      });
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = string.Format(builderResources.ChangeItemPermissions, (object) securedItemName),
        Description = string.Format(builderResources.ChangeItemPermissionsDescription, (object) securedItemName)
      });
    }

    private void AddInitialPermissionsFor(
      ISecuredObject secured,
      string permissionSet,
      Guid principalId,
      params string[] actions)
    {
      Telerik.Sitefinity.Security.Model.Permission permission = this.moduleBuilderManager.CreatePermission(permissionSet, secured.Id, principalId);
      permission.GrantActions(false, actions);
      secured.Permissions.Add(permission);
    }
  }
}
