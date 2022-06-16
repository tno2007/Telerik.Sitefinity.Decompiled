// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Composition.MessageEnhancer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Modules.Newsletters.Web;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Newsletters.Composition
{
  /// <summary>
  /// This class is used to enhance the content of the message with Sitefinity tokens.
  /// </summary>
  public static class MessageEnhancer
  {
    private static Regex srcRegex = new Regex("(?<=src\\=[\\x27\\x22])(?<Url>[^\\x27\\x22]*)(?=[\\x27\\x22])", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static Regex hrefRegex = new Regex("(?<=href\\=[\\x27\\x22])(?<Url>[^\\x27\\x22]*)(?=[\\x27\\x22])", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    public static string linkQueryString = "sf";
    internal const string CheckQueryString = "c";
    private static readonly string[] ignoreTrackingCodes = new string[2]
    {
      "#",
      "mailto"
    };

    public static string GetOpeningTracker(
      Campaign campaign,
      Subscriber subscriber,
      string rootUrl)
    {
      return MessageEnhancer.GetOpeningTracker(campaign, subscriber.ShortId, rootUrl);
    }

    public static string GetOpeningTracker(
      Campaign campaign,
      string subscriberShortId,
      string rootUrl)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<img src=\"");
      stringBuilder.Append(rootUrl);
      stringBuilder.Append((object) NewslettersLinkRouteHandler.linkPrefix);
      stringBuilder.Append("?");
      stringBuilder.Append(MessageEnhancer.linkQueryString);
      stringBuilder.Append("=");
      stringBuilder.Append(campaign.ShortId);
      stringBuilder.Append(subscriberShortId);
      stringBuilder.Append("\" style=\"width:1px; height:1px; display:none;\"");
      stringBuilder.Append("/>");
      return stringBuilder.ToString();
    }

    /// <summary>
    /// This method enhances the links inside of the message so that they can be tracked by the Newsletter module.
    /// </summary>
    /// <param name="campaign">The instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Campaign" /> type for which the message is being sent.</param>
    /// <param name="subscriber">The instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type to whom the message is being sent.</param>
    /// <param name="rawMessage">The instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Composition.RawMessageSource" /> type representing the raw source (html or plain text) of the message.</param>
    /// <param name="rootUrl">The root url of the website where Sitefinity is installed.</param>
    /// <returns>The body of the message with the enhanced links.</returns>
    public static string EnhanceLinks(
      Campaign campaign,
      Subscriber subscriber,
      RawMessageSource source,
      string rootUrl)
    {
      return MessageEnhancer.EnhanceLinks(campaign, subscriber.ShortId, source, rootUrl);
    }

    /// <summary>
    /// This method enhances the links inside of the message so that they can be tracked by the Newsletter module.
    /// </summary>
    /// <param name="campaign">The instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Campaign" /> type for which the message is being sent.</param>
    /// <param name="subscriberShortId">The short id of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.Subscriber" /> type to whom the message is being sent.</param>
    /// <param name="rawMessage">The instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Composition.RawMessageSource" /> type representing the raw source (html or plain text) of the message.</param>
    /// <param name="rootUrl">The root url of the website where Sitefinity is installed.</param>
    /// <returns>The body of the message with the enhanced links.</returns>
    public static string EnhanceLinks(
      Campaign campaign,
      string subscriberShortId,
      RawMessageSource source,
      string rootUrl)
    {
      return MessageEnhancer.EnahnceLinks(campaign, subscriberShortId, source.Source, rootUrl);
    }

    /// <summary>Gets the collection of all links used in the message.</summary>
    /// <param name="messageBody">The instance of the <see cref="T:Telerik.Sitefinity.Newsletters.Model.MessageBody" /> representing the message.</param>
    /// <returns>A list of all link urls found in the message.</returns>
    /// <remarks>Links are ordered in the way in which they are found.</remarks>
    public static IList<string> GetAllLinkUrls(MessageBody messageBody)
    {
      if (messageBody == null)
        throw new ArgumentNullException(nameof (messageBody));
      List<string> allLinkUrls = new List<string>();
      if (!string.IsNullOrEmpty(messageBody.BodyText))
      {
        foreach (Match match in MessageEnhancer.hrefRegex.Matches(messageBody.BodyText))
          allLinkUrls.Add(match.Value);
      }
      return (IList<string>) allLinkUrls;
    }

    public static void ResolveLinkQueryString(
      string queryString,
      out string campaignShortId,
      out string subscriberShortId,
      out string originalUrl)
    {
      campaignShortId = queryString.Sub(0, 5);
      subscriberShortId = queryString.Sub(6, 11);
      if (queryString.Length > 12)
        originalUrl = queryString.Substring(12);
      else
        originalUrl = string.Empty;
    }

    private static string EnahnceLinks(
      Campaign campaign,
      string subscriberShortId,
      string target,
      string rootUrl)
    {
      return MessageEnhancer.hrefRegex.Replace(target, (MatchEvaluator) (match => MessageEnhancer.ReplaceLink(match, campaign, subscriberShortId, rootUrl)));
    }

    private static string ReplaceLink(
      Match m,
      Campaign campaign,
      string subscriberShortId,
      string rootUrl)
    {
      string url = HttpUtility.UrlDecode(m.Value).Trim();
      if (((IEnumerable<string>) MessageEnhancer.ignoreTrackingCodes).Any<string>((Func<string, bool>) (code => url.StartsWith(code, StringComparison.OrdinalIgnoreCase))))
        return url;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0}{1}?{2}={3}{4}{5}", (object) rootUrl, (object) NewslettersLinkRouteHandler.linkPrefix, (object) MessageEnhancer.linkQueryString, (object) campaign.ShortId, (object) subscriberShortId, (object) MessageEnhancer.EncodeUrl(url));
      string hash = MessageEnhancer.ComputeHash(url);
      if (!hash.IsNullOrEmpty())
        stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "&{0}={1}", (object) "c", (object) HttpUtility.UrlEncode(hash));
      return stringBuilder.ToString();
    }

    /// <summary>
    /// If url contains placeholders html encode the url without placeholders, otherwise encode the entire url.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    internal static string EncodeUrl(string url)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string oldValue1 = "{|";
      string oldValue2 = "|}";
      string str1 = "|@@@@@|";
      if (url.IndexOf(oldValue1) > -1 && url.IndexOf(oldValue2) > -1 && url.IndexOf(oldValue1) < url.IndexOf(oldValue2))
      {
        url = url.Replace(oldValue1, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}{1}", (object) str1, (object) oldValue1));
        url = url.Replace(oldValue2, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}{1}", (object) oldValue2, (object) str1));
        string str2 = url;
        string[] separator = new string[1]{ str1 };
        foreach (string str3 in str2.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        {
          if (str3.StartsWith(oldValue1) && str3.EndsWith(oldValue2))
            stringBuilder.Append(str3);
          else
            stringBuilder.Append(HttpUtility.UrlEncode(str3));
        }
      }
      else
        stringBuilder.Append(HttpUtility.UrlEncode(url));
      return stringBuilder.ToString();
    }

    internal static string ComputeHash(string url)
    {
      string data = string.Empty;
      if (!RouteHelper.IsAbsoluteUrl(url))
        return string.Empty;
      string leftPart = new Uri(url).GetLeftPart(UriPartial.Authority);
      if (!leftPart.IsNullOrEmpty())
        data = leftPart;
      return SecurityManager.ComputeHash(data);
    }
  }
}
