// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Data.AsyncResult
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Telerik.Sitefinity.Workflow.Data
{
  internal abstract class AsyncResult : IAsyncResult
  {
    private static AsyncCallback asyncCompletionWrapperCallback;
    private AsyncCallback callback;
    private bool completedSynchronously;
    private bool endCalled;
    private Exception exception;
    private bool isCompleted;
    private ManualResetEvent manualResetEvent;
    private AsyncResult.AsyncCompletion nextAsyncCompletion;
    private object state;
    private object thisLock;

    protected AsyncResult(AsyncCallback callback, object state)
    {
      this.callback = callback;
      this.state = state;
      this.thisLock = new object();
    }

    public object AsyncState => this.state;

    public WaitHandle AsyncWaitHandle
    {
      get
      {
        if (this.manualResetEvent != null)
          return (WaitHandle) this.manualResetEvent;
        lock (this.ThisLock)
        {
          if (this.manualResetEvent == null)
            this.manualResetEvent = new ManualResetEvent(this.isCompleted);
        }
        return (WaitHandle) this.manualResetEvent;
      }
    }

    public bool CompletedSynchronously => this.completedSynchronously;

    public bool HasCallback => this.callback != null;

    public bool IsCompleted => this.isCompleted;

    private object ThisLock => this.thisLock;

    protected void Complete(bool completedSynchronously)
    {
      if (this.isCompleted)
        throw new InvalidProgramException();
      this.completedSynchronously = completedSynchronously;
      if (completedSynchronously)
      {
        this.isCompleted = true;
      }
      else
      {
        lock (this.ThisLock)
        {
          this.isCompleted = true;
          if (this.manualResetEvent != null)
            this.manualResetEvent.Set();
        }
      }
      if (this.callback == null)
        return;
      try
      {
        this.callback((IAsyncResult) this);
      }
      catch (Exception ex)
      {
        throw new InvalidProgramException("Async callback threw an Exception", ex);
      }
    }

    protected void Complete(bool completedSynchronously, Exception exception)
    {
      this.exception = exception;
      this.Complete(completedSynchronously);
    }

    private static void AsyncCompletionWrapperCallback(IAsyncResult result)
    {
      if (result.CompletedSynchronously)
        return;
      AsyncResult asyncState = (AsyncResult) result.AsyncState;
      AsyncResult.AsyncCompletion nextCompletion = asyncState.GetNextCompletion();
      Exception exception = (Exception) null;
      bool flag;
      try
      {
        flag = nextCompletion(result);
      }
      catch (Exception ex)
      {
        if (AsyncResult.IsFatal(ex))
        {
          throw;
        }
        else
        {
          flag = true;
          exception = ex;
        }
      }
      if (!flag)
        return;
      asyncState.Complete(false, exception);
    }

    public static bool IsFatal(Exception exception)
    {
      while (true)
      {
        switch (exception)
        {
          case OutOfMemoryException _ when !(exception is InsufficientMemoryException):
          case ThreadAbortException _:
          case AccessViolationException _:
          case SEHException _:
            goto label_1;
          case TypeInitializationException _:
          case TargetInvocationException _:
            exception = exception.InnerException;
            continue;
          default:
            goto label_4;
        }
      }
label_1:
      return true;
label_4:
      return false;
    }

    protected AsyncCallback PrepareAsyncCompletion(AsyncResult.AsyncCompletion callback)
    {
      this.nextAsyncCompletion = callback;
      if (AsyncResult.asyncCompletionWrapperCallback == null)
        AsyncResult.asyncCompletionWrapperCallback = new AsyncCallback(AsyncResult.AsyncCompletionWrapperCallback);
      return AsyncResult.asyncCompletionWrapperCallback;
    }

    private AsyncResult.AsyncCompletion GetNextCompletion()
    {
      AsyncResult.AsyncCompletion nextAsyncCompletion = this.nextAsyncCompletion;
      this.nextAsyncCompletion = (AsyncResult.AsyncCompletion) null;
      return nextAsyncCompletion;
    }

    protected static TAsyncResult End<TAsyncResult>(IAsyncResult result) where TAsyncResult : AsyncResult
    {
      if (result == null)
        throw new ArgumentNullException(nameof (result));
      if (!(result is TAsyncResult asyncResult))
        throw new ArgumentException("Invalid AsyncResult", nameof (result));
      asyncResult.endCalled = !asyncResult.endCalled ? true : throw new InvalidOperationException("AsyncResult already ended");
      if (!asyncResult.isCompleted)
        asyncResult.AsyncWaitHandle.WaitOne();
      if (asyncResult.manualResetEvent != null)
        asyncResult.manualResetEvent.Close();
      return asyncResult.exception == null ? asyncResult : throw asyncResult.exception;
    }

    protected delegate bool AsyncCompletion(IAsyncResult result);
  }
}
