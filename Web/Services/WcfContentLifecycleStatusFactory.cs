// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.WcfContentLifecycleStatusFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Factory for creating ContentItemContext, ContentViewModel and WcfContentLifecycleStatus
  /// </summary>
  public static class WcfContentLifecycleStatusFactory
  {
    /// <summary>
    /// Factory method for creating and filling information about <paramref name="content" />'s lifecycle status.
    /// </summary>
    /// <param name="content">Content to get lifecycle status for</param>
    /// <param name="manager">Content lifecycle manager that will be used to fill in information for <paramref name="content" /></param>
    /// <returns>WCF-ready object that can be used in a ContentItemContext or ContentViewModel</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///     When either <paramref name="content" /> or <paramref name="manager" /> is <c>null</c>.
    /// </exception>
    public static WcfContentLifecycleStatus Create<T>(
      T content,
      IContentLifecycleManager<T> manager,
      CultureInfo culture = null)
      where T : Content
    {
      return WcfContentLifecycleStatusFactory.CreateInternal<T>(content, manager, false, false, culture);
    }

    /// <summary>Creates the specified content.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content">The content.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="liveItem">The live content item related to the master content item.</param>
    /// <param name="tempItem">The temp content item related to the master content item.</param>
    /// <returns></returns>
    public static WcfContentLifecycleStatus Create<T>(
      T content,
      IContentLifecycleManager<T> manager,
      T liveContent,
      T tempContent,
      CultureInfo culture = null)
      where T : Content
    {
      return WcfContentLifecycleStatusFactory.CreateInternal<T>(content, manager, true, true, culture, liveContent, tempContent);
    }

    /// <summary>Creates the internal.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content">The content.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="checkedLive">if set to <c>true</c> [checked live].</param>
    /// <param name="checkedTemp">if set to <c>true</c> [checked temp].</param>
    /// <param name="liveItem">The live content item related to the master content item.</param>
    /// <param name="tempItem">The temp content item related to the master content item.</param>
    /// <returns></returns>
    public static WcfContentLifecycleStatus CreateInternal<T>(
      T content,
      IContentLifecycleManager<T> manager,
      bool isLoadedLiveItem,
      bool isLoadedTempItem,
      CultureInfo culture = null,
      T liveContent = null,
      T tempContent = null)
      where T : Content
    {
      if ((object) content == null)
        throw new ArgumentNullException(nameof (content));
      if (manager == null)
        throw new ArgumentNullException(nameof (manager));
      WcfContentLifecycleStatus status = new WcfContentLifecycleStatus();
      if (content.SupportsContentLifecycle)
      {
        T liveContentItem = WcfContentLifecycleStatusFactory.GetLiveContentItem<T>(isLoadedLiveItem, liveContent, content, manager);
        T tempContentItem = WcfContentLifecycleStatusFactory.GetTempContentItem<T>(isLoadedTempItem, tempContent, content, manager);
        T obj = default (T);
        using (new ElevatedModeRegion((IManager) manager))
          obj = manager.GetMaster(content);
        culture = culture.GetSitefinityCulture();
        int languageVersion1 = liveContentItem.GetLanguageVersion(culture);
        int languageVersion2 = obj.GetLanguageVersion(culture);
        status.LastModified = new DateTime?(obj.LastModified);
        status.LastModifiedBy = UserProfilesHelper.GetUserDisplayName(obj.LastModifiedBy);
        status.PublicationDate = new DateTime?(obj.PublicationDate);
        if ((object) liveContentItem != null)
        {
          bool flag = false;
          CultureInfo languageForObject = LocalizationHelper.GetDefaultLanguageForObject((object) liveContentItem);
          if (SystemManager.CurrentContext.AppSettings.Multilingual && culture == languageForObject)
          {
            if (liveContentItem.IsPublishedInCulture(culture))
            {
              WcfContentLifecycleStatusFactory.SetStatus<T>(languageVersion1, languageVersion2, liveContentItem, status, culture);
              flag = true;
            }
            else if (liveContentItem.IsPublishedInCultureRaw((CultureInfo) null))
            {
              WcfContentLifecycleStatusFactory.SetStatus<T>(liveContentItem.GetLanguageVersionRaw((CultureInfo) null), obj.GetLanguageVersionRaw((CultureInfo) null), liveContentItem, status, (CultureInfo) null);
              flag = true;
            }
            else
            {
              status.Message = Res.Get<ContentLifecycleMessages>().DraftNewerThanPublished;
              status.IsPublished = false;
            }
          }
          else if (liveContentItem.IsPublishedInCulture(culture))
          {
            WcfContentLifecycleStatusFactory.SetStatus<T>(languageVersion1, languageVersion2, liveContentItem, status, culture);
            flag = true;
          }
          else
          {
            status.Message = Res.Get<ContentLifecycleMessages>().Draft;
            status.IsPublished = false;
          }
          status.PublicationDate = new DateTime?(liveContentItem.PublicationDate);
          if (liveContentItem.Visible & flag)
            status.HasLiveVersion = true;
        }
        else
        {
          status.Message = Res.Get<ContentLifecycleMessages>().Draft;
          status.IsPublished = false;
        }
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if ((object) tempContentItem != null && tempContentItem.Owner != Guid.Empty)
        {
          string formattedUserName = SecurityManager.GetFormattedUserName(tempContentItem.Owner);
          status.Message = Res.Get<ContentLifecycleMessages>().LockedByUserName.Arrange((object) formattedUserName);
          status.LockedByUsername = formattedUserName;
          status.LockedSince = new DateTime?(tempContentItem.DateCreated);
          status.IsLocked = true;
          status.IsLockedByMe = tempContentItem.Owner == currentIdentity.UserId;
        }
        else
        {
          status.IsLocked = false;
          status.IsLockedByMe = false;
        }
        status.IsAdmin = currentIdentity.IsUnrestricted;
      }
      else
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        status.Message = Res.Get<ContentLifecycleMessages>().Published;
        status.IsAdmin = currentIdentity.IsUnrestricted;
        status.IsLocked = false;
        status.IsLockedByMe = false;
        status.ErrorMessage = (string) null;
      }
      status.SupportsContentLifecycle = content.SupportsContentLifecycle;
      if (content is IApprovalWorkflowItem approvalWorkflowItem)
        status.WorkflowStatus = (string) approvalWorkflowItem.ApprovalWorkflowState;
      if (status.WorkflowStatus.IsNullOrWhitespace())
        status.WorkflowStatus = status.IsPublished ? Res.Get<ContentLifecycleMessages>().Published : Res.Get<ContentLifecycleMessages>().Draft;
      return status;
    }

    /// <summary>
    /// Create content item context out of <paramref name="content" /> with auto-filled LifecycleStatus property
    /// </summary>
    /// <typeparam name="T">Type of the content item.</typeparam>
    /// <param name="content">Content item</param>
    /// <param name="manager">Lifecycle manager</param>
    /// <returns>ContentItemContext with auto-filled LifecycleStatus property</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///     When either <paramref name="content" /> or <paramref name="manager" /> is <c>null</c>.
    /// </exception>
    public static ContentItemContext<T> CreateItemContext<T>(
      T content,
      IContentLifecycleManager<T> manager,
      CultureInfo culture = null)
      where T : Content
    {
      ContentItemContext<T> itemContext = new ContentItemContext<T>();
      itemContext.Item = content;
      itemContext.LifecycleStatus = WcfContentLifecycleStatusFactory.Create<T>(content, manager, culture);
      itemContext.Warnings = SystemManager.StatusProviderRegistry.GetWarnings(content.OriginalContentId != Guid.Empty ? content.OriginalContentId : content.Id, content.GetType(), content.GetProviderName(), culture: culture).Select<WarningInfo, ItemWarning>((Func<WarningInfo, ItemWarning>) (w => new ItemWarning(w)));
      return itemContext;
    }

    /// <summary>
    /// Create content view model and auto-fill LifecycleStatus property
    /// </summary>
    /// <typeparam name="T">Type of the content</typeparam>
    /// <typeparam name="M">Type of the manager</typeparam>
    /// <typeparam name="P">Type of the manager's provider</typeparam>
    /// <param name="content">Content item</param>
    /// <param name="manager">Content item's manager</param>
    /// <returns>ContentViewModel with auto-filled LifecycleStatus property</returns>
    public static ContentViewModel CreateContentViewModel<T, M, P>(
      T content,
      M manager)
      where T : Content
      where M : ContentManagerBase<P>, IContentLifecycleManager<T>
      where P : ContentDataProvider
    {
      ContentViewModel contentViewModel = new ContentViewModel((Content) content, (ContentDataProviderBase) manager.Provider);
      contentViewModel.LifecycleStatus = WcfContentLifecycleStatusFactory.Create<T>(content, (IContentLifecycleManager<T>) manager);
      return contentViewModel;
    }

    private static void SetStatus<T>(
      int liveVersion,
      int masterVersion,
      T live,
      WcfContentLifecycleStatus status,
      CultureInfo culture)
      where T : Content
    {
      if (liveVersion == masterVersion)
      {
        switch (live.GetUIStatusRaw(culture))
        {
          case ContentUIStatus.Published:
            status.Message = Res.Get<ContentLifecycleMessages>().Published;
            status.IsPublished = true;
            break;
          case ContentUIStatus.Scheduled:
            status.Message = Res.Get<ContentLifecycleMessages>().Scheduled;
            status.IsPublished = false;
            break;
          default:
            status.Message = Res.Get<ContentLifecycleMessages>().Draft;
            status.IsPublished = false;
            break;
        }
      }
      else
      {
        status.Message = Res.Get<ContentLifecycleMessages>().DraftNewerThanPublished;
        status.IsPublished = false;
      }
    }

    private static T GetLiveContentItem<T>(
      bool checkedLive,
      T liveContent,
      T content,
      IContentLifecycleManager<T> manager)
      where T : Content
    {
      return checkedLive ? liveContent : manager.GetLive(content);
    }

    private static T GetTempContentItem<T>(
      bool checkedLive,
      T liveContent,
      T content,
      IContentLifecycleManager<T> manager)
      where T : Content
    {
      return checkedLive ? liveContent : manager.GetTemp(content);
    }
  }
}
