// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.NlbCheckMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.LoadBalancing;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// Data transfer object for sending NlbCheck validation message.
  /// </summary>
  internal class NlbCheckMessage : SystemMessageBase
  {
    /// <summary>NlbCheckMessage Key</summary>
    public const string NlbCheckMessageKey = "sf_NlbCheckMessage";
    /// <summary>Request type ping Key</summary>
    public const string RequestTypePing = "ping";
    /// <summary>Request type pong Key</summary>
    public const string RequestTypePong = "pong";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Health.NlbCheckMessage" /> class.
    /// </summary>
    public NlbCheckMessage() => this.Key = "sf_NlbCheckMessage";

    /// <summary>NlbCheckMessage MessageData class</summary>
    public class NlbCheckMessageInfo
    {
      /// <summary>Gets or sets NlbCheckMessage request type - ping/pong</summary>
      public string RequestType { get; set; }

      /// <summary>Gets or sets NlbCheckMessage original sender</summary>
      public string OriginalSenderId { get; set; }

      /// <summary>Gets or sets NlbCheckMessage transport timeout</summary>
      public long TimeoutTime { get; set; }
    }
  }
}
