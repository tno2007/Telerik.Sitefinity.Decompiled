// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.WcfHostFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>WCF host factory</summary>
  public class WcfHostFactory : ServiceHostFactory
  {
    /// <summary>
    /// Creates a <see cref="T:System.ServiceModel.ServiceHost" /> for a specified type of service with a specific base address.
    /// </summary>
    /// <param name="serviceType">Specifies the type of service to host.</param>
    /// <param name="baseAddresses">The <see cref="T:System.Array" /> of type <see cref="T:System.Uri" /> that contains the base addresses for the service hosted.</param>
    /// <returns>
    /// A <see cref="T:System.ServiceModel.ServiceHost" /> for the type of service specified with a specific base address.
    /// </returns>
    protected override ServiceHost CreateServiceHost(
      Type serviceType,
      Uri[] baseAddresses)
    {
      return (ServiceHost) new DefaultServiceHost(serviceType, baseAddresses);
    }
  }
}
