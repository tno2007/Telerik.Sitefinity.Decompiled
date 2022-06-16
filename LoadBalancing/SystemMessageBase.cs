// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.SystemMessageBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>Data transfer object for sending system message.</summary>
  [KnownType(typeof (InvalidateCacheMessage))]
  [DataContract]
  public class SystemMessageBase
  {
    private Dictionary<string, string> additionalInfo;

    /// <summary>Gets or sets the message data.</summary>
    /// <value>The message data.</value>
    [DataMember]
    public virtual string MessageData { get; set; }

    /// <summary>Gets or sets the additional message data.</summary>
    /// <value>The additional message data.</value>
    [DataMember]
    public virtual Dictionary<string, string> AdditionalInfo
    {
      get
      {
        if (this.additionalInfo == null)
          this.additionalInfo = new Dictionary<string, string>();
        return this.additionalInfo;
      }
      set => this.additionalInfo = value;
    }

    /// <summary>
    /// Gets or sets the ID of the sender (IP, Machine name, etc).
    /// </summary>
    /// <value>The ID of the sender.</value>
    [DataMember]
    public virtual string SenderId { get; set; }

    /// <summary>Gets or sets the key.</summary>
    /// <value>The key.</value>
    [DataMember]
    public string Key { get; set; }

    /// <summary>Gets or sets message creation timestamp.</summary>
    /// <value>UTC date time.</value>
    [DataMember]
    public long Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the connection name that current message is related to.
    /// </summary>
    /// <value>Name of related connection.</value>
    [DataMember]
    public string Connection { get; set; }

    /// <inheritdoc />
    public override string ToString() => string.Format("Key: '{0}'; Data: '{1}'.", (object) this.Key, (object) this.MessageData);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.SystemMessageBase" /> class.
    /// </summary>
    public SystemMessageBase() => this.Timestamp = DateTime.UtcNow.Ticks;
  }
}
