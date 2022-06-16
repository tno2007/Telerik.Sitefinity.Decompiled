// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.UrlEvaluator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Helper class for invoking preconfigured URL evaluators.
  /// URL evaluators are used to match URL patterns and build filter expressions based on them.
  /// </summary>
  public static class UrlEvaluator
  {
    private static IDictionary<string, IUrlEvaluator> cache = SystemManager.CreateStaticCache<string, IUrlEvaluator>();

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of the property the will be filtered.</param>
    /// <param name="values">The values the should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public static string Evaluate(
      string evaluatorName,
      string url,
      string propertyName,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out object[] values)
    {
      return UrlEvaluator.Evaluate(evaluatorName, url, propertyName, (Type) null, urlEvaluationMode, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="url">The URL.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="values">The values.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    public static string Evaluate(
      string evaluatorName,
      string url,
      string propertyName,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out object[] values)
    {
      return UrlEvaluator.GetEvaluator(evaluatorName).Evaluate(url, propertyName, contentType, urlEvaluationMode, urlKeyPrefix, out values);
    }

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public static string BuildUrl(string evaluatorName, object data, string urlKeyPrefix) => UrlEvaluator.BuildUrl(evaluatorName, data, UrlEvaluationMode.Default, urlKeyPrefix);

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="data">The data.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <param name="urlKeyPrefix">The URL key prefix.</param>
    /// <returns>The URL built based on the provided data</returns>
    public static string BuildUrl(
      string evaluatorName,
      object data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      return UrlEvaluator.GetEvaluator(evaluatorName).BuildUrl(data, urlEvaluationMode, urlKeyPrefix);
    }

    /// <summary>
    /// Evaluates the specified URL with the specified evaluator and appends filter expression to the provided query, if match is found.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <param name="control">The control.</param>
    /// <param name="query">The query.</param>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public static IQueryable<TData> EvaluateUrl<TData>(
      this Control control,
      IQueryable<TData> query,
      string evaluatorName,
      string propertyName,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      return control.EvaluateUrl<TData>(query, evaluatorName, propertyName, (Type) null, urlEvaluationMode, urlKeyPrefix);
    }

    /// <summary>
    ///  Evaluates the specified URL with the specified evaluator and appends filter expression to the provided query, if match is found.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <param name="control">The control.</param>
    /// <param name="query">The query.</param>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    public static IQueryable<TData> EvaluateUrl<TData>(
      this Control control,
      IQueryable<TData> query,
      string evaluatorName,
      string propertyName,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      object[] values;
      string url = control.EvaluateUrl(evaluatorName, propertyName, contentType, urlEvaluationMode, urlKeyPrefix, out values);
      if (string.IsNullOrEmpty(url))
        return query;
      RouteHelper.SetUrlParametersResolved();
      return query.Where<TData>(url, values);
    }

    /// <summary>Gets the page number.</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public static int GetPageNumber(
      this Control control,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      int defaultValue = 0,
      string evaluatorName = "PageNumber")
    {
      object[] values;
      control.EvaluateUrl(evaluatorName, (string) null, urlEvaluationMode, urlKeyPrefix, out values);
      if (values.Length != 1)
        return defaultValue;
      if (!(values[0] is PaginationParams paginationParams))
        return (int) values[0];
      if (paginationParams.ItemsPerPage != 0 || paginationParams.PageNumber != 0)
        RouteHelper.SetUrlParametersResolved();
      return paginationParams.PageNumber;
    }

    /// <summary>Gets the count of items per page.</summary>
    /// <param name="control">The control.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <param name="urlKeyPrefix">The URL key prefix.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns></returns>
    public static int GetItemsPerPage(
      this Control control,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      int defaultValue = 0,
      string evaluatorName = "ItemsPerPage")
    {
      object[] values;
      control.EvaluateUrl(evaluatorName, (string) null, urlEvaluationMode, urlKeyPrefix, out values);
      if (values.Length != 1)
        return defaultValue;
      if (!(values[0] is PaginationParams paginationParams))
        return (int) values[0];
      if (paginationParams.ItemsPerPage != 0 || paginationParams.PageNumber != 0)
        RouteHelper.SetUrlParametersResolved();
      return paginationParams.ItemsPerPage;
    }

    /// <summary>Attempts to get an OrderId from the URL query string.</summary>
    /// <param name="control">The control.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <param name="urlKeyPrefix">The URL key prefix.</param>
    /// <returns>Returns the Guid that was retrieved from the URL query string or it returns Guid.Empty.</returns>
    public static Guid GetOrderGuid(
      this Control control,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      object[] values;
      control.EvaluateUrl("OrderGuid", (string) null, urlEvaluationMode, urlKeyPrefix, out values);
      Guid orderGuid = values.OfType<Guid>().FirstOrDefault<Guid>();
      if (!(orderGuid != new Guid()))
        return orderGuid;
      RouteHelper.SetUrlParametersResolved();
      return orderGuid;
    }

    /// <summary>
    /// Gets the amount of items to skip depending on the
    /// current page number and items to display per page.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="itemsPerPage">The items tp display per page.</param>
    /// <returns></returns>
    public static int GetItemsToSkipCount(
      this Control control,
      int? itemsPerPage,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      if (!itemsPerPage.HasValue)
        return 0;
      int pageNumber = control.GetPageNumber(urlEvaluationMode, urlKeyPrefix);
      return pageNumber > 0 ? (pageNumber - 1) * itemsPerPage.Value : 0;
    }

    /// <summary>
    /// Evaluates the specified URL with the specified evaluator and returns a filter expression if match is found.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public static string EvaluateUrl(
      this Control control,
      string evaluatorName,
      string propertyName,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out object[] values)
    {
      return control.EvaluateUrl(evaluatorName, propertyName, (Type) null, urlEvaluationMode, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Evaluates the specified URL with the specified evaluator and returns a filter expression if match is found.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="values">The values.</param>
    /// <returns></returns>
    public static string EvaluateUrl(
      this Control control,
      string evaluatorName,
      string propertyName,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out object[] values)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      string empty = string.Empty;
      string url = urlEvaluationMode == UrlEvaluationMode.Default || urlEvaluationMode != UrlEvaluationMode.QueryString ? control.GetUrlParameterString(false) : control.GetQueryString();
      return UrlEvaluator.Evaluate(evaluatorName, url, propertyName, contentType, urlEvaluationMode, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Builds a URL string based on the provided data using the specified evaluator.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <param name="control">The control.</param>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public static string BuildUrl(
      this Control control,
      string evaluatorName,
      object data,
      string urlKeyPrefix)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      return control.BuildUrl(evaluatorName, data, UrlEvaluationMode.Default, urlKeyPrefix);
    }

    /// <summary>
    /// Builds a URL string based on the provided data using the specified evaluator.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="evaluatorName">Name of the evaluator.</param>
    /// <param name="data">The data.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <param name="urlKeyPrefix">The URL key prefix.</param>
    /// <returns>The URL built based on the provided data.</returns>
    public static string BuildUrl(
      this Control control,
      string evaluatorName,
      object data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      return UrlEvaluator.BuildUrl(evaluatorName, data, urlEvaluationMode, urlKeyPrefix);
    }

    /// <summary>Gets the specified URL evaluator.</summary>
    /// <param name="name">The name of the evaluator.</param>
    /// <returns></returns>
    public static IUrlEvaluator GetEvaluator(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(name);
      IUrlEvaluator evaluator;
      if (!UrlEvaluator.cache.TryGetValue(name, out evaluator))
      {
        lock (UrlEvaluator.cache)
        {
          if (!UrlEvaluator.cache.TryGetValue(name, out evaluator))
          {
            DataConfig dataConfig = Config.Get<DataConfig>();
            if (dataConfig.UrlEvaluators.ContainsKey(name))
            {
              DataProviderSettings urlEvaluator = dataConfig.UrlEvaluators[name];
              evaluator = (IUrlEvaluator) ObjectFactory.Resolve(urlEvaluator.ProviderType);
              evaluator.Initialize(urlEvaluator.Parameters);
            }
            else
              evaluator = ObjectFactory.Resolve<IUrlEvaluator>(name);
            UrlEvaluator.cache.Add(name, evaluator);
          }
        }
      }
      return evaluator;
    }
  }
}
