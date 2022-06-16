// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Replication.IReplicationMessageTransporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.LoadBalancing.Replication
{
  /// <summary>
  /// Manage sending and receiving replication related system messages.
  /// </summary>
  public interface IReplicationMessageTransporter : IDisposable
  {
    /// <summary>
    /// Initialize needed resources and report back whether current transporter can be used.
    /// </summary>
    /// <param name="parameters">Activation parameters.</param>
    /// <returns>True if trans</returns>
    bool Activate(NameValueCollection parameters);

    /// <summary>Send message over transportation channel.</summary>
    /// <param name="message">System message.</param>
    void Send(SystemMessageBase message);

    /// <summary>Subscribe handler for received messages.</summary>
    /// <param name="hander">Message handler.</param>
    void Subscribe(
      Action<IReplicationMessageTransporter, SystemMessageBase> hander);
  }
}
