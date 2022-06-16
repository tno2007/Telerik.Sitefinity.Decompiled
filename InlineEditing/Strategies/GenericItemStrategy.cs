// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Strategies.GenericItemStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services.InlineEditing;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.InlineEditing.Strategies
{
  /// <summary>
  /// Class that provides the functionality to manipulate(get/update/delete) items, without workflow, for the purpose of inline editing.
  /// (Shared content, Lists)
  /// </summary>
  internal class GenericItemStrategy : IInlineEditingStrategy
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
      Type type,
      string provider,
      InlineEditingOperation workflowOperation,
      string customWorkflowOperation)
    {
      WorkflowOperationResultModel operationResultModel = new WorkflowOperationResultModel();
      IManager mappedManager = ManagerBase.GetMappedManager(type, provider);
      object obj = mappedManager.GetItem(type, id);
      bool deleteTemp = workflowOperation.ToString().ToLower() == "publish";
      if (obj is ILifecycleDataItem)
      {
        ILifecycleDataItem lifecycleDataItem1 = obj as ILifecycleDataItem;
        ILifecycleManager lifecycleManager = mappedManager as ILifecycleManager;
        VersionManager versionManager = (VersionManager) null;
        ILifecycleDataItem lifecycleDataItem2 = lifecycleManager.Lifecycle.CheckIn(lifecycleDataItem1, deleteTemp: deleteTemp);
        ++lifecycleDataItem2.Version;
        if (lifecycleDataItem2 is IVersionSerializable)
        {
          versionManager = VersionManager.GetManager();
          versionManager.CreateVersion((IDataItem) lifecycleDataItem2, true);
        }
        if (deleteTemp)
          lifecycleManager.Lifecycle.Publish(lifecycleManager.Lifecycle.GetMaster(lifecycleDataItem2));
        lifecycleManager.SaveChanges();
        versionManager?.SaveChanges();
      }
      operationResultModel.RequiresPageOperation = false;
      operationResultModel.IsExecutedSuccessfully = true;
      return operationResultModel;
    }

    /// <inheritdoc />
    public virtual Guid GetEditableItemId(Guid id, Type type, string provider)
    {
      ILifecycleManager mappedManager = ManagerBase.GetMappedManager(type, provider) as ILifecycleManager;
      object obj = mappedManager.GetItem(type, id);
      Guid editableItemId = new Guid();
      switch (obj)
      {
        case ILifecycleDataItem _:
          ILifecycleDataItem cnt = obj as ILifecycleDataItem;
          ILifecycleDataItem lifecycleDataItem = mappedManager.Lifecycle.GetTemp(cnt);
          if (lifecycleDataItem == null || lifecycleDataItem.Owner == Guid.Empty)
          {
            ILifecycleDataItem master = mappedManager.Lifecycle.GetMaster(cnt);
            lifecycleDataItem = mappedManager.Lifecycle.CheckOut(master);
            mappedManager.SaveChanges();
          }
          editableItemId = lifecycleDataItem.Id;
          break;
        case IContentLifecycle _:
          editableItemId = id;
          break;
      }
      return editableItemId;
    }

    /// <inheritdoc />
    public virtual void SaveEditableItemFields(
      FieldValueModel[] fields,
      Guid id,
      Type type,
      string provider)
    {
      ILifecycleManager mappedManager = ManagerBase.GetMappedManager(type, provider) as ILifecycleManager;
      object obj = mappedManager.GetItem(type, id);
      if (obj is ILifecycleDataItem)
      {
        ILifecycleDataItem lifecycleDataItem1 = mappedManager.GetItem(type, id) as ILifecycleDataItem;
        if (lifecycleDataItem1.Status == ContentLifecycleStatus.Master)
          obj = (object) mappedManager.Lifecycle.CheckOut(lifecycleDataItem1);
        else if (lifecycleDataItem1.Status == ContentLifecycleStatus.Live)
        {
          ILifecycleDataItem lifecycleDataItem2 = mappedManager.Lifecycle.Edit(lifecycleDataItem1);
          obj = (object) mappedManager.Lifecycle.CheckOut(lifecycleDataItem2);
        }
      }
      InlineEditingHelper.SetItemFields(obj, fields);
      mappedManager.SaveChanges();
    }

    /// <inheritdoc />
    public virtual void DeleteEditableItem(Guid id, Type type, string provider)
    {
      if (!(ManagerBase.GetMappedManager(type, provider) is ILifecycleManager mappedManager))
        return;
      ILifecycleDataItem cnt = mappedManager.GetItem(type, id) as ILifecycleDataItem;
      Guid currentUserId = SecurityManager.CurrentUserId;
      bool flag1 = ClaimsManager.IsUnrestricted();
      if (cnt == null)
        return;
      Guid masterId = mappedManager.Lifecycle.GetMaster(cnt).Id;
      IEnumerable<ILifecycleDataItemGeneric> lifecycleDataItemGenerics = mappedManager.GetItems(type, (string) null, (string) null, 0, 0).OfType<ILifecycleDataItemGeneric>().Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (i => i.Status == ContentLifecycleStatus.Temp && i.OriginalContentId == masterId));
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
    public virtual object GetItem(Guid id, Type type, string provider) => (ManagerBase.GetMappedManager(type, provider) as ILifecycleManager).GetItem(type, id);

    /// <inheritdoc />
    public virtual LifecycleStatusModel GetItemStatus(object item)
    {
      LifecycleStatusModel itemStatus = new LifecycleStatusModel()
      {
        DisplayStatus = string.Empty,
        IsEditable = true,
        IsLocked = false,
        IsLockedByMe = false,
        IsStatusEditable = true
      };
      if (item is ILifecycleDataItem lifecycleDataItem)
      {
        string name = (lifecycleDataItem.Provider as IDataProviderBase).Name;
        ILifecycleManager mappedManager = ManagerBase.GetMappedManager(item.GetType(), name) as ILifecycleManager;
        itemStatus = InlineEditingHelper.GetItemLifecycleStatus(lifecycleDataItem, mappedManager);
      }
      itemStatus.IsPublished = true;
      return itemStatus;
    }

    /// <inheritdoc />
    public virtual string GetDetailsViewUrl(Type itemType, PageNode pageNode)
    {
      DetailFormViewElement detailFormViewElement = CustomFieldsContext.GetViews(itemType.FullName).Where<DetailFormViewElement>((Func<DetailFormViewElement, bool>) (v => v.DisplayMode == FieldDisplayMode.Write && v.ViewName.ToLower().Contains("edit"))).First<DetailFormViewElement>();
      return VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ContentViewEditDialog?" + "ControlDefinitionName=" + detailFormViewElement.ControlDefinitionName + "&ViewName=" + detailFormViewElement.ViewName + "&IsInlineEditingMode=true");
    }
  }
}
