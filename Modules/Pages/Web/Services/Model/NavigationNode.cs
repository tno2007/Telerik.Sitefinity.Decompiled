// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.NavigationNode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>Data transfer class that groups less data for a page.</summary>
  public class NavigationNode
  {
    public NavigationNode(
      Guid id,
      string text,
      string tooltip,
      string navigationUrl,
      string statusText = "",
      string status = "",
      Guid rootNodeId = default (Guid),
      bool isPublished = false)
    {
      this.Id = id;
      this.Text = text;
      this.ToolTip = tooltip;
      this.StatusText = statusText;
      this.Status = status;
      this.RootNodeId = rootNodeId;
      this.NaviagtionUrl = this.ResolveNavigationUrl(navigationUrl, isPublished);
    }

    /// <summary>Gets or sets the id of a navigation node.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the text of a navigation node.</summary>
    public string Text { get; set; }

    /// <summary>Gets or sets the tooltip of a navigation node.</summary>
    public string ToolTip { get; set; }

    /// <summary>Gets or sets the url of a navigation node.</summary>
    public string NaviagtionUrl { get; set; }

    /// <summary>Gets or sets the status of a navigation node.</summary>
    public string Status { get; set; }

    /// <summary>Gets or sets the status text of a navigation node.</summary>
    public string StatusText { get; set; }

    /// <summary>Gets or sets the root id of a navigation node.</summary>
    public Guid RootNodeId { get; set; }

    private string ResolveNavigationUrl(string navigationUrl, bool isPublished)
    {
      if (SystemManager.CurrentHttpContext == null || SystemManager.CurrentHttpContext.Request == null)
        return navigationUrl;
      ISite currentSite = SystemManager.MultisiteContext.CurrentSite;
      if (currentSite != null && this.RootNodeId != currentSite.SiteMapRootNodeId)
      {
        ISite siteBySiteMapRoot = SystemManager.MultisiteContext.GetSiteBySiteMapRoot(this.RootNodeId);
        if (siteBySiteMapRoot != null)
        {
          if (isPublished)
          {
            using (new SiteRegion(siteBySiteMapRoot, SiteContextResolutionTypes.ByParam))
              navigationUrl = SystemManager.MultisiteContext.ResolveUrl(navigationUrl);
          }
          else
            return navigationUrl.Contains<char>('?') ? navigationUrl + "&sf_site=" + siteBySiteMapRoot.Id.ToString() + "&sf_site_temp=true" : navigationUrl + "?sf_site=" + siteBySiteMapRoot.Id.ToString() + "&sf_site_temp=true";
        }
      }
      return navigationUrl;
    }
  }
}
