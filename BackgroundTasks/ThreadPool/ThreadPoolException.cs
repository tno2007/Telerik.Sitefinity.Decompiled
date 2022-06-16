// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.ThreadPoolException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.BackgroundTasks
{
  /// <summary>
  /// Base class for exceptions thrown by the Quartz <see cref="!:IScheduler" />.
  /// </summary>
  /// <remarks>
  /// SchedulerExceptions may contain a reference to another
  /// <see cref="T:System.Exception" />, which was the underlying cause of the SchedulerException.
  /// </remarks>
  [Serializable]
  internal class ThreadPoolException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:SchedulerException" /> class.
    /// </summary>
    public ThreadPoolException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:SchedulerException" /> class.
    /// </summary>
    /// <param name="msg">The MSG.</param>
    public ThreadPoolException(string msg)
      : base(msg)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:SchedulerException" /> class.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"></see> is zero (0). </exception>
    /// <exception cref="T:System.ArgumentNullException">The info parameter is null. </exception>
    protected ThreadPoolException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:SchedulerException" /> class.
    /// </summary>
    /// <param name="cause">The cause.</param>
    public ThreadPoolException(Exception cause)
      : base(cause.ToString(), cause)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:SchedulerException" /> class.
    /// </summary>
    /// <param name="msg">The MSG.</param>
    /// <param name="cause">The cause.</param>
    public ThreadPoolException(string msg, Exception cause)
      : base(msg, cause)
    {
    }

    /// <summary>
    /// Creates and returns a string representation of the current exception.
    /// </summary>
    /// <returns>A string representation of the current exception.</returns>
    /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" /></PermissionSet>
    public override string ToString() => this.InnerException == null ? base.ToString() : string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} [See nested exception: {1}]", (object) base.ToString(), (object) this.InnerException);
  }
}
