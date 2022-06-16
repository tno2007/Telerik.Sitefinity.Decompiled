// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.TrackingReporters.CustomErrorPagesReporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.UsageTracking.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.UsageTracking.TrackingReporters
{
  internal class CustomErrorPagesReporter
  {
    private const string ApplicationRelativePrefix = "~/";
    private char[] urlSegmentsDelimiter = new char[1]{ '/' };

    internal CustomErrorPagesReportModel GetCustomErrorPagesReport()
    {
      CustomErrorPagesReportModel errorPagesReport = new CustomErrorPagesReportModel();
      ErrorPageElement errorPages = Config.Get<PagesConfig>().ErrorPages;
      string str = errorPages.Mode.ToString();
      errorPagesReport.Mode = str;
      List<ErrorPageDataElement> source = new List<ErrorPageDataElement>((IEnumerable<ErrorPageDataElement>) errorPages.ErrorTypes.Values);
      errorPagesReport.ErrorTypes = new List<object>();
      errorPagesReport.ErrorTypes.AddRange((IEnumerable<object>) source.Select<ErrorPageDataElement, CustomErrorPageReportModel>((Func<ErrorPageDataElement, CustomErrorPageReportModel>) (p =>
      {
        IQueryable<PageSiteNode> siteMapNodes = this.GetSiteMapNodes(p);
        return new CustomErrorPageReportModel()
        {
          StatusCode = p.HttpStatusCode,
          Redirect = p.RedirectToErrorPage,
          ErrorPagesCount = siteMapNodes.Count<PageSiteNode>(),
          SitesContainingErrorPages = siteMapNodes.Select<PageSiteNode, Guid>((Expression<Func<PageSiteNode, Guid>>) (n => n.RootKey)).Distinct<Guid>().Count<Guid>()
        };
      })));
      return errorPagesReport;
    }

    private IQueryable<PageSiteNode> GetSiteMapNodes(string url)
    {
      List<PageSiteNode> source = new List<PageSiteNode>();
      IEnumerable<string> strings = SystemManager.CurrentContext.GetSites().Select<ISite, string>((Func<ISite, string>) (s => s.SiteMapName));
      if (url.StartsWith("~/"))
      {
        url = url.Substring("~/".Length);
        foreach (string rootName in strings)
        {
          PageSiteNode siteMapNode = (PageSiteNode) SiteMapBase.GetSiteMapProvider(rootName).FindSiteMapNode(url);
          if (siteMapNode != null && siteMapNode.IsPublished())
            source.Add(siteMapNode);
        }
      }
      else
      {
        string[] array = ((IEnumerable<string>) url.Split(this.UrlSegmentsDelimiter, StringSplitOptions.RemoveEmptyEntries)).Reverse<string>().ToArray<string>();
        foreach (string rootName in strings)
        {
          foreach (PageSiteNode allNode in SiteMapBase.GetSiteMapProvider(rootName).RootNode.GetAllNodes())
          {
            if (allNode.IsPublished())
            {
              int segmentsIndex = 0;
              PageSiteNode pageSiteNode = this.SearchUp(allNode, segmentsIndex, array);
              if (pageSiteNode != null)
                source.Add(pageSiteNode);
            }
          }
        }
      }
      return source.AsQueryable<PageSiteNode>();
    }

    private IQueryable<PageSiteNode> GetSiteMapNodes(
      ErrorPageDataElement errorPageDataElement)
    {
      return this.GetSiteMapNodes(errorPageDataElement.PageName);
    }

    private PageSiteNode SearchUp(
      PageSiteNode node,
      int segmentsIndex,
      string[] urlSegments)
    {
      if (node == null || segmentsIndex >= urlSegments.Length || !node.UrlName.Equals(urlSegments[segmentsIndex], StringComparison.OrdinalIgnoreCase))
        return (PageSiteNode) null;
      return segmentsIndex == urlSegments.Length - 1 ? node : this.SearchUp(node.ParentNode as PageSiteNode, ++segmentsIndex, urlSegments);
    }

    private char[] UrlSegmentsDelimiter => this.urlSegmentsDelimiter;
  }
}
