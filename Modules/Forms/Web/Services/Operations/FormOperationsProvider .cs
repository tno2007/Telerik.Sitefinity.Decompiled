// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.Services.Operations.FormOperationsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.Modules.Forms.Web.Services.Operations
{
  internal class FormOperationsProvider : IOperationProvider
  {
    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if (!typeof (FormDraft).IsAssignableFrom(clrType))
        return Enumerable.Empty<OperationData>();
      OperationData operationData1 = OperationData.Create<object>(new Func<OperationContext, object>(this.GetTempForm));
      operationData1.OperationType = OperationType.PerItem;
      OperationData operationData2 = OperationData.Create<IEnumerable<FormRule>>(new Action<OperationContext, IEnumerable<FormRule>>(this.SaveTempForm));
      operationData2.OperationType = OperationType.PerItem;
      operationData2.IsRead = false;
      return (IEnumerable<OperationData>) new OperationData[2]
      {
        operationData1,
        operationData2
      };
    }

    public object GetTempForm(OperationContext context)
    {
      Guid key = (Guid) context.Data["key"];
      FormDraft formDraft = (FormDraft) context.GetStrategy().GetItem(key);
      return formDraft != null ? (object) formDraft.ParentItem.GetTempItem() : (object) null;
    }

    public void SaveTempForm(OperationContext context, IEnumerable<FormRule> rules)
    {
      string str = rules == null || !rules.Any<FormRule>() ? (string) null : JsonConvert.SerializeObject((object) rules);
      Guid id = (Guid) context.Data["key"];
      if (rules == null)
        return;
      FormsManager manager = FormsManager.GetManager();
      manager.GetDraft(id).Rules = str;
      manager.SaveChanges();
    }
  }
}
