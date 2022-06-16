// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.GenericContent.UserActionHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Data.GenericContent
{
  public static class UserActionHelper
  {
    /// <summary>
    /// Get a query of actions that associate a data item with the currently logged user
    /// </summary>
    /// <param name="dataItemId">Id of the data item</param>
    /// <returns>Query</returns>
    public static IQueryable<UserAction> GetCurrentUserActions(Guid dataItemId)
    {
      SecurityManager manager = SecurityManager.GetManager();
      Guid userId = SecurityManager.GetCurrentUserId();
      DateTime now = DateTime.UtcNow;
      return manager.GetUserActions().Where<UserAction>((Expression<Func<UserAction, bool>>) (action => action.DataItemId == dataItemId && action.UserId == userId && action.ExiprationDate > now));
    }

    /// <summary>
    /// Get a query of named actions that associate a data item the currently logged user
    /// </summary>
    /// <param name="dataItemId">Id of the data item</param>
    /// <param name="actionName">Name of the action</param>
    /// <returns>Query</returns>
    public static IQueryable<UserAction> GetCurrentUserActions(
      Guid dataItemId,
      string actionName)
    {
      return UserActionHelper.GetCurrentUserActions(dataItemId).Where<UserAction>((Expression<Func<UserAction, bool>>) (a => a.ActionName == actionName));
    }

    /// <summary>
    /// Get a query of actions that associate data item with the current visitor's IP address
    /// </summary>
    /// <param name="dataItemId">Id of the data item</param>
    /// <returns>Query</returns>
    public static IQueryable<UserAction> GetCurrentIpAddressActions(
      Guid dataItemId)
    {
      string userIp = RatingHelper.GetUserIp();
      DateTime now = DateTime.UtcNow;
      return SecurityManager.GetManager().GetUserActions().Where<UserAction>((Expression<Func<UserAction, bool>>) (action => action.DataItemId == dataItemId && action.Context == userIp && action.ExiprationDate > now));
    }

    /// <summary>
    /// Get a query of named actions that associate data item with the current visitor's IP address
    /// </summary>
    /// <param name="dataItemId">Id of the data item</param>
    /// <param name="actionName">Name of the action</param>
    /// <returns>Query</returns>
    public static IQueryable<UserAction> GetCurrentIpAddressActions(
      Guid dataItemId,
      string actionName)
    {
      return UserActionHelper.GetCurrentIpAddressActions(dataItemId).Where<UserAction>((Expression<Func<UserAction, bool>>) (a => a.ActionName == actionName));
    }

    /// <summary>
    /// Get a query of actions that associate data item with the logged user's identity and ip address
    /// </summary>
    /// <param name="dataItemId">Id of the data item</param>
    /// <returns>Query</returns>
    public static IQueryable<UserAction> GetUniqueActions(Guid dataItemId)
    {
      Guid userId = SecurityManager.GetCurrentUserId();
      SecurityManager manager = SecurityManager.GetManager();
      string ip = RatingHelper.GetUserIp();
      DateTime now = DateTime.UtcNow;
      return manager.GetUserActions().Where<UserAction>((Expression<Func<UserAction, bool>>) (action => action.DataItemId == dataItemId && action.UserId == userId && action.Context == ip && action.ExiprationDate > now));
    }

    /// <summary>
    /// Get a query of named actions that associate data item with the logged user's identity and ip address
    /// </summary>
    /// <param name="dataItemId">Id of the data item</param>
    /// <param name="actionName">Name of the action</param>
    /// <returns>Query</returns>
    public static IQueryable<UserAction> GetUniqueActions(
      Guid dataItemId,
      string actionName)
    {
      return UserActionHelper.GetUniqueActions(dataItemId).Where<UserAction>((Expression<Func<UserAction, bool>>) (a => a.ActionName == actionName));
    }

    /// <summary>Get a query of all expired user actions</summary>
    /// <returns>Query</returns>
    public static IQueryable<UserAction> GetAllExpiredActions()
    {
      DateTime now = DateTime.UtcNow;
      return SecurityManager.GetManager().GetUserActions().Where<UserAction>((Expression<Func<UserAction, bool>>) (expired => expired.ExiprationDate <= now));
    }

    /// <summary>
    /// Record user action, possibly deleting the expired user actions with that name. Uses currently lo
    /// </summary>
    /// <param name="dataItemId">Id of the data item associated witht his action</param>
    /// <param name="actionName">Name of the action</param>
    /// <param name="data">Optional information about the action.</param>
    /// <param name="timeOut">Timeout. Used for setting expiration date of new user action.</param>
    /// <param name="deleteExpired">Set to <c>true</c> to delete expired actions with <paramref name="actionName" /></param>
    public static void RecordAction(
      Guid dataItemId,
      string actionName,
      string data,
      TimeSpan timeOut,
      bool deleteExpired)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      Guid userId = currentIdentity.UserId;
      string userIp = RatingHelper.GetUserIp();
      SecurityManager manager = SecurityManager.GetManager();
      if (deleteExpired)
      {
        IQueryable<UserAction> allExpiredActions = UserActionHelper.GetAllExpiredActions();
        Expression<Func<UserAction, bool>> predicate = (Expression<Func<UserAction, bool>>) (expired => expired.ActionName == actionName);
        foreach (UserAction action in (IEnumerable<UserAction>) allExpiredActions.Where<UserAction>(predicate))
          manager.DeleteUserAction(action);
      }
      UserAction userAction = UserActionHelper.GetCurrentUserActions(dataItemId, actionName).FirstOrDefault<UserAction>();
      if (userAction == null)
      {
        userAction = manager.CreateUserAction();
        userAction.ExiprationDate = DateTime.UtcNow.Add(timeOut);
        userAction.ActionName = actionName;
      }
      userAction.UserId = !currentIdentity.IsAuthenticated ? Guid.Empty : userId;
      userAction.Context = userIp;
      userAction.DataItemId = dataItemId;
      userAction.Data = data;
      manager.SaveChanges();
    }
  }
}
