// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.ResetApplicationMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Xml.Linq;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// Data transfer object for sending "reset application" message.
  /// </summary>
  public class ResetApplicationMessage : SystemMessageBase
  {
    private const string eltReset = "Reset";
    private const string attrAttemptFullRestart = "AttemptFullRestart";
    private const string attrReason = "Reason";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.ResetApplicationMessage" /> class.
    /// </summary>
    protected ResetApplicationMessage() => this.Key = "ResetApplicationKey";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.ResetApplicationMessage" /> class.
    /// </summary>
    /// <param name="attemptFullRestart">When <c>true</c>, attempt full restart on the receiving instances.</param>
    [Obsolete("Use the constructor with Flags parameter instead")]
    public ResetApplicationMessage(bool attemptFullRestart)
      : this(OperationReason.UnknownReason(), attemptFullRestart ? SystemRestartFlags.AttemptFullRestart : SystemRestartFlags.Default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.ResetApplicationMessage" /> class.
    /// </summary>
    /// <param name="reason">The reason.</param>
    /// <param name="flags">The flags.</param>
    public ResetApplicationMessage(OperationReason reason, SystemRestartFlags flags)
      : this()
    {
      this.Reason = reason;
      this.Flags = flags;
    }

    /// <summary>
    /// Creates a new instance by deserializing an XML representation.
    /// </summary>
    /// <param name="data">A serialized <see cref="T:Telerik.Sitefinity.LoadBalancing.ResetApplicationMessage" />.</param>
    public ResetApplicationMessage(string data)
      : this()
    {
      this.Deserialize(data);
    }

    /// <summary>A reason for the application restart.</summary>
    public virtual OperationReason Reason { get; set; }

    /// <summary>
    /// When <c>true</c>, attempt full restart on the receiving instances.
    /// </summary>
    [Obsolete("Use Flags property instead.")]
    public virtual bool AttemptFullRestart
    {
      get => (this.Flags | SystemRestartFlags.AttemptFullRestart) > SystemRestartFlags.Default;
      set => this.Flags |= SystemRestartFlags.AttemptFullRestart;
    }

    public virtual SystemRestartFlags Flags { get; set; }

    /// <inheritdoc />
    public override string MessageData
    {
      get => this.Serialize();
      set => this.Deserialize(value);
    }

    /// <summary>Returns an XML representation of the object.</summary>
    public string Serialize()
    {
      string str = this.Reason != null ? this.Reason.ToString() : string.Empty;
      return new XDocument(new object[1]
      {
        (object) new XElement((XName) "Reset", new object[2]
        {
          (object) new XAttribute((XName) "AttemptFullRestart", (object) this.Flags.ToString()),
          (object) new XAttribute((XName) "Reason", (object) str)
        })
      }).ToString();
    }

    private void Deserialize(string data)
    {
      using (StringReader stringReader = new StringReader(data))
      {
        XDocument xdocument = XDocument.Load((TextReader) stringReader);
        this.Flags = (SystemRestartFlags) Enum.Parse(typeof (SystemRestartFlags), xdocument.Root.Attribute((XName) "AttemptFullRestart").Value);
        this.Reason = OperationReason.Parse(xdocument.Root.Attribute((XName) "Reason").Value);
      }
    }
  }
}
