// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.ResetModelMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Xml.Linq;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// Data transfer object for sending "reset application" message.
  /// </summary>
  public class ResetModelMessage : SystemMessageBase
  {
    private const string eltReset = "Reset";
    private const string attrReason = "Reason";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.ResetApplicationMessage" /> class.
    /// </summary>
    /// <param name="attemptFullRestart">if set to <c>true</c> [attempt full restart].</param>
    public ResetModelMessage() => this.Key = "ResetModel";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.ResetApplicationMessage" /> class.
    /// </summary>
    /// <param name="flags">The flags.</param>
    /// <param name="reason">The reason.</param>
    public ResetModelMessage(OperationReason reason)
      : this()
    {
      this.Reason = reason;
    }

    /// <summary>
    /// Creates a new instance by deserializing an XML representation.
    /// </summary>
    /// <param name="data">A serialized <see cref="T:Telerik.Sitefinity.LoadBalancing.ResetApplicationMessage" />.</param>
    public ResetModelMessage(string data)
      : this()
    {
      this.Deserialize(data);
    }

    /// <summary>A reason for the application restart.</summary>
    public virtual OperationReason Reason { get; set; }

    /// <inheritdoc />
    public override string MessageData
    {
      get => this.Serialize();
      set => this.Deserialize(value);
    }

    /// <summary>Returns an XML representation of the object.</summary>
    public string Serialize() => new XDocument(new object[1]
    {
      (object) new XElement((XName) "Reset", (object) new XAttribute((XName) "Reason", (object) (this.Reason != null ? this.Reason.ToString() : string.Empty)))
    }).ToString();

    private void Deserialize(string data)
    {
      using (StringReader stringReader = new StringReader(data))
        this.Reason = OperationReason.Parse(XDocument.Load((TextReader) stringReader).Root.Attribute((XName) "Reason").Value);
    }
  }
}
