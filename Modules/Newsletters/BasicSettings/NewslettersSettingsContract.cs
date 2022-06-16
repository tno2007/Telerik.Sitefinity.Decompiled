// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.BasicSettings.NewslettersSettingsContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.SiteSettings;

namespace Telerik.Sitefinity.Modules.Newsletters.BasicSettings
{
  /// <summary>
  /// View model class for the basic settings of the newsletter module.
  /// </summary>
  [DataContract]
  [Obsolete("Use the EmailSettingsContract instead")]
  public class NewslettersSettingsContract : ISettingsDataContract, INewslettersSettings
  {
    /// <summary>
    /// Gets or sets the name or IP address of the host used for SMTP transactions.
    /// </summary>
    [DataMember]
    public string NotificationsSmtpProfile { get; set; }

    /// <summary>Gets or sets the host of the smtp server.</summary>
    [DataMember]
    public string SmtpHost { get; set; }

    /// <summary>Gets or sets the port of the smtp server.</summary>
    [DataMember]
    public int SmtpPort { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather smtp server authentication ought to be used.
    /// </summary>
    [DataMember]
    public bool UseSmtpAuthentication { get; set; }

    /// <summary>
    /// Gets or sets the username for the smtp server; used only if server uses authentication.
    /// </summary>
    [DataMember]
    public string SmtpUsername { get; set; }

    /// <summary>
    /// Gets or sets the password for the smtp server; used only if the server uses authentication.
    /// </summary>
    [DataMember]
    public string SmtpPassword { get; set; }

    /// <summary>Gets or sets the default email to be used.</summary>
    [DataMember]
    public string DefaultSenderEmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather a connection to SMTP server
    /// should be made over SSL.
    /// </summary>
    [DataMember]
    public bool UseSmtpSSL { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather bounced messages ought to be tracked.
    /// </summary>
    [DataMember]
    public bool TrackBouncedMessages { get; set; }

    /// <summary>Gets or sets the address of the POP3 server.</summary>
    [DataMember]
    public string Pop3Server { get; set; }

    /// <summary>Gets or sets the username of the POP3 account.</summary>
    [DataMember]
    public string Pop3Username { get; set; }

    /// <summary>Gets or sets the password of the POP3 account.</summary>
    [DataMember]
    public string Pop3Password { get; set; }

    /// <summary>Gets or sets the port number for the POP3 server.</summary>
    [DataMember]
    public int Pop3Port { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather connection to POP3 should be made through SSL.
    /// </summary>
    [DataMember]
    public bool Pop3UsesSSL { get; set; }

    /// <inheritdoc />
    [DataMember]
    public BounceAction SoftBounceAction { get; set; }

    /// <inheritdoc />
    [DataMember]
    public BounceAction HardBounceAction { get; set; }

    public void LoadDefaults(bool forEdit = false)
    {
      NewslettersConfig newslettersConfig = !forEdit ? Config.Get<NewslettersConfig>() : ConfigManager.GetManager().GetSection<NewslettersConfig>();
      this.SoftBounceAction = newslettersConfig.SoftBounceAction;
      this.HardBounceAction = newslettersConfig.HardBounceAction;
      this.Pop3Password = newslettersConfig.Pop3Password;
      this.Pop3Username = newslettersConfig.Pop3Username;
      this.Pop3UsesSSL = newslettersConfig.Pop3UsesSSL;
      this.Pop3Port = newslettersConfig.Pop3Port;
      this.Pop3Server = newslettersConfig.Pop3Server;
      this.TrackBouncedMessages = newslettersConfig.TrackBouncedMessages;
      this.SmtpHost = newslettersConfig.SmtpHost;
      this.SmtpPassword = newslettersConfig.SmtpPassword;
      this.SmtpUsername = newslettersConfig.SmtpUsername;
      this.SmtpPort = newslettersConfig.SmtpPort;
      this.UseSmtpSSL = newslettersConfig.UseSmtpSSL;
      this.UseSmtpAuthentication = newslettersConfig.UseSmtpAuthentication;
      this.NotificationsSmtpProfile = newslettersConfig.NotificationsSmtpProfile;
      ISenderProfile senderProfile = SystemManager.GetNotificationService().GetSenderProfile(NewslettersSettingsContract.GetServiceContext(), this.NotificationsSmtpProfile);
      this.SmtpUsername = senderProfile.CustomProperties["username"];
      this.SmtpPort = int.Parse(senderProfile.CustomProperties["port"]);
      this.SmtpPassword = senderProfile.CustomProperties["password"];
      this.SmtpHost = senderProfile.CustomProperties["host"];
      this.DefaultSenderEmailAddress = senderProfile.CustomProperties["defaultSenderEmailAddress"];
    }

    public void SaveDefaults()
    {
      ConfigManager manager = ConfigManager.GetManager();
      NewslettersConfig section = manager.GetSection<NewslettersConfig>();
      section.SoftBounceAction = this.SoftBounceAction;
      section.HardBounceAction = this.HardBounceAction;
      section.Pop3Password = this.Pop3Password;
      section.Pop3Username = this.Pop3Username;
      section.Pop3UsesSSL = this.Pop3UsesSSL;
      section.Pop3Port = this.Pop3Port;
      section.Pop3Server = this.Pop3Server;
      section.TrackBouncedMessages = this.TrackBouncedMessages;
      section.SmtpHost = this.SmtpHost;
      section.SmtpPassword = this.SmtpPassword;
      section.SmtpUsername = this.SmtpUsername;
      section.SmtpPort = this.SmtpPort;
      section.UseSmtpSSL = this.UseSmtpSSL;
      section.UseSmtpAuthentication = this.UseSmtpAuthentication;
      section.NotificationsSmtpProfile = this.NotificationsSmtpProfile;
      manager.SaveSection((ConfigSection) section);
      INotificationService notificationService = SystemManager.GetNotificationService();
      ServiceContext serviceContext = NewslettersSettingsContract.GetServiceContext();
      ISenderProfile senderProfile = notificationService.GetSenderProfile(serviceContext, this.NotificationsSmtpProfile);
      senderProfile.CustomProperties["username"] = this.SmtpUsername;
      senderProfile.CustomProperties["port"] = this.SmtpPort.ToString();
      senderProfile.CustomProperties["password"] = this.SmtpPassword;
      senderProfile.CustomProperties["host"] = this.SmtpHost;
      senderProfile.CustomProperties["defaultSenderEmailAddress"] = this.DefaultSenderEmailAddress;
      notificationService.SaveSenderProfile(serviceContext, senderProfile);
    }

    public static ServiceContext GetServiceContext() => new ServiceContext("ThisApplicationKey", "Comments");
  }
}
