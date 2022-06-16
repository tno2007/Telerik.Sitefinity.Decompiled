// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.CommentsStatisticSupport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;

namespace Telerik.Sitefinity.Modules.Comments
{
  internal class CommentsStatisticSupport : IContentStatisticSupport
  {
    public bool IsReusable => false;

    public IEnumerable<IStatisticSupportTypeInfo> GetTypeInfos(
      string moduleName = null)
    {
      return (IEnumerable<IStatisticSupportTypeInfo>) new List<StatisticSupportTypeInfo>()
      {
        new StatisticSupportTypeInfo(typeof (IComment), new string[1]
        {
          "Count"
        })
        {
          LandingPages = (IEnumerable<StatisticLandingPageInfo>) new StatisticLandingPageInfo[1]
          {
            new StatisticLandingPageInfo(CommentsModule.CommentsPageId)
          }
        }
      };
    }

    public string GetDefaultProviderName(string moduleName = null) => (string) null;

    public IEnumerable<string> GetProviderNames(string moduleName = null) => (IEnumerable<string>) null;

    public StatisticResult GetStatistic(
      Type type,
      string statisticKind,
      string provider,
      string filter = null)
    {
      if (!statisticKind.Equals("Count") || !type.Equals(typeof (IComment)))
        return (StatisticResult) null;
      ICommentService commentsService = SystemManager.GetCommentsService();
      CommentFilter commentFilter = new CommentFilter();
      commentFilter.Take = new int?(1);
      CommentFilter filter1 = commentFilter;
      int num;
      ref int local = ref num;
      commentsService.GetComments(filter1, out local);
      return new StatisticResult()
      {
        Kind = "Count",
        Value = (object) num
      };
    }
  }
}
