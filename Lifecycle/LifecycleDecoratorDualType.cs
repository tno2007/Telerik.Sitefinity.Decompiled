// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleDecorator`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Lifecycle
{
  /// <summary>
  /// Lifecycle decorator for pages,forms,templates, etc - all types that has separate items for draft versions
  /// </summary>
  /// <typeparam name="TItem"></typeparam>
  /// <typeparam name="TDraft"></typeparam>
  public abstract class LifecycleDecorator<TItem, TDraft> : 
    ILifecycleDecorator<TItem, TDraft>,
    ILifecycleDecorator
    where TItem : class, ILifecycleDataItemLive
    where TDraft : class, ILifecycleDataItemDraft
  {
    public LifecycleDecorator(ILifecycleManager<TItem, TDraft> manager) => this.Manager = manager;

    protected virtual ILifecycleManager<TItem, TDraft> Manager { get; private set; }

    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="item">Item in temp state that is to be checked in.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>An item in master state.</returns>
    ILifecycleDataItem ILifecycleDecorator.CheckIn(
      ILifecycleDataItem item,
      CultureInfo culture,
      bool deleteTemp = false)
    {
      return (ILifecycleDataItem) this.CheckIn((TDraft) item, culture, deleteTemp);
    }

    /// <summary>
    /// Checks out the content in master state. Item becomes temp after the check out.
    /// </summary>
    /// <param name="item">Item in master state that is to be checked out.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was checked out in temp state.</returns>
    ILifecycleDataItem ILifecycleDecorator.CheckOut(
      ILifecycleDataItem item,
      CultureInfo culture)
    {
      return (ILifecycleDataItem) this.CheckOut((TDraft) item, culture);
    }

    /// <summary>
    /// Edits the content in live state. Item becomes master after the edit.
    /// </summary>
    /// <param name="item">Item in live state that is to be edited.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was edited in master state.</returns>
    ILifecycleDataItem ILifecycleDecorator.Edit(
      ILifecycleDataItem item,
      CultureInfo culture)
    {
      return (ILifecycleDataItem) this.Edit((TItem) item, culture);
    }

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish.
    /// </summary>
    /// <param name="item">Item in master state that is to be published.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Published content item</returns>
    ILifecycleDataItem ILifecycleDecorator.Publish(
      ILifecycleDataItem item,
      CultureInfo culture)
    {
      return (ILifecycleDataItem) this.Publish((TDraft) item, culture);
    }

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish with the specified publication date.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="publicationDate">The publication date.</param>
    /// <param name="culture">The culture in which to perform the operation.</param>
    /// <returns>Published content item</returns>
    public ILifecycleDataItem PublishWithSpecificDate(
      ILifecycleDataItem item,
      DateTime publicationDate,
      CultureInfo culture = null)
    {
      return (ILifecycleDataItem) this.Publish((TDraft) item, culture);
    }

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Master (draft) state.</returns>
    ILifecycleDataItem ILifecycleDecorator.Unpublish(
      ILifecycleDataItem item,
      CultureInfo culture)
    {
      return (ILifecycleDataItem) this.Unpublish((TItem) item, culture);
    }

    /// <summary>
    /// Returns ID of the user that checked out the master item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="culture"></param>
    /// <returns>
    /// ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    Guid ILifecycleDecorator.GetCheckedOutBy(
      ILifecycleDataItem item,
      CultureInfo culture)
    {
      return this.GetCheckedOutBy((TItem) item, culture);
    }

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    bool ILifecycleDecorator.IsCheckedOut(
      ILifecycleDataItem item,
      CultureInfo culture)
    {
      return this.IsCheckedOut((TItem) item, culture);
    }

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// 
    ///             ///
    ///             <returns>
    /// True if it was checked out by a user with the specified id, false otherwize
    /// </returns>
    bool ILifecycleDecorator.IsCheckedOutBy(
      ILifecycleDataItem item,
      Guid userId,
      CultureInfo culture)
    {
      return this.IsCheckedOutBy((TItem) item, userId, culture);
    }

    /// <summary>
    /// Discards the temp item in the specified culture for the specified master item. All changes in
    /// the discarded item are lost.
    /// </summary>
    /// <param name="masterItem"></param>
    /// <param name="culture">The culture of the temp item to discard.</param>
    void ILifecycleDecorator.DiscardTemp(
      ILifecycleDataItem masterItem,
      CultureInfo culture = null)
    {
      this.DiscardTemp((TDraft) masterItem, culture);
    }

    /// <summary>
    /// Discards all temp item items for the specified master item. All changes in the discarded items
    /// are lost.
    /// </summary>
    /// <param name="masterItem"></param>
    void ILifecycleDecorator.DiscardAllTemps(ILifecycleDataItem masterItem) => this.DiscardAllTemps((TDraft) masterItem);

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    ILifecycleDataItem ILifecycleDecorator.GetLive(
      ILifecycleDataItem cnt,
      CultureInfo culture = null)
    {
      return cnt is TItem obj ? (ILifecycleDataItem) obj : (ILifecycleDataItem) (cnt as TDraft).ParentItem;
    }

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Item to get a temp for</param>
    /// <param name="culture"></param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    ILifecycleDataItem ILifecycleDecorator.GetTemp(
      ILifecycleDataItem cnt,
      CultureInfo culture = null)
    {
      return (object) (cnt as TDraft) != null ? (ILifecycleDataItem) this.GetTemp((TDraft) cnt) : (ILifecycleDataItem) this.GetTemp((TItem) cnt);
    }

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <param name="cnt">Item item whose master to get</param>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// Otherwize, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    ILifecycleDataItem ILifecycleDecorator.GetMaster(
      ILifecycleDataItem cnt)
    {
      return cnt is TDraft draftItem ? (ILifecycleDataItem) this.GetMaster(draftItem) : (ILifecycleDataItem) this.GetMaster(cnt as TItem);
    }

    /// <summary>
    /// Copies the source item properties to the destination item ones
    /// </summary>
    /// <param name="source">source item</param>
    /// <param name="destination">destination item</param>
    /// <param name="culture">If no culture is specified the current thread UI culture will be used in multilingual mode</param>
    public void CopyProperties(
      ILifecycleDataItem source,
      ILifecycleDataItem destination,
      CultureInfo culture = null)
    {
      TItem liveItem1 = source as TItem;
      TDraft draft1 = default (TDraft);
      if ((object) liveItem1 == null)
        draft1 = (TDraft) source;
      TItem liveItem2 = destination as TItem;
      TDraft draft2 = default (TDraft);
      if ((object) liveItem2 == null)
        draft2 = (TDraft) destination;
      if ((object) liveItem1 != null && (object) draft2 != null)
        this.Copy(liveItem1, draft2, culture);
      else if ((object) draft1 != null && (object) liveItem2 != null)
      {
        this.Copy(draft1, liveItem2, culture);
      }
      else
      {
        if ((object) draft1 == null || (object) draft2 == null)
          throw new InvalidOperationException("This implementation does not support copying from live to live.");
        this.Copy(draft1, draft2, culture);
      }
    }

    /// <summary>
    /// Gets the or create language data for the specified item in the specified culture
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public LanguageData GetOrCreateLanguageData(
      ILifecycleDataItem dataItem,
      CultureInfo culture)
    {
      return LifecycleExtensions.GetOrCreateLanguageData(dataItem, (ILanguageDataManager) this.Manager, culture);
    }

    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="temp"></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>An item in master state.</returns>
    public virtual TDraft CheckIn(TDraft temp, CultureInfo culture = null, bool deleteTemp = true)
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      if (this.Manager.Provider.SuppressSecurityChecks && currentUserId == Guid.Empty)
      {
        if (temp.Owner != Guid.Empty)
          throw new InvalidOperationException("The item is locked by another user. Most likely you are trying to modify the item by anonymous user, which is not allowed when the item is locked");
      }
      else
      {
        if (currentUserId == Guid.Empty)
          throw new UnauthorizedAccessException("Unauthenticated request. Most likely your session has expired. Please login and try again.");
        if (temp.Owner != currentUserId)
        {
          if (temp.Owner == Guid.Empty)
            throw new InvalidOperationException(Res.Get<ErrorMessages>().PageModifiedBySomeoneElse);
          User user = SecurityManager.GetUser(temp.Owner);
          throw new InvalidOperationException(Res.Get<ErrorMessages>().ItemIsLocked.Arrange((object) user));
        }
      }
      culture = culture.GetSitefinityCulture();
      TDraft master = this.GetMaster(temp);
      this.Copy(temp, master, culture);
      temp.ParentItem.Status = ContentLifecycleStatus.Master;
      master.IncrementLanguageVersion((ILanguageDataManager) this.Manager, culture);
      master.LastModified = DateTime.UtcNow;
      master.Owner = temp.Owner;
      if ((object) temp is IContent)
        master.CopyScheduledDates((IScheduleable) (object) temp, culture);
      if (deleteTemp)
      {
        this.DiscardAllTemps(master);
        master.ParentItem.LockedBy = Guid.Empty;
      }
      return master;
    }

    /// <summary>
    /// Checks out the content in master state. Item becomes temp after the check out.
    /// </summary>
    /// <param name="masterItem"></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was checked out in temp state.</returns>
    public virtual TDraft CheckOut(TDraft masterItem, CultureInfo culture = null)
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      TDraft draft = this.GetTemp(masterItem);
      TItem parentItem1 = (TItem) masterItem.ParentItem;
      if ((object) draft != null)
      {
        if (parentItem1.LockedBy != Guid.Empty && parentItem1.LockedBy != currentUserId)
        {
          if (!this.CanUnlockItem(parentItem1, draft, currentUserId))
          {
            User user = SecurityManager.GetUser(parentItem1.LockedBy);
            throw new InvalidOperationException(Res.Get<ErrorMessages>().PageIsLocked.Arrange((object) user.UserName));
          }
          this.DiscardTemp(draft);
          draft = default (TDraft);
        }
        else
        {
          if (draft.Owner != currentUserId)
            draft.Owner = currentUserId;
          if (parentItem1.LockedBy == Guid.Empty && this.CopyDuringCheckOut(masterItem))
            this.Copy(masterItem, draft, culture);
        }
      }
      if (parentItem1.LockedBy != currentUserId)
        parentItem1.LockedBy = currentUserId;
      ISecuredObject parentItem2 = masterItem.ParentItem as ISecuredObject;
      if ((object) draft == null)
      {
        if (!this.Manager.Provider.SuppressSecurityChecks && parentItem2 != null && !parentItem2.IsSecurityActionTypeGranted(SecurityActionTypes.Modify))
          throw new UnauthorizedAccessException("You do not have permissions to modify this item.");
        bool suppressSecurityChecks = this.Manager.Provider.SuppressSecurityChecks;
        try
        {
          this.Manager.Provider.SuppressSecurityChecks = true;
          draft = this.Manager.CreateDraft();
          draft.IsTempDraft = true;
          draft.ParentItem = (ILifecycleDataItemLive) parentItem1;
        }
        finally
        {
          this.Manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        }
        this.Copy(masterItem, draft, culture);
      }
      else if (parentItem1.Drafts.Any<ILifecycleDataItemDraft>((Func<ILifecycleDataItemDraft, bool>) (d => d.IsTempDraft && d.Owner != currentUserId)))
      {
        List<ILifecycleDataItemDraft> list = parentItem1.Drafts.Where<ILifecycleDataItemDraft>((Func<ILifecycleDataItemDraft, bool>) (d => d.IsTempDraft && d.Owner != currentUserId)).ToList<ILifecycleDataItemDraft>();
        TDraft[] array = new TDraft[list.Count<ILifecycleDataItemDraft>()];
        list.CopyTo((ILifecycleDataItemDraft[]) array, 0);
        for (int index = 0; index < array.Length; ++index)
          this.Manager.Delete(array[index]);
      }
      if ((object) parentItem1 is ICacheNotifyItem)
        ((ICacheNotifyItem) (object) parentItem1).SkipNotifyObjectChanged = true;
      return draft;
    }

    /// <summary>
    /// Edits the content in live state. Item becomes master after the edit.
    /// </summary>
    /// <param name="item">Item in live state that is to be edited.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was edited in master state.</returns>
    public virtual TDraft Edit(TItem item, CultureInfo culture = null)
    {
      TDraft draftItem = this.GetMaster(item);
      if ((object) draftItem == null)
      {
        draftItem = this.Manager.CreateDraft();
        draftItem.ParentItem = (ILifecycleDataItemLive) item;
        draftItem.IsTempDraft = false;
        this.Copy(item, draftItem, culture);
      }
      return draftItem;
    }

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish.
    /// </summary>
    /// <param name="item">Item in master state that is to be published.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Published content item</returns>
    public virtual TItem Publish(TDraft master, CultureInfo culture = null)
    {
      if (!this.IsMultilingualOn())
        culture = (CultureInfo) null;
      if ((object) master == null)
        throw new ArgumentNullException(nameof (master));
      TItem obj = !master.IsTempDraft ? (TItem) master.ParentItem : throw new InvalidOperationException("You can only publish a master item.");
      if (obj.Version > master.Version)
        throw new InvalidOperationException(Res.Get<ErrorMessages>().PageModifiedBySomeoneElse);
      int num = obj.Version > 0 ? 1 : 0;
      ++obj.Version;
      obj.LockedBy = Guid.Empty;
      obj.Status = ContentLifecycleStatus.Live;
      obj.LastModified = DateTime.UtcNow;
      master.Version = obj.Version;
      string languageKey = culture.GetLanguageKey();
      if (this.IsMultilingualOn() && !obj.PublishedTranslations.Contains(languageKey))
      {
        if (obj.PublishedTranslations.Count == 0 && obj.Visible)
          obj.PublishedTranslations.Add(LocalizationHelper.GetDefaultLanguageForObject((object) obj).GetLanguageKey());
        obj.AddPublishedTranslation(languageKey);
      }
      LanguageData languageData1 = this.GetOrCreateLanguageData((ILifecycleDataItem) obj, culture.GetSitefinityCulture());
      LanguageData languageData2 = this.GetOrCreateLanguageData((ILifecycleDataItem) master, culture.GetSitefinityCulture());
      languageData1.ContentState = LifecycleState.Published;
      languageData2.ContentState = LifecycleState.Published;
      languageData1.LanguageVersion = languageData2.LanguageVersion;
      DateTime utcNow = DateTime.UtcNow;
      if (num == 0)
      {
        languageData1.PublicationDate = utcNow;
        languageData2.PublicationDate = utcNow;
      }
      obj.Visible = true;
      this.Copy(master, obj, culture);
      List<ILifecycleDataItemDraft> list = obj.Drafts.Where<ILifecycleDataItemDraft>((Func<ILifecycleDataItemDraft, bool>) (d => d.IsTempDraft)).ToList<ILifecycleDataItemDraft>();
      if (this.Manager.DeleteTempAfterPublish || list.Count > 1)
      {
        TDraft[] array = new TDraft[list.Count];
        list.CopyTo((ILifecycleDataItemDraft[]) array, 0);
        for (int index = 0; index < array.Length; ++index)
          this.Manager.Delete(array[index]);
      }
      if ((object) obj is ICacheNotifyItem)
        ((ICacheNotifyItem) (object) obj).SkipNotifyObjectChanged = false;
      return obj;
    }

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="liveItem">Live item to unpublish.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Master (draft) state.</returns>
    public virtual TDraft Unpublish(TItem liveItem, CultureInfo culture = null)
    {
      int num = this.IsMultilingualOn() ? 1 : 0;
      this.GetOrCreateLanguageData((ILifecycleDataItem) liveItem, culture.GetSitefinityCulture()).ContentState = LifecycleState.None;
      liveItem.RemovePublishedTranslation(culture.GetLanguageKey());
      if (num != 0 && liveItem.PublishedTranslations.Count == 0)
        this.GetOrCreateLanguageData((ILifecycleDataItem) liveItem, (CultureInfo) null).ContentState = LifecycleState.None;
      liveItem.Visible = false;
      liveItem.LastModified = DateTime.UtcNow;
      if ((object) liveItem is ICacheNotifyItem)
        ((ICacheNotifyItem) (object) liveItem).SkipNotifyObjectChanged = false;
      return default (TDraft);
    }

    /// <summary>
    /// Returns ID of the user that checked out the master item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="culture"></param>
    /// <returns>
    /// ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    public virtual Guid GetCheckedOutBy(TItem item, CultureInfo culture = null)
    {
      ILifecycleDataItemDraft lifecycleDataItemDraft = item.Drafts.Where<ILifecycleDataItemDraft>((Func<ILifecycleDataItemDraft, bool>) (d => d.IsTempDraft)).SingleOrDefault<ILifecycleDataItemDraft>();
      return lifecycleDataItemDraft != null ? lifecycleDataItemDraft.Owner : Guid.Empty;
    }

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    public virtual bool IsCheckedOut(TItem item, CultureInfo culture = null) => item.Drafts.Count<ILifecycleDataItemDraft>((Func<ILifecycleDataItemDraft, bool>) (d => d.IsTempDraft)) > 0;

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// 
    ///             ///
    ///             <returns>
    /// True if it was checked out by a user with the specified id, false otherwize
    /// </returns>
    public virtual bool IsCheckedOutBy(TItem item, Guid userId, CultureInfo culture = null) => item.Drafts.Count<ILifecycleDataItemDraft>((Func<ILifecycleDataItemDraft, bool>) (d => d.IsTempDraft && d.Owner == userId)) > 0;

    public virtual void DiscardTemp(TDraft masterItem, CultureInfo culture = null) => masterItem.ParentItem.Drafts.Where<ILifecycleDataItemDraft>((Func<ILifecycleDataItemDraft, bool>) (d => d.IsTempDraft)).ToList<ILifecycleDataItemDraft>().ForEach((Action<ILifecycleDataItemDraft>) (td => this.DiscardTemp((TDraft) td)));

    public virtual void DiscardAllTemps(TDraft masterItem) => masterItem.ParentItem.Drafts.Where<ILifecycleDataItemDraft>((Func<ILifecycleDataItemDraft, bool>) (d => d.IsTempDraft)).ToList<ILifecycleDataItemDraft>().ForEach((Action<ILifecycleDataItemDraft>) (td => this.DiscardTemp((TDraft) td)));

    public virtual void DiscardTemp(TItem liveItem, CultureInfo culture = null)
    {
      TDraft master = this.GetMaster(liveItem);
      if ((object) master == null)
        return;
      this.DiscardTemp(master, culture);
    }

    public virtual void DiscardAllTemps(TItem liveItem)
    {
      TDraft master = this.GetMaster(liveItem);
      if ((object) master == null)
        return;
      this.DiscardAllTemps(master);
    }

    public virtual void DiscardTemp(TDraft tempItem)
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      TItem parentItem = (TItem) tempItem.ParentItem;
      if (parentItem.LockedBy == currentUserId || this.CanUnlockItem(parentItem, tempItem, currentUserId))
        parentItem.LockedBy = Guid.Empty;
      if (this.Manager.DeleteTempAfterPublish || tempItem.Version != parentItem.Version)
      {
        tempItem.ParentItem = (ILifecycleDataItemLive) null;
        this.Manager.Delete(tempItem);
      }
      if (!((object) parentItem is ICacheNotifyItem))
        return;
      ((ICacheNotifyItem) (object) parentItem).SkipNotifyObjectChanged = true;
    }

    public virtual TDraft GetTemp(TItem liveItem) => (TDraft) liveItem.GetTempItem();

    public virtual TDraft GetTemp(TDraft draftItem) => draftItem.IsTempDraft ? draftItem : (TDraft) draftItem.ParentItem.GetTempItem();

    public virtual TDraft GetMaster(TItem liveItem) => (TDraft) liveItem.GetMasterItem();

    public virtual TDraft GetMaster(TDraft draftItem) => !draftItem.IsTempDraft ? draftItem : (TDraft) draftItem.ParentItem.GetMasterItem();

    /// <summary>
    /// Discards all draft items both temp and master. All changes made in these items
    /// are lost.
    /// </summary>
    /// <param name="liveItem">The live item to clear drafts for.</param>
    public virtual void DiscardAllDrafts(TItem liveItem)
    {
      TDraft[] array = new TDraft[liveItem.Drafts.Count<ILifecycleDataItemDraft>()];
      liveItem.Drafts.ToList<ILifecycleDataItemDraft>().CopyTo((ILifecycleDataItemDraft[]) array, 0);
      for (int index = 0; index < array.Length; ++index)
        this.Manager.Delete(array[index]);
      liveItem.LockedBy = Guid.Empty;
    }

    /// <summary>
    /// Copies data from the specified live item to the specified draft item.
    /// </summary>
    /// <param name="liveItem">The live item to wrtie data to.</param>
    /// <param name="draftItem">The draft item to get data from.</param>
    /// <param name="culture">The culture of the operation.</param>
    public abstract void Copy(TItem liveItem, TDraft draftItem, CultureInfo culture);

    /// <summary>
    /// Copies data from the specified draft item to the specified live item.
    /// </summary>
    /// <param name="draftItem">The draft item to get data from.</param>
    /// <param name="liveItem">The live item to wrtie data to.</param>
    /// <param name="culture">The culture of the operation.</param>
    public abstract void Copy(TDraft draftItem, TItem liveItem, CultureInfo culture);

    /// <summary>
    /// Copies data from the specified source draft item to the specified target draft item.
    /// </summary>
    /// <param name="sourceDraft">The draft item to get data from.</param>
    /// <param name="targetDraft">The draft item to wrtie data to.</param>
    /// <param name="culture">The culture of the operation.</param>
    public abstract void Copy(TDraft sourceDraft, TDraft targetDraft, CultureInfo culture);

    /// <summary>
    /// Determines whether the current user is allowed to unlock the specified item.
    /// </summary>
    /// <param name="item">The live item instance.</param>
    /// <param name="tempItem">The temp item instance.</param>
    /// <param name="userId">The id of the current user.</param>
    /// <returns><c>True</c> if item can be unlocked, otherwise <c>false</c>.</returns>
    public virtual bool CanUnlockItem(TItem item, TDraft tempItem, Guid userId) => !(item is ISecuredObject securedObject) ? SecurityManager.IsUserUnrestricted(userId) : securedObject.IsGranted(SecurityActionTypes.Unlock);

    /// <inheritdoc />
    protected bool IsMultilingualOn() => SystemManager.CurrentContext.AppSettings.Multilingual;

    internal virtual bool CopyDuringCheckOut(TDraft master) => true;
  }
}
