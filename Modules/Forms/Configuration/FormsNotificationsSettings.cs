// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Configuration.FormsNotificationsSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Forms.Configuration
{
  /// <summary>
  /// Settings for the notification service subscriptions for forms module.
  /// </summary>
  public class FormsNotificationsSettings : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Configuration.FormsNotificationsSettings" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public FormsNotificationsSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the notifications are enabled.
    /// </summary>
    /// <value>Whether the notifications are enabled.</value>
    [ConfigurationProperty("enabled", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableFormNotificationsDescription", Title = "EnableNotifications")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to send detailed notification message.
    /// </summary>
    /// <value>Whether to send detailed notification message.</value>
    [ConfigurationProperty("enableDetailedNotificationMessage", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableDetailedNotificationMessageDescription", Title = "EnableDetailedNotificationMessageTitle")]
    public bool EnableDetailedNotificationMessage
    {
      get => (bool) this["enableDetailedNotificationMessage"];
      set => this["enableDetailedNotificationMessage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the id of the mail message template used for sending notifications to the subscribed user when a form entry is submitted.
    /// </summary>
    /// <value>The mail message template id.</value>
    [ConfigurationProperty("formEntrySubmittedNotificationTemplateId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "FormEntrySubmittedNotificationTemplateIdTitle")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("As of Sitefinity 12.0 you can customize the form notification templates from the UI. For more information visit: https://www.progress.com/documentation/sitefinity-cms/administration-configure-email-settings")]
    public Guid FormEntrySubmittedNotificationTemplateId
    {
      get => (Guid) this["formEntrySubmittedNotificationTemplateId"];
      set => this["formEntrySubmittedNotificationTemplateId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the id of the mail message template used for sending notifications to the subscribed user when a form entry is edited.
    /// </summary>
    /// <value>The mail message template id.</value>
    [ConfigurationProperty("updatedFormEntrySubmittedNotificationTemplateId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "UpdatedFormEntrySubmittedNotificationTemplateIdTitle")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("As of Sitefinity 12.0 you can customize the form notification templates from the UI. For more information visit: https://www.progress.com/documentation/sitefinity-cms/administration-configure-email-settings")]
    public Guid UpdatedFormEntrySubmittedNotificationTemplateId
    {
      get => (Guid) this["updatedFormEntrySubmittedNotificationTemplateId"];
      set => this["updatedFormEntrySubmittedNotificationTemplateId"] = (object) value;
    }

    /// <summary>Gets or sets the name of the sender profile.</summary>
    /// <value>The sender profile.</value>
    [ConfigurationProperty("senderProfile")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "SenderProfile")]
    public string SenderProfile
    {
      get => (string) this["senderProfile"];
      set => this["senderProfile"] = (object) value;
    }
  }
}
