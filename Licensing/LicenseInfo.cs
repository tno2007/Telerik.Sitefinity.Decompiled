// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.LicenseInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Licensing
{
  /// <summary>contains information about a license</summary>
  public sealed class LicenseInfo
  {
    private Version productVersion;
    private int users;
    private DateTime issueDate;
    private List<string> domains = new List<string>();
    private List<LicensedModuleInfo> licensedModules = new List<LicensedModuleInfo>();
    private List<string> addons = new List<string>();
    private LicenseCustomerInfo customerIthis;
    private bool isValid;
    private string[] validationErrors;
    private bool isCorrupted;
    private string licenseId;
    private int totalContentLimit;
    private int totalPublicPagesLimit;
    private int totalNewsLettersSubscribersCount;
    private bool isGranularPermissionsEnabled = true;
    private bool isPagesGranularPermissionsEnabled = true;
    private string licenseType;
    private bool isTrial;
    private bool isHosted;
    private DateTime expirationDate;
    private int workflowFeaturesLevel = 100;
    private int localizationFeaturesLevel = 100;
    private bool isPageControlsPermissionsEnabled = true;
    private bool supportLoadBalancing;
    private bool allowSubDomains;
    private bool isRecurringBilling;
    private int totalForumsLimit;
    private List<LicensedDomainItem> licensedDomainItems = new List<LicensedDomainItem>();
    private bool isMultiTenancyLicense;
    private bool skipDomainValidation;
    internal const bool AllowSubDomainsDefault = false;
    internal const bool IsRecurringBillingDefault = false;
    public static readonly Guid LoadBalancingModuleId = new Guid("C348F3CD-B7CE-4D25-AE5F-B16CC312A4B4");
    private const string CatchallDomainsParameter = "*";
    private static readonly char[] illegalCharacters = new char[14]
    {
      '$',
      '%',
      '&',
      '\'',
      '*',
      '+',
      '=',
      '?',
      '^',
      '`',
      '{',
      '|',
      '}',
      '~'
    };
    private static readonly string[] illegalDomains = new string[22]
    {
      "com",
      "org",
      "net",
      "au",
      "biz",
      "de",
      "fr",
      "ca",
      "ru",
      "cn",
      "es",
      "uk",
      "edu",
      "gov",
      "info",
      "eu",
      "bg",
      "it",
      "int",
      "us",
      "tr",
      "ro"
    };

    internal LicenseInfo()
    {
    }

    /// <summary>
    /// Gets or sets the unique license pageId. This corresponds to the customer purchase oder detail pageId
    /// </summary>
    /// <value>The license pageId.</value>
    public string LicenseId
    {
      get => this.licenseId;
      internal set => this.licenseId = value;
    }

    /// <summary>
    /// Gets or sets the license type. This corresponds to the customer purchase oder detail license type - the sietfinity editon
    /// 
    /// </summary>
    /// <value>The license type.</value>
    public string LicenseType
    {
      get => this.licenseType;
      internal set => this.licenseType = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this is trial(evaluation) license
    /// </summary>
    /// <value><c>true</c> if this instance is trial; otherwise, <c>false</c>.</value>
    public bool IsTrial
    {
      get => this.isTrial;
      internal set => this.isTrial = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this is a hosted licensed product
    /// </summary>
    /// <value><c>true</c> if this instance is hosted; otherwise, <c>false</c>.</value>
    public bool IsHosted
    {
      get => this.isHosted;
      internal set => this.isHosted = value;
    }

    /// <summary>Gets the customer this the license was issued to.</summary>
    /// <value>The customer.</value>
    public LicenseCustomerInfo Customer
    {
      get
      {
        if (this.customerIthis == null)
          this.customerIthis = new LicenseCustomerInfo();
        return this.customerIthis;
      }
    }

    /// <summary>Gets or sets the product version that is licensed.</summary>
    /// <value>The product version.</value>
    public Version ProductVersion
    {
      get => this.productVersion;
      internal set => this.productVersion = value;
    }

    /// <summary>
    /// Gets or sets the allowed number of concurrently logged admin users for this license.
    /// </summary>
    /// <value>The users.</value>
    public int Users
    {
      get => this.users;
      internal set => this.users = value;
    }

    /// <summary>Gets or sets the license issue date.</summary>
    /// <value>The issue date.</value>
    public DateTime IssueDate
    {
      get => this.issueDate;
      internal set => this.issueDate = value;
    }

    /// <summary>
    /// Gets or sets the expiration date for evalution versions.
    /// For licensed version this is the date when the license support expires
    /// </summary>
    /// <value>The expiration date.</value>
    public DateTime ExpirationDate
    {
      get => this.expirationDate;
      internal set => this.expirationDate = value;
    }

    /// <summary>
    /// Gets  the list of licensed domains for the license level
    /// (there could be additional domains in the LicensedDomainItems collection if this is a multi tenancy license).
    /// </summary>
    /// <value>The domains.</value>
    public IEnumerable<string> Domains
    {
      get
      {
        for (int i = 0; i < this.domains.Count; ++i)
          yield return this.domains[i];
      }
      internal set => this.domains = new List<string>(value);
    }

    /// <summary>
    /// Gets all licensed domains (including the internally defined ones for the multi tenancy license).
    /// </summary>
    /// <value>The get all licensed domains.</value>
    public IEnumerable<string> AllLicensedDomains
    {
      get
      {
        int i;
        if (this.domains != null)
        {
          for (i = 0; i < this.domains.Count; ++i)
            yield return this.domains[i];
        }
        if (this.isMultiTenancyLicense && this.licensedDomainItems != null)
        {
          for (i = 0; i < this.licensedDomainItems.Count; ++i)
          {
            LicensedDomainItem licensedDomainItem = this.licensedDomainItems[i];
            for (int k = 0; k < licensedDomainItem.domains.Count; ++k)
              yield return licensedDomainItem.domains[k];
            licensedDomainItem = (LicensedDomainItem) null;
          }
        }
      }
    }

    /// <summary>Gets or sets the licensed modules collection.</summary>
    /// <value>The licensed modules.</value>
    public IEnumerable<LicensedModuleInfo> LicensedModules
    {
      get
      {
        if (this.licensedModules != null)
        {
          for (int i = 0; i < this.licensedModules.Count; ++i)
            yield return this.licensedModules[i].Clone();
        }
      }
      internal set => this.licensedModules = new List<LicensedModuleInfo>(value);
    }

    /// <summary>Gets or sets the addons collection.</summary>
    /// <value>The addons.</value>
    public IEnumerable<string> Addons
    {
      get
      {
        if (this.addons != null)
        {
          for (int i = 0; i < this.addons.Count; ++i)
            yield return this.addons[i];
        }
      }
      internal set => this.addons = new List<string>(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this license is valid. E.g. all the requried license data is present and valid
    /// </summary>
    /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
    public bool IsValid
    {
      get => this.isValid;
      internal set => this.isValid = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this license was corrupted. E.g. was not possible to be parsed from the original stream
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is corrupted; otherwise, <c>false</c>.
    /// </value>
    public bool IsCorrupted
    {
      get => this.isCorrupted;
      internal set => this.isCorrupted = value;
    }

    /// <summary>Gets or sets the total content limit.</summary>
    /// <value>The total content limit.</value>
    public int TotalContentLimit
    {
      get => this.totalContentLimit;
      internal set => this.totalContentLimit = value;
    }

    /// <summary>Gets or sets the total public pages.</summary>
    /// <value>The total public pages.</value>
    public int TotalPublicPagesLimit
    {
      get => this.totalPublicPagesLimit;
      internal set => this.totalPublicPagesLimit = value;
    }

    /// <summary>Gets or sets the news letters subscribers count.</summary>
    /// <value>The news letters subscribers count.</value>
    public int TotalNewsLettersSubscribersCount
    {
      get => this.totalNewsLettersSubscribersCount;
      internal set => this.totalNewsLettersSubscribersCount = value;
    }

    /// <summary>
    /// Gets or sets the workflow features level.
    /// 0 - worfklow disabled, 100 - full workflow functionality supported
    /// </summary>
    /// <value>The workflow features level.</value>
    public int WorkflowFeaturesLevel
    {
      get => this.workflowFeaturesLevel;
      internal set => this.workflowFeaturesLevel = value;
    }

    /// <summary>
    /// Gets or sets the localization features level.
    /// 0 - localization disabled , 100 - Full localization supported
    /// </summary>
    /// <value>The localization features level.</value>
    public int LocalizationFeaturesLevel
    {
      get => this.localizationFeaturesLevel;
      internal set => this.localizationFeaturesLevel = value;
    }

    /// <summary>
    /// Gets the validation errors found during validating the license.
    /// </summary>
    /// <value>The validation errors.</value>
    public string[] ValidationErrors => this.validationErrors;

    /// <summary>
    /// Gets a value indicating whether this instance is granular permissions enabled.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is granular permissions enabled; otherwise, <c>false</c>.
    /// </value>
    public bool IsGranularPermissionsEnabled
    {
      get => this.isGranularPermissionsEnabled;
      internal set => this.isGranularPermissionsEnabled = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether pages granular permissions is enabled. This property should be checked only if IsGranularPermissionsEnabled is false
    /// This allows to turn on just the pages gr.permissions in small business and community editions
    /// </summary>
    public bool IsPagesGranularPermissionsEnabled
    {
      get => this.isPagesGranularPermissionsEnabled;
      internal set => this.isPagesGranularPermissionsEnabled = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether page widgets and placeholders can be secured separately from the page
    /// </summary>
    public bool IsPageControlsPermissionsEnabled
    {
      get => this.isPageControlsPermissionsEnabled;
      internal set => this.isPageControlsPermissionsEnabled = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether load balancing is enabled.
    /// </summary>
    /// <value>
    /// 	<c>true</c> load balancing is enabled; otherwise, <c>false</c>.
    /// </value>
    public bool SupportLoadBalancing
    {
      get => !this.supportLoadBalancing && this.CheckIsModuleLicensed(LicenseInfo.LoadBalancingModuleId.ToString()) || this.supportLoadBalancing;
      internal set => this.supportLoadBalancing = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether we allow sub domains of the registered license domains
    /// </summary>
    /// <value><c>true</c> if sub domains are enabled otherwise, <c>false</c>.</value>
    public bool AllowSubDomains
    {
      get => this.allowSubDomains;
      internal set => this.allowSubDomains = value;
    }

    /// <summary>
    /// Gets or sets the total number of forums limit. This value is 0 by default and means unlimited number of forums is allowed
    /// </summary>
    public int TotalForumsLimit
    {
      get => this.totalForumsLimit;
      internal set => this.totalForumsLimit = value;
    }

    /// <summary>Gets or sets the licensed domains collection.</summary>
    /// <value>The licensed sites.</value>
    public IEnumerable<LicensedDomainItem> LicensedDomainItems
    {
      get
      {
        if (this.licensedDomainItems != null)
        {
          for (int i = 0; i < this.licensedDomainItems.Count; ++i)
            yield return this.licensedDomainItems[i];
        }
      }
      internal set => this.licensedDomainItems = new List<LicensedDomainItem>(value);
    }

    /// <summary>
    /// Gets or sets whether Sitefinity is supporting multisite management
    /// </summary>
    public bool IsMultiTenancyLicense
    {
      get => this.isMultiTenancyLicense;
      internal set => this.isMultiTenancyLicense = value;
    }

    /// <summary>
    /// Gets or sets whether the license is a recurring billing license.
    /// </summary>
    /// <value>Whether the license is a recurring billing license.</value>
    public bool IsRecurringBilling
    {
      get => this.isRecurringBilling;
      internal set => this.isRecurringBilling = value;
    }

    public bool SkipDomainValidation
    {
      get
      {
        if (this.domains != null && this.domains.Count > 0 && this.domains[0] == "*")
          this.skipDomainValidation = true;
        return this.skipDomainValidation;
      }
    }

    /// <summary>
    /// Validates that all the required license data is available and valid. This methods sets the validation errors and the IsValid flag
    /// </summary>
    /// <returns>true if no validation errors were found</returns>
    internal bool Validate()
    {
      this.validationErrors = this.GetValidationErrors();
      this.isValid = ((IEnumerable<string>) this.validationErrors).Count<string>() == 0;
      return this.isValid;
    }

    /// <summary>
    /// Gets the LicenseInfo validation errors like missing product version,domains or missing module pageId-s.
    /// </summary>
    /// <returns></returns>
    internal string[] GetValidationErrors()
    {
      if (TestContext.Testing && TestContext.Attributes["LicenseInfo.GetValidationErrors"] == "true")
        return TestContext.GetMock<string[]>("LicenseInfo.GetValidationErrorsResult");
      List<string> errors = new List<string>();
      if (string.IsNullOrEmpty(this.LicenseId))
        errors.Add("No license ID");
      if (this.ProductVersion == (Version) null || string.IsNullOrEmpty(this.ProductVersion.ToString()))
        errors.Add("No product version.");
      if (this.IsRecurringBilling && this.IsTrial)
        errors.Add("Cannot be both recurring billing and trial.");
      if (this.IsRecurringBilling && this.ExpirationDate == DateTime.MinValue)
        errors.Add("Cannot be recurring billing without expiration date.");
      LicenseInfo.ValidateDomains(this.AllLicensedDomains, errors);
      LicenseInfo.ValidateModules(this.LicensedModules, errors);
      if (this.IsMultiTenancyLicense)
      {
        foreach (LicensedDomainItem licensedDomainItem in this.LicensedDomainItems)
        {
          string[] validationErrors = licensedDomainItem.GetValidationErrors();
          if (((IEnumerable<string>) validationErrors).Count<string>() > 0)
            errors.AddRange((IEnumerable<string>) validationErrors);
        }
      }
      return errors.ToArray();
    }

    internal static void ValidateModules(
      IEnumerable<LicensedModuleInfo> modules,
      List<string> errors)
    {
      foreach (LicensedModuleInfo module in modules)
      {
        if (module.Id == Guid.Empty)
          errors.Add("Licensed module without Id.");
      }
    }

    internal static void ValidateDomains(IEnumerable<string> domains, List<string> errors)
    {
      if (domains.Count<string>() == 0)
        errors.Add("No licensed domains specified.");
      foreach (string domain in domains)
      {
        if (!LicenseInfo.TestIsValidDomain(domain))
          errors.Add("Invalid domain name." + domain);
      }
    }

    /// <summary>Tests if the domain name is valid</summary>
    /// <param name="domain">The domain.</param>
    /// <returns></returns>
    internal static bool TestIsValidDomain(string domain)
    {
      if (domain.Equals("*"))
        return true;
      foreach (char ch in domain)
      {
        if (((IEnumerable<char>) LicenseInfo.illegalCharacters).Contains<char>(ch))
          return false;
      }
      return !domain.Contains("..") && !domain.TrimEnd().EndsWith(".") && !domain.StartsWith("\\") && !((IEnumerable<string>) LicenseInfo.illegalDomains).Contains<string>(domain);
    }

    /// <summary>
    /// Checks if the module is licensed for the whole instance (not per domain).
    /// </summary>
    /// <param name="moduleId">The module id to check for.</param>
    /// <returns>true if there is a module entry in the license with the same id</returns>
    public bool CheckIsModuleLicensed(string moduleId) => moduleId.Equals(Guid.Empty.ToString(), StringComparison.OrdinalIgnoreCase) || this.licensedModules != null && this.licensedModules.Any<LicensedModuleInfo>((Func<LicensedModuleInfo, bool>) (module => module.Sid.Equals(moduleId, StringComparison.OrdinalIgnoreCase)));

    /// <summary>
    /// Checks if the is module licensed for the given domain.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="domain">The domain.</param>
    /// <returns></returns>
    public bool CheckIsModuleLicensed(string moduleId, string domain)
    {
      if (this.CheckIsModuleLicensed(moduleId))
        return true;
      if (this.isMultiTenancyLicense)
      {
        if (this.licensedDomainItems == null)
          return false;
        string licensedDomain = string.Empty;
        this.TryGetLicensedDomain(new UriBuilder(domain).Host, out licensedDomain);
        if (!string.IsNullOrEmpty(licensedDomain))
          return this.LicensedDomainItems.Any<LicensedDomainItem>((Func<LicensedDomainItem, bool>) (di => di.Domains.Contains<string>(licensedDomain) && di.GetLicensedModules().Any<LicensedModuleInfo>(closure_0 ?? (closure_0 = (Func<LicensedModuleInfo, bool>) (m => m.Sid.Equals(moduleId, StringComparison.OrdinalIgnoreCase))))));
      }
      return false;
    }

    /// <summary>
    /// Checks if the is module licensed for at least one domain.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <returns></returns>
    public bool CheckIsModuleLicensedInAnyDomain(string moduleId)
    {
      if (this.CheckIsModuleLicensed(moduleId))
        return true;
      return this.isMultiTenancyLicense && this.LicensedDomainItems.Any<LicensedDomainItem>((Func<LicensedDomainItem, bool>) (di => di.GetLicensedModules().Any<LicensedModuleInfo>((Func<LicensedModuleInfo, bool>) (m => m.Sid.Equals(moduleId, StringComparison.OrdinalIgnoreCase)))));
    }

    /// <summary>
    /// Determines whether the module with the given id is licensed for the specified url.
    /// </summary>
    /// <param name="moduleId">The module id.</param>
    /// <param name="host">The host.</param>
    /// <returns></returns>
    public bool CheckIsModuleLicensedPerUrl(string moduleId, string url)
    {
      bool flag = false;
      string licensedDomain;
      if (this.TryGetLicensedDomain(new Uri(url).Host, out licensedDomain))
        flag = this.CheckIsModuleLicensed(moduleId, licensedDomain);
      return flag;
    }

    internal bool TryGetLicensedDomain(string host, out string licensedDomain)
    {
      licensedDomain = string.Empty;
      string lower = host.ToLower();
      if (lower != null)
      {
        foreach (string allLicensedDomain in this.AllLicensedDomains)
        {
          string str = allLicensedDomain.ToLower().Trim();
          if (lower == str || this.AllowSubDomains && lower.EndsWith("." + str))
          {
            licensedDomain = allLicensedDomain;
            return true;
          }
        }
      }
      if (!this.SkipDomainValidation)
        return false;
      licensedDomain = host;
      return true;
    }

    /// <summary>Gets the license from string.</summary>
    /// <param name="licenseXml">The license XML.</param>
    /// <returns></returns>
    public static LicenseInfo GetLicenseFromString(string licenseXml)
    {
      LicenseInfo licenseFromString = new LicenseInfo();
      licenseFromString.LoadXml(licenseXml);
      return licenseFromString;
    }

    /// <summary>Load the license from xml string.</summary>
    /// <param name="xml">The XML.</param>
    public void LoadXml(string xml)
    {
      XElement element;
      try
      {
        element = XElement.Load(XmlReader.Create((TextReader) new StringReader(xml)));
      }
      catch
      {
        this.IsCorrupted = true;
        return;
      }
      this.LoadXml(element);
    }

    /// <summary>Load the license from XElement</summary>
    /// <param name="root">The element.</param>
    internal void LoadXml(XElement element)
    {
      try
      {
        this.Users = LicenseInfo.LoadUsers(element);
        XElement xelement1 = element.Descendants((XName) "IssueDate").FirstOrDefault<XElement>();
        if (xelement1 != null)
          this.IssueDate = DateTime.Parse(xelement1.Value);
        XElement xelement2 = element.Descendants((XName) "ExpirationDate").FirstOrDefault<XElement>();
        if (xelement2 != null)
          this.ExpirationDate = DateTime.Parse(xelement2.Value);
        XElement xelement3 = element.Descendants((XName) "ProductVersion").FirstOrDefault<XElement>();
        if (xelement3 != null && !string.IsNullOrEmpty(xelement3.Value))
        {
          Version version = new Version(xelement3.Value);
          this.ProductVersion = new Version(version.Major, version.Minor);
        }
        XElement xelement4 = element.Descendants((XName) "LicenseId").FirstOrDefault<XElement>();
        if (xelement4 != null && !string.IsNullOrEmpty(xelement4.Value))
          this.LicenseId = xelement4.Value;
        XElement xelement5 = element.Descendants((XName) "IsTrial").FirstOrDefault<XElement>();
        if (xelement5 != null && !string.IsNullOrEmpty(xelement5.Value))
          this.IsTrial = bool.Parse(xelement5.Value.ToLower());
        XElement xelement6 = element.Descendants((XName) "IsHosted").FirstOrDefault<XElement>();
        if (xelement6 != null && !string.IsNullOrEmpty(xelement6.Value))
          this.IsHosted = bool.Parse(xelement6.Value.ToLower());
        XElement xelement7 = element.Descendants((XName) "LicenseType").FirstOrDefault<XElement>();
        if (xelement7 != null && !string.IsNullOrEmpty(xelement7.Value))
          this.LicenseType = xelement7.Value;
        XElement xelement8 = element.Descendants((XName) "WorkflowFeaturesLevel").FirstOrDefault<XElement>();
        if (xelement8 != null)
          this.WorkflowFeaturesLevel = int.Parse(xelement8.Value);
        XElement xelement9 = element.Descendants((XName) "LocalizationFeaturesLevel").FirstOrDefault<XElement>();
        if (xelement9 != null)
          this.LocalizationFeaturesLevel = int.Parse(xelement9.Value);
        XElement xml = element.Descendants((XName) "Customer").FirstOrDefault<XElement>();
        if (xml != null)
          this.Customer.LoadXml(xml);
        this.TotalNewsLettersSubscribersCount = LicenseInfo.LoadTotalNewslettersSubscribersCount(element);
        XElement xelement10 = element.Descendants((XName) "TotalContentLimit").FirstOrDefault<XElement>();
        if (xelement10 != null)
          this.TotalContentLimit = int.Parse(xelement10.Value);
        int num = LicenseInfo.LoadTotalPublicPagesLimit(element);
        if (num == -1)
          num = 0;
        this.TotalPublicPagesLimit = num;
        XElement xelement11 = element.Descendants((XName) "IsGranularPermissionsEnabled").FirstOrDefault<XElement>();
        if (xelement11 != null && !string.IsNullOrEmpty(xelement11.Value) && !bool.TryParse(xelement11.Value, out this.isGranularPermissionsEnabled))
          this.isGranularPermissionsEnabled = false;
        XElement xelement12 = element.Descendants((XName) "IsPagesGranularPermissionsEnabled").FirstOrDefault<XElement>();
        if (xelement12 != null && !string.IsNullOrEmpty(xelement12.Value) && !bool.TryParse(xelement12.Value, out this.isPagesGranularPermissionsEnabled))
          this.isPagesGranularPermissionsEnabled = false;
        XElement xelement13 = element.Descendants((XName) "IsPageControlsPermissionsEnabled").FirstOrDefault<XElement>();
        if (xelement13 != null && !string.IsNullOrEmpty(xelement13.Value) && !bool.TryParse(xelement13.Value, out this.isPageControlsPermissionsEnabled))
          this.isPageControlsPermissionsEnabled = false;
        XElement xelement14 = element.Descendants((XName) "SupportLoadBalancing").FirstOrDefault<XElement>();
        if (xelement14 != null && !string.IsNullOrEmpty(xelement14.Value) && !bool.TryParse(xelement14.Value, out this.supportLoadBalancing))
          this.supportLoadBalancing = false;
        this.allowSubDomains = LicenseInfo.LoadAllowSubdomains(element);
        this.IsRecurringBilling = LicenseInfo.LoadIsRecurringBilling(element);
        XElement xelement15 = element.Descendants((XName) "TotalForumsLimit").FirstOrDefault<XElement>();
        if (xelement15 != null)
          this.TotalForumsLimit = int.Parse(xelement15.Value);
        this.licensedModules = LicenseInfo.LoadLicensedModules(element);
        this.addons = LicenseInfo.LoadAddons(element);
        this.domains = LicenseInfo.LoadDomainValues(element);
        this.isMultiTenancyLicense = this.CheckIsModuleLicensed("FBD4773B-8688-4C75-8563-28BFDA27A185");
        if (this.isMultiTenancyLicense)
          this.LicensedDomainItems = LicenseInfo.LoadLicensedDomains(element, (IEnumerable<LicensedModuleInfo>) this.licensedModules, this.TotalPublicPagesLimit);
      }
      catch
      {
        this.isCorrupted = true;
      }
      this.Validate();
    }

    internal static bool LoadAllowSubdomains(XElement element)
    {
      bool result = false;
      XElement xelement = element.Descendants((XName) "AllowSubDomains").FirstOrDefault<XElement>();
      if (xelement != null && !string.IsNullOrEmpty(xelement.Value) && !bool.TryParse(xelement.Value, out result))
        result = false;
      return result;
    }

    internal static bool LoadIsRecurringBilling(XElement element)
    {
      bool result = false;
      XElement xelement = element.Descendants((XName) "IsRecurringBilling").FirstOrDefault<XElement>();
      if (xelement != null && !string.IsNullOrEmpty(xelement.Value) && !bool.TryParse(xelement.Value, out result))
        result = false;
      return result;
    }

    internal static int LoadTotalNewslettersSubscribersCount(XElement element)
    {
      int num = 0;
      XElement xelement = element.Descendants((XName) "totalNewsLettersSubscribersCount").FirstOrDefault<XElement>();
      if (xelement != null && !string.IsNullOrEmpty(xelement.Value))
        num = int.Parse(xelement.Value);
      return num;
    }

    internal static int LoadUsers(XElement element)
    {
      int num = 0;
      XElement xelement = element.Descendants((XName) "Users").FirstOrDefault<XElement>();
      if (xelement != null)
        num = int.Parse(xelement.Value);
      return num;
    }

    internal static List<string> LoadDomainValues(XElement element)
    {
      List<string> stringList = new List<string>();
      XElement xelement = element.Element((XName) "Domains");
      if (xelement != null)
      {
        foreach (XElement descendant in xelement.Descendants())
          stringList.Add(descendant.Value.ToLower().Trim());
      }
      return stringList;
    }

    internal static List<LicensedModuleInfo> LoadLicensedModules(
      XElement element)
    {
      List<LicensedModuleInfo> licensedModuleInfoList = new List<LicensedModuleInfo>();
      XElement xelement = element.Element((XName) "LicensedModules");
      if (xelement != null)
      {
        foreach (XElement descendant in xelement.Descendants((XName) "Module"))
        {
          LicensedModuleInfo licensedModuleInfo = new LicensedModuleInfo();
          licensedModuleInfo.LoadXml(descendant);
          licensedModuleInfoList.Add(licensedModuleInfo);
        }
      }
      return licensedModuleInfoList;
    }

    internal static List<string> LoadAddons(XElement element)
    {
      List<string> stringList = new List<string>();
      XElement xelement = element.Element((XName) "Addons");
      if (xelement != null)
      {
        foreach (XElement descendant in xelement.Descendants((XName) "Addon"))
        {
          string str = descendant.Value;
          stringList.Add(str);
        }
      }
      return stringList;
    }

    internal static int LoadTotalPublicPagesLimit(XElement element)
    {
      int num = -1;
      XElement xelement = element.Descendants((XName) "TotalPublicPages").FirstOrDefault<XElement>();
      if (xelement != null)
        num = int.Parse(xelement.Value);
      return num;
    }

    private static IEnumerable<LicensedDomainItem> LoadLicensedDomains(
      XElement element,
      IEnumerable<LicensedModuleInfo> rootModules,
      int totalPublicPagesSystem)
    {
      List<LicensedDomainItem> licensedDomainItemList = new List<LicensedDomainItem>();
      XElement xelement = element.Descendants((XName) "LicensedDomainItems").FirstOrDefault<XElement>();
      if (xelement != null)
      {
        LicensedModuleInfoComparer comparer = new LicensedModuleInfoComparer();
        foreach (XElement descendant in xelement.Descendants((XName) "LicensedDomainItem"))
        {
          LicensedDomainItem licensedDomainItem = new LicensedDomainItem();
          licensedDomainItem.LoadXml(descendant);
          licensedDomainItem.LicensedModules = licensedDomainItem.LicensedModules.Union<LicensedModuleInfo>(rootModules).Distinct<LicensedModuleInfo>((IEqualityComparer<LicensedModuleInfo>) comparer);
          if (licensedDomainItem.TotalPublicPagesLimit == -1)
            licensedDomainItem.TotalPublicPagesLimit = totalPublicPagesSystem;
          licensedDomainItemList.Add(licensedDomainItem);
        }
      }
      return (IEnumerable<LicensedDomainItem>) licensedDomainItemList;
    }

    /// <summary>Generates a XDocument from the current license info</summary>
    internal XDocument ToXmlDocument()
    {
      XElement root = new XElement((XName) "SitefinityLicense");
      root.Add((object) new XElement((XName) "LicenseId", (object) this.LicenseId));
      root.Add((object) new XElement((XName) "ProductVersion", (object) this.ProductVersion));
      root.Add((object) new XElement((XName) "IssueDate", (object) this.GetLicenseFormatDate(this.IssueDate)));
      root.Add((object) new XElement((XName) "ExpirationDate", (object) this.GetLicenseFormatDate(this.ExpirationDate)));
      root.Add((object) new XElement((XName) "Users", (object) this.Users));
      root.Add((object) new XElement((XName) "IsTrial", (object) this.IsTrial));
      root.Add((object) new XElement((XName) "IsHosted", (object) this.IsHosted));
      root.Add((object) new XElement((XName) "LicenseType", (object) this.LicenseType));
      root.Add((object) new XElement((XName) "TotalContentLimit", (object) this.TotalContentLimit));
      root.Add((object) new XElement((XName) "totalNewsLettersSubscribersCount", (object) this.TotalNewsLettersSubscribersCount));
      LicenseInfo.StorePublicPagesLimit(root, this.TotalPublicPagesLimit);
      root.Add((object) new XElement((XName) "TotalForumsLimit", (object) this.TotalForumsLimit));
      root.Add((object) new XElement((XName) "IsGranularPermissionsEnabled", (object) this.IsGranularPermissionsEnabled));
      root.Add((object) new XElement((XName) "IsPagesGranularPermissionsEnabled", (object) this.IsPagesGranularPermissionsEnabled));
      root.Add((object) new XElement((XName) "IsPageControlsPermissionsEnabled", (object) this.IsPageControlsPermissionsEnabled));
      root.Add((object) new XElement((XName) "WorkflowFeaturesLevel", (object) this.WorkflowFeaturesLevel));
      root.Add((object) new XElement((XName) "LocalizationFeaturesLevel", (object) this.LocalizationFeaturesLevel));
      root.Add((object) new XElement((XName) "SupportLoadBalancing", (object) this.SupportLoadBalancing));
      root.Add((object) new XElement((XName) "AllowSubDomains", (object) this.AllowSubDomains));
      root.Add((object) new XElement((XName) "IsRecurringBilling", (object) this.IsRecurringBilling));
      if (this.Customer != null && (!string.IsNullOrEmpty(this.Customer.Email) || !string.IsNullOrEmpty(this.Customer.Id) || !string.IsNullOrEmpty(this.Customer.Name)))
        root.Add((object) this.Customer.ToXmlElement());
      LicenseInfo.StoreLicensedModules(root, this.LicensedModules);
      LicenseInfo.StoreAddons(root, this.Addons);
      if (this.IsMultiTenancyLicense)
        LicenseInfo.StoreLicensedDomainItems(root, this.LicensedDomainItems);
      LicenseInfo.StoreDomains(root, this.Domains);
      return new XDocument(new object[1]{ (object) root });
    }

    internal static void StoreLicensedModules(
      XElement root,
      IEnumerable<LicensedModuleInfo> modules)
    {
      if (modules == null || modules.Count<LicensedModuleInfo>() <= 0)
        return;
      XElement lModules = new XElement((XName) "LicensedModules");
      modules.Where<LicensedModuleInfo>((Func<LicensedModuleInfo, bool>) (lm => lm.Id != Guid.Empty || !string.IsNullOrEmpty(lm.Name))).ToList<LicensedModuleInfo>().ForEach((Action<LicensedModuleInfo>) (u => lModules.Add((object) u.ToXmlElement())));
      root.Add((object) lModules);
    }

    internal static void StoreAddons(XElement root, IEnumerable<string> addons)
    {
      if (addons == null || addons.Count<string>() <= 0)
        return;
      XElement addonsElement = new XElement((XName) "Addons");
      addons.Where<string>((Func<string, bool>) (a => !string.IsNullOrEmpty(a))).ToList<string>().ForEach((Action<string>) (a => addonsElement.Add((object) new XElement((XName) "Addon", (object) a))));
      root.Add((object) addonsElement);
    }

    internal static void StorePublicPagesLimit(XElement root, int publicPagesLimit) => root.Add((object) new XElement((XName) "TotalPublicPages", (object) publicPagesLimit));

    internal static void StoreDomains(XElement root, IEnumerable<string> domains)
    {
      if (domains == null || domains.Count<string>() <= 0)
        return;
      XElement content = new XElement((XName) "Domains");
      foreach (string domain in domains)
        content.Add((object) new XElement((XName) "Domain", (object) domain));
      root.Add((object) content);
    }

    private static void StoreLicensedDomainItems(
      XElement root,
      IEnumerable<LicensedDomainItem> domains)
    {
      if (domains == null || domains.Count<LicensedDomainItem>() <= 0)
        return;
      XElement lDomains = new XElement((XName) "LicensedDomainItems");
      domains.Where<LicensedDomainItem>((Func<LicensedDomainItem, bool>) (ls => ls.Domains != null && ls.Domains.Count<string>() > 0)).ToList<LicensedDomainItem>().ForEach((Action<LicensedDomainItem>) (s => lDomains.Add((object) s.ToXmlElement())));
      root.Add((object) lDomains);
    }

    /// <summary>Generates a XML string from the current license info.</summary>
    /// <returns></returns>
    public string ToXmlString() => this.ToXmlDocument().ToString(SaveOptions.DisableFormatting);

    public override string ToString() => this.ToXmlString();

    private string GetLicenseFormatDate(DateTime value) => value.ToString("yyyy-MM-dd HH:mm:ss");
  }
}
