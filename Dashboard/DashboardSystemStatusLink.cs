// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Dashboard.DashboardSystemStatusLink
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Dashboard
{
  /// <summary>Defines a link for system status</summary>
  public class DashboardSystemStatusLink
  {
    /// <summary>Gets or sets the title of the link</summary>
    public string Title { get; set; }

    /// <summary>Gets or sets the url of the link</summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the link should be opened in the same tab
    /// </summary>
    public bool OpenInSameTab { get; set; }

    internal DashboardSystemStatusLink Clone() => new DashboardSystemStatusLink()
    {
      Title = this.Title,
      Url = this.Url,
      OpenInSameTab = this.OpenInSameTab
    };
  }
}
