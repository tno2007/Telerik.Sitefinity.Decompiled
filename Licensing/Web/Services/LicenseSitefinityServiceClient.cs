// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.Services.LicenseSitefinityServiceClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Channels;
using Telerik.Licensing.DataContracts.Base;
using Telerik.Licensing.ServiceContracts.Sitefinity;

namespace Telerik.Sitefinity.Licensing.Web.Services
{
  /// <summary>Sitefinity license server client</summary>
  public class LicenseSitefinityServiceClient : 
    ClientBase<ILicenseSitefinityService>,
    ILicenseSitefinityService
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Licensing.Web.Services.LicenseSitefinityServiceClient" /> class.
    /// </summary>
    /// <param name="binding">The binding.</param>
    /// <param name="remoteAddress">The remote address end point.</param>
    public LicenseSitefinityServiceClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    /// <summary>Activates the sitefinity.</summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="licenseCode">The license code.</param>
    /// <param name="version">The version.</param>
    /// <param name="productCode">The product code.</param>
    /// <param name="isTrial">if set to <c>true</c> [is trial].</param>
    /// <returns>The license string</returns>
    public string ActivateSitefinity(
      string userName,
      string password,
      string licenseCode,
      string version,
      string productCode,
      bool isTrial)
    {
      return this.Channel.ActivateSitefinity(userName, password, licenseCode, version, productCode, isTrial);
    }

    /// <summary>Activates the sitefinity.</summary>
    /// <param name="userCredential">Credentials of the user.</param>
    /// <param name="licenseCode">The license code.</param>
    /// <param name="version">The version.</param>
    /// <param name="productCode">The product code.</param>
    /// <returns>The license string</returns>
    public string ActivateSitefinityLicense(
      UserCredential userCredential,
      string licenseCode,
      string version,
      string productCode)
    {
      return this.Channel.ActivateSitefinityLicense(userCredential, licenseCode, version, productCode);
    }

    /// <summary>Activates the sitefinity as trial.</summary>
    /// <param name="userCredential">Credentials of the user.</param>
    /// <param name="licenseCode">The license code.</param>
    /// <param name="version">The version.</param>
    /// <param name="productCode">The product code.</param>
    /// <returns>The license string</returns>
    public string ActivateSitefinityTrialLicense(
      UserCredential userCredential,
      string licenseCode,
      string version,
      string productCode)
    {
      return this.Channel.ActivateSitefinityTrialLicense(userCredential, licenseCode, version, productCode);
    }

    /// <summary>Gets the sitefinity license.</summary>
    /// <param name="licenseId">The license id.</param>
    /// <param name="productVersion">The product version.</param>
    /// <returns>The license string</returns>
    public string GetSitefinityLicense(string licenseId, string productVersion) => this.Channel.GetSitefinityLicense(licenseId, productVersion);
  }
}
