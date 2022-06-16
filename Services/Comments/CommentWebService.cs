// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.CommentWebService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services.Captcha;
using Telerik.Sitefinity.Services.Comments.DTO;
using Telerik.Sitefinity.Services.Comments.Notifications;
using Telerik.Sitefinity.Services.Comments.Proxies;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Services.Comments
{
  /// <summary>
  /// This class represents the rest service used from the Comments feature in Sitefinity.
  /// </summary>
  public class CommentWebService : Service
  {
    private const string EmailRegexPattern = "^([a-zA-Z0-9_.+-])+\\@(([a-zA-Z0-9-])+\\.)+([a-zA-Z0-9]{2,4})+$";
    internal const string WebServiceUrl = "RestApi/comments-api";
    internal const int MaxCount = 500;

    /// <summary>
    /// Gets list of published comments filtered based on the provided <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.CommentsFilter" />
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    /// <exception cref="T:System.ArgumentException"></exception>
    public virtual CollectionResponse<CommentResponse> Get(
      CommentsFilter request)
    {
      this.Validate(request);
      List<string> threadKeys = new List<string>()
      {
        request.ThreadKey
      };
      List<string> statuses = new List<string>()
      {
        "Published"
      };
      int totalCount;
      IEnumerable<CommentResponse> commentResponses = CommentsUtilities.GetCommentResponses(CommentWebService.GetComments(out totalCount, threadKeys: ((IEnumerable<string>) threadKeys), statuses: ((IEnumerable<string>) statuses), sortDesc: request.SortDescending, take: request.Take, skip: request.Skip, newerThan: new DateTime?(request.NewerThan), olderThan: new DateTime?(request.OlderThan)), ClaimsManager.GetCurrentIdentity().IsBackendUser);
      CollectionResponse<CommentResponse> collectionResponse = new CollectionResponse<CommentResponse>();
      collectionResponse.Items = commentResponses;
      collectionResponse.TotalCount = totalCount;
      ServiceUtility.DisableCache();
      return collectionResponse;
    }

    /// <summary>
    /// Gets the count of <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects filtered by the specified <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.CommentsCountGetRequest" />.
    /// </summary>
    /// <param name="request">The threads filter.</param>
    /// <returns></returns>
    public virtual CollectionResponse<CommentsCountResponse> Get(
      CommentsCountGetRequest request)
    {
      this.Validate(request);
      int num = request.Status.Count == 0 ? 1 : (request.Status.Count != 1 ? 0 : (request.Status.Contains("Published") ? 1 : 0));
      ICommentService commentsService = SystemManager.GetCommentsService();
      List<CommentsCountResponse> source = new List<CommentsCountResponse>();
      if (num != 0)
      {
        ThreadFilter filter = new ThreadFilter();
        filter.ThreadKey.AddRange((IEnumerable<string>) request.ThreadKey);
        IEnumerable<IThread> threads = commentsService.GetThreads(filter);
        foreach (string str in request.ThreadKey)
        {
          string threadKey = str;
          IThread thread = threads.SingleOrDefault<IThread>((Func<IThread, bool>) (c => c.Key == threadKey));
          if (thread == null)
            source.Add(new CommentsCountResponse()
            {
              Key = threadKey,
              Count = 0
            });
          else if (thread.CommentsCount == 0 && thread.IsClosed)
            source.Add(new CommentsCountResponse()
            {
              Key = threadKey,
              Count = -1
            });
          else
            source.Add(new CommentsCountResponse()
            {
              Key = threadKey,
              Count = thread.CommentsCount
            });
        }
      }
      else
      {
        CommentFilter commentFilter = new CommentFilter();
        commentFilter.Take = new int?(1);
        CommentFilter filter1 = commentFilter;
        filter1.Status.AddRange((IEnumerable<string>) request.Status);
        List<string> collection = new List<string>();
        foreach (string str in request.ThreadKey)
        {
          int totalCount = 0;
          filter1.ThreadKey.Clear();
          filter1.ThreadKey.Add(str);
          commentsService.GetComments(filter1, out totalCount);
          source.Add(new CommentsCountResponse()
          {
            Key = str,
            Count = totalCount
          });
          if (totalCount == 0)
            collection.Add(str);
        }
        if (collection.Count > 0)
        {
          ThreadFilter filter2 = new ThreadFilter();
          filter2.ThreadKey.AddRange((IEnumerable<string>) collection);
          IEnumerable<string> closedThreads = commentsService.GetThreads(filter2).Where<IThread>((Func<IThread, bool>) (t => t.IsClosed)).Select<IThread, string>((Func<IThread, string>) (t => t.Key));
          foreach (CommentsCountResponse commentsCountResponse in source.Where<CommentsCountResponse>((Func<CommentsCountResponse, bool>) (r => closedThreads.Contains<string>(r.Key))))
            commentsCountResponse.Count = -1;
        }
      }
      CollectionResponse<CommentsCountResponse> collectionResponse = new CollectionResponse<CommentsCountResponse>();
      collectionResponse.Items = (IEnumerable<CommentsCountResponse>) source;
      collectionResponse.TotalCount = source.Count;
      ServiceUtility.DisableCache();
      return collectionResponse;
    }

    /// <summary>
    /// Gets the reviews statistics of <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects filtered by the specified <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.ReviewsStatisticsGetRequest" />.
    /// </summary>
    /// <param name="request">The threads filter.</param>
    /// <returns></returns>
    public virtual object Get(ReviewsStatisticsGetRequest request)
    {
      this.Validate(request);
      ThreadFilter filter = new ThreadFilter();
      filter.ThreadKey.AddRange((IEnumerable<string>) request.ThreadKey);
      IEnumerable<IThread> threads = SystemManager.GetCommentsService().GetThreads(filter);
      List<ReviewsStatisticsResponse> statisticsResponseList = new List<ReviewsStatisticsResponse>();
      foreach (string str in request.ThreadKey)
      {
        string key = str;
        IThread thread = threads.SingleOrDefault<IThread>((Func<IThread, bool>) (c => c.Key == key));
        int num;
        Decimal? nullable;
        if (thread != null)
        {
          num = thread.CommentsCount <= 0 ? (!thread.IsClosed ? 0 : -1) : thread.CommentsCount;
          nullable = thread.AverageRating;
        }
        else
        {
          num = 0;
          nullable = new Decimal?();
        }
        statisticsResponseList.Add(new ReviewsStatisticsResponse()
        {
          Key = key,
          Count = num,
          AverageRating = nullable
        });
      }
      ServiceUtility.DisableCache();
      return (object) statisticsResponseList;
    }

    /// <summary>Gets single comment.</summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public virtual CommentResponse Get(CommentGetRequest request)
    {
      this.Validate(request);
      IComment comment = SystemManager.GetCommentsService().GetComment(request.Key);
      if (comment == null)
        return (CommentResponse) null;
      if (!ClaimsManager.GetCurrentIdentity().IsBackendUser && comment.Status != "Published")
        throw new InvalidOperationException("This route handle only published Comments.");
      CommentResponse commentResponse = CommentsUtilities.GetCommentResponse(comment, ClaimsManager.GetCurrentIdentity().IsBackendUser);
      ServiceUtility.DisableCache();
      return commentResponse;
    }

    /// <summary>
    /// Gets list of comments filtered based on the provided <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.CommentsFilterExtended" />
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    /// <remarks>Requires backend authentication.</remarks>
    public virtual CollectionResponse<CommentResponse> Post(
      CommentsFilterExtended request)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      List<string> groupKeys = request.GroupKey;
      if (request.GroupKey == null || request.GroupKey.Count == 0)
        groupKeys = CommentsUtilities.GetGroupKeys();
      int totalCount;
      IEnumerable<CommentResponse> commentResponses = CommentsUtilities.GetCommentResponses(CommentWebService.GetComments(out totalCount, (IEnumerable<string>) groupKeys, (IEnumerable<string>) request.ThreadKey, (IEnumerable<string>) request.Language, (IEnumerable<string>) request.Status, (IEnumerable<string>) request.Behavior, request.SortDescending, request.Take, request.Skip, new DateTime?(request.NewerThan), new DateTime?(request.OlderThan), (IEnumerable<string>) request.ThreadType), ClaimsManager.GetCurrentIdentity().IsBackendUser);
      CollectionResponse<CommentResponse> collectionResponse = new CollectionResponse<CommentResponse>();
      collectionResponse.Items = commentResponses;
      collectionResponse.TotalCount = totalCount;
      ServiceUtility.DisableCache();
      return collectionResponse;
    }

    /// <summary>
    /// Create comments depending on the provided <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.CommentCreateRequest" />
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    /// <exception cref="T:System.InvalidOperationException"></exception>
    public virtual CommentResponse Post(CommentCreateRequest request) => this.CreateComment(request, false);

    internal CommentResponse CreateComment(
      CommentCreateRequest request,
      bool skipCaptcha)
    {
      this.Validate(request);
      CommentResponse comment;
      try
      {
        IAuthor author = CommentsUtilities.GetAuthor(request);
        ICommentService commentsService = SystemManager.GetCommentsService();
        IThread thread = commentsService.GetThread(request.ThreadKey);
        if (thread == null)
        {
          request.Thread.Key = request.ThreadKey;
          this.Validate(request.Thread);
          this.ValidatePostRequest(request.Thread.Type, request.Captcha, skipCaptcha);
          IGroup group = commentsService.GetGroup(request.Thread.GroupKey);
          if (group == null)
          {
            this.Validate(request.Thread.Group);
            request.Thread.Group.Key = request.Thread.GroupKey;
            GroupProxy groupProxy = new GroupProxy(request.Thread.Group.Name, request.Thread.Group.Description, author)
            {
              Key = request.Thread.Group.Key
            };
            group = commentsService.CreateGroup((IGroup) groupProxy);
          }
          ThreadProxy threadProxy = new ThreadProxy(request.Thread.Title, request.Thread.Type, group.Key, author, SystemManager.CurrentContext.Culture)
          {
            Key = request.Thread.Key,
            Language = request.Thread.Language,
            DataSource = request.Thread.DataSource,
            Behavior = request.Thread.Behavior
          };
          thread = commentsService.CreateThread((IThread) threadProxy);
        }
        else
        {
          if (thread.IsClosed)
            throw new InvalidOperationException("Thread is closed.");
          this.ValidatePostRequest(thread.Type, request.Captcha, skipCaptcha);
        }
        comment = this.SubmitCommentInternal(request, thread, author);
      }
      catch (InvalidOperationException ex)
      {
        throw new InvalidOperationException(Res.Get<CommentsResources>().CannotSubmitCommentMessage, (Exception) ex);
      }
      ServiceUtility.DisableCache();
      return comment;
    }

    /// <summary>Updates the specified comment.</summary>
    /// <remarks>Requires backend authentication.</remarks>
    /// <param name="comment">The comment.</param>
    public virtual CommentResponse Put(CommentUpdateRequest comment)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.Validate(comment);
      ICommentService commentsService = SystemManager.GetCommentsService();
      IComment comment1 = commentsService.GetComment(comment.Key);
      if (comment1 == null)
        throw new ItemNotFoundException("Comment not found: " + comment.Key);
      if (!string.IsNullOrWhiteSpace(comment.Message))
        comment1.Message = comment.Message;
      if (!string.IsNullOrWhiteSpace(comment.Status))
        comment1.Status = comment.Status;
      if (!string.IsNullOrWhiteSpace(comment.Email))
        comment1.Author.Email = comment.Email;
      if (!string.IsNullOrWhiteSpace(comment.Name))
        comment1.Author.Name = comment.Name;
      if (comment.Rating.HasValue)
        comment1.Rating = comment.Rating;
      if (!string.IsNullOrWhiteSpace(comment.CustomData))
        comment1.CustomData = comment.CustomData;
      comment1.LastModifiedBy = (IAuthor) null;
      CommentResponse commentResponse = CommentsUtilities.GetCommentResponse(commentsService.UpdateComment(comment1), true);
      ServiceUtility.DisableCache();
      return commentResponse;
    }

    /// <summary>
    /// Performs updates over the specified group of comments.
    /// </summary>
    /// <remarks>Requires backend authentication.</remarks>
    /// <param name="comment">The request.</param>
    public virtual CollectionResponse<CommentResponse> Put(
      CommentsBatchUpdateRequest request)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.Validate(request);
      ICommentService commentsService = SystemManager.GetCommentsService();
      CommentFilter filter = new CommentFilter();
      filter.CommentKey.AddRange((IEnumerable<string>) request.Key);
      List<IComment> list = commentsService.GetComments(filter).ToList<IComment>();
      if (request.Key.Count != list.Count)
        throw new ItemNotFoundException("Comment not found: " + request.Key.Except<string>(list.Select<IComment, string>((Func<IComment, string>) (c => c.Key))).First<string>());
      for (int index = 0; index < list.Count; ++index)
      {
        list[index].Status = request.Status;
        list[index].LastModifiedBy = (IAuthor) null;
      }
      IEnumerable<IComment> comments = commentsService.UpdateComments((IEnumerable<IComment>) list);
      IEnumerable<CommentResponse> commentResponses = CommentsUtilities.GetCommentResponses(comments, true);
      CollectionResponse<CommentResponse> collectionResponse = new CollectionResponse<CommentResponse>();
      collectionResponse.Items = commentResponses;
      collectionResponse.TotalCount = comments.Count<IComment>();
      ServiceUtility.DisableCache();
      return collectionResponse;
    }

    /// <summary>Deletes a comment with the specified key.</summary>
    /// <remarks>Requires backend authentication.</remarks>
    /// <param name="request">The request.</param>
    public virtual void Delete(CommentDeleteRequest request)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.Validate(request);
      SystemManager.GetCommentsService().DeleteComment(request.Key);
      ServiceUtility.DisableCache();
    }

    /// <summary>Deletes group of comments with the specified keys</summary>
    /// <remarks>Requires backend authentication.</remarks>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    /// <exception cref="T:System.ArgumentException">There are invalid comment keys in the collection.</exception>
    public virtual void Delete(CommentsBatchDeleteRequest request)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.Validate(request);
      SystemManager.GetCommentsService().DeleteComments((IEnumerable<string>) request.Key);
      ServiceUtility.DisableCache();
    }

    /// <summary>
    /// Gets collection of threads filtered by the specified <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.ThreadsFilterExtended" />.
    /// </summary>
    /// <remarks>Requires backend authentication.</remarks>
    public virtual CollectionResponse<ThreadResponse> Post(
      ThreadsFilterExtended request)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      int totalCount;
      IEnumerable<IThread> filteredThreads = CommentWebService.GetFilteredThreads((IEnumerable<string>) request.ThreadKey, out totalCount);
      CollectionResponse<ThreadResponse> collectionResponse = new CollectionResponse<ThreadResponse>();
      collectionResponse.TotalCount = totalCount;
      collectionResponse.Items = CommentsUtilities.GetThreadsResponses(filteredThreads);
      ServiceUtility.DisableCache();
      return collectionResponse;
    }

    /// <summary>Updates the specified thread.</summary>
    /// <remarks>Requires backend authentication.</remarks>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public virtual ThreadResponse Put(ThreadUpdateRequest request)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.Validate(request);
      ICommentService commentsService = SystemManager.GetCommentsService();
      IThread thread = commentsService.GetThread(request.Key);
      if (thread == null)
        throw new ItemNotFoundException("Thread not found: " + request.Key);
      thread.IsClosed = request.IsClosed;
      ThreadResponse threadResponse = CommentsUtilities.GetThreadResponse(commentsService.UpdateThread(thread));
      ServiceUtility.DisableCache();
      return threadResponse;
    }

    /// <summary>
    /// Gets whether the user is subscribed to receive notifications when new comment is posted.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    /// <exception cref="T:System.ArgumentException">The parameters are not valid for this request.</exception>
    public virtual NotificationStatusResponse Get(
      NotificationStatusRequest request)
    {
      ServiceUtility.RequestAuthentication();
      this.Validate(request);
      SubscriptionData subsctiptionData = CommentWebService.InitilizeCommentSubscriptionData(request.ThreadKey);
      ICommentService commentsService = SystemManager.GetCommentsService();
      NotificationStatusResponse notificationStatusResponse = new NotificationStatusResponse();
      notificationStatusResponse.IsSubscribed = commentsService.IsSubscribed(subsctiptionData);
      ServiceUtility.DisableCache();
      return notificationStatusResponse;
    }

    /// <summary>
    /// Subscribes the user to receive notification when new comment is posted to the specified thread
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    /// <exception cref="T:System.ArgumentException">The parameters are not valid for this request.</exception>
    public virtual void Post(NotificationSubscribeRequest request)
    {
      ServiceUtility.RequestAuthentication();
      this.Validate(request);
      SubscriptionData subsctiptionData = CommentWebService.InitilizeCommentSubscriptionData(request.ThreadKey);
      SystemManager.GetCommentsService().Subscribe(subsctiptionData);
      ServiceUtility.DisableCache();
    }

    /// <summary>
    /// Unsubscribes the user from receiving notification when new comment is posted to the specified thread
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    /// <exception cref="T:System.ArgumentException">The parameters are not valid for this request.</exception>
    public virtual void Post(NotificationUnsubscribeRequest request)
    {
      ServiceUtility.RequestAuthentication();
      this.Validate(request);
      SubscriptionData subsctiptionData = CommentWebService.InitilizeCommentSubscriptionData(request.ThreadKey);
      SystemManager.GetCommentsService().Unsubscribe(subsctiptionData);
      ServiceUtility.DisableCache();
    }

    /// <summary>Gets the captcha image and details.</summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    [Obsolete("Use Telerik.Sitefinity.Services.Captcha.CaptchaWebService instead")]
    public Telerik.Sitefinity.Services.Comments.DTO.CaptchaResponse Get(
      Telerik.Sitefinity.Services.Comments.DTO.CaptchaRequest request)
    {
      Telerik.Sitefinity.Services.Captcha.DTO.CaptchaResponse captchaResponse = new CaptchaWebService().Get(new Telerik.Sitefinity.Services.Captcha.DTO.CaptchaRequest());
      return new Telerik.Sitefinity.Services.Comments.DTO.CaptchaResponse()
      {
        Audio = captchaResponse.Audio,
        CorrectAnswer = captchaResponse.CorrectAnswer,
        Image = captchaResponse.Image,
        InitializationVector = captchaResponse.InitializationVector,
        Key = captchaResponse.Key
      };
    }

    /// <summary>Validates captcha image</summary>
    /// <param name="captchaInfo">The captcha info and answer</param>
    /// <returns>Weather answer is valid or not</returns>
    [Obsolete("Use Telerik.Sitefinity.Services.Captcha.CaptchaWebService instead")]
    public Telerik.Sitefinity.Services.Comments.DTO.CaptchaValidationResponse Post(
      Telerik.Sitefinity.Services.Comments.DTO.CaptchaInfo captchaInfo)
    {
      Telerik.Sitefinity.Services.Captcha.DTO.CaptchaValidationResponse validationResponse = new CaptchaWebService().Validate(captchaInfo.Key, captchaInfo.Answer);
      return new Telerik.Sitefinity.Services.Comments.DTO.CaptchaValidationResponse()
      {
        IsValid = validationResponse.IsValid,
        RefreshCaptcha = validationResponse.RefreshCaptcha
      };
    }

    private void Validate(CommentGetRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("CommentGetRequest");
      if (string.IsNullOrWhiteSpace(request.Key))
        throw new ArgumentException("Comment Key is not valid.");
    }

    private void Validate(CommentsCountGetRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("CommentsCountGetRequest");
      if (request.ThreadKey.Count == 0)
        throw new ArgumentException("ThreadKeys should contains at least one element.");
      if (request.Status == null)
        return;
      foreach (string statu in request.Status)
      {
        if (!CommentsUtilities.IsValidStatus(statu))
          throw new ArgumentException("Invalid Status.");
      }
      if (!ClaimsManager.GetCurrentIdentity().IsBackendUser && request.Status.Any<string>((Func<string, bool>) (s => s != "Published")))
        throw new InvalidOperationException("Non authenticated user can request only published comments count.");
    }

    private void Validate(ReviewsStatisticsGetRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("ReviewsStatisticsGetRequest");
      if (request.ThreadKey.Count == 0)
        throw new ArgumentException("ThreadKeys should contains at least one element.");
    }

    private void Validate(CommentsFilter request)
    {
      if (request == null)
        throw new ArgumentNullException("CommentsFilter");
      if (string.IsNullOrWhiteSpace(request.ThreadKey))
        throw new ArgumentException("ThreadKey");
    }

    private void Validate(CommentCreateRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("CommentCreateRequest");
      if (string.IsNullOrWhiteSpace(request.Message))
        throw new ArgumentException("Invalid Message.");
      if (string.IsNullOrWhiteSpace(request.ThreadKey))
        throw new ArgumentException("Invalid ThreadKey.");
      if (!string.IsNullOrEmpty(request.Email) && !Regex.IsMatch(request.Email, "^([a-zA-Z0-9_.+-])+\\@(([a-zA-Z0-9-])+\\.)+([a-zA-Z0-9]{2,4})+$"))
        throw new ArgumentException("Invalid email.");
      if (ClaimsManager.GetCurrentUserId() == Guid.Empty && string.IsNullOrWhiteSpace(request.Name))
        throw new ArgumentException("The Name (AuthorName) is required when anonymous user creates comment.");
    }

    private void Validate(CommentUpdateRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("CommentCreateRequest");
      if (string.IsNullOrWhiteSpace(request.Key))
        throw new ArgumentException("Invalid Key.");
      if (!string.IsNullOrWhiteSpace(request.Status) && !CommentsUtilities.IsValidStatus(request.Status))
        throw new ArgumentException("Invalid Status.");
    }

    private void Validate(CommentsBatchUpdateRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("CommentsBatchUpdateRequest");
      if (string.IsNullOrWhiteSpace(request.Status) || !CommentsUtilities.IsValidStatus(request.Status))
        throw new ArgumentException("Invalid Status.");
      if (request.Key.Count == 0)
        throw new ArgumentException("Keys should contains at least one element.");
    }

    private void Validate(CommentDeleteRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("CommentDeleteRequest");
      if (string.IsNullOrWhiteSpace(request.Key))
        throw new ArgumentException("Invalid Key.");
    }

    private void Validate(CommentsBatchDeleteRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("CommentsBatchDeleteRequest");
      if (request.Key.Count == 0)
        throw new ArgumentException("Keys should contains at least one element.");
    }

    private void Validate(ThreadCreateRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("ThreadCreateRequest");
      if (string.IsNullOrWhiteSpace(request.Key))
        throw new ArgumentException("Thread.Key parameter is required.");
      if (string.IsNullOrWhiteSpace(request.Type))
        throw new ArgumentException("Thread.Type parameter is required.");
      if (string.IsNullOrWhiteSpace(request.Title))
        throw new ArgumentException("Thread.Title parameter is required.");
      if (string.IsNullOrWhiteSpace(request.GroupKey))
        throw new ArgumentException("Thread.GroupKey parameter is required.");
      if (!this.ValidateThreadKey(request))
        throw new ArgumentException("Thread Key is not valid.");
    }

    private void Validate(ThreadUpdateRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("ThreadUpdateRequest");
      if (string.IsNullOrWhiteSpace(request.Key))
        throw new ArgumentException("Thread Key is not valid.");
    }

    private void Validate(GroupCreateRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("GroupCreateRequest");
      if (string.IsNullOrWhiteSpace(request.Key))
        throw new ArgumentException("Group.Key parameter is required.");
    }

    private void Validate(NotificationStatusRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("NotificationStatusRequest");
      if (string.IsNullOrWhiteSpace(request.ThreadKey))
        throw new ArgumentException("The ThreadKey is not valid.");
    }

    private void Validate(NotificationUnsubscribeRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("NotificationUnsubscribeRequest");
      if (string.IsNullOrWhiteSpace(request.ThreadKey))
        throw new ArgumentException("The ThreadKey is not valid.");
    }

    private void Validate(NotificationSubscribeRequest request)
    {
      if (request == null)
        throw new ArgumentNullException("NotificationSubscribeRequest");
      if (string.IsNullOrWhiteSpace(request.ThreadKey))
        throw new ArgumentException("The ThreadKey is not valid.");
    }

    private void ValidatePostRequest(string threadType, Telerik.Sitefinity.Services.Comments.DTO.CaptchaInfo info, bool skipCaptcha)
    {
      CommentsSettingsElement threadConfigByType = CommentsUtilities.GetThreadConfigByType(threadType);
      bool isAuthenticated = ClaimsManager.GetCurrentIdentity().IsAuthenticated;
      if (!threadConfigByType.AllowComments)
        throw new InvalidOperationException(Res.Get<CommentsResources>().CannotSubmitCommentMessage + "Thread does not allow the creation of new Comments!");
      if (threadConfigByType.RequiresAuthentication && !isAuthenticated)
        throw new InvalidOperationException(Res.Get<CommentsResources>().CannotSubmitCommentMessage + "Require Authentication!");
      bool flag1 = Config.Get<CommentsModuleConfig>().UseSpamProtectionImage && !skipCaptcha;
      CaptchaWebService captchaWebService = new CaptchaWebService();
      bool flag2 = info != null && captchaWebService.Validate(info.Key, info.Answer).IsValid;
      if (!isAuthenticated & flag1 && !flag2)
        throw new InvalidOperationException(Res.Get<CommentsResources>("CaptchavalidationError"));
    }

    private static IEnumerable<IComment> GetComments(
      out int totalCount,
      IEnumerable<string> groupKeys = null,
      IEnumerable<string> threadKeys = null,
      IEnumerable<string> languages = null,
      IEnumerable<string> statuses = null,
      IEnumerable<string> behaviors = null,
      bool sortDesc = false,
      int take = 0,
      int skip = 0,
      DateTime? newerThan = null,
      DateTime? olderThan = null,
      IEnumerable<string> threadTypes = null)
    {
      ICommentService commentsService = SystemManager.GetCommentsService();
      if (take == 0)
        take = ConfigManager.GetManager().GetSection<CommentsModuleConfig>().CommentsPerPage;
      if (take > 500)
        take = 500;
      CommentFilter commentFilter = new CommentFilter();
      if (groupKeys != null && groupKeys.Count<string>() > 0)
        commentFilter.GroupKey.AddRange(groupKeys);
      if (threadTypes != null && threadTypes.Count<string>() > 0)
        commentFilter.ThreadType.AddRange(threadTypes);
      if (threadKeys != null)
        commentFilter.ThreadKey.AddRange(threadKeys);
      if (languages != null && languages.Count<string>() > 0)
        commentFilter.Language.AddRange(languages);
      if (statuses != null && statuses.Count<string>() > 0)
        commentFilter.Status.AddRange(statuses);
      if (behaviors != null && behaviors.Count<string>() > 0)
        commentFilter.Behavior.AddRange(behaviors);
      if (newerThan.GetValueOrDefault() != new DateTime())
        commentFilter.FromDate = new DateTime?(newerThan.Value);
      if (olderThan.GetValueOrDefault() != new DateTime())
        commentFilter.ToDate = new DateTime?(olderThan.Value);
      if (sortDesc)
        commentFilter.SortDescending = true;
      if (take != 0)
        commentFilter.Take = new int?(take);
      if (skip != 0)
        commentFilter.Skip = new int?(skip);
      CommentFilter filter = commentFilter;
      ref int local = ref totalCount;
      return commentsService.GetComments(filter, out local);
    }

    private static IEnumerable<IThread> GetFilteredThreads(
      IEnumerable<string> threadKey,
      out int totalCount)
    {
      ICommentService commentsService = SystemManager.GetCommentsService();
      ThreadFilter threadFilter = new ThreadFilter();
      if (threadKey != null)
        threadFilter.ThreadKey.AddRange(threadKey);
      ThreadFilter filter = threadFilter;
      ref int local = ref totalCount;
      return commentsService.GetThreads(filter, out local);
    }

    private CommentResponse SubmitCommentInternal(
      CommentCreateRequest commentData,
      IThread thread,
      IAuthor author)
    {
      ICommentService commentsService = SystemManager.GetCommentsService();
      CommentsSettingsElement threadConfigByType = CommentsUtilities.GetThreadConfigByType(thread.Type);
      if (threadConfigByType.RequiresAuthentication && (author.Key.IsNullOrWhitespace() || author.Key == Guid.Empty.ToString()))
        throw new InvalidOperationException(Res.Get<CommentsResources>().CannotSubmitCommentMessage);
      Decimal? rating;
      if (threadConfigByType.EnableRatings)
      {
        rating = commentData.Rating;
        if (!rating.HasValue)
          throw new InvalidOperationException(Res.Get<CommentsResources>().RatingIsRequiredMessage);
      }
      rating = commentData.Rating;
      if (rating.HasValue && CommentsUtilities.GetCommentsByThreadForCurrentAuthorWithRating(thread.Key, commentsService).Count<IComment>() > 0)
        throw new InvalidOperationException(Res.Get<CommentsResources>().OnlyOneCommentWithRatingAllowedMessage);
      string fromCurrentRequest = CommentsUtilities.GetIpAddressFromCurrentRequest();
      CommentResponse commentResponse = CommentsUtilities.GetCommentResponse(commentsService.CreateComment((IComment) new CommentProxy(commentData.Message, thread.Key, author, fromCurrentRequest, commentData.Rating)
      {
        CustomData = commentData.CustomData,
        Status = (!threadConfigByType.RequiresApproval ? "Published" : "WaitingForApproval")
      }), ClaimsManager.GetCurrentIdentity().IsBackendUser);
      ServiceUtility.DisableCache();
      return commentResponse;
    }

    private bool ValidateThreadKey(ThreadCreateRequest request)
    {
      bool flag = false;
      Type itemType = TypeResolutionService.ResolveType(request.Type);
      if (!ConfigManager.GetManager().GetSection<CommentsModuleConfig>().DefaultSettings.EnableThreadCreationByConvension)
        return true;
      IManager manager;
      if (ManagerBase.TryGetMappedManager(itemType, request.DataSource, out manager))
      {
        string itemId;
        Guid result;
        string language;
        if (ControlUtilities.LocalizedKeyTryParse(request.Key, out itemId, out language, out string _) && manager != null && Guid.TryParse(itemId, out result))
        {
          object obj = manager.GetItem(itemType, result);
          if (obj != null)
          {
            flag = true;
            if (obj is ILocalizable localizable)
              flag = !SystemManager.CurrentContext.AppSettings.Multilingual ? language == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name : ((IEnumerable<CultureInfo>) localizable.AvailableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (x => x.Name == language)).FirstOrDefault<CultureInfo>() != null;
          }
        }
      }
      return flag;
    }

    private static SubscriptionData InitilizeCommentSubscriptionData(
      string subscriptionKey)
    {
      return new SubscriptionData()
      {
        ThreadKey = subscriptionKey,
        SubscriberKey = ClaimsManager.GetCurrentUserId().ToString()
      };
    }
  }
}
