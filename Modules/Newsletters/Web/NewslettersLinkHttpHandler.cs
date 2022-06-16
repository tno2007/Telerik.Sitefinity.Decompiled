// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.NewslettersLinkHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web
{
  /// <summary>
  /// Http handler that handles requests for the  links sent through newsletters.
  /// </summary>
  public class NewslettersLinkHttpHandler : IHttpHandler
  {
    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
    public bool IsReusable => true;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    public void ProcessRequest(HttpContextBase context)
    {
      string originalUrl;
      string campaignShortId;
      string subscriberShortId;
      MessageEnhancer.ResolveLinkQueryString(context.Request.QueryString[MessageEnhancer.linkQueryString], out campaignShortId, out subscriberShortId, out originalUrl);
      try
      {
        NewslettersManager manager = NewslettersManager.GetManager();
        Campaign campaign = manager.GetIssues().Where<Campaign>((Expression<Func<Campaign, bool>>) (c => c.ShortId == campaignShortId)).Single<Campaign>();
        Subscriber subscriber = manager.GetSubscribers().FirstOrDefault<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.ShortId == subscriberShortId));
        if (RouteHelper.IsAbsoluteUrl(originalUrl) && !string.IsNullOrEmpty(originalUrl))
        {
          string redirectLinkUrl = NewslettersLinkHttpHandler.GenerateRedirectLinkUrl(subscriber, manager, originalUrl, campaign);
          if (NewslettersLinkHttpHandler.IsUrlSafe(originalUrl, context.Request.QueryString["c"]))
            context.Response.Redirect(redirectLinkUrl);
          else
            NewslettersLinkHttpHandler.RedirectNotice(redirectLinkUrl, context.Response);
        }
        else
          NewslettersLinkHttpHandler.TrackingImage(context.Response, manager, campaign, subscriber);
      }
      catch (ThreadAbortException ex)
      {
      }
      catch (Exception ex)
      {
        Log.Write((object) ex);
        if (!RouteHelper.IsAbsoluteUrl(originalUrl) || string.IsNullOrEmpty(originalUrl))
          return;
        NewslettersLinkHttpHandler.RedirectNotice(originalUrl, context.Response);
      }
    }

    internal static string GenerateRedirectLinkUrl(
      Subscriber subscriber,
      NewslettersManager newslettersManager,
      string originalUrl,
      Campaign campaign)
    {
      if (campaign.MessageBody.MessageBodyType == MessageBodyType.HtmlText)
        originalUrl = HttpUtility.HtmlDecode(originalUrl);
      if (subscriber != null)
      {
        LinkClickStat linkClickStat = newslettersManager.CreateLinkClickStat();
        linkClickStat.DateTimeClicked = DateTime.UtcNow;
        linkClickStat.Url = originalUrl.Trim();
        linkClickStat.CampaignId = campaign.Id;
        linkClickStat.SubscriberId = subscriber.Id;
        if (newslettersManager.GetOpenStats().Count<OpenStat>((Expression<Func<OpenStat, bool>>) (os => os.CampaignId == campaign.Id && os.SubscriberId == subscriber.Id)) == 0)
          NewslettersLinkHttpHandler.CreateOpenStat(newslettersManager, campaign, subscriber);
        NewslettersLinkHttpHandler.SetSuccessfulDelivery(newslettersManager, campaign, subscriber);
        newslettersManager.SaveChanges();
      }
      string redirectLinkUrl = originalUrl;
      if (campaign.UseGoogleTracking)
      {
        StringBuilder stringBuilder = new StringBuilder(originalUrl);
        if (originalUrl.IndexOf("?") > -1)
          stringBuilder.Append("&");
        else
          stringBuilder.Append("?");
        stringBuilder.Append("utm_source=");
        if (campaign.RootCampaign != null)
        {
          stringBuilder.Append(HttpUtility.UrlEncode(campaign.RootCampaign.Name));
          stringBuilder.Append("&utm_medium=newsletter&utm_campaign=");
          stringBuilder.Append(HttpUtility.UrlEncode(campaign.Name));
        }
        else
        {
          stringBuilder.Append(HttpUtility.UrlEncode(campaign.Name));
          stringBuilder.Append("&utm_medium=email&utm_campaign=");
          stringBuilder.Append(HttpUtility.UrlEncode(campaign.MessageSubject));
        }
        redirectLinkUrl = stringBuilder.ToString();
      }
      return redirectLinkUrl;
    }

    private static void TrackingImage(
      HttpResponseBase response,
      NewslettersManager newslettersManager,
      Campaign campaign,
      Subscriber subscriber)
    {
      if (subscriber != null)
      {
        NewslettersLinkHttpHandler.CreateOpenStat(newslettersManager, campaign, subscriber);
        NewslettersLinkHttpHandler.SetSuccessfulDelivery(newslettersManager, campaign, subscriber);
        newslettersManager.SaveChanges();
      }
      response.ContentType = "image/gif";
      response.AddHeader("Pragma-directive", "no-cache");
      response.AddHeader("Cache-directive", "no-cache");
      response.AddHeader("Cache-control", "no-cache");
      response.AddHeader("Pragma", "no-cache");
      response.AddHeader("Expires", "0");
      Assembly assembly = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly;
      Stream outputStream = response.OutputStream;
      Stream manifestResourceStream = assembly.GetManifestResourceStream("Telerik.Sitefinity.Resources.Images.tracker.gif");
      int length = (int) manifestResourceStream.Length;
      byte[] buffer = new byte[1024];
      using (Stream stream = manifestResourceStream)
      {
        int count;
        for (; length > 0 && (count = stream.Read(buffer, 0, buffer.Length)) > 0; length -= count)
          outputStream.Write(buffer, 0, count);
      }
    }

    private static bool IsUrlSafe(string url, string check) => !check.IsNullOrEmpty() && url.Length <= 2000 && MessageEnhancer.ComputeHash(url) == check;

    private static void RedirectNotice(string url, HttpResponseBase response)
    {
      url = ObjectFactory.Resolve<IHtmlSanitizer>().SanitizeUrl(url);
      response.Write(NewslettersLinkHttpHandler.GetRedirectNoticeTemplate().Replace("{{urlLink}}", HttpUtility.HtmlAttributeEncode(url)).Replace("{{urlText}}", HttpUtility.HtmlEncode(url)));
    }

    private static void CreateOpenStat(
      NewslettersManager manager,
      Campaign campaign,
      Subscriber subscriber)
    {
      OpenStat openStat = manager.CreateOpenStat();
      openStat.OpenedDate = DateTime.UtcNow;
      openStat.CampaignId = campaign.Id;
      openStat.SubscriberId = subscriber.Id;
    }

    private static void SetSuccessfulDelivery(
      NewslettersManager manager,
      Campaign campaign,
      Subscriber subscriber)
    {
      DeliveryEntry deliveryEntry = manager.GetDeliveryEntries().FirstOrDefault<DeliveryEntry>((Expression<Func<DeliveryEntry, bool>>) (entry => entry.CampaignId == campaign.Id && entry.DeliverySubscriber == subscriber));
      if (deliveryEntry != null)
      {
        if (deliveryEntry.DeliveryStatus == DeliveryStatus.Success)
          return;
      }
      else
      {
        deliveryEntry = manager.CreateDeliveryEntry();
        deliveryEntry.CampaignId = campaign.Id;
        deliveryEntry.DeliverySubscriber = subscriber;
      }
      deliveryEntry.DeliveryDate = DateTime.UtcNow;
      deliveryEntry.DeliveryStatus = DeliveryStatus.Success;
    }

    private static string GetRedirectNoticeTemplate() => ControlUtilities.GetSitefinityTextResource("Telerik.Sitefinity.Resources.Templates.RedirectNotice.html");
  }
}
