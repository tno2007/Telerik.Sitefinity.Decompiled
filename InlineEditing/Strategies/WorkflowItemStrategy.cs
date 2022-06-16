// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Strategies.WorkflowItemStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services.InlineEditing;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.InlineEditing.Strategies
{
  /// <summary>
  /// Class that provides the functionality to manipulate(get/update/delete) items, supporting workflow, for the purpose of inline editing.
  /// </summary>
  internal class WorkflowItemStrategy : IInlineEditingStrategy
  {
    /// <inheritdoc />
    public virtual LifecycleStatusModel GetItemStatus(
      Guid id,
      Type type,
      string provider)
    {
      return this.GetItemStatus(this.GetItem(id, type, provider));
    }

    /// <inheritdoc />
    public virtual WorkflowOperationResultModel ExecuteOperation(
      Guid id,
      Type itemType,
      string provider,
      InlineEditingOperation workflowOperation,
      string customWorkflowOperation)
    {
      WorkflowOperationResultModel operationResultModel = new WorkflowOperationResultModel();
      operationResultModel.RequiresPageOperation = false;
      operationResultModel.IsExecutedSuccessfully = true;
      ILifecycleManager mappedManager1 = ManagerBase.GetMappedManager(itemType, provider) as ILifecycleManager;
      ILifecycleDataItem cnt = mappedManager1.GetItem(itemType, id) as ILifecycleDataItem;
      itemType = cnt.GetType();
      Guid id1 = cnt.Id;
      Guid id2 = cnt.Id;
      if (cnt.Status != ContentLifecycleStatus.Master)
        id1 = mappedManager1.Lifecycle.GetMaster(cnt).Id;
      else
        id2 = mappedManager1.Lifecycle.GetTemp(cnt).Id;
      string operationToExecute = string.Empty;
      int num = InlineEditingHelper.IsWorkflowOperationValid(itemType, provider, id, workflowOperation.ToString(), customWorkflowOperation, out operationToExecute) ? 1 : 0;
      if (num == 0)
      {
        operationResultModel.IsExecutedSuccessfully = false;
        operationResultModel.ItemId = id1;
        IHasTitle hasTitle = cnt as IHasTitle;
        operationResultModel.ItemName = hasTitle != null ? hasTitle.GetTitle() : string.Empty;
        operationResultModel.ItemType = itemType.FullName;
        operationResultModel.ProviderName = provider;
        operationResultModel.DetailsViewUrl = this.GetDetailsViewUrl(itemType, (PageNode) null);
      }
      WorkflowManager.MessageWorkflow(id1, itemType, provider, operationToExecute, false, new Dictionary<string, string>()
      {
        {
          "ContentType",
          itemType.FullName
        }
      });
      if (num == 0)
        this.DeleteEditableItem(id2, itemType, provider);
      mappedManager1.Dispose();
      ILifecycleManager mappedManager2 = ManagerBase.GetMappedManager(itemType, provider) as ILifecycleManager;
      ILifecycleDataItem lifecycleDataItem = mappedManager2.GetItem(itemType, id1) as ILifecycleDataItem;
      operationResultModel.ItemStatus = InlineEditingHelper.GetItemLifecycleStatus(lifecycleDataItem, mappedManager2);
      return operationResultModel;
    }

    /// <inheritdoc />
    public virtual string GetDetailsViewUrl(Type itemType, PageNode pageNode)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      if (typeof (DynamicContent).IsAssignableFrom(itemType))
      {
        str1 = ModuleNamesGenerator.GenerateContentViewDefinitionName(itemType.FullName);
        str2 = ModuleNamesGenerator.GenerateBackendEditViewName(ModuleBuilderManager.GetManager().GetDynamicModuleType(itemType).DisplayName);
      }
      else
      {
        DetailFormViewElement detailFormViewElement = CustomFieldsContext.GetViews(itemType.FullName).Where<DetailFormViewElement>((Func<DetailFormViewElement, bool>) (v => !v.IsMasterView && v.DisplayMode == FieldDisplayMode.Write)).FirstOrDefault<DetailFormViewElement>();
        if (detailFormViewElement != null)
        {
          str1 = detailFormViewElement.ControlDefinitionName;
          str2 = detailFormViewElement.ViewName;
        }
      }
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("~/Sitefinity/Dialog/ContentViewEditDialog?");
      stringBuilder.Append("ControlDefinitionName=");
      stringBuilder.Append(str1);
      stringBuilder.Append("&ViewName=");
      stringBuilder.Append(str2);
      stringBuilder.Append("&");
      stringBuilder.Append("IsInlineEditingMode");
      stringBuilder.Append("=true");
      return VirtualPathUtility.ToAbsolute(stringBuilder.ToString());
    }

    /// <inheritdoc />
    public virtual Guid GetEditableItemId(Guid id, Type type, string provider)
    {
      ILifecycleManager mappedManager = ManagerBase.GetMappedManager(type, provider) as ILifecycleManager;
      ILifecycleDataItem cnt = mappedManager.GetItem(type, id) as ILifecycleDataItem;
      ILifecycleDataItem lifecycleDataItem = mappedManager.Lifecycle.GetTemp(cnt);
      if (lifecycleDataItem == null || lifecycleDataItem.Owner == Guid.Empty)
      {
        ILifecycleDataItem master = mappedManager.Lifecycle.GetMaster(cnt);
        lifecycleDataItem = mappedManager.Lifecycle.CheckOut(master);
        mappedManager.SaveChanges();
      }
      return lifecycleDataItem.Id;
    }

    /// <inheritdoc />
    public virtual void SaveEditableItemFields(
      FieldValueModel[] fields,
      Guid id,
      Type type,
      string provider)
    {
      IManager mappedManager = ManagerBase.GetMappedManager(type, provider);
      ILifecycleDataItem lifecycleDataItem = (ILifecycleDataItem) null;
      try
      {
        lifecycleDataItem = mappedManager.GetItem(type, id) as ILifecycleDataItem;
      }
      catch (ItemNotFoundException ex)
      {
      }
      if (lifecycleDataItem == null || lifecycleDataItem.Owner != SecurityManager.CurrentUserId)
        throw new NotSupportedException(Res.Get<ContentLifecycleMessages>().ItemUpdateWhileEditing);
      InlineEditingHelper.SetItemFields((object) lifecycleDataItem, fields);
      mappedManager.SaveChanges();
    }

    /// <inheritdoc />
    public virtual void DeleteEditableItem(Guid id, Type type, string provider)
    {
      ILifecycleManager mappedManager = ManagerBase.GetMappedManager(type, provider) as ILifecycleManager;
      if (!(mappedManager.GetItemOrDefault(type, id) is ILifecycleDataItem itemOrDefault))
        return;
      Guid currentUserId = SecurityManager.CurrentUserId;
      bool flag1 = ClaimsManager.IsUnrestricted();
      Guid masterId = mappedManager.Lifecycle.GetMaster(itemOrDefault).Id;
      IEnumerable<ILifecycleDataItemGeneric> lifecycleDataItemGenerics = mappedManager.GetItems(type, (string) null, (string) null, 0, 0).OfType<ILifecycleDataItemGeneric>().Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (i => (i.Status == ContentLifecycleStatus.Temp || i.Status == ContentLifecycleStatus.PartialTemp) && i.OriginalContentId == masterId));
      bool flag2 = false;
      foreach (ILifecycleDataItemGeneric lifecycleDataItemGeneric in lifecycleDataItemGenerics)
      {
        if (lifecycleDataItemGeneric.Owner == currentUserId | flag1)
        {
          mappedManager.DeleteItem((object) lifecycleDataItemGeneric);
          flag2 = true;
        }
      }
      if (!flag2)
        return;
      mappedManager.SaveChanges();
    }

    /// <inheritdoc />
    public virtual LifecycleStatusModel GetItemStatus(object item)
    {
      ILifecycleDataItem lifecycleDataItem = item as ILifecycleDataItem;
      string name = (lifecycleDataItem.Provider as IDataProviderBase).Name;
      return InlineEditingHelper.GetItemLifecycleStatus(lifecycleDataItem, ManagerBase.GetMappedManager(item.GetType(), name) as ILifecycleManager);
    }

    /// <inheritdoc />
    public virtual object GetItem(Guid id, Type type, string provider) => (object) ((ManagerBase.GetMappedManager(type, provider) as ILifecycleManager).GetItemOrDefault(type, id) as ILifecycleDataItem);
  }
}
