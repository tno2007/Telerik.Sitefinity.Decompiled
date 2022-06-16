// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.LoginUtils
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Mail;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Provides utility functions for Login controls - sending of emails.
  /// </summary>
  public static class LoginUtils
  {
    private static IEmailSender sender;
    /// <summary>
    /// The HTTP query key for passing membership provider name
    /// </summary>
    public const string MembershipProviderQueryKey = "provider";

    /// <summary>
    /// Return a value indicating whether at least one membership providers is properly set up for password recovery
    /// </summary>
    public static bool AreProvidersSetUpForPasswordRecovery => UserManager.GetManager().GetContextProviders().Cast<MembershipDataProvider>().Any<MembershipDataProvider>((Func<MembershipDataProvider, bool>) (p => LoginUtils.IsProviderSetUpForPasswordRecovery(p)));

    /// <summary>
    /// Check if a provider is set up properly for password recovery
    /// </summary>
    /// <param name="provider">Users provider to check</param>
    /// <returns>True if the provider is properly set up, false otherwize.</returns>
    public static bool IsProviderSetUpForPasswordRecovery(MembershipDataProvider provider)
    {
      if (provider is MembershipProviderWrapper)
      {
        MembershipProviderWrapper membershipProviderWrapper = provider as MembershipProviderWrapper;
        return membershipProviderWrapper.EnablePasswordReset || membershipProviderWrapper.EnablePasswordRetrieval;
      }
      if (provider.RecoveryMailBody.IsNullOrWhitespace() || provider.RecoveryMailSubject.IsNullOrWhitespace() || provider.RecoveryMailAddress.IsNullOrWhitespace())
        return false;
      if (!provider.RecoveryMailBody.Contains("<%\\s*Password\\s*%>"))
        return false;
      try
      {
        MailAddress mailAddress = new MailAddress(provider.RecoveryMailAddress);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Retrieve an instance to the class used for sending emails
    /// </summary>
    public static IEmailSender Sender
    {
      get
      {
        if (LoginUtils.sender == null)
        {
          EmailSender emailSender = EmailSender.Get();
          emailSender.SenderProfileName = Config.Get<SecurityConfig>().Notifications.SenderProfile;
          LoginUtils.sender = (IEmailSender) emailSender;
        }
        return LoginUtils.sender;
      }
    }

    /// <summary>Gets the currently authenticated user.</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public static IPrincipal GetUser(Control control)
    {
      IPrincipal user = (IPrincipal) null;
      Page page = control.Page;
      if (page != null)
        return page.User;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext != null)
        user = currentHttpContext.User;
      return user;
    }

    /// <summary>Gets the name of the currently authenticated user.</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public static string GetUserName(Control control)
    {
      string userName = (string) null;
      IPrincipal user = LoginUtils.GetUser(control);
      if (user != null)
      {
        IIdentity identity = user.Identity;
        if (identity != null)
          userName = identity.Name;
      }
      return userName;
    }

    /// <summary>
    /// Returns whether the SMTP settings are set in the configuration.
    /// </summary>
    public static bool AreSmtpSettingsSet
    {
      get
      {
        string senderProfile = Config.Get<SecurityConfig>().Notifications.SenderProfile;
        return EmailSender.Get().VerifySenderProfile(senderProfile);
      }
    }

    /// <summary>
    /// Returns whether the rights to send an email are granted.
    /// </summary>
    public static bool ArePermissionsGrantedForSmtp => LoginUtils.Sender.CheckIfHasSmtpPermissions();

    /// <summary>Gets the SMTP permission error message.</summary>
    /// <value>The SMTP permission error message.</value>
    public static string SmtpPermissionErrorMessage
    {
      get
      {
        LoginUtils.Sender.CheckIfHasSmtpPermissions();
        return LoginUtils.Sender.LastErrorMessage;
      }
    }

    /// <summary>Checks the parameter.</summary>
    /// <param name="param">The param.</param>
    /// <param name="checkForNull">Check if parameter is null.</param>
    /// <param name="checkIfEmpty">if set to <c>true</c> [check if empty].</param>
    /// <param name="checkForCommas">if set to <c>true</c> [check for commas].</param>
    /// <param name="maxSize">Size of the max.</param>
    /// <param name="paramName">Name of the param.</param>
    public static void CheckParameter(
      string param,
      bool checkForNull,
      bool checkIfEmpty,
      bool checkForCommas,
      int maxSize,
      string paramName)
    {
      if (param == null)
      {
        if (checkForNull)
          throw new ArgumentNullException(paramName);
      }
      else
      {
        string str = param.Trim();
        if (checkIfEmpty && str.Length < 1)
          throw new ArgumentException(string.Format(Res.Get<ErrorMessages>().ParameterCanNotBeEmpty, (object) paramName));
        if (maxSize > 0 && str.Length > maxSize)
          throw new ArgumentException(string.Format(Res.Get<ErrorMessages>().ParameterTooLong, (object) paramName, (object) maxSize));
        if (checkForCommas && str.Contains(","))
          throw new ArgumentException(string.Format(Res.Get<ErrorMessages>().ParameterCanNotContainComma, (object) paramName));
      }
    }

    /// <summary>Checks if given email is valid</summary>
    /// <param name="email">Email to check</param>
    public static void CheckValidEmail(string email)
    {
      if (!new Regex("@.*?\\.").Match(email).Success)
        throw new ArgumentException(string.Format(Res.Get<ErrorMessages>().InvalidEmailUsernameErrorMessage, (object) email));
    }

    /// <summary>
    /// Builds an URL for the login page with a return URL for the original place in the forums
    /// </summary>
    /// <param name="loginPageUrl">The URL for the front end login page</param>
    /// <param name="securedUrl">The return URL for the original position in the forum</param>
    /// <returns>URL for the front end login page with a ReturnUrl parameter</returns>
    internal static string BuildLoginPageUrl(string loginPageUrl, string securedUrl) => loginPageUrl + "?" + SecurityManager.AuthenticationReturnUrl + "=" + HttpUtility.UrlEncode(UrlPath.ResolveAbsoluteUrl(securedUrl));

    /// <summary>
    /// Gets the default front end login page for the current site
    /// </summary>
    /// <returns>URL to the frontend login page</returns>
    internal static string GetDefaultLoginUrl()
    {
      string relativePath = string.Empty;
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      if (currentSite.FrontEndLoginPageId != Guid.Empty)
      {
        SiteMapNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(currentSite.FrontEndLoginPageId.ToString());
        if (siteMapNodeFromKey != null)
          relativePath = siteMapNodeFromKey.Url;
      }
      else if (!string.IsNullOrWhiteSpace(currentSite.FrontEndLoginPageUrl))
        relativePath = currentSite.FrontEndLoginPageUrl;
      return UrlPath.ResolveAbsoluteUrl(relativePath);
    }
  }
}
