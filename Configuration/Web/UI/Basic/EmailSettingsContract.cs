// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.SiteSettings;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>
  /// View model class for the email settings basic settings.
  /// </summary>
  [DataContract]
  public class EmailSettingsContract : ISettingsDataContract, IEmailSettingsContract
  {
    private const string DefaultProfileName = "Default";

    /// <summary>Gets or sets the name or IP address of the host.</summary>
    [DataMember]
    public string Host { get; set; }

    /// <summary>Gets or sets the port of the server.</summary>
    [DataMember]
    public int Port { get; set; }

    /// <summary>Gets or sets the username for the server.</summary>
    [DataMember]
    public string Username { get; set; }

    /// <summary>Gets or sets the password for the server.</summary>
    [DataMember]
    public string Password { get; set; }

    /// <summary>Gets or sets the default email to be used.</summary>
    [DataMember]
    public string DefaultSenderEmailAddress { get; set; }

    /// <summary>Gets or sets the default sender name.</summary>
    [DataMember]
    public string DefaultSenderName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the smtp server will use SSL.
    /// </summary>
    [DataMember]
    public bool UseSsl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether bounced messages ought to be tracked.
    /// </summary>
    [DataMember]
    public bool TrackBouncedMessages { get; set; }

    /// <summary>Gets or sets the value for the POP3 server</summary>
    [DataMember]
    public string Pop3Server { get; set; }

    /// <summary>Gets or sets the value for the POP3 username</summary>
    [DataMember]
    public string Pop3Username { get; set; }

    /// <summary>Gets or sets the value for the POP3 password</summary>
    [DataMember]
    public string Pop3Password { get; set; }

    /// <summary>Gets or sets the value for the POP3 port</summary>
    [DataMember]
    public int Pop3Port { get; set; }

    /// <summary>
    /// Gets or sets a value indicating weather if the SSL should be used when connecting to POP3
    /// </summary>
    [DataMember]
    public bool Pop3UsesSSL { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather the type of action that should be undertaken when message bounces with "soft bounce"
    /// </summary>
    [DataMember]
    public BounceAction SoftBounceAction { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather the type of action that should be undertaken when message bounces with "hard bounce"
    /// </summary>
    [DataMember]
    public BounceAction HardBounceAction { get; set; }

    /// <summary>Loads the defaults</summary>
    /// <param name="forEdit">True if for edit</param>
    public void LoadDefaults(bool forEdit = false)
    {
      this.LoadSmtpSettings();
      this.LoadPop3Settings();
    }

    /// <summary>Saves the defaults</summary>
    public void SaveDefaults()
    {
      this.SaveSmtpSettings();
      this.SavePop3Settings();
    }

    /// <summary>Loads smtp settings</summary>
    private void LoadSmtpSettings()
    {
      ISenderProfile smtpSenderProfile = this.GetDefaultSmtpSenderProfile();
      if (smtpSenderProfile == null)
        return;
      this.Host = smtpSenderProfile.CustomProperties["host"];
      int result1;
      int.TryParse(smtpSenderProfile.CustomProperties["port"], out result1);
      this.Port = result1;
      this.Username = smtpSenderProfile.CustomProperties["username"];
      this.Password = smtpSenderProfile.CustomProperties["password"];
      this.DefaultSenderEmailAddress = smtpSenderProfile.CustomProperties["defaultSenderEmailAddress"];
      this.DefaultSenderName = smtpSenderProfile.CustomProperties["defaultSenderName"];
      bool result2;
      bool.TryParse(smtpSenderProfile.CustomProperties["useSSL"], out result2);
      this.UseSsl = result2;
    }

    /// <summary>Saves smtp settings</summary>
    private void SaveSmtpSettings()
    {
      ISenderProfile profile = this.GetDefaultSmtpSenderProfile();
      if (profile == null)
      {
        SmtpSenderProfileProxy senderProfileProxy = new SmtpSenderProfileProxy();
        senderProfileProxy.ProfileType = "smtp";
        senderProfileProxy.ProfileName = "Default";
        profile = (ISenderProfile) senderProfileProxy;
      }
      profile.CustomProperties["host"] = this.Host;
      profile.CustomProperties["port"] = this.Port.ToString();
      profile.CustomProperties["username"] = this.Username;
      profile.CustomProperties["password"] = this.Password;
      profile.CustomProperties["defaultSenderEmailAddress"] = this.DefaultSenderEmailAddress;
      profile.CustomProperties["defaultSenderName"] = this.DefaultSenderName;
      profile.CustomProperties["useSSL"] = this.UseSsl.ToString();
      bool flag = !this.Username.IsNullOrEmpty() || !this.Password.IsNullOrEmpty();
      profile.CustomProperties["useAuthentication"] = flag.ToString();
      SystemManager.GetNotificationService().SaveSenderProfile((ServiceContext) null, profile);
    }

    private ISenderProfile GetDefaultSmtpSenderProfile()
    {
      ISenderProfile smtpSenderProfile = (ISenderProfile) null;
      try
      {
        smtpSenderProfile = SystemManager.GetNotificationService().GetDefaultSenderProfile((ServiceContext) null, "smtp");
      }
      catch
      {
      }
      return smtpSenderProfile;
    }

    private void LoadPop3Settings()
    {
      if (!this.Pop3SettingsAvailable())
        return;
      NewslettersConfig newslettersConfig = Config.Get<NewslettersConfig>();
      this.TrackBouncedMessages = newslettersConfig.TrackBouncedMessages;
      this.Pop3Server = newslettersConfig.Pop3Server;
      this.Pop3Port = newslettersConfig.Pop3Port;
      this.Pop3Username = newslettersConfig.Pop3Username;
      this.Pop3Password = newslettersConfig.Pop3Password;
      this.Pop3UsesSSL = newslettersConfig.Pop3UsesSSL;
      this.SoftBounceAction = newslettersConfig.SoftBounceAction;
      this.HardBounceAction = newslettersConfig.HardBounceAction;
    }

    private void SavePop3Settings()
    {
      if (!this.Pop3SettingsAvailable())
        return;
      ConfigManager manager = ConfigManager.GetManager();
      NewslettersConfig section = manager.GetSection<NewslettersConfig>();
      section.TrackBouncedMessages = this.TrackBouncedMessages;
      section.Pop3Server = this.Pop3Server;
      section.Pop3Port = this.Pop3Port;
      section.Pop3Username = this.Pop3Username;
      section.Pop3Password = this.Pop3Password;
      section.Pop3UsesSSL = this.Pop3UsesSSL;
      section.SoftBounceAction = this.SoftBounceAction;
      section.HardBounceAction = this.HardBounceAction;
      manager.SaveSection((ConfigSection) section);
    }

    private bool Pop3SettingsAvailable() => SystemManager.IsModuleEnabled("Newsletters");
  }
}
