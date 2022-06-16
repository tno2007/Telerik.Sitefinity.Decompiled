// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Configuration.NotificationsSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.ProtectionShield.Configuration
{
  /// <summary>
  /// Settings for the notification service subscriptions for protection shield.
  /// </summary>
  public class NotificationsSettings : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Configuration.NotificationsSettings" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public NotificationsSettings(ConfigElement parent)
      : base(parent)
    {
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
