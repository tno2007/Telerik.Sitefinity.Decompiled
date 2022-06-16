// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations.SitemapNavigationSiteMapControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations
{
  public class SitemapNavigationSiteMapControl : RadSiteMap
  {
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.NodeDataBound += new RadSiteMapNodeEventHandler(this.SitemapNavigationSiteMapControl_NodeDataBound);
    }

    private void SitemapNavigationSiteMapControl_NodeDataBound(
      object sender,
      RadSiteMapNodeEventArgs e)
    {
      NavigationUtilities.SetNavigationItemTarget((NavigationItem) e.Node);
    }
  }
}
