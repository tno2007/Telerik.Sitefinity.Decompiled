// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.Services.ILicenseService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Telerik.Sitefinity.Licensing.Web.Services
{
  [ServiceContract(Name = "LicensingService", Namespace = "http://services.telerik.com", ProtectionLevel = ProtectionLevel.None)]
  [ServiceKnownType(typeof (LicenseResponse))]
  [ServiceKnownType(typeof (UpgradeVersionInfo))]
  public interface ILicenseService
  {
    /// <summary>Gets the license contents.</summary>
    /// <param name="licenseId">The license pageId.</param>
    /// <param name="productVersion">The product version.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Xml)]
    LicenseResponse GetLicense(string licenseId, string productVersion);

    /// <summary>Gets the available upgrades.</summary>
    /// <param name="licenseId">The license pageId.</param>
    /// <param name="productVersion">The product version.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Xml)]
    UpgradeVersionInfo[] GetAvailableUpgrades(
      string licenseId,
      string productVersion);
  }
}
