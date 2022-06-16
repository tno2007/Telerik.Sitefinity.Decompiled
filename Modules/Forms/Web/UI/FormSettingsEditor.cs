// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.FormSettingsEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Web.UI.Basic;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms.Configuration;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Notifications;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  /// <summary>Represents the form settings used in the ZoneEditor</summary>
  public class FormSettingsEditor : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Forms.FormSettingsEditor.ascx");
    private const string controlScript = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormSettingsEditor.js";
    private static readonly string serviceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Forms/FormsService.svc/");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormSettingsEditor.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Id of the edited form</summary>
    public Guid FormDraftId { get; set; }

    /// <summary>ZoneEditorId Property</summary>
    public string ZoneEditorId { get; set; }

    /// <summary>The form description view model.</summary>
    public FormDescriptionViewModel Form { get; set; }

    /// <summary>Gets or sets the current culture.</summary>
    public CultureInfo CurrentCulture { get; set; }

    /// <summary>Choice Field with the different restriction options</summary>
    protected ChoiceField RestrictionsChoiceField => this.Container.GetControl<ChoiceField>("restrictionsChoiceField", true);

    /// <summary>Represents the text field for the confirmation box</summary>
    protected TextField SuccessMessageTextField => this.Container.GetControl<TextField>("successMessageTextField", true);

    /// <summary>Represents the text field for the redirect url</summary>
    protected TextField RedirectUrlTextField => this.Container.GetControl<TextField>("redirectUrlTextField", true);

    /// <summary>ChoiceField for the different label placement options</summary>
    protected ChoiceField LabelPlacementChoiceField => this.Container.GetControl<ChoiceField>("labelPlacementChoiceField", true);

    /// <summary>
    /// List with the radiobuttons for the confirmation options
    /// </summary>
    protected IEnumerable<Control> ConfirmationRadioButtons => this.Container.GetControls<RadioButton>().Where<KeyValuePair<string, Control>>((Func<KeyValuePair<string, Control>, bool>) (rb => rb.Value.ID.StartsWith("confirmation"))).Select<KeyValuePair<string, Control>, Control>((Func<KeyValuePair<string, Control>, Control>) (c => c.Value));

    /// <summary>Gets the email notifications control</summary>
    /// <value>The paging selection container.</value>
    protected virtual EmailNotificationsControl EmailNotifications => this.Container.GetControl<EmailNotificationsControl>("emailNotificationsControl", true);

    /// <summary>Gets the reference to the EmailTemplateEditor control</summary>
    protected virtual MessageTemplateEditor EmailTemplateEditor => this.Container.GetControl<MessageTemplateEditor>("emailTemplateEditor", true);

    /// <summary>Gets the reference to the ClientLabelManager control</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets the notifications not enabled error message holder
    /// </summary>
    public virtual HtmlControl EmailNotificationsNotEnabled => this.Container.GetControl<HtmlControl>("emailNotificationsNotEnabled", false);

    /// <summary>Gets the notifications not enabled error label</summary>
    public virtual Label EmailNotificationsNotEnabledLabel => this.Container.GetControl<Label>("emailNotificationsNotEnabledLabel", false);

    public string UserEmail { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.Form.Framework == FormFramework.Mvc)
        this.LabelPlacementChoiceField.Visible = false;
      this.RedirectUrlTextField.Attributes.CssStyle.Add("display", "none");
      FormSettingsEditor.GetCurrentUser();
      this.EmailNotifications.Form = this.Form;
      this.EmailNotifications.NotificationsEnabled = true;
      this.EmailNotifications.ToolTipContent = Res.Get<FormsResources>().FormNotificationEmailTooltipMessage;
      this.EmailNotifications.ToolTipVisible = true;
      if (Config.Get<FormsConfig>().Notifications.Enabled)
        return;
      this.EmailNotificationsNotEnabled.Visible = true;
      this.EmailNotificationsNotEnabledLabel.Text = Res.Get<ErrorMessages>().FormsSmtpSettingsAreNotSet;
    }

    private static User GetCurrentUser()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return UserManager.GetManager(currentIdentity.MembershipProvider).GetUser(currentIdentity.Name);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_serviceUrl", (object) FormSettingsEditor.serviceUrl);
      controlDescriptor.AddProperty("form", (object) this.Form);
      controlDescriptor.AddProperty("confirmationRadioButtons", (object) this.ConfirmationRadioButtons.Select<Control, string>((Func<Control, string>) (b => b.ClientID)));
      controlDescriptor.AddProperty("_zoneEditorId", (object) this.ZoneEditorId);
      controlDescriptor.AddProperty("_userEmail", (object) this.UserEmail);
      controlDescriptor.AddProperty("_defaultSuccessMessage", (object) Res.Get<FormsResources>().SuccessThanksForFillingOutOurForm);
      controlDescriptor.AddProperty("_submitActionsMap", (object) new FormSettingsEditor.EnumMap(typeof (SubmitAction)));
      controlDescriptor.AddProperty("_formLabelPlacementMap", (object) new FormSettingsEditor.EnumMap(typeof (FormLabelPlacement)));
      controlDescriptor.AddProperty("_submitRestrictionMap", (object) new FormSettingsEditor.EnumMap(typeof (SubmitRestriction)));
      controlDescriptor.AddProperty("_currentLanguage", (object) this.CurrentCulture?.Name);
      controlDescriptor.AddComponentProperty("restrictionsChoiceField", this.RestrictionsChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("successMessageTextField", this.SuccessMessageTextField.ClientID);
      controlDescriptor.AddComponentProperty("labelPlacementChoiceField", this.LabelPlacementChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("redirectUrlTextField", this.RedirectUrlTextField.ClientID);
      controlDescriptor.AddComponentProperty("emailListTextField", this.EmailNotifications.EmailListTextField.ClientID);
      controlDescriptor.AddElementProperty("notificationsCheckboxControl", this.EmailNotifications.NotificationsCheckboxControl.ClientID);
      controlDescriptor.AddProperty("_emailNotificationsControlId", (object) this.EmailNotifications.ClientID);
      controlDescriptor.AddComponentProperty("emailTemplateEditor", this.EmailTemplateEditor.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormSettingsEditor.js", this.GetType().Assembly.FullName)
    };

    private class EnumMap
    {
      public EnumMap(Type enumType)
      {
        this.ValuesToNamesMap = this.CreateEnumValueToNameMap(enumType);
        this.NamesToValuesMap = this.CreateEnumNameToValueMap(enumType);
      }

      public Dictionary<string, string> ValuesToNamesMap { get; private set; }

      public Dictionary<string, string> NamesToValuesMap { get; private set; }

      /// <summary>
      /// Creates a map string - int with each of the enumeration members
      /// </summary>
      private Dictionary<string, string> CreateEnumNameToValueMap(Type enumType) => ((IEnumerable<string>) Enum.GetNames(enumType)).ToDictionary<string, string, string>((Func<string, string>) (k => k), (Func<string, string>) (v => ((int) Enum.Parse(enumType, v)).ToString()));

      private Dictionary<string, string> CreateEnumValueToNameMap(Type enumType) => ((IEnumerable<string>) Enum.GetNames(enumType)).ToDictionary<string, string, string>((Func<string, string>) (v => ((int) Enum.Parse(enumType, v)).ToString()), (Func<string, string>) (k => k));
    }
  }
}
