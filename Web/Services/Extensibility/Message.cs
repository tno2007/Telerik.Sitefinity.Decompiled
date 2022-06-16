// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.Message
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>Represents a class for the message</summary>
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class Message
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.Extensibility.Message" /> class.
    /// </summary>
    public Message() => this.Operations = new ItemOperation[0];

    /// <summary>Gets or sets the title</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the description</summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>Gets or sets the related operations with this message</summary>
    [DataMember]
    public ItemOperation[] Operations { get; set; }
  }
}
