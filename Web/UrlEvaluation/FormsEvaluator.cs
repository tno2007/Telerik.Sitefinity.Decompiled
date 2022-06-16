// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.FormsEvaluator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Telerik.Microsoft.Practices.Unity.Utility;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Evaluates the form description name. The default format is: /ProviderName/FormName/ or /FormName/
  /// </summary>
  public class FormsEvaluator : IUrlEvaluator<Pair<string, string>>, IUrlEvaluator
  {
    private string regularExpression;

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
        string str1 = SystemManager.CurrentHttpContext.Request.QueryStringGet("provider");
        string str2 = SystemManager.CurrentHttpContext.Request.QueryStringGet("formName");
        values = new object[2]
        {
          (object) str1,
          (object) str2
        };
        return string.Empty;
      }
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (!string.IsNullOrWhiteSpace(url))
      {
        MatchCollection matchCollection = Regex.Matches(url, this.regularExpression);
        if (matchCollection.Count > 1)
          throw new ArgumentException("More then one matches. URL string should contain only one match.");
        if (matchCollection.Count == 1)
        {
          Group group = matchCollection[0].Groups["provider"];
          if (group.Success)
            empty1 = group.Value;
          empty2 = matchCollection[0].Groups["formName"].Value;
        }
      }
      values = new object[2]
      {
        (object) empty1,
        (object) empty2
      };
      return string.Empty;
    }

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(Pair<string, string> data, string urlKeyPrefix) => this.BuildUrl(data, UrlEvaluationMode.Default, urlKeyPrefix);

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(
      Pair<string, string> data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
      {
        if (string.IsNullOrEmpty(data.Second))
          throw new ArgumentNullException("data.Second must have the formName value.");
        return string.IsNullOrEmpty(data.First) ? QueryStringBuilder.Current.Add("formName", data.Second, true).ToString() : QueryStringBuilder.Current.Add("provider", data.First, true).Add("formName", data.Second, true).ToString();
      }
      if (string.IsNullOrEmpty(data.Second))
        throw new ArgumentNullException("data.Second must have the formName value.");
      return string.IsNullOrEmpty(data.First) ? string.Format("/{0}", (object) data.Second) : string.Format("/{0}/{1}", (object) data.First, (object) data.Second);
    }

    string IUrlEvaluator.BuildUrl(object data, string urlKeyPrefix) => this.BuildUrl((Pair<string, string>) data, urlKeyPrefix);

    string IUrlEvaluator.BuildUrl(
      object data,
      UrlEvaluationMode mode,
      string urlKeyPrefix)
    {
      return this.BuildUrl((Pair<string, string>) data, mode, urlKeyPrefix);
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
      this.regularExpression = "(?:/(?<provider>[\\w-_]+))?/(?<formName>[\\w-_]+)(?:/?\\z)";
    }
  }
}
