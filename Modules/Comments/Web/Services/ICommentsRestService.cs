// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.Services.ICommentsRestService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Comments.Web.Services
{
  [Obsolete("Use CommentWebService instead.")]
  [ServiceContract(Namespace = "Telerik.Sitefinity.Modules.Comments.Web.Services")]
  internal interface ICommentsRestService
  {
    /// <summary>Edits the comment.</summary>
    /// <param name="key">The key.</param>
    /// <param name="comment">The comment.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{key}")]
    CommentViewModel EditComment(CommentSubmitData comment, string key);

    /// <summary>Gets the comment by key.</summary>
    /// <param name="commentKey">The comment key.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "single/?commentKey={commentKey}")]
    CommentExtendedViewModel GetCommentByKey(string commentKey);

    /// <summary>Gets the comments extended.</summary>
    /// <param name="language">The language.</param>
    /// <param name="status">The status.</param>
    /// <param name="sortDirection">The sort direction.</param>
    /// <param name="take">The take.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="newerThan">The date filter.</param>
    /// <param name="olderThan">The date filter.</param>
    /// <param name="olderThan">The thread type.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "backend/?language={language}&status={status}&sortDirection={sortDirection}&take={take}&skip={skip}&newerThan={newerThan}&olderThan={olderThan}&threadType={threadType}")]
    CollectionContext<CommentExtendedViewModel> GetCommentsExtended(
      string language,
      string status,
      string sortDirection,
      string take,
      string skip,
      string newerThan,
      string olderThan,
      string threadType);

    /// <summary>Gets the comments extended by thread.</summary>
    /// <param name="thread">The thread.</param>
    /// <param name="status">The status.</param>
    /// <param name="sortDirection">The sort direction.</param>
    /// <param name="take">The take.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="newerThan">The date filter.</param>
    /// <param name="olderThan">The date filter.</param>
    /// <param name="olderThan">The thread type.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "backend/byThread/?thread={thread}&language={language}&status={status}&sortDirection={sortDirection}&take={take}&skip={skip}&newerThan={newerThan}&olderThan={olderThan}&threadType={threadType}")]
    CollectionContext<CommentExtendedViewModel> GetCommentsExtendedByThread(
      string thread,
      string language,
      string status,
      string sortDirection,
      string take,
      string skip,
      string newerThan,
      string olderThan,
      string threadType);

    /// <summary>Publish comment.</summary>
    /// <param name="commentKey">The comment key.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Publish/")]
    bool Publish(string commentKey);

    /// <summary>Hide comment.</summary>
    /// <param name="commentKey">The comment key.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Hide/")]
    bool Hide(string commentKey);

    /// <summary>Marks the comment as spam.</summary>
    /// <param name="commentKey">The comment key.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "MarkAsSpam/")]
    bool MarkAsSpam(string commentKey);

    /// <summary>Deletes the comment.</summary>
    /// <param name="commentKey">The comment key.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "")]
    bool DeleteComment(string commentKey);

    /// <summary>Close thread for new comments.</summary>
    /// <param name="threadKey">The thread key.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CloseThread/")]
    bool CloseThread(ThreadIdentificationViewModel thread);

    /// <summary>Open thread for new comments.</summary>
    /// <param name="threadKey">The thread key.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "OpenThread/")]
    bool OpenThread(ThreadIdentificationViewModel thread);

    /// <summary>Batch comments publish by list of comment keys.</summary>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "backend/group-publish/")]
    bool BatchCommentsPublish(string[] commentKeys);

    /// <summary>Batch comments delete by list of comment keys.</summary>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "backend/group-delete/")]
    bool BatchCommentsDelete(string[] commentKeys);

    /// <summary>Batch comments hide by list of comment keys.</summary>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "backend/group-hide/")]
    bool BatchCommentsHide(string[] commentKeys);

    /// <summary>Batch comments spam by list of comment keys.</summary>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "backend/group-spam/")]
    bool BatchCommentsSpam(string[] commentKeys);

    /// <summary>Gets the comments by thread.</summary>
    /// <param name="thread">The thread.</param>
    /// <param name="olderThan">The older than.</param>
    /// <param name="newerThan">The newer than.</param>
    /// <param name="sortDirection">The sort direction.</param>
    /// <param name="take">The take.</param>
    /// <param name="skip">The skip.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "byThread/?thread={thread}&language={language}&olderThan={olderThan}&newerThan={newerThan}&sortDirection={sortDirection}&take={take}&skip={skip}")]
    CollectionContext<CommentViewModel> GetCommentsByThread(
      string thread,
      string language,
      string olderThan,
      string newerThan,
      string sortDirection,
      string take,
      string skip);

    /// <summary>Submits the comment.</summary>
    /// <param name="comment">The comment.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "")]
    CommentViewModel SubmitComment(CommentSubmitData comment);

    /// <summary>Submits the comment and create thread and/or group.</summary>
    /// <param name="comment">The comment.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "extended/")]
    CommentViewModel SubmitCommentExtended(CommentSubmitDataExtended comment);

    /// <summary>Submits the comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <param name="answer">The correctAnswer hash.</param>
    /// <param name="correctAnswer">The comment.</param>
    /// <param name="initializationVector">The initialization vector.</param>
    /// <param name="key">The salt.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "withCaptcha/?answer={answer}&correctAnswer={correctAnswer}&initializationVector={initializationVector}&key={key}")]
    CommentViewModel SubmitCaptchaComment(
      CommentSubmitData comment,
      string answer,
      string correctAnswer,
      string initializationVector,
      string key);

    /// <summary>Submits the comment and create thread and/or group.</summary>
    /// <param name="comment">The comment.</param>
    /// <param name="answer">The correctAnswer hash.</param>
    /// <param name="correctAnswer">The comment.</param>
    /// <param name="initializationVector">The initialization vector.</param>
    /// <param name="key">The salt.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "withCaptchaExtended/?answer={answer}&correctAnswer={correctAnswer}&initializationVector={initializationVector}&key={key}")]
    CommentViewModel SubmitCaptchaCommentExtended(
      CommentSubmitDataExtended comment,
      string answer,
      string correctAnswer,
      string initializationVector,
      string key);

    /// <summary>Gets the comments count by ids.</summary>
    /// <param name="language">The language.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "getMultipleCommentCounts/")]
    string GetMultipleCommentCounts(string[] threadKeys);

    /// <summary>Gets the comments count by thread.</summary>
    /// <param name="thread">The thread.</param>
    /// <param name="status">The status.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "countByThread/?thread={thread}&status={status}")]
    int GetCommentsCountByThread(string thread, string status);

    /// <summary>Gets the comments count by group.</summary>
    /// <param name="language">The language.</param>
    /// <param name="status">The status.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "countByGroups/?language={language}&status={status}")]
    int GetCommentsCountByGroups(string language, string status);

    /// <summary>Gets the captcha.</summary>
    /// <returns>Catcha image and a corresponding key.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Captcha")]
    CaptchaResponse GetCaptcha();

    /// <summary>Gets the comments security settings.</summary>
    /// <param name="threadType">The thread type.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "getSecuritySettings/?threadType={threadType}")]
    CommentsSecurityViewModel GetSecuritySettings(string threadType);
  }
}
