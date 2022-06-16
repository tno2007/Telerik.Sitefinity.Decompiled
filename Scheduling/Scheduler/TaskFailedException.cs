// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.TaskFailedException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Sitefinity.Scheduling
{
  /// <summary>Exception that occurs when a task has failed.</summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  public class TaskFailedException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Scheduling.TaskFailedException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public TaskFailedException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
