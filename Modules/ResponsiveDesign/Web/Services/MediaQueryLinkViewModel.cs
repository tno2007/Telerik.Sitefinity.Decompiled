// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.MediaQueryLinkViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services
{
  /// <summary>View model for the MediaQueryLink type</summary>
  [DataContract]
  public class MediaQueryLinkViewModel
  {
    /// <summary>
    /// Gets or sets the ID of the item( currently Page or Template) that will have the corresponding media queries
    /// </summary>
    [DataMember]
    public Guid ItemId { get; set; }

    /// <summary>Gets or sets the item type</summary>
    [DataMember]
    public string ItemType { get; set; }

    /// <summary>
    /// Gets or sets the array that will hold the ids of all media queries that will be applied to the item
    /// </summary>
    [DataMember]
    public MediaQueryViewModel[] MediaQueries { get; set; }

    /// <summary>
    /// Gets or sets the selection type that will determine what media queries to be applied for this item
    /// </summary>
    [DataMember]
    public string LinkType { get; set; }
  }
}
