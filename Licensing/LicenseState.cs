// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.LicenseState
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Licensing.Configuration;
using Telerik.Sitefinity.Licensing.Providers;
using Telerik.Sitefinity.Licensing.Update;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Licensing
{
  /// <summary>
  /// keeps information about the current application licensing mode
  /// </summary>
  public sealed class LicenseState
  {
    internal static readonly ConcurrentProperty<LicenseState> currentLicense = new ConcurrentProperty<LicenseState>(new Func<LicenseState>(LicenseState.BuildCurrentLicense));
    private const string WwwAbbreviation = "www.";
    /// <summary>
    /// This is the grace period in which Sitefinity will keep working normally after the recurring billing monthly license has expired.
    /// The value is in days and begins when the current license expires.
    /// </summary>
    internal const int RecurringBillingGracePeriodInDays = 30;
    /// <summary>
    /// This is the period in which Sitefinity will work in a trial state after the recurring billing monthly license and grace period have expired.
    /// The value is in days and begins when the grace period ends.
    /// </summary>
    internal const int RecurringBillingTrialPeriodInDays = 30;
    /// <summary>
    /// This is the total period after which Sitefinity will stop working if the current monthly license is not renewed.
    /// This is the sum of the grace period and the trial period. The value is in days.
    /// </summary>
    internal const int RecurringBillingInvalidateLicensePeriod = 60;
    private Func<DateTime> currentDateTimeFunc;
    private static readonly object updateLicenseLock = new object();
    private static readonly TimeSpan updateCheckInterval = new TimeSpan(24, 0, 0);
    private static DateTime lastUpdateCheck;
    private static ILicenseUpdateStrategy updateStrategy = (ILicenseUpdateStrategy) new LicenseUpdateStrategy();

    internal LicenseState()
    {
    }

    /// <summary>
    /// Gets a value indicating whether license data is ok, without checking the validity of the domain.
    /// </summary>
    public bool IsLicenseDataOk => !this.MissingLicense && !this.InvalidProductVersion && !this.InvalidLicenseInfo && !this.InvalidSignature;

    /// <summary>
    /// Gets a value indicating whether the license state is trial mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is in trial mode; otherwise, <c>false</c>.
    /// </value>
    public bool IsInTrialMode
    {
      get
      {
        if (TestContext.Testing && TestContext.Attributes["LicenseState.IsInTrialMode"] == "true")
          return TestContext.GetMock<bool>("LicenseState.IsInTrialMode");
        if (!this.IsLicenseDataOk || this.InvalidDomain || this.LicenseInfo.IsTrial)
          return true;
        return this.LicenseInfo.IsRecurringBilling && this.GetCurrentTime() > this.LicenseInfo.ExpirationDate.AddDays(30.0);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether license product version is wrong.
    /// </summary>
    /// <value><c>true</c> if wrong product version otherwise, <c>false</c>.</value>
    public bool InvalidProductVersion { get; internal set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is valid domain.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is valid domain; otherwise, <c>false</c>.
    /// </value>
    public bool InvalidDomain => TestContext.Testing && TestContext.Attributes["LicenseState.InvalidDomain"] == "true" ? TestContext.GetMock<bool>("LicenseState.InvalidDomain") : !LicenseState.CheckCurrentDomain(this.LicenseInfo.AllLicensedDomains, this.LicenseInfo.AllowSubDomains);

    /// <summary>
    /// Gets or sets a value indicating whether the license file was missing .
    /// </summary>
    /// <value><c>true</c> if missing license otherwise, <c>false</c>.</value>
    public bool MissingLicense { get; internal set; }

    /// <summary>
    /// Gets or sets a value indicating whether the license has invalid signature
    /// </summary>
    /// <value><c>true</c> if [invalid signature]; otherwise, <c>false</c>.</value>
    public bool InvalidSignature { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether the license info is invalid
    /// </summary>
    /// <value><c>true</c> if [invalid license info]; otherwise, <c>false</c>.</value>
    public bool InvalidLicenseInfo => this.LicenseInfo == null || !this.LicenseInfo.IsValid || this.LicenseInfo.IsCorrupted;

    internal bool HasExpired
    {
      get
      {
        if (this.LicenseInfo.IsRecurringBilling)
        {
          this.UpdateExpiredLicense();
          return this.GetCurrentTime() > this.LicenseInfo.ExpirationDate.AddDays(60.0);
        }
        return (this.LicenseInfo.IsTrial || this.LicenseInfo.IsHosted) && this.GetCurrentTime() > this.LicenseInfo.ExpirationDate;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the system is runnign without a valid license.
    /// This includes: no license, corrupted license and expired trial license
    /// </summary>
    /// <value><c>true</c> if [invalid license]; otherwise, <c>false</c>.</value>
    public bool InvalidLicense => !this.IsLicenseDataOk || this.HasExpired;

    /// <summary>Gets or sets the license info.</summary>
    /// <value>The license info.</value>
    public LicenseInfo LicenseInfo { get; internal set; }

    /// <summary>Gets the current.</summary>
    /// <value>The current.</value>
    public static LicenseState Current => TestContext.Testing && TestContext.Attributes["LicenseState.Current"] == "true" ? TestContext.GetMock<LicenseState>("LicenseState.Current") : LicenseState.currentLicense.Value;

    /// <summary>Gets the current request domain.</summary>
    /// <returns></returns>
    internal static string GetCurrentRequestDomain => SystemManager.CurrentHttpContext != null ? SystemManager.CurrentHttpContext.Request.Url.Host : (string) null;

    /// <summary>Gets the type of the current host.</summary>
    /// <value>The type of the get current host.</value>
    internal static UriHostNameType GetCurrentHostType => SystemManager.CurrentHttpContext.Request.Url.HostNameType;

    /// <summary>Gets the current installation product version.</summary>
    /// <returns></returns>
    internal static Version GetProductVersion => TestContext.Testing && TestContext.Attributes["LicenseState.GetProductVersion"] == "true" ? TestContext.GetMock<Version>("LicenseState.GetProductVersionResult") : Assembly.GetExecutingAssembly().GetName().Version;

    /// <summary>
    /// Gets a value indicating whether this instance is unlimited users license.
    /// </summary>
    internal static bool IsUnlimitedUsersLicense => LicenseState.Current.LicenseInfo.Users == 0;

    /// <summary>
    /// Gets or sets the update strategy.
    /// The update strategy is used when the recurring billing monthly license expires.
    /// </summary>
    /// <value>The update strategy.</value>
    internal static ILicenseUpdateStrategy UpdateStrategy
    {
      get => LicenseState.updateStrategy;
      set => LicenseState.updateStrategy = value;
    }

    /// <summary>Invalidates the license.</summary>
    /// <param name="restartApplication">Whether to restart the application.</param>
    public static void InvalidateLicense(bool restartApplication = true)
    {
      SystemMessageDispatcher.QueueSystemMessage(new SystemMessageBase()
      {
        Key = "InvalidateLicenseKey"
      });
      LicenseState.currentLicense.Reset();
      if (!restartApplication)
        return;
      SystemManager.RestartApplication(OperationReason.FromKey("LicenseUpdate"));
    }

    /// <summary>Loads the license from file.</summary>
    private static LicenseState BuildCurrentLicense() => LicenseState.ParseLicense(LicenseState.LoadLicenseData());

    /// <summary>Parses the license data.</summary>
    /// <param name="encryptedLicData">The encrypted license data.</param>
    /// <param name="validateLicense">Indicates whether to validate the license.</param>
    /// <returns></returns>
    internal static LicenseState ParseLicense(
      string encryptedLicData,
      bool validateLicense)
    {
      if (TestContext.Testing && TestContext.Attributes["LicenseState.ParseLicense"] == "true")
        return TestContext.GetMock<LicenseState>("LicenseState.ParseLicense");
      string licenseXml;
      if (string.IsNullOrEmpty(encryptedLicData))
      {
        licenseXml = encryptedLicData;
      }
      else
      {
        try
        {
          SignedLicenseEnvelope signedLicenseEnvelope = new SignedLicenseEnvelope();
          byte[] data = Convert.FromBase64String(encryptedLicData);
          licenseXml = Encoding.UTF8.GetString(signedLicenseEnvelope.DecryptDataWithDes(data, signedLicenseEnvelope.GetDesKey()));
        }
        catch
        {
          return new LicenseState()
          {
            LicenseInfo = new LicenseInfo()
            {
              IsCorrupted = true
            }
          };
        }
      }
      return LicenseState.GetLicenseStateFromString(licenseXml, validateLicense);
    }

    internal static SignedLicenseEnvelope GetLicenseEnvelope(
      string encryptedLicData)
    {
      return TestContext.Testing && TestContext.Attributes["LicenseState.GetLicenseEnvelope"] == "true" ? TestContext.GetMock<SignedLicenseEnvelope>("LicenseState.GetLicenseEnvelope") : new SignedLicenseEnvelope(encryptedLicData);
    }

    /// <summary>Parses the license.</summary>
    /// <param name="encryptedLicData">The encrypted license data.</param>
    /// <returns></returns>
    public static LicenseState ParseLicense(string encryptedLicData) => LicenseState.ParseLicense(encryptedLicData, true);

    public static ILicenseProvider GetLicenseProvider()
    {
      LicenseProviderConfigElement providerConfigElement = (LicenseProviderConfigElement) null;
      LicenseElement licensing = Config.Get<SystemConfig>().Licensing;
      ConfigElementDictionary<string, LicenseProviderConfigElement> licenseProviders = licensing.LicenseProviders;
      string defaultLicenseProvider = licensing.DefaultLicenseProvider;
      if (licenseProviders.Count > 1 && defaultLicenseProvider != null && licenseProviders[defaultLicenseProvider] != null)
        providerConfigElement = licenseProviders[defaultLicenseProvider];
      else if (licenseProviders.Count == 1)
        providerConfigElement = licenseProviders.Values.FirstOrDefault<LicenseProviderConfigElement>();
      NameValueCollection parameters = (NameValueCollection) null;
      Type type;
      if (providerConfigElement != null)
      {
        type = providerConfigElement.ProviderType;
        parameters = providerConfigElement.Parameters;
      }
      else
        type = typeof (FileLicenseProvider);
      ILicenseProvider instance = (ILicenseProvider) Activator.CreateInstance(type);
      instance.Initialize(parameters);
      return instance;
    }

    /// <summary>Loads the license data from file.</summary>
    /// <returns></returns>
    internal static string LoadLicenseData() => LicenseState.GetLicenseProvider().LoadLicenseData();

    /// <summary>Gets the license state from license xml string.</summary>
    /// <param name="licenseXml">The license XML.</param>
    /// <param name="validateLicense">Indicates whether we should validate the license.</param>
    /// <returns></returns>
    internal static LicenseState GetLicenseStateFromString(
      string licenseXml,
      bool validateLicense)
    {
      if (TestContext.Testing && TestContext.Attributes["LicenseState.ParseLicense"] == "true")
        return TestContext.GetMock<LicenseState>("LicenseState.ParseLicense");
      LicenseState licenseStateFromString = new LicenseState();
      licenseStateFromString.LicenseInfo = new LicenseInfo();
      LicenseInfo licenseInfo = licenseStateFromString.LicenseInfo;
      if (string.IsNullOrEmpty(licenseXml))
      {
        licenseStateFromString.MissingLicense = true;
      }
      else
      {
        SignedLicenseEnvelope signedLicenseEnvelope = new SignedLicenseEnvelope();
        signedLicenseEnvelope.LoadEnvelopeXml(licenseXml);
        if (signedLicenseEnvelope.IsCorrupted)
        {
          licenseInfo.IsCorrupted = true;
          return licenseStateFromString;
        }
        licenseInfo.LoadXml(signedLicenseEnvelope.SignedInfo);
        if (validateLicense && !licenseInfo.IsCorrupted && licenseInfo.IsValid)
        {
          licenseStateFromString.InvalidSignature = !signedLicenseEnvelope.CheckSignature(LicenseState.GetLicensePublicKey());
          licenseStateFromString.InvalidProductVersion = !LicenseState.CheckProductVersion(licenseInfo.ProductVersion);
        }
      }
      return licenseStateFromString;
    }

    /// <summary>
    /// Gets the current time in UTC.
    /// This method is used to help with testing the time sensitive data such as recurring billing license expiration.
    /// </summary>
    /// <returns></returns>
    internal DateTime GetCurrentTime() => this.currentDateTimeFunc != null ? this.currentDateTimeFunc() : DateTime.UtcNow;

    /// <summary>
    /// Sets a function for getting the current time.
    /// This method is used for testing purposes to mock expiration of the license.
    /// </summary>
    /// <param name="func">The function. Use null to return to the default behavior.</param>
    internal void SetCurrentTimeFunc(Func<DateTime> func) => this.currentDateTimeFunc = func;

    /// <summary>
    /// Checks if current request domain is a licensed domain.
    /// </summary>
    /// <param name="licensedDomains">The licensed domains.</param>
    /// <returns></returns>
    internal static bool CheckCurrentDomain(IEnumerable<string> licensedDomains) => LicenseState.CheckCurrentDomain(licensedDomains, false);

    /// <summary>Checks if current request domain is a valid one.</summary>
    /// <param name="validDomains">The valid domains.</param>
    /// <param name="allowSubDomains">Whether sub domains should be checked or not.</param>
    /// <returns></returns>
    internal static bool CheckCurrentDomain(IEnumerable<string> validDomains, bool allowSubDomains) => LicenseState.CheckCurrentDomain(validDomains, allowSubDomains, LicenseState.GetCurrentRequestDomain, LicenseState.GetCurrentHostType);

    /// <summary>Checks if current request domain is a valid domain.</summary>
    /// <param name="validDomains">The valid domains.</param>
    /// <param name="allowSubDomains">Whether sub domains should be checked or not.</param>
    /// <param name="domain">The domain that will be compared to the list of valid domains.</param>
    /// <param name="hostNameType">The type of the host name.</param>
    /// <returns></returns>
    internal static bool CheckCurrentDomain(
      IEnumerable<string> validDomains,
      bool allowSubDomains,
      string domain,
      UriHostNameType hostNameType)
    {
      bool skipDomainValidation = false;
      LicenseInfo licenseInfo = LicenseState.Current.LicenseInfo;
      if (licenseInfo != null && licenseInfo.SkipDomainValidation)
        skipDomainValidation = true;
      return LicenseState.CheckDomain(domain, validDomains, allowSubDomains, hostNameType, skipDomainValidation);
    }

    /// <summary>Checks if current request domain is a valid domain.</summary>
    /// <param name="domain">The domain that will be compared to the list of licensed domains.</param>
    /// <param name="hostNameType">The type of the host name.</param>
    /// <returns>True if the license allows this domain.</returns>
    internal static bool CheckDomainIsLicensed(string domain, UriHostNameType hostNameType = UriHostNameType.Dns)
    {
      bool skipDomainValidation = false;
      LicenseInfo licenseInfo = LicenseState.Current.LicenseInfo;
      if (licenseInfo != null && licenseInfo.SkipDomainValidation)
        skipDomainValidation = true;
      return LicenseState.CheckDomain(domain, licenseInfo.AllLicensedDomains, licenseInfo.AllowSubDomains, hostNameType, skipDomainValidation);
    }

    /// <summary>Checks if domain is a valid one.</summary>
    /// <param name="domain">The domain that will be compared to the list of valid domains.</param>
    /// <param name="validDomains">The valid domains enumeration.</param>
    /// <param name="allowSubDomains">Whether sub domains should be checked or not.</param>
    /// <param name="hostNameType">The type of the domain name.</param>
    /// <param name="skipDomainValidation">If true the domain comparison check will be skipped. Otherwise domain validation will be performed.</param>
    /// <returns></returns>
    internal static bool CheckDomain(
      string domain,
      IEnumerable<string> validDomains,
      bool allowSubDomains,
      UriHostNameType hostNameType,
      bool skipDomainValidation = false)
    {
      string lower1 = domain.ToLower();
      if (lower1 == "localhost" && (SystemManager.CurrentHttpContext == null || SystemManager.CurrentHttpContext.Request.IsLocal))
        return true;
      if (lower1 != null && hostNameType == UriHostNameType.Dns)
      {
        if (skipDomainValidation)
          return true;
        if (validDomains != null)
        {
          foreach (string validDomain in validDomains)
          {
            string lower2 = validDomain.TrimEnd('/').ToLower();
            if (LicenseState.EqualDomain(lower1, lower2, allowSubDomains))
              return true;
          }
        }
      }
      return false;
    }

    /// <summary>
    /// Performs case-insensitive comparison of the provided domain if it is equal or a subdomain to the valid one. "www" subdomain is always allowed and treated as equal to the given one.
    /// </summary>
    /// <param name="domain">The domain to check.</param>
    /// <param name="validDomain">The valid domain.</param>
    /// <param name="allowSubDomain">If set to true the domain could be a subdomain of the valid one. Otherwise it should be exactly the same or the "www" subdomain.</param>
    /// <returns>True if the domain is the same or "www" subdomain or a subdomain of the valid one and subdomain is allowed.</returns>
    internal static bool EqualDomain(string domain, string validDomain, bool allowSubDomain)
    {
      if (domain.Equals(validDomain, StringComparison.InvariantCultureIgnoreCase))
        return true;
      if (allowSubDomain)
      {
        if (domain.EndsWith("." + validDomain, StringComparison.InvariantCultureIgnoreCase))
          return true;
      }
      else if (domain.StartsWith("www.", StringComparison.InvariantCultureIgnoreCase) && domain.IndexOf(validDomain, StringComparison.InvariantCultureIgnoreCase) == "www.".Length)
        return true;
      return false;
    }

    /// <summary>
    /// Checks the product version equals  the current instance of the product.
    /// </summary>
    /// <param name="productVersion">The product version to check for</param>
    /// <returns></returns>
    internal static bool CheckProductVersion(Version licenseVersion)
    {
      Version getProductVersion = LicenseState.GetProductVersion;
      return getProductVersion.Major == licenseVersion.Major && getProductVersion.Minor == licenseVersion.Minor;
    }

    /// <summary>
    /// Checks if the module is licensed for the whole instance (not per domain).
    /// </summary>
    /// <param name="moduleId">The module id to check for.</param>
    /// <returns>true if there is a module entry in the license with the same id</returns>
    public static bool CheckIsModuleLicensed(Guid moduleId) => LicenseState.CheckIsModuleLicensed(moduleId.ToString());

    /// <summary>
    /// Checks if the module is licensed for the whole instance (not per domain).
    /// </summary>
    /// <param name="moduleId">The module id to check for.</param>
    /// <returns>true if there is a module entry in the license with the same id</returns>
    public static bool CheckIsModuleLicensed(string moduleId) => LicenseState.CheckIsModuleLicensed(moduleId, (string) null);

    /// <summary>
    /// Checks if the is module licensed for the given domain.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="domain">The domain.</param>
    /// <returns></returns>
    public static bool CheckIsModuleLicensed(Guid moduleId, string domain) => LicenseState.CheckIsModuleLicensed(moduleId.ToString(), domain);

    /// <summary>
    /// Checks if the is module licensed for the given domain.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="domain">The domain.</param>
    /// <returns></returns>
    public static bool CheckIsModuleLicensed(string moduleId, string domain)
    {
      if (string.IsNullOrEmpty(moduleId))
        throw new ArgumentNullException(nameof (moduleId));
      return string.IsNullOrEmpty(domain) ? LicenseState.Current.LicenseInfo.CheckIsModuleLicensed(moduleId.ToString()) : LicenseState.Current.LicenseInfo.CheckIsModuleLicensed(moduleId, domain);
    }

    /// <summary>
    /// Checks if the is module licensed for at least one domain in the application.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <returns></returns>
    public static bool CheckIsModuleLicensedInAnyDomain(Guid moduleId) => LicenseState.CheckIsModuleLicensedInAnyDomain(moduleId.ToString());

    /// <summary>
    /// Checks if the is module licensed for at least one domain in the application.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <returns></returns>
    public static bool CheckIsModuleLicensedInAnyDomain(string moduleId)
    {
      if (string.IsNullOrEmpty(moduleId))
        throw new ArgumentNullException(nameof (moduleId));
      return LicenseState.Current.LicenseInfo.CheckIsModuleLicensedInAnyDomain(moduleId);
    }

    /// <summary>
    /// Checks if the is module licensed for the current domain.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <returns></returns>
    public static bool CheckIsModuleLicensedInCurrentDomain(string moduleId)
    {
      if (string.IsNullOrEmpty(moduleId))
        throw new ArgumentNullException(nameof (moduleId));
      return LicenseState.Current.LicenseInfo.CheckIsModuleLicensed(moduleId, SystemManager.CurrentContext.CurrentSite.LiveUrl);
    }

    public static bool SitefinityNodeAllowedInCurrentDomain(ISitefinitySiteMapNode sitefinityNode)
    {
      string attribute1 = sitefinityNode.Attributes["hideFromMenuOnInvalidLicense"];
      if (string.IsNullOrEmpty(attribute1) || !attribute1.Equals("True", StringComparison.OrdinalIgnoreCase))
        return true;
      string attribute2 = sitefinityNode.Attributes["ModuleIdAttribute"];
      return !string.IsNullOrEmpty(attribute2) && LicenseState.CheckIsModuleLicensedInCurrentDomain(attribute2);
    }

    /// <summary>Gets the signature public key.</summary>
    /// <returns></returns>
    internal static string GetLicensePublicKey() => ControlUtilities.GetTextResource("Telerik.Sitefinity.Licensing.LicenseKey.xml", typeof (LicenseState));

    /// <summary>Saves current license.</summary>
    internal static void SaveLicense(string licenseData)
    {
      if (!LicenseState.Current.InvalidLicense)
        AppPermission.Demand(AppAction.ManageLicenses);
      LicenseState.GetLicenseProvider().SaveLicense(licenseData);
      LicenseState.InvalidateLicense();
    }

    /// <summary>
    /// Saves the updated license.
    /// This is used after updating the recurring billing license and does not restart the application.
    /// </summary>
    private static void SaveUpdatedLicense(string licenseData)
    {
      LicenseState.GetLicenseProvider().SaveLicense(licenseData);
      LicenseState.InvalidateLicense(false);
    }

    /// <summary>
    /// Updates the expired license.
    /// This is used to renew the recurring billing license after expiration.
    /// </summary>
    private void UpdateExpiredLicense()
    {
      if (LicenseState.updateStrategy == null)
        return;
      DateTime currentTime = this.GetCurrentTime();
      if (currentTime <= this.LicenseInfo.ExpirationDate)
        return;
      bool flag = currentTime - LicenseState.lastUpdateCheck > LicenseState.updateCheckInterval;
      if (flag)
      {
        lock (LicenseState.updateLicenseLock)
        {
          flag = currentTime - LicenseState.lastUpdateCheck > LicenseState.updateCheckInterval;
          if (flag)
            LicenseState.lastUpdateCheck = currentTime;
        }
      }
      if (!flag)
        return;
      this.UpdateLicense();
    }

    /// <summary>
    /// Updates the license asynchronously.
    /// This is used to renew the expired recurring billing license.
    /// </summary>
    private void UpdateLicense()
    {
      if (LicenseState.updateStrategy == null)
        return;
      Task task = Task.Factory.StartNew((Action) (() =>
      {
        LicenseUpdateResult licenseUpdateResult = LicenseState.updateStrategy.UpdateLicense(this);
        switch (licenseUpdateResult.Status)
        {
          case LicenseUpdateStatus.Success:
            LicenseState.SaveUpdatedLicense(licenseUpdateResult.LicenseData);
            break;
          case LicenseUpdateStatus.AlreadyUpdated:
            LicenseState.InvalidateLicense(false);
            break;
        }
      }));
      if (!TestContext.Testing || !(TestContext.Attributes["LicenseState.SynchronousUpdate"] == "true"))
        return;
      task.Wait();
    }
  }
}
