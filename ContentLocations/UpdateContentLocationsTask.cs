// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.UpdateContentLocationsTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Scheduling;

namespace Telerik.Sitefinity.ContentLocations
{
  internal class UpdateContentLocationsTask : ScheduledTask
  {
    public UpdateContentLocationsTask() => this.ExecuteTime = DateTime.UtcNow;

    public int NumberOfAttempts { get; set; }

    public override void SetCustomData(string customData) => this.NumberOfAttempts = int.Parse(customData);

    public override string GetCustomData() => this.NumberOfAttempts.ToString();

    public override void ExecuteTask()
    {
      string str = "Updating content locations service";
      --this.NumberOfAttempts;
      try
      {
        ContentLocationsTracker locationsTracker = new ContentLocationsTracker();
        locationsTracker.IgnoreItemStatus = true;
        PageManager manager = PageManager.GetManager();
        using (new ElevatedModeRegion((IManager) manager))
        {
          IOrderedQueryable<PageData> source = manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => (int) p.Status == 2)).OrderBy<PageData, DateTime>((Expression<Func<PageData, DateTime>>) (p => p.DateCreated));
          int count = 100;
          int num1 = source.Count<PageData>();
          int num2 = 0;
          if (num1 > 0)
            num2 = (int) Math.Ceiling((double) num1 / (double) count);
          for (int index = 0; index < num2; ++index)
          {
            foreach (PageData pageData in (IEnumerable<PageData>) source.Skip<PageData>(index * count).Take<PageData>(count))
            {
              locationsTracker.Track((object) pageData, (DataProviderBase) manager.Provider);
              locationsTracker.SaveChanges();
            }
          }
        }
        Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED : {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
        if (this.NumberOfAttempts <= 0)
          return;
        SchedulingManager manager = SchedulingManager.GetManager();
        UpdateContentLocationsTask contentLocationsTask = new UpdateContentLocationsTask();
        contentLocationsTask.Id = Guid.NewGuid();
        contentLocationsTask.ExecuteTime = DateTime.UtcNow.AddMinutes(1.0);
        UpdateContentLocationsTask task = contentLocationsTask;
        task.NumberOfAttempts = this.NumberOfAttempts;
        manager.AddTask((ScheduledTask) task);
        manager.SaveChanges();
      }
    }
  }
}
