// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.SafeLogger
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.BackgroundTasks
{
  /// <summary>Provides mechanism for an exception less logging.</summary>
  internal static class SafeLogger
  {
    /// <summary>
    /// Tries to write in the log and prevents from throwing an exception (e.g. if the log is not available).
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="eventType">Type of the event.</param>
    internal static bool TryLog(string message, TraceEventType eventType)
    {
      try
      {
        if (!Config.SectionHandler.Testing.Enabled)
          return false;
        Telerik.Sitefinity.Abstractions.Log.Write((object) message, eventType);
        return true;
      }
      catch
      {
      }
      return false;
    }

    /// <summary>
    /// Tries to write in the log and prevents from throwing an exception (e.g. if the log is not available).
    /// </summary>
    /// <param name="message">The message formatter.</param>
    /// <param name="eventType">Type of the event.</param>
    /// <param name="args">The arguments that will be replaced in the message.</param>
    internal static bool TryLog(string message, TraceEventType eventType, params object[] args)
    {
      try
      {
        message = string.Format(message, args);
        return SafeLogger.TryLog(message, eventType);
      }
      catch
      {
      }
      return false;
    }
  }
}
