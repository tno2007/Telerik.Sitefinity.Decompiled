// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.UrlQueryEvaluator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Helper class for invoking preconfigured URL query evaluators.
  /// URL query evaluators are used to match URL patterns and build filter expressions based on them.
  /// </summary>
  /// <typeparam name="TData">The type of objects in the query that is filtered</typeparam>
  public static class UrlQueryEvaluator<TData>
  {
    private static IDictionary<string, IUrlQueryEvaluator<TData>> cache = SystemManager.CreateStaticCache<string, IUrlQueryEvaluator<TData>>();

    /// <summary>
    /// Evaluates the specified URL with the specified evaluator and returns the input query accordingly
    /// </summary>
    /// <param name="query">The query</param>
    /// <param name="evaluatorName">Name of the evaluator</param>
    /// <param name="url">The url</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns>The query with the Url filters applied</returns>
    public static IQueryable<TData> Evaluate(
      IQueryable<TData> query,
      string evaluatorName,
      string url,
      UrlEvaluationMode urlEvaluationMode)
    {
      return UrlQueryEvaluator<TData>.GetEvaluator(evaluatorName).Evaluate(query, url, urlEvaluationMode);
    }

    /// <summary>Gets the specified URL query evaluator.</summary>
    /// <param name="name">The name of the evaluator.</param>
    /// <returns>The URL query evaluator object</returns>
    public static IUrlQueryEvaluator<TData> GetEvaluator(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(name);
      IUrlQueryEvaluator<TData> evaluator;
      if (!UrlQueryEvaluator<TData>.cache.TryGetValue(name, out evaluator))
      {
        lock (UrlQueryEvaluator<TData>.cache)
        {
          if (!UrlQueryEvaluator<TData>.cache.TryGetValue(name, out evaluator))
          {
            DataConfig dataConfig = Config.Get<DataConfig>();
            if (dataConfig.UrlEvaluators.ContainsKey(name))
            {
              DataProviderSettings urlEvaluator = dataConfig.UrlEvaluators[name];
              evaluator = (IUrlQueryEvaluator<TData>) ObjectFactory.Resolve(urlEvaluator.ProviderType);
              evaluator.Initialize(urlEvaluator.Parameters);
            }
            else
              evaluator = ObjectFactory.Resolve<IUrlQueryEvaluator<TData>>(name);
            UrlQueryEvaluator<TData>.cache.Add(name, evaluator);
          }
        }
      }
      return evaluator;
    }
  }
}
