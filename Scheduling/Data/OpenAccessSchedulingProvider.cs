// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Data.OpenAccessSchedulingProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Scheduling.Data
{
  /// <summary>OpenAccess implementation fot the scheduling provider</summary>
  [ContentProviderDecorator(typeof (OpenAccessContentDecorator))]
  public class OpenAccessSchedulingProvider : 
    SchedulingDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>Create a new task data with random ID</summary>
    /// <returns>Newly created task data</returns>
    public override ScheduledTaskData CreateTaskData() => this.CreateTaskData(this.GetNewGuid());

    /// <summary>Create a new task data with specific ID</summary>
    /// <param name="id">Primary key</param>
    /// <returns>Newly created task data</returns>
    public override ScheduledTaskData CreateTaskData(Guid id)
    {
      ScheduledTaskData taskData = new ScheduledTaskData();
      taskData.Id = id;
      this.SetupDataItemForCreation((IDataItem) taskData);
      return taskData;
    }

    /// <summary>Search task data by primary key</summary>
    /// <param name="id">Primary key</param>
    /// <returns>Task data with primary key equal to <paramref name="id" /></returns>
    /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">
    /// When no task data is found by <paramref name="id" />
    /// </exception>
    public override ScheduledTaskData GetTaskData(Guid id) => this.GetObjectById<ScheduledTaskData>(id);

    /// <summary>
    /// Get a query of all task data items stored by the current provider
    /// </summary>
    /// <returns>All task data items stored by the current provider</returns>
    public override IQueryable<ScheduledTaskData> GetTaskData()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<ScheduledTaskData>((DataProviderBase) this).Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (taskData => taskData.ApplicationName == appName));
    }

    /// <summary>
    /// Remove a task data item from the current transaction that will be deleted when the transaction is commited
    /// </summary>
    /// <param name="task">Task data to delete</param>
    public override void DeleteTaskData(ScheduledTaskData task) => this.GetContext().Remove((object) task);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new SchedulingMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <inheritdoc />
    internal override Lock CreateLock(string lockId)
    {
      Lock entity = new Lock(lockId);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override IQueryable<Lock> GetLocks() => this.GetContext().GetAll<Lock>();

    /// <inheritdoc />
    internal override void DeleteLock(Lock obj) => this.GetContext().Remove((object) obj);

    /// <summary>
    /// Sets up IDataItem.{ApplicationName, Provider and Transaction} and adds it to the scope
    /// </summary>
    /// <param name="item">Data item to set up</param>
    protected virtual void SetupDataItemForCreation(IDataItem item)
    {
      item.ApplicationName = this.ApplicationName;
      item.Provider = (object) this;
      item.Transaction = (object) this.GetContext();
      this.GetContext().Add((object) item);
    }

    /// <summary>Get data item by id</summary>
    /// <typeparam name="T">IDataItem</typeparam>
    /// <param name="id">Primary key</param>
    /// <returns>Item from database</returns>
    /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">When not found or not from this provider</exception>
    protected virtual T GetObjectById<T>(Guid id) where T : IDataItem
    {
      T itemById = this.GetContext().GetItemById<T>(id.ToString());
      if (itemById.ApplicationName != this.ApplicationName)
        throw new ItemNotFoundException();
      itemById.Provider = (object) this;
      itemById.Transaction = (object) this.GetContext();
      return itemById;
    }
  }
}
