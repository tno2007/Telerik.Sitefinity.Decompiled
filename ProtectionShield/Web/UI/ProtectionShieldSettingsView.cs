// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Web.UI.ProtectionShieldSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.ProtectionShield.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.ProtectionShield.Web.UI
{
  /// <summary>Protection shield settings view</summary>
  public class ProtectionShieldSettingsView : SimpleScriptView
  {
    internal const string ScriptReference = "Telerik.Sitefinity.ProtectionShield.Web.UI.Scripts.ProtectionShieldSettingsView.js";
    internal const string ClickMenuScriptReference = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js";
    private readonly string accessTokensServiceUrl = VirtualPathUtility.ToAbsolute("~/RestApi//Sitefinity/shield/access-tokens");
    private readonly string blockAccessTokenServiceUrl = VirtualPathUtility.ToAbsolute("~/RestApi//Sitefinity/shield/access-tokens/block");
    private readonly string unblockAccessTokenServiceUrl = VirtualPathUtility.ToAbsolute("~/RestApi//Sitefinity/shield/access-tokens/unblock");
    private readonly string removeAccessTokenServiceUrl = VirtualPathUtility.ToAbsolute("~/RestApi//Sitefinity/shield/access-tokens/remove");
    private readonly string protectionShieldServiceUrl = VirtualPathUtility.ToAbsolute("~/RestApi//Sitefinity/shield/protection");
    private readonly string emailInvitationServiceUrl = VirtualPathUtility.ToAbsolute("~/RestApi//Sitefinity/shield/invite");
    private readonly string resendEmailInvitationServiceUrl = VirtualPathUtility.ToAbsolute("~/RestApi//Sitefinity/shield/invite/resend");
    private readonly string basicEmailSettingsUrl = "~/Sitefinity/Administration/Settings/Basic/EmailSettings/";
    private readonly string advancedEmailSettingsUrl = "~/Sitefinity/Administration/Settings/Advanced/Notifications/";
    private static readonly string LayoutTemplatePathName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ProtectionShield.ProtectionShieldSettingsView.ascx");

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get
      {
        if (base.LayoutTemplatePath.IsNullOrEmpty())
          base.LayoutTemplatePath = ProtectionShieldSettingsView.LayoutTemplatePathName;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the access tokens service URL hidden field
    /// </summary>
    protected virtual HiddenField AccessTokensServiceUrlHiddenField => this.Container.GetControl<HiddenField>("hdfAccessTokensServiceUrl", true);

    /// <summary>
    /// Gets the reference to the block access token service URL hidden field
    /// </summary>
    protected virtual HiddenField BlockAccessTokenServiceUrlHiddenField => this.Container.GetControl<HiddenField>("hdfBlockAccessTokenServiceUrl", true);

    /// <summary>
    /// Gets the reference to the unblock access token service URL hidden field
    /// </summary>
    protected virtual HiddenField UnblockAccessTokenServiceUrlHiddenField => this.Container.GetControl<HiddenField>("hdfUnblockAccessTokenServiceUrl", true);

    /// <summary>
    /// Gets the reference to the remove access token service URL hidden field
    /// </summary>
    protected virtual HiddenField RemoveAccessTokenServiceUrlHiddenField => this.Container.GetControl<HiddenField>("hdfRemoveAccessTokenServiceUrl", true);

    /// <summary>
    /// Gets the reference to the protection shield service URL hidden field
    /// </summary>
    protected virtual HiddenField ProtectionShieldServiceUrlHiddenField => this.Container.GetControl<HiddenField>("hdfProtectionShieldServiceUrl", true);

    /// <summary>
    /// Gets the reference to the email invitation service URL hidden field
    /// </summary>
    protected virtual HiddenField EmailInvitationServiceUrlHiddenField => this.Container.GetControl<HiddenField>("hdfEmailInvitationServiceUrl", true);

    /// <summary>
    /// Gets the reference to the resend email invitation service URL hidden field
    /// </summary>
    protected virtual HiddenField ResendEmailInvitationServiceUrlHiddenField => this.Container.GetControl<HiddenField>("hdfResendEmailInvitationServiceUrl", true);

    /// <summary>Gets the reference to shield enable hidden field</summary>
    protected virtual HiddenField IsShieldEnabledHiddenField => this.Container.GetControl<HiddenField>("hdfIsShieldEnabled", true);

    /// <summary>Gets the reference to smtp settings hidden field</summary>
    protected virtual HiddenField SmtpSettingsAreValidHiddenField => this.Container.GetControl<HiddenField>("hdfSmtpSettingsAreValid", true);

    /// <summary>Gets the reference to access statuses hidden field</summary>
    protected virtual HiddenField AccessStatusesJsonHiddenField => this.Container.GetControl<HiddenField>("hdfAccessStatusesJson", true);

    /// <summary>Gets the reference to restriction level hidden field</summary>
    protected virtual HiddenField IsReadOnlyModeEnabledHiddenField => this.Container.GetControl<HiddenField>("hdfIsReadOnlyModeEnabled", true);

    /// <summary>Gets the reference to restriction level hidden field</summary>
    protected virtual HiddenField EnabledForAllSitesHiddenField => this.Container.GetControl<HiddenField>("hdfEnabledForAllSites", true);

    /// <summary>Gets the SMTP configuration error label.</summary>
    /// <value>The SMTP configuration error.</value>
    protected virtual Label SmtpConfigurationError => this.Container.GetControl<Label>("smtpConfigurationError", false);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[0];

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>()
    {
      new System.Web.UI.ScriptReference()
      {
        Assembly = "Telerik.Sitefinity.Resources",
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js"
      },
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.ProtectionShield.Web.UI.Scripts.ProtectionShieldSettingsView.js", typeof (ProtectionShieldSettingsView).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.AccessTokensServiceUrlHiddenField.Value = this.accessTokensServiceUrl;
      this.BlockAccessTokenServiceUrlHiddenField.Value = this.blockAccessTokenServiceUrl;
      this.UnblockAccessTokenServiceUrlHiddenField.Value = this.unblockAccessTokenServiceUrl;
      this.RemoveAccessTokenServiceUrlHiddenField.Value = this.removeAccessTokenServiceUrl;
      this.ProtectionShieldServiceUrlHiddenField.Value = this.protectionShieldServiceUrl;
      this.EmailInvitationServiceUrlHiddenField.Value = this.emailInvitationServiceUrl;
      this.ResendEmailInvitationServiceUrlHiddenField.Value = this.resendEmailInvitationServiceUrl;
      this.IsShieldEnabledHiddenField.Value = SystemManager.IsShieldEnabled.ToString();
      this.SmtpSettingsAreValidHiddenField.Value = this.SmtpSettingsAreValidInternal().ToString();
      bool flag = Config.RestrictionLevel == RestrictionLevel.ReadOnlyConfigFile;
      this.IsReadOnlyModeEnabledHiddenField.Value = flag.ToString();
      this.EnabledForAllSitesHiddenField.Value = Config.Get<ProtectionShieldConfig>().EnabledForAllSites.ToString();
      if (flag)
      {
        this.SmtpConfigurationError.Text = Res.Get<ProtectionShieldResources>().TheSystemHasNotBeenConfiguredToSendEmailsReadOnlyMode;
      }
      else
      {
        string senderProfile = Config.Get<ProtectionShieldConfig>().Notifications.SenderProfile;
        if (senderProfile.IsNullOrEmpty())
        {
          string str = RouteHelper.ResolveUrl(this.basicEmailSettingsUrl, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
          this.SmtpConfigurationError.Text = Res.Get<ProtectionShieldResources>().TheSystemHasNotBeenConfiguredToSendEmailsInBasicSettings.Arrange((object) str);
        }
        else
        {
          string str = RouteHelper.ResolveUrl(this.advancedEmailSettingsUrl, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
          this.SmtpConfigurationError.Text = Res.Get<ProtectionShieldResources>().TheSystemHasNotBeenConfiguredToSendEmailsInAdvancedSettings.Arrange((object) str, (object) senderProfile);
        }
      }
      var data = new
      {
        Allowed = Res.Get<ProtectionShieldResources>().AccessStatusAllowed,
        Blocked = Res.Get<ProtectionShieldResources>().AccessStatusBlocked,
        NotSent = Res.Get<ProtectionShieldResources>().AccessStatusInvitationNotSent,
        Sending = Res.Get<ProtectionShieldResources>().AccessStatusSending
      };
      this.AccessStatusesJsonHiddenField.Value = JsonConvert.SerializeObject((object) data);
    }

    private bool SmtpSettingsAreValidInternal()
    {
      string senderProfile = Config.Get<ProtectionShieldConfig>().Notifications.SenderProfile;
      return SystemManager.GetNotificationService() is ISenderProfileVerifiable notificationService && notificationService.VerifySenderProfile(senderProfile, out string _);
    }
  }
}
