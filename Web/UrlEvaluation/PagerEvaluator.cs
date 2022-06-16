// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.PagerEvaluator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Extracts the current page number and the items per page in the url. The format is /page/2/show/3.
  /// The parameters can be used separately (E.g /show/2 or /page/3). Does not support the format /show/2/page/3.
  /// </summary>
  internal class PagerEvaluator : IUrlEvaluator<PaginationParams>, IUrlEvaluator, IUrlStripper
  {
    private string regularExpression;
    internal const string pageKey = "page";
    internal const string showKey = "show";
    private const string allKey = "all";
    private const string urlPrefixKey = "urlPrefix";
    private const int allItems = -1;

    /// <inheritdoc />
    public string BuildUrl(object data, string urlKeyPrefix) => this.BuildUrl(PaginationParams.Parse(data), urlKeyPrefix);

    /// <inheritdoc />
    public string BuildUrl(PaginationParams data, string urlKeyPrefix) => this.BuildUrl(data, UrlEvaluationMode.Default, urlKeyPrefix);

    /// <inheritdoc />
    public string BuildUrl(object data, UrlEvaluationMode urlEvaluationMode, string urlKeyPrefix) => this.BuildUrl(PaginationParams.Parse(data), urlEvaluationMode, urlKeyPrefix);

    /// <inheritdoc />
    public string BuildUrl(
      PaginationParams data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
      {
        QueryStringBuilder queryStringBuilder = QueryStringBuilder.Current.Add(urlKeyPrefix + "page", data.PageNumber.ToString(), true);
        if (data.ItemsPerPage > 0)
          queryStringBuilder.Add("show", data.ItemsPerPage.ToString(), true);
        return queryStringBuilder.ToString();
      }
      string str = string.Empty;
      if (!urlKeyPrefix.IsNullOrEmpty())
        str = str + "/!" + urlKeyPrefix;
      if (data.PageNumber > 1)
        str += string.Format("/{0}/{1}", (object) "page", (object) data.PageNumber);
      if (data.ItemsPerPage == -1)
        str += string.Format("/{0}/{1}", (object) "show", (object) "all");
      else if (data.ItemsPerPage > 1)
        str += string.Format("/{0}/{1}", (object) "show", (object) data.ItemsPerPage);
      if (!urlEvaluationMode.HasFlag((Enum) UrlEvaluationMode.DiscardCurrentQueryString))
        str += QueryStringBuilder.Current.ToString();
      return str;
    }

    /// <inheritdoc />
    public string Evaluate(string url, string propertyName, string key, out object[] values) => this.Evaluate(url, propertyName, (Type) null, key, out values);

    /// <inheritdoc />
    public string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      string key,
      out object[] values)
    {
      return this.Evaluate(url, propertyName, contentType, UrlEvaluationMode.Default, key, out values);
    }

    /// <inheritdoc />
    public string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string key,
      out object[] values)
    {
      return urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString ? this.EvaluateFromQueryString(key, out values) : this.EvaluateFromUrlPath(url, key, out values);
    }

    /// <inheritdoc />
    public void Initialize(NameValueCollection config)
    {
      this.regularExpression = config != null ? config["regularExpression"] : throw new ArgumentNullException(nameof (config));
      if (!string.IsNullOrEmpty(this.regularExpression))
        return;
      this.regularExpression = string.Format("^/?(!(?<{0}>\\w*)/)*((?i:{1})/(?<{1}>\\d*))?/?((?i:{2})/(?<{2}>\\d*|{3}))?(?:/|\\z)$", (object) "urlPrefix", (object) "page", (object) "show", (object) "all");
    }

    /// <inheritdoc />
    public string StripUrl(string url, string urlKeyPrefix)
    {
      string regularExpression = this.regularExpression;
      string queryString;
      url = UrlUtilities.StripQueryString(url, out queryString);
      MatchCollection matchCollection = Regex.Matches(url, regularExpression);
      if (matchCollection.Count > 1)
        throw new ArgumentException("More then one matches. URL string should contain only one match.");
      if (matchCollection.Count == 1)
      {
        Match match = matchCollection[0];
        if (((!string.IsNullOrEmpty(urlKeyPrefix) ? 0 : (!match.Groups["urlPrefix"].Success ? 1 : 0)) | (!match.Groups["urlPrefix"].Success ? (false ? 1 : 0) : (match.Groups["urlPrefix"].Value == urlKeyPrefix ? 1 : 0))) != 0)
          return Regex.Replace(url, regularExpression, string.Empty) + queryString;
      }
      return url;
    }

    /// <summary>
    /// Gets the page number if there is a match in the URL string.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="urlKeyPrefix">The URL key prefix.</param>
    /// <returns></returns>
    public virtual int GetPageNumber(string url, string urlKeyPrefix)
    {
      object[] values;
      this.Evaluate(url, (string) null, urlKeyPrefix, out values);
      return (int) values[0];
    }

    /// <summary>Gets the items per page.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="urlKeyPrefix">The URL key prefix.</param>
    /// <returns></returns>
    public virtual int GetItemsPerPage(string url, string urlKeyPrefix)
    {
      object[] values;
      this.Evaluate(url, (string) null, urlKeyPrefix, out values);
      return (int) values[1];
    }

    private string EvaluateFromUrlPath(string url, string key, out object[] values)
    {
      int result1 = 0;
      int result2 = 0;
      if (!string.IsNullOrWhiteSpace(url))
      {
        MatchCollection matchCollection = Regex.Matches(url, this.regularExpression);
        if (matchCollection.Count > 1)
          throw new ArgumentException("More then one matches. URL string should contain only one match.");
        if (matchCollection.Count == 1)
        {
          Match match = matchCollection[0];
          if (((!string.IsNullOrEmpty(key) ? 0 : (!match.Groups["urlPrefix"].Success ? 1 : 0)) | (!match.Groups["urlPrefix"].Success ? (false ? 1 : 0) : (match.Groups["urlPrefix"].Value == key ? 1 : 0))) != 0)
          {
            int.TryParse(match.Groups["page"].Value, out result1);
            string s = match.Groups["show"].Value;
            if (s == "all")
              result2 = -1;
            else
              int.TryParse(s, out result2);
          }
        }
      }
      values = (object[]) new PaginationParams[1]
      {
        new PaginationParams(result1, result2)
      };
      return string.Empty;
    }

    private string EvaluateFromQueryString(string key, out object[] values)
    {
      string parameterName = key + "page";
      HttpRequestBase request = SystemManager.CurrentHttpContext.Request;
      string s1 = request.QueryStringGet(parameterName);
      string s2 = request.QueryStringGet("show");
      int result;
      int.TryParse(s1, out result);
      int itemsPerPage;
      ref int local = ref itemsPerPage;
      int.TryParse(s2, out local);
      values = (object[]) new PaginationParams[1]
      {
        new PaginationParams(result, itemsPerPage)
      };
      return string.Empty;
    }

    /// <summary>Gets the name of the evaluator</summary>
    public static string Name => typeof (PagerEvaluator).Name;
  }
}
