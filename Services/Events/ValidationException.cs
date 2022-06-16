// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Events.ValidationException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.Events
{
  /// <summary>
  /// An exception meant to be thrown by pre-processing event handlers in validation scenarios to explicitly state that the content is invalid,
  /// and thus to be distinguished from other exceptions that the event handler may throw, which can be misinterpreted as failed validation.
  /// </summary>
  public class ValidationException : Exception
  {
    public ValidationException()
    {
    }

    public ValidationException(string message)
      : base(message)
    {
    }

    public ValidationException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
