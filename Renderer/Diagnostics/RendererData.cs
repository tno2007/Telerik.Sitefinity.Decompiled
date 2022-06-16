// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Diagnostics.RendererData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Renderer.Diagnostics
{
  /// <summary>The data submitted through the renderer application.</summary>
  public class RendererData
  {
    /// <summary>Gets or sets the url.</summary>
    [DataMember]
    public string Url { get; set; }

    /// <summary>Gets or sets the authentication redirect url.</summary>
    [DataMember]
    public string AuthRedirectUrl { get; set; }

    /// <summary>Gets or sets the sitefinity url.</summary>
    [DataMember]
    public string SitefinityUrl { get; set; }

    /// <summary>Gets or sets the web service path.</summary>
    [DataMember]
    public string WebService { get; set; }

    /// <summary>Gets or sets the host header name.</summary>
    [DataMember]
    public string HostHeaderName { get; set; }

    /// <summary>Gets or sets the identity server host header name.</summary>
    [DataMember]
    public string IdentityServerHostHeaderValue { get; set; }
  }
}
