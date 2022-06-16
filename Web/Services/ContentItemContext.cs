// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.ContentItemContext`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Provides context information for a content item that is exposed in a web service
  /// </summary>
  /// <typeparam name="T">Type of the content item</typeparam>
  /// <remarks>
  /// Main benefit of wrapping the content item is that WCF interceptions work. If <typeparamref name="T" /> is not in
  /// the assembly of the web service, surrogates won't work (i.e. dynamic properties and automation transaction entering).
  /// </remarks>
  [DataContract]
  public class ContentItemContext<T> : RelatedDataItemContextBase<T>, IContextWithAdditionalUrls
    where T : IContent
  {
    private Dictionary<string, object> sfAdditonalInfo;
    private WcfChange versionInfo;
    private string itemType;
    private WcfApprovalTrackingRecord lastApprovalTrackingRecord;
    private string[] urlNames;
    private bool allowMultipleUrls;
    private bool additionalUrlsRedirectToDefault;
    private string defaultUrl;

    /// <summary>Wrapped data - the content item itself.</summary>
    [DataMember]
    public override T Item
    {
      get => base.Item;
      set
      {
        base.Item = value;
        this.itemType = value.GetType().FullName;
      }
    }

    /// <summary>
    /// Type of the content item. Set automatically when <see cref="P:Telerik.Sitefinity.Web.Services.ContentItemContext`1.Item" /> is set.
    /// </summary>
    [DataMember]
    public string ItemType
    {
      get => this.itemType;
      set => this.itemType = value;
    }

    /// <summary>Versioning information</summary>
    [DataMember]
    public WcfChange VersionInfo
    {
      get => this.versionInfo;
      set => this.versionInfo = value;
    }

    /// <summary>Publication settings</summary>
    [DataMember]
    public List<WcfPipeSettings> PublicationSettings { get; set; }

    /// <summary>Contains information about the content lifecycle.</summary>
    [DataMember]
    public WcfContentLifecycleStatus LifecycleStatus { get; set; }

    /// <summary>Gets or sets the current approval record.</summary>
    /// <value>The current approval record.</value>
    [DataMember]
    public WcfApprovalTrackingRecord LastApprovalTrackingRecord
    {
      get
      {
        if (this.lastApprovalTrackingRecord == null && this.Item is IWorkflowItem workflowItem)
        {
          IApprovalTrackingRecord trackingRecordFromCache = workflowItem.GetCurrentApprovalTrackingRecordFromCache();
          if (trackingRecordFromCache != null)
            this.lastApprovalTrackingRecord = new WcfApprovalTrackingRecord(trackingRecordFromCache, workflowItem);
        }
        return this.lastApprovalTrackingRecord;
      }
      set
      {
      }
    }

    /// <summary>Gets or sets the URL names of the item.</summary>
    [DataMember]
    public string[] AdditionalUrlNames
    {
      get => this.urlNames;
      set => this.urlNames = value;
    }

    /// <summary>Gets or sets the flag, if multiple urls are allowed.</summary>
    [DataMember]
    public bool AllowMultipleUrls
    {
      get => this.allowMultipleUrls;
      set => this.allowMultipleUrls = value;
    }

    /// <summary>
    /// Gets or sets the flag, if all additional url-s will redirect to the default one.
    /// </summary>
    [DataMember]
    public bool AdditionalUrlsRedirectToDefault
    {
      get => this.additionalUrlsRedirectToDefault;
      set => this.additionalUrlsRedirectToDefault = value;
    }

    /// <summary>Gets or sets the default URL.</summary>
    /// <value>The default URL.</value>
    [DataMember]
    public string DefaultUrl
    {
      get => this.defaultUrl;
      set => this.defaultUrl = value;
    }

    /// <summary>
    /// Gets or sets additional information that can be used by derived types to store specific information.
    /// </summary>
    /// <value>The additional info.</value>
    [DataMember]
    public Dictionary<string, object> SfAdditionalInfo
    {
      get
      {
        if (this.sfAdditonalInfo == null)
          this.sfAdditonalInfo = new Dictionary<string, object>();
        return this.sfAdditonalInfo;
      }
      set => this.sfAdditonalInfo = value;
    }
  }
}
