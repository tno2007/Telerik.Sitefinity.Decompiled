// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.InMemoryTraceListener
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace Telerik.Sitefinity.Abstractions
{
  public class InMemoryTraceListener : CustomTraceListener
  {
    private static readonly ConcurrentQueue<LogEntry> entries = new ConcurrentQueue<LogEntry>();
    private const string TraceEventCacheKey = "TraceEventCache";

    [Obsolete("Use EntriesAsEnumerable instead, because it is more efficient.")]
    public static List<LogEntry> Entries => InMemoryTraceListener.entries.ToList<LogEntry>();

    public static IEnumerable<LogEntry> EntriesAsEnumerable => (IEnumerable<LogEntry>) InMemoryTraceListener.entries;

    public static void ClearData()
    {
      do
        ;
      while (InMemoryTraceListener.entries.TryDequeue(out LogEntry _));
    }

    public override void TraceData(
      TraceEventCache eventCache,
      string source,
      TraceEventType eventType,
      int id,
      object data)
    {
      LogEntry logEntry = this.CreateLogEntry(eventCache, source, eventType, id, data);
      InMemoryTraceListener.entries.Enqueue(logEntry);
    }

    public override void TraceData(
      TraceEventCache eventCache,
      string source,
      TraceEventType eventType,
      int id,
      params object[] data)
    {
      for (int index = 0; index < data.Length; ++index)
        InMemoryTraceListener.entries.Enqueue(this.CreateLogEntry(eventCache, source, eventType, id, (object) data));
    }

    public override void Write(string message)
    {
    }

    public override void WriteLine(string message)
    {
    }

    protected virtual LogEntry CreateLogEntry(
      TraceEventCache eventCache,
      string source,
      TraceEventType eventType,
      int id,
      object data)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary.Add("TraceEventCache", (object) eventCache);
      LogEntry logEntry;
      if (data is LogEntry)
      {
        logEntry = data as LogEntry;
      }
      else
      {
        object message = data;
        string[] categories;
        if (!string.IsNullOrEmpty(source))
          categories = new string[1]{ source };
        else
          categories = new string[0];
        int eventId = id;
        int severity = (int) eventType;
        Dictionary<string, object> properties = dictionary;
        logEntry = new LogEntry(message, (ICollection<string>) categories, int.MaxValue, eventId, (TraceEventType) severity, (string) null, (IDictionary<string, object>) properties);
      }
      return logEntry;
    }

    internal static void Register()
    {
      string name = Enum.GetName(typeof (ConfigurationPolicy), (object) ConfigurationPolicy.TestTracing);
      LogWriter writer = Log.Writer;
      if (!writer.TraceSources.ContainsKey(name))
        writer.TraceSources.Add(name, new LogSource(name, (IEnumerable<TraceListener>) new List<TraceListener>()
        {
          (TraceListener) new InMemoryTraceListener()
        }, SourceLevels.All));
      InMemoryTraceListener.ClearData();
    }

    internal static void Unregister()
    {
      string name = Enum.GetName(typeof (ConfigurationPolicy), (object) ConfigurationPolicy.TestTracing);
      LogWriter writer = Log.Writer;
      if (writer.TraceSources.ContainsKey(name))
        writer.TraceSources.Remove(name);
      InMemoryTraceListener.ClearData();
    }
  }
}
