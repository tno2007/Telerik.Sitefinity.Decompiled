// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Configuration.NewslettersConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.BasicSettings;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Modules.Newsletters.DynamicLists.Providers;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Newsletters.Configuration
{
  /// <summary>Represents the configuration section for newsletters.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "NewslettersConfigDescription", Title = "NewslettersConfigTitle")]
  public class NewslettersConfig : ModuleConfigBase, INewslettersSettings
  {
    /// <summary>Gets a collection of dynamic list provider settings.</summary>
    [ConfigurationProperty("dynamicListProviders")]
    public ConfigElementDictionary<string, DynamicListProviderSettings> DynamicListProviders => (ConfigElementDictionary<string, DynamicListProviderSettings>) this["dynamicListProviders"];

    /// <summary>
    /// Gets or sets the name of the notificaitons service to use for sending campaign issues.
    /// </summary>
    [ConfigurationProperty("notificationsSmtpProfile", DefaultValue = "Default")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NotificationsSmtpProfile", Title = "NotificationsSmtpProfileTitle")]
    public virtual string NotificationsSmtpProfile
    {
      get => (string) this["notificationsSmtpProfile"];
      set => this["notificationsSmtpProfile"] = (object) value;
    }

    /// <summary>Gets or sets the smtp server host.</summary>
    [ConfigurationProperty("smtpHost")]
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpHost")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual string SmtpHost
    {
      get => (string) this["smtpHost"];
      set => this["smtpHost"] = (object) value;
    }

    /// <summary>Gets or sets the port of the smtp server.</summary>
    [ConfigurationProperty("smtpPort")]
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpPort")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual int SmtpPort
    {
      get
      {
        object obj = this["smtpPort"];
        return obj != null ? (int) obj : 25;
      }
      set => this["smtpPort"] = value >= 0 && value <= (int) ushort.MaxValue ? (object) value : throw new ConfigurationException(Res.Get<NewslettersResources>().SMTPPortOutOfRange);
    }

    /// <summary>
    /// Gets or sets value indicating weather authentication is required by the smtp server.
    /// </summary>
    [ConfigurationProperty("useSmtpAuthentication")]
    [DescriptionResource(typeof (ConfigDescriptions), "UseSmtpAuthentication")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool UseSmtpAuthentication
    {
      get => this["useSmtpAuthentication"] != null && (bool) this["useSmtpAuthentication"];
      set => this["useSmtpAuthentication"] = (object) value;
    }

    /// <summary>
    /// Gets the username to be used to authenticate with the smtp server.
    /// </summary>
    [ConfigurationProperty("smtpUsername")]
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpUsername")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual string SmtpUsername
    {
      get => (string) this["smtpUsername"];
      set => this["smtpUsername"] = (object) value;
    }

    /// <summary>
    /// Gets the password to be used to authenticate with the smtp server.
    /// </summary>
    [ConfigurationProperty("smtpPassword")]
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpPassword")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual string SmtpPassword
    {
      get => (string) this["smtpPassword"];
      set => this["smtpPassword"] = (object) value;
    }

    public string DefaultSenderEmailAddress
    {
      get => (string) this["defaultSenderEmailAddress"];
      set => this["defaultSenderEmailAddress"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value indicating weather smtp server should communicate over SSL.
    /// </summary>
    [ConfigurationProperty("useSmtpSSL")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool UseSmtpSSL
    {
      get
      {
        object obj = this["useSmtpSSL"];
        return obj != null && (bool) obj;
      }
      set => this["useSmtpSSL"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value indicating weather bounced messages ought to be tracked.
    /// </summary>
    [ConfigurationProperty("trackBouncedMessages")]
    public virtual bool TrackBouncedMessages
    {
      get
      {
        object obj = this["trackBouncedMessages"];
        return obj != null && (bool) obj;
      }
      set => this["trackBouncedMessages"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the type of action that should be undertaken when message bounces
    /// with "soft bounce"
    /// </summary>
    [ConfigurationProperty("softBounceAction", DefaultValue = "RetryLater")]
    public virtual BounceAction SoftBounceAction
    {
      get
      {
        object obj = this["softBounceAction"];
        return obj != null ? (BounceAction) obj : BounceAction.DoNothing;
      }
      set => this["softBounceAction"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the type of action that should be undertaken when message bounces
    /// with "hard bounce"
    /// </summary>
    [ConfigurationProperty("hardBounceAction", DefaultValue = "DoNothing")]
    public virtual BounceAction HardBounceAction
    {
      get => (BounceAction) this["hardBounceAction"];
      set => this["hardBounceAction"] = (object) value;
    }

    /// <summary>Gets or sets the POP3 server.</summary>
    [ConfigurationProperty("pop3Server")]
    public virtual string Pop3Server
    {
      get => (string) this["pop3Server"];
      set => this["pop3Server"] = (object) value;
    }

    /// <summary>Gets or sets the username for the POP3 account.</summary>
    [ConfigurationProperty("pop3Username")]
    public virtual string Pop3Username
    {
      get => (string) this["pop3Username"];
      set => this["pop3Username"] = (object) value;
    }

    /// <summary>Gets or sets the password for the POP3 account.</summary>
    [ConfigurationProperty("pop3Password")]
    public virtual string Pop3Password
    {
      get => (string) this["pop3Password"];
      set => this["pop3Password"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the port number to be used by POP3 client.
    /// </summary>
    [ConfigurationProperty("pop3Port")]
    public virtual int Pop3Port
    {
      get => this["pop3Port"] as int? ?? 995;
      set => this["pop3Port"] = value >= 0 && value <= (int) ushort.MaxValue ? (object) value : throw new ConfigurationException(Res.Get<NewslettersResources>().Pop3PortOutOfRange);
    }

    /// <summary>
    /// Gets or sets the value indicating if the SSL should be used when connecting to POP3
    /// </summary>
    [ConfigurationProperty("pop3UsesSSL")]
    public virtual bool Pop3UsesSSL
    {
      get
      {
        object obj = this["pop3UsesSSL"];
        return obj != null && (bool) obj;
      }
      set => this["pop3UsesSSL"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the interval in minutes to retry after an issue was sent to collect bounce messages from the POP3 account.
    /// </summary>
    [ConfigurationProperty("bounceCollectionIntervalMinutes", DefaultValue = 5)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NewslettersBounceCollectionIntervalMinutesDescription", Title = "NewslettersBounceCollectionIntervalMinutesTitle")]
    public virtual int BounceCollectionIntervalMinutes
    {
      get => this["bounceCollectionIntervalMinutes"] as int? ?? 5;
      set => this["bounceCollectionIntervalMinutes"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the amount of retries after an issue was sent to collect bounce messages from the POP3 account.
    /// </summary>
    [ConfigurationProperty("bounceCollectionRetryCount", DefaultValue = 0)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BounceCollectionRetryCountDescription", Title = "BounceCollectionRetryCountTitle")]
    public virtual int BounceCollectionRetryCount
    {
      get => (int) this["bounceCollectionRetryCount"];
      set => this["bounceCollectionRetryCount"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the interval in minutes to retry the sending of a bounced message with bounce action BounceAction.RetryLater.
    /// </summary>
    [ConfigurationProperty("bouncedMessagesRetryIntervalMinutes", DefaultValue = 1)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BouncedMessagesRetryIntervalMinutesDescription", Title = "BouncedMessagesRetryIntervalMinutesTitle")]
    public virtual int BouncedMessagesRetryIntervalMinutes
    {
      get => (int) this["bouncedMessagesRetryIntervalMinutes"];
      set => this["bouncedMessagesRetryIntervalMinutes"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the amount of retries per message with bounce action BounceAction.RetryLater.
    /// </summary>
    [ConfigurationProperty("bouncedMessagesRetryCount", DefaultValue = 3)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BouncedMessagesRetryCountDescription", Title = "BouncedMessagesRetryCountTitle")]
    public virtual int BouncedMessagesRetryCount
    {
      get => (int) this["bouncedMessagesRetryCount"];
      set => this["bouncedMessagesRetryCount"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value, indicating if the Sitefinity email headers should be disabled.
    /// </summary>
    [ConfigurationProperty("disableSitefinityEmailHeaders", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableSitefinityEmailHeadersDescription", Title = "DisableSitefinityEmailHeadersTitle")]
    public virtual bool DisableSitefinityEmailHeaders
    {
      get => (bool) this["disableSitefinityEmailHeaders"];
      set => this["disableSitefinityEmailHeaders"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value, indicating length of the campaign's subject. The default value is 78 in order to meet this standard: http://www.faqs.org/rfcs/rfc2822.html
    /// </summary>
    [ConfigurationProperty("campaignSubjectLength", DefaultValue = 78)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CampaignSubjectLengthDescription", Title = "CampaignSubjectLengthTitle")]
    public virtual int CampaignSubjectLength
    {
      get => (int) this["campaignSubjectLength"];
      set => this["campaignSubjectLength"] = (object) value;
    }

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.DynamicListProviders.Add(new DynamicListProviderSettings((ConfigElement) this.DynamicListProviders)
      {
        Name = "UsersDynamicListProvider",
        Title = "Sitefinity users",
        ProviderType = typeof (UsersDynamicListProvider)
      });
      this.DynamicListProviders.Add(new DynamicListProviderSettings((ConfigElement) this.DynamicListProviders)
      {
        Name = "FormsDynamicListProvider",
        Title = "Sitefinity forms module",
        ProviderType = typeof (FormsDynamicListProvider)
      });
    }

    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) this.Providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores newsletters data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessNewslettersDataProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/Newsletters"
          }
        }
      });
    }
  }
}
