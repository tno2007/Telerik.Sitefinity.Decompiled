// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.SchedulingSystemStatusRetreiver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Sitefinity.Dashboard;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Scheduling.Model;

namespace Telerik.Sitefinity.Scheduling
{
  internal class SchedulingSystemStatusRetreiver : ISystemStatusRetriever
  {
    public IEnumerable<DashboardSystemStatus> GetSystemStatus()
    {
      List<DashboardSystemStatus> systemStatus = new List<DashboardSystemStatus>();
      int num = SchedulingManager.GetManager().GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => (int) t.Status == 2)).Count<ScheduledTaskData>();
      if (num > 0)
      {
        string format = num == 1 ? Res.Get<SchedulingResources>().StatusWidgetDescriptionSingular : Res.Get<SchedulingResources>().StatusWidgetDescriptionPlural;
        systemStatus.Add(new DashboardSystemStatus()
        {
          Title = Res.Get<SchedulingResources>().StatusWidgetTitle,
          Description = string.Format(format, (object) num),
          CanFindSolution = false,
          Links = (IList<DashboardSystemStatusLink>) new List<DashboardSystemStatusLink>()
          {
            new DashboardSystemStatusLink()
            {
              Title = Res.Get<SchedulingResources>().StatusWidgetLinkLabel,
              Url = this.GetSchedulingPageUrl(),
              OpenInSameTab = true
            }
          }
        });
      }
      return (IEnumerable<DashboardSystemStatus>) systemStatus;
    }

    private string GetSchedulingPageUrl() => VirtualPathUtility.ToAbsolute("~/Sitefinity/Administration/Scheduled-tasks");
  }
}
