// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Api.OData.Exceptions.ErrorCodeException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.Api.OData.Exceptions
{
  internal class ErrorCodeException : Exception
  {
    private static readonly string ExceptionName = typeof (Exception).Name;
    private readonly string errorCodeField;

    public ErrorCodeException()
      : this((string) null)
    {
    }

    public ErrorCodeException(string message)
      : this(message, (string) null)
    {
    }

    public ErrorCodeException(string message, string errorCode)
      : this(message, errorCode, (Exception) null)
    {
    }

    public ErrorCodeException(string message, string errorCode, Exception inner)
      : base(message, inner)
    {
      this.errorCodeField = errorCode;
    }

    internal string ErrorCode
    {
      get
      {
        if (this.errorCodeField == null)
        {
          string name = this.GetType().Name;
          if (name.EndsWith(ErrorCodeException.ExceptionName))
            return name.Replace(ErrorCodeException.ExceptionName, string.Empty);
        }
        return this.errorCodeField;
      }
    }
  }
}
