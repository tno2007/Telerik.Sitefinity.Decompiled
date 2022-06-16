// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.BounceCheckTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Communication;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Utilities.Json;

namespace Telerik.Sitefinity.Modules.Newsletters.Data
{
  /// <summary>The scheduled task for checking bounced messages</summary>
  internal class BounceCheckTask : ScheduledTask
  {
    internal const string taskName = "BounceCheckTask";
    private BounceCheckTask.BounceCheckTaskSettings settings = new BounceCheckTask.BounceCheckTaskSettings();
    private static object lockObj = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.BounceCheckTask" /> class.
    /// </summary>
    public BounceCheckTask()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.BounceCheckTask" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="executeTime">The execute time.</param>
    /// <param name="retryMins">The retry interval in minutes.</param>
    /// <param name="retryCount">The retry count.</param>
    public BounceCheckTask(
      string providerName,
      DateTime executeTime,
      int retryMins,
      int retryCount)
    {
      this.settings.ProviderName = providerName;
      this.settings.RetryMinutes = retryMins;
      this.settings.RetryCount = retryCount;
      this.ExecuteTime = executeTime;
    }

    /// <summary>Identifier used for Task Factory</summary>
    public override string TaskName => nameof (BounceCheckTask);

    /// <summary>Executes the task.</summary>
    public override void ExecuteTask()
    {
      try
      {
        BounceChecker.CheckBouncedMessages(NewslettersManager.GetManager(this.settings.ProviderName));
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("Error while checking the bounced emails: {0}", (object) ex));
      }
      --this.settings.RetryCount;
      BounceCheckTask.ScheduleRun(this.settings);
    }

    /// <summary>
    /// Gets any custom data that the task needs persisted. The data should be serialized as a string.
    /// The <seealso cref="M:Telerik.Sitefinity.Modules.Newsletters.Data.BounceCheckTask.SetCustomData(System.String)" />should have implementation for deserialized the data
    /// </summary>
    /// <returns>string serialized custom task data</returns>
    public override string GetCustomData() => this.settings.ToString();

    /// <summary>
    /// Sets the custom data. This is used when reviving a task from a persistent storage to deserialize any custom stored data
    /// </summary>
    public override void SetCustomData(string customData) => this.settings = BounceCheckTask.BounceCheckTaskSettings.Parse(customData);

    /// <summary>Schedules a bounce check.</summary>
    /// <param name="providerName">The provider name.</param>
    public static void ScheduleBounceCheck(string providerName)
    {
      NewslettersConfig newslettersConfig = Config.Get<NewslettersConfig>();
      int collectionIntervalMinutes = newslettersConfig.BounceCollectionIntervalMinutes;
      int collectionRetryCount = newslettersConfig.BounceCollectionRetryCount;
      DateTime executeTime = DateTime.UtcNow.AddMinutes((double) collectionIntervalMinutes);
      lock (BounceCheckTask.lockObj)
      {
        SchedulingManager manager = SchedulingManager.GetManager();
        ScheduledTaskData scheduledTaskData = manager.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == nameof (BounceCheckTask))).AsEnumerable<ScheduledTaskData>().Where<ScheduledTaskData>((Func<ScheduledTaskData, bool>) (t => BounceCheckTask.BounceCheckTaskSettings.Parse(t.TaskData).ProviderName == providerName)).FirstOrDefault<ScheduledTaskData>();
        if (scheduledTaskData != null)
        {
          BounceCheckTask.BounceCheckTaskSettings checkTaskSettings = new BounceCheckTask.BounceCheckTaskSettings()
          {
            RetryCount = collectionRetryCount + 1,
            RetryMinutes = collectionIntervalMinutes,
            ProviderName = providerName
          };
          scheduledTaskData.TaskData = checkTaskSettings.ToString();
        }
        else
        {
          BounceCheckTask task = new BounceCheckTask(providerName, executeTime, collectionIntervalMinutes, collectionRetryCount);
          manager.AddTask((ScheduledTask) task);
        }
        manager.SaveChanges();
      }
    }

    private static void ScheduleRun(BounceCheckTask.BounceCheckTaskSettings settings)
    {
      if (settings.RetryCount < 0)
        return;
      DateTime executeTime = DateTime.UtcNow.AddMinutes((double) settings.RetryMinutes);
      BounceCheckTask task = new BounceCheckTask(settings.ProviderName, executeTime, settings.RetryMinutes, settings.RetryCount);
      SchedulingManager manager = SchedulingManager.GetManager();
      lock (BounceCheckTask.lockObj)
      {
        manager.AddTask((ScheduledTask) task);
        manager.SaveChanges();
      }
    }

    [DataContract]
    private class BounceCheckTaskSettings
    {
      [DataMember]
      public string ProviderName { get; set; }

      [DataMember]
      public int RetryMinutes { get; set; }

      [DataMember]
      public int RetryCount { get; set; }

      public override string ToString() => this.ToJson<BounceCheckTask.BounceCheckTaskSettings>();

      public static BounceCheckTask.BounceCheckTaskSettings Parse(string data) => JsonUtility.FromJson<BounceCheckTask.BounceCheckTaskSettings>(data);
    }
  }
}
