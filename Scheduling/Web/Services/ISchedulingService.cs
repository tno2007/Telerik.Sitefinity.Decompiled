// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Web.Services.ISchedulingService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;

namespace Telerik.Sitefinity.Scheduling.Web.Services
{
  /// <summary>
  /// The WCF web service interface for scheduling tasks management.
  /// </summary>
  [ServiceContract]
  public interface ISchedulingService
  {
    /// <summary>Gets the task progress.</summary>
    /// <param name="taskId">The task id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{taskId}/progress?provider={providerName}")]
    WcfScheduledTask GetTaskProgress(string taskId, string providerName);

    /// <summary>Gets task progress by task name</summary>
    /// <param name="taskName"></param>
    /// <param name="providerName"></param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/taskName/{taskName}/progress?provider={providerName}")]
    WcfScheduledTask GetTaskProgressByName(string taskName, string providerName);

    /// <summary>Manages the task.</summary>
    /// <param name="taskId">The task id.</param>
    /// <param name="taskCommand">The task command.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{taskId}/manage?command={taskCommand}&provider={providerName}")]
    void ManageTask(string taskId, SchedulingTaskCommand taskCommand, string providerName);
  }
}
