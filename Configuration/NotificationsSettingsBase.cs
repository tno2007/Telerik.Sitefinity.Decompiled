// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.NotificationsSettingsBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Base settings for the notification service subscriptions.
  /// </summary>
  public class NotificationsSettingsBase : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.NotificationsSettingsBase" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public NotificationsSettingsBase(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the status of the notifications is enabled.
    /// </summary>
    /// <value>The status of notifications.</value>
    [ConfigurationProperty("enabled", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NotificationsEnabledDesciption", Title = "NotificationsEnabledTitle")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>Gets or sets the name of the sender profile.</summary>
    /// <value>The sender profile.</value>
    [ConfigurationProperty("senderProfile")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SenderProfileDesciption", Title = "SenderProfileTitle")]
    public string SenderProfile
    {
      get => (string) this["senderProfile"];
      set => this["senderProfile"] = (object) value;
    }

    internal static class BaseProps
    {
      internal const string SenderProfile = "senderProfile";
      internal const string Enabled = "enabled";
    }
  }
}
