// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  public static class WorkflowExtensions
  {
    /// <summary>
    /// Gets the state of the workflow. If the item was created in multilingual then no fallback is applied.
    /// </summary>
    /// <param name="item">The item.</param>
    public static string GetWorkflowState(this IApprovalWorkflowItem item) => item.GetWorkflowState(SystemManager.CurrentContext.Culture);

    internal static string GetWorkflowState(this IApprovalWorkflowItem item, CultureInfo culture) => item.GetWorkflowState(culture, out IStatusInfo _);

    internal static string GetWorkflowState(
      this IApprovalWorkflowItem item,
      CultureInfo culture,
      out IStatusInfo status)
    {
      if (item.TryGetExternalStatus(out status, culture) && !status.Data.Status.IsNullOrEmpty())
        return status.Data.Status;
      string workflowState = item.ApprovalWorkflowState.GetStringNoFallback(culture);
      if (workflowState == "Scheduled")
        workflowState = "Draft";
      return workflowState;
    }

    internal static bool TryGetExternalStatus(
      this IWorkflowItem item,
      out IStatusInfo status,
      CultureInfo culture = null)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      status = (IStatusInfo) SystemManager.StatusProviderRegistry.GetItemStatuses(item.Id, item.GetActualType(), item.GetWorkflowItemProviderName(), item.GetRootKey(), culture, StatusBehaviour.Workflow).FirstOrDefault<StatusInfo>();
      return status != null;
    }

    internal static void AddLanguage(this WorkflowScope scope, string languageName)
    {
      ISet<string> languages = scope.GetLanguages();
      languages.Add(languageName);
      scope.SetLanguages((IEnumerable<string>) languages);
    }

    internal static void RemoveLanguage(this WorkflowScope scope, string languageName)
    {
      ISet<string> languages = scope.GetLanguages();
      languages.Remove(languageName);
      scope.SetLanguages((IEnumerable<string>) languages);
    }

    internal static ISet<string> GetLanguages(this WorkflowScope scope)
    {
      if (string.IsNullOrEmpty(scope.Language))
        return (ISet<string>) new HashSet<string>();
      return (ISet<string>) new HashSet<string>((IEnumerable<string>) scope.Language.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries));
    }

    internal static void SetLanguages(this WorkflowScope scope, IEnumerable<string> languages)
    {
      string str = string.Join(",", languages.Distinct<string>());
      scope.Language = str;
    }

    internal static IList<string> GetContentFilter(this IWorkflowExecutionTypeScope typeScope)
    {
      if (string.IsNullOrEmpty(typeScope.ContentFilter))
        return (IList<string>) new List<string>();
      return (IList<string>) new List<string>((IEnumerable<string>) typeScope.ContentFilter.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries));
    }

    internal static IList<string> GetContentFilter(this WorkflowTypeScope typeScope)
    {
      if (string.IsNullOrEmpty(typeScope.ContentFilter))
        return (IList<string>) new List<string>();
      return (IList<string>) new List<string>((IEnumerable<string>) typeScope.ContentFilter.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries));
    }

    internal static void SetContentFilter(
      this WorkflowTypeScope typeScope,
      IEnumerable<string> contentFilter)
    {
      string str = string.Join(",", contentFilter.Distinct<string>());
      typeScope.ContentFilter = str;
    }

    internal static Guid GetScopeSiteId(this WorkflowScope scope)
    {
      SiteItemLink siteItemLink = WorkflowManager.GetManager().GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemType == typeof (WorkflowScope).FullName && l.ItemId == scope.Id)).FirstOrDefault<SiteItemLink>();
      return siteItemLink != null ? siteItemLink.SiteId : Guid.Empty;
    }
  }
}
