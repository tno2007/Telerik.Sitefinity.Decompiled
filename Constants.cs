// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Constants
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Static class with all the constants used in Sitefinity
  /// </summary>
  public static class Constants
  {
    public const string EmailAddressRegexPattern = "^[a-zA-Z0-9.!#$%&'*\\+\\-/=?^_`{|}~]+@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,63}$";
    /// <summary>Location of the embedded Sitefinity backend themes</summary>
    public const string EmbeddedThemesLocation = "Telerik.Sitefinity.Resources.Themes";
    /// <summary>The virtual path prefix for all embedded resources.</summary>
    public const string SitefinityResourcesPath = "~/SFRes/";
    /// <summary>
    /// The invariant culture name constant which is used to send the invariant culture in the headers of a request
    /// </summary>
    public const string InvariantCultureName = "INVARIANT";
    /// <summary>
    /// Defines the URL pattern for Virtual Path Provider serving Sitefinity pages.
    /// </summary>
    public const string VirtualPathProviderPageService = "~/SFPageService/";
    public const string VirtualPathProviderMvcPages = "~/SFMVCPageService/";
    /// <summary>
    /// Defines the path prefix for all controls whose templates are stored in the database
    /// </summary>
    public const string VirtualPathProviderControlPresentationPath = "~/SfCtrlPresentation/";
    /// <summary>
    /// The template name of the default frontend page template.
    /// </summary>
    public const string DefaultFrontendPageTemplate = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
    /// <summary>
    /// Used internally by Sitefinity to handle embedded templates customized in the database
    /// </summary>
    internal const string ControlTemplatePart = "_SFCT_/";
    /// <summary>
    /// Used internally by Sitefinity to fetch widget templates by developer name.
    /// </summary>
    internal const string ControlTemplateByNamePart = "ByName/";
    /// <summary>
    /// Used internally for handling the site id query string parameter
    /// </summary>
    internal const string SiteIdParamKey = "sf_site";
    /// <summary>
    /// The virtual path prefixes for all sitefinity resources.
    /// </summary>
    internal const string SitefinityResourcesFilesPath = "~/Res/";
    /// <summary>
    /// The virtual path prefixes for all external sitefinity resources.
    /// </summary>
    internal const string SitefinityExternalResourcesPath = "~/ExtRes/";
    internal const string MultisiteContextUserIdKey = "sfMultisiteContextUserId";
    /// <summary>
    /// Used internally to give the name of the items parameter indicating if MultisiteContext.DetermineCurrentSite method should allow request
    /// for user who cannot access the backend of no site.
    /// When the parameter is set to False or not set at all and the user cannot access any backend site he is redirected to NeedAdminRightsPage.
    /// When the parameter is set to True the DetermineCurrentSite method returns the default site.
    /// </summary>
    internal const string SuppressSiteAccessExceptions = "sfSuppressSiteExceptions";
    /// <summary>
    /// Used internally for handling the query string parameter that indicates that the current site id cookie should not be updated.
    /// </summary>
    internal const string SiteTempParamKey = "sf_site_temp";
    internal const string PersonalizationModuleName = "Personalization";
    internal const string ABTestingModuleName = "ABTesting";
    internal const string AnalyticsModuleName = "Analytics";
    internal const string AnalyticsBasePath = "/Sitefinity/marketing/Analytics";
    internal const string EcommerceOffsitePaymentNotificationHandlerUrl = "Ecommerce/offsite-payment-notification/";
    internal const string EcommerceOffsitePaymentExecuteHandlerUrl = "Ecommerce/offsite-payment-execute";
    internal const string EcommerceOffsitePaymentReturnHandlerUrl = "Ecommerce/offsite-payment-return/";
    internal const string SiteCultureKey = "sf_culture";
    internal const string CultureKey = "culture";
    internal const string SiteLanguageKey = "lang";
    internal const string LanguageKey = "language";
    internal static readonly string CoreAssemblyName = typeof (Constants).Assembly.FullName;
    internal static readonly Guid RecycleBinHomePageId = new Guid("A9BD3CD2-0A80-4DCB-9407-2B4F26BFBA33");
    internal static readonly Guid RecycleBinGroupPageId = new Guid("CC3F621C-7E38-4404-8E98-FA4E2094B4D4");
  }
}
