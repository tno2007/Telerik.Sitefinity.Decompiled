// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.IEmailSettingsContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Newsletters;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>Contract for the email settings basic settings</summary>
  internal interface IEmailSettingsContract
  {
    /// <summary>Gets or sets the host of the server.</summary>
    string Host { get; set; }

    /// <summary>Gets or sets the port of the server.</summary>
    int Port { get; set; }

    /// <summary>Gets or sets the username for the server.</summary>
    string Username { get; set; }

    /// <summary>Gets or sets the password for the server.</summary>
    string Password { get; set; }

    /// <summary>Gets or sets the default email to be used.</summary>
    string DefaultSenderEmailAddress { get; set; }

    /// <summary>Gets or sets if the smtp server will use SSL.</summary>
    bool UseSsl { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether bounced messages ought to be tracked.
    /// </summary>
    bool TrackBouncedMessages { get; set; }

    /// <summary>Gets or sets the value for the POP3 server</summary>
    string Pop3Server { get; set; }

    /// <summary>Gets or sets the value for the POP3 username</summary>
    string Pop3Username { get; set; }

    /// <summary>Gets or sets the value for the POP3 password</summary>
    string Pop3Password { get; set; }

    /// <summary>Gets or sets the value for the POP3 port</summary>
    int Pop3Port { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether if the SSL should be used when connecting to POP3
    /// </summary>
    bool Pop3UsesSSL { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the type of action that should be undertaken when message bounces with "soft bounce"
    /// </summary>
    BounceAction SoftBounceAction { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the type of action that should be undertaken when message bounces with "hard bounce"
    /// </summary>
    BounceAction HardBounceAction { get; set; }
  }
}
