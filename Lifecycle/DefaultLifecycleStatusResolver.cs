// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.DefaultLifecycleStatusResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Lifecycle
{
  public class DefaultLifecycleStatusResolver
  {
    public virtual void SetStatusInfo(
      ILifecycleViewModel item,
      ILifecycleDataItem live,
      ILifecycleDataItem temp)
    {
      if (item == null)
        throw new ArgumentNullException("The lifeCycleViewModel item was not set");
      if (item is IApprovalWorkflowItem workflowItem)
      {
        string statusKey;
        item.Lifecycle.Message = IApprovalWorkflowExtensions.GetLocalizedStatus(workflowItem, out statusKey, out IStatusInfo _);
        if (statusKey.IsNullOrEmpty())
          statusKey = "draft";
        item.Lifecycle.WorkflowStatus = statusKey;
        switch (statusKey.ToLower())
        {
          case "awaitingapproval":
            item.Lifecycle.IsPublished = false;
            break;
          case "awaitingpublishing":
            item.Lifecycle.IsPublished = false;
            break;
          case "draft":
            if (live != null)
              item.Lifecycle.Message = string.Format("{0} {1}", (object) item.Lifecycle.Message, (object) Res.Get<Labels>().NewerThanPublishedInParentheses);
            item.Lifecycle.IsPublished = false;
            break;
          case "published":
            item.Lifecycle.IsPublished = true;
            break;
          case "rejected":
            item.Lifecycle.IsPublished = false;
            break;
          case "rejectedforpublishing":
            item.Lifecycle.IsPublished = false;
            break;
          case "scheduled":
            item.Lifecycle.IsPublished = false;
            break;
          case "unpublished":
            item.Lifecycle.IsPublished = false;
            break;
        }
        Status status = StatusResolver.Resolve(workflowItem.GetType(), item.GetProviderName(), workflowItem.Id);
        if (status != null)
        {
          item.Lifecycle.AdditionalStatusText = status.Text;
          item.Lifecycle.StatusProviderName = status.PrimaryProvider;
        }
      }
      if (live != null)
      {
        switch (item)
        {
          case DynamicContent _:
            item.Lifecycle.PublicationDate = new DateTime?((live as DynamicContent).PublicationDate);
            break;
          case Content _:
            item.Lifecycle.PublicationDate = new DateTime?((live as Content).PublicationDate);
            break;
        }
      }
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (temp != null && temp.Owner != Guid.Empty)
      {
        this.SetLockedByUserStatus((ILifecycleDataItem) item, temp);
        switch (item)
        {
          case DynamicContent _:
            item.Lifecycle.LockedSince = new DateTime?((temp as DynamicContent).DateCreated);
            break;
          case Content _:
            item.Lifecycle.LockedSince = new DateTime?((temp as Content).DateCreated);
            break;
        }
        item.Lifecycle.IsLocked = true;
        item.Lifecycle.IsLockedByMe = temp.Owner == currentIdentity.UserId;
      }
      item.Lifecycle.IsAdmin = currentIdentity.IsUnrestricted;
    }

    private bool IsDraftScheduled(ILifecycleViewModel item, out string status)
    {
      if (item is IScheduleable scheduleable)
      {
        DateTime? expirationDate;
        if (!(scheduleable.PublicationDate > DateTime.UtcNow))
        {
          expirationDate = scheduleable.ExpirationDate;
          DateTime utcNow = DateTime.UtcNow;
          if ((expirationDate.HasValue ? (expirationDate.GetValueOrDefault() > utcNow ? 1 : 0) : 0) == 0)
            goto label_9;
        }
        expirationDate = scheduleable.ExpirationDate;
        DateTime utcNow1 = DateTime.UtcNow;
        if ((expirationDate.HasValue ? (expirationDate.GetValueOrDefault() > utcNow1 ? 1 : 0) : 0) != 0 && scheduleable.PublicationDate > DateTime.UtcNow)
          status = Res.Get<ApprovalWorkflowResources>("ScheduledDraft", (object) string.Format("{0}, {1}", (object) Res.Get<ApprovalWorkflowResources>().ScheduledPublicationDate, (object) Res.Get<ApprovalWorkflowResources>().ScheduledExpirationDate));
        else if (scheduleable.PublicationDate > DateTime.UtcNow)
          status = Res.Get<ApprovalWorkflowResources>("ScheduledDraft", (object) Res.Get<ApprovalWorkflowResources>().ScheduledPublicationDate);
        else
          status = Res.Get<ApprovalWorkflowResources>("ScheduledDraft", (object) Res.Get<ApprovalWorkflowResources>().ScheduledExpirationDate);
        return true;
      }
label_9:
      status = (string) null;
      return false;
    }

    protected virtual void SetLockedByUserStatus(ILifecycleDataItem item, ILifecycleDataItem temp = null)
    {
      if (temp == null)
        temp = item;
      if (!(item is ILifecycleViewModel lifecycleViewModel))
        return;
      lifecycleViewModel.Lifecycle.WorkflowStatus = ContentUIStatus.PrivateCopy.ToString();
      lifecycleViewModel.Lifecycle.Message = string.Format(Res.Get<Labels>().LockedByFormat, (object) CommonMethods.GetUserName(temp.Owner), (object) lifecycleViewModel.Lifecycle.Message);
      string formattedUserName = SecurityManager.GetFormattedUserName(temp.Owner);
      lifecycleViewModel.Lifecycle.LockedByUsername = formattedUserName;
    }
  }
}
