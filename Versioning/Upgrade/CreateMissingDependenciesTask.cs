// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Upgrade.CreateMissingDependenciesTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Versioning.Upgrade
{
  internal class CreateMissingDependenciesTask : ScheduledTask
  {
    private const string UpgradeMsg = "Create dependencies for existing MediaContent's changes";

    public int NumberOfAttempts { get; set; }

    public override void SetCustomData(string customData) => this.NumberOfAttempts = int.Parse(customData);

    public override string GetCustomData() => this.NumberOfAttempts.ToString();

    public override void ExecuteTask()
    {
      --this.NumberOfAttempts;
      try
      {
        string transactionName = "CreateMissingDependencies";
        int transactionNumber = 1;
        VersionManager versionManager = this.GetVersionManager(transactionName, transactionNumber);
        string[] mediaContentTypes = new string[3]
        {
          typeof (Image).FullName,
          typeof (Document).FullName,
          typeof (Video).FullName
        };
        for (IQueryable<IGrouping<Guid, Change>> source1 = this.ChangesWithoutDependenciesExpression(versionManager, mediaContentTypes); source1.Count<IGrouping<Guid, Change>>() > 0; source1 = this.ChangesWithoutDependenciesExpression(versionManager, mediaContentTypes))
        {
          IGrouping<Guid, Change> source2 = source1.First<IGrouping<Guid, Change>>();
          this.CreateDependenciesForChanges(versionManager, source2.Key, (IEnumerable<Change>) source2.ToList<Change>());
          versionManager.Provider.CommitTransaction();
          ++transactionNumber;
          versionManager = this.GetVersionManager(transactionName, transactionNumber);
        }
        Log.Write((object) string.Format("PASSED : {0}", (object) "Create dependencies for existing MediaContent's changes"), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        this.ProcessError(ex, "Create dependencies for existing MediaContent's changes");
      }
    }

    private IQueryable<IGrouping<Guid, Change>> ChangesWithoutDependenciesExpression(
      VersionManager versionManager,
      string[] mediaContentTypes)
    {
      return versionManager.Provider.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (c => mediaContentTypes.Contains<string>(c.Parent.TypeName) && c.Dependencies.Count == 0)).GroupBy<Change, Guid>((Expression<Func<Change, Guid>>) (c => c.Parent.Id));
    }

    private VersionManager GetVersionManager(
      string transactionName,
      int transactionNumber)
    {
      return VersionManager.GetManager((string) null, transactionName + (object) transactionNumber);
    }

    private void CreateDependenciesForChanges(
      VersionManager versionManager,
      Guid contentId,
      IEnumerable<Change> changes)
    {
      LibrariesManager manager = LibrariesManager.GetManager();
      IEnumerable<IDependentItem> source1 = (IEnumerable<IDependentItem>) null;
      try
      {
        source1 = ((IHasVersionDependency) manager.GetMediaItem(contentId)).GetDependencies();
      }
      catch (ItemNotFoundException ex)
      {
      }
      if (source1 == null || source1.Count<IDependentItem>() == 0)
      {
        versionManager.DeleteItem(contentId);
      }
      else
      {
        List<Dependency> source2 = new List<Dependency>();
        foreach (Change change1 in changes)
        {
          Change change = change1;
          IDependentItem dependentItem = source1.FirstOrDefault<IDependentItem>((Func<IDependentItem, bool>) (di => di.Culture == change.Culture));
          if (dependentItem == null && change.Culture == AppSettings.CurrentSettings.GetCultureLcid(CultureInfo.InvariantCulture))
            dependentItem = source1.FirstOrDefault<IDependentItem>((Func<IDependentItem, bool>) (di => di.Culture == AppSettings.CurrentSettings.GetCultureLcid(AppSettings.CurrentSettings.DefaultFrontendLanguage)));
          if (dependentItem == null)
            dependentItem = source1.First<IDependentItem>();
          Dependency dependency = source2.FirstOrDefault<Dependency>((Func<Dependency, bool>) (d => d.Key == dependentItem.Key && d.CleanUpTaskType == dependentItem.CleanUpTaskType.FullName));
          if (dependency == null)
          {
            dependency = versionManager.Provider.CreateDependency(dependentItem.Key, dependentItem.CleanUpTaskType);
            dependency.Data = dependentItem.GetData();
            source2.Add(dependency);
          }
          change.Dependencies.Add(dependency);
        }
      }
    }

    private void ProcessError(Exception err, string upgradeMsg)
    {
      if (this.NumberOfAttempts > 0)
      {
        SchedulingManager manager = SchedulingManager.GetManager();
        CreateMissingDependenciesTask task = new CreateMissingDependenciesTask();
        task.Id = Guid.NewGuid();
        task.NumberOfAttempts = this.NumberOfAttempts;
        task.ExecuteTime = DateTime.UtcNow.AddMinutes(1.0);
        manager.AddTask((ScheduledTask) task);
        manager.SaveChanges();
      }
      else
      {
        Log.Write((object) string.Format((IFormatProvider) CultureInfo.InvariantCulture, "FAILED : {0} - {1}", (object) upgradeMsg, (object) err.Message), ConfigurationPolicy.UpgradeTrace);
        if (Exceptions.HandleException(err, ExceptionPolicyName.IgnoreExceptions))
          throw err;
      }
    }
  }
}
