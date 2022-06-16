// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.ScheduledTasks.IPermanentDeleteTaskService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.RecycleBin.ScheduledTasks
{
  /// <summary>
  /// A service for managing <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> objects which are used as scheduled tasks for permanent deletion
  /// of a <see cref="!:IDataItem" /> objects marked as deleted.
  /// </summary>
  public interface IPermanentDeleteTaskService
  {
    /// <summary>
    /// Creates a <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> to be used as scheduled task for permanent deletion
    /// of a <see cref="!:IDataItem" /> object marked as deleted by a given <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
    /// </summary>
    /// <param name="recycleBinDataItem">The <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> object for which we constructed a <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /></param>
    /// <param name="providerName">Name of the scheduled task provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>
    /// The GUID of the <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> that was constructed.
    /// </returns>
    Guid CreateTask(
      IRecycleBinDataItem recycleBinDataItem,
      string providerName = null,
      string transactionName = null);

    /// <summary>
    /// Creates <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> objects to be used as scheduled task for permanent deletion
    /// for all <see cref="!:IDataItem" /> object marked as deleted by <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
    /// </summary>
    /// <param name="recycleBinItemProviderName">Name of the recycle bin item provider.</param>
    /// <param name="scheduledTaskProviderName">Name of the scheduled task provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    void CreateTasksForAllRecycleBinItems(
      string recycleBinItemProviderName = null,
      string scheduledTaskProviderName = null,
      string transactionName = null);

    /// <summary>
    /// Deletes the permanent delete task related to a given <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
    /// </summary>
    /// <param name="recycleBinDataItem">The <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> to which the scheduled task is related.</param>
    /// <param name="providerName">Name of the scheduled task provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    void DeleteTask(
      IRecycleBinDataItem recycleBinDataItem,
      string providerName = null,
      string transactionName = null);

    /// <summary>
    /// Deletes all the permanent delete tasks related to <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> objects.
    /// </summary>
    /// <param name="providerName">Name of the scheduled task provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    void DeleteAllTasks(string providerName = null, string transactionName = null);
  }
}
