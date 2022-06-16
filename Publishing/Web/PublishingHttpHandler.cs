// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.PublishingHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing.Web
{
  public class PublishingHttpHandler : IHttpHandler
  {
    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
    /// </returns>
    public bool IsReusable => true;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context)
    {
      string str1 = (string) context.Items[(object) PublishingRouteHandler.FeedUrlName];
      if (str1 == null)
        this.HandleInvalidUrl(context.Response);
      string searchablePipeSettingsUrl = this.GetSearchablePipeSettingsUrl(str1);
      string providerName = ManagerBase<PublishingDataProviderBase>.GetDefaultProviderName();
      string str2 = context.Request.QueryString["provider"];
      if (str2 != null)
        providerName = str2;
      RssPipeSettings rssPipeSettings = PublishingManager.GetManager(providerName).GetPipeSettings<RssPipeSettings>().Where<RssPipeSettings>((Expression<Func<RssPipeSettings, bool>>) (ps => ps.UrlName == searchablePipeSettingsUrl && ps.PublishingPoint.IsActive)).FirstOrDefault<RssPipeSettings>();
      if (rssPipeSettings == null)
        this.HandleInvalidUrl(context.Response);
      using (SiteRegion.FromSiteId(PublishingManager.GetSitesByPointFromCache(rssPipeSettings.PublishingPoint).FirstOrDefault<Guid>()))
      {
        if (SystemManager.CurrentContext.CurrentSite.IsOffline)
        {
          this.HandleInvalidUrl(context.Response);
        }
        else
        {
          IPipe pipe = PublishingSystemFactory.GetPipe(rssPipeSettings.PipeName);
          pipe.Initialize((PipeSettings) rssPipeSettings);
          this.SetPipeContext(pipe, str1, context.Request, rssPipeSettings.FormatSettings);
          this.HandleRequest(pipe, context.Response);
        }
      }
    }

    private string GetSearchablePipeSettingsUrl(string rawUrl)
    {
      int length = rawUrl.LastIndexOf(".");
      if (length != -1)
      {
        string str = rawUrl.Substring(length + 1);
        if (str.ToLower() == "rss" || str.ToLower() == "atom")
          return rawUrl.Substring(0, length);
      }
      return rawUrl;
    }

    protected virtual void SetPipeContext(
      IPipe rssFeed,
      string requestUrl,
      HttpRequest request,
      RssFormatOutputSettings originalSettings)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      if (rssFeed is RSSOutboundPipe)
        dictionary = ((RSSOutboundPipe) rssFeed).PipeContext;
      switch (originalSettings)
      {
        case RssFormatOutputSettings.SmartFeed:
          string header = request.Headers["Accept"];
          if (header.Contains("application/rss+xml") && !header.Contains("application/atom+xml"))
          {
            dictionary["FeedType"] = (object) RssFormatOutputSettings.RssOnly;
            break;
          }
          if (!header.Contains("application/rss+xml") && header.Contains("application/atom+xml"))
          {
            dictionary["FeedType"] = (object) RssFormatOutputSettings.AtomOnly;
            break;
          }
          dictionary["FeedType"] = (object) RssFormatOutputSettings.RssOnly;
          break;
        case RssFormatOutputSettings.RssAndAtom:
          if (requestUrl.ToLower().EndsWith(".atom"))
          {
            dictionary["FeedType"] = (object) RssFormatOutputSettings.AtomOnly;
            break;
          }
          dictionary["FeedType"] = (object) RssFormatOutputSettings.RssOnly;
          break;
      }
    }

    protected internal virtual void HandleRequest(IPipe pipe, HttpResponse response)
    {
      response.Clear();
      PublishingConfig publishingConfig = Config.Get<PublishingConfig>();
      IFeedFormatter feedFormatter = ObjectFactory.Resolve<IFeedFormatter>();
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      try
      {
        this.SetCulture(pipe);
        if (!publishingConfig.TransformRssXmlToHtml)
        {
          this.WriteXmlToResponse(feedFormatter.GetFeedFormatter(pipe), response);
        }
        else
        {
          byte[] transformedHtmlAsByteArray = ObjectFactory.Resolve<IPublishingXslTranslator>().GetTransformedHtmlAsByteArray(pipe);
          response.OutputStream.Write(transformedHtmlAsByteArray, 0, transformedHtmlAsByteArray.Length);
        }
      }
      catch (UnauthorizedAccessException ex)
      {
        throw new HttpException(403, (string) null);
      }
      finally
      {
        SystemManager.CurrentContext.Culture = culture;
      }
    }

    /// <summary>Writes the XML to response.</summary>
    /// <param name="syndicationFeedFormatter">The syndication feed formatter.</param>
    /// <param name="response">The response.</param>
    protected virtual void WriteXmlToResponse(
      SyndicationFeedFormatter syndicationFeedFormatter,
      HttpResponse response)
    {
      XmlWriterSettings settings = new XmlWriterSettings()
      {
        CheckCharacters = false
      };
      using (XmlWriter writer = XmlWriter.Create(response.OutputStream, settings))
      {
        syndicationFeedFormatter.WriteTo(writer);
        response.ContentType = !(syndicationFeedFormatter is Rss20FeedFormatter) ? "application/atom+xml" : "application/rss+xml";
        writer.Close();
      }
    }

    private void HandleInvalidUrl(HttpResponse response)
    {
      response.StatusCode = 404;
      response.End();
      response.Flush();
    }

    private void SetCulture(IPipe pipe)
    {
      if (!SystemManager.CurrentContext.AppSettings.Multilingual)
        return;
      SystemManager.CurrentContext.Culture = pipe.PipeSettings.LanguageIds.Count <= 0 ? SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage ?? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(pipe.PipeSettings.LanguageIds[0]);
    }
  }
}
