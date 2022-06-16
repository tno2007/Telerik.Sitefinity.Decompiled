// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.TrackingConsent.TrackingConsentManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TrackingConsent.Configuration;

namespace Telerik.Sitefinity.TrackingConsent
{
  /// <summary>Common actions for tracking consent.</summary>
  public class TrackingConsentManager
  {
    private const string DomainSeparator = ".";
    private static object compiledDialogsSync = new object();
    private static HashSet<string> compiledDialogs = new HashSet<string>();
    private static ResourceTemplateProcessor processor = new ResourceTemplateProcessor();
    internal const string ConsentCookieName = "sf-tracking-consent";
    internal const string DialogRelativePath = "~/App_Data/Sitefinity/TrackingConsent/consentDialog.html";
    internal const string DialogResourcePath = "Telerik.Sitefinity.TrackingConsent.Controls.ConsentDialog.html";

    /// <summary>Gets the consent settings for current context.</summary>
    /// <returns>The consent settings.</returns>
    public static ITrackingConsentSettings GetCurrentConsentSettings()
    {
      string authority = SystemManager.CurrentHttpContext.Request.Url.Authority;
      TrackingConsentConfig trackingConsentConfig = Config.Get<SystemConfig>().TrackingConsentConfig;
      List<string> list = trackingConsentConfig.DomainOverrides.Keys.ToList<string>();
      list.Sort();
      list.Reverse();
      string key = (string) null;
      foreach (string str1 in list)
      {
        if (str1 == authority)
        {
          key = str1;
          break;
        }
        string str2 = str1;
        if (!string.IsNullOrEmpty(str2) && !str2.StartsWith("."))
          str2 = "." + str2;
        if (authority.EndsWith(str2))
        {
          key = str1;
          break;
        }
      }
      return key != null ? (ITrackingConsentSettings) trackingConsentConfig.DomainOverrides[key] : (ITrackingConsentSettings) trackingConsentConfig;
    }

    /// <summary>Checks if current user has agreed to be tracked.</summary>
    /// <returns>True if user has agreed, otherwise false.</returns>
    public static bool HasCurrentUserProvidedConsent()
    {
      HttpCookie cookie = SystemManager.CurrentHttpContext.Request.Cookies["sf-tracking-consent"];
      return cookie != null && cookie.Value == "true";
    }

    /// <summary>
    /// Check if user actions can be tracked. User can be tracked when:
    /// - tracking consent is not required.
    /// - tracking consent is required and user has agreed to be tracked.
    /// </summary>
    /// <returns>True if has provided consent for being tracked, otherwise false.</returns>
    public static bool CanTrackCurrentUser() => !TrackingConsentManager.GetCurrentConsentSettings().ConsentIsRequired || TrackingConsentManager.HasCurrentUserProvidedConsent();

    /// <summary>Initializes consent dialog data.</summary>
    internal static void EnsureConsentDialogExists()
    {
      string str = SystemManager.CurrentHttpContext.Server.MapPath("~/App_Data/Sitefinity/TrackingConsent/consentDialog.html");
      if (File.Exists(str))
        return;
      using (Stream manifestResourceStream = typeof (TrackingConsentManager).Assembly.GetManifestResourceStream("Telerik.Sitefinity.TrackingConsent.Controls.ConsentDialog.html"))
      {
        using (StreamReader streamReader = new StreamReader(manifestResourceStream))
        {
          string end = streamReader.ReadToEnd();
          new FileInfo(str).Directory.Create();
          File.WriteAllText(str, end);
        }
      }
    }

    /// <summary>Generate html for consent dialog.</summary>
    /// <param name="dialogPath">Relative path of the file containing dialog html.</param>
    /// <returns>The html of consent dialog.</returns>
    internal static string GetConsentDialogHtml(string dialogPath)
    {
      if (string.IsNullOrEmpty(dialogPath))
        dialogPath = "~/App_Data/Sitefinity/TrackingConsent/consentDialog.html";
      CultureInfo culture = (CultureInfo) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        culture = SystemManager.CurrentContext.Culture;
      string processedTemplate;
      return !TrackingConsentManager.processor.Process(dialogPath, culture, out processedTemplate) ? (string) null : processedTemplate;
    }
  }
}
