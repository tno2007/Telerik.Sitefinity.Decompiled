// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.IMembershipSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// The service returning the settings for membership provider.
  /// </summary>
  [ServiceContract]
  public interface IMembershipSettings
  {
    /// <summary>
    /// Gets the settings for a membership provider.Returns result in JSON.
    /// </summary>
    /// <param name="provider">The membership provider name.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets the settings for a membership provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{provider=null}/?")]
    [OperationContract]
    MembershipProviderSetting GetMembershipProvider(string provider);

    /// <summary>
    /// Gets the settings for a membership provider.Returns result in XML.
    /// </summary>
    /// <param name="provider">The membership provider name.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets the settings for a membership provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{provider=null}/?")]
    [OperationContract]
    MembershipProviderSetting GetMembershipProviderInXml(string provider);

    /// <summary>
    /// Recovers the password of user. Returns results in JSON format.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="provider">The membership provider.</param>
    /// <param name="answer">The password answer.Needed only if membership provider has RequiresQuestionAndAnswer set to true.</param>
    /// <returns>The new password of user</returns>
    [WebHelp(Comment = "Retrieves or resets the password for a membership user. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "recoverPassword/{username}/?provider={provider}&answer={answer}")]
    [OperationContract]
    string RecoverPasswordOfUser(string username, string provider, string answer);

    /// <summary>
    /// Recovers the password of user. Returns results in XML format.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="provider">The membership provider.</param>
    /// <param name="answer">The password answer.Needed only if membership provider has RequiresQuestionAndAnswer set to true.</param>
    /// <returns>The new password of user</returns>
    [WebHelp(Comment = "Retrieves or resets the password for a membership user. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/recoverPassword/{username}/?provider={provider}&answer={answer}")]
    [OperationContract]
    string RecoverPasswordOfUserInXml(string username, string provider, string answer);

    /// <summary>Changes user's password.</summary>
    /// <param name="passwordChangeData">Old and new password.</param>
    /// <param name="userId">Id of the user.</param>
    /// <param name="provider">The name of the membership provider.</param>
    [WebHelp(Comment = "Changes user's password.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/changePassword/{userId}/?provider={provider}")]
    [OperationContract]
    void ChangePassword(WcfPasswordChangeData passwordChangeData, string userId, string provider);

    /// <summary>Gets the roles for users.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Returns the roles for the users. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "getRolesForUser/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<UserRolesItem> GetRolesForUsers(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the roles for users in XML.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Returns the roles for the users. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/getRolesForUser/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<UserRolesItem> GetRolesForUsersInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);
  }
}
