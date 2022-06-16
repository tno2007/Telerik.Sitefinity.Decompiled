// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.IUrlEvaluator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>Represents an interface for URL evaluators.</summary>
  public interface IUrlEvaluator
  {
    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of the property the will be filtered.</param>
    /// <param name="values">The values the should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    string Evaluate(string url, string propertyName, string key, out object[] values);

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
    string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      string key,
      out object[] values);

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
    string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string key,
      out object[] values);

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    string BuildUrl(object data, string urlKeyPrefix);

    /// <summary>
    /// Builds a URL string based on the provided data and url evaluation mode.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns>The URL build based on the provided data and url evaluation mode.</returns>
    string BuildUrl(object data, UrlEvaluationMode urlEvaluationMode, string urlKeyPrefix);

    /// <summary>
    /// Initializes this instance with the provided configuration information.
    /// </summary>
    /// <param name="config"></param>
    void Initialize(NameValueCollection config);
  }
}
