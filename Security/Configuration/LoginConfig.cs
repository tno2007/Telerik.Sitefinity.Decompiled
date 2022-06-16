// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.LoginConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>Configuration section for login control settings.</summary>
  public class LoginConfig : ConfigSection
  {
    /// <summary>
    /// Gets or sets a value indicating whether to show forgot password link in login form.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if show forgot password should be shown in the login form; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("showForgotPasswordLinkInLoginForm", DefaultValue = "true", IsRequired = true)]
    [DescriptionResource(typeof (ConfigDescriptions), "ShowForgotPasswordLinkInLoginForm")]
    public bool ShowForgotPasswordLinkInLoginForm
    {
      get => (bool) this["showForgotPasswordLinkInLoginForm"];
      set => this["showForgotPasswordLinkInLoginForm"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show change password link in the login form.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if forgot password link should be shown in login form; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("showChangePasswordLinkInLoginForm", DefaultValue = "false", IsRequired = true)]
    [DescriptionResource(typeof (ConfigDescriptions), "ShowChangePasswordLinkInLoginForm")]
    public bool ShowChangePasswordLinkInLoginForm
    {
      get => (bool) this["showChangePasswordLinkInLoginForm"];
      set => this["showChangePasswordLinkInLoginForm"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show register link in the login form.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if register link should be shown in login form; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("showRegisterLinkInLoginForm", DefaultValue = "false", IsRequired = true)]
    [DescriptionResource(typeof (ConfigDescriptions), "ShowRegisterLinkInLoginForm")]
    public bool ShowRegisterLinkInLoginForm
    {
      get => (bool) this["showRegisterLinkInLoginForm"];
      set => this["showRegisterLinkInLoginForm"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show help link in the login form.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if help link should be shown in login form; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("showHelpLinkInLoginForm", DefaultValue = "false", IsRequired = true)]
    [DescriptionResource(typeof (ConfigDescriptions), "ShowHelpLinkInLoginForm")]
    public bool ShowHelpLinkInLoginForm
    {
      get => (bool) this["showHelpLinkInLoginForm"];
      set => this["showHelpLinkInLoginForm"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the default value for the remember me check box on login screens.
    /// </summary>
    [ConfigurationProperty("defaultRememberMeLoginCheckBoxValue", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultRememberMeLoginCheckBoxValueDescription", Title = "DefaultRememberMeLoginCheckBoxValueTitle")]
    public virtual bool DefaultRememberMeLoginCheckBoxValue
    {
      get => (bool) this["defaultRememberMeLoginCheckBoxValue"];
      set => this["defaultRememberMeLoginCheckBoxValue"] = (object) value;
    }

    /// <summary>
    /// Gets ot sets a value indicating if the browser should be allowed to autofill the login fields.
    /// </summary>
    [ConfigurationProperty("disableBrowserAutocomplete", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableBrowserAutocompleteDescription", Title = "DisableBrowserAutocompleteTitle")]
    public virtual bool DisableBrowserAutocomplete
    {
      get => (bool) this["disableBrowserAutocomplete"];
      set => this["disableBrowserAutocomplete"] = (object) value;
    }
  }
}
