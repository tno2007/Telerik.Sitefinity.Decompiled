// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ElmahTraceListenerExtensions
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
  /// <summary>
  /// Extensions class that adds the ElmahTraceListener to the trace listeners collection.
  /// </summary>
  public static class ElmahTraceListenerExtensions
  {
    public static SendToTraceListenerExtension Elmah(
      this ILoggingConfigurationSendTo context,
      string listenerName)
    {
      return (SendToTraceListenerExtension) new ElmahTraceListenerExtensions.SendToElmahTraceListenerBuilder(context, listenerName);
    }

    public class SendToElmahTraceListenerBuilder : SendToTraceListenerExtension
    {
      public SendToElmahTraceListenerBuilder(
        ILoggingConfigurationSendTo context,
        string listenerName)
        : base(context)
      {
        ElmahTraceListenerExtensions.ElmahTraceListenerData traceListenerData = new ElmahTraceListenerExtensions.ElmahTraceListenerData();
        traceListenerData.Name = listenerName;
        this.AddTraceListenerToSettingsAndCategory((TraceListenerData) traceListenerData);
      }
    }

    public class ElmahTraceListenerData : TraceListenerData
    {
      public ElmahTraceListenerData()
        : base(typeof (ElmahTraceListener))
      {
        this.ListenerDataType = typeof (ElmahTraceListenerExtensions.ElmahTraceListenerData);
      }

      protected override Expression<Func<TraceListener>> GetCreationExpression() => (Expression<Func<TraceListener>>) (() => Expression.New(typeof (ElmahTraceListener)));
    }
  }
}
