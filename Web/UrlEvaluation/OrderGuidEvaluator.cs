// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.OrderGuidEvaluator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Evaluates the Order Guid in the URL. The default format is: /order/{GUID}/
  /// </summary>
  public class OrderGuidEvaluator : IUrlEvaluator<Guid>, IUrlEvaluator
  {
    private string regularExpression;
    private const string urlPrefix = "urlPrefix";
    private const string defaultRegularExpression = "(\\!(?<urlPrefix>\\w*)/)*(?i:order)/(?<order>[^/]*)(?:/|\\z)";
    private const string regexConfigElementName = "regularExpression";
    private const string queryStringIdentifier = "order";
    private const string preDataQueryStringPart = "/order/";

    public Guid GetOrderGuid(string url, string urlKeyPrefix)
    {
      object[] values;
      this.Evaluate(url, (string) null, urlKeyPrefix, out values);
      return (Guid) values[0];
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of the property the will be filtered.</param>
    /// <param name="values">The values the should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public string Evaluate(
      string url,
      string propertyName,
      string urlKeyPrefix,
      out object[] values)
    {
      return this.Evaluate(url, propertyName, (Type) null, urlKeyPrefix, out values);
    }

    /// <summary>Evaluates the specified URL.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      string urlKeyPrefix,
      out object[] values)
    {
      return this.Evaluate(url, propertyName, contentType, UrlEvaluationMode.Default, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">Name of the property that will be filtered.</param>
    /// <param name="contentType">The content type of the filtered items.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <param name="values">The values that should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out object[] values)
    {
      Guid result = Guid.Empty;
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
      {
        string input = SystemManager.CurrentHttpContext.Request.QueryStringGet(urlKeyPrefix + "order");
        if (!string.IsNullOrEmpty(input))
        {
          Guid.TryParse(input, out result);
          values = new object[1]{ (object) result };
        }
        else
          values = new object[0];
        return string.Empty;
      }
      if (!string.IsNullOrWhiteSpace(url))
      {
        MatchCollection matchCollection = Regex.Matches(url, this.regularExpression);
        if (matchCollection.Count > 1)
          throw new ArgumentException("More then one matches. URL string should contain only one match.");
        if (matchCollection.Count == 1 && (string.IsNullOrEmpty(urlKeyPrefix) && !matchCollection[0].Groups["urlPrefix"].Success || matchCollection[0].Groups["urlPrefix"].Success && matchCollection[0].Groups["urlPrefix"].Value == urlKeyPrefix))
          Guid.TryParse(matchCollection[0].Groups["order"].Value, out result);
      }
      values = new object[1]{ (object) result };
      return string.Empty;
    }

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public string BuildUrl(object data, string urlKeyPrefix) => this.BuildUrl((Guid) data, urlKeyPrefix);

    /// <summary>
    /// Builds a URL string based on the provided data and evaluation mode.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="mode">The mode.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public string BuildUrl(object data, UrlEvaluationMode urlEvaluationMode, string urlKeyPrefix) => this.BuildUrl((Guid) data, urlEvaluationMode, urlKeyPrefix);

    public void Initialize(NameValueCollection config)
    {
      this.regularExpression = config != null ? config["regularExpression"] : throw new ArgumentNullException(nameof (config));
      if (!string.IsNullOrEmpty(this.regularExpression))
        return;
      this.regularExpression = "(\\!(?<urlPrefix>\\w*)/)*(?i:order)/(?<order>[^/]*)(?:/|\\z)";
    }

    public string BuildUrl(Guid data, string urlKeyPrefix) => this.BuildUrl(data, UrlEvaluationMode.Default, urlKeyPrefix);

    public string BuildUrl(Guid data, UrlEvaluationMode urlEvaluationMode, string urlKeyPrefix)
    {
      if (urlEvaluationMode == UrlEvaluationMode.QueryString)
        return QueryStringBuilder.Current.Add(urlKeyPrefix + "order", data.ToString(), true).ToString();
      string str = string.Empty;
      if (!string.IsNullOrEmpty(urlKeyPrefix))
        str = str + "/!" + urlKeyPrefix;
      if (data != Guid.Empty)
        str = str + "/order/" + (object) data;
      return str + "/" + QueryStringBuilder.Current.ToString();
    }
  }
}
