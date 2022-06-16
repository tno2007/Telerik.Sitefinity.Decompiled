// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.CustomErrorPages.ErrorPagesHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Events;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Web.OutputCache;

namespace Telerik.Sitefinity.Web.CustomErrorPages
{
  internal class ErrorPagesHandler
  {
    internal const string DefaultErrorPageKey = "Default";
    internal const string CustomErrorPageContextItem = "sf_custom_error_page";
    private const string ApplicationRelativePrefix = "~/";
    private const string ErrorPageStatusHeader = "sf-status-code";
    private const string ContentTypeHtml = "text/html";
    private const string InitialRequestUrlTitle = "initialRequestUrl";
    private const char UrlPathSegmentsDelimiter = '/';
    private const char UrlPathQueryDelimiter = '?';
    private Dictionary<HttpErrorCode, ErrorPagesHandler.ErrorPage> errorPages;
    private CustomErrorPagesMode? mode;
    private ErrorPagesHandler.ErrorPage defaultErrorPage;
    private static readonly object ErrorPagesLock = new object();
    private static readonly object DefaultPageLock = new object();

    public ErrorPagesHandler() => Bootstrapper.Bootstrapped += new EventHandler<EventArgs>(this.Bootstrapper_Bootstrapped);

    /// <summary>Processes the error page request.</summary>
    public void ProcessRequest()
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (this.Mode == CustomErrorPagesMode.Disabled || this.Mode == CustomErrorPagesMode.RemoteOnly && currentHttpContext.Request.IsLocal || currentHttpContext.Response.ContentType != null && !currentHttpContext.Response.ContentType.Equals("text/html", StringComparison.InvariantCultureIgnoreCase))
        return;
      if (currentHttpContext.Response.StatusCode == 200)
      {
        string header = currentHttpContext.Request.Headers["sf-status-code"];
        int result;
        if (header == null || !int.TryParse(header, out result) || !Enum.IsDefined(typeof (HttpErrorCode), (object) result) || currentHttpContext.Response.HeadersWritten)
          return;
        currentHttpContext.Items[(object) "sf_custom_error_page"] = (object) true;
        currentHttpContext.Response.StatusCode = result;
        currentHttpContext.Response.TrySkipIisCustomErrors = true;
        SitefinityOutputCacheProvider.SetIgnoreStatusCodeValidation();
      }
      else
      {
        HttpErrorCode statusCode = (HttpErrorCode) currentHttpContext.Response.StatusCode;
        if (!Enum.IsDefined(typeof (HttpErrorCode), (object) statusCode))
          return;
        ErrorPagesHandler.ErrorPage errorPage;
        if (this.ErrorPages.ContainsKey(statusCode))
        {
          errorPage = this.ErrorPages[statusCode];
        }
        else
        {
          if (this.DefaultErrorPage == null)
            return;
          errorPage = this.DefaultErrorPage;
        }
        string errorPageLocation = this.GetErrorPageLocation(currentHttpContext, errorPage);
        if (string.IsNullOrEmpty(errorPageLocation) && this.DefaultErrorPage != null && this.DefaultErrorPage != errorPage)
        {
          errorPage = this.DefaultErrorPage;
          errorPageLocation = this.GetErrorPageLocation(currentHttpContext, errorPage);
        }
        if (string.IsNullOrEmpty(errorPageLocation))
          return;
        string str1 = string.Format("{0}{1}=", (object) '?', (object) "initialRequestUrl");
        string str2 = currentHttpContext.Request.Url.Query;
        while (str2.StartsWith(str1))
        {
          str2 = HttpUtility.UrlDecode(str2.Substring(str1.Length));
          string str3;
          if (str2.Contains(str1))
          {
            str3 = str2.Substring(0, str2.IndexOf(str1));
            str2 = str2.Substring(str2.IndexOf(str1));
          }
          else
            str3 = str2;
          if (str3.EndsWith(VirtualPathUtility.ToAbsolute(errorPageLocation)))
            return;
        }
        string str4 = HttpUtility.UrlEncode(currentHttpContext.Request.Url.AbsoluteUri);
        string str5 = str1 + str4;
        string str6 = errorPageLocation + str5;
        if (errorPage.Redirect || !VirtualPathUtility.IsAppRelative(str6))
        {
          currentHttpContext.Response.Redirect(str6);
        }
        else
        {
          NameValueCollection headers = currentHttpContext.Request.Headers;
          headers.Add("sf-status-code", ((int) statusCode).ToString((IFormatProvider) NumberFormatInfo.InvariantInfo));
          currentHttpContext.Server.TransferRequest(str6, true, (string) null, headers, true);
        }
      }
    }

    private Dictionary<HttpErrorCode, ErrorPagesHandler.ErrorPage> ErrorPages
    {
      get
      {
        if (this.errorPages == null)
        {
          lock (ErrorPagesHandler.ErrorPagesLock)
          {
            if (this.errorPages == null)
            {
              this.errorPages = new Dictionary<HttpErrorCode, ErrorPagesHandler.ErrorPage>();
              foreach (ErrorPageDataElement errorPageDataElement in (IEnumerable<ErrorPageDataElement>) Config.Get<PagesConfig>().ErrorPages.ErrorTypes.Values)
              {
                HttpErrorCode result;
                if (Enum.TryParse<HttpErrorCode>(errorPageDataElement.HttpStatusCode, out result) && !this.errorPages.ContainsKey(result))
                  this.errorPages.Add(result, new ErrorPagesHandler.ErrorPage()
                  {
                    Redirect = errorPageDataElement.RedirectToErrorPage,
                    Location = errorPageDataElement.PageName
                  });
              }
            }
          }
        }
        return this.errorPages;
      }
    }

