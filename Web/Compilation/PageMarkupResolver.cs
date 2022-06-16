// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.PageMarkupResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Compilation.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web.Compilation
{
  internal class PageMarkupResolver
  {
    internal static IList<PageMarkupModel> GetPageMarkup(
      Guid rootId,
      IList<Guid> nodeIds)
    {
      List<PageMarkupModel> result = new List<PageMarkupModel>();
      Action<SiteMapBase> action = (Action<SiteMapBase>) (siteMap =>
      {
        foreach (Guid nodeId in (IEnumerable<Guid>) nodeIds)
        {
          string key = PageSiteNode.GetKey(nodeId);
          if (siteMap.FindSiteMapNodeFromKey(key) is PageSiteNode siteMapNodeFromKey2)
            result.AddRange((IEnumerable<PageMarkupModel>) PageMarkupResolver.GetPageMarkup(RouteHelper.GetFirstPageDataNode(siteMapNodeFromKey2, true)));
        }
      });
      if (rootId == SiteInitializer.BackendRootNodeId)
      {
        SiteMapBase currentProvider = BackendSiteMap.GetCurrentProvider();
        action(currentProvider);
      }
      else
      {
        using (SiteRegion.FromSiteMapRoot(rootId))
        {
          SiteMapBase currentProvider = SiteMapBase.GetCurrentProvider() as SiteMapBase;
          action(currentProvider);
        }
      }
      return (IList<PageMarkupModel>) result;
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    internal static IList<PageMarkupModel> GetPageMarkup(PageSiteNode siteNode)
    {
      List<PageMarkupModel> pageMarkup = new List<PageMarkupModel>();
      if (siteNode != null)
      {
        object dataToken = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["SiteMapNode"];
        foreach (CultureInfo language in PageMarkupResolver.GetLanguages(siteNode))
        {
          CultureInfo lang = language;
          using (new CultureRegion(lang))
          {
            string currentTheme = ThemeController.GetCurrentTheme(HttpContext.Current.Request.RequestContext.HttpContext, siteNode.Theme, siteNode.IsBackend);
            string virtualPath = Telerik.Sitefinity.Web.PageRouteHandler.BuildVirtualPath(siteNode, currentTheme, siteNode.IsMultilingual, (RouteData) null);
            Stream virtualFileStream = (Stream) null;
            string str1 = siteNode.Url;
            if (!RouteHelper.IsAbsoluteUrl(str1))
            {
              str1 = SystemManager.CurrentContext.ResolveUrl(str1);
              if (!RouteHelper.IsAbsoluteUrl(str1))
                str1 = UrlPath.ResolveAbsoluteUrl(siteNode.Url);
            }
            Guid siteId = SystemManager.CurrentContext.CurrentSite.Id;
            string str2 = string.Format("{0}?{1}={2}", (object) str1, (object) "sf_site", (object) siteId);
            string str3 = virtualPath;
            string str4;
            string str5;
            if (siteNode.Framework == PageTemplateFramework.Mvc)
            {
              str4 = virtualPath.Replace("~/SFMVCPageService/", string.Empty);
              str5 = "~/SFMVCPageService/".Replace("/", string.Empty).Replace("~", string.Empty);
            }
            else
            {
              str4 = virtualPath.Replace("~/SFPageService/", string.Empty);
              str5 = "~/SFPageService/".Replace("/", string.Empty).Replace("~", string.Empty);
            }
            PageMarkupModel pageMarkupModel = new PageMarkupModel();
            pageMarkupModel.Key = siteNode.Key;
            pageMarkupModel.CultureName = lang.Name;
            pageMarkupModel.FileName = str4;
            pageMarkupModel.VersionKey = siteNode.VersionKey;
            pageMarkupModel.VirtualPath = virtualPath;
            pageMarkupModel.VirtualDirectory = str5;
            pageMarkupModel.Url = str2;
            PageMarkupModel model = pageMarkupModel;
            pageMarkup.Add(model);
            try
            {
              SystemManager.RunWithElevatedPrivilege((SystemManager.RunWithElevatedPrivilegeDelegate) (parameters =>
              {
                using (SiteRegion.FromSiteId(siteId))
                {
                  using (new CultureRegion(lang))
                  {
                    HttpContext.Current.Request.RequestContext.RouteData.DataTokens["SiteMapNode"] = (object) siteNode;
                    virtualFileStream = VirtualPathManager.OpenFile(virtualPath);
                    using (StreamReader streamReader = new StreamReader(virtualFileStream))
                      model.Markup = streamReader.ReadToEnd();
                  }
                }
              }), (object[]) null, str1);
            }
            catch (Exception ex)
            {
              model.ErrorMessage = ex.Message;
              Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
            }
          }
        }
      }
      return (IList<PageMarkupModel>) pageMarkup;
    }

    private static CultureInfo[] GetLanguages(PageSiteNode siteNode)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (!siteNode.IsBackend && appSettings.Multilingual)
        return siteNode.AvailableLanguages;
      if (siteNode.IsBackend && ((IEnumerable<CultureInfo>) appSettings.DefinedBackendLanguages).Count<CultureInfo>() > 1)
        return appSettings.DefinedBackendLanguages;
      return new CultureInfo[1]
      {
        CultureInfo.InvariantCulture
      };
    }
  }
}
