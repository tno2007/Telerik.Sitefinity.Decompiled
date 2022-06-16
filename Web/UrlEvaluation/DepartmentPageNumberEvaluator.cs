// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.DepartmentPageNumberEvaluator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Evaluates a page number in the URL. The default format is: /Page/4/
  /// </summary>
  public class DepartmentPageNumberEvaluator : IUrlEvaluator<int>, IUrlEvaluator
  {
    private string regularExpression;

    /// <summary>
    /// Gets the page number if there is a match in the URL string.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns></returns>
    public virtual int GetPageNumber(string url, string urlKeyPrefix)
    {
      object[] values;
      this.Evaluate(url, (string) null, urlKeyPrefix, out values);
      return (int) values[0];
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
    public virtual string Evaluate(
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
    public virtual string Evaluate(
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
    public virtual string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out object[] values)
    {
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
      {
        string s = SystemManager.CurrentHttpContext.Request.QueryStringGet(urlKeyPrefix + "page");
        if (!string.IsNullOrEmpty(s))
        {
          int num = int.Parse(s);
          values = new object[1]{ (object) num };
        }
        else
          values = new object[0];
        return string.Empty;
      }
      int num1 = 0;
      if (!string.IsNullOrWhiteSpace(url))
      {
        MatchCollection matchCollection = Regex.Matches(url, this.regularExpression);
        if (matchCollection.Count > 1)
          throw new ArgumentException("More then one matches. URL string should contain only one match.");
        if (matchCollection.Count == 1 && (string.IsNullOrEmpty(urlKeyPrefix) && !matchCollection[0].Groups["urlPrefix"].Success || matchCollection[0].Groups["urlPrefix"].Success && matchCollection[0].Groups["urlPrefix"].Value == urlKeyPrefix))
        {
          num1 = int.Parse(matchCollection[0].Groups["page"].Value);
          RouteHelper.SetUrlParametersResolved();
        }
      }
      values = new object[1]{ (object) num1 };
      return string.Empty;
    }

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(int data, string urlKeyPrefix) => this.BuildUrl(data, UrlEvaluationMode.Default, urlKeyPrefix);

    /// <summary>
    /// Builds a URL string based on the provided data and evaluation mode.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="mode">The mode.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(
      int data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
        return QueryStringBuilder.Current.Add(urlKeyPrefix + "page", data.ToString(), true).ToString();
      string str1 = string.Empty;
      if (!string.IsNullOrEmpty(urlKeyPrefix))
        str1 = str1 + "/!" + urlKeyPrefix;
      if (data > 1)
        str1 = str1 + "/page/" + (object) data;
      TaxonomyEvaluator taxonomyEvaluator = new TaxonomyEvaluator();
      string empty = string.Empty;
      string url = urlEvaluationMode == UrlEvaluationMode.Default || urlEvaluationMode != UrlEvaluationMode.QueryString ? this.GetUrlParameterString(false) : this.GetQueryString();
      if (!string.IsNullOrWhiteSpace(url))
      {
        object[] values;
        UrlEvaluator.Evaluate("Taxonomy", url, (string) null, UrlEvaluationMode.Default, string.Empty, out values);
        if (values.Length != 0 && values[0].ToString() != Guid.Empty.ToString())
        {
          ITaxon taxon1 = TaxonomyManager.GetManager().GetTaxon(new Guid(values[0].ToString()));
          string taxon2 = taxon1 is HierarchicalTaxon ? (taxon1 as HierarchicalTaxon).FullUrl : taxon1.UrlName.Value;
          TaxonBuildOptions options = TaxonBuildOptions.None;
          switch (taxon1)
          {
            case HierarchicalTaxonomy _:
              options = TaxonBuildOptions.Hierarchical;
              break;
            case FlatTaxonomy _:
              options = TaxonBuildOptions.Flat;
              break;
          }
          string str2 = taxonomyEvaluator.BuildUrl("Departments", taxon2, "Department", options, UrlEvaluationMode.Default, (string) null);
          str1 += !str1.EndsWith("/") || !str2.StartsWith("/") ? str2 : str2.Substring(1);
        }
      }
      return str1 + "/" + QueryStringBuilder.Current.ToString();
    }

    public string GetUrlParameterString(bool excludePrefixedParams)
    {
      string[] urlParameters = this.GetUrlParameters();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("/");
      if (urlParameters != null)
      {
        foreach (string str in urlParameters)
        {
          if (!excludePrefixedParams || !str.StartsWith("!"))
            stringBuilder.Append(str + "/");
        }
      }
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      string str1 = stringBuilder.ToString();
      return !string.IsNullOrEmpty(str1) ? str1 : (string) null;
    }

    /// <summary>Gets the query string (as a string).</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public string GetQueryString()
    {
      NameValueCollection stringParameters = this.GetQueryStringParameters();
      if (stringParameters == null)
        return (string) null;
      string[] allKeys = stringParameters.AllKeys;
      QueryStringBuilder queryStringBuilder = new QueryStringBuilder();
      foreach (string name in allKeys)
        queryStringBuilder.Add(name, stringParameters[name]);
      return queryStringBuilder.ToString();
    }

    /// <returns></returns>
    public string[] GetUrlParameters() => SystemManager.CurrentHttpContext.Request.RequestContext.RouteData.Values["Params"] as string[];

    /// <summary>Gets the query string parameters.</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public NameValueCollection GetQueryStringParameters() => SystemManager.CurrentHttpContext.Request.RequestContext.RouteData.Values["Query"] as NameValueCollection;

    string IUrlEvaluator.BuildUrl(object data, string urlKeyPrefix) => this.BuildUrl((int) data, urlKeyPrefix);

    string IUrlEvaluator.BuildUrl(
      object data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      return this.BuildUrl((int) data, urlEvaluationMode, urlKeyPrefix);
    }

    /// <summary>
    /// Initializes this instance with the provided configuration information.
    /// </summary>
    /// <param name="config"></param>
    public virtual void Initialize(NameValueCollection config)
    {
      this.regularExpression = config != null ? config["regularExpression"] : throw new ArgumentNullException(nameof (config));
      if (!string.IsNullOrEmpty(this.regularExpression))
        return;
      this.regularExpression = "(\\!(?<urlPrefix>\\w*)/)*(?i:page)/(?<page>\\d*)(?:/|\\z)";
    }
  }
}
