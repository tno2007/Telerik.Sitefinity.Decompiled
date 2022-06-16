﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Configuration.CommentsNotificationSettingsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Comments.Configuration
{
  internal class CommentsNotificationSettingsElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Comments.Configuration.CommentsNotificationSettingsElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public CommentsNotificationSettingsElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the SMTP profile used for sending comments notifications..
    /// </summary>
    /// <value>The profile.</value>
    [ConfigurationProperty("Profile", DefaultValue = "Default")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsProfileDescription", Title = "CommentsProfileCaption")]
    public string Profile
    {
      get => (string) this[nameof (Profile)];
      set => this[nameof (Profile)] = (object) value;
    }

    private static class ConfigProps
    {
      public const string Profile = "Profile";
    }
  }
}
