// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SitefinityTokenIdCookieHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security.Claims
{
  internal class SitefinityTokenIdCookieHelper
  {
    /// <summary>
    /// Gets the sitefinity specific cookie by adding the port as prefix (in case it's not the default one)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    internal static string GetSitefinityCookieName(string name, HttpContextBase context)
    {
      string cookieName1 = SitefinityTokenIdCookieHelper.GenerateCookieName(context.Request.Url.IsDefaultPort ? string.Empty : context.Request.Url.Port.ToString(), name);
      if (!SitefinityTokenIdCookieHelper.CookieExists(cookieName1, context))
      {
        Telerik.Sitefinity.Services.SiteUrlSettings siteUrlSettings = Config.Get<SystemConfig>().SiteUrlSettings;
        if (siteUrlSettings.EnableNonDefaultSiteUrlSettings)
        {
          string cookieName2 = SitefinityTokenIdCookieHelper.GenerateCookieName(siteUrlSettings.NonDefaultHttpPort, name);
          if (SitefinityTokenIdCookieHelper.CookieExists(cookieName2, context))
            return cookieName2;
          string cookieName3 = SitefinityTokenIdCookieHelper.GenerateCookieName(siteUrlSettings.NonDefaultHttpsPort, name);
          if (SitefinityTokenIdCookieHelper.CookieExists(cookieName3, context))
            return cookieName3;
        }
      }
      return cookieName1;
    }

    private static string GenerateCookieName(string port, string name) => string.Format("{0}{1}", (object) port, (object) name);

    private static bool CookieExists(string name, HttpContextBase context) => context.Request.Cookies[name] != null;
  }
}
