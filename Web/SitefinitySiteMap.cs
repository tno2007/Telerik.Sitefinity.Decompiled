// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SitefinitySiteMap
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Provides implementation for <see cref="T:System.Web.SiteMapProvider" /> for Sitefinity pages.
  /// </summary>
  public class SitefinitySiteMap : SiteMapBase
  {
    /// <summary>Gets the key of the root node for this provider.</summary>
    /// <value>The root node key.</value>
    public override string RootNodeKey
    {
      get
      {
        ISite currentSite = SystemManager.CurrentContext.CurrentSite;
        Guid guid = currentSite == null ? Config.Get<PagesConfig>().HomePageId : currentSite.HomePageId;
        return guid == Guid.Empty ? string.Empty : guid.ToString().ToUpper();
      }
    }

    /// <summary>Gets the current site map provider.</summary>
    /// <returns></returns>
    public new static SiteMapProvider GetCurrentProvider() => SiteMapBase.GetSiteMapProvider<SitefinitySiteMap>(nameof (SitefinitySiteMap));
  }
}
