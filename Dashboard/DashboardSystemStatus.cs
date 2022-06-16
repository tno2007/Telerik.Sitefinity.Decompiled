// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Dashboard.DashboardSystemStatus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Dashboard
{
  /// <summary>Defines a system status object</summary>
  public class DashboardSystemStatus
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Dashboard.DashboardSystemStatus" /> class.
    /// </summary>
    public DashboardSystemStatus() => this.CanFindSolution = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Dashboard.DashboardSystemStatus" /> class.
    /// </summary>
    /// <param name="title">The title</param>
    /// <param name="description">The description</param>
    public DashboardSystemStatus(string title, string description)
    {
      this.Title = title;
      this.Description = description;
      this.Links = (IList<DashboardSystemStatusLink>) new List<DashboardSystemStatusLink>();
      this.CanFindSolution = true;
    }

    /// <summary>Gets or sets the title of the status</summary>
    public string Title { get; set; }

    /// <summary>Gets or sets the description of the status</summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether 'Find a solution' link should be displayed;
    /// </summary>
    public bool CanFindSolution { get; set; }

    /// <summary>Gets or sets the links of the status</summary>
    public IList<DashboardSystemStatusLink> Links { get; set; }
  }
}
