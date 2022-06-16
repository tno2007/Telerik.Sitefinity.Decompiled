// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.IUrlQueryEvaluator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Linq;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>Represents an interface for URL query evaluators.</summary>
  /// <typeparam name="TData">The type of the data.</typeparam>
  public interface IUrlQueryEvaluator<TData>
  {
    /// <summary>
    /// Initializes this instance with the provided configuration information.
    /// </summary>
    /// <param name="config">The configuration information</param>
    void Initialize(NameValueCollection config);

    /// <summary>
    /// Evaluates the specified URL with the specified evaluator and returns the input query accordingly
    /// </summary>
    /// <param name="query">The query</param>
    /// <param name="url">The url</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns>The query with the Url filters applied</returns>
    IQueryable<TData> Evaluate(
      IQueryable<TData> query,
      string url,
      UrlEvaluationMode urlEvaluationMode);
  }
}
