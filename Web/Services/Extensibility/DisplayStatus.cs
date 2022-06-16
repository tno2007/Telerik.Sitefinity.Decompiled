// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.DisplayStatus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>Represents a class for display status property.</summary>
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class DisplayStatus : ItemEventInfo, IScheduledStatus
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.Extensibility.DisplayStatus" /> class.
    /// </summary>
    public DisplayStatus() => this.Message = new Message();

    /// <summary>
    /// Gets or sets the Name of the status ("Unpublished, Awaiting approval").
    /// </summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the Source this status belongs to ("Lifecycle", "Workflow", "Translations").
    /// </summary>
    [DataMember]
    public string Source { get; set; }

    /// <summary>Gets or sets the Message of the status</summary>
    [DataMember]
    public Message Message { get; set; }

    /// <summary>Gets or sets the label of the status</summary>
    [DataMember]
    public string Label { get; set; }

    /// <summary>Gets or sets the detailed label of the status</summary>
    [DataMember]
    public string DetailedLabel { get; set; }

    /// <summary>Gets or sets the publication date.</summary>
    [DataMember]
    public DateTime? PublicationDate { get; set; }

    /// <summary>Gets or sets the expiration date.</summary>
    [DataMember]
    public DateTime? ExpirationDate { get; set; }
  }
}
