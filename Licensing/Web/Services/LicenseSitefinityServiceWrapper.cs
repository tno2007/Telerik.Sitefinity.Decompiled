// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.Services.LicenseSitefinityServiceClientWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Telerik.Sitefinity.Licensing.Web.Services
{
  /// <summary>License server client wrapper</summary>
  internal sealed class LicenseSitefinityServiceClientWrapper : IDisposable
  {
    private LicenseSitefinityServiceClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Licensing.Web.Services.LicenseSitefinityServiceClientWrapper" /> class.
    /// </summary>
    /// <param name="bidning">The bidning.</param>
    /// <param name="remoteAddress">The remote address.</param>
    public LicenseSitefinityServiceClientWrapper(Binding bidning, EndpointAddress remoteAddress) => this.client = new LicenseSitefinityServiceClient(bidning, remoteAddress);

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      bool flag = false;
      try
      {
        if (this.client.State == CommunicationState.Faulted)
          return;
        this.client.Close();
        flag = true;
      }
      finally
      {
        if (!flag)
          this.client.Abort();
      }
    }

    /// <summary>Gets the client.</summary>
    /// <value>The client.</value>
    public LicenseSitefinityServiceClient Client => this.client;
  }
}
