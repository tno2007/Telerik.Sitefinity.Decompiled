// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.AuthorEvaluator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Extracts the username form the URL string. Always the first segment in the URL is assumed.
  /// </summary>
  public class AuthorEvaluator : IUrlEvaluator<IOwnership>, IUrlEvaluator
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

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of the property the will be filtered.</param>
    /// <param name="contentType">The content type of the filtered items.</param>
    /// <param name="values">The values the should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public virtual string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      string urlKeyPrefix,
      out object[] values)
    {
      return this.Evaluate(url, propertyName, contentType, string.Empty, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of the property the will be filtered.</param>
    /// <param name="values">The values the should be passed to the dynamic LINQ.</param>
    /// <param name="membershipProvider">The membership provider name.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public virtual string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      string membershipProvider,
      string urlKeyPrefix,
      out object[] values)
    {
      return this.Evaluate(url, propertyName, contentType, membershipProvider, UrlEvaluationMode.Default, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of that property the will be filtered.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="membershipProvider">The membership provider name.</param>
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
      return this.Evaluate(url, propertyName, contentType, string.Empty, urlEvaluationMode, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of that property the will be filtered.</param>
    /// <param name="membershipProvider">The membership provider name.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <param name="values">The values that should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public virtual string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      string membershipProvider,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out object[] values)
    {
      if (string.IsNullOrEmpty(propertyName))
        throw new ArgumentNullException(nameof (propertyName));
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
      {
        string str = SystemManager.CurrentHttpContext.Request.QueryStringGet(urlKeyPrefix + "author");
        if (!string.IsNullOrEmpty(str))
        {
          User user = !string.IsNullOrEmpty(membershipProvider) ? UserManager.GetManager(membershipProvider).GetUser(str) : UserManager.FindUser(str);
          if (user != null)
          {
            values = new object[1]{ (object) user.Id };
            return string.Format("({0} == @0)", (object) propertyName);
          }
        }
        values = new object[0];
        return string.Empty;
      }
      if (!string.IsNullOrWhiteSpace(url))
      {
        MatchCollection matchCollection = Regex.Matches(url, this.regularExpression);
        if (matchCollection.Count == 1)
        {
          string str = matchCollection[0].Groups["username"].Value;
          User user = !string.IsNullOrEmpty(membershipProvider) ? UserManager.GetManager(membershipProvider).GetUser(str) : UserManager.FindUser(str);
          if (user != null)
          {
            values = new object[1]{ (object) user.Id };
            return string.Format("({0} == @0)", (object) propertyName);
          }
        }
      }
      values = new object[0];
      return string.Empty;
    }

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(IOwnership data, string urlKeyPrefix) => this.BuildUrl(data, (string) null);

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <param name="membershpProvider">The membership provider.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(IOwnership data, string membershipProvider, string urlKeyPrefix) => this.BuildUrl(data, membershipProvider, UrlEvaluationMode.Default, urlKeyPrefix);

    /// <summary>
    /// Builds a URL string based on the provided data and url evaluation mode.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(
      IOwnership data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      return this.BuildUrl(data, (string) null, urlEvaluationMode, urlKeyPrefix);
    }

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(
      IOwnership data,
      string membershipProvider,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      User user = !string.IsNullOrEmpty(membershipProvider) ? UserManager.GetManager(membershipProvider).GetUser(data.Owner) : UserManager.FindUser(data.Owner);
      if (urlEvaluationMode == UrlEvaluationMode.Default)
        return "/" + user.UserName;
      return urlEvaluationMode == UrlEvaluationMode.QueryString ? QueryStringBuilder.Current.Add(urlKeyPrefix + "author", user.UserName, true).ToString() : "/" + user.UserName;
    }

    string IUrlEvaluator.BuildUrl(object data, string urlKeyPrefix) => this.BuildUrl((IOwnership) data, urlKeyPrefix);

    string IUrlEvaluator.BuildUrl(
      object data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      return this.BuildUrl((IOwnership) data, (string) null, urlEvaluationMode, urlKeyPrefix);
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
      this.regularExpression = "^/(?<username>[\\w|\\-|_|\\.]+)(?:/|\\z)$";
    }
  }
}
