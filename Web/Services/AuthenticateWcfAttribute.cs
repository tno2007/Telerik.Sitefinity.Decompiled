// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.AuthenticateWcfAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Telerik.Sitefinity.Web.Services
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
  internal class AuthenticateWcfAttribute : 
    Attribute,
    IEndpointBehavior,
    IServiceBehavior,
    IDispatchMessageInspector
  {
    private bool needAdminRights;
    private bool demandBackendUser;

    public AuthenticateWcfAttribute(bool needAdminRights)
      : this(needAdminRights, false)
    {
    }

    public AuthenticateWcfAttribute(bool needAdminRights, bool demandBackendUser)
    {
      this.needAdminRights = needAdminRights;
      this.demandBackendUser = demandBackendUser;
    }

    public void AddBindingParameters(
      ServiceEndpoint endpoint,
      BindingParameterCollection bindingParameters)
    {
    }

    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
    }

    public void ApplyDispatchBehavior(
      ServiceEndpoint endpoint,
      EndpointDispatcher endpointDispatcher)
    {
      endpointDispatcher.DispatchRuntime.MessageInspectors.Add((IDispatchMessageInspector) this);
    }

    public void Validate(ServiceEndpoint endpoint)
    {
    }

    public void AddBindingParameters(
      ServiceDescription serviceDescription,
      ServiceHostBase serviceHostBase,
      Collection<ServiceEndpoint> endpoints,
      BindingParameterCollection bindingParameters)
    {
    }

    public void ApplyDispatchBehavior(
      ServiceDescription serviceDescription,
      ServiceHostBase serviceHostBase)
    {
      foreach (ServiceEndpoint endpoint in (Collection<ServiceEndpoint>) serviceDescription.Endpoints)
      {
        if (endpoint.Behaviors.Find<AuthenticateWcfAttribute>() != null)
          endpoint.Behaviors.Remove<AuthenticateWcfAttribute>();
        endpoint.Behaviors.Add((IEndpointBehavior) this);
      }
    }

    public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
    }

    public object AfterReceiveRequest(
      ref Message request,
      IClientChannel channel,
      InstanceContext instanceContext)
    {
      ServiceUtility.RequestAuthentication(this.needAdminRights, this.demandBackendUser);
      return (object) null;
    }

    public void BeforeSendReply(ref Message reply, object correlationState)
    {
    }
  }
}
