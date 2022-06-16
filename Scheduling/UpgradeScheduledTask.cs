// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.UpgradeScheduledTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Utilities.Json;

namespace Telerik.Sitefinity.Scheduling
{
  public abstract class UpgradeScheduledTask : ScheduledTask
  {
    public UpgradeScheduledTask() => this.ExecuteTime = DateTime.UtcNow;

    public int NumberOfAttempts { get; set; }

    public Version UpgradeFrom { get; set; }

    public abstract string UpgradeMessage { get; }

    public override void SetCustomData(string customData)
    {
      UpgradeScheduledTask.UpgradeScheduledTaskSettings scheduledTaskSettings = UpgradeScheduledTask.UpgradeScheduledTaskSettings.Parse(customData);
      this.NumberOfAttempts = scheduledTaskSettings.NumberOfAttempts;
      this.UpgradeFrom = scheduledTaskSettings.UpgradeFrom;
    }

    public override string GetCustomData() => new UpgradeScheduledTask.UpgradeScheduledTaskSettings()
    {
      NumberOfAttempts = this.NumberOfAttempts,
      UpgradeFrom = this.UpgradeFrom
    }.ToString();

    public override void ExecuteTask()
    {
      --this.NumberOfAttempts;
      try
      {
        this.Upgrade();
        Log.Write((object) string.Format("PASSED : {0}", (object) this.UpgradeMessage), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED : {0} - {1}", (object) this.UpgradeMessage, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
        if (this.NumberOfAttempts <= 0)
          return;
        SchedulingManager manager = SchedulingManager.GetManager();
        ScheduledTask taskInstance = this.CreateTaskInstance();
        if (taskInstance == null)
          return;
        manager.AddTask(taskInstance);
        manager.SaveChanges();
      }
    }

    protected abstract ScheduledTask CreateTaskInstance();

    protected abstract void Upgrade();

    [DataContract]
    private class UpgradeScheduledTaskSettings
    {
      [DataMember]
      public int NumberOfAttempts { get; set; }

      [DataMember]
      public Version UpgradeFrom { get; set; }

      public override string ToString() => this.ToJson<UpgradeScheduledTask.UpgradeScheduledTaskSettings>();

      public static UpgradeScheduledTask.UpgradeScheduledTaskSettings Parse(
        string data)
      {
        return JsonUtility.FromJson<UpgradeScheduledTask.UpgradeScheduledTaskSettings>(data);
      }
    }
  }
}
