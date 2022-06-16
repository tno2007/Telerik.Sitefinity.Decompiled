// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.GenericContent.RatingHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration.ContentView.Plugins;

namespace Telerik.Sitefinity.Data.GenericContent
{
  /// <summary>Helper functions for rating (voting)</summary>
  public static class RatingHelper
  {
    /// <summary>Name of the rating action</summary>
    /// <value>ContentRated</value>
    public const string RateActionName = "ContentRated";

    /// <summary>Rate with current user</summary>
    /// <param name="dataItemId">Id of the data item to rate</param>
    /// <param name="rating">Rating to give</param>
    public static void RateWithCurrentUser(Guid dataItemId, Decimal rating) => UserActionHelper.RecordAction(dataItemId, "ContentRated", rating.ToString(), Config.Get<ContentPluginsConfig>().VoteTimeout, true);

    /// <summary>Get rating for currently logged on user</summary>
    /// <param name="dataItemId">ID of the data item that was rated</param>
    /// <returns>Returns user rating or -1 if user hasn'r rated yet.</returns>
    /// <exception cref="T:System.FormatException">If rating value cannot be converted to <see cref="T:System.Decimal" /></exception>
    public static Decimal GetCurrentUserRating(Guid dataItemId)
    {
      SecurityManager manager = SecurityManager.GetManager();
      DateTime now = DateTime.UtcNow;
      RatingHelper.GetUserIp();
      Guid userId = SecurityManager.GetCurrentUserId();
      UserAction userAction = manager.GetUserActions().Where<UserAction>((Expression<Func<UserAction, bool>>) (a => a.ExiprationDate >= now && a.ActionName == "ContentRated" && a.DataItemId == dataItemId)).FirstOrDefault<UserAction>((Expression<Func<UserAction, bool>>) (a => a.UserId == userId));
      Decimal result;
      return userAction != null && Decimal.TryParse(userAction.Data, out result) ? result : -1.0M;
    }

    /// <summary>
    /// Determines whether the current user can vote by their identity or ip address
    /// </summary>
    /// <param name="dataItemId">The data item pageId.</param>
    /// <param name="mode">Determines how a user can vote</param>
    /// <returns>
    /// 	<c>true</c> if this the current user can vote by their identity or ip address; otherwise, <c>false</c>.
    /// </returns>
    public static bool CanUserRate(Guid dataItemId)
    {
      ContentPluginsConfig config = Config.Get<ContentPluginsConfig>();
      return (!config.VoteBlockByUserId || !RatingHelper.HasCurrentUserVoted(dataItemId, config)) && (!config.VoteBlockByIpAddress || !RatingHelper.HasCurrentIpBeenUsedForVoting(dataItemId, config));
    }

    /// <summary>
    /// Determines whether the current request IP has been used for voting
    /// </summary>
    /// <param name="dataItemId">Id of the content item.</param>
    /// <param name="config">Plugin configuration settings</param>
    /// <returns>
    /// 	<c>true</c> if the current request IP has been used for voting; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasCurrentIpBeenUsedForVoting(Guid dataItemId, ContentPluginsConfig config)
    {
      if (!config.CanUsersVoteOnlyOnce)
        return false;
      if (SystemManager.CurrentHttpContext == null)
        return true;
      string ipAddress = RatingHelper.GetUserIp();
      if (string.IsNullOrEmpty(ipAddress))
        return true;
      string actionName = "ContentRated";
      DateTime now = DateTime.UtcNow;
      return SecurityManager.GetManager().GetUserActions().FirstOrDefault<UserAction>((Expression<Func<UserAction, bool>>) (a => a.DataItemId == dataItemId && a.Context == ipAddress && a.ActionName == actionName && a.ExiprationDate > now)) != null;
    }

    /// <summary>
    /// Determines whether the currently logged user has voted
    /// </summary>
    /// <param name="dataItemId">Id of the content item.</param>
    /// <param name="config">Plugin configuration settings</param>
    /// <returns>
    /// 	<c>true</c> if the currently logged user has voted; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasCurrentUserVoted(Guid dataItemId, ContentPluginsConfig config)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity == null || !currentIdentity.IsAuthenticated)
        return true;
      if (!config.CanUsersVoteOnlyOnce)
        return false;
      Guid userId = currentIdentity.UserId;
      SecurityManager manager = SecurityManager.GetManager();
      string actionName = "ContentRated";
      DateTime now = DateTime.UtcNow;
      return manager.GetUserActions().FirstOrDefault<UserAction>((Expression<Func<UserAction, bool>>) (a => a.DataItemId == dataItemId && a.UserId == userId && a.ActionName == actionName && a.ExiprationDate > now)) != null;
    }

    /// <summary>
    /// Deletes all votes for the current user, be it by pageId or ip
    /// </summary>
    /// <param name="itemId">ID of the item that was rated.</param>
    public static void DeleteForCurrentUser(Guid itemId)
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      string currentUserIp = RatingHelper.GetUserIp();
      SecurityManager manager = SecurityManager.GetManager();
      IQueryable<UserAction> userActions = manager.GetUserActions();
      Expression<Func<UserAction, bool>> predicate = (Expression<Func<UserAction, bool>>) (a => a.DataItemId == itemId && a.ActionName == "ContentRated" && (a.UserId == currentUserId || a.Context == currentUserIp));
      foreach (UserAction action in (IEnumerable<UserAction>) userActions.Where<UserAction>(predicate))
        manager.DeleteUserAction(action);
      manager.SaveChanges();
    }

    /// <summary>
    /// Gets the user ip, using HTTP_X_FORWARDED_FOR and REMOTE_ADDR server variables
    /// </summary>
    /// <returns>User IP address, as reported by the browser.</returns>
    public static string GetUserIp()
    {
      string serverVariable = SystemManager.CurrentHttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
      if (string.IsNullOrEmpty(serverVariable))
        serverVariable = SystemManager.CurrentHttpContext.Request.ServerVariables["REMOTE_ADDR"];
      return serverVariable;
    }

    /// <summary>Get message for the subtitle</summary>
    /// <param name="ratedItem">Item to get info from</param>
    /// <returns>Localized message for average rating.</returns>
    public static string GetSubtitleMessage(Content ratedItem) => ratedItem == null ? string.Empty : (ratedItem.VotesCount <= 1U ? (ratedItem.VotesCount != 1U ? Res.Get<ContentViewPluginMessages>().NobodyHasVotedSoFar : string.Format(Res.Get<ContentViewPluginMessages>().AverageXOutOfOnePerson, (object) ratedItem.VotesSum)) : string.Format(Res.Get<ContentViewPluginMessages>().AverageXOutOfXPeople, (object) (ratedItem.VotesSum / (Decimal) ratedItem.VotesCount), (object) ratedItem.VotesCount));
  }
}
