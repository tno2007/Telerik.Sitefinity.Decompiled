// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.Services.LicenseUpdater
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Licensing.DataContracts.Base;
using Telerik.Sitefinity.Licensing.Web.Services.LicenseServerExceptions;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Licensing.Web.Services
{
  /// <summary>
  /// API for downloading license keys from the Sitefinity license server
  /// </summary>
  public static class LicenseUpdater
  {
    private const string SitefinityProductCode = "SF4.0";

    private static Binding GetLicenseBinding()
    {
      BasicHttpBinding licenseBinding = new BasicHttpBinding();
      licenseBinding.CloseTimeout = TimeSpan.Parse("00:01:00");
      licenseBinding.OpenTimeout = TimeSpan.Parse("00:01:00");
      licenseBinding.ReceiveTimeout = TimeSpan.Parse("00:10:00");
      licenseBinding.SendTimeout = TimeSpan.Parse("00:01:00");
      licenseBinding.AllowCookies = false;
      licenseBinding.BypassProxyOnLocal = false;
      licenseBinding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
      licenseBinding.MaxBufferSize = 65536;
      licenseBinding.MaxBufferPoolSize = 524288L;
      licenseBinding.MaxReceivedMessageSize = 65536L;
      licenseBinding.MessageEncoding = WSMessageEncoding.Text;
      licenseBinding.TextEncoding = Encoding.UTF8;
      licenseBinding.TransferMode = TransferMode.Buffered;
      licenseBinding.UseDefaultWebProxy = true;
      licenseBinding.ReaderQuotas.MaxDepth = 32;
      licenseBinding.ReaderQuotas.MaxStringContentLength = 32768;
      licenseBinding.ReaderQuotas.MaxArrayLength = 16384;
      licenseBinding.ReaderQuotas.MaxBytesPerRead = 4096;
      licenseBinding.ReaderQuotas.MaxNameTableCharCount = 16384;
      licenseBinding.Security.Mode = !LicenseUpdater.GetLicenseServiceUrl().StartsWith("https:") ? BasicHttpSecurityMode.None : BasicHttpSecurityMode.Transport;
      licenseBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
      licenseBinding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
      licenseBinding.Security.Transport.ExtendedProtectionPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);
      licenseBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
      licenseBinding.Security.Message.AlgorithmSuite = SecurityAlgorithmSuite.Default;
      return (Binding) licenseBinding;
    }

    /// <summary>Gets a client channel to the license service.</summary>
    /// <returns>The client channel</returns>
    private static LicenseSitefinityServiceClientWrapper GetLicenseService()
    {
      EndpointAddress remoteAddress = new EndpointAddress(new Uri(LicenseUpdater.GetLicenseServiceUrl()), Array.Empty<AddressHeader>());
      return new LicenseSitefinityServiceClientWrapper(LicenseUpdater.GetLicenseBinding(), remoteAddress);
    }

    /// <summary>Gets the sitefinity license.</summary>
    /// <param name="licenseId">The license id.</param>
    /// <returns>The license</returns>
    public static string GetSitefinityLicense(string licenseId)
    {
      try
      {
        using (LicenseSitefinityServiceClientWrapper licenseService = LicenseUpdater.GetLicenseService())
          return licenseService.Client.GetSitefinityLicense(licenseId, LicenseUpdater.GetLicensingProductVersion());
      }
      catch (Exception ex)
      {
        LicenseUpdater.ResolveException(ex);
        throw ex;
      }
    }

    /// <summary>Activates the sitefinity.</summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="licenseCode">The license code.</param>
    /// <param name="isTrial">if set to <c>true</c> [is trial].</param>
    /// <returns>The license string</returns>
    public static string ActivateSitefinity(
      string userName,
      string password,
      string licenseCode,
      bool isTrial)
    {
      try
      {
        using (LicenseSitefinityServiceClientWrapper licenseService = LicenseUpdater.GetLicenseService())
        {
          UserCredential userCredential = new UserCredential()
          {
            Name = userName,
            Password = password
          };
          return isTrial ? licenseService.Client.ActivateSitefinityTrialLicense(userCredential, licenseCode, LicenseUpdater.GetLicensingProductVersion(), "SF4.0") : licenseService.Client.ActivateSitefinityLicense(userCredential, licenseCode, LicenseUpdater.GetLicensingProductVersion(), "SF4.0");
        }
      }
      catch (Exception ex)
      {
        LicenseUpdater.ResolveException(ex);
        throw ex;
      }
    }

    private static string GetLicensingProductVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

    private static void ResolveException(Exception ex)
    {
      if (ex is FaultException)
        LicenseUpdater.HandleFaultException((FaultException) ex);
      if (ex is CommunicationException)
        throw new GeneralServerProblem(ex);
    }

    private static void HandleFaultException(FaultException ex)
    {
      string message = ex.Message;
      Match match = new Regex("^(\\s*[1-9][0-9]*\\s*[:])").Match(message);
      if (!match.Success)
        throw new LicenseServerException(message);
      switch (int.Parse(match.Value.Replace(":", "").Trim()))
      {
        case 1:
          throw new LicenseServerException(Res.Get<LicensingMessages>().GeneralServerError, 1, (Exception) ex);
        case 2:
          throw new LicenseServerException(Res.Get<LicensingMessages>().GeneralServerError, 2, (Exception) ex);
        case 3:
          throw new LicenseServerException(Res.Get<LicensingMessages>().GeneralServerError, 3, (Exception) ex);
        case 4:
          throw new LicenseServerException(Res.Get<LicensingMessages>().ServerWrongCredentials, 4, (Exception) ex);
        case 5:
          throw new LicenseServerException(Res.Get<LicensingMessages>().GeneralServerError, 5, (Exception) ex);
        case 6:
          throw new LicenseServerException(Res.Get<LicensingMessages>().GeneralServerError, 6, (Exception) ex);
        case 7:
          throw new LicenseServerException(Res.Get<LicensingMessages>().ServerWrongNoAvailableLicense.Arrange((object) Res.Get<LicensingMessages>().ExternalLinkLicenseServerException), 7, (Exception) ex);
        case 8:
          throw new LicenseServerException(Res.Get<LicensingMessages>().ServerMoreThanOneLicense.Arrange((object) Res.Get<LicensingMessages>().ExternalLinkLicenseServerException), 8, (Exception) ex);
        case 9:
          throw new LicenseServerException(Res.Get<LicensingMessages>().GeneralServerError, 9, (Exception) ex);
        default:
          throw new LicenseServerException(message.Substring(match.Value.Length), (Exception) ex);
      }
    }

    /// <summary>Gets the license service URL.</summary>
    /// <returns>The license service URL</returns>
    public static string GetLicenseServiceUrl() => "https://www.telerik.com/services/LicenseService/Service.svc";
  }
}
