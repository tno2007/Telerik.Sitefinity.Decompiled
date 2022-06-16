// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.ModuleBuilderBeforeCommitEventFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Builder.Events.BeforeCommitEvents.DynamicContentProviderEvents;
using Telerik.Sitefinity.DynamicModules.Builder.Events.BeforeCommitEvents.PermissionEvents;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// Factory for constructing events for dynamic content that are raised before commit transaction.
  /// </summary>
  internal class ModuleBuilderBeforeCommitEventFactory : 
    ModuleBuilderEventFactory,
    IModuleBuilderBeforeCommitEventFactory,
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
          return (PermissionDataEventBase) new PermissionCreatingEvent(permission, str, dataProviderName, currentUserId);
        case SecurityConstants.TransactionActionType.Updated:
          return (PermissionDataEventBase) new PermissionUpdatingEvent(permission, str, dataProviderName, currentUserId);
        case SecurityConstants.TransactionActionType.Deleted:
          return (PermissionDataEventBase) new PermissionDeletingEvent(permission, str, dataProviderName, currentUserId);
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
          return (DynamicContentProviderEventBase) new DynamicContentProviderCreatingEvent(dynamicContentProvider, str, dataProviderName, currentUserId);
        case SecurityConstants.TransactionActionType.Updated:
          return (DynamicContentProviderEventBase) new DynamicContentProviderUpdatingEvent(dynamicContentProvider, str, dataProviderName, currentUserId);
        case SecurityConstants.TransactionActionType.Deleted:
          return (DynamicContentProviderEventBase) new DynamicContentProviderDeletingEvent(dynamicContentProvider, str, dataProviderName, currentUserId);
        default:
          return new DynamicContentProviderEventBase(dynamicContentProvider.Id, dynamicContentProvider.GetType(), str, dataProviderName, currentUserId);
      }
    }
  }
}
