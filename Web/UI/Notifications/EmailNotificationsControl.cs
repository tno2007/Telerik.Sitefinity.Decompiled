// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Notifications.EmailNotificationsControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI.Notifications
{
  /// <summary>
  /// Base control that handles the calls for notification subscription.
  /// </summary>
  public class EmailNotificationsControl : SimpleScriptView
  {
    internal const string ScriptName = "Telerik.Sitefinity.Web.UI.Scripts.EmailNotificationsControl.js";
    internal static readonly string VirtualLayoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Backend.EmailNotificationsControl.ascx";
    internal static readonly string VirtualLayoutTemplatePath = ControlUtilities.ToVppPath(EmailNotificationsControl.VirtualLayoutTemplateName);
    private string smptSettingsUrl = "~/Sitefinity/Administration/Settings/Advanced/System/";

    /// <summary>Gets the layout template's relative or virtual path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EmailNotificationsControl.VirtualLayoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the notifications are enabled for the module using this control
    /// </summary>
    public virtual bool NotificationsEnabled { get; set; }

    /// <summary>
    /// Gets or sets the turn on/off notifications configuration location.
    /// </summary>
    /// <value>The turn notifications configuration location.</value>
    internal string TurnNotificationsConfigLocation { get; set; }

    /// <summary>Gets or sets the form description view model.</summary>
    public FormDescriptionViewModel Form { get; set; }

    /// <summary>Gets the check box list control for the notifications</summary>
    public HtmlInputCheckBox NotificationsCheckboxControl => this.Container.GetControl<HtmlInputCheckBox>("notificationsCheckboxControl", true);

    /// <summary>Gets the header tool tip control</summary>
    public SitefinityToolTip HeaderToolTip => this.Container.GetControl<SitefinityToolTip>("headerToolTip", false);

    /// <summary>
    /// Gets or sets a value indicating whether to show a tooltip.
    /// </summary>
    public bool ToolTipVisible { get; set; }

    /// <summary>Gets or sets the content of the tooltip.</summary>
    public string ToolTipContent { get; set; }

    /// <summary>Gets the text field for the emails list</summary>
    public TextField EmailListTextField => this.Container.GetControl<TextField>("emailListTextField", true);

    /// <summary>
    /// Gets the notifications not enabled error message holder
    /// </summary>
    public virtual HtmlControl EmailNotificationsNotEnabled => this.Container.GetControl<HtmlControl>("emailNotificationsNotEnabled", false);

    /// <summary>Gets the notifications not enabled error label</summary>
    public virtual Label EmailNotificationsNotEnabledLabel => this.Container.GetControl<Label>("emailNotificationsNotEnabledLabel", false);

    /// <summary>Tag key</summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddComponentProperty("labelManager", this.Container.GetControl<ClientLabelManager>("labelManager", true).ClientID);
      controlDescriptor.AddProperty("_userEmail", (object) this.UserEmail);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.EmailNotificationsControl.js", typeof (EmailNotificationsControl).Assembly.GetName().ToString())
    };

    /// <summary>Initializes the controls</summary>
    /// <param name="container">The container</param>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.NotificationsEnabled)
      {
        this.EmailNotificationsNotEnabled.Visible = true;
        if (!string.IsNullOrWhiteSpace(this.TurnNotificationsConfigLocation))
          this.smptSettingsUrl = this.TurnNotificationsConfigLocation;
        string str = RouteHelper.ResolveUrl(this.smptSettingsUrl, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
        this.EmailNotificationsNotEnabledLabel.Text = Res.Get<ErrorMessages>().SmtpSettingsAreNotSet.Arrange((object) str);
      }
      this.UserEmail = EmailNotificationsControl.GetCurrentUser().Email;
      this.HeaderToolTip.Content = this.ToolTipContent;
      this.HeaderToolTip.Visible = this.ToolTipVisible;
    }

    private static User GetCurrentUser()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return UserManager.GetManager(currentIdentity.MembershipProvider).GetUser(currentIdentity.Name);
    }

    private string UserEmail { get; set; }
  }
}
