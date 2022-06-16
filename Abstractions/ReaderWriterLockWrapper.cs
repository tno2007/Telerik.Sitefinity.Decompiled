// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ReaderWriterLockWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Telerik.Sitefinity.Abstractions
{
  internal class ReaderWriterLockWrapper
  {
    private ReaderWriterLockSlim readWriteLock = new ReaderWriterLockSlim();

    public bool EnterForRead(Action action, int millisecondsTimeout = 5000)
    {
      if (this.readWriteLock.TryEnterReadLock(millisecondsTimeout))
      {
        try
        {
          action();
          return true;
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
        finally
        {
          this.readWriteLock.ExitReadLock();
        }
      }
      else
        this.LogReadWriteLockTimeout("EnterReadLock timeout.");
      return false;
    }

    public bool EnterForWrite(Action action, int millisecondsTimeout = 5000)
    {
      if (this.readWriteLock.TryEnterWriteLock(millisecondsTimeout))
      {
        try
        {
          action();
          return true;
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
        finally
        {
          this.readWriteLock.ExitWriteLock();
        }
      }
      else
        this.LogReadWriteLockTimeout("EnterWriteLock timeout.");
      return false;
    }

    private void LogReadWriteLockTimeout(string message)
    {
      StringBuilder stringBuilder = new StringBuilder();
      StackTrace stackTrace = new StackTrace();
      for (int index = 1; index < stackTrace.FrameCount; ++index)
      {
        MethodBase method = stackTrace.GetFrame(index).GetMethod();
        string str = method.DeclaringType != (Type) null ? method.DeclaringType.FullName : "unknown";
        if (!str.StartsWith("Telerik.Microsoft"))
        {
          stringBuilder.AppendLine();
          stringBuilder.AppendFormat("{0}: {1}", (object) str, (object) method.ToString());
        }
      }
      string str1 = stringBuilder.ToString();
      Log.Write((object) string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\\n\\r\r\n                IsReadLockHeld: {1}\\n\\r\r\n                IsWriteLockHeld: {2}\\n\\r\r\n                WaitingReadCount: {3}\\n\\r\r\n                WaitingWriteCount: {4}\\n\\r\r\n                WaitingWriteCount: {5}", (object) message, (object) this.readWriteLock.IsReadLockHeld, (object) this.readWriteLock.IsWriteLockHeld, (object) this.readWriteLock.WaitingReadCount, (object) this.readWriteLock.WaitingWriteCount, (object) str1), TraceEventType.Information);
    }
  }
}
