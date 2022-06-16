// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.WcfApprovalTrackingRecord
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Represents data contract for <see cref="T:Telerik.Sitefinity.Workflow.Model.Tracking.ApprovalTrackingRecord" /> class
  /// </summary>
  [DataContract]
  public class WcfApprovalTrackingRecord
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.WcfApprovalTrackingRecord" /> class.
    /// </summary>
    /// <param name="record">The record.</param>
    /// <param name="item">The item.</param>
    public WcfApprovalTrackingRecord(IApprovalTrackingRecord record, IWorkflowItem item)
    {
      IApprovalWorkflowItem workflowItem = item as IApprovalWorkflowItem;
      CultureInfo culture = (CultureInfo) null;
      if (record.Culture > 0)
        culture = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(record.Culture);
      this.Initialize(record, item, workflowItem != null ? workflowItem.GetLocalizedStatus(culture) : string.Empty);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.WcfApprovalTrackingRecord" /> class.
    /// </summary>
    /// <param name="record">The record.</param>
    public WcfApprovalTrackingRecord(
      IApprovalTrackingRecord record,
      IWorkflowItem item,
      string localizedApprovalStatus)
    {
      this.Initialize(record, item, localizedApprovalStatus);
    }

    private void Initialize(
      IApprovalTrackingRecord record,
      IWorkflowItem item,
      string localizedApprovalStatus)
    {
      if (record == null)
        return;
      this.DateCreated = record.DateCreated;
      this.Note = record.Note;
      this.Status = record.Status;
      this.UIStatus = !(item is IApprovalWorkflowItem) ? record.Status : localizedApprovalStatus;
      this.UserName = WcfApprovalTrackingRecord.GetUser(record.UserId);
    }

    /// <summary>Represents the time the tracking record occurred.</summary>
    [DataMember]
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets a note providing information about the current status
    /// </summary>
    /// <value>The note.</value>
    [DataMember]
    public string Note { get; set; }

    /// <summary>Gets or sets the content status.</summary>
    /// <value>The status.</value>
    [DataMember]
    public string Status { get; set; }

    /// <summary>Gets or sets the UI status.</summary>
    /// <value>The UI status.</value>
    [DataMember]
    public string UIStatus { get; set; }

    /// <summary>The full name of the user</summary>
    [DataMember]
    public string UserName { get; set; }

    private static string GetUser(Guid id) => UserProfilesHelper.GetUserDisplayName(id);
  }
}
