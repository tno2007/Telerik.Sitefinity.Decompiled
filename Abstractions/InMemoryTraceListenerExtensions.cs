// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.InMemoryTraceListenerExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace Telerik.Sitefinity.Abstractions
{
  public static class InMemoryTraceListenerExtensions
  {
    public static SendToTraceListenerExtension InMemory(
      this ILoggingConfigurationSendTo context,
      string listenerName)
    {
      return (SendToTraceListenerExtension) new InMemoryTraceListenerExtensions.SendToMemoryTraceListenerBuilder(context, listenerName);
    }

    public class SendToMemoryTraceListenerBuilder : SendToTraceListenerExtension
    {
      public SendToMemoryTraceListenerBuilder(
        ILoggingConfigurationSendTo context,
        string listenerName)
        : base(context)
      {
        InMemoryTraceListenerExtensions.InMemoryTraceListenerData traceListenerData = new InMemoryTraceListenerExtensions.InMemoryTraceListenerData();
        traceListenerData.Name = listenerName;
        this.AddTraceListenerToSettingsAndCategory((TraceListenerData) traceListenerData);
      }
    }

    public class InMemoryTraceListenerData : TraceListenerData
    {
      public InMemoryTraceListenerData()
        : base(typeof (InMemoryTraceListener))
      {
        this.ListenerDataType = typeof (InMemoryTraceListenerExtensions.InMemoryTraceListenerData);
      }

      protected override Expression<Func<TraceListener>> GetCreationExpression() => (Expression<Func<TraceListener>>) (() => Expression.New(typeof (InMemoryTraceListener)));
    }
  }
}
