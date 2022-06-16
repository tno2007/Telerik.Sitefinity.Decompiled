// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.ModuleBuilderAfterCommitEventFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.DynamicContentProviderEvents;
using Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.PermissionEvents;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// Factory for constructing events for module builder data items that are raised after commit transaction.
  /// </summary>
  internal class ModuleBuilderAfterCommitEventFactory : 
    ModuleBuilderEventFactory,
    IModuleBuilderAfterCommitEventFactory,
    IModuleBuilderEventFactory
  {
    public override PermissionDataEventBase CreatePermissionEventByAction(
      Permission permission,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName)
    {
      string str = actionType.ToString();
      Guid currentUserId = ClaimsManager.GetCurrentUserId();
      switch (actionType)
      {
        case SecurityConstants.TransactionActionType.New:
          return (PermissionDataEventBase) new PermissionCreatedEvent(permission, str, dataProviderName, currentUserId, permission.LastModified);
        case SecurityConstants.TransactionActionType.Updated:
          return (PermissionDataEventBase) new PermissionUpdatedEvent(permission, str, dataProviderName, currentUserId, permission.LastModified);
        case SecurityConstants.TransactionActionType.Deleted:
          return (PermissionDataEventBase) new PermissionDeletedEvent(permission, str, dataProviderName, currentUserId, permission.LastModified);
        default:
          return new PermissionDataEventBase(permission.Id, permission.GetType(), str, dataProviderName, currentUserId);
      }
    }

    public override DynamicContentProviderEventBase CreateDynamicContentProviderEventByAction(
      DynamicContentProvider dynamicContentProvider,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName)
    {
      string str = actionType.ToString();
      Guid currentUserId = ClaimsManager.GetCurrentUserId();
      switch (actionType)
      {
        case SecurityConstants.TransactionActionType.New:
          return (DynamicContentProviderEventBase) new DynamicContentProviderCreatedEvent(dynamicContentProvider, str, dataProviderName, currentUserId, dynamicContentProvider.LastModified);
        case SecurityConstants.TransactionActionType.Updated:
          return (DynamicContentProviderEventBase) new DynamicContentProviderUpdatedEvent(dynamicContentProvider, str, dataProviderName, currentUserId, dynamicContentProvider.LastModified);
        case SecurityConstants.TransactionActionType.Deleted:
          return (DynamicContentProviderEventBase) new DynamicContentProviderDeletedEvent(dynamicContentProvider, str, dataProviderName, currentUserId, dynamicContentProvider.LastModified);
        default:
          return new DynamicContentProviderEventBase(dynamicContentProvider.Id, dynamicContentProvider.GetType(), str, dataProviderName, currentUserId);
      }
    }
  }
}
