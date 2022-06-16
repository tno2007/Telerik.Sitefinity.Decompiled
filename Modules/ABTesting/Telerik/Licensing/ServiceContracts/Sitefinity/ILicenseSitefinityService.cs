// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.ServiceContracts.Sitefinity.ILicenseSitefinityService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.CodeDom.Compiler;
using System.ServiceModel;
using Telerik.Licensing.DataContracts.Base;

namespace Telerik.Licensing.ServiceContracts.Sitefinity
{
  /// <summary>Sitefinity licensing server interface</summary>
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [ServiceContract(ConfigurationName = "ILicenseSitefinityService")]
  public interface ILicenseSitefinityService
  {
    /// <summary>Activates the sitefinity.</summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="licenseCode">The license code.</param>
    /// <param name="version">The version.</param>
    /// <param name="productCode">The product code.</param>
    /// <param name="isTrial">if set to <c>true</c> [is trial].</param>
    /// <returns>The license string</returns>
    [OperationContract(Action = "http://tempuri.org/ILicenseSitefinityService/ActivateSitefinity", ReplyAction = "http://tempuri.org/ILicenseSitefinityService/ActivateSitefinityResponse")]
    [FaultContract(typeof (string), Action = "http://tempuri.org/ILicenseSitefinityService/ActivateSitefinityFaultString", Name = "string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/")]
    string ActivateSitefinity(
      string userName,
      string password,
      string licenseCode,
      string version,
      string productCode,
      bool isTrial);

    /// <summary>Activates the sitefinity.</summary>
    /// <param name="userCredential">Credentials of the user.</param>
    /// <param name="licenseCode">The license code.</param>
    /// <param name="version">The version.</param>
    /// <param name="productCode">The product code.</param>
    /// <returns>The license string</returns>
    [FaultContract(typeof (string))]
    [OperationContract]
    string ActivateSitefinityLicense(
      UserCredential userCredential,
      string licenseCode,
      string version,
      string productCode);

    /// <summary>Activates the sitefinity as trial.</summary>
    /// <param name="userCredential">Credentials of the user.</param>
    /// <param name="licenseCode">The license code.</param>
    /// <param name="version">The version.</param>
    /// <param name="productCode">The product code.</param>
    /// <returns>The license string</returns>
    [FaultContract(typeof (string))]
    [OperationContract]
    string ActivateSitefinityTrialLicense(
      UserCredential userCredential,
      string licenseCode,
      string version,
      string productCode);

    /// <summary>Gets the sitefinity license.</summary>
    /// <param name="licenseId">The license id.</param>
    /// <param name="productVersion">The product version.</param>
    /// <returns>The license string</returns>
    [OperationContract(Action = "http://tempuri.org/ILicenseSitefinityService/GetSitefinityLicense", ReplyAction = "http://tempuri.org/ILicenseSitefinityService/DummyMethodSitefinityResponse")]
    [FaultContract(typeof (string), Action = "http://tempuri.org/ILicenseSitefinityService/GetSitefinityLicenseStringFault", Name = "string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/")]
    string GetSitefinityLicense(string licenseId, string productVersion);
  }
}
