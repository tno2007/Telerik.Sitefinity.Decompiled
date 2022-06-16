// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.PageTempPreviewGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>Provides methods for working with page previews</summary>
  public class PageTempPreviewGenerator
  {
    /// <summary>Gets the preview URL.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="language">The language.</param>
    /// <param name="expirationTimeInHours">The expiration time in hours.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public static string GetPreviewUrl(
      Guid pageId,
      string language,
      double expirationTimeInHours,
      string providerName)
    {
      PageNode pageNode = PageManager.GetManager(providerName).GetPageNode(pageId);
      using (SiteRegion.FromSiteMapRoot(pageNode.RootNodeId))
      {
        CultureInfo cultureInfo = CultureInfo.GetCultureInfo(language);
        string str = RouteHelper.ResolveUrl(pageNode.GetBackendUrl("Preview", cultureInfo), UrlResolveOptions.Rooted);
        if (SystemManager.CurrentContext.AppSettings.Multilingual && cultureInfo != SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage)
          str = Url.Combine(str, language);
        string paramValue = SecurityManager.GetUserAuthenticationKey(new TimeSpan?(TimeSpan.FromHours(expirationTimeInHours)), str).UrlEncode();
        return UrlPath.ResolveAbsoluteUrl(Url.AppendUrlParameter(Url.AppendUrlParameter(Url.AppendUrlParameter(str, "sf-auth", paramValue), "sf_site", SystemManager.CurrentContext.CurrentSite.Id.ToString()), "sf_site_temp", true.ToString()));
      }
    }
  }
}
