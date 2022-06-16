// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.DefaultServiceHost
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.ObjectModel;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using Telerik.Sitefinity.SiteSettings.Web.Services;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>Zero configuration service host for the WCF services.</summary>
  public class DefaultServiceHost : WebServiceHost2
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.DefaultServiceHost" /> class.
    /// </summary>
    /// <param name="singletonInstance">The singleton instance.</param>
    /// <param name="baseAddresses">The base addresses.</param>
    public DefaultServiceHost(object singletonInstance, params Uri[] baseAddresses)
      : base(singletonInstance, baseAddresses)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.DefaultServiceHost" /> class.
    /// </summary>
    /// <param name="serviceType">The service type.</param>
    /// <param name="baseAddresses">The base address of the service.</param>
    public DefaultServiceHost(System.Type serviceType, params Uri[] baseAddresses)
      : base(serviceType, false, baseAddresses)
    {
      if (typeof (IBasicSettingsService).IsAssignableFrom(serviceType))
      {
        SettingsContractTypeResolver.RegisterServiceHost((WebServiceHost) this);
      }
      else
      {
        if (!(serviceType.FullName == "Telerik.Sitefinity.Modules.Ecommerce.Catalog.Web.Services.ProductService"))
          return;
        TypeResolutionService.ResolveType("Telerik.Sitefinity.Modules.Ecommerce.Catalog.Web.Services.ProductTypeResolver, Telerik.Sitefinity.Ecommerce").GetMethod("RegisterProductServiceHost").Invoke((object) null, new object[1]
        {
          (object) this
        });
      }
    }

    /// <summary>
    /// This method will be called when the host is being opened.
    /// </summary>
    protected override void OnOpening()
    {
      base.OnOpening();
      this.Description.Behaviors.Add((IServiceBehavior) new WcfErrorHandlingBehavior());
      foreach (ServiceEndpoint endpoint in (Collection<ServiceEndpoint>) this.Description.Endpoints)
      {
        if (endpoint.Behaviors.Find<LocalizationBehavior>() != null)
          endpoint.Behaviors.Remove<LocalizationBehavior>();
        endpoint.Behaviors.Add((IEndpointBehavior) new LocalizationBehavior());
        HttpTransportBindingElement transportBindingElement = ((CustomBinding) endpoint.Binding).Elements.Find<HttpTransportBindingElement>();
        if (transportBindingElement != null)
          transportBindingElement.AuthenticationScheme = AuthenticationSchemes.Anonymous;
      }
    }
  }
}
