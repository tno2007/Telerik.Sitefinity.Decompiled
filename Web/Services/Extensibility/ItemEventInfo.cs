// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.ItemEventInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>Represents a class for workflow status property.</summary>
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ItemEventInfo
  {
    /// <summary>Gets or sets the date of the event.</summary>
    [DataMember]
    public DateTime Date { get; set; }

    /// <summary>Gets or sets the name of the user.</summary>
    [DataMember]
    public string User { get; set; }

    /// <summary>Gets or sets the name of the user.</summary>
    [DataMember]
    public string Id { get; set; }
  }
}
