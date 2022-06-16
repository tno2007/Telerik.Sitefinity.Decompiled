// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.CommentsManagerExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  internal static class CommentsManagerExtensions
  {
    /// <summary>
    /// Returns a dictionary, mapping each of the given live content IDs to its comment count.
    /// </summary>
    /// <param name="commentsManager">The comments manager to be used.</param>
    /// <param name="liveIds">A colleciton of live content IDs.</param>
    /// <returns>a dictionary, mapping each of the given live content IDs to its comment count.</returns>
    /// <remarks>
    /// Use the <see cref="!:GetValueOrDefault" /> extension methods (defined in the <see cref="T:Telerik.Sitefinity.Utilities.Extensions" /> class)
    /// to look up the returned dictionary as it may even be <c>null</c>.
    /// </remarks>
    public static IDictionary<Guid, int> GetCommentCounts(
      this ICommentsManager commentsManager,
      IEnumerable<Guid> liveIds,
      bool dateFilteringEnabled = false,
      int batchSize = 50)
    {
      if (commentsManager == null)
        return (IDictionary<Guid, int>) null;
      if (!liveIds.Any<Guid>())
        return (IDictionary<Guid, int>) null;
      DateTime dateTime = DateTime.MinValue;
      if (dateFilteringEnabled)
      {
        GlobalCommentsSettings commentsSettings = Config.Get<CommentsConfig>().CommentsSettings;
        if (commentsSettings.HideCommentsAfterNumberOfDays)
          dateTime = DateTime.UtcNow - TimeSpan.FromDays((double) commentsSettings.NumberOfDaysToHideComments);
      }
      ICommentService commentsService = SystemManager.GetCommentsService();
      List<string> collection = new List<string>();
      collection.AddRange(liveIds.Select<Guid, string>((Func<Guid, string>) (id => ControlUtilities.GetLocalizedKey((object) id))));
      IDictionary<Guid, int> commentCounts;
      if (dateTime != DateTime.MinValue)
      {
        commentCounts = (IDictionary<Guid, int>) new Dictionary<Guid, int>();
        CommentFilter filter = new CommentFilter();
        filter.Status.Add("Published");
        filter.Take = new int?(1);
        filter.FromDate = new DateTime?(dateTime);
        foreach (string resolveKey in collection)
        {
          filter.ThreadKey.Clear();
          filter.ThreadKey.Add(resolveKey);
          int totalCount;
          commentsService.GetComments(filter, out totalCount);
          commentCounts.Add(CommentsManagerExtensions.ResolveItemId(resolveKey), totalCount);
        }
      }
      else
      {
        ThreadFilter filter = new ThreadFilter();
        filter.ThreadKey.AddRange((IEnumerable<string>) collection);
        commentCounts = (IDictionary<Guid, int>) commentsService.GetThreads(filter).ToDictionary<IThread, Guid, int>((Func<IThread, Guid>) (t => CommentsManagerExtensions.ResolveItemId(t.Key)), (Func<IThread, int>) (t => t.CommentsCount));
      }
      return commentCounts;
    }

    private static Guid ResolveItemId(string resolveKey)
    {
      Guid contentItemId;
      ControlUtilities.TryGetItemId(resolveKey, out contentItemId);
      return contentItemId;
    }
  }
}
