// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.WcfErrorHandlingBehavior
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Behavior that attaches error handler and processes unhandled exceptions in WCF services
  /// </summary>
  internal class WcfErrorHandlingBehavior : IServiceBehavior, IErrorHandler
  {
    /// <summary>Adds the binding parameters.</summary>
    /// <param name="serviceDescription">The service description.</param>
    /// <param name="serviceHostBase">The service host base.</param>
    /// <param name="endpoints">The endpoints.</param>
    /// <param name="bindingParameters">The binding parameters.</param>
    public void AddBindingParameters(
      ServiceDescription serviceDescription,
      ServiceHostBase serviceHostBase,
      Collection<ServiceEndpoint> endpoints,
      BindingParameterCollection bindingParameters)
    {
    }

    /// <summary>
    /// Provides the ability to change run-time property values or insert custom
    /// extension objects such as error handlers, message or parameter interceptors,
    /// security extensions, and other custom extension objects.
    /// </summary>
    /// <param name="serviceDescription">The service description.</param>
    /// <param name="serviceHostBase">The host that is currently being built.</param>
    public void ApplyDispatchBehavior(
      ServiceDescription serviceDescription,
      ServiceHostBase serviceHostBase)
    {
      foreach (ChannelDispatcher channelDispatcher in (SynchronizedCollection<ChannelDispatcherBase>) serviceHostBase.ChannelDispatchers)
        channelDispatcher.ErrorHandlers.Add((IErrorHandler) this);
    }

    /// <summary>
    /// Provides the ability to inspect the service host and the service description
    /// to confirm that the service can run successfully.
    /// </summary>
    /// <param name="serviceDescription">The service description.</param>
    /// <param name="serviceHostBase">The service host that is currently being constructed.</param>
    public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
    }

    /// <summary>
    /// Enables error-related processing and returns a value that indicates
    /// whether the dispatcher aborts the session and the instance context in certain
    /// cases.
    /// </summary>
    /// <param name="error">The exception thrown during processing.</param>
    /// <returns>
    /// true if  should not abort the session (if there is one) and instance
    /// context if the instance context is not <see cref="F:System.ServiceModel.InstanceContextMode.Single" />;
    /// otherwise, false. The default is false.
    /// </returns>
    public bool HandleError(Exception error)
    {
      Exceptions.HandleException(error, ExceptionPolicyName.UnhandledExceptions);
      return false;
    }

    /// <summary>
    /// Enables the creation of a custom <see cref="T:System.ServiceModel.FaultException`1" />
    /// that is returned from an exception in the course of a service method.
    /// </summary>
    /// <param name="error">The <see cref="T:System.Exception" /> object thrown in the
    /// course of the service operation.</param>
    /// <param name="version">The SOAP version of the message.</param>
    /// <param name="fault">The <see cref="T:System.ServiceModel.Channels.Message" />
    /// object that is returned to the client, or service, in the duplex case.</param>
    public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
    {
    }
  }
}
