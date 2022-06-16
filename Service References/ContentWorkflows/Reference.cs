// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentWorkflows.ServiceClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Telerik.Sitefinity.ContentWorkflows
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class ServiceClient : ClientBase<IService>, IService
  {
    public ServiceClient()
    {
    }

    public ServiceClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public ServiceClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public ServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public ServiceClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    public string SendMessage(
      string operationName,
      bool isCheckedOut,
      Guid workflowDefinitionId,
      Guid contentId,
      string providerName,
      Dictionary<string, string> contextBag)
    {
      return this.Channel.SendMessage(operationName, isCheckedOut, workflowDefinitionId, contentId, providerName, contextBag);
    }
  }
}
