// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.BasicSettings.INewslettersSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.SiteSettings;

namespace Telerik.Sitefinity.Modules.Newsletters.BasicSettings
{
  [ConfigSettings("newslettersConfig")]
  [Obsolete("Use the IEmailSettingsContract instead")]
  internal interface INewslettersSettings
  {
    /// <summary>
    /// Gets or sets the name or IP address of the host used for SMTP transactions.
    /// </summary>
    string NotificationsSmtpProfile { get; set; }

    /// <summary>Gets or sets the host of the smtp server.</summary>
    string SmtpHost { get; set; }

    /// <summary>Gets or sets the port of the smtp server.</summary>
    int SmtpPort { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather smtp server authentication ought to be used.
    /// </summary>
    bool UseSmtpAuthentication { get; set; }

    /// <summary>
    /// Gets or sets the username for the smtp server; used only if server uses authentication.
    /// </summary>
    string SmtpUsername { get; set; }

    /// <summary>
    /// Gets or sets the password for the smtp server; used only if the server uses authentication.
    /// </summary>
    string SmtpPassword { get; set; }

    /// <summary>Gets or sets the default email to be used.</summary>
    string DefaultSenderEmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather a connection to SMTP server
    /// should be made over SSL.
    /// </summary>
    bool UseSmtpSSL { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather bounced messages ought to be tracked.
    /// </summary>
    bool TrackBouncedMessages { get; set; }

    /// <summary>Gets or sets the address of the POP3 server.</summary>
    string Pop3Server { get; set; }

    /// <summary>Gets or sets the username of the POP3 account.</summary>
    string Pop3Username { get; set; }

    /// <summary>Gets or sets the password of the POP3 account.</summary>
    string Pop3Password { get; set; }

    /// <summary>Gets or sets the port number for the POP3 server.</summary>
    int Pop3Port { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather connection to POP3 should be made through SSL.
    /// </summary>
    bool Pop3UsesSSL { get; set; }

    /// <summary>
    /// Gets or sets the action that should be undertaken when message delivery fails
    /// with "soft bounce"
    /// </summary>
    BounceAction SoftBounceAction { get; set; }

    /// <summary>
    /// Gets or sets the action that should be undertaken when message delivery fails
    /// with "hard bounce"
    /// </summary>
    BounceAction HardBounceAction { get; set; }
  }
}
