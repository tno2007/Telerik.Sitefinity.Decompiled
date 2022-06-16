// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.IUrlEvaluator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>Represents an interface for URL evaluators.</summary>
  /// <typeparam name="TData">The type of the data.</typeparam>
  public interface IUrlEvaluator<TData> : IUrlEvaluator
  {
    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    string BuildUrl(TData data, string urlKeyPrefix);

    /// <summary>
    /// Builds a URL string based on the provided data and url evaluation mode.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns>The URL build based on the provided data and url evaluation mode.</returns>
    string BuildUrl(TData data, UrlEvaluationMode urlEvaluationMode, string urlKeyPrefix);
  }
}
