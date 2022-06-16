// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.WorkflowNotificationContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Collections.Generic;
using Telerik.Sitefinity.Services.Notifications;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>
  /// Represents a context used when generating a workflow notification.
  /// </summary>
  public class WorkflowNotificationContext
  {
    /// <summary>
    /// Gets a collection of the subscribers of the notification.
    /// </summary>
    public IEnumerable<ISubscriberRequest> Subscribers { get; internal set; }

    /// <summary>
    /// Gets the current status (awaiting, rejected, approved...) of the workflow item.
    /// <seealso cref="F:Telerik.Sitefinity.Workflow.ApprovalStatusConstants.AwaitingApproval" />
    /// <seealso cref="F:Telerik.Sitefinity.Workflow.ApprovalStatusConstants.AwaitingPublishing" />
    /// <seealso cref="F:Telerik.Sitefinity.Workflow.ApprovalStatusConstants.Rejected" />
    /// <seealso cref="F:Telerik.Sitefinity.Workflow.ApprovalStatusConstants.RejectedForPublishing" />
    /// <seealso cref="F:Telerik.Sitefinity.Workflow.ApprovalStatusConstants.Published" />
    /// <seealso cref="F:Telerik.Sitefinity.Workflow.ApprovalStatusConstants.Unpublished" />
    /// <seealso cref="F:Telerik.Sitefinity.Workflow.ApprovalStatusConstants.Draft" />
    /// <seealso cref="F:Telerik.Sitefinity.Workflow.ApprovalStatusConstants.Scheduled" />
    /// </summary>
    public string ApprovalStatus { get; internal set; }

    /// <summary>
    /// Gets the note written by the last status changer.
    /// For example if the content was rejected, then it will hold the rejection reason.
    /// </summary>
    public string ApprovalNote { get; internal set; }

    /// <summary>
    /// Gets a localized (in default BackEnd culture) name of the item's type.
    /// </summary>
    public string ItemTypeLabel { get; internal set; }

    /// <summary>Gets a URL where the item is shown in preview mode.</summary>
    public string ItemPreviewUrl { get; internal set; }

    /// <summary>Gets the name of the workflow item.</summary>
    public string ItemTitle { get; internal set; }

    /// <summary>
    /// Gets the name of the site where item will be published.
    /// </summary>
    public string SiteName { get; internal set; }

    /// <summary>
    /// Gets the URL of the site where item will be published.
    /// </summary>
    public string SiteUrl { get; internal set; }

    /// <summary>Gets the time when the item was last modified.</summary>
    public DateTime ModificationTime { get; internal set; }

    /// <summary>
    /// Gets the id of the user who last modified the workflow item.
    /// Can be Guid.Empty if user is unknown.
    /// </summary>
    public Guid LastModifierId { get; internal set; }

    /// <summary>
    /// Gets the Id of the user who last changed the approval status
    /// of the workflow item.
    /// </summary>
    public Guid LastStateChangerId { get; internal set; }

    /// <summary>Gets the URL where one can log into site's BackEnd.</summary>
    public string LoginUrl { get; internal set; }

    /// <summary>
    /// Gets the DataContext of the currently executing activity.
    /// </summary>
    public WorkflowDataContext ActivityDataContext { get; internal set; }

    /// <summary>
    /// Gets the full URL with scheme where one can access the Site`s frontend.
    /// </summary>
    public Uri SiteAbsoluteUrl { get; internal set; }
  }
}
