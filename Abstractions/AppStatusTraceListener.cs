// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.AppStatusTraceListener
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Globalization;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Creates a listener for storing Application Status tracing information.
  /// </summary>
  internal class AppStatusTraceListener : CustomTraceListener
  {
    /// <summary>
    /// Writes trace information, a data object and event information to the concurrent queue.
    /// </summary>
    /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
    /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">The trace data to emit.</param>
    public override void TraceData(
      TraceEventCache eventCache,
      string source,
      TraceEventType eventType,
      int id,
      object data)
    {
      AppStatusEntry entry = AppStatusTraceListener.GetEntry(data, eventType);
      if (entry == null)
        return;
      SystemManager.AppStatusBuffer.TryAdd(entry);
    }

    /// <summary>
    /// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
    /// </summary>
    /// <param name="message">A message to write.</param>
    public override void Write(string message) => this.WriteLine(message);

    /// <summary>
    /// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
    /// </summary>
    /// <param name="message">A message to write.</param>
    public override void WriteLine(string message) => this.TraceData(new TraceEventCache(), string.Empty, TraceEventType.Information, 0, (object) message);

    /// <summary>
    /// Helper method used to prepare an AppStatusEntry object.
    /// </summary>
    /// <param name="data">Log entry or string data.</param>
    /// <param name="eventType">Trace event type.</param>
    /// <returns>An AppStatusEntry object.</returns>
    internal static AppStatusEntry GetEntry(object data, TraceEventType eventType = TraceEventType.Information)
    {
      switch (data)
      {
        case LogEntry _:
          LogEntry logEntry = data as LogEntry;
          return new AppStatusEntry()
          {
            Message = logEntry.Message,
            SeverityString = logEntry.LoggedSeverity,
            TimestampString = logEntry.TimeStampString
          };
        case string _:
          return new AppStatusEntry()
          {
            Message = data as string,
            SeverityString = eventType.ToString(),
            TimestampString = DateTime.UtcNow.ToString((IFormatProvider) CultureInfo.CurrentCulture)
          };
        default:
          return (AppStatusEntry) null;
      }
    }
  }
}
