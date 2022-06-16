// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.ISystemMessageSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// System message senders are classes that are responsible to send a system message
  /// (e.g. cache invalidation notificaiton) to all other Sitefinity instances in a
  /// load balanced environment.
  /// </summary>
  public interface ISystemMessageSender
  {
    /// <summary>
    /// A value indicating whether this sender should be invoked for sending messages.
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// Sends a system message to all other instances of the system.
    /// </summary>
    /// <param name="msg">The message.</param>
    void SendSystemMessage(SystemMessageBase msg);

    void SendSystemMessages(SystemMessageBase[] msgs);
  }
}
