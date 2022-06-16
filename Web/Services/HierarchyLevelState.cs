// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.HierarchyLevelState
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Used to pass information on each level of folders hierarchy in the content modules
  /// </summary>
  [DataContract]
  public class HierarchyLevelState
  {
    /// <summary>
    /// Gets or sets the Skip used for the getting of folder hierarchy item's neighbors
    /// </summary>
    [DataMember]
    public int Skip { get; set; }

    /// <summary>
    /// Gets or sets the Take used for the getting of folder hierarchy item's neighbors
    /// </summary>
    [DataMember]
    public int Take { get; set; }

    /// <summary>
    /// Gets or sets the Total count of items in a folder hierarchy level
    /// </summary>
    [DataMember]
    public int Total { get; set; }
  }
}
