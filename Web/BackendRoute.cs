// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.BackendRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Libraries.UserFiles;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents a Sitefinity backend route.</summary>
  public class BackendRoute : SitefinityRoute
  {
    private static readonly string backendRootPath = "~/" + "Sitefinity" + "/";

    protected override bool ProcessRedirects(
      HttpContextBase httpContext,
      ref PageSiteNode node,
      bool isAdditionalUrl,
      string[] urlParams)
    {
      string backendRootPath = BackendRoute.BackendRootPath;
      string str = backendRootPath + "default.aspx";
      if (backendRootPath.Equals(httpContext.Request.AppRelativeCurrentExecutionFilePath, StringComparison.OrdinalIgnoreCase) || str.Equals(httpContext.Request.AppRelativeCurrentExecutionFilePath, StringComparison.OrdinalIgnoreCase) || !this.IsNodeAccessible(node))
      {
        string key = Config.Get<PagesConfig>().BackendHomePageId.ToString();
        SiteMapNode siteMapNodeFromKey = node.Provider.FindSiteMapNodeFromKey(key);
        if (siteMapNodeFromKey != null)
        {
          httpContext.Response.Redirect(siteMapNodeFromKey.Url, true);
          return false;
        }
      }
      bool flag = base.ProcessRedirects(httpContext, ref node, isAdditionalUrl, urlParams);
      if (node.IsBackend)
      {
        foreach (IRedirectStrategy redirectStrategy in ObjectFactory.Container.ResolveAll(typeof (IRedirectStrategy)))
        {
          string url = redirectStrategy.Redirect(httpContext, (SiteMapNode) node);
          if (!string.IsNullOrEmpty(url))
          {
            httpContext.Response.Redirect(url, false);
            if (httpContext.ApplicationInstance != null)
              httpContext.ApplicationInstance.CompleteRequest();
            return true;
          }
        }
      }
      return flag;
    }

    protected override bool VerifyRequest(HttpContextBase httpContext)
    {
      if (base.VerifyRequest(httpContext))
      {
        string executionFilePath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
        if (executionFilePath.StartsWith(BackendRoute.BackendRootPath, StringComparison.OrdinalIgnoreCase))
          return true;
        if (executionFilePath.Equals(BackendRoute.BackendRootPath.Remove(BackendRoute.BackendRootPath.Length - 1), StringComparison.OrdinalIgnoreCase))
        {
          httpContext.Response.Redirect(BackendRoute.BackendRootPath, true);
          return true;
        }
      }
      return false;
    }

    protected override SiteMapBase GetSiteMapProvider() => BackendSiteMap.GetCurrentProvider();

    protected override void OnNodePreProcessing(HttpContextBase httpContext, PageSiteNode siteNode)
    {
      base.OnNodePreProcessing(httpContext, siteNode);
      bool? nullable = siteNode.UseSiteContext();
      if (nullable.HasValue)
      {
        SystemManager.CurrentContext.IsGlobalContext = !nullable.Value;
      }
      else
      {
        bool flag = ((SystemManager.CurrentContext.IsGlobalContext ? 1 : 0) | (siteNode.Id == MultisiteModule.HomePageId || siteNode.Id == SiteInitializer.UsersPageId && !SecurityManager.AllowSeparateUsersPerSite && !SystemManager.CurrentContext.IsOneSiteMode || siteNode.Id == SiteInitializer.RolesPageId || siteNode.Id == SiteInitializer.AdvancedSettingsNodeId || siteNode.Id == SiteInitializer.BasicSettingsNodeId || siteNode.Id == UserFilesConstants.HomePageId || siteNode.Id == SiteInitializer.ContentLocationsPageId ? 1 : (siteNode.Id == SiteInitializer.MarkedItemsPageId ? 1 : 0))) != 0;
        if (!flag)
        {
          for (PageSiteNode parentNode = siteNode.ParentNode as PageSiteNode; !flag && parentNode != null; parentNode = parentNode.ParentNode as PageSiteNode)
            flag = parentNode.Id == SiteInitializer.SystemNodeId || parentNode.Id == SiteInitializer.ToolsNodeId || parentNode.Id == SiteInitializer.ConnectivityPageNodeId;
        }
        if (!flag)
          return;
        SystemManager.CurrentContext.IsGlobalContext = true;
      }
    }

    protected override bool LanguageFallback => true;

    private bool IsNodeAccessible(PageSiteNode node) => string.IsNullOrEmpty(node.ModuleName) || SystemManager.IsModuleAccessible(node.ModuleName);

    internal static string BackendRootPath => BackendRoute.backendRootPath;
  }
}
