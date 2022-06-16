// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Web.Services.ICommentsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Data.WcfHelpers;

namespace Telerik.Sitefinity.Services.Content.Web.Services
{
  /// <summary>
  /// Defines a service that operates with comments. This service only exposes methods which are not specific to the
  /// Generic Content functionality of comments. This is why it cannot create/delete/get comments.
  /// </summary>
  [ServiceContract]
  [AllowDynamicFields]
  public interface ICommentsService
  {
    /// <summary>Hides the comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <param name="commentId">The comment pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/hide/{commentId}/?provider={providerName}&managerType={managerType}")]
    void HideComment(string commentId, string providerName, string managerType);

    /// <summary>Publishes the comment.</summary>
    /// <param name="commentId">The comment pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/publish/{commentId}/?provider={providerName}&managerType={managerType}")]
    void PublishComment(string commentId, string providerName, string managerType);

    /// <summary>Marks the comment as spam.</summary>
    /// <param name="comment">The comment.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/spam/{commentId}/?provider={providerName}&managerType={managerType}")]
    void MarkCommentAsSpam(string commentId, string providerName, string managerType);

    /// <summary>Blocks the comments coming from the given IP</summary>
    /// <param name="ip">The ip.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/blockip/{ip}/?provider={providerName}&managerType={managerType}")]
    void BlockCommentsForIp(string ip, string providerName, string managerType);

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/blockemail/{email}/?provider={providerName}&managerType={managerType}")]
    void BlockCommentsForEmail(string email, string providerName, string managerType);
  }
}
