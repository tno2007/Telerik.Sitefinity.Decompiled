// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.IMailingListService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Interface that defines the List service for the newsletters module.
  /// </summary>
  [ServiceContract]
  public interface IMailingListService
  {
    /// <summary>
    /// Saves a newsletter list. If the list with specified id exists that list will be updated; otherwise new list will be created.
    /// The saved list is returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">Id of the list to be saved.</param>
    /// <param name="mailingList">The view model of the list object.</param>
    /// <param name="provider">The provider through which the list ought to be saved.</param>
    /// <returns>The saved list.</returns>
    [WebHelp(Comment = "Saves a newsletter list. If the list with specified id exists that list will be updated; otherwise new list will be created. The saved list is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{mailingListId}/?provider={provider}")]
    [OperationContract]
    MailingListViewModel SaveMailingList(
      string mailingListId,
      MailingListViewModel mailingList,
      string provider);

    /// <summary>
    /// Saves a newsletter list. If the list with specified id exists that list will be updated; otherwise new list will be created.
    /// The saved list is returned in XML format.
    /// </summary>
    /// <param name="listId">The list id.</param>
    /// <param name="list">The view model of the list object.</param>
    /// <param name="provider">The provider through which the list ought to be saved.</param>
    /// <returns>The saved list.</returns>
    [WebHelp(Comment = "Saves a newsletter list. If the list with specified id exists that list will be updated; otherwise new list will be created. The saved list is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{mailingListId}/?provider={provider}")]
    [OperationContract]
    MailingListViewModel SaveMailingListInXml(
      string mailingListId,
      MailingListViewModel mailingList,
      string provider);

    /// <summary>
    /// Gets all mailing lists of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the mailing lists ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the mailing mailing lists.</param>
    /// <param name="skip">Number of mailing lists to skip in result set. (used for paging)</param>
    /// <param name="take">Number of mailing lists to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MailingListViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all lists of the newsletter module for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<MailingListViewModel> GetMailingLists(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all mailing lists of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the mailing lists ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the mailing lists.</param>
    /// <param name="skip">Number of mailing lists to skip in result set. (used for paging)</param>
    /// <param name="take">Number of mailing lists to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.MailingListViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all lists of the newsletter module for the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<MailingListViewModel> GetMailingListsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Deletes the mailing list by id and returns true if the mailing list has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">Id of the mailing list to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes list for given provider and supplied id. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{mailingListId}/?provider={provider}")]
    [OperationContract]
    bool DeleteMailingList(string mailingListId, string provider);

    /// <summary>
    /// Deletes the mailing list by id and returns true if the mailing list has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="mailingListId">Id of the mailing list to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes mailing list for given provider and supplied id. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{mailingListId}/?provider={provider}")]
    [OperationContract]
    bool DeleteMailingListInXml(string mailingListId, string provider);

    /// <summary>
    /// Deletes a collection of mailing lists. Result is returned in JSON format.
    /// </summary>
    /// <param name="mailingListIds">An array of the ids of the mailing lists to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all mailing lists have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple mailing lists.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteMailingLists(string[] mailingListIds, string provider);

    /// <summary>
    /// Deletes a collection of mailing lists. Result is returned in XML format.
    /// </summary>
    /// <param name="mailingListIds">An array of the ids of the mailing lists to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all mailing lists have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple mailing lists.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteMailingListsInXml(string[] mailingListIds, string provider);

    /// <summary>Gets the mailing list and returns it in JSON format.</summary>
    /// <param name="mailingListId">Id of the mailing list that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the mailing list.</param>
    /// <returns>An instance of MailingListViewModel.</returns>
    [WebHelp(Comment = "Gets the mailing list and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{mailingListId}/?provider={provider}")]
    [OperationContract]
    MailingListViewModel GetMailingList(string mailingListId, string provider);

    /// <summary>Gets the mailing list and returns it in XML format.</summary>
    /// <param name="mailingListId">Id of the mailing list that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the mailing list.</param>
    /// <returns>An instance of MailingListViewModel.</returns>
    [WebHelp(Comment = "Gets the mailing list and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{mailingListId}/?provider={provider}")]
    [OperationContract]
    MailingListViewModel GetMailingListInXml(
      string mailingListId,
      string provider);
  }
}
