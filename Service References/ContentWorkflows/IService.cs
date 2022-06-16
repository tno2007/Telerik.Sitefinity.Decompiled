// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentWorkflows.IService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ServiceModel;

namespace Telerik.Sitefinity.ContentWorkflows
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(ConfigurationName = "ContentWorkflows.IService")]
  public interface IService
  {
    [OperationContract(Action = "http://tempuri.org/IService/SendMessage", ReplyAction = "http://tempuri.org/IService/SendMessageResponse")]
    [return: MessageParameter(Name = "itemStatus")]
    string SendMessage(
      string operationName,
      bool isCheckedOut,
      Guid workflowDefinitionId,
      Guid contentId,
      string providerName,
      Dictionary<string, string> contextBag);
  }
}
