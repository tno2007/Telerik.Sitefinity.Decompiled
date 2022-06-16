// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.Services.Data.PublishingPointViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Publishing.Web.Services.Data
{
  /// <summary>
  /// View model of the publishing point used for list views. Includes only limited properties of the publishing point.
  /// Includes also short data about the output pipes, which can be used for a UI summary on how the context is exported from the publishing point
  /// </summary>
  [DataContract(Name = "PublishingPointViewModel", Namespace = "Telerik.Sitefinity.Publishing.Web.Services.Data")]
  public class PublishingPointViewModel
  {
    private List<PipeShortData> _ouputPipes;

    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public bool IsActive { get; set; }

    [DataMember]
    public bool IsBackend { get; set; }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string Owner { get; set; }

    [DataMember]
    public DateTime DateCreated { get; set; }

    [DataMember]
    public DateTime? LastPublicationDate { get; set; }

    /// <summary>Gets or sets the output pipes.</summary>
    /// <value>The output pipes.</value>
    [DataMember]
    public List<PipeShortData> OutputPipes
    {
      get
      {
        if (this._ouputPipes == null)
          this._ouputPipes = new List<PipeShortData>();
        return this._ouputPipes;
      }
      set => this._ouputPipes = value;
    }
  }
}
