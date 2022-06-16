﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.DeleteItemActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle.Fluent;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>
  /// Deletes the currently processed worfklow item or one of its language versions based on the context parameters.
  /// </summary>
  public class DeleteItemActivity : CodeActivity
  {
    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
      WorkflowDataContext dataContext = context.DataContext;
      IWorkflowItem workflowItem = (IWorkflowItem) dataContext.GetProperties()["workflowItem"].GetValue((object) dataContext);
      Dictionary<string, string> workflowContextBag = dataContext.GetProperties()["contextBag"].GetValue((object) dataContext) as Dictionary<string, string>;
      if (workflowItem == null)
        return;
      RelatedDataHelper.CheckRelatingData(workflowContextBag, workflowItem.Id, workflowItem.GetType().FullName);
      string name = (string) null;
      workflowContextBag.TryGetValue("Language", out name);
      CultureInfo cultureInfo = (CultureInfo) null;
      if (!string.IsNullOrEmpty(name))
        cultureInfo = new CultureInfo(name);
      bool flag = false;
      string str = (string) null;
      workflowContextBag.TryGetValue("SkipRecycleBin", out str);
      if (string.IsNullOrEmpty(str) || str.ToLowerInvariant() != "true")
      {
        Type itemType = TypeResolutionService.ResolveType(workflowItem.GetType().FullName);
        string providerName = dataContext.GetProperties()["providerName"].GetValue((object) dataContext) as string;
        flag = this.MoveToRecycleBin(itemType, workflowItem.Id, providerName, cultureInfo);
      }
      if (flag)
        return;
      MasterFacade masterFacade = dataContext.GetProperties()["masterFluent"].GetValue((object) dataContext) as MasterFacade;
      if (masterFacade.IsCheckedOut((CultureInfo) null))
        masterFacade = (dataContext.GetProperties()["fluent"].GetValue((object) dataContext) as TempFacade).CheckIn(true);
      masterFacade.Delete(cultureInfo).SaveChanges();
    }

    protected ISupportRecyclingManager GetDataItemRecyclingManager(
      Type itemType,
      string itemProviderName)
    {
      return (string.IsNullOrEmpty(SystemManager.CurrentContext.GlobalTransaction) ? ManagerBase.GetMappedManager(itemType, itemProviderName) : ManagerBase.GetManagerInTransaction(ManagerBase.GetMappedManagerType(itemType), itemProviderName, SystemManager.CurrentContext.GlobalTransaction)) as ISupportRecyclingManager;
    }

    private bool MoveToRecycleBin(
      Type itemType,
      Guid itemId,
      string providerName,
      CultureInfo language)
    {
      ISupportRecyclingManager recyclingManager = this.GetDataItemRecyclingManager(itemType, providerName);
      if (recyclingManager == null || !recyclingManager.RecycleBin.TryMoveToRecycleBin(itemType, itemId, recyclingManager, language))
        return false;
      if (string.IsNullOrEmpty(recyclingManager.TransactionName))
        recyclingManager.SaveChanges();
      return true;
    }
  }
}
