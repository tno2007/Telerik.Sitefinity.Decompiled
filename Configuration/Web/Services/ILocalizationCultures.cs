// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.ILocalizationCultures
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Configuration.Web
{
  /// <summary>
  /// Mandates the members to be implemented by every WCF service which implements service for
  /// localization cultures.
  /// </summary>
  [ServiceContract]
  public interface ILocalizationCultures
  {
    /// <summary>
    /// Gets the collection <see cref="T:Telerik.Sitefinity.Localization.Configuration.CultureElement" /> objects in JSON format, which represent
    /// the cultures used by the current installation and defined through the configuration.
    /// </summary>
    /// <param name="sortExpression">The sort expression used to order cultures.</param>
    /// <param name="skip">The number of cultures to skip before taking the subset.</param>
    /// <param name="take">The number of cultures to take in the subset.</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to format cultures.</param>
    /// <param name="provider">
    /// The name of the configuration provider provider used to retrieved the list of cultures.
    /// </param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with the list of CultureElement
    /// objects.
    /// </returns>
    [WebHelp(Comment = "Gets all supported cultures configured for the current installation of Sitefinity in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={provider}")]
    [OperationContract]
    CollectionContext<CultureElement> GetCultures(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider);
  }
}
