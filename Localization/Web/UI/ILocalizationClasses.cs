// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.ILocalizationClasses
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Localization.Web
{
  /// <summary>
  /// Mandates the members to be implemented by every WCF service which implements service for
  /// localization classes.
  /// </summary>
  [ServiceContract]
  public interface ILocalizationClasses
  {
    /// <summary>
    /// Gets the collection of localization classes in JSON format.
    /// </summary>
    /// <param name="sortExpression">The sortExpression expression used to sort the localization classes.</param>
    /// <param name="skip">The number of classes to skip before taking the subset.</param>
    /// <param name="take">The number of classes to take in the subset.</param>
    /// <param name="filter">The filter expression in dynamic LINQ form used to filter the classes.</param>
    /// <param name="provider">The localization provider used to retrieve the classes.</param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with the collection of localization classes.
    /// </returns>
    [WebHelp(Comment = "The method returns all the resource classes in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={provider}")]
    [OperationContract]
    CollectionContext<string> GetClasses(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider);

    /// <summary>
    /// Gets the collection of localization classes in XML format.
    /// </summary>
    /// <param name="sortExpression">The sort expression used to sort the localization classes.</param>
    /// <param name="skip">The number of classes to skip before taking the subset.</param>
    /// <param name="take">The number of classes to take in the subset.</param>
    /// <param name="filter">The filter expression in dynamic LINQ form used to filter the classes.</param>
    /// <param name="provider">The localization provider used to retrieve the classes.</param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with the collection of localization classes.
    /// </returns>
    [WebHelp(Comment = "The method returns all the resource classes in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={provider}")]
    [OperationContract]
    CollectionContext<string> GetClassesInXml(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider);
  }
}
