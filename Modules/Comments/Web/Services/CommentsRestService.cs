// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.Services.CommentsRestService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Services.Comments.DTO;
using Telerik.Sitefinity.Services.Comments.Proxies;
using Telerik.Sitefinity.Services.UserSession;
using Telerik.Sitefinity.Services.UserSession.DTO;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Comments.Web.Services
{
  [Obsolete("Use CommentWebService instead.")]
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  internal class CommentsRestService : ICommentsRestService
  {
    /// <summary>Edits the comment.</summary>
    /// <param name="commentData">The comment data.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public CommentViewModel EditComment(CommentSubmitData commentData, string key)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      CommentResponse cmntResponse = new CommentWebService().Put(new CommentUpdateRequest()
      {
        Key = key,
        Message = commentData.Message,
        Name = commentData.Name,
        Email = commentData.Email,
        Rating = commentData.Rating
      });
      AuthorProxy authorProxy = new AuthorProxy(cmntResponse.Name, cmntResponse.Email);
      return this.CreateCommentFromResponse(cmntResponse, authorProxy);
    }

    /// <summary>Gets the comment by key.</summary>
    /// <param name="commentKey">The comment key.</param>
    /// <returns></returns>
    public CommentExtendedViewModel GetCommentByKey(string commentKey)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      CommentWebService commentWebService = new CommentWebService();
      CommentResponse cmntResponse = commentWebService.Get(new CommentGetRequest()
      {
        Key = commentKey
      });
      AuthorProxy author = new AuthorProxy(cmntResponse.Name, cmntResponse.Email);
      CollectionResponse<ThreadResponse> collectionResponse = commentWebService.Post(new ThreadsFilterExtended()
      {
        ThreadKey = {
          cmntResponse.ThreadKey
        }
      });
      return this.CreateCommentExtendedFromResponse(cmntResponse, collectionResponse.Items, author);
    }

    /// <summary>Gets the comments extended.</summary>
    /// <param name="language">The language.</param>
    /// <param name="status">The status.</param>
    /// <param name="sortDirection">The sort direction.</param>
    /// <param name="take">The take.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="newerThen">The date filter.</param>
    /// <param name="olderThan">The date filter.</param>
    /// <param name="olderThan">The thread type.</param>
    public CollectionContext<CommentExtendedViewModel> GetCommentsExtended(
      string language,
      string status,
      string sortDirection,
      string take,
      string skip,
      string newerThan,
      string olderThan,
      string threadType)
    {
      CommentWebService commentWebService = new CommentWebService();
      List<string> stringList1 = new List<string>();
      if (!language.IsNullOrWhitespace())
        stringList1.Add(language);
      List<string> stringList2 = new List<string>();
      if (!status.IsNullOrWhitespace())
        stringList2.Add(status);
      List<string> stringList3 = new List<string>();
      if (!threadType.IsNullOrWhitespace())
        stringList3.Add(threadType);
      bool flag = string.Equals("desc", sortDirection, StringComparison.OrdinalIgnoreCase);
      int result1;
      int.TryParse(take, out result1);
      int result2;
      int.TryParse(skip, out result2);
      DateTime result3;
      DateTime.TryParse(newerThan, out result3);
      DateTime result4;
      DateTime.TryParse(olderThan, out result4);
      CollectionResponse<CommentResponse> collectionResponse = commentWebService.Post(new CommentsFilterExtended()
      {
        Language = stringList1,
        Status = stringList2,
        SortDescending = flag,
        Take = result1,
        Skip = result2,
        OlderThan = result4,
        NewerThan = result3,
        ThreadType = stringList3
      });
      IEnumerable<string> source = collectionResponse.Items.Select<CommentResponse, string>((Func<CommentResponse, string>) (c => c.ThreadKey)).Distinct<string>();
      CollectionResponse<ThreadResponse> threadsResponse = commentWebService.Post(new ThreadsFilterExtended()
      {
        ThreadKey = source.ToList<string>()
      });
      return new CollectionContext<CommentExtendedViewModel>(collectionResponse.Items.Select<CommentResponse, CommentExtendedViewModel>((Func<CommentResponse, CommentExtendedViewModel>) (c => this.CreateCommentExtendedFromResponse(c, threadsResponse.Items, new AuthorProxy(c.Name, c.Email)))))
      {
        TotalCount = collectionResponse.TotalCount
      };
    }

    /// <summary>Gets the comments extended by thread.</summary>
    /// <param name="thread">The thread.</param>
    /// <param name="status">The status.</param>
    /// <param name="sortDirection">The sort direction.</param>
    /// <param name="take">The take.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="newerThan">The date filter.</param>
    /// <param name="olderThan">The date filter.</param>
    /// <param name="olderThan">The thread type.</param>
    public CollectionContext<CommentExtendedViewModel> GetCommentsExtendedByThread(
      string thread,
      string language,
      string status,
      string sortDirection,
      string take,
      string skip,
      string newerThan,
      string olderThan,
      string threadType)
    {
      CommentWebService commentWebService = new CommentWebService();
      List<string> stringList1 = new List<string>();
      if (!language.IsNullOrWhitespace())
        stringList1.Add(language);
      List<string> stringList2 = new List<string>();
      if (!status.IsNullOrWhitespace())
        stringList2.Add(status);
      List<string> stringList3 = new List<string>();
      if (!threadType.IsNullOrWhitespace())
        stringList3.Add(threadType);
      bool flag = string.Equals("desc", sortDirection, StringComparison.OrdinalIgnoreCase);
      int result1;
      int.TryParse(take, out result1);
      int result2;
      int.TryParse(skip, out result2);
      DateTime result3;
      DateTime.TryParse(newerThan, out result3);
      DateTime result4;
      DateTime.TryParse(olderThan, out result4);
      CollectionResponse<CommentResponse> collectionResponse = commentWebService.Post(new CommentsFilterExtended()
      {
        ThreadKey = new List<string>() { thread },
        Language = stringList1,
        Status = stringList2,
        SortDescending = flag,
        Take = result1,
        Skip = result2,
        OlderThan = result4,
        NewerThan = result3,
        ThreadType = stringList3
      });
      CollectionResponse<ThreadResponse> threadsResponse = commentWebService.Post(new ThreadsFilterExtended()
      {
        ThreadKey = {
          thread
        }
      });
      return new CollectionContext<CommentExtendedViewModel>(collectionResponse.Items.Select<CommentResponse, CommentExtendedViewModel>((Func<CommentResponse, CommentExtendedViewModel>) (c => this.CreateCommentExtendedFromResponse(c, threadsResponse.Items, new AuthorProxy(c.Name, c.Email)))))
      {
        TotalCount = collectionResponse.TotalCount
      };
    }

    /// <summary>Publish comment.</summary>
    /// <param name="commentKey">The comment key.</param>
    public bool Publish(string commentKey)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      CommentWebService commentWebService = new CommentWebService();
      IComment comment = SystemManager.GetCommentsService().GetComment(commentKey);
      commentWebService.Put(new CommentUpdateRequest()
      {
        Key = commentKey,
        Name = comment.Author.Name,
        Email = comment.Author.Email,
        Message = comment.Message,
        Status = "Published"
      });
      ServiceUtility.DisableCache();
      return true;
    }

    /// <summary>Hide comment.</summary>
    /// <param name="commentKey">The comment key.</param>
    public bool Hide(string commentKey)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      CommentWebService commentWebService = new CommentWebService();
      IComment comment = SystemManager.GetCommentsService().GetComment(commentKey);
      commentWebService.Put(new CommentUpdateRequest()
      {
        Key = commentKey,
        Name = comment.Author.Name,
        Email = comment.Author.Email,
        Message = comment.Message,
        Status = "Hidden"
      });
      ServiceUtility.DisableCache();
      return true;
    }

    /// <summary>Marks the asynchronous spam.</summary>
    /// <param name="commentKey">The comment key.</param>
    public bool MarkAsSpam(string commentKey)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      CommentWebService commentWebService = new CommentWebService();
      IComment comment = SystemManager.GetCommentsService().GetComment(commentKey);
      commentWebService.Put(new CommentUpdateRequest()
      {
        Key = commentKey,
        Name = comment.Author.Name,
        Email = comment.Author.Email,
        Message = comment.Message,
        Status = "Spam"
      });
      ServiceUtility.DisableCache();
      return true;
    }

    /// <summary>Deletes the comment.</summary>
    /// <param name="commentKey">The comment key.</param>
    public bool DeleteComment(string commentKey)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      new CommentWebService().Delete(new CommentDeleteRequest()
      {
        Key = commentKey
      });
      ServiceUtility.DisableCache();
      return true;
    }

    /// <summary>Close thread for new comments.</summary>
    /// <param name="threadKey">The thread key.</param>
    public bool CloseThread(ThreadIdentificationViewModel threadData)
    {
      new CommentWebService().Put(new ThreadUpdateRequest()
      {
        Key = threadData.Key,
        IsClosed = true
      });
      return true;
    }

    /// <summary>Open thread for new comments.</summary>
    /// <param name="threadKey">The thread key.</param>
    public bool OpenThread(ThreadIdentificationViewModel threadData)
    {
      new CommentWebService().Put(new ThreadUpdateRequest()
      {
        Key = threadData.Key,
        IsClosed = false
      });
      return true;
    }

    /// <summary>Batch comments publish by list of comment keys.</summary>
    /// <param name="commentKey">The comment key.</param>
    public bool BatchCommentsPublish(string[] commentKeys)
    {
      new CommentWebService().Put(new CommentsBatchUpdateRequest()
      {
        Key = ((IEnumerable<string>) commentKeys).ToList<string>(),
        Status = "Published"
      });
      return true;
    }

    /// <summary>Batch comments delete by list of comment keys</summary>
    /// <param name="commentKey">The comment key.</param>
    public bool BatchCommentsDelete(string[] commentKeys)
    {
      new CommentWebService().Delete(new CommentsBatchDeleteRequest()
      {
        Key = ((IEnumerable<string>) commentKeys).ToList<string>()
      });
      return true;
    }

    /// <summary>Batch comments hide by list of comment keys</summary>
    /// <param name="commentKey">The comment key.</param>
    public bool BatchCommentsHide(string[] commentKeys)
    {
      new CommentWebService().Put(new CommentsBatchUpdateRequest()
      {
        Key = ((IEnumerable<string>) commentKeys).ToList<string>(),
        Status = "Hidden"
      });
      return true;
    }

    /// <summary>Batch comments hide by list of comment keys</summary>
    /// <param name="commentKey">The comment key.</param>
    public bool BatchCommentsSpam(string[] commentKeys)
    {
      new CommentWebService().Put(new CommentsBatchUpdateRequest()
      {
        Key = ((IEnumerable<string>) commentKeys).ToList<string>(),
        Status = "Spam"
      });
      return true;
    }

    /// <summary>Gets the comments by thread.</summary>
    /// <param name="thread">The thread.</param>
    /// <param name="language"></param>
    /// <param name="olderThan">The older than.</param>
    /// <param name="newerThan">The newer than.</param>
    /// <param name="sortDirection">The sort direction.</param>
    /// <param name="take">The take.</param>
    /// <param name="skip">The skip.</param>
    /// <returns></returns>
    public CollectionContext<CommentViewModel> GetCommentsByThread(
      string thread,
      string language,
      string olderThan,
      string newerThan,
      string sortDirection,
      string take,
      string skip)
    {
      CommentWebService commentWebService = new CommentWebService();
      List<string> stringList = new List<string>();
      if (!language.IsNullOrWhitespace())
        stringList.Add(language);
      new List<string>() { "Published" };
      bool flag = string.Equals("desc", sortDirection, StringComparison.OrdinalIgnoreCase);
      int result1;
      int.TryParse(take, out result1);
      int result2;
      int.TryParse(skip, out result2);
      DateTime result3;
      DateTime.TryParse(newerThan, out result3);
      DateTime result4;
      DateTime.TryParse(olderThan, out result4);
      CollectionResponse<CommentResponse> collectionResponse = commentWebService.Get(new CommentsFilter()
      {
        ThreadKey = thread,
        SortDescending = flag,
        Take = result1,
        Skip = result2,
        OlderThan = result4,
        NewerThan = result3
      });
      return new CollectionContext<CommentViewModel>(collectionResponse.Items.Select<CommentResponse, CommentViewModel>((Func<CommentResponse, CommentViewModel>) (c => this.CreateCommentFromResponse(c, new AuthorProxy(c.Name, c.Email)))))
      {
        TotalCount = collectionResponse.TotalCount
      };
    }

    /// <summary>Gets the comments count by thread.</summary>
    /// <param name="threadKey">The thread key.</param>
    /// <param name="status">The status.</param>
    /// <returns></returns>
    public int GetCommentsCountByThread(string threadKey, string status)
    {
      List<string> stringList = new List<string>();
      if (!string.IsNullOrEmpty(status))
        stringList.Add(status);
      return new CommentWebService().Post(new CommentsFilterExtended()
      {
        ThreadKey = new List<string>() { threadKey },
        Status = stringList,
        Take = 1
      }).TotalCount;
    }

    /// <summary>Gets the multiple threads.</summary>
    /// <param name="threadKeys">The thread keys.</param>
    /// <returns></returns>
    public string GetMultipleCommentCounts(string[] threadKeys)
    {
      IEnumerable<ThreadResponse> items = new CommentWebService().Post(new ThreadsFilterExtended()
      {
        ThreadKey = ((IEnumerable<string>) threadKeys).ToList<string>()
      }).Items;
      List<CommentsReducedViewModel> reducedViewModelList = new List<CommentsReducedViewModel>();
      foreach (ThreadResponse threadResponse in items)
        reducedViewModelList.Add(new CommentsReducedViewModel()
        {
          ThreadKey = threadResponse.Key,
          CommentsCount = threadResponse.CommentsCount,
          IsClosed = threadResponse.IsClosed
        });
      return new JavaScriptSerializer().Serialize((object) reducedViewModelList);
    }

    /// <summary>Gets the comments count by group.</summary>
    /// <param name="language">The language.</param>
    /// <param name="status">The status.</param>
    /// <returns></returns>
    public int GetCommentsCountByGroups(string language, string status)
    {
      List<string> stringList1 = new List<string>();
      if (!string.IsNullOrEmpty(language))
        stringList1.Add(language);
      List<string> stringList2 = new List<string>();
      if (!string.IsNullOrEmpty(status))
        stringList2.Add(status);
      return new CommentWebService().Post(new CommentsFilterExtended()
      {
        Language = stringList1,
        Status = stringList2,
        Take = 1
      }).TotalCount;
    }

    /// <summary>Submits the comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <returns></returns>
    /// <exception cref="T:System.InvalidOperationException"></exception>
    public CommentViewModel SubmitComment(CommentSubmitData comment)
    {
      CommentResponse commentViaWebService = CommentsUtilities.CreateCommentViaWebService(comment.Email, comment.Name, comment.Message, comment.Thread);
      return new CommentViewModel((IComment) new CommentProxy()
      {
        Author = (IAuthor) new AuthorProxy(commentViaWebService.Name, commentViaWebService.Email),
        AuthorIpAddress = commentViaWebService.AuthorIpAddress,
        DateCreated = commentViaWebService.DateCreated,
        Key = commentViaWebService.Key,
        Message = commentViaWebService.Message,
        Status = commentViaWebService.Status,
        ThreadKey = commentViaWebService.ThreadKey
      });
    }

    /// <summary>Submits the comment and create thread and/or group.</summary>
    /// <param name="comment">The comment.</param>
    /// <returns></returns>
    /// <exception cref="T:System.InvalidOperationException"></exception>
    public CommentViewModel SubmitCommentExtended(CommentSubmitDataExtended comment)
    {
      CommentResponse commentViaWebService = CommentsUtilities.CreateCommentViaWebService(comment.Email, comment.Name, comment.Message, comment.Thread, comment.ThreadType, comment.ThreadTitle, comment.DataSource, comment.Language, comment.GroupKey);
      return new CommentViewModel((IComment) new CommentProxy()
      {
        Author = (IAuthor) new AuthorProxy(commentViaWebService.Name, commentViaWebService.Email),
        AuthorIpAddress = commentViaWebService.AuthorIpAddress,
        DateCreated = commentViaWebService.DateCreated,
        Key = commentViaWebService.Key,
        Message = commentViaWebService.Message,
        Status = commentViaWebService.Status,
        ThreadKey = commentViaWebService.ThreadKey
      });
    }

    /// <summary>Submits the comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <param name="answer">The correctAnswer hash.</param>
    /// <param name="correctAnswer">The comment.</param>
    /// <param name="initializationVector">The initialization vector.</param>
    /// <param name="key">The salt.</param>
    /// <returns></returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// </exception>
    public CommentViewModel SubmitCaptchaComment(
      CommentSubmitData comment,
      string answer,
      string correctAnswer,
      string initializationVector,
      string key)
    {
      CommentResponse commentViaWebService = CommentsUtilities.CreateCommentViaWebService(comment.Email, comment.Name, comment.Message, comment.Thread, answer: answer, correctAnswer: correctAnswer, initializationVector: initializationVector, captchaKey: key);
      return new CommentViewModel((IComment) new CommentProxy()
      {
        Author = (IAuthor) new AuthorProxy(commentViaWebService.Name, commentViaWebService.Email),
        AuthorIpAddress = commentViaWebService.AuthorIpAddress,
        DateCreated = commentViaWebService.DateCreated,
        Key = commentViaWebService.Key,
        Message = commentViaWebService.Message,
        Status = commentViaWebService.Status,
        ThreadKey = commentViaWebService.ThreadKey
      });
    }

    /// <summary>Submits the comment and create thread and/or group.</summary>
    /// <param name="comment">The comment.</param>
    /// <param name="answer">The correctAnswer hash.</param>
    /// <param name="correctAnswer">The comment.</param>
    /// <param name="initializationVector">The initialization vector.</param>
    /// <param name="key">The salt.</param>
    /// <returns></returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// </exception>
    public CommentViewModel SubmitCaptchaCommentExtended(
      CommentSubmitDataExtended comment,
      string answer,
      string correctAnswer,
      string initializationVector,
      string key)
    {
      CommentResponse commentViaWebService = CommentsUtilities.CreateCommentViaWebService(comment.Email, comment.Name, comment.Message, comment.Thread, comment.ThreadType, comment.ThreadTitle, comment.DataSource, comment.Language, comment.GroupKey, answer, correctAnswer, initializationVector, key);
      return new CommentViewModel((IComment) new CommentProxy()
      {
        Author = (IAuthor) new AuthorProxy(commentViaWebService.Name, commentViaWebService.Email),
        AuthorIpAddress = commentViaWebService.AuthorIpAddress,
        DateCreated = commentViaWebService.DateCreated,
        Key = commentViaWebService.Key,
        Message = commentViaWebService.Message,
        Status = commentViaWebService.Status,
        ThreadKey = commentViaWebService.ThreadKey
      });
    }

    /// <summary>Gets the captcha.</summary>
    /// <returns>Captcha image and a corresponding key.</returns>
    public CaptchaResponse GetCaptcha()
    {
      Telerik.Sitefinity.Services.Comments.DTO.CaptchaResponse captchaResponse = new CommentWebService().Get(new CaptchaRequest());
      return new CaptchaResponse()
      {
        Key = captchaResponse.Key,
        Image = captchaResponse.Image,
        CorrectAnswer = captchaResponse.CorrectAnswer,
        InitializationVector = captchaResponse.InitializationVector
      };
    }

    /// <summary>Gets the comments security settings.</summary>
    /// <param name="threadType">The thread type.</param>
    public CommentsSecurityViewModel GetSecuritySettings(string threadType)
    {
      StatusResponse statusResponse = new UserSessionService().Get(new StatusRequest());
      return new CommentsSecurityViewModel()
      {
        IsAuthenticated = statusResponse.IsAuthenticated,
        RequireCaptcha = Config.Get<CommentsModuleConfig>().UseSpamProtectionImage
      };
    }

    private CommentExtendedViewModel CreateCommentExtendedFromResponse(
      CommentResponse cmntResponse,
      IEnumerable<ThreadResponse> threadsCache,
      AuthorProxy author)
    {
      ThreadResponse threadResponse = threadsCache.Single<ThreadResponse>((Func<ThreadResponse, bool>) (t => t.Key == cmntResponse.ThreadKey));
      return new CommentExtendedViewModel((IComment) new CommentProxy()
      {
        Author = (IAuthor) author,
        AuthorIpAddress = cmntResponse.AuthorIpAddress,
        DateCreated = cmntResponse.DateCreated,
        Key = cmntResponse.Key,
        Message = cmntResponse.Message,
        Status = cmntResponse.Status,
        ThreadKey = cmntResponse.ThreadKey
      }, (IThread) new ThreadProxy(threadResponse.Title, threadResponse.Type, (string) null, (IAuthor) author, SystemManager.CurrentContext.Culture))
      {
        CommentsCountByThread = threadResponse.CommentsCount
      };
    }

    private CommentViewModel CreateCommentFromResponse(
      CommentResponse cmntResponse,
      AuthorProxy authorProxy)
    {
      return new CommentViewModel((IComment) new CommentProxy()
      {
        Author = (IAuthor) authorProxy,
        AuthorIpAddress = cmntResponse.AuthorIpAddress,
        DateCreated = cmntResponse.DateCreated,
        Key = cmntResponse.Key,
        Message = cmntResponse.Message,
        Status = cmntResponse.Status,
        ThreadKey = cmntResponse.ThreadKey
      });
    }
  }
}
