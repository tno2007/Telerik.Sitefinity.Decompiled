// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Configuration.GlobalCommentsSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.GenericContent.Configuration
{
  /// <summary>Global Comments settings configuration class.</summary>
  public class GlobalCommentsSettings : CommentsSettings, IGlobalCommentsSettings, ICommentsSettings
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Configuration.GlobalCommentsSettings" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public GlobalCommentsSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display name field.
    /// </summary>
    /// <value>When set to <c>true</c> name field is displayed; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("displayNameField", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisplayNameFieldDescription", Title = "DisplayNameFieldCaption")]
    public bool DisplayNameField
    {
      get => (bool) this["displayNameField"];
      set => this["displayNameField"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the name field is required.
    /// </summary>
    /// <value><c>true</c> if name field is required; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("isNameFieldRequired", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsNameFieldRequiredDescription", Title = "IsNameFieldRequiredCaption")]
    public bool IsNameFieldRequired
    {
      get => (bool) this["isNameFieldRequired"];
      set => this["isNameFieldRequired"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display email field.
    /// </summary>
    /// <value>When set to <c>true</c> email field is displayed; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("displayEmailField", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisplayEmailFieldDescription", Title = "DisplayEmailFieldCaption")]
    public bool DisplayEmailField
    {
      get => (bool) this["displayEmailField"];
      set => this["displayEmailField"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the email field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the email field is required; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("isEmailFieldRequired", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsEmailFieldRequiredDescription", Title = "IsEmailFieldRequiredCaption")]
    public bool IsEmailFieldRequired
    {
      get => (bool) this["isEmailFieldRequired"];
      set => this["isEmailFieldRequired"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display website field.
    /// </summary>
    /// <value>When set to <c>true</c> website field is displayed; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("displayWebSiteField", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisplayWebSiteFieldDescription", Title = "DisplayWebSiteFieldCaption")]
    public bool DisplayWebSiteField
    {
      get => (bool) this["displayWebSiteField"];
      set => this["displayWebSiteField"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the website field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if website field is required; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("isWebSiteFieldRequired", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsWebSiteFieldRequiredDescription", Title = "IsWebSiteFieldRequiredCaption")]
    public bool IsWebSiteFieldRequired
    {
      get => (bool) this["isWebSiteFieldRequired"];
      set => this["isWebSiteFieldRequired"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display message field.
    /// </summary>
    /// <value>When set to <c>true</c> message field is displayed; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("displayMessageField", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisplayMessageFieldDescription", Title = "DisplayMessageFieldCaption")]
    public bool DisplayMessageField
    {
      get => (bool) this["displayMessageField"];
      set => this["displayMessageField"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the message field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the message field is required; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("isMessageFieldRequired", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsMessageFieldRequiredDescription", Title = "IsMessageFieldRequiredCaption")]
    public bool IsMessageFieldRequired
    {
      get => (bool) this["isMessageFieldRequired"];
      set => this["isMessageFieldRequired"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use spam protection image.
    /// </summary>
    /// <value>
    /// When set to <c>true</c> spam protection image is used; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("useSpamProtectionImage", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UseSpamProtectionImageDescription", Title = "UseSpamProtectionImageCaption")]
    public bool UseSpamProtectionImage
    {
      get => (bool) this["useSpamProtectionImage"];
      set => this["useSpamProtectionImage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to hide comments after specified number of days. The number of days is specified with value of property <see cref="P:Telerik.Sitefinity.Modules.GenericContent.Configuration.GlobalCommentsSettings.NumberOfDaysToHideComments" />.
    /// The property depends on property
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments are hidden after specified number of days; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("hideCommentsAfterNumberOfDays", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HideCommentsAfterNumberOfDaysDescription", Title = "HideCommentsAfterNumberOfDaysCaption")]
    public bool HideCommentsAfterNumberOfDays
    {
      get => (bool) this["hideCommentsAfterNumberOfDays"];
      set => this["hideCommentsAfterNumberOfDays"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the number of days after which to hide comments.
    /// </summary>
    /// <value>The number of days to hide comments.</value>
    [ConfigurationProperty("numberOfDaysToHideComments", DefaultValue = 30)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NumberOfDaysToHideCommentsDescription", Title = "NumberOfDaysToHideCommentsCaption")]
    public int NumberOfDaysToHideComments
    {
      get => (int) this["numberOfDaysToHideComments"];
      set => this["numberOfDaysToHideComments"] = (object) value;
    }
  }
}
