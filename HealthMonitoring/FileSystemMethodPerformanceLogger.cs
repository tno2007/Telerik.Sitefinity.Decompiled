// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.FileSystemMethodPerformanceLogger
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Telerik.Sitefinity.HealthMonitoring
{
  internal class FileSystemMethodPerformanceLogger : MethodPerformanceLogger
  {
    private const string Format = "{0} \"{1}\" - {2}";
    private string folder;
    private TimeSpan unknownTimеТhreshold = TimeSpan.FromMilliseconds(50.0);

    public FileSystemMethodPerformanceLogger(string folder) => this.folder = folder;

    public override void PersistData()
    {
      if (string.IsNullOrEmpty(this.folder))
        return;
      IList<MethodExecutionTime> log = this.Log;
      if (log == null || log.Count <= 0)
        return;
      using (StreamWriter writer = new StreamWriter(Path.Combine(this.folder, "data-{0:yyyy-MM-dd_hh-mm-ss.fff-tt}-{1}.log".Arrange((object) DateTime.UtcNow, (object) Thread.CurrentThread.ManagedThreadId))))
      {
        TimeSpan zero = TimeSpan.Zero;
        foreach (MethodExecutionTime parent in (IEnumerable<MethodExecutionTime>) log)
        {
          zero += parent.ExecutionTime;
          this.WriteTree(writer, parent, 1);
        }
        writer.WriteLine("Total: {0}", (object) zero);
      }
      log.Clear();
    }

    private void WriteTree(StreamWriter writer, MethodExecutionTime parent, int level)
    {
      string str = new string('-', level);
      writer.WriteLine(string.Format("{0} \"{1}\" - {2}", (object) str, (object) parent.Key, (object) parent.ExecutionTime));
      if (parent.Children.Count <= 0)
        return;
      TimeSpan zero = TimeSpan.Zero;
      foreach (MethodExecutionTime child in (IEnumerable<MethodExecutionTime>) parent.Children)
      {
        zero += child.ExecutionTime;
        this.WriteTree(writer, child, level + 1);
      }
      TimeSpan timeSpan = parent.ExecutionTime - zero;
      if (!(timeSpan > this.unknownTimеТhreshold))
        return;
      writer.WriteLine(string.Format("{0} \"{1}\" - {2}", (object) new string('-', level + 1), (object) "Unknown", (object) timeSpan));
    }
  }
}
