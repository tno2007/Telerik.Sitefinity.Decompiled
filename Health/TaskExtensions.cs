// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.TaskExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// Defines extension methods for <see cref="N:System.Threading.Tasks" /> class.
  /// </summary>
  internal static class TaskExtensions
  {
    /// <summary>Applies a task timeout.</summary>
    /// <typeparam name="TResult">The result type of the task.</typeparam>
    /// <param name="task">The task.</param>
    /// <param name="timeout">The timeout value to be applied.</param>
    /// <returns>The result task.</returns>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Not needed")]
    public static async Task<TResult> SetTimeout<TResult>(
      this Task<TResult> task,
      TimeSpan timeout)
    {
      if (await Task.WhenAny((Task) task, Task.Delay(timeout)).ConfigureAwait(false) == task)
        return await task.ConfigureAwait(false);
      throw new TimeoutException();
    }
  }
}
