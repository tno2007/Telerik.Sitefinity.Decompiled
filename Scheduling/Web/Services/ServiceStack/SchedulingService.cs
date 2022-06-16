// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack.SchedulingService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack.Dto;
using Telerik.Sitefinity.Services.ServiceStack.Filters;

namespace Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack
{
  /// <summary>SchedulingService service</summary>
  /// <seealso cref="!:ServiceStack.Service" />
  [RequestBackendAuthenticationFilter(true)]
  internal class SchedulingService : Service
  {
    private const int DefaultPageSize = 50;

    public GetScheduledTasksResponse Get(GetScheduledTasks requestInfo)
    {
      int count = requestInfo.Take == 0 ? 50 : requestInfo.Take;
      IQueryable<ScheduledTaskData> queryable = this.ApplySearchFilter(SchedulingManager.GetManager().GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => !x.IsSystem)), requestInfo.SearchTerm, requestInfo.FilterBy);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      List<ScheduledTaskViewModel> list = this.SortTasks(queryable, requestInfo.OrderBy).Skip<ScheduledTaskData>(requestInfo.Skip).Take<ScheduledTaskData>(count).Select<ScheduledTaskData, ScheduledTaskViewModel>(Expression.Lambda<Func<ScheduledTaskData, ScheduledTaskViewModel>>((Expression) Expression.MemberInit(Expression.New(typeof (ScheduledTaskViewModel)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ScheduledTaskViewModel.set_TaskId)), )))); //unable to render the statement
      return new GetScheduledTasksResponse()
      {
        Items = (IEnumerable<ScheduledTaskViewModel>) list,
        TotalCount = queryable.Count<ScheduledTaskData>()
      };
    }

    public void Put(ManageScheduleTask requestInfo)
    {
      Telerik.Sitefinity.Scheduling.Web.Services.SchedulingService schedulingService = new Telerik.Sitefinity.Scheduling.Web.Services.SchedulingService();
      if (!string.IsNullOrEmpty(requestInfo.TaskId))
      {
        Guid taskId = Guid.Parse(requestInfo.TaskId);
        SchedulingManager manager = SchedulingManager.GetManager();
        ScheduledTaskData taskData = manager.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => x.Id == taskId)).FirstOrDefault<ScheduledTaskData>();
        if (taskData != null && requestInfo.LastModified == taskData.LastModified)
        {
          schedulingService.ManageTaskInternal(requestInfo.Operation, manager, taskData);
          return;
        }
      }
      if (this.Response == null)
        return;
      this.Response.StatusCode = 400;
    }

    public IEnumerable<GetFilterByResponse> Get(
      GetFilterByRequest requestInfo)
    {
      IQueryable<ScheduledTaskData> source = SchedulingManager.GetManager().GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => !x.IsSystem));
      Dictionary<int, int> dictionary = source.GroupBy<ScheduledTaskData, int>((Expression<Func<ScheduledTaskData, int>>) (t => (int) t.Status)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<ScheduledTaskData>()
      }).ToDictionary(t => t.Key, t => t.Count);
      List<GetFilterByResponse> filterByResponseList = new List<GetFilterByResponse>();
      filterByResponseList.Add(new GetFilterByResponse()
      {
        Id = 0,
        Name = Res.Get<SchedulingResources>().AllTasksLabel,
        TasksCount = dictionary.Sum<KeyValuePair<int, int>>((Func<KeyValuePair<int, int>, int>) (t => t.Value))
      });
      filterByResponseList.Add(new GetFilterByResponse()
      {
        Id = 1,
        Name = Res.Get<SchedulingResources>().PеndingLabel,
        TasksCount = this.GetCountByStatus(TaskStatus.Pending, dictionary)
      });
      filterByResponseList.Add(new GetFilterByResponse()
      {
        Id = 2,
        Name = Res.Get<SchedulingResources>().StartedLabel,
        TasksCount = this.GetCountByStatus(TaskStatus.Started, dictionary)
      });
      filterByResponseList.Add(new GetFilterByResponse()
      {
        Id = 3,
        Name = Res.Get<SchedulingResources>().StoppedLabel,
        TasksCount = this.GetCountByStatus(TaskStatus.Stopped, dictionary)
      });
      filterByResponseList.Add(new GetFilterByResponse()
      {
        Id = 4,
        Name = Res.Get<SchedulingResources>().FailedLabel,
        TasksCount = this.GetCountByStatus(TaskStatus.Failed, dictionary)
      });
      int num = source.Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => !string.IsNullOrEmpty(t.ScheduleData))).Count<ScheduledTaskData>();
      filterByResponseList.Add(new GetFilterByResponse()
      {
        Id = 5,
        Name = Res.Get<SchedulingResources>().RecurringLabel,
        TasksCount = num
      });
      return (IEnumerable<GetFilterByResponse>) filterByResponseList;
    }

    private int GetCountByStatus(TaskStatus taskStatus, Dictionary<int, int> allScheduledTasks)
    {
      int countByStatus = 0;
      allScheduledTasks.TryGetValue((int) taskStatus, out countByStatus);
      return countByStatus;
    }

    private IQueryable<ScheduledTaskData> ApplySearchFilter(
      IQueryable<ScheduledTaskData> allScheduledTasks,
      string searchTerm,
      FilterByType filterBy)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SchedulingService.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new SchedulingService.\u003C\u003Ec__DisplayClass4_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.searchTerm = searchTerm;
      // ISSUE: reference to a compiler-generated field
      if (string.IsNullOrEmpty(cDisplayClass40.searchTerm) || filterBy != FilterByType.All)
        return this.FilterTasksByStatus(allScheduledTasks, filterBy);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      return allScheduledTasks.Where<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.Call((Expression) Expression.Call(t.TaskName, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToUpper)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.Contains)), (Expression) Expression.Call((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass40, typeof (SchedulingService.\u003C\u003Ec__DisplayClass4_0)), FieldInfo.GetFieldFromHandle(__fieldref (SchedulingService.\u003C\u003Ec__DisplayClass4_0.searchTerm))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToUpper)), Array.Empty<Expression>())), parameterExpression));
    }

    private IOrderedQueryable<ScheduledTaskData> SortTasks(
      IQueryable<ScheduledTaskData> allScheduledTasks,
      OrderByType orderBy)
    {
      switch (orderBy)
      {
        case OrderByType.LastExecutionDate:
          return allScheduledTasks.OrderBy<ScheduledTaskData, DateTime?>((Expression<Func<ScheduledTaskData, DateTime?>>) (t => t.LastExecutedTime));
        case OrderByType.Status:
          return allScheduledTasks.OrderBy<ScheduledTaskData, int>((Expression<Func<ScheduledTaskData, int>>) (t => (int) t.Status == 2 ? int.MaxValue : (int) t.Status));
        case OrderByType.NameAsc:
          return allScheduledTasks.OrderBy<ScheduledTaskData, string>((Expression<Func<ScheduledTaskData, string>>) (t => t.TaskName));
        case OrderByType.NameDesc:
          return allScheduledTasks.OrderByDescending<ScheduledTaskData, string>((Expression<Func<ScheduledTaskData, string>>) (t => t.TaskName));
        default:
          return allScheduledTasks.OrderBy<ScheduledTaskData, DateTime>((Expression<Func<ScheduledTaskData, DateTime>>) (t => t.ExecuteTime));
      }
    }

    private IQueryable<ScheduledTaskData> FilterTasksByStatus(
      IQueryable<ScheduledTaskData> allScheduledTasks,
      FilterByType filterBy)
    {
      switch (filterBy)
      {
        case FilterByType.Pending:
          return allScheduledTasks.Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => (int) x.Status == 0));
        case FilterByType.Started:
          return allScheduledTasks.Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => (int) x.Status == 1));
        case FilterByType.Stopped:
          return allScheduledTasks.Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => (int) x.Status == 3));
        case FilterByType.Failed:
          return allScheduledTasks.Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => (int) x.Status == 2));
        case FilterByType.Recurring:
          return allScheduledTasks.Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => !string.IsNullOrEmpty(x.ScheduleData)));
        default:
          return allScheduledTasks;
      }
    }
  }
}
