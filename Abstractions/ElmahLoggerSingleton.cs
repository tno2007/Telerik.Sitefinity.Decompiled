// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ElmahLoggerSingleton
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Logs the data(could be error, information, warning, etc.) via Elmah using reflection.
  ///  </summary>
  public sealed class ElmahLoggerSingleton
  {
    private static ElmahLoggerSingleton instance = (ElmahLoggerSingleton) null;
    private static readonly object syncLock = new object();

    private ElmahLoggerSingleton()
    {
    }

    /// <summary>Gets the instance.</summary>
    /// <returns></returns>
    public static ElmahLoggerSingleton GetInstance()
    {
      if (ElmahLoggerSingleton.instance == null)
      {
        lock (ElmahLoggerSingleton.syncLock)
        {
          if (ElmahLoggerSingleton.instance == null)
            ElmahLoggerSingleton.instance = new ElmahLoggerSingleton();
        }
      }
      return ElmahLoggerSingleton.instance;
    }

    /// <summary>
    /// Creates the Elmah error by passing the log entry properties and
    /// logs the data(could be error, information, warning, etc.) via Elmah using reflection.
    /// </summary>
    /// <param name="logEntry">The log entry.</param>
    /// <param name="elmahAssembly">The elmah assembly.</param>
    public void Log(LogEntry logEntry, Assembly elmahAssembly)
    {
      lock (ElmahLoggerSingleton.syncLock)
      {
        Type type = elmahAssembly.GetType("Elmah.Error", true);
        string str = logEntry.Message;
        if (logEntry.Severity != TraceEventType.Information && logEntry.Severity != TraceEventType.Warning && str.IndexOf("An") != -1)
          str = str.Substring(str.IndexOf("An"), str.IndexOf("\r\n-") - 1);
        object[] objArray = new object[2]
        {
          (object) new HttpException(logEntry.Message),
          (object) HttpContext.Current
        };
        object instance = Activator.CreateInstance(type, objArray);
        PropertyInfo property1 = type.GetProperty("ApplicationName");
        property1.SetValue(instance, Convert.ChangeType((object) logEntry.AppDomainName, property1.PropertyType), (object[]) null);
        PropertyInfo property2 = type.GetProperty("Message");
        property2.SetValue(instance, Convert.ChangeType((object) str, property2.PropertyType), (object[]) null);
        object obj = elmahAssembly.GetType("Elmah.ErrorLog", true).GetMethod("GetDefault", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Invoke((object) null, new object[1]
        {
          (object) HttpContext.Current
        });
        obj.GetType().GetMethod(nameof (Log)).Invoke(obj, new object[1]
        {
          instance
        });
      }
    }
  }
}
