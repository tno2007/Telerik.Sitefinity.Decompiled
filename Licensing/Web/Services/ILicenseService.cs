// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.Services.LicenseResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Licensing.Web.Services
{
  /// <summary>License data + request result code if error messages</summary>
  [DataContract(Name = "LicenseResponse", Namespace = "http://services.telerik.com")]
  public class LicenseResponse
  {
    /// <summary>Gets or sets the response code.</summary>
    /// <value>The response code.</value>
    [DataMember]
    public LicenseRequestResult ResponseCode { get; set; }

    /// <summary>Gets or sets the response message.</summary>
    /// <value>The response message.</value>
    [DataMember]
    public string ResponseMessage { get; set; }

    /// <summary>Gets or sets the license data.</summary>
    /// <value>The license data.</value>
    [DataMember]
    public string LicenseData { get; set; }
  }
}
