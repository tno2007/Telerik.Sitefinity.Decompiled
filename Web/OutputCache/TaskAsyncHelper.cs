// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.TaskAsyncHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal static class TaskAsyncHelper
  {
    /// <summary>
    /// Assists in converting an method written using the Task Asynchronous Pattern to a Begin/End method pair
    /// </summary>
    /// <param name="taskFunc">The task func</param>
    /// <param name="callback">The callback</param>
    /// <param name="state">The object state</param>
    /// <returns>The async result</returns>
    public static IAsyncResult BeginTask(
      Func<Task> taskFunc,
      AsyncCallback callback,
      object state)
    {
      Task task = taskFunc();
      if (task == null)
        return (IAsyncResult) null;
      TaskWrapperAsyncResult taskWrapperAsyncResult = new TaskWrapperAsyncResult(task, state);
      bool isCompleted = task.IsCompleted;
      if (isCompleted)
        taskWrapperAsyncResult.ForceCompletedSynchronously();
      if (callback == null)
        return (IAsyncResult) taskWrapperAsyncResult;
      if (!isCompleted)
        task.ContinueWith((Action<Task>) (_ => callback((IAsyncResult) taskWrapperAsyncResult)));
      else
        callback((IAsyncResult) taskWrapperAsyncResult);
      return (IAsyncResult) taskWrapperAsyncResult;
    }

    public static void EndTask(IAsyncResult ar)
    {
      if (ar == null)
        throw new ArgumentNullException(nameof (ar));
      if (!(ar is TaskWrapperAsyncResult wrapperAsyncResult))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "The provided {0} is invalid", (object) "IAsyncResult"));
      wrapperAsyncResult.Task.GetAwaiter().GetResult();
    }
  }
}
