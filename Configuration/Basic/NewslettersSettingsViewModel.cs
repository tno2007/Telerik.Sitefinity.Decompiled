// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Basic.NewslettersSettingsViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;

namespace Telerik.Sitefinity.Configuration.Basic
{
  /// <summary>
  /// View model class for the basic settings of the newsletter module.
  /// </summary>
  [DataContract]
  [Obsolete("Use NewslettersSettingsContract instead.")]
  public class NewslettersSettingsViewModel
  {
    private ConfigManager manager;
    private NewslettersConfig newslettersConfigSection;

    /// <summary>
    /// Gets or sets the name or IP address of the host used for SMTP transactions.
    /// </summary>
    [DataMember]
    public string NotificationsSmtpProfile
    {
      get => this.NewslettersConfigSection.NotificationsSmtpProfile;
      set => this.NewslettersConfigSection.NotificationsSmtpProfile = value;
    }

    /// <summary>Gets or sets the host of the smtp server.</summary>
    [DataMember]
    public string SmtpHost
    {
      get => this.NewslettersConfigSection.SmtpHost;
      set => this.NewslettersConfigSection.SmtpHost = value;
    }

    /// <summary>Gets or sets the port of the smtp server.</summary>
    [DataMember]
    public int SmtpPort
    {
      get => this.NewslettersConfigSection.SmtpPort;
      set => this.NewslettersConfigSection.SmtpPort = value;
    }

    /// <summary>
    /// Gets or sets the value indicating weather smtp server authentication ought to be used.
    /// </summary>
    [DataMember]
    public bool UseSmtpAuthentication
    {
      get => this.NewslettersConfigSection.UseSmtpAuthentication;
      set => this.NewslettersConfigSection.UseSmtpAuthentication = value;
    }

    /// <summary>
    /// Gets or sets the username for the smtp server; used only if server uses authentication.
    /// </summary>
    [DataMember]
    public string SmtpUsername
    {
      get => this.NewslettersConfigSection.SmtpUsername;
      set => this.NewslettersConfigSection.SmtpUsername = value;
    }

    /// <summary>
    /// Gets or sets the password for the smtp server; used only if the server uses authentication.
    /// </summary>
    [DataMember]
    public string SmtpPassword
    {
      get => this.NewslettersConfigSection.SmtpPassword;
      set => this.NewslettersConfigSection.SmtpPassword = value;
    }

    /// <summary>
    /// Gets or sets the value indicating weather a connection to SMTP server
    /// should be made over SSL.
    /// </summary>
    [DataMember]
    public bool UseSmtpSSL
    {
      get => this.NewslettersConfigSection.UseSmtpSSL;
      set => this.NewslettersConfigSection.UseSmtpSSL = value;
    }

    /// <summary>
    /// Gets or sets the value indicating weather bounced messages ought to be tracked.
    /// </summary>
    [DataMember]
    public bool TrackBouncedMessages
    {
      get => this.NewslettersConfigSection.TrackBouncedMessages;
      set => this.NewslettersConfigSection.TrackBouncedMessages = value;
    }

    /// <summary>Gets or sets the address of the POP3 server.</summary>
    [DataMember]
    public string Pop3Server
    {
      get => this.NewslettersConfigSection.Pop3Server;
      set => this.NewslettersConfigSection.Pop3Server = value;
    }

    /// <summary>Gets or sets the username of the POP3 account.</summary>
    [DataMember]
    public string Pop3Username
    {
      get => this.NewslettersConfigSection.Pop3Username;
      set => this.NewslettersConfigSection.Pop3Username = value;
    }

    /// <summary>Gets or sets the password of the POP3 account.</summary>
    [DataMember]
    public string Pop3Password
    {
      get => this.NewslettersConfigSection.Pop3Password;
      set => this.NewslettersConfigSection.Pop3Password = value;
    }

    /// <summary>Gets or sets the port number for the POP3 server.</summary>
    [DataMember]
    public int Pop3Port
    {
      get => this.NewslettersConfigSection.Pop3Port;
      set => this.NewslettersConfigSection.Pop3Port = value;
    }

    /// <summary>
    /// Gets or sets the value indicating weather connection to POP3 should be made through SSL.
    /// </summary>
    [DataMember]
    public bool Pop3UsesSSL
    {
      get => this.NewslettersConfigSection.Pop3UsesSSL;
      set => this.NewslettersConfigSection.Pop3UsesSSL = value;
    }

    /// <summary>
    /// Gets or sets the action that should be undertaken when message delivery fails
    /// with "soft bounce"
    /// </summary>
    [DataMember]
    public int SoftBounceAction
    {
      get => (int) this.NewslettersConfigSection.SoftBounceAction;
      set => this.NewslettersConfigSection.SoftBounceAction = (BounceAction) value;
    }

    /// <summary>
    /// Gets or sets the action that should be undertaken when message delivery fails
    /// with "hard bounce"
    /// </summary>
    [DataMember]
    public int HardBounceAction
    {
      get => (int) this.NewslettersConfigSection.HardBounceAction;
      set => this.NewslettersConfigSection.HardBounceAction = (BounceAction) value;
    }

    /// <summary>Gets the reference to the configuration manager.</summary>
    private ConfigManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = ConfigManager.GetManager();
        return this.manager;
      }
    }

    /// <summary>
    /// Gets the reference to the newsletters configuration section.
    /// </summary>
    private NewslettersConfig NewslettersConfigSection
    {
      get
      {
        if (this.newslettersConfigSection == null)
          this.newslettersConfigSection = this.Manager.GetSection<NewslettersConfig>();
        return this.newslettersConfigSection;
      }
    }

    /// <summary>Saves the changes made to the configuration.</summary>
    public void SaveChanges() => this.Manager.SaveSection((ConfigSection) this.NewslettersConfigSection);
  }
}
