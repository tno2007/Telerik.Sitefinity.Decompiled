// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Utilities.LinkParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Web;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Web.Utilities
{
  /// <summary>
  /// Helper class for resolving and unresolving dynamic links in HTML formatted text.
  /// </summary>
  public static class LinkParser
  {
    internal const string UnresolvedLinkItemsSeparator = "%7C";
    internal const string UnresolvedLinkOptionsSeparator = "%3A";
    private const string AnchorLinkUrlAttributeName = "href";
    /// <summary>
    /// Name of the HTML tag attribute used to put information for URL resolving/unresolving
    /// </summary>
    public const string InternalLinkAttrebuteName = "sfref";
    /// <summary>
    /// Name of the html attribute applied to tags that participate in resolving/unresolving urls.
    /// The value of the attribute, when present, should hold the query part of the url
    /// </summary>
    public const string InternalLinkQueryStringAttributeName = "sfqs";
    /// <summary>
    /// Name of the html anchor used when a link can't be resolved
    /// </summary>
    public const string LinkNotResolvedAchhorName = "#sf404";
    /// <summary>
    /// String literal that is used instead of an actual url whenever an item's url cannot be resolved
    /// </summary>
    public const string LinkNotResolved = "#Link.Not.Resolved#";
    private const string MailToPrefix = "mailto:";
    private static readonly Regex EmailRegExpr = new Regex("^[a-zA-Z0-9.!#$%&'*\\+\\-/=?^_`{|}~]+@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,63}$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static Dictionary<string, int> tagsTopSkip = new Dictionary<string, int>();
    internal static readonly ConcurrentProperty<HashSet<string>> ExternalBlobPathCache = new ConcurrentProperty<HashSet<string>>(new Func<HashSet<string>>(LinkParser.BuildExternalBlobPathCache));

    /// <summary>
    /// Replaces all dynamic links in the specified HTML text with their actual URL paths.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <returns>Resolved HTML text.</returns>
    public static string ResolveLinks(string html) => LinkParser.ParseHtml(html, (GetItemUrl) null, (ResolveUrl) null, true, false, false, (ProcessChunk) null);

    /// <summary>
    /// Replaces all dynamic links in the specified HTML text with their actual URL paths.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <param name="resolveAsAbsoluteUrl">
    /// If true, URLs are resolved as absolute paths, including protocol and domain name.
    /// </param>
    /// <returns>Resolved HTML text.</returns>
    public static string ResolveLinks(string html, bool resolveAsAbsoluteUrl) => LinkParser.ParseHtml(html, (GetItemUrl) null, (ResolveUrl) null, true, false, resolveAsAbsoluteUrl, (ProcessChunk) null);

    /// <summary>
    /// Replaces all dynamic links in the specified HTML text with their actual URL paths.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <param name="preserveOriginalValue">
    /// If true the original value of altered attributes is
    /// preserved in additional attribute named sfref.
    /// </param>
    /// <param name="resolveAsAbsoluteUrl">
    /// If true, URLs are resolved as absolute paths, including protocol and domain name.
    /// </param>
    /// <returns>Resolved HTML text.</returns>
    public static string ResolveLinks(
      string html,
      bool preserveOriginalValue,
      bool resolveAsAbsoluteUrl)
    {
      return LinkParser.ParseHtml(html, (GetItemUrl) null, (ResolveUrl) null, true, preserveOriginalValue, resolveAsAbsoluteUrl, (ProcessChunk) null);
    }

    /// <summary>
    /// Replaces all dynamic links in the specified HTML text with their actual URL paths.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <param name="itemUrl">
    /// Delegate for processing item information. Must return the item URL.
    /// </param>
    /// <returns>Resolved HTML text.</returns>
    public static string ResolveLinks(string html, GetItemUrl itemUrl) => LinkParser.ParseHtml(html, itemUrl, (ResolveUrl) null, true, false, false, (ProcessChunk) null);

    /// <summary>
    /// Replaces all dynamic links in the specified HTML text with their actual URL paths.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <param name="resolveUrl">
    /// Delegate for processing unresolved URL. Must return resolved URL.
    /// </param>
    /// <returns>Resolved HTML text.</returns>
    public static string ResolveLinks(string html, ResolveUrl resolveUrl) => LinkParser.ParseHtml(html, (GetItemUrl) null, resolveUrl, true, false, false, (ProcessChunk) null);

    /// <summary>
    /// Replaces all dynamic links in the specified HTML text with their actual URL paths.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <param name="itemUrl">
    /// Delegate for processing item information. Must return the item URL.
    /// </param>
    /// <param name="resolveUrl">
    /// Delegate for processing unresolved URL. Must return resolved URL.
    /// </param>
    /// <param name="preserveOriginalValue">
    /// If true the original value of altered attributes is
    /// preserved in additional attribute named sfref.
    /// </param>
    /// <returns>Resolved HTML text.</returns>
    public static string ResolveLinks(
      string html,
      GetItemUrl itemUrl,
      ResolveUrl resolveUrl,
      bool preserveOriginalValue)
    {
      return LinkParser.ParseHtml(html, itemUrl, resolveUrl, true, preserveOriginalValue, false, (ProcessChunk) null);
    }

    /// <summary>
    /// Replaces all dynamic links in the specified HTML text with their actual URL paths.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <param name="itemUrl">
    /// Delegate for processing item information. Must return the item URL.
    /// </param>
    /// <param name="resolveUrl">
    /// Delegate for processing unresolved URL. Must return resolved URL.
    /// </param>
    /// <param name="preserveOriginalValue">
    /// If true the original value of altered attributes is
    /// preserved in additional attribute named sfref.
    /// </param>
    /// <param name="resolveAsAbsoluteUrl">
    /// If true, URLs are resolved as absolute paths, including protocol and domain name.
    /// </param>
    /// <returns>Resolved HTML text.</returns>
    public static string ResolveLinks(
      string html,
      GetItemUrl itemUrl,
      ResolveUrl resolveUrl,
      bool preserveOriginalValue,
      bool resolveAsAbsoluteUrl)
    {
      return LinkParser.ParseHtml(html, itemUrl, resolveUrl, true, preserveOriginalValue, resolveAsAbsoluteUrl, (ProcessChunk) null);
    }

    /// <summary>
    /// Replaces all dynamic links in the specified HTML text with their actual URL paths.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <param name="itemUrl">
    /// Delegate for processing item information. Must return the item URL.
    /// </param>
    /// <param name="resolveUrl">
    /// Delegate for processing unresolved URL. Must return resolved URL.
    /// </param>
    /// <param name="preserveOriginalValue">
    /// If true the original value of altered attributes is
    /// preserved in additional attribute named sfref.
    /// </param>
    /// <param name="resolveAsAbsoluteUrl">
    /// If true, URLs are resolved as absolute paths, including protocol and domain name.
    /// </param>
    /// <param name="processChunk">
    /// Delegate for processing html chunk. Returns true if the chunk was modified.
    /// </param>
    /// <returns>Resolved HTML text.</returns>
    public static string ResolveLinks(
      string html,
      GetItemUrl itemUrl,
      ResolveUrl resolveUrl,
      bool preserveOriginalValue,
      bool resolveAsAbsoluteUrl,
      ProcessChunk processChunk)
    {
      return LinkParser.ParseHtml(html, itemUrl, resolveUrl, true, preserveOriginalValue, resolveAsAbsoluteUrl, processChunk);
    }

    /// <summary>
    /// Replaces actual URLs with unresolved, starting with ~/, or item information.
    /// This method works only if the text was previously resolved with preserveOriginalValue parameter set true.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <returns>Unresolved HTML text.</returns>
    public static string UnresolveLinks(string html) => LinkParser.ParseHtml(html, (GetItemUrl) null, (ResolveUrl) null, false, false, false, (ProcessChunk) null);

    /// <summary>
    /// Determines if a given HTML text contains dynamic links.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <returns>Gets a value indicating if the specified HTML text contains dynamic links.</returns>
    public static bool ContainsDynamicLinks(string html)
    {
      if (string.IsNullOrEmpty(html))
        return false;
      HtmlParser htmlParser = new HtmlParser(html);
      htmlParser.SetChunkHashMode(false);
      HtmlChunk next;
      while ((next = htmlParser.ParseNext()) != null)
      {
        if (next.Type == HtmlChunkType.OpenTag)
        {
          for (int index = 0; index < next.ParamsCount; ++index)
          {
            string attribute = next.Attributes[index];
            if (attribute == "href" || attribute == "src")
            {
              string str = next.Values[index];
              if (!string.IsNullOrEmpty(str) && (str.StartsWith("~/") || str.StartsWith("[")))
                return true;
            }
          }
        }
      }
      return false;
    }

    /// <summary>
    /// Returns dynamic links if a given HTML text contains dynamic links.
    /// </summary>
    /// <param name="html">Source HTML text.</param>
    /// <param name="unresolveHtml">Indicates whether the html param is in raw format and have to be unresolved</param>
    /// <returns>Gets a list of all dynamic links contained in the specified HTML text.</returns>
    internal static List<string> GetDynamicLinks(string html, bool unresolveHtml)
    {
      List<string> source = new List<string>();
      try
      {
        if (unresolveHtml)
        {
          IList<LinkMetadata> contentLinks;
          LinkParser.UnresolveContentLinks(html, out contentLinks);
          foreach (LinkMetadata linkMetadata in (IEnumerable<LinkMetadata>) contentLinks)
          {
            string unresolvedMediaItemLink = LinkParser.GetUnresolvedMediaItemLink(linkMetadata.Provider, new Guid(linkMetadata.Key), linkMetadata.ClrItemType);
            source.Add(unresolvedMediaItemLink);
          }
        }
        else
        {
          IList<string> unresolvedLinks;
          LinkParser.ParseHtml(html, (GetItemUrl) ((providerName, id, resolveAsAbsolute, status) => string.Empty), (ResolveUrl) ((url, resolveAsAbsolute) => string.Empty), true, true, false, (ProcessChunk) null, out unresolvedLinks);
          source.AddRange((IEnumerable<string>) unresolvedLinks);
        }
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
      }
      return source.Select<string, string>((Func<string, string>) (x => x.Replace("%7C", "|"))).ToList<string>();
    }

    /// <summary>
    /// Gets a delegate for processing chunks that modifies attributes.
    /// </summary>
    /// <param name="attr">The attribute to be modified.</param>
    /// <param name="value">The value to be set to the attribute.</param>
    /// <returns>Returns a ProcessChunk delegate.</returns>
    public static ProcessChunk GetModifyAttributeProcessChunk(
      string tagName,
      string attr,
      string value)
    {
      return (ProcessChunk) (chunk => LinkParser.SetChunkTargetAttribute(chunk, tagName, attr, value));
    }

    /// <summary>Extracts values from image Uri.</summary>
    /// <param name="link">Image uri.</param>
    /// <param name="result">The result.</param>
    /// <returns>Return a value indicating success.</returns>
    public static bool TryExtractValues(Uri link, out IDictionary<string, object> result)
    {
      string path = link.GetComponents(UriComponents.Path, UriFormat.Unescaped);
      if (LinkParser.ExternalBlobPathCache.Value.Any<string>((Func<string, bool>) (blobPath => path.StartsWith(blobPath, StringComparison.OrdinalIgnoreCase))))
      {
        LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
        string[] strArray = new string[4]
        {
          librariesConfig.Images.UrlRoot,
          librariesConfig.Videos.UrlRoot,
          librariesConfig.Documents.UrlRoot,
          librariesConfig.DefaultUrlRoot
        };
        foreach (string str in strArray)
        {
          int num = path.IndexOf("/" + str + "/");
          if (num > -1)
          {
            path = path.Substring(num + 1);
            break;
          }
        }
      }
      else
      {
        string str = HostingEnvironment.ApplicationVirtualPath.Substring(1);
        if (!string.IsNullOrEmpty(str) && path.StartsWith(str))
          path = path.Substring(str.Length + 1);
      }
      QueryStringBuilder queryStringValues = new QueryStringBuilder(link.GetComponents(UriComponents.Query, UriFormat.Unescaped));
      return LibraryRoute.TryExtractValues(path, (NameValueCollection) queryStringValues, out result);
    }

    /// <summary>Gets image Id.</summary>
    /// <param name="values">Values.</param>
    /// <param name="imageId">Image id.</param>
    /// <param name="imageProvider">Image provider.</param>
    /// <returns>Return a value indicating success.</returns>
    public static bool TryGetImageId(
      IDictionary<string, object> values,
      out Guid imageId,
      out string imageProvider)
    {
      bool published = (ContentLifecycleStatus) values["MediaStatus"] == ContentLifecycleStatus.Live;
      string url = (string) values["ContentUrl"];
      imageProvider = (string) values["ProviderName"];
      MediaFileLink fileFromUrl = LibrariesManager.GetManager(imageProvider).GetFileFromUrl(url, published, out string _, out int _);
      if (fileFromUrl == null)
      {
        imageId = Guid.Empty;
        return false;
      }
      imageId = fileFromUrl.MediaContentId;
      Log.Write((object) "Made a query for resolving an image - {0}.".Arrange((object) url), ConfigurationPolicy.TestTracing);
      return true;
    }

    /// <summary>
    /// Tries to get the page URL for a specific culture from a valid URL of the same page.
    /// </summary>
    /// <param name="initialPageUrl">The initial page URL.</param>
    /// <param name="pageUrlForCurrentCulture">The page URL for a specific culture.</param>
    /// <param name="culture">The page culture.</param>
    /// <returns>The page URL for a specific culture.</returns>
    public static bool TryGetPageUrlForCulture(
      string initialPageUrl,
      out string pageUrlForCurrentCulture,
      CultureInfo culture = null)
    {
      return LinkParser.TryGetPageUrlForCulture(initialPageUrl, false, out pageUrlForCurrentCulture, culture);
    }

    /// <summary>
    /// Tries to get the page URL for a specific culture from a valid URL of the same page.
    /// </summary>
    /// <param name="initialPageUrl">The initial page URL.</param>
    /// <param name="preserveParameters">Preserves the details parameters in the url.</param>
    /// <param name="pageUrlForCurrentCulture">The page URL for a specific culture.</param>
    /// <param name="culture">The page culture.</param>
    /// <returns>The page URL for a specific culture.</returns>
    public static bool TryGetPageUrlForCulture(
      string initialPageUrl,
      bool preserveParameters,
      out string pageUrlForCurrentCulture,
      CultureInfo culture = null)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      Uri uri = new Uri(initialPageUrl);
      SiteMapBase currentProvider = SiteMapBase.GetCurrentProvider() as SiteMapBase;
      if (!(LinkParser.FindSiteMapNode(uri.PathAndQuery, currentProvider) is PageSiteNode siteMapNode))
      {
        pageUrlForCurrentCulture = (string) null;
        return false;
      }
      using (new CultureRegion(culture))
      {
        string str = siteMapNode.GetPageViewUrl();
        if (preserveParameters)
          str = uri.AbsoluteUri;
        pageUrlForCurrentCulture = str;
      }
      return true;
    }

    internal static string UnresolveContentLinks(string html) => LinkParser.UnresolveContentLinks(html, out IList<LinkMetadata> _);

    internal static string UnresolveContentLinks(
      string html,
      out IList<LinkMetadata> contentLinks,
      bool prioritizeSfRefAttribute = false)
    {
      contentLinks = (IList<LinkMetadata>) new List<LinkMetadata>();
      if (string.IsNullOrEmpty(html))
        return html;
      StringBuilder stringBuilder = new StringBuilder(html.Length);
      HtmlParser htmlParser = new HtmlParser(html);
      htmlParser.SetChunkHashMode(true);
      htmlParser.AutoExtractBetweenTagsOnly = false;
      htmlParser.KeepRawHTML = true;
      htmlParser.CompressWhiteSpaceBeforeTag = false;
      while (true)
      {
        HtmlChunk next = htmlParser.ParseNext();
        if (next != null)
        {
          if (next.Type != HtmlChunkType.OpenTag)
          {
            stringBuilder.Append(next.Html);
          }
          else
          {
            try
            {
              LinkMetadata linkMetadata = (LinkMetadata) null;
              string str1 = (string) null;
              string key = (string) null;
              if (next.TagName == "img" && next.HasAttribute("src"))
              {
                key = "src";
                string str2 = next.AttributesMap[(object) key].ToString();
                if (!LinkParser.IsImageDataUrl(str2))
                {
                  if (!str2.StartsWith("http"))
                    str2 = UrlPath.ResolveAbsoluteUrl(str2, true);
                  Uri result;
                  if (Uri.TryCreate(str2, UriKind.Absolute, out result))
                    str1 = LinkParser.UnresolveMediaItem(result, typeof (Image), out linkMetadata);
                }
              }
              else if (next.TagName == "video" && next.HasAttribute("src"))
              {
                key = "src";
                string str3 = next.AttributesMap[(object) key].ToString();
                if (!str3.StartsWith("http"))
                  str3 = UrlPath.ResolveAbsoluteUrl(str3, true);
                Uri result;
                if (Uri.TryCreate(str3, UriKind.Absolute, out result))
                  str1 = LinkParser.UnresolveMediaItem(result, typeof (Video), out linkMetadata);
              }
              else if (next.TagName == "a" && next.HasAttribute("href"))
              {
                key = "href";
                string str4 = next.AttributesMap[(object) "href"].ToString();
                string url = LinkParser.DecodeHtmlUrl(str4);
                if (LinkParser.IsEmailLink(url))
                {
                  next.AttributesMap[(object) "href"] = (object) url;
                  stringBuilder.Append(next.GenerateHtml().Replace("'", "\""));
                  continue;
                }
                if (!str4.StartsWith("http"))
                  str4 = UrlPath.ResolveAbsoluteUrl(str4, true);
                if (prioritizeSfRefAttribute && next.HasAttribute("sfref"))
                  LinkParser.TryUnresolveWithSitefinityDynamicLink(next, str4, out str1, false);
                if (string.IsNullOrEmpty(str1) && !LinkParser.TryUnresolveMediaItem(str4, out linkMetadata, out str1) && !LinkParser.TryUnresolveWithSitefinityDynamicLink(next, str4, out str1))
                  LinkParser.TryUnresolvePage(str4, out str1);
              }
              if (linkMetadata != null)
                contentLinks.Add(linkMetadata);
              string str5;
              if (!string.IsNullOrWhiteSpace(str1))
              {
                next.AttributesMap[(object) key] = (object) str1;
                if (next.HasAttribute("sfref"))
                  next.RemoveAttribute("sfref");
                str5 = next.GenerateHtml().Replace("'", "\"");
              }
              else
                str5 = next.Html;
              stringBuilder.Append(str5);
            }
            catch (Exception ex)
            {
              Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
            }
          }
        }
        else
          break;
      }
      return stringBuilder.ToString();
    }

    private static bool IsEmailLink(string url)
    {
      if ((string.IsNullOrEmpty(url) ? 0 : (url.ToLower().Trim().StartsWith("mailto:") ? 1 : 0)) == 0)
        return false;
      string input = url.Replace("mailto:", string.Empty);
      return !string.IsNullOrEmpty(input) && LinkParser.EmailRegExpr.IsMatch(input);
    }

    private static string DecodeHtmlUrl(string url) => HttpUtility.HtmlDecode(url);

    private static bool TryUnresolveWithSitefinityDynamicLink(
      HtmlChunk chunk,
      string url,
      out string unresolvedVal,
      bool resolveLink = true)
    {
      unresolvedVal = (string) null;
      if (chunk.HasAttribute("sfref"))
      {
        unresolvedVal = chunk.AttributesMap[(object) "sfref"].ToString();
        string empty1 = string.Empty;
        string str1 = string.Empty;
        url = LinkParser.HtmlDecode(url);
        int startIndex1 = url.LastIndexOf('#');
        if (startIndex1 != -1)
        {
          str1 = url.Substring(startIndex1);
          url = url.Remove(startIndex1);
        }
        int startIndex2 = url.IndexOf('?');
        if (startIndex2 != -1)
        {
          QueryStringBuilder queryStringBuilder = new QueryStringBuilder(url.Substring(startIndex2), true);
          queryStringBuilder.Remove(MediaContentExtensions.UrlVersionQueryParam);
          empty1 = queryStringBuilder.ToString();
          url = url.Remove(startIndex2);
        }
        if (unresolvedVal != null)
          chunk.AttributesMap[(object) "href"] = (object) unresolvedVal;
        string empty2 = string.Empty;
        string str2 = !resolveLink ? LinkParser.ExtractParams(chunk.GenerateHtml().Replace("'", "\""), url) : LinkParser.GetLinkParams(chunk.GenerateHtml().Replace("'", "\""), url);
        unresolvedVal = unresolvedVal + str2 + empty1 + str1;
        chunk.RemoveAttribute("sfref");
      }
      return unresolvedVal != null;
    }

    private static bool TryUnresolveMediaItem(
      string url,
      out LinkMetadata linkMetadata,
      out string unresolvedUrl)
    {
      linkMetadata = (LinkMetadata) null;
      unresolvedUrl = (string) null;
      try
      {
        Type[] typeArray = new Type[3]
        {
          typeof (Image),
          typeof (Document),
          typeof (Video)
        };
        foreach (Type itemType in typeArray)
        {
          Uri result;
          if (Uri.TryCreate(url, UriKind.Absolute, out result))
          {
            unresolvedUrl = LinkParser.UnresolveMediaItem(result, itemType, out linkMetadata);
            if (!string.IsNullOrEmpty(unresolvedUrl))
              return true;
          }
        }
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
      }
      return false;
    }

    private static string GetLinkParams(string linkHtml, string hrefWithParams) => LinkParser.ExtractParams(LinkParser.ResolveLinks(linkHtml.ToString(), new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, false, true), hrefWithParams);

    private static string ExtractParams(string link, string hrefWithParams)
    {
      StringBuilder stringBuilder = new StringBuilder(link.Length);
      HtmlParser htmlParser = new HtmlParser(link);
      htmlParser.SetChunkHashMode(true);
      htmlParser.AutoExtractBetweenTagsOnly = false;
      htmlParser.KeepRawHTML = true;
      htmlParser.CompressWhiteSpaceBeforeTag = false;
      string str1 = string.Empty;
      while (true)
      {
        string str2;
        do
        {
          HtmlChunk next;
          do
          {
            next = htmlParser.ParseNext();
            if (next != null)
            {
              if (next.Type != HtmlChunkType.OpenTag)
                stringBuilder.Append(next.Html);
            }
            else
              goto label_7;
          }
          while (!(next.TagName == "a") || !next.HasAttribute("href"));
          string key = "href";
          string str3 = next.AttributesMap[(object) key].ToString();
          str2 = str3.EndsWith("\\") ? str3.Remove(str3.Length - 1) : str3;
        }
        while (!hrefWithParams.Contains(str2));
        str1 = hrefWithParams.Remove(0, str2.Length);
      }
label_7:
      return str1;
    }

    private static string UnresolveMediaItem(Uri link, Type itemType, out LinkMetadata linkMeta)
    {
      linkMeta = (LinkMetadata) null;
      Guid mediaItemId = Guid.Empty;
      string imageProvider = (string) null;
      if (link.IsAbsoluteUri && LibraryHttpHandler.Cache[LibraryHttpHandler.GetCacheKey(link, SystemManager.CurrentHttpContext)] is LibraryHttpHandler.IOutputItem outputItem)
      {
        mediaItemId = outputItem.Id;
        imageProvider = outputItem.Provider;
      }
      if (!HostingEnvironment.IsHosted)
        return (string) null;
      IDictionary<string, object> result;
      if (LinkParser.TryExtractValues(link, out result))
      {
        string thumbnail = result["ThumbnailName"] as string;
        if (mediaItemId == Guid.Empty && string.IsNullOrEmpty(imageProvider) && !LinkParser.TryGetImageId(result, out mediaItemId, out imageProvider))
          return (string) null;
        MediaContent mediaContent = (LibrariesManager.GetManager(imageProvider).GetItems(itemType, string.Empty, string.Empty, 0, 0) as IQueryable<MediaContent>).FirstOrDefault<MediaContent>((Expression<Func<MediaContent, bool>>) (x => (x.Id == mediaItemId || x.OriginalContentId == mediaItemId) && ((int) x.Status == 0 || (int) x.Status == 2)));
        if (mediaContent != null)
        {
          Guid itemId = mediaContent.Status == ContentLifecycleStatus.Master ? mediaContent.Id : mediaContent.OriginalContentId;
          linkMeta = new LinkMetadata()
          {
            Provider = imageProvider,
            Key = itemId.ToString(),
            ItemType = itemType.FullName,
            ClrItemType = itemType
          };
          return LinkParser.GetUnresolvedMediaItemLink(imageProvider, itemId, itemType, thumbnail, link.Fragment, link.Query);
        }
      }
      return (string) null;
    }

    private static bool TryUnresolvePage(string url, out string result)
    {
      if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
      {
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        try
        {
          LinkParser.LinkResult linkResult = new LinkParser.LinkResult(new Uri(url));
          SystemManager.RunWithElevatedPrivilege(new SystemManager.RunWithElevatedPrivilegeDelegate(LinkParser.UnresolvePageInternal), new object[1]
          {
            (object) linkResult
          });
          result = linkResult.Result;
          return result != null;
        }
        catch
        {
        }
        finally
        {
          if (!culture.Equals((object) SystemManager.CurrentContext.Culture))
            SystemManager.CurrentContext.Culture = culture;
        }
      }
      result = (string) null;
      return false;
    }

    private static void UnresolvePageInternal(object[] parameters)
    {
      LinkParser.LinkResult parameter = (LinkParser.LinkResult) parameters[0];
      Uri uri = parameter.Uri;
      string authority1 = uri.Authority;
      string str1 = (string) null;
      SitefinityContextBase currentContext = SystemManager.CurrentContext;
      ISite site = (ISite) null;
      if (currentContext != null)
      {
        IMultisiteContext multisiteContext = currentContext.MultisiteContext;
        site = multisiteContext == null ? currentContext.CurrentSite : multisiteContext.GetSiteByDomain(authority1);
      }
      string authority2 = site.GetUri().Authority;
      if (site.IsDefault && authority2.Equals("localhost", StringComparison.OrdinalIgnoreCase))
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null)
          authority2 = currentHttpContext.Request.Url.Authority;
      }
      if (authority1.Equals(authority2, StringComparison.OrdinalIgnoreCase))
      {
        using (SiteRegion.FromSiteId(site.Id))
        {
          string str2 = HostingEnvironment.ApplicationVirtualPath.Substring(1);
          string path = uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
          string str3 = uri.GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped) + uri.Fragment;
          if (!string.IsNullOrEmpty(str2) && path.StartsWith(str2))
          {
            path = path.Substring(str2.Length + 1);
            str3 = str3.Substring(str2.Length + 1);
          }
          SiteMapBase currentProvider = SiteMapBase.GetCurrentProvider() as SiteMapBase;
          string[] urlParameters;
          SiteMapNode siteMapNode = LinkParser.FindSiteMapNode(path, currentProvider, out urlParameters);
          if (siteMapNode == null || urlParameters != null && urlParameters.Length != 0)
            return;
          ISite currentSite = SystemManager.CurrentContext.CurrentSite;
          CultureInfo culture = (CultureInfo) null;
          if (SystemManager.CurrentContext.AppSettings.Multilingual)
            culture = SystemManager.CurrentContext.Culture;
          str1 = LinkParser.GetUnresolvedPageLink(currentSite.SiteMapRootNodeId, siteMapNode.Key, culture);
          string str4 = siteMapNode.Url.Replace("~", string.Empty);
          if (str3.StartsWith(str4))
            str1 += str3.Substring(str4.Length);
        }
      }
      parameter.Result = str1;
    }

    internal static string GetUnresolvedMediaItemLink(
      string itemProvider,
      Guid itemId,
      Type itemType,
      string thumbnail = null,
      string fragment = null,
      string query = null)
    {
      string str = (string) null;
      if (itemType == typeof (Image))
        str = "images";
      else if (itemType == typeof (Video))
        str = "videos";
      else if (itemType == typeof (Document))
        str = "documents";
      return thumbnail == null ? "[{0}{3}{1}]{2}{4}{5}".Arrange((object) str, (object) itemProvider, (object) itemId, (object) "%7C", (object) fragment, (object) query) : "[{0}{4}{1}{4}tmb{5}{3}]{2}{6}{7}".Arrange((object) str, (object) itemProvider, (object) itemId, (object) thumbnail, (object) "%7C", (object) "%3A", (object) fragment, (object) query);
    }

    internal static string GetUnresolvedPageLink(
      Guid siteMapRootNodeId,
      string siteMapNodeKey,
      CultureInfo culture = null)
    {
      if (culture == null)
        return string.Format("[{0}]{1}", (object) siteMapRootNodeId, (object) siteMapNodeKey);
      return string.Format("[{0}{3}lng{4}{1}]{2}", (object) siteMapRootNodeId, (object) culture.Name, (object) siteMapNodeKey, (object) "%7C", (object) "%3A");
    }

    internal static SiteMapNode FindSiteMapNode(string path, SiteMapBase siteMapProvider) => LinkParser.FindSiteMapNode(path, siteMapProvider, out string[] _);

    internal static SiteMapNode FindSiteMapNode(
      string path,
      SiteMapBase siteMapProvider,
      out string[] urlParameters)
    {
      return ObjectFactory.Resolve<SitefinityRoute>().FindSiteMapNode(ref path, siteMapProvider, out bool _, out urlParameters, out bool _);
    }

    public static LinkParser.LinkNotResolvedBehaviour LinkNotResolvedLogic(
      bool preservOriginalValues)
    {
      return LinkParser.LinkNotResolvedBehaviour.LeadTo404;
    }

    private static string ParseHtml(
      string html,
      GetItemUrl itemUrl,
      ResolveUrl resolveUrl,
      bool resolve,
      bool preserveOriginalValue,
      bool resolveAbsolute,
      ProcessChunk processChunk)
    {
      return LinkParser.ParseHtml(html, itemUrl, resolveUrl, resolve, preserveOriginalValue, resolveAbsolute, processChunk, out IList<string> _);
    }

    private static string ParseHtml(
      string html,
      GetItemUrl itemUrl,
      ResolveUrl resolveUrl,
      bool resolve,
      bool preserveOriginalValue,
      bool resolveAbsolute,
      ProcessChunk processChunk,
      out IList<string> unresolvedLinks)
    {
      unresolvedLinks = (IList<string>) new List<string>();
      if (string.IsNullOrEmpty(html))
        return html;
      RenderBehaviour render;
      switch (LinkParser.LinkNotResolvedLogic(preserveOriginalValue))
      {
        case LinkParser.LinkNotResolvedBehaviour.LeadTo404:
          render = new RenderBehaviour(LinkParser.LeadTo404RenderBehaviour);
          break;
        case LinkParser.LinkNotResolvedBehaviour.SkipOpenCloseTag:
          render = new RenderBehaviour(LinkParser.SkipTagIfNotResolvedRenderBehaviour);
          break;
        case LinkParser.LinkNotResolvedBehaviour.LinkToAnchor:
          render = new RenderBehaviour(LinkParser.LinkToAnchorRenderBehaviour);
          break;
        default:
          render = new RenderBehaviour(LinkParser.SkipTagIfNotResolvedRenderBehaviour);
          break;
      }
      StringBuilder htmlContent = new StringBuilder(html.Length);
      HtmlParser htmlParser = new HtmlParser(html);
      htmlParser.SetChunkHashMode(false);
      htmlParser.AutoExtractBetweenTagsOnly = false;
      htmlParser.KeepRawHTML = true;
      htmlParser.CompressWhiteSpaceBeforeTag = false;
      string str = string.Empty;
      int num = 0;
      HtmlChunk next = htmlParser.ParseNext();
      if (next.Html.StartsWith("<!DOCTYPE"))
      {
        LinkParser.AppendResult(htmlContent, next, LinkParser.ResolveResult.Default, render, preserveOriginalValue);
        next = htmlParser.ParseNext();
      }
      for (; next != null; next = htmlParser.ParseNext())
      {
        LinkParser.ResolveResult resolveResult = LinkParser.ResolveResult.Default;
        if (next.Type == HtmlChunkType.OpenTag)
        {
          for (int valueIndex = 0; valueIndex < next.ParamsCount; ++valueIndex)
          {
            string attribute = next.Attributes[valueIndex];
            if (attribute == "href" || attribute == "src" || ((!(next.TagName == "object") ? 0 : (attribute == "sfref" ? 1 : 0)) & (resolve ? 1 : 0)) != 0 && !preserveOriginalValue || attribute == "value" && next.TagName == "param")
            {
              if (resolve)
              {
                string unresolvedLink;
                resolveResult = LinkParser.Resolve(next, valueIndex, itemUrl, resolveUrl, preserveOriginalValue, resolveAbsolute, out unresolvedLink);
                if (!string.IsNullOrEmpty(unresolvedLink))
                  unresolvedLinks.Add(unresolvedLink);
              }
              else
                resolveResult = LinkParser.Unresolve(next, valueIndex);
              if (((!(next.TagName == "object") ? 0 : (attribute == "sfref" ? 1 : 0)) & (resolve ? 1 : 0)) != 0 && !preserveOriginalValue)
              {
                next.RemoveAttribute("sfref");
                break;
              }
              break;
            }
          }
        }
        if (resolveResult.SkipWholeTag && num == 0)
          str = next.TagName;
        if (next.Type == HtmlChunkType.OpenTag && str == next.TagName && next.TagName != string.Empty)
          ++num;
        if (num == 0)
        {
          str = string.Empty;
          if (processChunk != null)
            resolveResult.Modified = processChunk(next) || resolveResult.Modified;
          LinkParser.AppendResult(htmlContent, next, resolveResult, render, preserveOriginalValue);
        }
        if ((next.Type == HtmlChunkType.CloseTag || next.IsEndClosure) && str == next.TagName)
          --num;
      }
      return htmlContent.ToString();
    }

    /// <summary>
    /// Append resulting HTML of the current operation to the html builder
    /// </summary>
    /// <param name="htmlContent">HTML builder</param>
    /// <param name="currentChunk">HTML chunk to evaluate and append to the builder</param>
    /// <param name="resolveResult">Result of the current url resolving/unresolving operation</param>
    /// <param name="preserveOriginalValue">
    /// If true the original value of altered attributes is
    /// preserved in additional attribute named sfref.
    /// </param>
    private static void AppendResult(
      StringBuilder htmlContent,
      HtmlChunk currentChunk,
      LinkParser.ResolveResult resolveResult,
      RenderBehaviour render,
      bool preserveOriginalValue)
    {
      if (resolveResult.Modified)
        render(htmlContent, currentChunk, resolveResult, preserveOriginalValue);
      else
        htmlContent.Append(currentChunk.Html);
    }

    private static void LinkToAnchorRenderBehaviour(
      StringBuilder htmlContent,
      HtmlChunk currentChunk,
      LinkParser.ResolveResult resolveResult,
      bool preserveOriginalValue)
    {
      for (int index = 0; index < currentChunk.ParamsCount; ++index)
      {
        if (currentChunk.Attributes[index] == "href" && currentChunk.Values[index] == "#Link.Not.Resolved#")
          currentChunk.Values[index] = "#sf404";
      }
      htmlContent.Append(currentChunk.GenerateHtml());
    }

    private static void LeadTo404RenderBehaviour(
      StringBuilder htmlContent,
      HtmlChunk currentChunk,
      LinkParser.ResolveResult resolveResult,
      bool preserveOriginalValue)
    {
      htmlContent.Append(currentChunk.GenerateHtml());
    }

    private static void SkipTagIfNotResolvedRenderBehaviour(
      StringBuilder htmlContent,
      HtmlChunk currentChunk,
      LinkParser.ResolveResult resolveResult,
      bool preserveOriginalValue)
    {
      int num;
      if (!LinkParser.tagsTopSkip.TryGetValue(currentChunk.TagName, out num))
        num = -1;
      if (resolveResult.RenderTagContentsOnly && currentChunk.Type == HtmlChunkType.OpenTag)
      {
        if (!currentChunk.IsEndClosure && !LinkParser.tagsTopSkip.ContainsKey(currentChunk.TagName))
          LinkParser.tagsTopSkip.Add(currentChunk.TagName, 0);
        else
          ++LinkParser.tagsTopSkip[currentChunk.TagName];
      }
      else if (num != 0 || currentChunk.Type != HtmlChunkType.CloseTag)
      {
        if (num > 0 && currentChunk.Type != HtmlChunkType.CloseTag)
          LinkParser.tagsTopSkip[currentChunk.TagName] = num - 1;
        htmlContent.Append(currentChunk.GenerateHtml());
      }
      else
        LinkParser.tagsTopSkip.Remove(currentChunk.TagName);
    }

    private static LinkParser.ResolveResult Unresolve(HtmlChunk chunk, int valueIndex)
    {
      string paramValue = chunk.GetParamValue("sfref");
      if (string.IsNullOrEmpty(paramValue))
        return LinkParser.ResolveResult.Default;
      string str1 = LinkParser.HtmlDecode(HttpUtility.UrlDecode(chunk.Values[valueIndex]));
      string empty = string.Empty;
      string str2 = string.Empty;
      int startIndex1 = str1.IndexOf('?');
      if (startIndex1 != -1)
      {
        QueryStringBuilder queryStringBuilder = new QueryStringBuilder(str1.Substring(startIndex1), true);
        queryStringBuilder.Remove(MediaContentExtensions.UrlVersionQueryParam);
        empty = queryStringBuilder.ToString();
      }
      else
      {
        int startIndex2 = str1.LastIndexOf('#');
        if (startIndex2 != -1)
          str2 = str1.Substring(startIndex2);
      }
      chunk.Values[valueIndex] = paramValue + empty + str2;
      chunk.RemoveAttribute("sfref");
      return new LinkParser.ResolveResult(true, false);
    }

    private static LinkParser.ResolveResult Resolve(
      HtmlChunk chunk,
      int valueIndex,
      GetItemUrl itemUrl,
      ResolveUrl resolveUrl,
      bool preserveOriginalValue,
      bool resolveAbsolute,
      out string unresolvedLink)
    {
      unresolvedLink = (string) null;
      string str1 = chunk.Values[valueIndex];
      if (!string.IsNullOrEmpty(str1))
      {
        if (str1.StartsWith("#"))
          return new LinkParser.ResolveResult(false, false);
        if (str1.StartsWith("~/"))
        {
          bool skipWholeTag = false;
          if (resolveUrl != null)
          {
            string str2 = resolveUrl(str1, resolveAbsolute);
            if (str2 == "#Link.Not.Resolved#")
              skipWholeTag = true;
            chunk.Values[valueIndex] = str2;
          }
          else
            chunk.Values[valueIndex] = !resolveAbsolute ? UrlPath.ResolveUrl(str1) : UrlPath.ResolveAbsoluteUrl(str1);
          return new LinkParser.ResolveResult(true, false, skipWholeTag);
        }
        if (str1.StartsWith("["))
        {
          bool skipWholeTag = false;
          string str3 = string.Empty;
          int num1 = str1.IndexOf('/');
          if (num1 != -1)
          {
            str3 = str1.Substring(num1);
            str1 = str1.Substring(0, num1);
          }
          string str4 = string.Empty;
          int num2 = str1.IndexOf('?');
          if (num2 != -1)
          {
            str4 = str1.Substring(num2);
            str1 = str1.Substring(0, num2);
          }
          string str5 = string.Empty;
          int num3 = str1.LastIndexOf('#');
          if (num3 != -1)
          {
            str5 = str1.Substring(num3);
            str1 = str1.Substring(0, num3);
          }
          int num4 = str1.IndexOf("]");
          string providerName = str1.Substring(1, num4 - 1);
          string str6 = str1.Substring(num4 + 1);
          ContentLifecycleStatus clcStatus = LinkParser.ExtractCLCStatus(str4);
          unresolvedLink = str1;
          if (itemUrl == null)
            throw new NotImplementedException("No default implementation for retrieving item URL yet. Please use delegate to handle retrieval.");
          if (LinkParser.IsGuid(str6))
          {
            string str7 = itemUrl(providerName, new Guid(str6), resolveAbsolute, clcStatus);
            if (str7 == "#Link.Not.Resolved#")
            {
              chunk.Values[valueIndex] = !preserveOriginalValue ? str6 : string.Format("Item with ID: '{0}' was not found!", (object) str6);
            }
            else
            {
              string str8 = str7 + str3;
              if (str4.Length > 0)
              {
                int num5 = str8.IndexOf('?');
                if (num5 != -1)
                {
                  string queryString = str8.Substring(num5);
                  string str9 = str8.Substring(0, num5);
                  QueryStringBuilder queryStringBuilder1 = new QueryStringBuilder(queryString, true);
                  QueryStringBuilder queryStringBuilder2 = new QueryStringBuilder(str4, true);
                  foreach (string name in (NameObjectCollectionBase) queryStringBuilder2)
                  {
                    if (!queryStringBuilder1.Contains(name))
                      queryStringBuilder1.Add(name, queryStringBuilder2[name]);
                  }
                  str8 = str9 + queryStringBuilder1.ToString();
                }
                else
                  str8 += str4;
              }
              chunk.Values[valueIndex] = str8 + str5;
            }
          }
          else
            chunk.Values[valueIndex] = !preserveOriginalValue ? str6 : "Item ID is not a valid Guid: " + str6;
          if (preserveOriginalValue)
            chunk.AddAttribute("sfref", str1);
          return new LinkParser.ResolveResult(true, false, str6, skipWholeTag);
        }
        if (resolveAbsolute)
        {
          string str10 = !(chunk.TagName == "a") || SystemManager.CurrentContext.MultisiteContext.CurrentSiteContext.ResolutionType == SiteContextResolutionTypes.ByDomain || SystemManager.CurrentContext.MultisiteContext.CurrentSiteContext.ResolutionType == SiteContextResolutionTypes.ByFolder ? UrlPath.ResolveAbsoluteUrl(str1) : UrlPath.ResolveAbsoluteUrl(SystemManager.CurrentContext.CurrentSite.GetUri(), str1);
          chunk.Values[valueIndex] = str10;
          if (!preserveOriginalValue)
            chunk.RemoveAttribute("sfref");
          return new LinkParser.ResolveResult(true, false);
        }
      }
      return new LinkParser.ResolveResult(false, false);
    }

    private static ContentLifecycleStatus ExtractCLCStatus(string query)
    {
      ContentLifecycleStatus clcStatus = ContentLifecycleStatus.Live;
      if (!string.IsNullOrWhiteSpace(query))
      {
        string str = "&" + query.Substring(1);
        int num1 = str.IndexOf("&status=", StringComparison.OrdinalIgnoreCase);
        if (num1 != -1)
        {
          int num2 = str.IndexOf('&', num1 + 1);
          ContentLifecycleStatus result;
          if (Enum.TryParse<ContentLifecycleStatus>(num2 == -1 ? str.Substring(num1 + "&status=".Length) : str.Substring(num1 + "&status=".Length, num2 - num1 - "&status=".Length), true, out result))
            clcStatus = result;
        }
      }
      return clcStatus;
    }

    private static bool SetChunkTargetAttribute(
      HtmlChunk chunk,
      string tagName,
      string attr,
      string value)
    {
      if (!(chunk.TagName == tagName) || chunk.Type != HtmlChunkType.OpenTag || chunk.HasAttribute(attr) && !(chunk.GetParamValue(attr) != value))
        return false;
      chunk.SetAttribute(attr, value);
      return true;
    }

    /// <summary>
    /// Helper method to check if the passed string is of type GUID.
    /// </summary>
    /// <param name="input">the string value that should be verified</param>
    /// <returns>A boolean value that, if true, states that the string is a valid GUID.</returns>
    private static bool IsGuid(string input) => Utility.IsGuid(input);

    /// <summary>
    /// Decode only the apostrophe symbol for now in case more symbols cause issues we will decode them too
    /// </summary>
    /// <param name="html"></param>
    /// <returns>Decoded html</returns>
    private static string HtmlDecode(string html) => html.Replace("&#39;", "'");

    private static bool IsImageDataUrl(string url) => new Regex("^data:image\\/.*;base64,.*").IsMatch(url);

    private static HashSet<string> BuildExternalBlobPathCache()
    {
      HashSet<string> stringSet = new HashSet<string>();
      LibrariesManager.GetManager();
      string str = "/dummypath";
      foreach (BlobStorageProvider staticProvider in (Collection<BlobStorageProvider>) BlobStorageManager.GetManager().StaticProviders)
      {
        if (staticProvider is IExternalBlobStorageProvider blobStorageProvider)
        {
          BlobContentLocation content = new BlobContentLocation((IBlobContent) new BlobContentProxy()
          {
            FilePath = str
          });
          string itemUrl = blobStorageProvider.GetItemUrl((IBlobContentLocation) content);
          if (!string.IsNullOrEmpty(itemUrl) && itemUrl.EndsWith(str, StringComparison.OrdinalIgnoreCase))
          {
            Uri result;
            Uri.TryCreate(itemUrl.Substring(0, itemUrl.Length - str.Length), UriKind.Absolute, out result);
            string components = result.GetComponents(UriComponents.Path, UriFormat.Unescaped);
            if (!string.IsNullOrEmpty(components))
              stringSet.Add(components);
          }
        }
      }
      return stringSet;
    }

    private class LinkResult
    {
      public LinkResult(Uri uri) => this.Uri = uri;

      public Uri Uri { get; private set; }

      public string Result { get; set; }
    }

    /// <summary>
    /// Shows the outcome of a resolving/unresolving operation
    /// </summary>
    public struct ResolveResult
    {
      /// <summary>Content was modified</summary>
      public bool Modified;
      /// <summary>Skip opening and closing tag</summary>
      public bool RenderTagContentsOnly;
      /// <summary>ID of the item</summary>
      public string ItemID;
      /// <summary>ID of the item</summary>
      public bool SkipWholeTag;
      /// <summary>Not modified, don't skip tags</summary>
      /// <remarks>Should be the same as default(ResolveResult)</remarks>
      public static readonly LinkParser.ResolveResult Default = new LinkParser.ResolveResult(false, false);

      /// <summary>
      /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Web.Utilities.LinkParser.ResolveResult" />
      /// </summary>
      /// <param name="modified">Content was modified</param>
      /// <param name="renderContentsOnly">Skipe opening and closing tag</param>
      public ResolveResult(bool modified, bool renderContentsOnly)
        : this(modified, renderContentsOnly, (string) null)
      {
      }

      /// <summary>
      /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Web.Utilities.LinkParser.ResolveResult" />
      /// </summary>
      /// <param name="modified">Content was modified</param>
      /// <param name="renderContentsOnly">Skipe opening and closing tag</param>
      /// <param name="id">Item id whose resolution failed</param>
      public ResolveResult(bool modified, bool renderContentsOnly, string id)
      {
        this.Modified = modified;
        this.RenderTagContentsOnly = renderContentsOnly;
        this.ItemID = id;
        this.SkipWholeTag = false;
      }

      /// <summary>
      /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Web.Utilities.LinkParser.ResolveResult" />
      /// </summary>
      /// <param name="modified">Content was modified</param>
      /// <param name="renderContentsOnly">Skipe opening and closing tag</param>
      /// <param name="skipWholeTag">if set to <c>true</c> will not render the tag and it's content.</param>
      public ResolveResult(bool modified, bool renderContentsOnly, bool skipWholeTag)
      {
        this.Modified = modified;
        this.RenderTagContentsOnly = renderContentsOnly;
        this.SkipWholeTag = skipWholeTag;
        this.ItemID = (string) null;
      }

      /// <summary>
      /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Web.Utilities.LinkParser.ResolveResult" />
      /// </summary>
      /// <param name="modified">Content was modified</param>
      /// <param name="renderContentsOnly">Skipe opening and closing tag</param>
      /// <param name="id">Item id whose resolution failed</param>
      /// <param name="skipWholeTag">if set to <c>true</c> will not render the tag and it's content.</param>
      public ResolveResult(bool modified, bool renderContentsOnly, string id, bool skipWholeTag)
      {
        this.Modified = modified;
        this.RenderTagContentsOnly = renderContentsOnly;
        this.SkipWholeTag = skipWholeTag;
        this.ItemID = id;
      }
    }

    /// <summary>
    /// Determines what should be done if a link is not resolved
    /// </summary>
    public enum LinkNotResolvedBehaviour
    {
      /// <summary>Leave the link broken</summary>
      LeadTo404,
      /// <summary>
      /// Skip opening and closing tag, but leave its contents intact
      /// </summary>
      SkipOpenCloseTag,
      /// <summary>Link to a special achor</summary>
      LinkToAnchor,
    }
  }
}
