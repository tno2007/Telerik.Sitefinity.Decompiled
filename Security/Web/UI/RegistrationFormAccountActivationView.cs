// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.RegistrationFormAccountActivationView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Mail;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI
{
  public class RegistrationFormAccountActivationView : ContentViewDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.UserProfiles.RegistrationFormAccountActivationView.ascx");
    internal const string designerViewJs = "Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.RegistrationFormAccountActivationView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/EmailTemplateEditor?ViewName={0}";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView" /> class.
    /// </summary>
    public RegistrationFormAccountActivationView() => this.LayoutTemplatePath = RegistrationFormAccountActivationView.layoutTemplatePath;

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "registrationFormAccountActivationViewSettings";

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<UserProfilesResources>().RegistrationFormAccountActivationViewTitle;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets a reference to the PageSelectorWrapper div tag.</summary>
    public virtual HtmlGenericControl PageSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("pageSelectorWrapper", true);

    /// <summary>Gets a reference to the pages selector.</summary>
    protected virtual PagesSelector PageSelector => this.Container.GetControl<PagesSelector>("pageSelector", true);

    /// <summary>
    /// Gets a reference to the SelectConfirmationPageButton LinkButton.
    /// </summary>
    protected virtual LinkButton SelectConfirmationPageButton => this.Container.GetControl<LinkButton>("selectConfirmationPageButton", true);

    /// <summary>
    /// Gets a reference to the SelectConfirmationPageButtonLiteral Literal.
    /// </summary>
    protected virtual Literal SelectConfirmationPageButtonLiteral => this.Container.GetControl<Literal>("selectConfirmationPageButtonLiteral", true);

    /// <summary>Gets the instance of the RadWindowManager class</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", false);

    /// <summary>Gets the widget editor dialog.</summary>
    protected virtual Telerik.Web.UI.RadWindow WidgetEditorDialog => this.Container.GetControl<Telerik.Web.UI.RadWindow>("widgetEditorDialog", true);

    /// <summary>
    /// Gets a reference to the client label manager in the control.
    /// </summary>
    /// <value>The client label manager.</value>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets a reference to the PagesPanel div panel.</summary>
    public virtual HtmlGenericControl ConfirmationPanel => this.Container.GetControl<HtmlGenericControl>("confirmationPanel", true);

    /// <summary>
    /// Gets a reference to the ConfirmationEmailTemplateSelector ChoiceField.
    /// </summary>
    public virtual ChoiceField ConfirmationEmailTemplateSelector => this.Container.GetControl<ChoiceField>("confirmationEmailTemplateSelector", true);

    /// <summary>
    /// Gets a reference to the SuccessEmailTemplateSelector ChoiceField.
    /// </summary>
    public virtual ChoiceField SuccessEmailTemplateSelector => this.Container.GetControl<ChoiceField>("successEmailTemplateSelector", true);

    /// <summary>
    /// Gets a reference to the EditConfirmationEmailTemplateLink HyperLink.
    /// </summary>
    public virtual HyperLink EditConfirmationEmailTemplateLink => this.Container.GetControl<HyperLink>("editConfirmationEmailTemplateLink", true);

    /// <summary>
    /// Gets a reference to the CreateConfirmationEmailTemplateLink HyperLink.
    /// </summary>
    public virtual HyperLink CreateConfirmationEmailTemplateLink => this.Container.GetControl<HyperLink>("createConfirmationEmailTemplateLink", true);

    /// <summary>
    /// Gets a reference to the EditSuccessEmailTemplateLink HyperLink.
    /// </summary>
    public virtual HyperLink EditSuccessEmailTemplateLink => this.Container.GetControl<HyperLink>("editSuccessEmailTemplateLink", true);

    /// <summary>
    /// Gets a reference to the CreateSuccessEmailTemplateLink HyperLink.
    /// </summary>
    public virtual HyperLink CreateSuccessEmailTemplateLink => this.Container.GetControl<HyperLink>("createSuccessEmailTemplateLink", true);

    /// <summary>Gets a reference to the InfoLabel2 SitefinityLabel.</summary>
    public virtual SitefinityLabel InfoLabel2 => this.Container.GetControl<SitefinityLabel>(nameof (InfoLabel2), true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      SiteMapNode siteMapNodeFromKey = BackendSiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(SiteInitializer.SettingsNodeId.ToString());
      string str = siteMapNodeFromKey != null ? RouteHelper.ResolveUrl(siteMapNodeFromKey.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/Advanced/System" : string.Empty;
      this.InfoLabel2.Text = string.Format(Res.Get<ErrorMessages>().SmtpSettingsAreNotSet, (object) str);
      this.ConfirmationPanel.Style["display"] = "none";
      this.BindTemplates();
      this.PageSelector.UICulture = this.GetUICulture();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void BindTemplates()
    {
      PageManager manager = PageManager.GetManager();
      string filterExpression1 = string.Format("ControlType == \"{0}\" && Condition==\"{1}\"", (object) typeof (RegistrationForm).FullName, (object) RegistrationEmailType.Confirmation.ToString());
      this.BindTemplate(manager, this.ConfirmationEmailTemplateSelector, filterExpression1);
      string filterExpression2 = string.Format("ControlType == \"{0}\" && Condition==\"{1}\"", (object) typeof (RegistrationForm).FullName, (object) RegistrationEmailType.Success.ToString());
      this.BindTemplate(manager, this.SuccessEmailTemplateSelector, filterExpression2);
    }

    private void BindTemplate(
      PageManager pageManager,
      ChoiceField templateSelector,
      string filterExpression)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      IQueryable<ControlPresentation> query = (multisiteContext == null ? pageManager.GetPresentationItems<ControlPresentation>() : pageManager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id)).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "EMAIL_TEMPLATE"));
      int? nullable = new int?();
      string filterExpression1 = filterExpression;
      string empty = string.Empty;
      int? skip = new int?(0);
      int? take = new int?(0);
      ref int? local = ref nullable;
      foreach (ControlPresentation setExpression in (IEnumerable<ControlPresentation>) DataProviderBase.SetExpressions<ControlPresentation>(query, filterExpression1, empty, skip, take, ref local))
        templateSelector.Choices.Add(new ChoiceItem()
        {
          Text = setExpression.Name,
          Value = setExpression.Id.ToString()
        });
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (RegistrationFormAccountActivationView).FullName, this.ClientID);
      string senderProfile = Config.Get<SecurityConfig>().Notifications.SenderProfile;
      bool flag = EmailSender.Get().VerifySenderProfile(senderProfile);
      if (this.RadWindowManager != null)
        controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      controlDescriptor.AddComponentProperty("widgetEditorDialog", this.WidgetEditorDialog.ClientID);
      controlDescriptor.AddElementProperty("pageSelectorWrapper", this.PageSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("selectConfirmationPageButton", this.SelectConfirmationPageButton.ClientID);
      controlDescriptor.AddElementProperty("selectConfirmationPageButtonLiteral", this.SelectConfirmationPageButtonLiteral.ClientID);
      controlDescriptor.AddElementProperty("confirmationPanel", this.ConfirmationPanel.ClientID);
      controlDescriptor.AddElementProperty("editConfirmationEmailTemplateLink", this.EditConfirmationEmailTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createConfirmationEmailTemplateLink", this.CreateConfirmationEmailTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editSuccessEmailTemplateLink", this.EditSuccessEmailTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createSuccessEmailTemplateLink", this.CreateSuccessEmailTemplateLink.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/EmailTemplateEditor?ViewName={0}"));
      controlDescriptor.AddProperty("_editTemplateViewName", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      controlDescriptor.AddProperty("_createTemplateViewName", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      controlDescriptor.AddProperty("_smtpIsSet", (object) flag);
      controlDescriptor.AddComponentProperty("confirmationEmailTemplateSelector", this.ConfirmationEmailTemplateSelector.ClientID);
      controlDescriptor.AddComponentProperty("successEmailTemplateSelector", this.SuccessEmailTemplateSelector.ClientID);
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.RegistrationFormAccountActivationView.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources")
      };
    }
  }
}
