// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.ApprovalStatusConstants
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Contains constants for possible workflow approval statuses.
  /// </summary>
  public static class ApprovalStatusConstants
  {
    /// <summary>Item is waiting for approval.</summary>
    public const string AwaitingReview = "AwaitingReview";
    /// <summary>Item is waiting for approval.</summary>
    public const string AwaitingApproval = "AwaitingApproval";
    /// <summary>Item is waiting for publishing.</summary>
    public const string AwaitingPublishing = "AwaitingPublishing";
    /// <summary>Item was rejected by approver.</summary>
    public const string Rejected = "Rejected";
    /// <summary>Item was rejected for publishing.</summary>
    public const string RejectedForPublishing = "RejectedForPublishing";
    /// <summary>Item was rejected for review.</summary>
    public const string RejectedForReview = "RejectedForReview";
    /// <summary>Item was rejected for approval.</summary>
    public const string RejectedForApproval = "RejectedForApproval";
    /// <summary>Item is published.</summary>
    public const string Published = "Published";
    /// <summary>Item was unpublished.</summary>
    public const string Unpublished = "Unpublished";
    /// <summary>Item was saved as draft.</summary>
    public const string Draft = "Draft";
    /// <summary>Item was scheduled for publishing.</summary>
    public const string Scheduled = "Scheduled";
  }
}
