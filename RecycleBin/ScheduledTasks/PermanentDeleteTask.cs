// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.RecycleBin.ScheduledTasks
{
  /// <summary>
  /// Defines the object to be used as scheduled task for permanent deletion of a <see cref="!:IDataItem" /> object
  /// marked as deleted by a given <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
  /// </summary>
  public abstract class PermanentDeleteTask : ScheduledTask
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> class.
    /// </summary>
    public PermanentDeleteTask()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> class.
    /// </summary>
    /// <param name="recycleBinDataItem">The <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.</param>
    public PermanentDeleteTask(IRecycleBinDataItem recycleBinDataItem)
    {
      this.RecycleBinDataItemProviderName = ObjectFactory.Resolve<IProviderNameResolver>().GetProviderName(recycleBinDataItem.Provider);
      this.RecycleBinDataItemId = recycleBinDataItem.Id;
      this.Key = recycleBinDataItem.Id.ToString();
      this.Title = recycleBinDataItem.DeletedItemTitle;
    }

    /// <summary>
    /// Gets or sets the Id of the related <see cref="!:RecycleBinDataItem" />.
    /// </summary>
    public virtual Guid RecycleBinDataItemId { get; set; }

    /// <summary>
    /// Gets or sets the provider name of the related <see cref="!:RecycleBinDataItem" />.
    /// </summary>
    public virtual string RecycleBinDataItemProviderName { get; set; }

    /// <summary>
    /// Gets any permanent delete task data that the task needs persisted. The data should be serialized as a string.
    /// The <seealso cref="M:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask.SetCustomData(System.String)" /> should have implementation for deserializing the permanent delete task data.
    /// </summary>
    /// <returns>String containing the serialized permanent delete task data.</returns>
    public override string GetCustomData() => this.RecycleBinDataItemId.ToString() + ";" + this.RecycleBinDataItemProviderName;

    /// <summary>
    /// Sets the permanent delete task data. This is used when reviving a task from a persistent storage to deserialize the permanent delete task stored data.
    /// </summary>
    /// <param name="customData">The stored serialized permanent delete task data.</param>
    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(';');
      this.RecycleBinDataItemId = new Guid(strArray[0]);
      this.RecycleBinDataItemProviderName = strArray[1];
    }
  }
}
