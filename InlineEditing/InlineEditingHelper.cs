// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.InlineEditingHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.InlineEditing.Setters;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.InlineEditing;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Activities;

namespace Telerik.Sitefinity.InlineEditing
{
  /// <summary>
  /// Helper class that provides additional operations over editable containers for the Inline Editing in Sitefinity
  /// </summary>
  public static class InlineEditingHelper
  {
    /// <summary>Sets the fields of the items</summary>
    /// <param name="item"></param>
    /// <param name="fields"></param>
    public static void SetItemFields(object item, FieldValueModel[] fields)
    {
      PropertySetterBase defaultValue = ObjectFactory.Resolve<PropertySetterBase>();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(item);
      IEnumerable<IPropertySetter> source = ObjectFactory.Container.ResolveAll(typeof (IPropertySetter)).Cast<IPropertySetter>();
      foreach (FieldValueModel field in fields)
      {
        PropertyDescriptor property = properties[field.Name];
        if (property != null)
          source.Where<IPropertySetter>((Func<IPropertySetter, bool>) (s => s.GetPropertyType() == property.PropertyType)).DefaultIfEmpty<IPropertySetter>((IPropertySetter) defaultValue).FirstOrDefault<IPropertySetter>().Set(item, field, property);
      }
      if (!(item is DynamicContent))
        return;
      (item as DynamicContent).UnresolveLinks();
    }

    internal static bool IsWorkflowOperationValid(
      Type itemType,
      string providerName,
      Guid itemId,
      string workflowOperation,
      string customWorkflowOperation,
      out string operationToExecute)
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      Dictionary<string, DecisionActivity> dictionary = WorkflowManager.GetCurrentDecisions(itemType, providerName, itemId, culture).Where<KeyValuePair<string, DecisionActivity>>((Func<KeyValuePair<string, DecisionActivity>, bool>) (d => !d.Value.HideInUI)).ToDictionary<KeyValuePair<string, DecisionActivity>, string, DecisionActivity>((Func<KeyValuePair<string, DecisionActivity>, string>) (x => x.Key), (Func<KeyValuePair<string, DecisionActivity>, DecisionActivity>) (x => x.Value));
      if (workflowOperation == InlineEditingOperation.SaveAsDraft.ToString() || string.IsNullOrEmpty(workflowOperation))
      {
        operationToExecute = InlineEditingHelper.GetDraftOperationName(dictionary);
        return true;
      }
      if (!dictionary.ContainsKey(workflowOperation))
      {
        IOrderedEnumerable<KeyValuePair<string, DecisionActivity>> source = dictionary.Where<KeyValuePair<string, DecisionActivity>>((Func<KeyValuePair<string, DecisionActivity>, bool>) (x => x.Value.Placeholder.ToString() == "MainAction")).OrderBy<KeyValuePair<string, DecisionActivity>, int>((Func<KeyValuePair<string, DecisionActivity>, int>) (x => x.Value.Ordinal));
        if (source.Count<KeyValuePair<string, DecisionActivity>>() > 0)
        {
          operationToExecute = source.First<KeyValuePair<string, DecisionActivity>>().Key;
          return false;
        }
      }
      if (workflowOperation == InlineEditingOperation.Custom.ToString())
        workflowOperation = customWorkflowOperation;
      operationToExecute = workflowOperation;
      return true;
    }

    internal static string GetDraftOperationName(Dictionary<string, DecisionActivity> allDecisions)
    {
      string draftOperationName = string.Empty;
      IEnumerable<KeyValuePair<string, DecisionActivity>> source = allDecisions.Where<KeyValuePair<string, DecisionActivity>>((Func<KeyValuePair<string, DecisionActivity>, bool>) (d => d.Key.ToLowerInvariant().Contains("draft")));
      if (source.Count<KeyValuePair<string, DecisionActivity>>() > 0)
        draftOperationName = source.First<KeyValuePair<string, DecisionActivity>>().Key;
      return draftOperationName;
    }

    /// <summary>
    /// Substitutes each item in the provided collection with its master item or temp item (if one exists for current user)
    /// </summary>
    /// <remarks>Works only in InlineEditing mode</remarks>
    /// <returns>Collection of items eligible for InlineEditing</returns>
    public static List<T> ApplyItemFilter<T>(IManager manager, List<T> items) where T : ILifecycleDataItem
    {
      if (SystemManager.IsInlineEditingMode && ContentLocatableViewExtensions.GetRequestedItemStatus() == ContentLifecycleStatus.Temp)
      {
        for (int index = 0; index < items.Count<T>(); ++index)
        {
          object resultItem;
          if (ContentLocatableViewExtensions.TryGetItemWithRequestedStatus((ILifecycleDataItem) items[index], manager as ILifecycleManager, out resultItem))
          {
            ILifecycleDataItem lifecycleDataItem = (ILifecycleDataItem) resultItem;
            if (lifecycleDataItem.Status == ContentLifecycleStatus.Master || lifecycleDataItem.Status == ContentLifecycleStatus.Temp && (lifecycleDataItem.Owner == SecurityManager.GetCurrentUserId() || lifecycleDataItem.Owner == Guid.Empty))
              items[index] = (T) resultItem;
          }
        }
      }
      return items;
    }

