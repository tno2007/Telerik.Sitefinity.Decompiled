// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ISubscriberService
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
  /// Interface that defines the Subscriber service for the newsletters module.
  /// </summary>
  [ServiceContract]
  public interface ISubscriberService
  {
    /// <summary>
    /// Saves a newsletter subscriber. If the subscriber with specified id exists that subscriber will be updated; otherwise new subscriber will be created.
    /// The saved subscriber is returned in JSON format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber to be saved.</param>
    /// <param name="subscriber">The view model of the subscriber object.</param>
    /// <param name="provider">The provider through which the subscriber ought to be saved.</param>
    /// <returns>The saved subscriber.</returns>
    [WebHelp(Comment = "Saves a newsletter subscriber. If the subscriber with specified id exists that subscriber will be updated; otherwise new subscriber will be created. The saved subscriber is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{subscriberId}/?provider={provider}")]
    [OperationContract]
    SubscriberViewModel SaveSubscriber(
      string subscriberId,
      SubscriberViewModel subscriber,
      string provider);

    /// <summary>
    /// Saves a newsletter subscriber. If the subscriber with specified id exists that subscriber will be updated; otherwise new subscriber will be created.
    /// The saved subscriber is returned in XML format.
    /// </summary>
    /// <param name="subscriberId">The subscriber id.</param>
    /// <param name="subscriber">The view model of the subscriber object.</param>
    /// <param name="provider">The provider through which the subscriber ought to be saved.</param>
    /// <returns>The saved subscriber.</returns>
    [WebHelp(Comment = "Saves a newsletter subscriber. If the subscriber with specified id exists that subscriber will be updated; otherwise new subscriber will be created. The saved subscriber is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{subscriberId}/?provider={provider}")]
    [OperationContract]
    SubscriberViewModel SaveSubscriberInXml(
      string subscriberId,
      SubscriberViewModel subscriber,
      string provider);

    /// <summary>
    /// Gets all subscribers of the newsletter module for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the subscribers.</param>
    /// <param name="skip">Number of subscribers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of subscribers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all subscribers of the newsletter module for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<SubscriberViewModel> GetSubscribers(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all subscribers of the newsletter module for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the subscribers.</param>
    /// <param name="skip">Number of subscribers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of subscribers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberViewModel" /> objects.</returns>
    [WebHelp(Comment = "Gets all subscribers of the newsletter module for the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<SubscriberViewModel> GetSubscribersInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all subscribers of the newsletter module for the given provider and mailing list. The results are returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the subscribers.</param>
    /// <param name="skip">Number of subscribers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of subscribers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <param name="dynamicListKey">The dynamic list key. If presented, the method will return the subscribers from the given dynamic list.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberViewModel" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets all subscribers of the newsletter module for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/mailingList/{mailingListId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&dynamicListKey={dynamicListKey}")]
    [OperationContract]
    CollectionContext<SubscriberViewModel> GetMailingListSubscribers(
      string mailingListId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string dynamicListKey);

    /// <summary>
    /// Gets all subscribers of the newsletter module for the given provider and mailing list. The results are returned in XML format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the subscribers.</param>
    /// <param name="skip">Number of subscribers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of subscribers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <param name="dynamicListKey">The dynamic list key. If presented, the method will return the subscribers from the given dynamic list.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SubscriberViewModel" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets all subscribers of the newsletter module for the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/mailingList/{mailingListId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&dynamicListKey={dynamicListKey}")]
    [OperationContract]
    CollectionContext<SubscriberViewModel> GetMailingListSubscribersInXml(
      string mailingListId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string dynamicListKey);

    /// <summary>
    /// Deletes the subscriber by id and returns true if the subscriber has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes the subscriber for given provider and supplied id. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{subscriberId}/?provider={provider}")]
    [OperationContract]
    bool DeleteSubscriber(string subscriberId, string provider);

    /// <summary>
    /// Deletes the subscriber by id and returns true if the subscriber has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes the subscriber for given provider and supplied id. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{subscriberId}/?provider={provider}")]
    [OperationContract]
    bool DeleteSubscriberInXml(string subscriberId, string provider);

    /// <summary>
    /// Deletes a collection of subscribers. Result is returned in JSON format.
    /// </summary>
    /// <param name="subscriberIds">An array of the ids of the subscribers to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all subscribers have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple subscribers.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteSubscribers(string[] subscriberIds, string provider);

    /// <summary>
    /// Deletes a collection of subscribers. Result is returned in XML format.
    /// </summary>
    /// <param name="subscriberIds">An array of the ids of the subscribers to delete.</param>
    /// <param name="provider">The name of the newsletter provider.</param>
    /// <returns>True if all subscribers have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple subscribers.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/batchdelete/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteSubscribersInXml(string[] subscriberIds, string provider);

    /// <summary>
    /// Adding existing subscribers to a mailing list. The results are returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="subscriberIds">An array of the ids of the subscribers to add.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <returns>
    /// True if the subscribers have been successfully added; otherwise false.
    /// </returns>
    [WebHelp(Comment = "Adding existing subscribers to a mailing list. The results are returned in JSON format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/add/{mailingListId}/?provider={provider}")]
    [OperationContract]
    bool AddSubscribers(string mailingListId, string[] subscriberIds, string provider);

    /// <summary>
    /// Adding existing subscribers to a mailing list. The results are returned in XML format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="subscriberIds">An array of the ids of the subscribers to add.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <returns>
    /// True if the subscribers have been successfully added; otherwise false.
    /// </returns>
    [WebHelp(Comment = "Adding existing subscribers to a mailing list. The results are returned in XML format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/add/{mailingListId}/?provider={provider}")]
    [OperationContract]
    bool AddSubscribersInXml(string mailingListId, string[] subscriberIds, string provider);

    /// <summary>
    /// Removing existing subscribers from a mailing list. The results are returned in JSON format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="subscriberIds">An array of the ids of the subscribers to remove.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <returns>
    /// True if the subscribers have been successfully removed; otherwise false.
    /// </returns>
    [WebHelp(Comment = "Removing existing subscribers from a mailing list. The results are returned in JSON format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/remove/{mailingListId}/?provider={provider}")]
    [OperationContract]
    bool RemoveSubscribers(string mailingListId, string[] subscriberIds, string provider);

    /// <summary>
    /// Removing existing subscribers from a mailing list. The results are returned in XML format.
    /// </summary>
    /// <param name="mailingListId">The mailing list id.</param>
    /// <param name="subscriberIds">An array of the ids of the subscribers to remove.</param>
    /// <param name="provider">Name of the provider from which the subscribers ought to be retrieved.</param>
    /// <returns>
    /// True if the subscribers have been successfully removed; otherwise false.
    /// </returns>
    [WebHelp(Comment = "Removing existing subscribers from a mailing list. The results are returned in XML format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/remove/{mailingListId}/?provider={provider}")]
    [OperationContract]
    bool RemoveSubscribersInXml(string mailingListId, string[] subscriberIds, string provider);

    /// <summary>
    /// Gets the single subscriber item and returns it in JSON format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the subscriber.</param>
    /// <returns>An instance of SubscriberViewModel.</returns>
    [WebHelp(Comment = "Gets the single subscriber item and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{subscriberId}/?provider={provider}")]
    [OperationContract]
    SubscriberViewModel GetSubscriber(string subscriberId, string provider);

    /// <summary>
    /// Gets the single subscriber item and returns it in XML format.
    /// </summary>
    /// <param name="subscriberId">Id of the subscriber that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider that is to be used to retrieve the subscriber.</param>
    /// <returns>An instance of SubscriberViewModel.</returns>
    [WebHelp(Comment = "Gets the single subscriber item and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{subscriberId}/?provider={provider}")]
    [OperationContract]
    SubscriberViewModel GetSubscriberInXml(string subscriberId, string provider);
  }
}
