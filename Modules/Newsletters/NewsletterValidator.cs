// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.NewsletterValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.BasicSettings;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>
  /// This class provides validation functions for the Newsletter module.
  /// </summary>
  public static class NewsletterValidator
  {
    private static Regex emailRegex = new Regex("^[a-zA-Z0-9.!#$%&'*\\+\\-/=?^_`{|}~]+@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,63}$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>Determines weather the value is</summary>
    /// <param name="value"></param>
    /// <returns>True if the passed value is a valid email, otherwise false.</returns>
    public static bool IsValidEmail(string value) => !string.IsNullOrEmpty(value) ? NewsletterValidator.emailRegex.IsMatch(value) : throw new ArgumentNullException(nameof (value));

    /// <summary>
    /// Verifies that all the needed smtp settings are present.
    /// </summary>
    /// <param name="newsletterConfig">Instance of the configuration</param>
    public static bool VerifySmtpSettings(NewslettersConfig newsletterConfig = null)
    {
      if (newsletterConfig == null)
        newsletterConfig = Config.Get<NewslettersConfig>();
      string newslettersNotificationProfileName = SystemManager.CurrentContext.GetSetting<NewslettersSettingsContract, INewslettersSettings>().NotificationsSmtpProfile;
      INotificationService notificationService = SystemManager.GetNotificationService();
      if (string.IsNullOrWhiteSpace(newslettersNotificationProfileName))
        throw new InvalidOperationException(Res.Get<NewslettersResources>().NotificationsSmtpProfileNameCannotBeEmpty);
      ISenderProfile senderProfile = notificationService.GetSenderProfiles((QueryParameters) null).FirstOrDefault<ISenderProfile>((Func<ISenderProfile, bool>) (profile => profile.ProfileName == newslettersNotificationProfileName));
      if (senderProfile == null)
        throw new InvalidOperationException(string.Format(Res.Get<NewslettersResources>().NotificationsSmtpProfileNotFound, (object) newslettersNotificationProfileName));
      if (senderProfile.ProfileType != "smtp")
        throw new InvalidOperationException(string.Format(Res.Get<NewslettersResources>().NotificationsSmtpProfileIsNotSmtp, (object) newslettersNotificationProfileName));
      int num = !string.IsNullOrEmpty(senderProfile.CustomProperties["host"]) ? int.Parse(senderProfile.CustomProperties["port"]) : throw new InvalidOperationException(Res.Get<NewslettersResources>().SmtpHostCannotBeEmpty);
      if (num < 0 || num > (int) ushort.MaxValue)
        throw new InvalidOperationException(Res.Get<NewslettersResources>().SmtpPortMustBeInRange);
      if (bool.Parse(senderProfile.CustomProperties["useAuthentication"]))
      {
        if (string.IsNullOrEmpty(senderProfile.CustomProperties["username"]))
          throw new InvalidOperationException(Res.Get<NewslettersResources>().ProvideSmtpUserName);
        if (string.IsNullOrEmpty(senderProfile.CustomProperties["password"]))
          throw new InvalidOperationException(Res.Get<NewslettersResources>().ProvideSmtpPassword);
      }
      return true;
    }
  }
}
