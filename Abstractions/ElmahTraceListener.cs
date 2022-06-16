// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ElmahTraceListener
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>Custom listener for writing to the ELMAH logs.</summary>
  public class ElmahTraceListener : CustomTraceListener
  {
    public const string elmahAssemblyName = "Elmah";
    private static Assembly elmahAssembly;

    /// <summary>
    /// Writes trace information, a data object and event information to the listener specific output.
    /// </summary>
    /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
    /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">The trace data to emit.</param>
    /// <PermissionSet>
    /// <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override void TraceData(
      TraceEventCache eventCache,
      string source,
      TraceEventType eventType,
      int id,
      object data)
    {
      if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, (string) null, (object[]) null, data, (object[]) null))
        return;
      switch (data)
      {
        case LogEntry _:
          Assembly assemblyFromAppDomain = ElmahTraceListener.GetElmahAssemblyFromAppDomain();
          if (assemblyFromAppDomain == (Assembly) null)
            throw new Exception("Elmah assembly is not added to the app domain of the web application!");
          ElmahLoggerSingleton.GetInstance().Log(data as LogEntry, assemblyFromAppDomain);
          break;
        case string _:
          this.Write(data as string);
          break;
      }
    }

    /// <summary>
    /// Finds the Elmah assembly in the Application domain.
    /// If the assembly is not present returns null.
    /// </summary>
    private static Assembly GetElmahAssemblyFromAppDomain()
    {
      if (ElmahTraceListener.elmahAssembly == (Assembly) null)
      {
        string assemblyName = "Elmah";
        ElmahTraceListener.elmahAssembly = ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>) (a => a.GetName().Name == assemblyName)).SingleOrDefault<Assembly>();
      }
      return ElmahTraceListener.elmahAssembly;
    }

    /// <summary>
    /// Writes trace information, an array of data objects and event information to the listener specific output.
    /// </summary>
    /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
    /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
    /// <param name="id">A numeric identifier for the event.</param>
    /// <param name="data">An array of objects to emit as data.</param>
    /// <PermissionSet>
    /// <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    /// </PermissionSet>
    public override void TraceData(
      TraceEventCache eventCache,
      string source,
      TraceEventType eventType,
      int id,
      params object[] data)
    {
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
  }
}
