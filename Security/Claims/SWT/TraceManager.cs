// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.TraceManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  internal static class TraceManager
  {
    private static TraceSource ts = new TraceSource("Telerik.Sitefinity.Security.Claims.SWT", SourceLevels.Warning);

    public static void TraceError(Exception exception) => TraceManager.ts.TraceData(TraceEventType.Error, 1, (object) exception);

    public static void TraceError(Exception exception, string errorId)
    {
      string message = string.Format("Error Id = {0},  Exception Message:\r\n{1}", (object) errorId, (object) exception);
      TraceManager.ts.TraceEvent(TraceEventType.Error, 1, message);
    }

    public static void TraceInformation(string format, params object[] arg)
    {
      string message = string.Format(format, arg);
      TraceManager.ts.TraceEvent(TraceEventType.Information, 2, message);
    }

    public static void TraceWarning(string format, params object[] arg)
    {
      string message = string.Format(format, arg);
      TraceManager.ts.TraceEvent(TraceEventType.Warning, 3, message);
    }

    public static void TraceVerbose(string format, params object[] arg)
    {
      string message = string.Format(format, arg);
      TraceManager.ts.TraceEvent(TraceEventType.Verbose, 5, message);
    }
  }
}
