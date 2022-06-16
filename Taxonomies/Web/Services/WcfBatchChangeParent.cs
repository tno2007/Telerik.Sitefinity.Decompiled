// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.WcfBatchChangeParent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>
  /// Wcf class that conveys the information about batch changing of the taxon parent.
  /// </summary>
  [DataContract]
  public class WcfBatchChangeParent
  {
    /// <summary>
    /// Gets or sets the pageId of the taxon that will become a new parent.
    /// </summary>
    [DataMember]
    public Guid NewParentId { get; set; }

    /// <summary>
    /// Gets or sets the array of GUIDs that represent the taxa that will change its parent.
    /// </summary>
    [DataMember]
    public Guid[] TaxonIds { get; set; }
  }
}
