// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.LicensingMessages
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Messages and labels related to licensing</summary>
  [ObjectInfo("LicensingMessages", ResourceClassId = "LicensingMessages")]
  public sealed class LicensingMessages : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.LicensingMessages" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public LicensingMessages()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.LicensingMessages" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public LicensingMessages(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Error Messages</summary>
    [ResourceEntry("LicensingMessagesTitle", Description = "The title of this class.", LastModified = "2010/03/15", Value = "Licensing Messages")]
    public string LicensingMessagesTitle => this[nameof (LicensingMessagesTitle)];

    /// <summary>Error Messages Title plural</summary>
    [ResourceEntry("LicensingMessagesTitlePlural", Description = "The title plural of this class.", LastModified = "2010/03/15", Value = "Licensing Messages Title Plural")]
    public string LicensingMessagesTitlePlural => this[nameof (LicensingMessagesTitlePlural)];

    /// <summary>Contains localizable resources for Sitefinity errors.</summary>
    [ResourceEntry("LicensingMessagesDescription", Description = "The description of this class.", LastModified = "2010/03/15", Value = "Contains localizable resources for Sitefinity messages related to the licensing.")]
    public string LicensingMessagesDescription => this[nameof (LicensingMessagesDescription)];

    /// <summary>Gets the in trial mode text.</summary>
    /// <value>The in trial mode text.</value>
    [ResourceEntry("InTrialMode", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Sitefinity is running in Evaluation mode, because there is no valid license provided. Enter License Key to start using it in Licensed mode.")]
    public string InTrialMode => this[nameof (InTrialMode)];

    /// <summary>Gets the missing license text.</summary>
    /// <value>The missing license text .</value>
    [ResourceEntry("MissingLicense", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Missing License")]
    public string MissingLicense => this[nameof (MissingLicense)];

    /// <summary>Gets the wrong product version text.</summary>
    /// <value>The wrong product version text.</value>
    [ResourceEntry("WrongProductVersion", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Wrong product version")]
    public string WrongProductVersion => this["MissingLicense"];

    /// <summary>Gets the invalid domain text.</summary>
    /// <value>The invalid domain text.</value>
    [ResourceEntry("InvalidDomain", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Invalid Domain")]
    public string InvalidDomain => this[nameof (InvalidDomain)];

    /// <summary>Gets the license is corrupted text.</summary>
    /// <value>The license is corrupted text.</value>
    [ResourceEntry("LicenseIsCorrupted", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "License is corrupted")]
    public string LicenseIsCorrupted => this[nameof (LicenseIsCorrupted)];

    /// <summary>Gets the invalid license signature text.</summary>
    /// <value>The invalid license signature text.</value>
    [ResourceEntry("InvalidLicenseSignature", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Invalid License Signature")]
    public string InvalidLicenseSignature => this[nameof (InvalidLicenseSignature)];

    /// <summary>message when trying to update unexisting lciense key</summary>
    /// <value>The license is corrupted text.</value>
    [ResourceEntry("UpdateNoSuchLicenseKey", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "the entered license key is not entered correctly or does not exist")]
    public string UpdateNoSuchLicenseKey => this[nameof (UpdateNoSuchLicenseKey)];

    /// <summary>message when trying to update unlicensed key</summary>
    /// <value>The license is corrupted text.</value>
    [ResourceEntry("UpdateNoLicenseForProductVersion", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "This license key is not licensed for your product version")]
    public string UpdateNoLicenseForProductVersion => this[nameof (UpdateNoLicenseForProductVersion)];

    /// <summary>Message for license updateing connection problems.</summary>
    [ResourceEntry("UpdateConnectionProblem", Description = "Message for license updateing connection problems.", LastModified = "2010/03/12", Value = "Connection with the licensing server could not be established. Try to update the license manually")]
    public string UpdateConnectionProblem => this[nameof (UpdateConnectionProblem)];

    /// <summary>
    /// Message shown in license info page when a licensing problem occurs during the lciense update.
    /// </summary>
    [ResourceEntry("UpdateLicensingProblem", Description = "Message shown in license info page when a licensing problem occurs during the lciense update", LastModified = "2010/03/12", Value = "The license will not be updated because ")]
    public string UpdateLicensingProblem => this[nameof (UpdateLicensingProblem)];

    /// <summary>Message shown in page editor.</summary>
    [ResourceEntry("WidgetNotLicensed", Description = "Message shown in page editor.", LastModified = "2010/03/12", Value = "This Widget is not licensed. Editing is disabled")]
    public string WidgetNotLicensed => this[nameof (WidgetNotLicensed)];

    /// <summary>Caption shown in DashboardSystemStatusWidget</summary>
    [ResourceEntry("SystemErrorTitle", Description = "Caption shown in DashboardSystemStatusWidget", LastModified = "2019/01/16", Value = "Domain license error")]
    public string SystemErrorTitle => this[nameof (SystemErrorTitle)];

    /// <summary>Description shown in DashboardSystemStatusWidget</summary>
    [ResourceEntry("SystemErrorDescription", Description = "Description shown in DashboardSystemStatusWidget", LastModified = "2019/01/16", Value = "There are errors with the licensed domain(s). This may result in getting a Trial message on the frontend of your site. The following domain(s) are not registered in your license: ")]
    public string SystemErrorDescription => this[nameof (SystemErrorDescription)];

    /// <summary>Text shown in DashboardSystemStatusWidget link</summary>
    [ResourceEntry("SystemErrorLinkText", Description = "Text shown in DashboardSystemStatusWidget link", LastModified = "2019/01/29", Value = "Find a solution")]
    public string SystemErrorLinkTitle => this["SystemErrorLinkText"];

    /// <summary>Url shown in DashboardSystemStatusWidget link</summary>
    [ResourceEntry("SystemErrorLinkUrl", Description = "Url shown in DashboardSystemStatusWidget link", LastModified = "2021/02/03", Value = "https://www.progress.com/documentation/sitefinity-cms/troubleshooting-licenses-and-domains?ref=sf-dashboard")]
    public string SystemErrorLinkUrl => this[nameof (SystemErrorLinkUrl)];

    /// <summary>Message logged into error log</summary>
    [ResourceEntry("SystemErrorLogMessage", Description = "Message logged into error log", LastModified = "2019/02/15", Value = "The domain name {0} configured to serve your website content was not found in your current license file. Refer to {1} for troubleshooting advice.")]
    public string SystemErrorLogMessage => this[nameof (SystemErrorLogMessage)];

    [ResourceEntry("SiteInfoTemplate", Description = "HTML Template for invalid site domain info", LastModified = "2019/03/27", Value = "<div class=\"sfSystemStatusDetailsSingleLine\"><div class=\"siteName\"><em>Site:</em> {0}</div><div class=\"siteDomains\"><em>Domain(s):</em> {1}</div></div>")]
    public string SiteInfoTemplate => this[nameof (SiteInfoTemplate)];

    /// <summary>Gets the modules text.</summary>
    /// <value>The modules.</value>
    [ResourceEntry("Modules", Description = "Label shown in license info page.", LastModified = "2020/10/06", Value = "Licensed modules")]
    public string Modules => this[nameof (Modules)];

    /// <summary>Gets the domains text.</summary>
    /// <value>The domains text.</value>
    [ResourceEntry("Domains", Description = "Label shown in license info page.", LastModified = "2010/03/12", Value = "Domains")]
    public string Domains => this[nameof (Domains)];

    /// <summary>Gets the subdomains text.</summary>
    /// <value>The subdomains text.</value>
    [ResourceEntry("Subdomains", Description = "Label shown in license info page.", LastModified = "2011/09/19", Value = "Subdomains")]
    public string Subdomains => this[nameof (Subdomains)];

    /// <summary>Gets the max users text.</summary>
    /// <value>The max users text.</value>
    [ResourceEntry("MaxUsers", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Concurrent users")]
    public string MaxUsers => this[nameof (MaxUsers)];

    /// <summary>Gets the email text.</summary>
    /// <value>The email text.</value>
    [ResourceEntry("Email", Description = "Label shown in license info page.", LastModified = "2010/03/12", Value = "Email")]
    public string Email => this[nameof (Email)];

    /// <summary>Gets the company name text.</summary>
    /// <value>The company name text.</value>
    [ResourceEntry("CompanyName", Description = "Label shown in license info page.", LastModified = "2020/09/29", Value = "Company name")]
    public string CompanyName => this[nameof (CompanyName)];

    /// <summary>Gets the password text.</summary>
    /// <value>The password text.</value>
    [ResourceEntry("Password", Description = "Label shown in license info page.", LastModified = "2018/09/20", Value = "Password")]
    public string Password => this[nameof (Password)];

    /// <summary>License type</summary>
    /// <value>License type</value>
    [ResourceEntry("LicenseTypeLabel", Description = "License type", LastModified = "2018/09/20", Value = "License type")]
    public string LicenseTypeLabel => this[nameof (LicenseTypeLabel)];

    /// <summary>Gets the name text.</summary>
    /// <value>The name text.</value>
    [ResourceEntry("CustomerName", Description = "Label shown in license info page.", LastModified = "2010/03/12", Value = "Customer name")]
    public string Name => this["CustomerName"];

    /// <summary>Gets the build version text.</summary>
    /// <value>The name text.</value>
    [ResourceEntry("BuildVersion", Description = "Label shown in license info page.", LastModified = "2014/01/23", Value = "Build version")]
    public string BuildVersion => this[nameof (BuildVersion)];

    /// <summary>Gets the customer ID text.</summary>
    /// <value>The customer ID text.</value>
    [ResourceEntry("CustomerID", Description = "Label shown in license info page.", LastModified = "2010/03/12", Value = "Customer Id")]
    public string CustomerId => this[nameof (CustomerId)];

    /// <summary>Gets the issued on text.</summary>
    /// <value>The issued on text.</value>
    [ResourceEntry("IssuedOn", Description = "Label shown in license info page.", LastModified = "2010/03/12", Value = "Date of activation")]
    public string IssuedOn => this[nameof (IssuedOn)];

    /// <summary>Gets the version text.</summary>
    /// <value>The version text.</value>
    [ResourceEntry("ProductVersion", Description = "Label shown in license info page.", LastModified = "2010/03/12", Value = "Product version")]
    public string ProductVersion => this[nameof (ProductVersion)];

    [ResourceEntry("ProductFileVersion", Description = "Label shown in license info page.", LastModified = "2016/04/29", Value = "Product file version")]
    public string ProductFileVersion => this[nameof (ProductFileVersion)];

    /// <summary>Gets the license ID text.</summary>
    /// <value>The license ID text.</value>
    [ResourceEntry("LicenseId", Description = "Label shown in license info page.", LastModified = "2010/03/12", Value = "License Key")]
    public string LicenseId => this[nameof (LicenseId)];

    /// <summary>Gets the content limit text.</summary>
    /// <value>The version text.</value>
    [ResourceEntry("ContentLimit", Description = "Label shown in license info page.", LastModified = "2010/10/22", Value = "Published content items")]
    public string ContentLimit => this[nameof (ContentLimit)];

    /// <summary>Gets the pages limit text.</summary>
    /// <value>The version text.</value>
    [ResourceEntry("PagesLimit", Description = "Label shown in license info page.", LastModified = "2010/10/22", Value = "Published Pages")]
    public string PagesLimit => this[nameof (PagesLimit)];

    /// <summary>Unlimited</summary>
    [ResourceEntry("Unlimited", Description = "Unlimited", LastModified = "2010/10/22", Value = "Unlimited")]
    public string Unlimited => this[nameof (Unlimited)];

    /// <summary>Gets the pages limit text.</summary>
    /// <value>The version text.</value>
    [ResourceEntry("IsTrailLicense", Description = "Label shown in license info page.", LastModified = "2010/10/22", Value = "Evaluation license")]
    public string IsTrailLicense => this[nameof (IsTrailLicense)];

    /// <summary>Email Campaigns subscribers count format</summary>
    /// <value>The version text.</value>
    [ResourceEntry("EmailCampaignsSubscribersCountFormat", Description = "Label displaying 'Email Campaigns' and subscribers count.", LastModified = "2011/08/05", Value = "{0} - {1} subscribers")]
    public string EmailCampaignsSubscribersCountFormat => this[nameof (EmailCampaignsSubscribersCountFormat)];

    /// <summary>Gets the domains text.</summary>
    /// <value>The domains text.</value>
    [ResourceEntry("Allowed", Description = "Label shown in license info page.", LastModified = "2011/09/19", Value = "Allowed")]
    public string Allowed => this[nameof (Allowed)];

    /// <summary>Gets the domains text.</summary>
    /// <value>The domains text.</value>
    [ResourceEntry("NotAllowed", Description = "Label shown in license info page.", LastModified = "2011/09/19", Value = "Registration Required")]
    public string NotAllowed => this[nameof (NotAllowed)];

    /// <summary>Label shown in License expiration widget</summary>
    /// <value>Expiration date:</value>
    [ResourceEntry("ExpirationDate", Description = "Label shown in License expiration widget", LastModified = "2014/01/14", Value = "Expiration date:")]
    public string ExpirationDate => this[nameof (ExpirationDate)];

    /// <summary>Label shown in License expiration widget</summary>
    /// <value>License holder:</value>
    [ResourceEntry("LicenseHolder", Description = "Label shown in License expiration widget", LastModified = "2014/01/14", Value = "License holder:")]
    public string LicenseHolder => this[nameof (LicenseHolder)];

    /// <summary>Label shown in License expiration widget</summary>
    [ResourceEntry("DaysRemaining", Description = "Label shown in License expiration widget", LastModified = "2014/01/14", Value = "Days Remaining")]
    public string DaysRemaining => this[nameof (DaysRemaining)];

    /// <summary>Label shown in License expiration widget</summary>
    [ResourceEntry("LicenseFreeTrialHeader", Description = "Label shown in License expiration widget", LastModified = "2019/03/19", Value = "Free trial")]
    public string LicenseFreeTrialHeader => this[nameof (LicenseFreeTrialHeader)];

    [ResourceEntry("LicenseExpirationWidgetTitle", Description = "", LastModified = "2014/07/01", Value = "License expiration")]
    public string LicenseExpirationWidgetTitle => this[nameof (LicenseExpirationWidgetTitle)];

    [ResourceEntry("LicenseExpirationWidgetDescription", Description = "", LastModified = "2014/01/15", Value = "License Expiration Notification Widget")]
    public string LicenseExpirationWidgetDescription => this[nameof (LicenseExpirationWidgetDescription)];

    [ResourceEntry("LearnMore", Description = "", LastModified = "2014/01/15", Value = "Learn more")]
    public string LearnMore => this[nameof (LearnMore)];

    [ResourceEntry("ExternalLinkSubscriptionLicense", Description = "External Link: Learn more subscription license", LastModified = "2018/10/16", Value = "https://www.progress.com/sitefinity-cms/editions/purchase-faq")]
    public string ExternalLinkSubscriptionLicense => this[nameof (ExternalLinkSubscriptionLicense)];

    [ResourceEntry("ExternalLinkSubscriptionBenefits", Description = "External Link: Learn more subscription benefits", LastModified = "2018/10/12", Value = "https://www.progress.com/support/sitefinity-maintenance-benefits")]
    public string ExternalLinkSubscriptionBenefits => this[nameof (ExternalLinkSubscriptionBenefits)];

    [ResourceEntry("Details", Description = "", LastModified = "2014/06/19", Value = "Details")]
    public string Details => this[nameof (Details)];

    /// <summary>Label shown in license info page.</summary>
    /// <value>License type</value>
    [ResourceEntry("LicenseType", Description = "Label shown in license info page.", LastModified = "2014/07/11", Value = "License type")]
    public string LicenseType => this[nameof (LicenseType)];

    /// <summary>Label shown in license info page.</summary>
    /// <value>Package and add-ons</value>
    [ResourceEntry("PackageAndAddons", Description = "Label shown in license info page.", LastModified = "2020/09/29", Value = "Package and add-ons")]
    public string PackageAndAddons => this[nameof (PackageAndAddons)];

    /// <summary>
    /// Label shown in license info page when the current license is a perpetual license.
    /// </summary>
    /// <value>Perpetual</value>
    [ResourceEntry("Perpetual", Description = "Label shown in license info page when the current license is a perpetual license.", LastModified = "2014/07/11", Value = "Perpetual")]
    public string Perpetual => this[nameof (Perpetual)];

    /// <summary>
    /// Label shown in license info page when the current license is a subscription license.
    /// </summary>
    /// <value>Subscription</value>
    [ResourceEntry("Subscription", Description = "Label shown in license info page when the current license is a subscription license.", LastModified = "2014/07/11", Value = "Subscription")]
    public string Subscription => this[nameof (Subscription)];

    /// <summary>
    /// Translated message, similar to "Version and Licensing".
    /// </summary>
    /// <value>Title of the license information screen.</value>
    [ResourceEntry("VersionAndLicensing", Description = "Title of the license information screen.", LastModified = "2010/10/12", Value = "Version and Licensing")]
    public string VersionAndLicensing => this[nameof (VersionAndLicensing)];

    /// <summary>Gets the product.</summary>
    /// <value>The product.</value>
    [ResourceEntry("Product", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Product")]
    public string Product => this[nameof (Product)];

    /// <summary>Gets the purchase information.</summary>
    /// <value>The purchase information.</value>
    [ResourceEntry("PurchaseInformation", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Purchase Information")]
    public string PurchaseInformation => this[nameof (PurchaseInformation)];

    /// <summary>Gets the purchase information.</summary>
    /// <value>The purchase information.</value>
    [ResourceEntry("LicenseDetailsHeader", Description = "Label shown in license info page.", LastModified = "2010/03/19", Value = "License details")]
    public string LicenseDetailsHeader => this[nameof (LicenseDetailsHeader)];

    /// <summary>
    /// phrase: The following parameters are applied for all domains.
    /// </summary>
    [ResourceEntry("LicenseDetailsHeaderNote", Description = "phrase: The following parameters are applied for all domains.", LastModified = "2010/10/11", Value = "The following parameters are applied for all domains")]
    public string LicenseDetailsHeaderNote => this[nameof (LicenseDetailsHeaderNote)];

    /// <summary>License Problems Header</summary>
    [ResourceEntry("LicenseProblems", Description = "Header shown in license info page.", LastModified = "2010/03/12", Value = "License status")]
    public string LicenseProblems => this[nameof (LicenseProblems)];

    /// <summary>word: Domains</summary>
    [ResourceEntry("DomainsHeader", Description = "word: Domains", LastModified = "2012/10/04", Value = "Domains")]
    public string DomainsHeader => this[nameof (DomainsHeader)];

    /// <summary>phrase: AdditionalExtensions</summary>
    [ResourceEntry("AdditionalExtensions", Description = "phrase: Additional Extensions", LastModified = "2012/10/11", Value = "Additional Extensions")]
    public string AdditionalExtensions => this[nameof (AdditionalExtensions)];

    /// <summary>phrase: PageLimitation</summary>
    [ResourceEntry("PageLimitation", Description = "phrase: Page Limitation", LastModified = "2012/10/11", Value = "Page Limitation")]
    public string PageLimitation => this[nameof (PageLimitation)];

    /// <summary>Gets the enter license content message.</summary>
    /// <value>The enter license content message.</value>
    [ResourceEntry("EnterLicenseContentMessage", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Enter the License data")]
    public string EnterLicenseContentMessage => this[nameof (EnterLicenseContentMessage)];

    /// <summary>Gets the license pageId number message.</summary>
    /// <value>Invalid License Info</value>
    [ResourceEntry("InvalidLicenseInfo", Description = "Invalid License Info", LastModified = "2010/03/12", Value = "Invalid License Info")]
    public string InvalidLicenseInfo => this[nameof (InvalidLicenseInfo)];

    /// <summary>LicenseIdNumberMessage</summary>
    /// <value>LicenseIdNumberMessage</value>
    [ResourceEntry("LicenseIdNumberMessage", Description = "LicenseIdNumberMessage", LastModified = "2010/03/12", Value = "License key (xx-digits)")]
    public string LicenseIdNumberMessage => this[nameof (LicenseIdNumberMessage)];

    /// <summary>Please, enter license key!</summary>
    /// <value>Please, enter license key!</value>
    [ResourceEntry("EmptyLicenseFieldMsg", Description = "Please, enter license key!", LastModified = "2010/03/12", Value = "Please, enter license key!")]
    public string EmptyLicenseFieldMsg => this[nameof (EmptyLicenseFieldMsg)];

    /// <summary>Gets the update license.</summary>
    /// <value>The update license.</value>
    [ResourceEntry("UpdateLicense", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Continue")]
    public string UpdateLicense => this[nameof (UpdateLicense)];

    /// <summary>the enter license contents message.</summary>
    [ResourceEntry("LicenseContentMessage", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "License data")]
    public string LicenseContentMessage => this[nameof (LicenseContentMessage)];

    /// <summary>Gets the Enter license ID message.</summary>
    /// <value>The Enter license ID message.</value>
    [ResourceEntry("EnterLicenseIDMessage", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Enter License key (xx-digits)")]
    public string EnterLicenseIDMessage => this[nameof (EnterLicenseIDMessage)];

    /// <summary>Gets the License data dialog confirmation header.</summary>
    [ResourceEntry("LicenseDataConfirmationHeader", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "Your Sitefinity license will be updated.New license details are:")]
    public string LicenseDataConfirmationHeader => this[nameof (LicenseDataConfirmationHeader)];

    [ResourceEntry("PurchaseRenewal", Description = "", LastModified = "2014/01/15", Value = "Purchase a renewal")]
    public string PurchaseRenewal => this[nameof (PurchaseRenewal)];

    [ResourceEntry("ExternalLinkPurchaseRenewal", Description = "External Link: Purchase a renewal", LastModified = "2017/11/22", Value = "http://www.telerik.com/account/renewals-upgrades.aspx?skucid=21")]
    public string ExternalLinkPurchaseRenewal => this[nameof (ExternalLinkPurchaseRenewal)];

    [ResourceEntry("RenewAndUpdate", Description = "", LastModified = "2014/06/19", Value = "*Please renew and update your license file.")]
    public string RenewAndUpdate => this[nameof (RenewAndUpdate)];

    [ResourceEntry("PurchaseLicense", Description = "", LastModified = "2014/07/07", Value = "Purchase a license ")]
    public string PurchaseLicense => this[nameof (PurchaseLicense)];

    [ResourceEntry("ExternalLinkPurchaseLicense", Description = "External Link: Purchase a license", LastModified = "2018/10/12", Value = "https://www.progress.com/sitefinity-cms/editions")]
    public string ExternalLinkPurchaseLicense => this[nameof (ExternalLinkPurchaseLicense)];

    [ResourceEntry("UpdateLicenseBtnMsg", Description = "", LastModified = "2014/07/09", Value = "Update your license file")]
    public string UpdateLicenseBtnMsg => this[nameof (UpdateLicenseBtnMsg)];

    [ResourceEntry("OrLabel", Description = "", LastModified = "2014/07/09", Value = "Or")]
    public string OrLabel => this[nameof (OrLabel)];

    /// <summary>Message shown in license expiration widget.</summary>
    /// <value>*We are unable to update your license file or your payment couldn't be processed.</value>
    [ResourceEntry("UnableToUpdateLicense", Description = "Message shown in license expiration widget.", LastModified = "2014/07/11", Value = "*We are unable to update your license file or your payment couldn't be processed.")]
    public string UnableToUpdateLicense => this[nameof (UnableToUpdateLicense)];

    /// <summary>Message shown in license expiration widget.</summary>
    /// <value>If you do not make a payment your website will stop working in {0} days</value>
    [ResourceEntry("SiteWillStopWorking", Description = "Message shown in license expiration widget.", LastModified = "2014/07/11", Value = "If you do not make a payment your website will stop working in {0} days")]
    public string SiteWillStopWorking => this[nameof (SiteWillStopWorking)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("CurrentDomainNotInLicense", Description = "Warning shown in confirmation screen", LastModified = "2010/03/23", Value = "You are currently using Sitefinity from a domain which is not included in the new license.")]
    public string CurrentDomainNotInLicense => this[nameof (CurrentDomainNotInLicense)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("LicenseKeyIsChanging", Description = "Warning shown in confirmation screen", LastModified = "2010/03/23", Value = "Your License key will be replaced with the new one below.")]
    public string LicenseKeyIsChanging => this[nameof (LicenseKeyIsChanging)];

    /// <summary>Reload license</summary>
    [ResourceEntry("ReloadLicense", Description = "Reload license", LastModified = "2010/03/23", Value = "Reload license")]
    public string ReloadLicense => this[nameof (ReloadLicense)];

    /// <summary>Reload license</summary>
    [ResourceEntry("Refresh", Description = "Refresh", LastModified = "2010/03/23", Value = "Refresh")]
    public string Refresh => this[nameof (Refresh)];

    [ResourceEntry("LicenseExpirationDateApproaching", Description = "", LastModified = "2014/01/14", Value = "Your expiration date is approaching")]
    public string LicenseExpirationDateApproaching => this[nameof (LicenseExpirationDateApproaching)];

    [ResourceEntry("LicenseExpiredSubscriptionMessage", Description = "", LastModified = "2014/07/09", Value = "Your subscription for Sitefinity {0} has expired")]
    public string LicenseExpiredSubscriptionMessage => this[nameof (LicenseExpiredSubscriptionMessage)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("ToRunSitefinityYouNeedToActivete", Description = "Activate a license for Progress Sitefinity CMS", LastModified = "2018/09/21", Value = "Activate a license for Progress Sitefinity CMS")]
    public string ToRunSitefinityYouNeedToActivete => this[nameof (ToRunSitefinityYouNeedToActivete)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("ViewAndCompareAllEditions", Description = "Compare all editions", LastModified = "2010/03/23", Value = "Compare all editions")]
    public string ViewAndCompareAllEditions => this[nameof (ViewAndCompareAllEditions)];

    /// <summary>
    /// Gets External Link: Current Domain Not In License warning
    /// </summary>
    [ResourceEntry("ExternalLinkViewAndCompareAllEditions", Description = "External Link: Compare all editions", LastModified = "2018/09/20", Value = "https://www.progress.com/sitefinity-cms/editions")]
    public string ExternalLinkViewAndCompareAllEditions => this[nameof (ExternalLinkViewAndCompareAllEditions)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("WhatTypeOfInstallation", Description = "What type of installation you want to use? ", LastModified = "2010/03/23", Value = "What type of installation you want to use? ")]
    public string WhatTypeOfInstallation => this[nameof (WhatTypeOfInstallation)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("EnterEmailAndThePassword", Description = "Enter the email and the password from your Telerik account", LastModified = "2018/09/21", Value = "Enter the email and the password from your Telerik account")]
    public string EnterEmailAndThePassword => this[nameof (EnterEmailAndThePassword)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("IfYouHaveLicense", Description = "If you have already a license file upload it here to activate it.", LastModified = "2010/03/23", Value = "If you have already a license file upload it here to activate it.")]
    public string IfYouHaveLicense => this[nameof (IfYouHaveLicense)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("ActivateLicense", Description = "Activate License", LastModified = "2010/03/23", Value = "Activate License")]
    public string ActivateLicense => this[nameof (ActivateLicense)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("UseDownloadedLicense", Description = "Use a <strong>license file</strong> you have downloaded", LastModified = "2018/10/03", Value = "Use a <strong>license file</strong> you have downloaded")]
    public string UseDownloadedLicense => this[nameof (UseDownloadedLicense)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("UseSitefinityAccount", Description = "Use the <strong>email and password</strong> for your Telerik account", LastModified = "2018/09/20", Value = "Use the <strong>email and password</strong> for your Telerik account")]
    public string UseSitefinityAccount => this[nameof (UseSitefinityAccount)];

    /// <summary>Current Domain Not In License warning</summary>
    [ResourceEntry("Edition", Description = "Edition", LastModified = "2010/03/23", Value = "Edition")]
    public string Edition => this[nameof (Edition)];

    /// <summary>Forgot password</summary>
    [ResourceEntry("ForgotPassword", Description = "Forgot email or password", LastModified = "2018/10/03", Value = "Forgot password")]
    public string ForgotPassword => this[nameof (ForgotPassword)];

    /// <summary>Gets External Link: Forgotten email or password</summary>
    [ResourceEntry("ExternalLinkForgotPassword", Description = "External Link: Forgotten email or password", LastModified = "2018/09/20", Value = "https://www.telerik.com/login/v2/telerik")]
    public string ExternalLinkForgotPassword => this[nameof (ExternalLinkForgotPassword)];

    /// <summary>Gets External Link: Telerik account</summary>
    [ResourceEntry("ExternalLinkTelerikAccount", Description = "External Link: Telerik account", LastModified = "2018/09/20", Value = "https://www.telerik.com/login/v2/telerik")]
    public string ExternalLinkTelerikAccount => this[nameof (ExternalLinkTelerikAccount)];

    /// <summary>Get your extended trial license</summary>
    [ResourceEntry("GetYourExtendedTrialLicense", Description = "Get your extended trial license", LastModified = "2011/03/08", Value = "Get your extended trial license")]
    public string GetYourExtendedTrialLicense => this[nameof (GetYourExtendedTrialLicense)];

    /// <summary>
    /// If you need your trial period to be extended please, contact sitefinitysales@progress.com
    /// </summary>
    [ResourceEntry("IfYouNeedToExtend", Description = "If you need your trial period to be extended please, contact sitefinitysales@progress.com", LastModified = "2011/03/08", Value = "If you need your trial period to be <b>extended</b> please, contact <a href=\"mailto:sitefinitysales@progress.com\">sitefinitysales@progress.com</a>")]
    public string IfYouNeedToExtend => this[nameof (IfYouNeedToExtend)];

    /// <summary>
    /// Gets External Link: If you want to extend the hosted trial period Purchase a renewal on our website
    /// </summary>
    [ResourceEntry("ExternalLinkIfYouNeedToExtendHosted", Description = "External Link:  If you want to extend the hosted trial period Purchase a renewal on our website", LastModified = "2018/10/12", Value = "https://www.progress.com/sitefinity-cms/editions")]
    public string ExternalLinkIfYouNeedToExtendHosted => this[nameof (ExternalLinkIfYouNeedToExtendHosted)];

    /// <summary>
    /// If you want to extend the hosted trial period Purchase a renewal on our website or contact sitefinitysales@progress.com
    /// </summary>
    [ResourceEntry("IfYouNeedToExtendHosted", Description = "If you want to extend the hosted trial period Purchase a renewal on our website or contact sitefinitysales@progress.com", LastModified = "2017/11/23", Value = "If you want to extend the hosted trial period <a href=\"{0}\" target=\"_blank\">Purchase a renewal</a> on our website or contact <a href=\"mailto:sitefinitysales@progress.com\">sitefinitysales@progress.com</a>")]
    public string IfYouNeedToExtendHosted => this[nameof (IfYouNeedToExtendHosted)];

    /// <summary>If you have already purchased a renewal</summary>
    [ResourceEntry("IfPurchasedHostedRenewal", Description = "If you have already purchased a renewal", LastModified = "2011/06/24", Value = "If you have already purchased a renewal")]
    public string IfPurchasedHostedRenewal => this[nameof (IfPurchasedHostedRenewal)];

    /// <summary>Activate renewed license</summary>
    [ResourceEntry("ActivateRenewedHostedLicense", Description = "Activate renewed license", LastModified = "2011/06/24", Value = "Activate renewed license")]
    public string ActivateRenewedHostedLicense => this[nameof (ActivateRenewedHostedLicense)];

    /// <summary>If the trial period has been extended</summary>
    [ResourceEntry("IfTheTrialPeriodHasBeenExtended", Description = "If the trial period has been extended", LastModified = "2011/03/08", Value = "If the trial period has been extended")]
    public string IfTheTrialPeriodHasBeenExtended => this[nameof (IfTheTrialPeriodHasBeenExtended)];

    /// <summary>Activate a new valid license</summary>
    [ResourceEntry("ActivateNewValidLicense", Description = "Activate a new valid license", LastModified = "2011/03/08", Value = "Activate a new valid license")]
    public string ActivateNewValidLicense => this[nameof (ActivateNewValidLicense)];

    /// <summary>
    /// Make sure you use the license file for Sitefinity CMS version {0}.
    /// </summary>
    [ResourceEntry("SitefinityVersionMessage", Description = "Make sure you use the license file for Sitefinity CMS version {0}.", LastModified = "2019/02/27", Value = "Make sure you use the license file for Sitefinity CMS <strong>version {0}</strong>.")]
    public string SitefinityVersionMessage => this[nameof (SitefinityVersionMessage)];

    /// <summary>
    /// The uploaded license file does not match your Sitefinity CMS version. Upload the license file for Sitefinity CMS version {0}.
    /// </summary>
    [ResourceEntry("InvalidLicenseVersion", Description = "The uploaded license file does not match your Sitefinity CMS version. Upload the license file for Sitefinity CMS version {0}.", LastModified = "2019/02/27", Value = "The uploaded license file does not match your Sitefinity CMS version. Upload the license file for Sitefinity CMS version {0}.")]
    public string InvalidLicenseVersion => this[nameof (InvalidLicenseVersion)];

    /// <summary>Confirm license.</summary>
    [ResourceEntry("ConfirmLicense", Description = "Confirm license", LastModified = "2019/02/27", Value = "Confirm license")]
    public string ConfirmLicense => this[nameof (ConfirmLicense)];

    /// <summary>License cannot be activated.</summary>
    [ResourceEntry("LicenseCannotBeActivated", Description = "License cannot be activated", LastModified = "2019/02/27", Value = "License cannot be activated")]
    public string LicenseCannotBeActivated => this[nameof (LicenseCannotBeActivated)];

    /// <summary>Gets the license save successfull.</summary>
    /// <value>The license save successfull.</value>
    [ResourceEntry("LicenseSavedSuccessfull", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "The license is updated. You are now using Sitefinity in licensed mode for the registered domains.")]
    public string LicenseSavedSuccessfull => this[nameof (LicenseSavedSuccessfull)];

    /// <summary>Gets the license not saved successfully.</summary>
    /// <value>The license not saved successfully.</value>
    [ResourceEntry("LicenseNotSavedSuccessfully", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "The license will not be updated. The license data you have entered is not valid. ")]
    public string LicenseNotSavedSuccessfully => this[nameof (LicenseNotSavedSuccessfully)];

    /// <summary>License Is Up To Date message</summary>
    [ResourceEntry("LicenseIsUpToDate", Description = "Message shown in license info page when the license is current", LastModified = "2010/03/12", Value = "Your license is current, no need to update")]
    public string LicenseIsUpToDate => this[nameof (LicenseIsUpToDate)];

    /// <summary>Missing license key label</summary>
    [ResourceEntry("MissingLicenseKey", Description = "Message shown in license info page for the license key when the license is not ok", LastModified = "2010/03/12", Value = "Missing license key")]
    public string MissingLicenseKey => this[nameof (MissingLicenseKey)];

    /// <summary>Add License message</summary>
    [ResourceEntry("AddLicense", Description = "Message shown in license info page, reminding to add a license , when such is missing", LastModified = "2010/03/12", Value = "Enter License key")]
    public string AddLicense => this[nameof (AddLicense)];

    /// <summary>Update message</summary>
    [ResourceEntry("Update", Description = "Update license button label", LastModified = "2010/03/12", Value = "Update")]
    public string Update => this[nameof (Update)];

    /// <summary>Gets the license save successfull.</summary>
    /// <value>The license save successfull.</value>
    [ResourceEntry("LicenseFileSaveProblem", Description = "Message shown in license info page.", LastModified = "2010/03/12", Value = "The system was not able to save the license")]
    public string LicenseFileSaveProblem => this[nameof (LicenseFileSaveProblem)];

    /// <summary>License server exception</summary>
    /// <value>License server exception</value>
    [ResourceEntry("ServerWrongCredentials", Description = "Incorrect email and/or password ", LastModified = "2010/03/12", Value = "<h1>Incorrect email and/or password</h1><p class=\"sfNote sfMTop15\">If you need any help with licensing please, contact <a href=\"mailto:sitefinitysales@progress.com\">sitefinitysales@progress.com</a></p>")]
    public string ServerWrongCredentials => this[nameof (ServerWrongCredentials)];

    /// <summary>Gets External Link: License server exception</summary>
    /// <value>License server exception</value>
    [ResourceEntry("ExternalLinkLicenseServerException", Description = "External Link: License server exception", LastModified = "2018/10/22", Value = "https://www.progress.com/login")]
    public string ExternalLinkLicenseServerException => this[nameof (ExternalLinkLicenseServerException)];

    /// <summary>License server exception</summary>
    /// <value>License server exception</value>
    [ResourceEntry("ServerWrongNoAvailableLicense", Description = "No license available", LastModified = "2017/11/23", Value = "<h1>There is no license available for this edition</h1><p class=\"sfMTop15\">To be able to activate a license you need to purchase it first.<br /><a href=\"{0}\" target=\"_blank\">Login</a> to your Telerik account to make a purchase.</p><p class=\"sfNote sfMTop15\">If you need any help with licensing please, contact <a href=\"mailto:sitefinitysales@progress.com\">sitefinitysales@progress.com</a></p>")]
    public string ServerWrongNoAvailableLicense => this[nameof (ServerWrongNoAvailableLicense)];

    /// <summary>License server exception</summary>
    /// <value>License server exception</value>
    [ResourceEntry("ServerMoreThanOneLicense", Description = "More than one license available.", LastModified = "2017/11/23", Value = "<h1>More than one license is available for this edition</h1><p class=\"sfMTop15\"><a href=\"{0}\" target=\"_blank\">Login</a> to your Telerik account, download the license file you want to use and activate it manually.</p><p class=\"sfNote sfErrorDetail\">If you need any help with licensing please, contact <a href=\"mailto:sitefinitysales@progress.com\">sitefinitysales@progress.com</a></p>")]
    public string ServerMoreThanOneLicense => this[nameof (ServerMoreThanOneLicense)];

    /// <summary>License server exception</summary>
    /// <value>License server exception</value>
    [ResourceEntry("ServerConnectionProblem", Description = "Connection problem", LastModified = "2010/03/12", Value = "<h1>An error has occurred while trying to get the license from the licensing server </h1><p class=\"sfMTop15\">Please, contact <a href=\"mailto:sitefinitysales@progress.com\">sitefinitysales@progress.com</a> for further assistance</p>")]
    public string GeneralServerError => this["ServerConnectionProblem"];

    /// <summary>
    /// Label used when the subscription monthly license has expired.
    /// </summary>
    /// <value>Your subscription for Sitefinity {0} has expired*</value>
    [ResourceEntry("LicenseExpiredMonthlySubscriptionMessage", Description = "Label used when the subscription monthly license has expired.", LastModified = "2014/07/11", Value = "Your subscription for Sitefinity {0} has expired*")]
    public string LicenseExpiredMonthlySubscriptionMessage => this[nameof (LicenseExpiredMonthlySubscriptionMessage)];

    /// <summary>Gets External link for: Sitefinity website</summary>
    /// <value>https://www.progress.com/sitefinity-cms</value>
    [ResourceEntry("ExternalLinkSitefinityWebsite", Description = "External link for: Sitefinity website", LastModified = "2018/10/12", Value = "https://www.progress.com/sitefinity-cms")]
    public string ExternalLinkSitefinityWebsite => this[nameof (ExternalLinkSitefinityWebsite)];

    /// <summary>
    /// Hint text what is Telerik account when the pop up is opened
    /// </summary>
    /// <value><p><strong>What is a Telerik account?</strong><br />If you have purchased a license you must have a <a href="{0}">Telerik account</a>. Use its credentials to log in below.<br /><br />If you are running a trial version and you don't have a password for your Telerik account, use the <a href="{1}">Forgot password</a> option to generate a new one.</p></value>
    [ResourceEntry("WhayIsTelerikAccountHint", Description = "Hint text what is Telerik account when the pop up is opened", LastModified = "2018/09/20", Value = "<p><strong class='sfBaseTxt'>What is a Telerik account?</strong><br />If you have purchased a license you must have a <a href='{0}' class='sfNowrapLine' target='_blank'>Telerik account</a>. Use its credentials to log in below.<br /><br />If you are running a trial version and you don't have a password for your Telerik account, use the <a href='{1}' class='sfNowrapLine' target='_blank'>Forgot password</a> option to generate a new one.</p>")]
    public string WhayIsTelerikAccountHint => this[nameof (WhayIsTelerikAccountHint)];

    /// <summary>Why renew label</summary>
    /// <value>Why renew</value>
    [ResourceEntry("WhyRenewLabel", Description = "Why renew label", LastModified = "2019/12/02", Value = "Why renew")]
    public string WhyRenewLabel => this[nameof (WhyRenewLabel)];

    /// <summary>
    /// Renew Sitefinity license to get product updates and technical support. message
    /// </summary>
    /// <value>Why renew</value>
    [ResourceEntry("RenewSitefinityLicenseToGetSupportMessage", Description = "Renew Sitefinity license to get product updates and technical support. message", LastModified = "2020/04/07", Value = "Renew Sitefinity license to get product updates and technical support.")]
    public string RenewSitefinityLicenseToGetSupportMessage => this[nameof (RenewSitefinityLicenseToGetSupportMessage)];

    /// <summary>
    /// Sitefinity license to get product updates and technical support.
    /// </summary>
    /// <value>Sitefinity license to get product updates and technical support.</value>
    [ResourceEntry("ExpiredSitefinityLicenseMessage", Description = "Sitefinity license to get product updates and technical support.", LastModified = "2020/04/07", Value = "<strong>Sitefinity license</strong> to get product updates and technical support.")]
    public string ExpiredSitefinityLicenseMessage => this[nameof (ExpiredSitefinityLicenseMessage)];

    /// <summary>
    /// Purchase Sitefinity license to get product updates and technical support.
    /// </summary>
    /// <value>Purchase Sitefinity license to get product updates and technical support.</value>
    [ResourceEntry("TrialLicenseExpiredMessage", Description = "Purchase Sitefinity license to get product updates and technical support.", LastModified = "2020/06/08", Value = "Purchase Sitefinity license to get product updates and technical support.")]
    public string TrialLicenseExpiredMessage => this[nameof (TrialLicenseExpiredMessage)];

    /// <summary>Renew label</summary>
    /// <value>Renew:</value>
    [ResourceEntry("RenewColonLabel", Description = "Renew label", LastModified = "2019/12/02", Value = "Renew:")]
    public string RenewColonLabel => this[nameof (RenewColonLabel)];
  }
}