    private CustomErrorPagesMode Mode
    {
      get
      {
        if (!this.mode.HasValue)
          this.mode = new CustomErrorPagesMode?(Config.Get<PagesConfig>().ErrorPages.Mode);
        return this.mode.Value;
      }
    }

    private ErrorPagesHandler.ErrorPage DefaultErrorPage
    {
      get
      {
        if (this.defaultErrorPage == null)
        {
          lock (ErrorPagesHandler.DefaultPageLock)
          {
            if (this.defaultErrorPage == null)
            {
              ErrorPageDataElement errorType = Config.Get<PagesConfig>().ErrorPages.ErrorTypes["Default"];
              if (errorType != null)
                this.defaultErrorPage = new ErrorPagesHandler.ErrorPage()
                {
                  Location = errorType.PageName,
                  Redirect = errorType.RedirectToErrorPage
                };
            }
          }
        }
        return this.defaultErrorPage;
      }
    }

    private string GetErrorPageLocation(
      HttpContextBase context,
      ErrorPagesHandler.ErrorPage errorPage)
    {
      string lowerInvariant = errorPage.Location.TrimStart('/').ToLowerInvariant();
      string errorPageLocation = (string) null;
      if (lowerInvariant.StartsWith("~/"))
      {
        SiteMapNode siteMapNode = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNode(lowerInvariant.TrimStart('~'));
        if (siteMapNode != null && siteMapNode is PageSiteNode pageSiteNode && pageSiteNode.IsPublished())
          errorPageLocation = VirtualPathUtility.ToAppRelative(siteMapNode.Url);
      }
      else
      {
        string url = context.Request.RawUrl.ToLowerInvariant();
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        if (multisiteContext != null)
          url = multisiteContext.UnresolveUrlAndApplyDetectedSite(url);
        string requestUrl = ObjectFactory.Resolve<UrlLocalizationService>().UnResolveUrl(url);
        if (!requestUrl.StartsWith('/'.ToString()))
          requestUrl = string.Format("{0}{1}", (object) '/', (object) requestUrl);
        SiteMapNode siteMapNode1 = this.GetCurrentNode(requestUrl);
        if (siteMapNode1 is PageSiteNode pageSiteNode1)
        {
          if (pageSiteNode1.UnresolvedUrl.TrimStart('~').Equals(requestUrl, StringComparison.OrdinalIgnoreCase))
            siteMapNode1 = siteMapNode1.ParentNode;
        }
        SiteMapProvider currentProvider = SitefinitySiteMap.GetCurrentProvider();
        do
        {
          string rawUrl = string.Format("{0}{1}", (object) '/', (object) lowerInvariant);
          if (siteMapNode1?.ParentNode != null && siteMapNode1 is PageSiteNode pageSiteNode2)
            rawUrl = pageSiteNode2.UnresolvedUrl.TrimStart('~').TrimEnd('/') + rawUrl;
          if (!rawUrl.StartsWith('/'.ToString()))
            rawUrl = string.Format("{0}{1}", (object) '/', (object) rawUrl);
          SiteMapNode siteMapNode2 = currentProvider is SiteMapBase ? ((SiteMapBase) currentProvider).FindSiteMapNode(rawUrl, false) : currentProvider.FindSiteMapNode(rawUrl);
          if (siteMapNode2 != null && siteMapNode2.IsAccessibleToUser(HttpContext.Current) && siteMapNode2.Url.EndsWith(string.Format("{0}{1}", (object) '/', (object) lowerInvariant), StringComparison.OrdinalIgnoreCase) && siteMapNode2 is PageSiteNode pageSiteNode3 && pageSiteNode3.IsPublished())
          {
            errorPageLocation = VirtualPathUtility.ToAppRelative(siteMapNode2.Url);
            break;
          }
          siteMapNode1 = siteMapNode1?.ParentNode;
        }
        while (siteMapNode1 != null);
      }
      return errorPageLocation;
    }

    private SiteMapNode GetCurrentNode(string requestUrl) => (SitefinitySiteMap.GetCurrentProvider().FindSiteMapNode(requestUrl) ?? (SiteMapNode) SiteMapBase.GetLastFoundNode()) ?? SitefinitySiteMap.GetCurrentProvider().RootNode;

    private void Bootstrapper_Bootstrapped(object sender, EventArgs e) => EventHub.Subscribe<ConfigEvent>(new SitefinityEventHandler<ConfigEvent>(this.ConfigUpdatedEventHandler));

    private void ConfigUpdatedEventHandler(ConfigEvent configEvent)
    {
      if (!Config.Get<PagesConfig>().TagName.Equals(configEvent.ConfigName))
        return;
      this.mode = new CustomErrorPagesMode?(Config.Get<PagesConfig>().ErrorPages.Mode);
      lock (ErrorPagesHandler.ErrorPagesLock)
        this.errorPages = (Dictionary<HttpErrorCode, ErrorPagesHandler.ErrorPage>) null;
      lock (ErrorPagesHandler.DefaultPageLock)
        this.defaultErrorPage = (ErrorPagesHandler.ErrorPage) null;
    }

    private class ErrorPage
    {
      public bool Redirect { get; set; }

      public string Location { get; set; }
    }
  }
}
