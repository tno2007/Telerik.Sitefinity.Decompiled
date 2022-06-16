// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.Retry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Threading;

namespace Telerik.Sitefinity.Utilities
{
  internal static class Retry
  {
    public static void Handle<TException>(
      Action tryAction,
      Action<Exception> catchAction = null,
      int retryInterval = 500,
      int maxAttemptCount = 20)
      where TException : Exception
    {
      Retry.Handle<TException, object>((Func<object>) (() =>
      {
        tryAction();
        return (object) null;
      }), catchAction, retryInterval, maxAttemptCount);
    }

    public static TResult Handle<TException, TResult>(
      Func<TResult> tryFunction,
      Action<Exception> catchAction = null,
      int retryInterval = 500,
      int maxAttemptCount = 20)
      where TException : Exception
    {
      int num = 0;
      while (num < maxAttemptCount)
      {
        try
        {
          return tryFunction();
        }
        catch (TException ex)
        {
          TException exception = ex;
          ++num;
          if (catchAction != null)
            catchAction((Exception) exception);
          if (num == maxAttemptCount)
            throw (object) exception;
          Thread.Sleep(retryInterval);
        }
      }
      return default (TResult);
    }
  }
}
