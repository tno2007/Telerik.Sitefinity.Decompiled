// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.NavigationSiteMapProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  internal class NavigationSiteMapProvider : SiteMapBase
  {
    public SiteMapBase BaseSitemap { get; set; }

    public string StartingNodeUrl { get; set; }

    public NavigationSiteMapProvider(SiteMapBase smb, string startingNodeUrl)
    {
      this.BaseSitemap = smb;
      this.StartingNodeUrl = startingNodeUrl;
    }

    public override string RootNodeKey => this.BaseSitemap.RootNodeKey;

    /// <summary>Finds the site map node.</summary>
    /// <param name="rawUrl">The raw URL.</param>
    /// <returns></returns>
    public override SiteMapNode FindSiteMapNode(string rawUrl)
    {
      rawUrl.Contains(this.StartingNodeUrl);
      return this.FindSiteMapNode(rawUrl, true);
    }
  }
}