    internal static string GetDisplayName(object item, string pageTitle)
    {
      string empty = string.Empty;
      return !(item is PageDraftControl) ? (!(item is TemplateControl) || string.IsNullOrWhiteSpace(pageTitle) ? (!(item is IHasTitle) ? item.GetType().Name : ((IHasTitle) item).GetTitle()) : pageTitle) : (string) ((PageDraftControl) item).Page.ParentPage.Title;
    }

    internal static LifecycleStatusModel GetItemLifecycleStatus(
      ILifecycleDataItem item,
      ILifecycleManager manager)
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      LifecycleStatusModel lifecycleStatus = (LifecycleStatusModel) null;
      if (item.Status == ContentLifecycleStatus.Temp && item.Owner == Guid.Empty)
        item = manager.Lifecycle.GetMaster(item);
      switch (item)
      {
        case DynamicContent _:
          DynamicContent dynamicItem = item as DynamicContent;
          lifecycleStatus = InlineEditingHelper.GetDynamicContentLifecycleStatus(dynamicItem);
          DynamicModuleType dynamicModuleType = ModuleBuilderManager.GetManager().GetDynamicModuleType(item.GetType());
          string name1 = ((DataProviderBase) dynamicItem.Provider).Name;
          lifecycleStatus.IsEditable = dynamicModuleType.IsEditable(name1);
          break;
        case Content _:
          lifecycleStatus = InlineEditingHelper.GetContentLifecycleStatus(item, manager, culture);
          if (item is ISecuredObject)
          {
            ISecuredObject secObj = item as ISecuredObject;
            string permissionSetName = secObj.SupportedPermissionSets.Length != 0 ? secObj.SupportedPermissionSets[0] : "";
            bool flag = false;
            if (secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Manage))
              flag = secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Manage);
            else if (secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Modify))
              flag = secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Modify);
            lifecycleStatus.IsEditable = flag;
            break;
          }
          break;
      }
      string approvalWorkflowState = lifecycleStatus.WorkflowStatus;
      if (item is IApprovalWorkflowItem approvalWorkflowItem)
      {
        string name2 = ((DataProviderBase) item.Provider).Name;
        if (WorkflowManager.GetCurrentDecisions(item.GetType(), name2, item.Id, culture) == null)
        {
          lifecycleStatus.IsEditable = false;
          lifecycleStatus.DisplayStatus = Res.Get<Labels>().PromptMessageCannotEdit;
        }
        approvalWorkflowState = approvalWorkflowItem.GetWorkflowState(culture);
      }
      return InlineEditingHelper.ApplyWorkflowRestrictions(lifecycleStatus, approvalWorkflowState);
    }

    private static LifecycleStatusModel GetContentLifecycleStatus(
      ILifecycleDataItem lifecycleDataItem,
      ILifecycleManager manager,
      CultureInfo culture)
    {
      if (lifecycleDataItem == null)
        throw new ArgumentNullException(nameof (lifecycleDataItem));
      if (manager == null)
        throw new ArgumentNullException(nameof (manager));
      LifecycleSimpleInfo lifecycleSimpleInfo = lifecycleDataItem.GetLifecycleSimpleInfo(manager, culture);
      LifecycleStatusModel contentLifecycleStatus = new LifecycleStatusModel()
      {
        WorkflowStatus = lifecycleSimpleInfo.WorkflowStatus,
        DisplayStatus = lifecycleSimpleInfo.DisplayStatus,
        IsLocked = lifecycleSimpleInfo.IsLocked,
        IsLockedByMe = lifecycleSimpleInfo.IsLockedByMe,
        IsAdmin = ClaimsManager.GetCurrentIdentity().IsUnrestricted
      };
      contentLifecycleStatus.IsPublished = contentLifecycleStatus.WorkflowStatus == "Published";
      return contentLifecycleStatus;
    }

    private static LifecycleStatusModel GetDynamicContentLifecycleStatus(
      DynamicContent dynamicItem)
    {
      if (dynamicItem == null)
        throw new ArgumentNullException(nameof (dynamicItem));
      dynamicItem.PopulateLifecycleInformation();
      return new LifecycleStatusModel()
      {
        IsPublished = dynamicItem.Lifecycle.IsPublished,
        WorkflowStatus = dynamicItem.Lifecycle.WorkflowStatus,
        DisplayStatus = dynamicItem.Lifecycle.Message,
        IsAdmin = dynamicItem.Lifecycle.IsAdmin,
        IsLocked = dynamicItem.Lifecycle.IsLocked,
        IsLockedByMe = dynamicItem.Lifecycle.IsLockedByMe
      };
    }

    internal static LifecycleStatusModel ApplyWorkflowRestrictions(
      LifecycleStatusModel lifecycleStatus,
      string approvalWorkflowState)
    {
      lifecycleStatus.IsStatusEditable = true;
      if (!lifecycleStatus.IsPublished && approvalWorkflowState != "Draft" && approvalWorkflowState != "Published")
        lifecycleStatus.IsStatusEditable = false;
      return lifecycleStatus;
    }

    internal static LifecycleStatusModel GetPageControlLifecycleStatus(
      PageNode pageNode)
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      PageData pageData = pageNode.GetPageData();
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      LifecycleStatusModel lifecycleStatus = new LifecycleStatusModel();
      if (pageNode.NodeType == NodeType.Standard || pageNode.NodeType == NodeType.External)
      {
        lifecycleStatus.IsLocked = pageData.LockedBy != Guid.Empty;
        lifecycleStatus.IsLockedByMe = pageData.LockedBy == currentIdentity.UserId;
        lifecycleStatus.IsPublished = pageData.Status == ContentLifecycleStatus.Live;
        lifecycleStatus.LockedByUsername = CommonMethods.GetUserName(pageData.LockedBy);
      }
      InlineEditingHelper.SetDisplayStatus(pageNode, pageData, lifecycleStatus, (CultureInfo) null);
      if (string.IsNullOrEmpty(lifecycleStatus.WorkflowStatus))
      {
        lifecycleStatus.WorkflowStatus = "Draft";
        lifecycleStatus.DisplayStatus = Res.Get("PageResources", ContentUIStatus.Draft.ToString());
      }
      lifecycleStatus.IsEditable = InlineEditingHelper.IsPageEditable(pageNode) && InlineEditingHelper.IsContentEditable(pageNode);
      lifecycleStatus.IsAdmin = currentIdentity.IsUnrestricted;
      if (WorkflowManager.GetCurrentDecisions(pageNode.GetType(), string.Empty, pageNode.Id, culture) == null)
      {
        lifecycleStatus.IsEditable = false;
        lifecycleStatus.DisplayStatus = Res.Get<Labels>().PromptMessageCannotEdit;
      }
      return InlineEditingHelper.ApplyWorkflowRestrictions(lifecycleStatus, pageNode.GetWorkflowState());
    }

    private static void SetDisplayStatus(
      PageNode node,
      PageData data,
      LifecycleStatusModel lifecycleStatus,
      CultureInfo culture)
    {
      switch (node.NodeType)
      {
        case NodeType.Standard:
        case NodeType.External:
          InlineEditingHelper.SetStandardPageStatusText(node, data, lifecycleStatus, culture);
          break;
        case NodeType.Group:
          lifecycleStatus.WorkflowStatus = "Group";
          lifecycleStatus.DisplayStatus = Res.Get("PageResources", "Group");
          break;
        case NodeType.InnerRedirect:
        case NodeType.OuterRedirect:
        case NodeType.Rewriting:
          lifecycleStatus.WorkflowStatus = "Redirect";
          lifecycleStatus.DisplayStatus = string.Format(Res.Get<PageResources>().RedirectingPage);
          break;
      }
    }

    private static void SetStandardPageStatusText(
      PageNode node,
      PageData data,
      LifecycleStatusModel lifecycleStatus,
      CultureInfo culture)
    {
      string status;
      lifecycleStatus.DisplayStatus = InlineEditingHelper.GetStandardPageStatusText(node, data, out status, culture);
      lifecycleStatus.WorkflowStatus = status;
    }

    internal static string GetStandardPageStatusText(
      PageNode node,
      PageData data,
      out string status,
      CultureInfo culture)
    {
      string localizedStatus = node.GetLocalizedStatus(out status, out IStatusInfo _, culture);
      LifecycleExtensions.GetOverallStatus((ILifecycleDataItemLive) data, culture, ref status, ref localizedStatus);
      return localizedStatus;
    }

    internal static bool IsPageEditable(PageNode page) => page.IsEditable("Pages", "Modify");

    internal static bool IsContentEditable(PageNode page) => page.IsGranted("Pages", "EditContent");
  }
}
