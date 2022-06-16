// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Events.EventHandlerInvocationException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Reflection;

namespace Telerik.Sitefinity.Services.Events
{
  internal class EventHandlerInvocationException : Exception
  {
    private const string defaultMessage = "Exception has been thrown by an event handler. See the InnerException for details.";

    public EventHandlerInvocationException(Exception innerException)
      : base("Exception has been thrown by an event handler. See the InnerException for details.", innerException)
    {
    }

    public override string Message => this.InnerException is TargetInvocationException innerException && innerException.InnerException != null ? innerException.InnerException.Message : base.Message;
  }
}
