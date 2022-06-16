// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Lifecycle
{
  public static class LifecycleExtensions
  {
    internal static string StatusNewerThanPublished = "PublishedAndDraft";
    internal static string StatusUnpublished = "Unpublished";
    internal static string StatusLocked = "Locked";
    internal static string StatusPublished = "Published";
    internal static string StatusDraft = "Draft";

    /// <summary>
    /// Determines whether the specified type is ILifecycleDataItem and whether the related manager is ILifecycleManager
    /// </summary>
    public static bool IsILifecycle(this Type type) => typeof (ILifecycleDataItem).IsAssignableFrom(type);

    /// <summary>
    /// Determines whether the workflow status must be synced for all languages for the specified type.
    /// </summary>
    public static bool MustSyncWorkflowStatus(this Type type) => type != typeof (PageNode) && !typeof (ILifecycleDataItem).IsAssignableFrom(type);

    /// <summary>
    /// Gets the item (live, temp or master) which is related with the specified item by lifecycle status.
    /// The method supports only items of type Content and ILifecycleDataItem
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="manager">The manager to be used.</param>
    /// <param name="provider">The provider to be used.</param>
    /// <param name="status">The lifecycle status of the related item.</param>
    public static Content GetRelatedItem(
      this object item,
      object manager,
      string provider,
      ContentLifecycleStatus status)
    {
      Content content = item as Content;
      Content relatedItem = (Content) null;
      if (content != null)
      {
        switch (manager)
        {
          case null:
            switch (item)
            {
              case ILifecycleDataItem _:
                if (ManagerBase.GetMappedManager(item.GetType(), provider) is ILifecycleManager mappedManager1)
                {
                  relatedItem = mappedManager1.GetRelatedItem((ILifecycleDataItem) item, status);
                  break;
                }
                break;
              case Content _:
                if (ManagerBase.GetMappedManager(item.GetType(), provider) is IContentLifecycleManager mappedManager2)
                {
                  relatedItem = mappedManager2.GetRelatedItem((Content) item, status);
                  break;
                }
                break;
            }
            break;
          case ILifecycleManager _ when item is ILifecycleDataItem:
            relatedItem = ((ILifecycleManager) manager).GetRelatedItem((ILifecycleDataItem) item, status);
            break;
          case IContentLifecycleManager _ when item is Content:
            relatedItem = ((IContentLifecycleManager) manager).GetRelatedItem((Content) item, status);
            break;
        }
      }
      return relatedItem;
    }

    /// <summary>
    /// Gets the item (live, temp or master) which is related with the specified item by lifecycle status.
    /// </summary>
    /// <param name="manager">The IContentLifecycleManager manager.</param>
    /// <param name="item">The item.</param>
    /// <param name="status">The status.</param>
    public static Content GetRelatedItem(
      this IContentLifecycleManager manager,
      Content item,
      ContentLifecycleStatus status)
    {
      if (status == ContentLifecycleStatus.Live)
        return manager.GetLive(item);
      return status == ContentLifecycleStatus.Temp ? manager.GetTemp(item) : (Content) null;
    }

    /// <summary>
    /// Gets the item (live, temp or master) which is related with the specified item by lifecycle status.
    /// </summary>
    /// <param name="manager">The ILifecycleManager manager.</param>
    /// <param name="item">The item.</param>
    /// <param name="status">The status.</param>
    /// <returns></returns>
    public static Content GetRelatedItem(
      this ILifecycleManager manager,
      ILifecycleDataItem item,
      ContentLifecycleStatus status)
    {
      if (status == ContentLifecycleStatus.Live)
        return manager.Lifecycle.GetLive(item) as Content;
      return status == ContentLifecycleStatus.Temp ? manager.Lifecycle.GetTemp(item) as Content : (Content) null;
    }

    /// <summary>
    /// Tries to load the language data for a specified culture
    /// In monolingual the culture is always invariant - empty string
    /// If no culture is specified takes the current UI from the thread
    /// </summary>
    /// <param name="dataItem">Data item</param>
    /// <param name="culture">Culture</param>
    public static LanguageData GetOrCreateLanguageData(
      ILifecycleDataItem dataItem,
      ILanguageDataManager manager,
      CultureInfo culture = null)
    {
      if (culture != null && culture.Equals((object) CultureInfo.InvariantCulture))
        culture = (CultureInfo) null;
      string languageKey = culture.GetLanguageKeyRaw();
      LanguageData languageData = dataItem.LanguageData.FirstOrDefault<LanguageData>((Func<LanguageData, bool>) (l => l.Language == languageKey));
      if (languageData == null)
      {
        languageData = manager.CreateLanguageData();
        languageData.Language = culture.GetLanguageKeyRaw();
        languageData.PublicationDate = DateTime.Now;
        dataItem.LanguageData.Add(languageData);
      }
      return languageData;
    }

    public static void UpgradePublishedTranslationsAndLanguageData<TItem, TManager>(
      SiteInitializer initializer,
      ContentModuleConfigBase moduleConfiguration)
      where TItem : Content, ILifecycleDataItem
      where TManager : IContentManager, ILifecycleManager
    {
      foreach (DataProviderSettings provider in (ConfigElementCollection) moduleConfiguration.Providers)
        LifecycleExtensions.UpgradeLifecycleItems<TItem, TManager>(initializer.GetManagerInTransaction<TManager>(provider.Name));
    }

    /// <summary>
    /// Creates language data and published translations records for all items
    /// </summary>
    public static void UpgradeLifecycleItems<TItem, TManager>(TManager manager)
      where TItem : Content, ILifecycleDataItem
      where TManager : IContentManager, ILifecycleManager
    {
      IQueryable<TItem> items = manager.GetItems<TItem>();
      bool multilingual = SystemManager.CurrentContext.AppSettings.Multilingual;
      CultureInfo frontendLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
      foreach (TItem obj in (IEnumerable<TItem>) items)
      {
        if (!obj.LanguageData.Any<LanguageData>())
        {
          LanguageData languageData1 = LifecycleExtensions.GetOrCreateLanguageData((ILifecycleDataItem) obj, (ILanguageDataManager) manager, CultureInfo.InvariantCulture);
          LifecycleExtensions.InitializeLanguageDataForCulture<TItem>(obj, languageData1);
          if (multilingual)
          {
            foreach (CultureInfo culture in ((IEnumerable<CultureInfo>) obj.GetAvailableCultures()).Where<CultureInfo>((Func<CultureInfo, bool>) (c => c.LCID != CultureInfo.InvariantCulture.LCID)))
            {
              LanguageData languageData2 = LifecycleExtensions.GetOrCreateLanguageData((ILifecycleDataItem) obj, (ILanguageDataManager) manager, culture);
              LifecycleExtensions.InitializeLanguageDataForCulture<TItem>(obj, languageData2);
              switch (obj.Status)
              {
                case ContentLifecycleStatus.Master:
                  if (obj.ContentState == "PUBLISHED")
                  {
                    obj.AddPublishedTranslation(culture.GetLanguageKey());
                    continue;
                  }
                  continue;
                case ContentLifecycleStatus.Temp:
                  obj.AddPublishedTranslation(frontendLanguage.GetLanguageKey());
                  continue;
                case ContentLifecycleStatus.Live:
                  if (obj.Visible)
                  {
                    obj.AddPublishedTranslation(culture.GetLanguageKey());
                    continue;
                  }
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Configures the language data item to reflect the obsolete Content Lifecycle implementation
    /// </summary>
    /// <param name="item">Content item</param>
    /// <param name="languageData">language data</param>
    public static void InitializeLanguageDataForCulture<TItem>(
      TItem item,
      LanguageData languageData)
      where TItem : Content, ILifecycleDataItem
    {
      languageData.LanguageVersion = item.Version;
      languageData.PublicationDate = item.PublicationDate;
      languageData.ExpirationDate = item.ExpirationDate;
      switch (item.Status)
      {
        case ContentLifecycleStatus.Master:
          if (item.ContentState == "PUBLISHED")
          {
            languageData.ContentState = LifecycleState.Published;
            break;
          }
          languageData.ContentState = LifecycleState.None;
          break;
        case ContentLifecycleStatus.Temp:
          languageData.ContentState = LifecycleState.None;
          break;
        case ContentLifecycleStatus.Live:
          if (item.Visible)
          {
            languageData.ContentState = LifecycleState.Published;
            break;
          }
          languageData.ContentState = LifecycleState.None;
          break;
      }
    }

    public static void UpgradePublishedTranslationsAndLanguageData<TItem, TManager>(
      SiteInitializer initializer,
      IEnumerable<DataProviderSettings> providers)
      where TItem : class, IContent, ILifecycleDataItemLive
      where TManager : ILanguageDataManager, IContentManager
    {
      foreach (DataProviderSettings provider in providers)
      {
        TManager managerInTransaction = initializer.GetManagerInTransaction<TManager>(provider.Name);
        IQueryable<TItem> items = managerInTransaction.GetItems<TItem>();
        LifecycleExtensions.UpgradeLiveLifecycleItems<TItem>((ILanguageDataManager) managerInTransaction, items);
      }
    }

    /// <summary>
    /// Creates language data and published translations records for all items
    /// </summary>
    public static void UpgradeLiveLifecycleItems<TItem>(
      ILanguageDataManager manager,
      IQueryable<TItem> itemsToUpgrade)
      where TItem : class, ILifecycleDataItemLive
    {
      foreach (TItem obj in (IEnumerable<TItem>) itemsToUpgrade)
      {
        if (!obj.LanguageData.Any<LanguageData>())
        {
          CultureInfo[] availableCultures = obj.AvailableCultures;
          LifecycleExtensions.UpgradeLifecycleItem(manager, (ILifecycleDataItem) obj, availableCultures);
          foreach (ILifecycleDataItemDraft draft in obj.Drafts)
            LifecycleExtensions.UpgradeLifecycleItem(manager, (ILifecycleDataItem) draft, availableCultures);
        }
      }
    }

    internal static void UpgradeLifecycleItem(
      ILanguageDataManager manager,
      ILifecycleDataItem item,
      CultureInfo[] availableCultures)
    {
      int num = SystemManager.CurrentContext.AppSettings.Multilingual ? 1 : 0;
      LanguageData languageData1 = LifecycleExtensions.GetOrCreateLanguageData(item, manager);
      if (num != 0)
      {
        LanguageData sourceLanguageData = (LanguageData) null;
        CultureInfo languageForObject = LocalizationHelper.GetDefaultLanguageForObject((object) item);
        if (availableCultures.Length == 0)
          availableCultures = new CultureInfo[1]
          {
            languageForObject
          };
        foreach (CultureInfo availableCulture in availableCultures)
        {
          if (!availableCulture.Equals((object) CultureInfo.InvariantCulture))
          {
            LanguageData languageData2 = LifecycleExtensions.GetOrCreateLanguageData(item, manager, availableCulture);
            LifecycleExtensions.InitializeLiveItemLanguageDataForCulture(item, languageData2);
            if (languageData2.ContentState == LifecycleState.Published)
              item.AddPublishedTranslation(availableCulture.GetLanguageKey());
            if (sourceLanguageData == null || languageForObject.Equals((object) availableCulture))
              sourceLanguageData = languageData2;
          }
        }
        languageData1.CopyFrom(sourceLanguageData);
      }
      else
        LifecycleExtensions.InitializeLiveItemLanguageDataForCulture(item, languageData1);
    }

    /// <summary>
    /// Configures the language data item to reflect the obsolete Content Lifecycle implementation
    /// </summary>
    /// <param name="item">Content item</param>
    /// <param name="languageData">language data</param>
    public static void InitializeLiveItemLanguageDataForCulture(
      ILifecycleDataItem item,
      LanguageData languageData)
    {
      languageData.LanguageVersion = item.Version;
      if (item is Content)
      {
        Content content = (Content) item;
        languageData.PublicationDate = content.PublicationDate;
        languageData.ExpirationDate = content.ExpirationDate;
      }
      PageData pageData = item as PageData;
      PageTemplate pageTemplate = item as PageTemplate;
      if (pageData != null && pageData.NavigationNode != null && pageData.NavigationNode.IsBackend || pageTemplate != null && pageTemplate.IsBackend || item.Status == ContentLifecycleStatus.Live && item.Visible)
        languageData.ContentState = LifecycleState.Published;
      else
        languageData.ContentState = LifecycleState.None;
    }

    /// <summary>
    /// Returns whether the item is published. Works in both monolingual and multilingual configuration. In monolingual the CultureInfo
    /// parameter is not used.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The language to check. Only used in multilingual.</param>
    /// <returns></returns>
    public static bool IsPublished(this ILifecycleDataItemLive item, CultureInfo culture = null) => new LifecycleStatusItemLiveWraper(item).IsPublished(culture);

    internal static bool IsPublished(this ILifecycleStatusItemLive item, CultureInfo culture = null)
    {
      if (item == null)
        return false;
      culture = culture.GetSitefinityCulture();
      if (item.PublishedTranslations.Count != 0)
        return item.PublishedTranslations.Contains(culture.Name);
      CultureInfo languageForObject = LocalizationHelper.GetDefaultLanguageForObject((object) item);
      if (culture == null || !languageForObject.Equals((object) culture))
        return false;
      ILanguageData languageData = item.GetLanguageData(culture);
      return languageData != null && languageData.ContentState == LifecycleState.Published;
    }

    /// <summary>
    /// Returns status information for the specified item. Handles Draft, Published, Draft (newer than published), Locked and Unpublished
    /// statuses. If the status key is already set to something, only (newer than published) and Locked is applied to it, when necessary.
    /// </summary>
    /// <param name="liveItem">The item to get status information for.</param>
    /// <param name="culture">The culture to get status information for.</param>
    /// <param name="statusKey">This is a key used to represent the status.</param>
    /// <param name="statusText">This is a localized representation of the status of the item.</param>
    public static void GetOverallStatus(
      ILifecycleDataItemLive liveItem,
      CultureInfo culture,
      ref string statusKey,
      ref string statusText)
    {
      LifecycleExtensions.GetOverallStatus((ILifecycleStatusItemLive) new LifecycleStatusItemLiveWraper(liveItem), culture, ref statusKey, ref statusText);
    }

    internal static void GetOverallStatus(
      ILifecycleStatusItemLive liveItem,
      CultureInfo culture,
      ref string statusKey,
      ref string statusText)
    {
      LifecycleExtensions.GetLifecycleStatus(liveItem, culture, ref statusKey, ref statusText);
      if (!(liveItem.LockedBy != Guid.Empty))
        return;
      statusKey = LifecycleExtensions.StatusLocked;
      statusText = string.Format(Res.Get<Labels>().LockedByFormat, (object) UserProfilesHelper.GetUserDisplayName(liveItem.LockedBy), (object) statusText);
    }

    /// <summary>
    /// Returns status information for the specified item. Handles Draft, Published, Draft (newer than published) and Unpublished
    /// statuses.
    /// </summary>
    /// <param name="liveItem">The item to get status information for.</param>
    /// <param name="culture">The culture to get status information for.</param>
    /// <param name="statusKey">This is a key used to represent the status.</param>
    /// <param name="statusText">This is a localized representation of the status of the item.</param>
    public static void GetLifecycleStatus(
      ILifecycleDataItemLive liveItem,
      CultureInfo culture,
      ref string statusKey,
      ref string statusText)
    {
      LifecycleExtensions.GetLifecycleStatus((ILifecycleStatusItemLive) new LifecycleStatusItemLiveWraper(liveItem), culture, ref statusKey, ref statusText);
    }

    internal static void GetLifecycleStatus(
      ILifecycleStatusItemLive liveItem,
      CultureInfo culture,
      ref string statusKey,
      ref string statusText)
    {
      bool flag1 = !string.IsNullOrEmpty(statusKey);
      if (liveItem.IsPublished(culture))
      {
        if (liveItem.HasDraftNewerThanPublished(culture))
        {
          if (string.IsNullOrEmpty(statusText))
          {
            statusKey = LifecycleExtensions.StatusNewerThanPublished;
            statusText = Res.Get("PageResources", LifecycleExtensions.StatusDraft);
          }
          statusText = string.Format("{0} {1}", (object) statusText, (object) Res.Get<Labels>().NewerThanPublishedInParentheses);
        }
        else if (!flag1)
          statusKey = LifecycleExtensions.StatusPublished;
      }
      else if (!flag1)
      {
        bool flag2 = false;
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
        {
          ILanguageData languageData = liveItem.GetLanguageData(culture);
          if (languageData != null && languageData.LanguageVersion > 0)
            flag2 = true;
        }
        else if (liveItem.Version > 0)
          flag2 = true;
        statusKey = flag2 ? LifecycleExtensions.StatusUnpublished : LifecycleExtensions.StatusDraft;
      }
      if (!string.IsNullOrEmpty(statusText))
        return;
      statusText = Res.Get("PageResources", statusKey.ToString());
    }

    /// <summary>
    /// Checks whether changes should be applied to invariant language data of the item
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>If the culture is the default multilingual culture and the item is not temp then returns true else false</returns>
    public static bool ShouldApplyChangesToInvariant(
      this ILifecycleDataItem item,
      CultureInfo culture)
    {
      CultureInfo languageForObject = LocalizationHelper.GetDefaultLanguageForObject((object) item);
      return SystemManager.CurrentContext.AppSettings.Multilingual && culture.Equals((object) languageForObject) && item.Status != ContentLifecycleStatus.Temp;
    }

    /// <summary>Copies the language data properties.</summary>
    /// <param name="sourceData">The source data.</param>
    /// <param name="destinationData">The destination data.</param>
    public static void CopyLanguageDataProperties(
      this LanguageData sourceData,
      LanguageData destinationData)
    {
      destinationData.ApplicationName = sourceData.ApplicationName;
      destinationData.ExpirationDate = sourceData.ExpirationDate;
      destinationData.PublicationDate = sourceData.PublicationDate;
      destinationData.ScheduledDate = sourceData.ScheduledDate;
      destinationData.ContentState = sourceData.ContentState;
      destinationData.LanguageVersion = sourceData.LanguageVersion;
    }

    /// <summary>
    /// Copies from source language data to invariant language data.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="culture">The culture.</param>
    public static void CopyToInvariantLanguageData(
      this ILifecycleDataItem source,
      ILanguageDataManager manager,
      CultureInfo culture)
    {
      LanguageData languageData1 = source.GetLanguageData(culture);
      LanguageData languageData2 = LifecycleExtensions.GetOrCreateLanguageData(source, manager);
      if (languageData1 == null)
        return;
      languageData1.CopyLanguageDataProperties(languageData2);
      languageData2.Language = (string) null;
    }

    /// <summary>
    /// Increments the language version of the item, if the item is of type Content then increments it's version
    /// If the culture is the default multilingual culture then increments the invariant language version too
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="culture">The culture.</param>
    public static void IncrementLanguageVersion(
      this ILifecycleDataItem item,
      ILanguageDataManager manager,
      CultureInfo culture)
    {
      ++LifecycleExtensions.GetOrCreateLanguageData(item, manager, culture).LanguageVersion;
      ++item.Version;
    }

    /// <summary>Determines whether the specified item is published.</summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>
    ///   <c>true</c> if the specified item is published; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPublished(this ILifecycleDataItem item, CultureInfo culture = null)
    {
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (!item.Visible)
          return false;
        return item.LanguageData.Count == 1 && item.PublishedTranslations.Count == 0 && item.LanguageData.FirstOrDefault<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == null && ld.ContentState == LifecycleState.Published)) != null && SystemManager.CurrentContext.Culture.Equals((object) SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage) || item.PublishedTranslations.Contains(culture.GetLanguageKey());
      }
      return item.Visible && item.LanguageData.FirstOrDefault<LanguageData>((Func<LanguageData, bool>) (ld => (ld.Language == null || ld.Language == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name) && ld.ContentState == LifecycleState.Published)) != null;
    }

    /// <summary>
    /// Copies the scheduled dates (publication, expiration date) from temp to the master LanguageData.
    /// </summary>
    /// <param name="master">The master.</param>
    /// <param name="temp">The temp.</param>
    /// <param name="culture">The culture.</param>
    public static void CopyScheduledDates(
      this ILifecycleDataItem master,
      IScheduleable temp,
      CultureInfo culture)
    {
      LanguageData languageData = master.GetLanguageData(culture);
      languageData.ExpirationDate = temp.ExpirationDate;
      languageData.PublicationDate = temp.PublicationDate;
      if (!master.ShouldApplyChangesToInvariant(culture))
        return;
      LanguageData languageDataRaw = master.GetLanguageDataRaw((CultureInfo) null);
      if (languageDataRaw == null)
        return;
      languageDataRaw.ExpirationDate = temp.ExpirationDate;
      languageDataRaw.PublicationDate = temp.PublicationDate;
    }

    /// <summary>
    /// Adds the specified culture to the published translations of the item.
    /// If the specifies culture is null the current UI culture from the thread will be used.
    /// </summary>
    /// <param name="lifecycleItem">content item</param>
    /// <param name="culture">culture</param>
    public static void AddPublishedTranslation(
      this ILifecycleDataItemGeneric lifecycleItem,
      CultureInfo culture)
    {
      LifecycleExtensionsModel.AddPublishedTranslation(lifecycleItem, culture);
    }

    /// <summary>
    /// Adds the specified culture to the published translations of the item.
    /// If the specifies culture is null the current UI culture from the thread will be used.
    /// </summary>
    /// <param name="lifecycleItem">content item</param>
    /// <param name="culture">culture</param>
    public static void RemovePublishedTranslation(
      this ILifecycleDataItemGeneric lifecycleItem,
      CultureInfo culture)
    {
      LifecycleExtensionsModel.RemovePublishedTranslation(lifecycleItem, culture);
    }

    /// <summary>
    /// Gets the language data corresponding to the specified culture
    /// In monolingual it will load the invariant culture, in multilingual will load the one for the specified culture (or the current UI culture if null)
    /// </summary>
    public static LanguageData GetLanguageData(
      this ILifecycleDataItem item,
      CultureInfo culture)
    {
      return LifecycleExtensionsModel.GetLanguageData(item, culture);
    }

    /// <summary>
    /// Gets the language data corresponding to the specified culture
    /// </summary>
    public static LanguageData GetLanguageDataRaw(
      this ILifecycleDataItem item,
      CultureInfo culture)
    {
      return LifecycleExtensionsModel.GetLanguageDataRaw(item, culture);
    }

    /// <summary>
    /// Gets the UI status of the ILifecycleDataItemGeneric item
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static ContentUIStatus GetUIStatus(
      this ILifecycleDataItem item,
      CultureInfo culture = null)
    {
      return LifecycleExtensionsModel.GetUIStatus(item, culture);
    }

    /// <summary>
    /// Sets the UI status of a content object depending on whether this object also implements ILifecycleDataItemGeneric
    /// </summary>
    /// <param name="item">The item.</param>
    public static void SetUIStatus(this object item) => LifecycleExtensionsModel.SetUIStatus(item);

    /// <summary>
    /// Gets the UI status of a content object depending on whether this object also implements ILifecycleDataItemGeneric
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public static ContentUIStatus GetUIStatus(this object item) => LifecycleExtensionsModel.GetUIStatus(item);

    /// <summary>
    /// Returns whether the specified live item has a draft newer than the published item (changed after the publishing).
    /// </summary>
    /// <param name="liveItem">The live item instance.</param>
    /// <param name="culture">The culture to check.</param>
    /// <returns></returns>
    public static bool HasDraftNewerThanPublished(
      this ILifecycleDataItemLive liveItem,
      CultureInfo culture)
    {
      return LifecycleExtensionsModel.HasDraftNewerThanPublished(liveItem, culture);
    }

    /// <summary>
    /// Returns whether item is newer than its corresponding live item.
    /// </summary>
    /// <param name="masterItem">The master item.</param>
    /// <param name="culture">The culture to check.</param>
    /// <returns></returns>
    public static bool IsNewerThanPublished(
      this ILifecycleDataItemDraft masterItem,
      CultureInfo culture)
    {
      return LifecycleExtensionsModel.IsNewerThanPublished(masterItem, culture);
    }

    public static ILifecycleDataItemDraft GetMasterItem(
      this ILifecycleDataItemLive liveItem)
    {
      return LifecycleExtensionsModel.GetMasterItem(liveItem);
    }

    public static ILifecycleDataItemDraft GetTempItem(
      this ILifecycleDataItemLive liveItem)
    {
      return LifecycleExtensionsModel.GetTempItem(liveItem);
    }

    /// <summary>
    /// Determines whether the item is newer than the live item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="live">The live item.</param>
    /// <param name="culture">The culture.</param>
    public static bool IsNewer(
      this ILifecycleDataItem first,
      ILifecycleDataItem second,
      CultureInfo culture = null)
    {
      return LifecycleExtensionsModel.IsNewer(first, second, culture);
    }

    /// <summary>Gets the language version.</summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>For items implementing returns the language version for the specified culture. For items which do not implement ILifecycleDataItem
    /// and which implement Content returns the item version. Otherwise returns -1</returns>
    public static int GetLanguageVersion(this object item, CultureInfo culture = null) => LifecycleExtensionsModel.GetLanguageVersion(item, culture);

    /// <summary>
    /// Determines whether the specified item is not null and if the item implements ILifecycleDataItem,
    /// determines whether the item has published translation for the specified culture
    /// </summary>
    [Obsolete("Use IsPublishedInCulture")]
    public static bool IsAvailableForCulture(this ILifecycleDataItem item, CultureInfo culture = null) => LifecycleExtensionsModel.IsPublishedInCulture(item, culture);

    /// <summary>
    /// Determines whether the item has published translation for the specified culture. When the specified culture is null
    /// the current UI culture is used.
    /// </summary>
    public static bool IsPublishedInCulture(this ILifecycleDataItem item, CultureInfo culture = null) => LifecycleExtensionsModel.IsPublishedInCulture(item, culture);

    /// <summary>
    /// Determines whether the item has published translation for the specified culture. When the specified culture is null
    /// the it is considered to be the invariant culture.
    /// </summary>
    public static bool IsPublishedInCultureRaw(this ILifecycleDataItem item, CultureInfo culture) => LifecycleExtensionsModel.IsPublishedInCultureRaw(item, culture);

    /// <summary>
    /// If the specified item implements ILifecycleDataItem, then determines whether the item has published translation for the specified culture. When the specified culture is null
    /// the current UI culture is used, otherwise returns true (for backward competability)
    /// </summary>
    public static bool IsPublishedInCulture(this Content item, CultureInfo culture = null) => !(item is ILifecycleDataItem) || ((ILifecycleDataItem) item).IsPublishedInCulture(culture);

    /// <summary>
    /// If the specified item implements ILifecycleDataItem, then determines whether the item has published translation for the specified culture.When the specified culture is null
    /// the it is considered to be the invariant culture, otherwise returns true (for backward competability)
    /// </summary>
    public static bool IsPublishedInCultureRaw(this Content item, CultureInfo culture)
    {
      if (item is ILifecycleDataItem)
        return ((ILifecycleDataItem) item).IsPublishedInCultureRaw(culture);
      return item != null && item.Visible;
    }

    /// <summary>
    /// Gets the UI status of the ILifecycleDataItemGeneric item
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    public static ContentUIStatus GetUIStatusRaw(
      this ILifecycleDataItem item,
      CultureInfo culture = null)
    {
      return LifecycleExtensionsModel.GetUIStatusRaw(item, culture);
    }

    public static ContentUIStatus GetUIStatusRaw(
      this object item,
      CultureInfo culture = null)
    {
      return LifecycleExtensionsModel.GetUIStatusRaw(item, culture);
    }

    /// <summary>Gets the language version for the specified culture.</summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>For items implementing ILifecycleDataItem returns the language version for the specified culture. For items which do not implement ILifecycleDataItem
    /// and which implement Content returns the item version. Otherwise returns -1</returns>
    public static int GetLanguageVersionRaw(this object item, CultureInfo culture) => LifecycleExtensionsModel.GetLanguageVersionRaw(item, culture);

    internal static int GetLanguageVersionWithFallbackForDefault(
      this object item,
      CultureInfo culture)
    {
      int result = -1;
      using (new CultureRegion(culture))
        CommonMethods.ExecuteMlLogic<object>((Action<object>) (itemInner => result = item.GetLanguageVersionRaw((CultureInfo) null)), (Action<object>) (itemInner =>
        {
          result = item.GetLanguageVersionRaw(culture);
          if (result != -1)
            return;
          result = item.GetLanguageVersionRaw((CultureInfo) null);
        }), (Action<object>) (itemInner => result = item.GetLanguageVersionRaw(culture)), item);
      return result;
    }

    /// <summary>
    /// Adds the language to the collection of the published translation of the item,
    /// synchronizing the published translation of split pages when the item is a split page
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="languageKey">The language key.</param>
    public static void AddPublishedTranslation(this ILifecycleDataItem item, string languageKey) => LifecycleExtensionsModel.AddPublishedTranslation(item, languageKey);

    /// <summary>
    /// Removes the specified language from the published translations list,
    /// synchronizing the published translation of split pages when the item is a split page
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="languageKey">The language key.</param>
    public static void RemovePublishedTranslation(this ILifecycleDataItem item, string languageKey) => LifecycleExtensionsModel.RemovePublishedTranslation(item, languageKey);

    /// <summary>
    /// Synchronizes the published translation with the specified culture list
    /// </summary>
    /// <param name="pageLinks">The page links.</param>
    /// <param name="availableCultures">The available cultures.</param>
    [Obsolete("There is no need to synchronize published translations in versions 7.0+")]
    public static void SynchronizePublishedTranslation(
      IList<PageData> pageLinks,
      List<string> availableCultures)
    {
    }

    /// <summary>Gets lifecycle status info for ILifecycleDataItem.</summary>
    /// <param name="item">The ILifecycleDataItem.</param>
    /// <param name="manager">The manager to be used.</param>
    /// <param name="culture">The culture info.</param>
    internal static LifecycleSimpleInfo GetLifecycleSimpleInfo(
      this ILifecycleDataItem item,
      ILifecycleManager manager,
      CultureInfo culture)
    {
      ILifecycleDataItem live = manager.Lifecycle.GetLive(item, culture);
      ILifecycleDataItem temp = manager.Lifecycle.GetTemp(item, culture);
      ILifecycleDataItem master = manager.Lifecycle.GetMaster(item);
      LifecycleSimpleInfo lifecycleSimpleInfo = new LifecycleSimpleInfo();
      if (master is IApprovalWorkflowItem workflowItem)
      {
        string statusKey;
        lifecycleSimpleInfo.DisplayStatus = workflowItem.GetLocalizedStatus(out statusKey, out IStatusInfo _, culture);
        lifecycleSimpleInfo.WorkflowStatus = statusKey;
      }
      if (lifecycleSimpleInfo.WorkflowStatus.IsNullOrWhitespace())
        lifecycleSimpleInfo.WorkflowStatus = Res.Get<ContentLifecycleMessages>().Draft;
      if (live != null && live.Visible && live.IsPublished() && master.IsNewer(live))
        lifecycleSimpleInfo.DisplayStatus = string.Format("{0} {1}", (object) lifecycleSimpleInfo.DisplayStatus, (object) Res.Get<Labels>().NewerThanPublishedInParentheses);
      if (temp != null && temp.Owner != Guid.Empty)
      {
        lifecycleSimpleInfo.WorkflowStatus = ContentUIStatus.PrivateCopy.ToString();
        lifecycleSimpleInfo.DisplayStatus = string.Format(Res.Get<Labels>().LockedByFormat, (object) CommonMethods.GetUserName(temp.Owner), (object) lifecycleSimpleInfo.DisplayStatus);
        lifecycleSimpleInfo.IsLocked = true;
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        lifecycleSimpleInfo.IsLockedByMe = temp.Owner == currentIdentity.UserId;
      }
      return lifecycleSimpleInfo;
    }

    /// <summary>Executes an action for all versions of an item.</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="item">The item.</param>
    /// <param name="action">The action.</param>
    internal static void DoForAllVersions(
      this ILifecycleManager manager,
      ILifecycleDataItem item,
      Action<ILifecycleDataItem> action)
    {
      ILifecycleDecorator lifecycle = manager.Lifecycle;
      ILifecycleDataItem master = lifecycle.GetMaster(item);
      if (master != null)
        action(master);
      ILifecycleDataItem temp = lifecycle.GetTemp(item);
      if (temp != null)
        action(temp);
      ILifecycleDataItem live = lifecycle.GetLive(item);
      if (live == null)
        return;
      action(live);
    }

    internal static IQueryable<T> EnhanceQueryToFilterPublished<T>(
      this IQueryable<T> query,
      CultureInfo culture)
      where T : ILifecycleDataItem
    {
      IQueryable<T> ret = query;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CommonMethods.ExecuteMlLogic<IQueryable<T>>((Action<IQueryable<T>>) (subQuery => ret = subQuery.Where<T>((Expression<Func<T, bool>>) (x => x.Visible))), (Action<IQueryable<T>>) (subQuery => ret = subQuery.Where<T>((Expression<Func<T, bool>>) (x => x.PublishedTranslations.Count == 0 && x.Visible || x.PublishedTranslations.Contains(this.culture.Name)))), (Action<IQueryable<T>>) (subQuery => ret = subQuery.Where<T>((Expression<Func<T, bool>>) (x => x.PublishedTranslations.Contains(this.culture.Name)))), query);
      return ret;
    }

    /// <summary>
    /// Use this after upgrading from monolingual to multilingual.
    /// The Visible property is false for the master items so we are getting the live ones.
    /// </summary>
    internal static bool IsItemPublished<TItem>(TItem item, IManager manager, CultureInfo culture) where TItem : ILifecycleDataItem
    {
      if (item.Status != ContentLifecycleStatus.Live)
      {
        ILifecycleDataItem live = (manager as ILifecycleManager).Lifecycle.GetLive((ILifecycleDataItem) item, culture);
        if (live != null)
          return LifecycleExtensions.IsItemPublished<ILifecycleDataItem>(live, culture);
      }
      return false;
    }

    /// <summary>Checks if the item has ever been published.</summary>
    internal static bool WasItemPublished<TItem>(TItem item, IManager manager, CultureInfo culture) where TItem : ILifecycleDataItem
    {
      bool flag = false;
      ILifecycleDataItem lifecycleDataItem = item.Status != ContentLifecycleStatus.Live ? (manager as ILifecycleManager).Lifecycle.GetLive((ILifecycleDataItem) item, culture) : (ILifecycleDataItem) item;
      if (lifecycleDataItem != null)
      {
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
        {
          LanguageData languageData = lifecycleDataItem.GetLanguageData(culture);
          if (languageData != null && languageData.LanguageVersion > 0 || SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage == culture && lifecycleDataItem.LanguageData.Count == 1 && lifecycleDataItem.Version > 0)
            flag = true;
        }
        else if (lifecycleDataItem.Version > 0)
          flag = true;
      }
      return flag;
    }

    public static bool IsItemPublished<TItem>(TItem item, CultureInfo culture) where TItem : ILifecycleDataItem => ((IEnumerable<ILifecycleDataItem>) new ILifecycleDataItem[1]
    {
      (ILifecycleDataItem) item
    }).AsQueryable<ILifecycleDataItem>().EnhanceQueryToFilterPublished<ILifecycleDataItem>(culture).FirstOrDefault<ILifecycleDataItem>() != null;

    internal static bool IsItemPublishedInDefaultLang<TItem>(TItem item) where TItem : ILifecycleDataItemGeneric
    {
      CultureInfo frontendLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
      return item.PublishedTranslations.Count == 0 && item.Visible || item.PublishedTranslations.Contains(frontendLanguage.Name);
    }
  }
}
