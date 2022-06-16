// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.CommentsUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.TemporaryStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Events;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Services.Comments.DTO;
using Telerik.Sitefinity.Services.Comments.Proxies;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Comments
{
  internal static class CommentsUtilities
  {
    private static readonly string backwardCompatibilityEventsTransactionName = Guid.NewGuid().ToString();
    internal static int transactionSize = 500;

    internal static bool IsValidStatus(string status)
    {
      bool flag = true;
      if (status != "Published" && status != "Hidden" && status != "Spam" && status != "WaitingForApproval")
        flag = false;
      return flag;
    }

    /// <summary>Gets the group key.</summary>
    /// <param name="dataSourceName">Name of the data source. That is usually manager type. For dynamic items it is the dynamic module name.</param>
    /// <param name="providerName">Name of the provider.</param>
    internal static string GetGroupKey(string dataSourceName, string providerName) => dataSourceName + "_" + providerName;

    /// <summary>
    /// Gets the comments by thread for the current author that have rating.
    /// </summary>
    /// <param name="commentKeys">The thread key.</param>
    /// <param name="commentsService">The comments service.</param>
    /// <returns></returns>
    internal static IEnumerable<IComment> GetCommentsByThreadForCurrentAuthorWithRating(
      string threadKey,
      ICommentService commentsService)
    {
      CommentFilter filter = new CommentFilter();
      filter.ThreadKey.Add(threadKey);
      filter.HasRating = new bool?(true);
      Guid currentUserId = ClaimsManager.GetCurrentUserId();
      if (currentUserId != Guid.Empty)
        filter.AuthorKey.Add(currentUserId.ToString());
      else
        filter.AuthorIpAddress.Add(CommentsUtilities.GetIpAddressFromCurrentRequest());
      return SystemManager.GetCommentsService().GetComments(filter);
    }

    internal static IThread GetOrCreateThreadIfAllowedCommentsDetailView(
      IAuthor author,
      string threadKey,
      string threadType,
      string title,
      string groupKey,
      bool requireAuthentication,
      bool? requireApproval,
      bool allowComments)
    {
      ICommentService commentsService = SystemManager.GetCommentsService();
      string currentLanguage = CommentsUtilities.GetCurrentLanguage();
      IThread thread = commentsService.GetThread(threadKey);
      if (thread != null)
        return thread;
      if (groupKey.IsNullOrEmpty())
        throw new ArgumentNullException((string) null, "GroupKey cannot be null");
      if (!allowComments)
        return (IThread) null;
      CommentsUtilities.ResolveGroup(commentsService, author, groupKey);
      ThreadProxy threadProxy = new ThreadProxy(title, threadType, groupKey, author, SystemManager.CurrentContext.Culture)
      {
        Key = threadKey,
        Language = currentLanguage
      };
      return commentsService.CreateThread((IThread) threadProxy);
    }

    /// <summary>Gets the author IP address.</summary>
    /// <returns></returns>
    internal static string GetIpAddressFromCurrentRequest()
    {
      string serverVariable = SystemManager.CurrentHttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
      if (serverVariable.IsNullOrEmpty())
        serverVariable = SystemManager.CurrentHttpContext.Request.ServerVariables["REMOTE_ADDR"];
      return serverVariable;
    }

    internal static IThread GetThread(string key, string language) => SystemManager.GetCommentsService().GetThread(language);

    /// <summary>
    /// Gets the thread configuration. Falls back to default settings if there are no settings for the type.
    /// </summary>
    /// <param name="threadKey">The thread key.</param>
    internal static CommentsSettingsElement GetThreadConfig(string threadKey)
    {
      CommentsModuleConfig commentsModuleConfig = Config.Get<CommentsModuleConfig>();
      return threadKey.IsNullOrEmpty() ? commentsModuleConfig.DefaultSettings : CommentsUtilities.GetThreadConfigByType(SystemManager.GetCommentsService().GetThread(threadKey).Type);
    }

    /// <summary>
    /// Gets the thread configuration by its type. Falls back to default settings if there are no settings for the type.
    /// </summary>
    /// <param name="threadKey">The thread key.</param>
    internal static CommentsSettingsElement GetThreadConfigByType(
      string threadType)
    {
      CommentsModuleConfig commentsModuleConfig = Config.Get<CommentsModuleConfig>();
      return threadType.IsNullOrEmpty() || !commentsModuleConfig.CommentableTypes.Contains(threadType) ? commentsModuleConfig.DefaultSettings : (CommentsSettingsElement) commentsModuleConfig.CommentableTypes[threadType];
    }

    /// <summary>
    /// Gets the group keys collection applicable for the current site.
    /// </summary>
    /// <returns></returns>
    internal static List<string> GetGroupKeys()
    {
      List<string> groupKeys = new List<string>();
      groupKeys.Add(SystemManager.CurrentContext.CurrentSite.Id.ToString());
      if (SystemManager.CurrentContext.CurrentSite.SiteDataSourceLinks != null)
      {
        foreach (MultisiteContext.SiteDataSourceLinkProxy siteDataSourceLink in (IEnumerable<MultisiteContext.SiteDataSourceLinkProxy>) SystemManager.CurrentContext.CurrentSite.SiteDataSourceLinks)
          groupKeys.Add(CommentsUtilities.GetGroupKey(siteDataSourceLink.DataSourceName, siteDataSourceLink.ProviderName));
      }
      else
      {
        foreach (IDataSource dataSource in SystemManager.DataSourceRegistry.GetDataSources())
        {
          foreach (DataProviderInfo providerInfo in dataSource.ProviderInfos)
            groupKeys.Add(CommentsUtilities.GetGroupKey(dataSource.Name, providerInfo.ProviderName));
        }
      }
      return groupKeys;
    }

    /// <summary>Gets the current language.</summary>
    internal static string GetCurrentLanguage()
    {
      SitefinityContextBase currentContext = SystemManager.CurrentContext;
      if (!currentContext.AppSettings.Multilingual)
        return currentContext.CurrentSite.DefaultCulture.Name;
      string str = SystemManager.CurrentContext.Culture.ToString();
      return !string.IsNullOrWhiteSpace(str) ? str : CultureInfo.GetCultureInfo(Config.Get<ResourcesConfig>().DefaultCulture.UICulture).ToString();
    }

    /// <summary>Gets the commented item URL.</summary>
    /// <param name="parentThread">The parent thread.</param>
    /// <returns></returns>
    internal static string GetCommentedItemUrl(IThread parentThread)
    {
      string commentedItemUrl = (string) null;
      Guid contentItemId;
      if (ControlUtilities.TryGetItemId(parentThread.Key, out contentItemId))
      {
        Type itemType = TypeResolutionService.ResolveType(parentThread.Type, false);
        if (itemType != (Type) null)
        {
          if (itemType == typeof (PageNode) || itemType.IsSubclassOf(typeof (PageNode)))
          {
            commentedItemUrl = CommentsUtilities.GetPageUrl(contentItemId.ToString(), parentThread.Language);
          }
          else
          {
            IContentItemLocation itemDefaultLocation = SystemManager.GetContentLocationService().GetItemDefaultLocation(itemType, parentThread.DataSource, contentItemId, new CultureInfo(parentThread.Language));
            if (itemDefaultLocation != null)
              commentedItemUrl = itemDefaultLocation.ItemAbsoluteUrl;
          }
        }
      }
      if (commentedItemUrl == null)
        commentedItemUrl = "javascript:void(0);";
      return commentedItemUrl;
    }

    /// <summary>Gets the page URL.</summary>
    /// <param name="pageNodeId">The page node id.</param>
    /// <param name="language">The language.</param>
    /// <returns></returns>
    internal static string GetPageUrl(string pageNodeId, string language)
    {
      SiteMapProvider currentProvider = SitefinitySiteMap.GetCurrentProvider();
      using (new CultureRegion(language))
      {
        SiteMapNode node;
        if (currentProvider is SiteMapBase)
        {
          SiteMapBase siteMapBase = (SiteMapBase) currentProvider;
          node = siteMapBase.FindSiteMapNodeFromKey(pageNodeId, false);
          if (node != null)
            node = siteMapBase.FindSiteMapNodeForSpecificLanguage(node, new CultureInfo(language), false);
        }
        else
          node = currentProvider.FindSiteMapNodeFromKey(pageNodeId);
        if (node != null)
          return UrlPath.ResolveUrl(node.Url);
      }
      return "javascript:void(0);";
    }

    internal static IAuthor GetAuthor(CommentCreateRequest commentData)
    {
      IAuthor author = (IAuthor) new AuthorProxy();
      Guid currentUserId = ClaimsManager.GetCurrentUserId();
      if (currentUserId != Guid.Empty)
        author.Key = currentUserId.ToString();
      author.Name = string.IsNullOrWhiteSpace(commentData.Name) ? (string) null : commentData.Name;
      author.Email = string.IsNullOrWhiteSpace(commentData.Email) ? (string) null : commentData.Email;
      return author;
    }

    internal static IAuthor ResolveAuthorInfo(
      IAuthor author,
      bool includeSensitiveInformation = false)
    {
      bool flag1 = string.IsNullOrWhiteSpace(author.Name);
      int num = string.IsNullOrWhiteSpace(author.Email) ? 1 : 0;
      IAuthor author1 = (IAuthor) new AuthorProxy()
      {
        Key = author.Key
      };
      if (!flag1)
        author1.Name = author.Name;
      if (num == 0 & includeSensitiveInformation)
        author1.Email = author.Email;
      Guid result;
      Guid.TryParse(author.Key, out result);
      bool flag2 = (num & (includeSensitiveInformation ? 1 : 0)) != 0;
      if (result != Guid.Empty && flag1 | flag2)
      {
        ICacheUserProfile cachedUserProfile = (ICacheUserProfile) UserManager.GetCachedUserProfile(result);
        if (cachedUserProfile != null)
        {
          if (flag1)
            author1.Name = cachedUserProfile.Nickname ?? UserProfilesHelper.GetUserDisplayName(result);
          if (flag2)
            author1.Email = cachedUserProfile.Email;
        }
        else if (flag1)
          author1.Name = Res.Get<UserProfilesResources>().MissingUser;
      }
      return author1;
    }

    /// <summary>
    /// Checks if the comments are supported and enabled for the type.
    /// </summary>
    /// <returns>true if the item has comments and the comments module allows comments; false otherwise</returns>
    internal static bool IsCommentableType(string itemTypeFullName)
    {
      if (!SystemManager.IsModuleAccessible("Comments"))
        return false;
      CommentsModuleConfig commentsModuleConfig = Config.Get<CommentsModuleConfig>();
      if (commentsModuleConfig == null || !commentsModuleConfig.DefaultSettings.AllowComments)
        return false;
      CommentableTypeElement commentableTypeElement = (CommentableTypeElement) null;
      return commentsModuleConfig.CommentableTypes.TryGetValue(itemTypeFullName, out commentableTypeElement);
    }

    /// <summary>
    /// Returns flag that indicated when comments are enabled for given type.
    /// </summary>
    /// <param name="itemTypeFullName">The full name of the type.</param>
    internal static bool CommentsEnabled(string itemTypeFullName)
    {
      CommentableTypeElement commentableTypeElement;
      bool flag = Config.Get<CommentsModuleConfig>().CommentableTypes.TryGetValue(itemTypeFullName, out commentableTypeElement);
      return flag ? commentableTypeElement.AllowComments : flag;
    }

    /// <summary>Try patse the commented item id by thread key.</summary>
    /// <param name="threadKey">The thread key.</param>
    public static bool TryParseCommentedItemId(string threadKey, out Guid contentItemId)
    {
      contentItemId = Guid.Empty;
      if (threadKey.IsNullOrEmpty())
        return false;
      string[] source = threadKey.Split('_');
      return ((IEnumerable<string>) source).Count<string>() > 0 && Guid.TryParse(source[0], out contentItemId);
    }

    internal static Guid GetCommentId(string commentKey)
    {
      Guid result;
      return Guid.TryParse(commentKey, out result) ? result : Guid.Empty;
    }

    internal static Guid GetCommentetItemId(string threadKey)
    {
      Guid contentItemId;
      return CommentsUtilities.TryParseCommentedItemId(threadKey, out contentItemId) ? contentItemId : Guid.Empty;
    }

    private static void RemoveCaptchaFromTempStorage(string key) => ObjectFactory.Resolve<ITemporaryStorage>().Remove(key);

    internal static void RaiseOldCommentCreatingEvent(
      ref IComment comment,
      string threadType,
      string threadDataSource,
      string eventOrigin)
    {
      Comment compatibleComment = CommentsUtilities.CreateBackwardCompatibleComment(CommentsUtilities.backwardCompatibilityEventsTransactionName);
      CommentsUtilities.Populate(ref compatibleComment, comment, threadType, threadDataSource);
      CommentCreatingEvent commentCreatingEvent = new CommentCreatingEvent();
      commentCreatingEvent.Origin = eventOrigin;
      commentCreatingEvent.DataItem = compatibleComment;
      commentCreatingEvent.CreationDate = comment.DateCreated;
      EventHub.Raise((IEvent) commentCreatingEvent, true);
      CommentsUtilities.Populate(ref comment, commentCreatingEvent.DataItem);
      TransactionManager.DisposeTransaction(CommentsUtilities.backwardCompatibilityEventsTransactionName);
    }

    internal static void RaiseOldCommentCreatedEvent(IComment comment, string eventOrigin)
    {
      CommentCreatedEvent commentCreatedEvent = new CommentCreatedEvent();
      commentCreatedEvent.Origin = eventOrigin;
      commentCreatedEvent.CommentId = CommentsUtilities.GetCommentId(comment.Key);
      commentCreatedEvent.CreationDate = comment.DateCreated;
      EventHub.Raise((IEvent) commentCreatedEvent, true);
    }

    internal static void RaiseOldCommentUpdatingEvent(
      ref IComment comment,
      string threadType,
      string threadDataSource,
      string eventOrigin)
    {
      Comment compatibleComment = CommentsUtilities.CreateBackwardCompatibleComment(CommentsUtilities.backwardCompatibilityEventsTransactionName);
      CommentsUtilities.Populate(ref compatibleComment, comment, threadType, threadDataSource);
      CommentUpdatingEvent commentUpdatingEvent = new CommentUpdatingEvent();
      commentUpdatingEvent.Origin = eventOrigin;
      commentUpdatingEvent.DataItem = compatibleComment;
      EventHub.Raise((IEvent) commentUpdatingEvent, true);
      CommentsUtilities.Populate(ref comment, commentUpdatingEvent.DataItem);
      TransactionManager.DisposeTransaction(CommentsUtilities.backwardCompatibilityEventsTransactionName);
    }

    internal static void RaiseOldCommentUpdatedEvent(IComment comment, string eventOrigin)
    {
      CommentUpdatedEvent commentUpdatedEvent = new CommentUpdatedEvent();
      commentUpdatedEvent.Origin = eventOrigin;
      commentUpdatedEvent.CommentId = CommentsUtilities.GetCommentId(comment.Key);
      commentUpdatedEvent.ModificationDate = DateTime.UtcNow;
      EventHub.Raise((IEvent) commentUpdatedEvent, true);
    }

    internal static void RaiseOldCommentDeletingEvent(
      IComment comment,
      string threadType,
      string threadDataSource,
      string eventOrigin)
    {
      Comment compatibleComment = CommentsUtilities.CreateBackwardCompatibleComment(CommentsUtilities.backwardCompatibilityEventsTransactionName);
      CommentsUtilities.Populate(ref compatibleComment, comment, threadType, threadDataSource);
      CommentDeletingEvent commentDeletingEvent = new CommentDeletingEvent();
      commentDeletingEvent.Origin = eventOrigin;
      commentDeletingEvent.DataItem = compatibleComment;
      EventHub.Raise((IEvent) commentDeletingEvent, true);
      TransactionManager.DisposeTransaction(CommentsUtilities.backwardCompatibilityEventsTransactionName);
    }

    internal static void RaiseOldCommentDeletedEvent(IComment comment, string eventOrigin)
    {
      CommentDeletedEvent commentDeletedEvent = new CommentDeletedEvent();
      commentDeletedEvent.Origin = eventOrigin;
      commentDeletedEvent.CommentId = CommentsUtilities.GetCommentId(comment.Key);
      commentDeletedEvent.DeletionDate = DateTime.UtcNow;
      EventHub.Raise((IEvent) commentDeletedEvent, true);
    }

    internal static void Populate(ref IComment comment, Comment commentOld)
    {
      comment.Author = (IAuthor) new AuthorProxy();
      if (commentOld.Owner != Guid.Empty)
        comment.Author.Key = commentOld.Owner.ToString();
      if (!string.IsNullOrWhiteSpace((string) commentOld.AuthorName) && commentOld.AuthorName != (Lstring) commentOld.ProfileAuthorName)
        comment.Author.Name = (string) commentOld.AuthorName;
      if (!string.IsNullOrWhiteSpace(commentOld.Email) && commentOld.Email != commentOld.ProfileEmail)
        comment.Author.Email = commentOld.Email;
      if (commentOld.LastModifiedBy != Guid.Empty)
        comment.LastModifiedBy = (IAuthor) new AuthorProxy()
        {
          Key = commentOld.LastModifiedBy.ToString()
        };
      comment.Key = commentOld.Id.ToString();
      comment.AuthorIpAddress = commentOld.IpAddress;
      comment.Message = (string) commentOld.Content;
      comment.DateCreated = commentOld.DateCreated;
      comment.LastModified = commentOld.LastModified;
      comment.Status = CommentsUtilities.GetCommentStatus(commentOld.CommentStatus);
    }

    internal static void Populate(
      ref Comment commentOld,
      IComment comment,
      string threadType,
      string threadDataSource)
    {
      Guid result1;
      if (Guid.TryParse(comment.Author.Key, out result1))
        commentOld.Owner = result1;
      if (!string.IsNullOrWhiteSpace(comment.Author.Name))
        commentOld.AuthorName = (Lstring) comment.Author.Name;
      if (!string.IsNullOrWhiteSpace(comment.Author.Email))
        commentOld.Email = comment.Author.Email;
      if ((string.IsNullOrWhiteSpace((string) commentOld.AuthorName) || string.IsNullOrWhiteSpace(commentOld.Email)) && commentOld.Owner != Guid.Empty)
      {
        ICacheUserProfile cachedUserProfile = (ICacheUserProfile) UserManager.GetCachedUserProfile(result1);
        if (cachedUserProfile != null)
        {
          if (string.IsNullOrWhiteSpace((string) commentOld.AuthorName))
            commentOld.ProfileAuthorName = cachedUserProfile.Nickname;
          if (string.IsNullOrWhiteSpace(commentOld.Email))
            commentOld.ProfileEmail = cachedUserProfile.Email;
        }
      }
      Guid result2;
      if (comment.LastModifiedBy != null && Guid.TryParse(comment.LastModifiedBy.Key, out result2))
        commentOld.LastModifiedBy = result2;
      commentOld.IpAddress = comment.AuthorIpAddress;
      commentOld.CommentStatus = CommentsUtilities.GetBackwardCompatibleCommentStatus(comment.Status);
      commentOld.Content = (Lstring) comment.Message;
      commentOld.DateCreated = comment.DateCreated;
      commentOld.LastModified = comment.LastModified;
      commentOld.Id = CommentsUtilities.GetCommentId(comment.Key);
      commentOld.CommentedItemID = CommentsUtilities.GetCommentetItemId(comment.ThreadKey);
      commentOld.CommentedItemType = threadType;
      commentOld.ProviderName = threadDataSource;
    }

    internal static Comment CreateBackwardCompatibleComment(string transactionName)
    {
      SitefinityOAContext context = ((IOpenAccessDataProvider) MetadataManager.GetManager((string) null, transactionName).Provider).GetContext();
      Comment compatibleComment = new Comment();
      Comment entity = compatibleComment;
      context.Add((object) entity);
      return compatibleComment;
    }

    internal static string GetCommentStatus(CommentStatus status)
    {
      switch (status)
      {
        case CommentStatus.Published:
          return "Published";
        case CommentStatus.Spam:
          return "Spam";
        case CommentStatus.WaitingForApproval:
          return "WaitingForApproval";
        default:
          return "Hidden";
      }
    }

    internal static CommentStatus GetBackwardCompatibleCommentStatus(string status)
    {
      if (status == "Published")
        return CommentStatus.Published;
      if (status == "Spam")
        return CommentStatus.Spam;
      if (status == "Hidden")
        return CommentStatus.Hidden;
      if (status == "WaitingForApproval")
        ;
      return CommentStatus.WaitingForApproval;
    }

    /// <summary>Returns parent thread</summary>
    /// <remarks>Create thead if thread doesn't exist; Create group if thread doesn't exist;</remarks>
    internal static IThread ResolveThread(
      Comment comment,
      string threadKey = null,
      string language = null)
    {
      if (threadKey == null)
        threadKey = ControlUtilities.GetLocalizedKey((object) comment.CommentedItemID, language);
      ICommentService commentsService = SystemManager.GetCommentsService();
      IThread thread = commentsService.GetThread(threadKey);
      if (thread == null)
      {
        string commentedItemType = comment.CommentedItemType;
        string groupKey = CommentsUtilities.GetGroupKey(ManagerBase.GetMappedManager(comment.CommentedItemType).GetType().FullName, comment.ProviderName);
        string title = (comment.CommentedItem as IHasTitle).GetTitle();
        if (title.IsNullOrEmpty())
          title = "Backward compatible thread";
        if (language == null)
          language = CommentsUtilities.GetCurrentLanguage();
        AuthorProxy authorProxy = !(comment.Owner != Guid.Empty) ? new AuthorProxy((string) comment.AuthorName, comment.Email) : new AuthorProxy(comment.Owner.ToString());
        CommentsUtilities.ResolveGroup(commentsService, (IAuthor) authorProxy, groupKey);
        ThreadProxy threadProxy = new ThreadProxy(title, commentedItemType, groupKey, (IAuthor) authorProxy, SystemManager.CurrentContext.Culture)
        {
          Key = threadKey,
          Language = language,
          DataSource = comment.ProviderName
        };
        thread = commentsService.CreateThread((IThread) threadProxy);
      }
      return thread;
    }

    /// <summary>Returns parent group</summary>
    /// <remarks>Create group if group doesn't exist</remarks>
    internal static IGroup ResolveGroup(ICommentService cs, IAuthor author, string groupKey)
    {
      IGroup group = cs.GetGroup(groupKey);
      if (group == null)
      {
        string liveUrl = SystemManager.CurrentContext.CurrentSite.LiveUrl;
        GroupProxy groupProxy = new GroupProxy(groupKey, liveUrl, author)
        {
          Key = groupKey
        };
        group = cs.CreateGroup((IGroup) groupProxy);
      }
      return group;
    }

    internal static void ProcessGenericComment(Comment comment)
    {
      ICommentService commentsService = SystemManager.GetCommentsService();
      if (comment.DirtyItemState == SecurityConstants.TransactionActionType.New)
      {
        IThread thread = CommentsUtilities.ResolveThread(comment);
        if (thread == null)
          return;
        IComment comment1 = (IComment) new CommentProxy();
        CommentsUtilities.Populate(ref comment1, comment);
        comment1.ThreadKey = thread.Key;
        commentsService.CreateComment(comment1);
      }
      else if (comment.DirtyItemState == SecurityConstants.TransactionActionType.Updated)
      {
        IComment comment2 = commentsService.GetComment(comment.Id.ToString());
        CommentsUtilities.Populate(ref comment2, comment);
        commentsService.UpdateComment(comment2);
      }
      else
      {
        if (comment.DirtyItemState != SecurityConstants.TransactionActionType.Deleted)
          return;
        commentsService.DeleteComment(comment.Id.ToString());
      }
    }

    internal static void UpgradeComments(Type[] managerTypes, string moduleName)
    {
      ICommentService commentsService = SystemManager.GetCommentsService();
      foreach (Type managerType in managerTypes)
      {
        if (managerType.ImplementsInterface(typeof (ICommentsManager)))
        {
          try
          {
            foreach (string providerName in ManagerBase.GetManager(managerType).GetProviderNames(ProviderBindingOptions.NoFilter))
            {
              try
              {
                IManager manager = ManagerBase.GetManager(managerType, providerName);
                CommentsUtilities.UpgradeComments(commentsService, manager);
              }
              catch (Exception ex)
              {
                Log.Write((object) string.Format("FAILED: MigrateComments - {0}", (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
                if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                  throw;
              }
            }
          }
          catch (Exception ex)
          {
            Log.Write((object) string.Format("FAILED: MigrateComments - {0}", (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw;
          }
        }
      }
    }

    private static void UpgradeComments(ICommentService commentService, IManager manager)
    {
      if (!(manager is ICommentsManager) || !(manager.Provider is ContentDataProviderBase provider))
        return;
      List<Comment> list = provider.GetComments_Old().Take<Comment>(CommentsUtilities.transactionSize).ToList<Comment>();
      bool flag = list.Count<Comment>() > 0;
      List<IComment> migratedComments = new List<IComment>();
      Dictionary<string, IThread> threadsCache = new Dictionary<string, IThread>();
      List<string> stringList = new List<string>();
      try
      {
        while (flag)
        {
          for (int index = 0; index < list.Count<Comment>(); ++index)
          {
            Comment comment = list[index];
            if (comment.CommentedItem is ILifecycleDataItem commentedItem && SystemManager.CurrentContext.AppSettings.Multilingual)
            {
              stringList.Clear();
              foreach (LanguageData languageData in (IEnumerable<LanguageData>) commentedItem.LanguageData)
              {
                if (languageData.Language != null && !stringList.Contains(languageData.Language))
                {
                  CommentsUtilities.UpgradeComment(comment, stringList.Count > 0, languageData.Language, ref migratedComments, ref threadsCache);
                  stringList.Add(languageData.Language);
                }
              }
            }
            else
              CommentsUtilities.UpgradeComment(comment, false, (string) null, ref migratedComments, ref threadsCache);
            provider.Delete_Old(comment);
          }
          using (new ElevatedModeRegion(manager))
          {
            commentService.CreateComments((IEnumerable<IComment>) migratedComments);
            migratedComments.Clear();
            provider.CommitTransaction();
            list = provider.GetComments_Old().Take<Comment>(CommentsUtilities.transactionSize).ToList<Comment>();
            flag = list.Count<Comment>() > 0;
          }
        }
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions))
          return;
        throw;
      }
    }

    private static void UpgradeComment(
      Comment comment,
      bool generateNewId,
      string language,
      ref List<IComment> migratedComments,
      ref Dictionary<string, IThread> threadsCache)
    {
      if (string.IsNullOrWhiteSpace((string) comment.Content))
        return;
      string localizedKey = ControlUtilities.GetLocalizedKey((object) comment.CommentedItemID, language);
      if (!threadsCache.ContainsKey(localizedKey))
        threadsCache.Add(localizedKey, CommentsUtilities.ResolveThread(comment, localizedKey, language));
      IComment comment1 = (IComment) new CommentProxy();
      comment1.ThreadKey = localizedKey;
      CommentsUtilities.Populate(ref comment1, comment);
      Guid result = Guid.Empty;
      Guid.TryParse(comment1.Author.Key, out result);
      if ((result == Guid.Empty || string.IsNullOrWhiteSpace(comment1.Author.Key)) && string.IsNullOrWhiteSpace(comment1.Author.Name))
        comment1.Author.Name = "-";
      if (generateNewId)
        comment1.Key = (string) null;
      migratedComments.Add(comment1);
    }

    internal static IEnumerable<CommentResponse> GetCommentResponses(
      IEnumerable<IComment> comments,
      bool includeSensitiveInformation = false)
    {
      List<CommentResponse> commentResponses = new List<CommentResponse>();
      foreach (IComment comment in comments)
        commentResponses.Add(CommentsUtilities.GetCommentResponse(comment, includeSensitiveInformation));
      return (IEnumerable<CommentResponse>) commentResponses;
    }

    internal static CommentResponse GetCommentResponse(
      IComment comment,
      bool includeSensitiveInformation = false)
    {
      CommentResponse commentResponce = new CommentResponse();
      commentResponce.Key = comment.Key;
      commentResponce.Message = comment.Message;
      commentResponce.DateCreated = comment.DateCreated;
      commentResponce.ThreadKey = comment.ThreadKey;
      commentResponce.Status = comment.Status;
      commentResponce.CustomData = comment.CustomData;
      commentResponce.Rating = comment.Rating;
      if (includeSensitiveInformation)
        commentResponce.AuthorIpAddress = comment.AuthorIpAddress;
      CommentsUtilities.PopulateAuthorInfo(ref commentResponce, comment.Author, includeSensitiveInformation);
      return commentResponce;
    }

    internal static IEnumerable<ThreadResponse> GetThreadsResponses(
      IEnumerable<IThread> threads)
    {
      List<ThreadResponse> threadsResponses = new List<ThreadResponse>();
      foreach (IThread thread in threads)
        threadsResponses.Add(CommentsUtilities.GetThreadResponse(thread));
      return (IEnumerable<ThreadResponse>) threadsResponses;
    }

    internal static ThreadResponse GetThreadResponse(IThread thread) => new ThreadResponse()
    {
      Key = thread.Key,
      Title = thread.Title,
      Type = thread.Type,
      CommentsCount = thread.CommentsCount,
      Language = thread.Language,
      IsClosed = thread.IsClosed,
      GroupKey = thread.GroupKey,
      DataSource = thread.DataSource,
      ItemUrl = CommentsUtilities.GetCommentedItemUrl(thread)
    };

    internal static void PopulateAuthorInfo(
      ref CommentResponse commentResponce,
      IAuthor author,
      bool includeSensitiveInformation)
    {
      IAuthor author1 = CommentsUtilities.ResolveAuthorInfo(author, includeSensitiveInformation);
      commentResponce.Name = author1.Name;
      commentResponce.Email = author1.Email;
      Guid result;
      Guid.TryParse(author1.Key, out result);
      Telerik.Sitefinity.Libraries.Model.Image image;
      commentResponce.ProfilePictureUrl = UserProfilesHelper.GetAvatarImageUrl(result, out image);
      if (image != null && !string.IsNullOrEmpty(image.MediaUrl))
        commentResponce.ProfilePictureThumbnailUrl = image.ThumbnailUrl;
      else
        commentResponce.ProfilePictureThumbnailUrl = RouteHelper.ResolveUrl("~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", UrlResolveOptions.Rooted);
    }

    internal static CommentResponse CreateCommentViaWebService(
      string email,
      string name,
      string message,
      string threadKey,
      string threadType = null,
      string threadTitle = null,
      string dataSource = null,
      string language = null,
      string groupKey = null,
      string answer = null,
      string correctAnswer = null,
      string initializationVector = null,
      string captchaKey = null,
      bool skipCaptcha = false)
    {
      CommentWebService commentWebService = new CommentWebService();
      CommentCreateRequest commentCreateRequest = new CommentCreateRequest()
      {
        Name = name,
        Email = email,
        Message = message,
        ThreadKey = threadKey
      };
      commentCreateRequest.Thread = new ThreadCreateRequest()
      {
        DataSource = dataSource,
        GroupKey = groupKey,
        Language = language,
        Title = threadTitle,
        Type = threadType
      };
      commentCreateRequest.Thread.Group = new GroupCreateRequest()
      {
        Key = groupKey,
        Name = groupKey,
        Description = dataSource
      };
      commentCreateRequest.Captcha = new CaptchaInfo()
      {
        Answer = answer,
        CorrectAnswer = correctAnswer,
        InitializationVector = initializationVector,
        Key = captchaKey
      };
      CommentCreateRequest request = commentCreateRequest;
      int num = skipCaptcha ? 1 : 0;
      return commentWebService.CreateComment(request, num != 0);
    }
  }
}
