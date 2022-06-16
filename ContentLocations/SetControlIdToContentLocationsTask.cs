// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.SetControlIdToContentLocationsTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.ContentLocations
{
  internal class SetControlIdToContentLocationsTask : ScheduledTask
  {
    private const string UpgradeMsg = "Set ControlId to existing content locations";

    public SetControlIdToContentLocationsTask() => this.ExecuteTime = DateTime.UtcNow;

    public int NumberOfAttempts { get; set; }

    public override void SetCustomData(string customData) => this.NumberOfAttempts = int.Parse(customData);

    public override string GetCustomData() => this.NumberOfAttempts.ToString();

    public override void ExecuteTask()
    {
      --this.NumberOfAttempts;
      try
      {
        PageManager manager1 = PageManager.GetManager();
        ContentLocationsManager manager2 = ContentLocationsManager.GetManager();
        using (new ElevatedModeRegion((IManager) manager1))
        {
          IOrderedQueryable<PageData> source = manager2.GetLocations().Join((IEnumerable<PageData>) manager1.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => (int) p.Status == 2)), (Expression<Func<ContentLocationDataItem, Guid>>) (l => l.PageId), (Expression<Func<PageData, Guid>>) (item => item.NavigationNodeId), (l, item) => new
          {
            l = l,
            item = item
          }).Where(data => (Guid?) data.l.ControlId == new Guid?()).Select(data => data.item).OrderBy<PageData, DateTime>((Expression<Func<PageData, DateTime>>) (p => p.DateCreated));
          int count = 100;
          int num1 = source.Count<PageData>();
          int num2 = 0;
          if (num1 > 0)
            num2 = (int) Math.Ceiling((double) num1 / (double) count);
          for (int index = 0; index < num2; ++index)
            this.UpgradePageDataItems(source.Skip<PageData>(index * count).Take<PageData>(count), manager1);
        }
        Log.Write((object) string.Format("PASSED : {0}", (object) "Set ControlId to existing content locations"), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        this.LogError(ex, "Set ControlId to existing content locations");
      }
    }

    private void UpgradePageDataItems(IQueryable<PageData> pageItems, PageManager pageManager)
    {
      ContentLocationsTracker locationsTracker = new ContentLocationsTracker();
      locationsTracker.IgnoreItemStatus = true;
      foreach (PageData pageItem in (IEnumerable<PageData>) pageItems)
      {
        PageNode navigationNode;
        try
        {
          navigationNode = pageItem.NavigationNode;
          if (navigationNode == null)
            continue;
        }
        catch (Exception ex)
        {
          continue;
        }
        SiteRegion siteRegion = (SiteRegion) null;
        try
        {
          IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
          if (multisiteContext != null)
            siteRegion = new SiteRegion(multisiteContext.GetSiteBySiteMapRoot(navigationNode.RootNodeId));
          locationsTracker.Track((object) pageItem, (DataProviderBase) pageManager.Provider);
          locationsTracker.SaveChanges();
        }
        finally
        {
          siteRegion?.Dispose();
        }
      }
    }

    private void LogError(Exception err, string upgradeMsg)
    {
      Log.Write((object) string.Format("FAILED : {0} - {1}", (object) upgradeMsg, (object) err.Message), ConfigurationPolicy.UpgradeTrace);
      if (Exceptions.HandleException(err, ExceptionPolicyName.IgnoreExceptions))
        throw err;
      if (this.NumberOfAttempts <= 0)
        return;
      SchedulingManager manager = SchedulingManager.GetManager();
      SetControlIdToContentLocationsTask contentLocationsTask = new SetControlIdToContentLocationsTask();
      contentLocationsTask.Id = Guid.NewGuid();
      contentLocationsTask.ExecuteTime = DateTime.UtcNow.AddMinutes(1.0);
      SetControlIdToContentLocationsTask task = contentLocationsTask;
      task.NumberOfAttempts = this.NumberOfAttempts;
      manager.AddTask((ScheduledTask) task);
      manager.SaveChanges();
    }
  }
}
