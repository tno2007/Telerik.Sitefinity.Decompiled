// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Microsoft.Practices.Unity.Properties;
using Telerik.Microsoft.Practices.Unity.Utility;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Lifecycle
{
  /// <summary>
  /// Represents an implementation of lifecycle in 3 stages - Master, Temp &amp; Live with a single item type.
  /// The different status - live, master are kept within the same type but in different records
  /// </summary>
  public class LifecycleDecorator : IExtendedLifecycleDecorator, ILifecycleDecorator
  {
    private readonly Type[] itemTypes;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.LifecycleDecorator" /> class.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="copyDelegate">The copy delegate.</param>
    /// <param name="itemTypes">The item types.</param>
    public LifecycleDecorator(
      ILifecycleManager manager,
      LifecycleItemCopyDelegate copyDelegate,
      params Type[] itemTypes)
    {
      this.Manager = manager;
      this.LifecycleCopyDelegate = copyDelegate;
      this.itemTypes = itemTypes.Length != 0 ? itemTypes : throw new ArgumentOutOfRangeException("At least one item type should be specified");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.LifecycleDecorator" /> class.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="copyDelegate">The copy delegate.</param>
    /// <param name="itemTypes">The item types.</param>
    [Obsolete("Use to support the ContentManagerBase<Provider>, IContentLifecycleManager<ILifecycleDataItem> implementation.")]
    [InjectionConstructor]
    public LifecycleDecorator(
      ILifecycleManager manager,
      Action<Content, Content> copyDelegate,
      params Type[] itemTypes)
    {
      this.Manager = manager;
      this.CopyDelegate = copyDelegate;
      this.itemTypes = itemTypes.Length != 0 ? itemTypes : throw new ArgumentOutOfRangeException("At least one item type should be specified");
    }

    protected ILifecycleManager Manager { get; private set; }

    protected LifecycleItemCopyDelegate LifecycleCopyDelegate { get; private set; }

    protected Action<Content, Content> CopyDelegate { get; private set; }

    internal LifecycleUnpublishItemDelegate UnpublishItemDelegate { get; set; }

    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="temp"></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>An item in master state.</returns>
    public ILifecycleDataItemGeneric CheckIn(
      ILifecycleDataItemGeneric temp,
      CultureInfo culture = null,
      bool deleteTemp = true)
    {
      Guard.ArgumentNotNull((object) temp, nameof (temp));
      this.GuardType((object) temp, nameof (temp));
      CultureInfo sitefinityCulture = culture.GetSitefinityCulture();
      if (temp.Status != ContentLifecycleStatus.Temp && temp.Status != ContentLifecycleStatus.PartialTemp)
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().TempExpectedForCheckIn);
      ILifecycleDataItemGeneric lifecycleDataItemGeneric = (ILifecycleDataItemGeneric) this.Manager.GetItem(temp.GetType(), temp.OriginalContentId);
      if (lifecycleDataItemGeneric.Status != ContentLifecycleStatus.Master)
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterNotFound);
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (temp.Owner != Guid.Empty && (currentIdentity == null || !currentIdentity.IsUnrestricted && !(currentIdentity.UserId == temp.Owner)))
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().CannotCheckIn);
      this.CopyProperties((ILifecycleDataItem) temp, (ILifecycleDataItem) lifecycleDataItemGeneric, culture);
      lifecycleDataItemGeneric.IncrementLanguageVersion((ILanguageDataManager) this.Manager, sitefinityCulture);
      if (temp.Status != ContentLifecycleStatus.PartialTemp)
        this.Manager.CopyItemRelations(temp, lifecycleDataItemGeneric);
      if (typeof (IScheduleable).IsAssignableFrom(temp.GetType()))
      {
        ((IScheduleable) lifecycleDataItemGeneric).PublicationDate = ((IScheduleable) temp).PublicationDate;
        lifecycleDataItemGeneric.CopyScheduledDates((IScheduleable) temp, sitefinityCulture);
      }
      bool suppressSecurityChecks = this.Manager.Provider.SuppressSecurityChecks;
      this.Manager.Provider.SuppressSecurityChecks = true;
      if (deleteTemp)
      {
        if (temp.LanguageData != null && temp.LanguageData.Where<LanguageData>((Func<LanguageData, bool>) (ld => !ld.Language.IsNullOrEmpty())).Count<LanguageData>() > 1)
          this.Manager.DeleteItem((object) temp);
        else
          temp.Owner = Guid.Empty;
      }
      this.Manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      lifecycleDataItemGeneric.LastModifiedBy = SecurityManager.CurrentUserId;
      lifecycleDataItemGeneric.LastModified = DateTime.UtcNow;
      return lifecycleDataItemGeneric;
    }

    /// <summary>
    /// Checks out the content in master state. Item becomes temp after the check out.
    /// </summary>
    /// <param name="master"></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>A content that was checked out in temp state.</returns>
    public ILifecycleDataItemGeneric CheckOut(
      ILifecycleDataItemGeneric master,
      CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) master, nameof (master));
      this.GuardType((object) master, nameof (master));
      if (master is ISecuredObject securedObject && !this.Manager.Provider.SuppressSecurityChecks && !securedObject.IsSecurityActionTypeGranted(SecurityActionTypes.Manage) && !this.Manager.Provider.IsSecurityActionGranted(securedObject, SecurityActionTypes.Modify))
        throw new UnauthorizedAccessException("You do not have permissions to modify this item.");
      Guid guid = master.Status == ContentLifecycleStatus.Master ? master.Id : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpectedForCheckOut);
      ILifecycleDataItemGeneric temp = this.GetTemp(master, culture);
      bool flag1 = true;
      if (temp == null)
      {
        bool suppressSecurityChecks = this.Manager.Provider.SuppressSecurityChecks;
        this.Manager.Provider.SuppressSecurityChecks = true;
        temp = (ILifecycleDataItemGeneric) this.Manager.CreateItem(master.GetType());
        temp.OriginalContentId = guid;
        temp.AddPublishedTranslation(culture);
        this.Manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
      else if (temp.Owner != Guid.Empty)
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        bool flag2 = securedObject != null ? securedObject.IsSecurityActionTypeGranted(SecurityActionTypes.Unlock) : currentIdentity.IsUnrestricted;
        if (currentIdentity == null || !flag2 && !(currentIdentity.UserId == temp.Owner))
          throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().AlreadyCheckedOut);
        flag1 = false;
      }
      if (flag1)
        temp.Status = this.GetAllTemps((ILifecycleDataItem) master).Any<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (t => t.Id != temp.Id && t.Owner != Guid.Empty && t.Status == ContentLifecycleStatus.Temp)) ? ContentLifecycleStatus.PartialTemp : ContentLifecycleStatus.Temp;
      this.CopyProperties((ILifecycleDataItem) master, (ILifecycleDataItem) temp, culture);
      this.Manager.CopyItemRelations(master, temp);
      if (typeof (IScheduleable).IsAssignableFrom(master.GetType()))
        ((IScheduleable) temp).PublicationDate = ((IScheduleable) master).PublicationDate;
      temp.Owner = SecurityManager.CurrentUserId;
      temp.LastModifiedBy = temp.Owner;
      temp.LastModified = DateTime.UtcNow;
      return temp;
    }

    /// <summary>
    /// Edits the content in live state. Item becomes master after the edit.
    /// </summary>
    /// <param name="liveItem">Item in live state that is to be edited.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>A content that was edited in master state.</returns>
    public ILifecycleDataItemGeneric Edit(
      ILifecycleDataItemGeneric liveItem,
      CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) liveItem, nameof (liveItem));
      this.GuardType((object) liveItem, nameof (liveItem));
      if (liveItem.Status != ContentLifecycleStatus.Live)
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().LiveExpectedForEdit);
      if (!(this.Manager.GetItem(liveItem.GetType(), liveItem.OriginalContentId) is ILifecycleDataItemGeneric destination) || destination.Status != ContentLifecycleStatus.Master)
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterNotFound);
      this.CopyProperties((ILifecycleDataItem) liveItem, (ILifecycleDataItem) destination, culture);
      this.Manager.CopyItemRelations(liveItem, destination);
      return destination;
    }

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish.
    /// </summary>
    /// <param name="masterItem">Item in master state that is to be published.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>Published content item</returns>
    public ILifecycleDataItemGeneric Publish(
      ILifecycleDataItemGeneric masterItem,
      CultureInfo culture = null)
    {
      if (SystemManager.CurrentHttpContext == null || SystemManager.CurrentHttpContext.Items == null || SystemManager.CurrentHttpContext.Items[(object) "PublicationDate"] == null)
        return this.PublishWithSpecificDate(masterItem, DateTime.MinValue, culture);
      DateTime result;
      DateTime.TryParse(SystemManager.CurrentHttpContext.Items[(object) "PublicationDate"].ToString(), out result);
      return this.PublishWithSpecificDate(masterItem, result, culture);
    }

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish with the specified publication date.
    /// </summary>
    /// <param name="masterItem">The item.</param>
    /// <param name="publicationDate">The publication date.</param>
    /// <param name="culture">The culture in which to perform the operation.</param>
    /// <returns>Published content item</returns>
    public ILifecycleDataItemGeneric PublishWithSpecificDate(
      ILifecycleDataItemGeneric masterItem,
      DateTime publicationDate,
      CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) masterItem, nameof (masterItem));
      this.GuardType((object) masterItem, nameof (masterItem));
      if (masterItem.Status != ContentLifecycleStatus.Master)
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpectedForPublish);
      culture = culture.GetSitefinityCulture();
      ILifecycleDataItemGeneric live = this.GetLive(masterItem, culture);
      int newItemsCount = 0;
      if (live == null || live != null && !live.Visible)
        newItemsCount = 1;
      LicenseLimitations.CanSaveItems(masterItem.GetType(), newItemsCount);
      return this.ExecuteOnPublish(masterItem, live, culture, new DateTime?(publicationDate));
    }

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="liveItem">Live item to unpublish.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>Master (draft) state.</returns>
    public ILifecycleDataItemGeneric Unpublish(
      ILifecycleDataItemGeneric liveItem,
      CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) liveItem, nameof (liveItem));
      this.GuardType((object) liveItem, nameof (liveItem));
      ILifecycleDataItemGeneric lifecycleDataItemGeneric = liveItem.Status == ContentLifecycleStatus.Live ? this.GetMaster(liveItem) : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().LiveExpectedForUnpublish);
      CultureInfo sitefinityCulture = culture.GetSitefinityCulture();
      LanguageData languageData1 = lifecycleDataItemGeneric.GetLanguageData(sitefinityCulture);
      if (languageData1 == null && sitefinityCulture == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage)
        languageData1 = this.GetOrCreateLanguageData((ILifecycleDataItem) lifecycleDataItemGeneric, sitefinityCulture);
      else if (languageData1 == null)
        languageData1 = this.GetOrCreateLanguageData((ILifecycleDataItem) lifecycleDataItemGeneric, (CultureInfo) null);
      languageData1.ContentState = LifecycleState.None;
      LanguageData languageData2 = liveItem.GetLanguageData(sitefinityCulture);
      if (languageData2 != null)
        languageData2.ContentState = LifecycleState.None;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        liveItem.RemovePublishedTranslation(sitefinityCulture);
        lifecycleDataItemGeneric.RemovePublishedTranslation(sitefinityCulture);
        if (liveItem.ShouldApplyChangesToInvariant(sitefinityCulture))
        {
          LanguageData languageDataRaw1 = lifecycleDataItemGeneric.GetLanguageDataRaw((CultureInfo) null);
          if (languageDataRaw1 != null)
            languageDataRaw1.ContentState = LifecycleState.None;
          LanguageData languageDataRaw2 = liveItem.GetLanguageDataRaw((CultureInfo) null);
          if (languageDataRaw2 != null)
            languageDataRaw2.ContentState = LifecycleState.None;
        }
        if (liveItem.PublishedTranslations.Count == 0)
          LifecycleDecorator.HideItem(liveItem);
      }
      else
        LifecycleDecorator.HideItem(liveItem);
      if (this.UnpublishItemDelegate != null)
        this.UnpublishItemDelegate((ILifecycleDataItem) liveItem, (ILifecycleDataItem) lifecycleDataItemGeneric, culture);
      if (liveItem is IHasTrackingContext context)
      {
        if (culture != null)
          context.RegisterOperation(OperationStatus.Unpublished, new string[1]
          {
            culture.GetLanguageKey()
          });
        else
          context.RegisterOperation(OperationStatus.Unpublished);
      }
      return lifecycleDataItemGeneric;
    }

    /// <summary>
    /// Returns ID of the user that checked out the master item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="culture"></param>
    /// <returns>
    /// ID of the user that checked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    public Guid GetCheckedOutBy(ILifecycleDataItemGeneric item, CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) item, nameof (item));
      this.GuardType((object) item, nameof (item));
      ILifecycleDataItemGeneric temp = this.GetTemp(item, culture);
      return temp != null ? temp.Owner : Guid.Empty;
    }

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>True if the item is checked out, false otherwise.</returns>
    public bool IsCheckedOut(ILifecycleDataItemGeneric item, CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) item, nameof (item));
      this.GuardType((object) item, nameof (item));
      ILifecycleDataItemGeneric temp = this.GetTemp(item, culture);
      return temp != null && temp.Owner != Guid.Empty;
    }

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// 
    ///             ///
    ///             <returns>
    /// True if it was checked out by a user with the specified id, false otherwise
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="item" /> is <c>null</c>.</exception>
    /// <exception cref="T:System.ArgumentException">When the type of <paramref name="item" /> is not valid.</exception>
    public bool IsCheckedOutBy(ILifecycleDataItemGeneric item, Guid userId, CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) item, nameof (item));
      this.GuardType((object) item, nameof (item));
      ILifecycleDataItemGeneric temp = this.GetTemp(item, culture);
      return temp != null && temp.Owner == userId;
    }

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>
    /// Public (live) version of <paramref name="cnt" />, if it exists
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="item" /> is <c>null</c>.</exception>
    /// <exception cref="T:System.ArgumentException">When the type of <paramref name="item" /> is not valid.</exception>
    public ILifecycleDataItemGeneric GetLive(
      ILifecycleDataItemGeneric item,
      CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) item, nameof (item));
      this.GuardType((object) item, nameof (item));
      if (item.Status != ContentLifecycleStatus.Live)
      {
        Guid masterId;
        switch (item.Status)
        {
          case ContentLifecycleStatus.Master:
          case ContentLifecycleStatus.Deleted:
            masterId = item.Id;
            break;
          default:
            masterId = item.OriginalContentId;
            break;
        }
        string filterExpression = string.Format("OriginalContentId = {0} && Status = Live", (object) masterId);
        IEnumerator enumerator = this.Manager.GetItems(item.GetType(), filterExpression, string.Empty, 0, 0).GetEnumerator();
        if (enumerator.MoveNext())
          return (ILifecycleDataItemGeneric) enumerator.Current;
        ILifecycleDataItemGeneric itemInTransaction = this.Manager.Provider.GetDirtyItems().OfType<ILifecycleDataItemGeneric>().FirstOrDefault<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (i => i.OriginalContentId == masterId && i.Status == ContentLifecycleStatus.Live));
        if (itemInTransaction != null && this.Manager.Provider.GetDirtyItemStatus((object) itemInTransaction) != SecurityConstants.TransactionActionType.Deleted)
          return itemInTransaction;
      }
      return (ILifecycleDataItemGeneric) null;
    }

    /// <summary>
    /// Get a temp for <paramref name="item" />, if it exists.
    /// </summary>
    /// <param name="item">Item to get a temp for</param>
    /// <param name="culture"></param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="item" /> is <c>null</c>.</exception>
    /// <exception cref="T:System.ArgumentException">When the type of <paramref name="item" /> is not valid.</exception>
    /// <returns>Temp version of <paramref name="item" />, if it exists.</returns>
    public ILifecycleDataItemGeneric GetTemp(
      ILifecycleDataItemGeneric item,
      CultureInfo culture = null)
    {
      IEnumerable<ILifecycleDataItemGeneric> allTemps = this.GetAllTemps((ILifecycleDataItem) item);
      ILifecycleDataItemGeneric temp = (ILifecycleDataItemGeneric) null;
      if (this.AllowConcurrentEditing)
      {
        foreach (ILifecycleDataItemGeneric lifecycleDataItemGeneric in allTemps)
        {
          if (lifecycleDataItemGeneric.GetLanguageData(culture) != null)
          {
            temp = lifecycleDataItemGeneric;
            break;
          }
        }
      }
      else
        temp = allTemps.FirstOrDefault<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (t => t.Status == ContentLifecycleStatus.Temp));
      return temp;
    }

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <param name="item">Item whose master to get</param>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="item" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="item" /> is <c>null</c>.</exception>
    /// <returns>
    /// If <paramref name="item" /> is master itself, returns item.
    /// Otherwise, looks up the master associated with <paramref name="item" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    public ILifecycleDataItemGeneric GetMaster(
      ILifecycleDataItemGeneric item)
    {
      Guard.ArgumentNotNull((object) item, nameof (item));
      this.GuardType((object) item, nameof (item));
      ILifecycleDataItemGeneric lifecycleDataItemGeneric;
      switch (item.Status)
      {
        case ContentLifecycleStatus.Master:
          lifecycleDataItemGeneric = item;
          break;
        case ContentLifecycleStatus.Temp:
        case ContentLifecycleStatus.Live:
        case ContentLifecycleStatus.PartialTemp:
          lifecycleDataItemGeneric = this.Manager.GetItem(item.GetType(), item.OriginalContentId) as ILifecycleDataItemGeneric;
          break;
        default:
          throw new NotSupportedException(string.Format("Status '{0}' is not supported by the Lifecycle decorator.", (object) item.Status));
      }
      return lifecycleDataItemGeneric != null ? lifecycleDataItemGeneric : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterNotFound);
    }

    /// <summary>
    /// Discards the temp item in the specified culture for the specified master item. All changes in
    /// the discarded item are lost.
    /// </summary>
    /// <param name="masterItem"></param>
    /// <param name="culture">The culture of the temp item to discard.</param>
    public virtual void DiscardTemp(ILifecycleDataItem masterItem, CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) masterItem, nameof (masterItem));
      this.GuardType((object) masterItem, nameof (masterItem));
      ILifecycleDataItemGeneric lifecycleDataItemGeneric = masterItem.Status == ContentLifecycleStatus.Master ? this.GetTemp((ILifecycleDataItemGeneric) masterItem, culture) : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpected);
      if (lifecycleDataItemGeneric == null)
        return;
      lifecycleDataItemGeneric.Owner = Guid.Empty;
    }

    /// <summary>
    /// Discards all temp items for the specified master item. All changes in the discarded items
    /// are lost.
    /// </summary>
    /// <param name="masterItem"></param>
    public virtual void DiscardAllTemps(ILifecycleDataItem masterItem)
    {
      Guard.ArgumentNotNull((object) masterItem, nameof (masterItem));
      this.GuardType((object) masterItem, nameof (masterItem));
      if (masterItem.Status != ContentLifecycleStatus.Master)
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpected);
      foreach (ILifecycleDataItemGeneric allTemp in this.GetAllTemps(masterItem))
      {
        allTemp.Owner = masterItem.Owner;
        this.Manager.DeleteItem((object) allTemp);
      }
    }

    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="item">Item in temp state that is to be checked in.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>An item in master state.</returns>
    ILifecycleDataItem ILifecycleDecorator.CheckIn(
      ILifecycleDataItem item,
      CultureInfo culture = null,
      bool deleteTemp = true)
    {
      return (ILifecycleDataItem) this.CheckIn((ILifecycleDataItemGeneric) item, culture, deleteTemp);
    }

    /// <summary>
    /// Checks out the content in master state. Item becomes temp after the check out.
    /// </summary>
    /// <param name="item">Item in master state that is to be checked out.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>A content that was checked out in temp state.</returns>
    ILifecycleDataItem ILifecycleDecorator.CheckOut(
      ILifecycleDataItem item,
      CultureInfo culture = null)
    {
      return (ILifecycleDataItem) this.CheckOut((ILifecycleDataItemGeneric) item, culture);
    }

    /// <summary>
    /// Edits the content in live state. Item becomes master after the edit.
    /// </summary>
    /// <param name="item">Item in live state that is to be edited.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>A content that was edited in master state.</returns>
    ILifecycleDataItem ILifecycleDecorator.Edit(
      ILifecycleDataItem item,
      CultureInfo culture = null)
    {
      return (ILifecycleDataItem) this.Edit((ILifecycleDataItemGeneric) item, culture);
    }

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish.
    /// </summary>
    /// <param name="item">Item in master state that is to be published.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>Published content item</returns>
    ILifecycleDataItem ILifecycleDecorator.Publish(
      ILifecycleDataItem item,
      CultureInfo culture = null)
    {
      return (ILifecycleDataItem) this.Publish((ILifecycleDataItemGeneric) item, culture);
    }

    ILifecycleDataItem ILifecycleDecorator.PublishWithSpecificDate(
      ILifecycleDataItem item,
      DateTime publicationDate,
      CultureInfo culture = null)
    {
      return (ILifecycleDataItem) this.PublishWithSpecificDate((ILifecycleDataItemGeneric) item, publicationDate, culture);
    }

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>Master (draft) state.</returns>
    ILifecycleDataItem ILifecycleDecorator.Unpublish(
      ILifecycleDataItem item,
      CultureInfo culture = null)
    {
      return (ILifecycleDataItem) this.Unpublish((ILifecycleDataItemGeneric) item, culture);
    }

    /// <summary>
    /// Returns ID of the user that checked out the master item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="culture"></param>
    /// <returns>
    /// ID of the user that checked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    Guid ILifecycleDecorator.GetCheckedOutBy(
      ILifecycleDataItem item,
      CultureInfo culture = null)
    {
      return this.GetCheckedOutBy((ILifecycleDataItemGeneric) item, culture);
    }

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>True if the item is checked out, false otherwise.</returns>
    bool ILifecycleDecorator.IsCheckedOut(
      ILifecycleDataItem item,
      CultureInfo culture = null)
    {
      return this.IsCheckedOut((ILifecycleDataItemGeneric) item, culture);
    }

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <returns>
    /// True if it was checked out by a user with the specified id, false otherwise
    /// </returns>
    /// 
    ///             ///
    bool ILifecycleDecorator.IsCheckedOutBy(
      ILifecycleDataItem item,
      Guid userId,
      CultureInfo culture = null)
    {
      return this.IsCheckedOutBy((ILifecycleDataItemGeneric) item, userId, culture);
    }

    ILifecycleDataItem ILifecycleDecorator.GetLive(
      ILifecycleDataItem cnt,
      CultureInfo culture = null)
    {
      return (ILifecycleDataItem) this.GetLive((ILifecycleDataItemGeneric) cnt, culture);
    }

    ILifecycleDataItem ILifecycleDecorator.GetTemp(
      ILifecycleDataItem cnt,
      CultureInfo culture = null)
    {
      return (ILifecycleDataItem) this.GetTemp((ILifecycleDataItemGeneric) cnt, culture);
    }

    ILifecycleDataItem ILifecycleDecorator.GetMaster(
      ILifecycleDataItem cnt)
    {
      return (ILifecycleDataItem) this.GetMaster((ILifecycleDataItemGeneric) cnt);
    }

    /// <summary>
    /// Copies the properties - executes the copy delegate, also copies workflow item records, ordinal,
    /// </summary>
    /// <param name="source">Source item</param>
    /// <param name="destination">Destination item</param>
    /// <param name="culture">Culture</param>
    public virtual void CopyProperties(
      ILifecycleDataItem source,
      ILifecycleDataItem destination,
      CultureInfo culture = null)
    {
      CopyOptions copyOptionsInternal = this.GetCopyOptionsInternal(source, destination);
      destination.ApplicationName = source.ApplicationName;
      if (typeof (IOrderedItem).InstancesAreAssignableToType((object) source, (object) destination))
        ((IOrderedItem) destination).Ordinal = ((IOrderedItem) source).Ordinal;
      if (typeof (IDynamicFieldsContainer).InstancesAreAssignableToType((object) source, (object) destination))
      {
        IDynamicFieldsContainer dynamicFieldsContainer1 = (IDynamicFieldsContainer) source;
        IDynamicFieldsContainer dynamicFieldsContainer2 = (IDynamicFieldsContainer) destination;
        if (copyOptionsInternal != CopyOptions.LocalazibleFields)
          ContentLifecycleManagerExtensions.CopyDynamicFieldsRecursively(dynamicFieldsContainer1, dynamicFieldsContainer2);
        if (copyOptionsInternal != CopyOptions.AllFields)
        {
          dynamicFieldsContainer1.CopyLstringPropertiesTo(dynamicFieldsContainer2, culture);
          dynamicFieldsContainer1.CopyLocalizablePropertiesTo(dynamicFieldsContainer2, culture);
        }
        else
          LocalizationHelper.CopyLocalizablePropertiesTo(dynamicFieldsContainer1, dynamicFieldsContainer2);
      }
      if (copyOptionsInternal != CopyOptions.LocalazibleFields)
      {
        if (destination is ISecuredObject)
          ((ISecuredObject) destination).CopySecurityFrom((ISecuredObject) source, (IDataProviderBase) this.Manager.Provider, (IDataProviderBase) this.Manager.Provider);
        if (typeof (Content).InstancesAreAssignableToType((object) source, (object) destination))
          this.CopyContentProperties(source as Content, destination as Content, culture);
      }
      this.CopyLanguageData(source, destination, culture);
      this.CopyDelegateWrapper(source, destination, culture);
    }

    /// <summary>
    /// Wrapper that supports the obsolete "copy" methods functionality. Depends on the delegate that the decorated is initialized with.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="culture"></param>
    protected virtual void CopyDelegateWrapper(
      ILifecycleDataItem source,
      ILifecycleDataItem destination,
      CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) source, nameof (source));
      Guard.ArgumentNotNull((object) destination, nameof (destination));
      if (source.GetType() != destination.GetType())
        throw new ArgumentException("source and destination must be of the same type");
      if (this.LifecycleCopyDelegate != null)
        this.LifecycleCopyDelegate(source, destination, culture);
      else
        this.CopyDelegate((Content) source, (Content) destination);
    }

    /// <summary>
    /// Copies the language data - publication,scheduled dates between the items
    /// </summary>
    protected virtual void CopyLanguageData(
      ILifecycleDataItem source,
      ILifecycleDataItem destination,
      CultureInfo culture = null)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      LanguageData languageData1 = source.GetLanguageData(culture);
      LanguageData languageData2 = this.GetOrCreateLanguageData(destination, culture);
      if (languageData1 != null)
      {
        languageData1.CopyLanguageDataProperties(languageData2);
      }
      else
      {
        languageData2.PublicationDate = DateTime.UtcNow;
        languageData2.ContentState = LifecycleState.None;
      }
    }

    /// <summary>
    /// Copies the common properties for items that are based on <typeparamref name="Content" />
    /// </summary>
    protected virtual void CopyContentProperties(
      Content source,
      Content destination,
      CultureInfo culture = null)
    {
      Guard.ArgumentNotNull((object) source, nameof (source));
      Guard.ArgumentNotNull((object) destination, nameof (destination));
      if (source.Status == ContentLifecycleStatus.PartialTemp)
        return;
      destination.ExpirationDate = source.ExpirationDate;
      destination.Version = source.Version;
      destination.DefaultPageId = source.DefaultPageId;
      destination.PostRights = source.PostRights;
      destination.AllowTrackBacks = source.AllowTrackBacks;
      destination.AllowComments = source.AllowComments;
      destination.ApproveComments = source.ApproveComments;
      destination.EmailAuthor = source.EmailAuthor;
      destination.IncludeInSitemap = source.IncludeInSitemap;
    }

    /// <summary>
    /// Lifecycle operation executed upon the Publish operation - statues are set, publication dates, etc
    /// </summary>
    /// <param name="masterItem"></param>
    /// <param name="liveItem"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    protected virtual ILifecycleDataItemGeneric ExecuteOnPublish(
      ILifecycleDataItemGeneric masterItem,
      ILifecycleDataItemGeneric liveItem,
      CultureInfo culture = null,
      DateTime? publicationDate = null)
    {
      Guid guid = masterItem.Status == ContentLifecycleStatus.Master ? masterItem.Id : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpectedForPublish);
      DateTime utcNow = DateTime.UtcNow;
      bool flag1 = false;
      if (publicationDate.HasValue && publicationDate.Value != DateTime.MinValue)
      {
        utcNow = publicationDate.Value;
        flag1 = true;
      }
      bool flag2 = false;
      LanguageData languageData1;
      if (liveItem == null)
      {
        liveItem = (ILifecycleDataItemGeneric) this.Manager.CreateItem(masterItem.GetType());
        liveItem.OriginalContentId = guid;
        liveItem.Status = ContentLifecycleStatus.Live;
        languageData1 = this.GetOrCreateLanguageData((ILifecycleDataItem) liveItem, culture);
        languageData1.PublicationDate = utcNow;
        flag1 = true;
      }
      else
      {
        LanguageData languageDataRaw = liveItem.GetLanguageDataRaw((CultureInfo) null);
        if (liveItem.Visible && liveItem.LanguageData.Count == 1 && languageDataRaw != null && languageDataRaw.ContentState == LifecycleState.Published && liveItem.PublishedTranslations.Count == 0)
          flag2 = true;
        languageData1 = this.GetOrCreateLanguageData((ILifecycleDataItem) liveItem, culture);
        if (!liveItem.Visible)
        {
          languageData1.PublicationDate = utcNow;
          flag1 = true;
        }
      }
      this.CopyProperties((ILifecycleDataItem) masterItem, (ILifecycleDataItem) liveItem, culture);
      this.Manager.CopyItemRelations(masterItem, liveItem);
      languageData1.ExpirationDate = new DateTime?();
      languageData1.ContentState = LifecycleState.Published;
      if (((!SystemManager.CurrentContext.AppSettings.Multilingual ? 0 : (!liveItem.PublishedTranslations.Contains(culture.Name) ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        languageData1.PublicationDate = utcNow;
      liveItem.Owner = masterItem.Owner;
      liveItem.LastModifiedBy = masterItem.LastModifiedBy;
      liveItem.LastModified = DateTime.UtcNow;
      LanguageData languageData2 = this.GetOrCreateLanguageData((ILifecycleDataItem) masterItem, culture);
      languageData2.PublicationDate = languageData1.PublicationDate;
      languageData2.ContentState = LifecycleState.Published;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (flag2)
        {
          CultureInfo frontendLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
          liveItem.AddPublishedTranslation(frontendLanguage);
          masterItem.AddPublishedTranslation(frontendLanguage);
        }
        liveItem.AddPublishedTranslation(culture);
        masterItem.AddPublishedTranslation(culture);
        if (liveItem.ShouldApplyChangesToInvariant(culture))
        {
          LanguageData languageDataRaw1 = masterItem.GetLanguageDataRaw((CultureInfo) null);
          if (languageDataRaw1 != null)
            languageDataRaw1.ContentState = LifecycleState.Published;
          LanguageData languageDataRaw2 = liveItem.GetLanguageDataRaw((CultureInfo) null);
          if (languageDataRaw2 != null)
            languageDataRaw2.ContentState = LifecycleState.Published;
        }
      }
      liveItem.Visible = true;
      if (typeof (IScheduleable).InstancesAreAssignableToType((object) liveItem, (object) masterItem))
      {
        IScheduleable scheduleable1 = (IScheduleable) masterItem;
        IScheduleable scheduleable2 = (IScheduleable) liveItem;
        if (flag1)
          scheduleable1.PublicationDate = scheduleable2.PublicationDate = utcNow;
        else if (scheduleable2.PublicationDate != scheduleable1.PublicationDate)
          scheduleable2.PublicationDate = scheduleable1.PublicationDate;
      }
      if (liveItem is ILocatable locatable && typeof (UrlDataProviderBase).IsAssignableFrom(this.Manager.Provider.GetType()))
      {
        UrlDataProviderBase provider = this.Manager.Provider as UrlDataProviderBase;
        provider.RecompileItemUrls<ILocatable>(locatable, culture);
        if (CommonMethods.IsUrlDuplicate((DataProviderBase) provider, (object) liveItem))
          throw new DuplicateUrlException("Url already exists");
      }
      if (masterItem is ILocatable && typeof (UrlDataProviderBase).IsAssignableFrom(this.Manager.Provider.GetType()))
      {
        UrlDataProviderBase provider = this.Manager.Provider as UrlDataProviderBase;
        provider.RecompileItemUrls<ILocatable>((ILocatable) masterItem, culture);
        if (CommonMethods.IsUrlDuplicate((DataProviderBase) provider, (object) liveItem))
          throw new DuplicateUrlException("Url already exists");
      }
      return liveItem;
    }

    /// <summary>
    /// Tries to load the language data for a specified culture
    /// In monolingual the culture is always invariant - empty string
    /// If no culture is specified takes the current UI from the thread
    /// </summary>
    /// <param name="dataItem">Data item</param>
    /// <param name="culture">The culture</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Lifecycle.LanguageData" />.</returns>
    public LanguageData GetOrCreateLanguageData(
      ILifecycleDataItem dataItem,
      CultureInfo culture = null)
    {
      return LifecycleExtensions.GetOrCreateLanguageData(dataItem, (ILanguageDataManager) this.Manager, culture);
    }

    /// <summary>Gets the lifecycle data items</summary>
    /// <param name="item">The lifecycle data item.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItemGeneric" />.</returns>
    protected internal virtual IEnumerable<ILifecycleDataItemGeneric> GetAllTemps(
      ILifecycleDataItem item)
    {
      Guard.ArgumentNotNull((object) item, nameof (item));
      this.GuardType((object) item, nameof (item));
      Guid guid = item.Id;
      if (item is ILifecycleDataItemGeneric lifecycleDataItemGeneric && lifecycleDataItemGeneric.Status != ContentLifecycleStatus.Master && lifecycleDataItemGeneric.Status != ContentLifecycleStatus.Deleted)
        guid = lifecycleDataItemGeneric.OriginalContentId;
      string filterExpression = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "OriginalContentId = {0} && (Status = Temp || Status = PartialTemp)", (object) guid);
      return this.Manager.GetItems(item.GetType(), filterExpression, (string) null, 0, 0).Cast<ILifecycleDataItemGeneric>();
    }

    /// <summary>
    /// Verifies if the item is assignable to at least one of the supported by the decorator types
    /// </summary>
    /// <param name="instance">An object instance.</param>
    /// <param name="argumentName">Argument name.</param>
    protected virtual void GuardType(object instance, string argumentName)
    {
      Guard.ArgumentNotNull(instance, nameof (instance));
      Type itemType = this.itemTypes[0];
      for (int index = 0; index < this.itemTypes.Length; ++index)
      {
        itemType = this.itemTypes[index];
        if (itemType.IsInstanceOfType(instance))
          return;
      }
      throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.TypesAreNotAssignable, (object) itemType, (object) instance.GetType().FullName), argumentName);
    }

    private static void HideItem(ILifecycleDataItemGeneric liveItem)
    {
      liveItem.Visible = false;
      if (!(liveItem is IOrganizable organizable))
        return;
      organizable.Organizer.ClearAll();
    }

    CopyOptions IExtendedLifecycleDecorator.GetCopyOptions(
      ILifecycleDataItem source,
      ILifecycleDataItem destination)
    {
      return this.GetCopyOptionsInternal(source, destination);
    }

    private CopyOptions GetCopyOptionsInternal(
      ILifecycleDataItem source,
      ILifecycleDataItem destination)
    {
      if ((destination.Status != ContentLifecycleStatus.Live || source.Status != ContentLifecycleStatus.Master) && (destination.Status != ContentLifecycleStatus.Master || source.Status != ContentLifecycleStatus.Temp && source.Status != ContentLifecycleStatus.PartialTemp) || !this.AllowConcurrentEditing)
        return CopyOptions.AllFields;
      return source.Status == ContentLifecycleStatus.PartialTemp ? CopyOptions.LocalazibleFields : CopyOptions.LocalizableAndCommonFields;
    }

    private bool AllowConcurrentEditing => SystemManager.CurrentContext.AllowConcurrentEditing;
  }
}
