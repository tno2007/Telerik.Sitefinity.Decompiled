// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ContentViewModelBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Lifecycle.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Workflow.Model.Tracking;
using Telerik.Sitefinity.Workflow.Services.Data;

namespace Telerik.Sitefinity.Modules
{
  [DataContract]
  public abstract class ContentViewModelBase : ILocalizable
  {
    private string parentUrl;
    private Guid id;
    private string title;
    private DateTime dateCreated;
    private DateTime lastModified;
    private DateTime publicationDate;
    private string providerName;
    private DateTime expirationDate;
    private Guid? defaultPageId;
    private int version;
    /// <summary>The content data provider</summary>
    protected ContentDataProviderBase provider;
    private string owner;
    protected bool isEditable = true;
    protected bool isDeletable = true;
    protected bool isUnlockable = true;
    private WcfApprovalTrackingRecord lastApprovalTrackingRecord;
    private string status;
    private ILifecycleManager lifecycleManager;
    private int? commentsCount;
    private ISchedulingStatus schedulingStatus;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentViewModel" /> class.
    /// </summary>
    public ContentViewModelBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ContentViewModelBase" /> class.
    /// </summary>
    /// <param name="contentItem">The master content item.</param>
    /// <param name="provider">The content provider.</param>
    /// <param name="liveItem">The live content item relevant to the master content item</param>
    /// <param name="tempItem">The temp content item relevant to the master content item.</param>
    public ContentViewModelBase(
      Content contentItem,
      ContentDataProviderBase provider,
      Content liveItem,
      Content tempItem)
    {
      this.Initialize(contentItem, provider, true, liveItem, tempItem);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public ContentViewModelBase(Content contentItem, ContentDataProviderBase provider) => this.Initialize(contentItem, provider, false);

    /// <summary>Checks if a secured object</summary>
    /// <param name="secObj">Checks if a secured object is modifiable (editable). Can be null.</param>
    /// <returns>true if modify is granted, or if secObj is null (it is not a secured object)</returns>
    protected virtual bool IsContentItemEditable(ISecuredObject secObj) => ContentViewModelBase.IsItemEditable(secObj);

    internal static bool IsItemEditable(ISecuredObject secObj)
    {
      if (secObj == null)
        return true;
      string permissionSetName = secObj.SupportedPermissionSets.Length != 0 ? secObj.SupportedPermissionSets[0] : "";
      if (secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Manage))
        return secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Manage);
      if (secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Modify))
        return secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Modify);
      return secObj is IHasContentChildren && secObj.SupportedPermissionSets.Length != 0 && secObj.IsSecurityActionSupported(secObj.SupportedPermissionSets[1], SecurityActionTypes.Manage);
    }

    /// <summary>Checks if a secured object is deletable</summary>
    /// <param name="secObj">Checks if a secured ojbect is deletable.</param>
    /// <returns>true if delte is granted, or if secObj is null (it is not a secured object)</returns>
    protected virtual bool IsContentItemDeletable(ISecuredObject secObj) => ContentViewModelBase.IsItemDeletable(secObj);

    internal static bool IsItemDeletable(ISecuredObject secObj)
    {
      if (secObj == null)
        return true;
      string permissionSetName = secObj.SupportedPermissionSets.Length != 0 ? secObj.SupportedPermissionSets[0] : "";
      if (secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Manage))
        return secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Manage);
      return secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Delete) && secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Delete);
    }

    protected virtual bool IsContentItemUnlockable(ISecuredObject secObj) => ContentViewModelBase.IsItemUnlockable(secObj);

    internal static bool IsItemUnlockable(ISecuredObject secObj)
    {
      if (secObj == null)
        return true;
      foreach (string supportedPermissionSet in secObj.SupportedPermissionSets)
      {
        if (secObj.IsSecurityActionTypeGranted(supportedPermissionSet, SecurityActionTypes.Unlock))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected virtual Content GetLive() => this.ContentItem.GetRelatedItem((object) this.lifecycleManager, this.ProviderName, ContentLifecycleStatus.Live);

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected virtual Content GetTemp() => this.ContentItem.GetRelatedItem((object) this.lifecycleManager, this.ProviderName, ContentLifecycleStatus.Temp);

    /// <summary>Gets or sets the content item.</summary>
    /// <value>The content item.</value>
    public Content ContentItem { get; set; }

    /// <summary>Gets or sets the live content item.</summary>
    /// <value>The live content item.</value>
    public Content LiveContentItem { get; set; }

    /// <summary>Gets or sets the parent.</summary>
    /// <value>The parent.</value>
    public Content Parent
    {
      get => this.ContentItem is IHasParent contentItem ? contentItem.Parent : (Content) null;
      set
      {
      }
    }

    /// <summary>Gets or sets the parent URL.</summary>
    /// <value>The parent URL.</value>
    [DataMember]
    public string ParentUrl
    {
      get => this.Parent == null ? this.parentUrl : this.GetItemUrl(this.Parent);
      set => this.parentUrl = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this item is editable.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this item is editable; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public virtual bool IsEditable
    {
      get => this.isEditable;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this item is deletable.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this item is deletable; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public virtual bool IsDeletable
    {
      get => this.isDeletable;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this item is unlockable.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this item is unlockable; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public virtual bool IsUnlockable
    {
      get => this.isUnlockable;
      set
      {
      }
    }

    /// <summary>Gets or sets the UI status.</summary>
    /// <value>The UI status.</value>
    [DataMember]
    public string UIStatus { get; set; }

    [DataMember]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Telerik.Sitefinity.Services.Status AdditionalStatus { get; set; }

    [DataMember]
    public int ItemsCount { get; set; }

    /// <summary>Gets or sets the pageId.</summary>
    /// <value>The pageId.</value>
    [DataMember]
    public Guid Id
    {
      get => this.ContentItem == null ? this.id : this.ContentItem.Id;
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.Id = value;
        else
          this.id = value;
      }
    }

    /// <summary>Gets or sets the live content item id.</summary>
    /// <value>The live content item id.</value>
    [DataMember]
    public Guid? LiveContentId
    {
      get
      {
        if (this.LiveContentItem != null)
          return new Guid?(this.LiveContentItem.Id);
        return this.ContentItem != null ? new Guid?(this.ContentItem.Id) : new Guid?(this.id);
      }
      set
      {
      }
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title
    {
      get => this.ContentItem == null ? this.title : this.ContentItem.Title.Value;
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.Title = (Lstring) value;
        else
          this.title = value;
      }
    }

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    [DataMember]
    public string Owner
    {
      get
      {
        if (this.owner == null && this.ContentItem != null)
          this.owner = this.provider.GetItemOwner((IDataItem) this.ContentItem);
        return this.owner;
      }
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.Owner = new Guid(value);
        else
          this.owner = value;
      }
    }

    /// <summary>Gets or sets the author.</summary>
    /// <value>The author.</value>
    [DataMember]
    public virtual string Author
    {
      get => this.Owner;
      set
      {
      }
    }

    /// <summary>Gets or sets the date created.</summary>
    /// <value>The date created.</value>
    [DataMember]
    public DateTime DateCreated
    {
      get => this.ContentItem != null ? this.ContentItem.DateCreated : this.dateCreated;
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.DateCreated = value;
        else
          this.dateCreated = value;
      }
    }

    /// <summary>Gets or sets the date modified.</summary>
    /// <value>The date modified.</value>
    [DataMember]
    public DateTime DateModified
    {
      get => this.ContentItem != null ? this.ContentItem.LastModified : this.lastModified;
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.LastModified = value;
        else
          this.lastModified = value;
      }
    }

    /// <summary>Gets or sets the publication date.</summary>
    /// <value>The publication date.</value>
    [DataMember]
    public DateTime PublicationDate
    {
      get => this.ContentItem != null ? this.ContentItem.PublicationDate : this.publicationDate;
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.PublicationDate = value;
        else
          this.publicationDate = value;
      }
    }

    /// <summary>Gets the provider name.</summary>
    /// <value>The provider name.</value>
    [DataMember]
    public string ProviderName
    {
      get => this.provider != null ? this.provider.Name : this.providerName;
      set => this.providerName = value;
    }

    /// <summary>Gets or sets the expiration date.</summary>
    /// <value>The expiration date.</value>
    [DataMember]
    public DateTime ExpirationDate
    {
      get
      {
        if (this.schedulingStatus != null)
          return this.schedulingStatus.ExpirationDate.HasValue ? this.schedulingStatus.ExpirationDate.Value : DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
        if (this.ContentItem == null)
          return this.expirationDate;
        return this.ContentItem.ExpirationDate.HasValue ? this.ContentItem.ExpirationDate.Value : DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
      }
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.ExpirationDate = new DateTime?(value);
        else
          this.expirationDate = value;
      }
    }

    /// <summary>Gets or sets the publication status enumeration.</summary>
    /// <value>The publication status.</value>
    [DataMember]
    public virtual string Status
    {
      get
      {
        if (string.IsNullOrEmpty(this.status) && this.ContentItem != null)
        {
          if (this.ContentItem is IApprovalWorkflowItem contentItem)
          {
            this.status = contentItem.GetLocalizedStatus();
            if (string.IsNullOrWhiteSpace(this.status))
              this.status = StatusConverter.FromModel(this.ContentItem.Status);
          }
          else
            this.status = StatusConverter.FromModel(this.ContentItem.Status);
        }
        return this.status;
      }
      set => this.status = value;
    }

    /// <summary>Gets or sets the comments count.</summary>
    /// <value>The comments count.</value>
    [DataMember]
    public int CommentsCount
    {
      get
      {
        if (this.commentsCount.HasValue)
          return this.commentsCount.Value;
        if (this.LiveContentItem == null && this.ContentItem == null)
          return 0;
        Guid itemId = this.LiveContentItem == null ? this.ContentItem.Id : this.LiveContentItem.Id;
        ICommentService commentsService = SystemManager.GetCommentsService();
        string localizedKey = ControlUtilities.GetLocalizedKey((object) itemId);
        CommentFilter commentFilter = new CommentFilter();
        commentFilter.ThreadKey.Add(localizedKey);
        commentFilter.Take = new int?(1);
        CommentFilter filter = commentFilter;
        int commentsCount;
        ref int local = ref commentsCount;
        commentsService.GetComments(filter, out local);
        return commentsCount;
      }
      set => this.commentsCount = new int?(value);
    }

    /// <summary>Contains information about the content lifecycle.</summary>
    [DataMember]
    public WcfContentLifecycleStatus LifecycleStatus { get; set; }

    /// <summary>
    /// Contains information about the workflow operations available.
    /// </summary>
    [DataMember]
    public IList<WorkflowVisualElement> WorkflowOperations { get; set; }

    [DataMember]
    public WcfChange VersionInfo { get; set; }

    /// <summary>Gets or sets the id of the  default page.</summary>
    /// <value>The default page id.</value>
    [DataMember]
    public virtual Guid? DefaultPageId
    {
      get => this.ContentItem != null ? this.ContentItem.DefaultPageId : this.defaultPageId;
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.DefaultPageId = value;
        else
          this.defaultPageId = value;
      }
    }

    /// <summary>Gets or sets the current approval record.</summary>
    /// <value>The current approval record.</value>
    [DataMember]
    public WcfApprovalTrackingRecord LastApprovalTrackingRecord
    {
      get
      {
        if (this.lastApprovalTrackingRecord == null && this.ContentItem != null && this.ContentItem is IWorkflowItem contentItem)
        {
          IApprovalTrackingRecord trackingRecordFromCache = contentItem.GetCurrentApprovalTrackingRecordFromCache();
          if (trackingRecordFromCache != null)
            this.lastApprovalTrackingRecord = new WcfApprovalTrackingRecord(trackingRecordFromCache, contentItem);
        }
        return this.lastApprovalTrackingRecord;
      }
      set => this.lastApprovalTrackingRecord = value;
    }

    /// <summary>Gets or sets the current content version.</summary>
    /// <value>The version.</value>
    [DataMember]
    public virtual int Version
    {
      get => this.ContentItem != null ? this.ContentItem.Version : this.version;
      set
      {
        if (this.ContentItem != null)
          this.ContentItem.Version = value;
        else
          this.version = value;
      }
    }

    protected string GetItemUrl(Content content)
    {
      try
      {
        return content is ILocatable locatable ? this.provider.GetItemUrl(locatable) : string.Empty;
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }

    private void Initialize(
      Content contentItem,
      ContentDataProviderBase provider,
      bool isLoadedLiveAndTemp,
      Content liveItem = null,
      Content tempItem = null)
    {
      this.ContentItem = contentItem;
      this.provider = provider;
      if (this.provider == null)
        this.provider = this.ContentItem.Provider as ContentDataProviderBase;
      this.lifecycleManager = ManagerBase.GetMappedManager(this.ContentItem.GetType(), this.provider.Name) as ILifecycleManager;
      ContentUIStatus contentUiStatus;
      if (contentItem.SupportsContentLifecycle)
      {
        Content liveItem1 = this.GetLiveItem(isLoadedLiveAndTemp, liveItem);
        Content tempItem1 = this.GetTempItem(isLoadedLiveAndTemp, tempItem);
        this.SetApprovalWorflowStatus();
        if (this.ContentItem is ILifecycleDataItemGeneric contentItem1)
        {
          this.LifecycleSetNewerThanPublishedStatus(liveItem1 as ILifecycleDataItemGeneric);
          this.SetLockedByUserStatus(tempItem1);
          if (string.IsNullOrEmpty(this.UIStatus))
          {
            contentUiStatus = LifecycleExtensions.GetUIStatus(contentItem1, (CultureInfo) null);
            this.UIStatus = contentUiStatus.ToString();
          }
        }
        else
        {
          this.SetNewerThanPublishedStatus(liveItem1);
          this.SetLockedByUserStatus(tempItem1);
          if (string.IsNullOrEmpty(this.UIStatus))
          {
            contentUiStatus = this.ContentItem.GetContentUIStatus();
            this.UIStatus = contentUiStatus.ToString();
          }
        }
      }
      if (string.IsNullOrEmpty(this.UIStatus))
      {
        contentUiStatus = ContentUIStatus.Draft;
        this.UIStatus = contentUiStatus.ToString();
      }
      this.isEditable = this.IsContentItemEditable(this.ContentItem as ISecuredObject);
      this.isDeletable = this.IsContentItemDeletable(this.ContentItem as ISecuredObject);
      this.isUnlockable = this.IsContentItemUnlockable(this.ContentItem as ISecuredObject);
    }

    private void SetLockedByUserStatus(Content temp)
    {
      if (temp == null || !(temp.Owner != Guid.Empty))
        return;
      this.UIStatus = ContentUIStatus.PrivateCopy.ToString();
      this.Status = string.Format(Res.Get<Labels>().LockedByFormat, (object) CommonMethods.GetUserName(temp.Owner), (object) this.Status);
    }

    private void SetApprovalWorflowStatus()
    {
      if (!(this.ContentItem is IApprovalWorkflowItem))
        return;
      string statusKey;
      IStatusInfo statusInfo;
      this.Status = IApprovalWorkflowExtensions.GetLocalizedStatus((IWorkflowItem) this.ContentItem, out statusKey, out statusInfo);
      this.UIStatus = statusKey;
      this.schedulingStatus = statusInfo != null ? statusInfo.Data as ISchedulingStatus : (ISchedulingStatus) null;
    }

    private void LifecycleSetNewerThanPublishedStatus(ILifecycleDataItemGeneric live)
    {
      if (live == null || !live.Visible || !live.IsPublished() || !LifecycleExtensions.IsNewer((ILifecycleDataItem) this.ContentItem, (ILifecycleDataItem) live))
        return;
      this.Status = string.Format("{0} {1}", (object) this.Status, (object) Res.Get<Labels>().NewerThanPublishedInParentheses);
    }

    private void SetNewerThanPublishedStatus(Content live)
    {
      if (live == null || !live.Visible || this.ContentItem.Version <= live.Version)
        return;
      this.Status = string.Format("{0} {1}", (object) this.Status, (object) Res.Get<Labels>().NewerThanPublishedInParentheses);
    }

    private Content GetTempItem(bool isLoadedTempItem, Content tempItem) => isLoadedTempItem ? tempItem : this.GetTemp();

    private Content GetLiveItem(bool isLoadedLiveItem, Content liveItem)
    {
      this.LiveContentItem = !isLoadedLiveItem ? this.GetLive() : liveItem;
      return this.LiveContentItem;
    }

    /// <summary>Gets cultures available for this item.</summary>
    /// <value>The available cultures.</value>
    public virtual CultureInfo[] AvailableCultures
    {
      get
      {
        if (this.ContentItem is ILifecycleDataItem)
          return ((ILifecycleDataItem) this.ContentItem).GetCultureList().ToArray();
        return this.ContentItem != null ? this.ContentItem.AvailableCultures : new CultureInfo[0];
      }
    }

    /// <summary>Gets languages available for this item.</summary>
    /// <value>The available languages.</value>
    [DataMember]
    public virtual string[] AvailableLanguages => ((IEnumerable<CultureInfo>) this.AvailableCultures).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)).ToArray<string>();
  }
}
