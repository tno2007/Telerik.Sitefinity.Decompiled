// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderModuleEventInterceptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.DynamicModules.Builder.Events;
using Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.DynamicContentProviderEvents;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>
  /// Handles <see cref="T:Telerik.Sitefinity.Data.Events.IDataEvent" />s for which the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderModule" /> needs to subscribe and the logic about processing those events.
  /// </summary>
  public class ModuleBuilderModuleEventInterceptor : 
    IModuleBuilderModuleEventInterceptor,
    IEventInterceptor
  {
    /// <summary>
    /// Subscribes this instance for <see cref="!:IEvent" />s.
    /// </summary>
    public void Subscribe()
    {
      EventHub.Subscribe<IDynamicContentProviderUpdatedEvent>(new SitefinityEventHandler<IDynamicContentProviderUpdatedEvent>(this.HandleDynamicContentProviderUpdatedEvent));
      EventHub.Subscribe<IDynamicModuleTypeUpdatedEvent>(new SitefinityEventHandler<IDynamicModuleTypeUpdatedEvent>(this.HandleDynamicModuleTypeUpdatedEvent));
    }

    private void HandleDynamicContentProviderUpdatedEvent(
      IDynamicContentProviderUpdatedEvent updatedEvent)
    {
      DynamicContentProvider originalPermissionHolder = updatedEvent.Item;
      if (!(originalPermissionHolder.ParentSecuredObjectType == typeof (DynamicModuleType).FullName))
        return;
      this.HandleTypePermissionHolderUpdateEvent((IPropertyChangeDataEvent) updatedEvent, (ISecuredObject) originalPermissionHolder);
    }

    private void HandleDynamicModuleTypeUpdatedEvent(IDynamicModuleTypeUpdatedEvent updatedEvent)
    {
      DynamicModuleType originalPermissionHolder = updatedEvent.Item;
      this.HandleTypePermissionHolderUpdateEvent((IPropertyChangeDataEvent) updatedEvent, (ISecuredObject) originalPermissionHolder);
    }

    private void HandleTypePermissionHolderUpdateEvent(
      IPropertyChangeDataEvent updatedEvent,
      ISecuredObject originalPermissionHolder)
    {
      if (!updatedEvent.ChangedProperties.ContainsKey(EventsPropertyNames.InheritsPermissions) && !updatedEvent.ChangedProperties.ContainsKey(EventsPropertyNames.Permissions))
        return;
      bool newValueOrDefault = this.GetNewValueOrDefault<bool>(updatedEvent, EventsPropertyNames.InheritsPermissions, originalPermissionHolder.InheritsPermissions);
      IEnumerable<Permission> toSyncToChildren = this.GetPermissionToSyncToChildren((IEnumerable<Permission>) this.GetNewValueOrDefault<IList<Permission>>(updatedEvent, EventsPropertyNames.Permissions, originalPermissionHolder.Permissions), newValueOrDefault, originalPermissionHolder.Id);
      ObjectFactory.Resolve<DynamicModulePermissionSyncronizer>().ResetChildPermissions(originalPermissionHolder, (IEnumerable<Guid>) toSyncToChildren.Select<Permission, Guid>((Func<Permission, Guid>) (p => p.Id)).ToArray<Guid>());
    }

    private T GetNewValueOrDefault<T>(
      IPropertyChangeDataEvent updatedEvent,
      string propertyName,
      T defaultValue)
    {
      return !updatedEvent.ChangedProperties.ContainsKey(propertyName) ? defaultValue : (T) updatedEvent.ChangedProperties[propertyName].NewValue;
    }

    /// <summary>
    /// Filters a set of permission to get only those that should be to synced to the permission children.
    /// </summary>
    /// <param name="permissions">The permissions to be filtered.</param>
    /// <param name="inheritsPermissions">The InheritsPermissions flag to be used when determining if the secured object inherits its permission or not.</param>
    /// <param name="securedObjectId">The id of the secured object for which we filter the permissions.</param>
    /// <returns>
    /// The filtered <paramref name="permissions" /> collection, containing only the <see cref="!:Permissions" /> to be synced to the inheriting children.
    /// </returns>
    private IEnumerable<Permission> GetPermissionToSyncToChildren(
      IEnumerable<Permission> permissions,
      bool inheritsPermissions,
      Guid securedObjectId)
    {
      InheritanceAction inheritanceAction = !inheritsPermissions ? InheritanceAction.Break : InheritanceAction.Restore;
      return this.FilterPermissionsByInheritanceAction(permissions, inheritanceAction, securedObjectId);
    }

    private IEnumerable<Permission> FilterPermissionsByInheritanceAction(
      IEnumerable<Permission> permissions,
      InheritanceAction inheritanceAction,
      Guid parentId)
    {
      switch (inheritanceAction)
      {
        case InheritanceAction.Break:
          permissions = permissions.Where<Permission>((Func<Permission, bool>) (p => p.ObjectId == parentId));
          break;
        case InheritanceAction.Restore:
          permissions = permissions.Where<Permission>((Func<Permission, bool>) (p => p.ObjectId != parentId));
          break;
        case InheritanceAction.SyncWithParent:
          permissions = permissions.Where<Permission>((Func<Permission, bool>) (p => p.ObjectId != parentId));
          break;
      }
      return permissions;
    }
  }
}
