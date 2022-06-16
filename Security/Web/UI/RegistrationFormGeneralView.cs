// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView
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
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Security.Principals;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI
{
  public class RegistrationFormGeneralView : ContentViewDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.UserProfiles.RegistrationFormGeneralView.ascx");
    internal const string designerViewJs = "Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.RegistrationFormGeneralView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";
    private const string itemTypesServiceUrlFormat = "~/Sitefinity/Services/UserProfiles/UserProfileTypesService.svc/?itemType={0}";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView" /> class.
    /// </summary>
    public RegistrationFormGeneralView() => this.LayoutTemplatePath = RegistrationFormGeneralView.layoutTemplatePath;

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "registrationFormGeneralViewSettings";

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<UserProfilesResources>().RegistrationFormGeneralViewTitle;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

    /// <summary>Gets a reference to the RoleSelectorWrapper div tag.</summary>
    public virtual HtmlGenericControl RoleSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("roleSelectorWrapper", true);

    /// <summary>Gets a reference to the PageSelectorWrapper div tag.</summary>
    public virtual HtmlGenericControl PageSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("pageSelectorWrapper", true);

    /// <summary>Gets a reference to the SelectRolesButton LinkButton.</summary>
    public virtual LinkButton SelectRolesButton => this.Container.GetControl<LinkButton>("selectRolesButton", true);

    /// <summary>
    /// Gets a reference to the SelectUserButtonLiteral Literal.
    /// </summary>
    public virtual Literal SelectRolesButtonLiteral => this.Container.GetControl<Literal>("selectRolesButtonLiteral", true);

    /// <summary>Gets a reference to the role selector.</summary>
    protected virtual RoleSelector RoleSelector => this.Container.GetControl<RoleSelector>("roleSelector", true);

    /// <summary>Gets a reference to the pages selector.</summary>
    protected virtual PagesSelector PageSelector => this.Container.GetControl<PagesSelector>("pageSelector", true);

    /// <summary>
    /// Gets a reference to the SelectPageButtonLiteral Literal.
    /// </summary>
    protected virtual Literal SelectPageButtonLiteral => this.Container.GetControl<Literal>("selectPageButtonLiteral", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template.
    /// </summary>
    protected virtual HyperLink EditTemplateLink => this.Container.GetControl<HyperLink>("editTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template.
    /// </summary>
    protected virtual HyperLink CreateTemplateLink => this.Container.GetControl<HyperLink>("createTemplateLink", true);

    /// <summary>Gets the instance of the RadWindowManager class</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", false);

    /// <summary>Gets the widget editor dialog.</summary>
    protected virtual Telerik.Web.UI.RadWindow WidgetEditorDialog => this.Container.GetControl<Telerik.Web.UI.RadWindow>("widgetEditorDialog", true);

    /// <summary>
    /// Gets a reference to the client label manager in the control.
    /// </summary>
    /// <value>The client label manager.</value>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets a reference to the MembershipProviderSelector.</summary>
    protected virtual ChoiceField MembershipProviderSelector => this.Container.GetControl<ChoiceField>("membershipProviderSelector", true);

    /// <summary>
    /// Gets the choice field responsible for displaying templates for the widget.
    /// </summary>
    protected virtual ChoiceField TemplateSelector => this.Container.GetControl<ChoiceField>("templateSelector", true);

    /// <summary>Gets a reference to the PagesPanel div panel.</summary>
    public virtual HtmlGenericControl PagesPanel => this.Container.GetControl<HtmlGenericControl>("pagesPanel", true);

    /// <summary>
    /// Gets a reference to the AfterSubmissionMessage TextField.
    /// </summary>
    protected virtual TextField AfterSubmissionMessage => this.Container.GetControl<TextField>("afterSubmissionMessage", true);

    /// <summary>Gets a reference to the SelectPageButton LinkButton.</summary>
    protected virtual LinkButton SelectPageButton => this.Container.GetControl<LinkButton>("selectPageButton", true);

    /// <summary>
    /// Gets a reference to the ConfirmationTextField TextField.
    /// </summary>
    protected virtual TextField ConfirmationTextField => this.Container.GetControl<TextField>("confirmationTextField", true);

    /// <summary>Gets a reference to the CssClassTextField TextField.</summary>
    protected virtual TextField CssClassTextField => this.Container.GetControl<TextField>("cssClassTextField", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.BindMembershipProfiles();
      this.BindTemplates();
      this.PagesPanel.Style["display"] = "none";
      this.PageSelector.UICulture = this.GetUICulture();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Binds the MembershipProviderSelector choice field.</summary>
    private void BindMembershipProfiles()
    {
      IEnumerable<DataProviderBase> contextProviders = UserManager.GetManager().GetContextProviders();
      if (contextProviders.Count<DataProviderBase>() == 1)
      {
        this.MembershipProviderSelector.Visible = false;
      }
      else
      {
        foreach (DataProviderBase dataProviderBase in contextProviders)
          this.MembershipProviderSelector.Choices.Add(new ChoiceItem()
          {
            Text = dataProviderBase.Name,
            Value = dataProviderBase.Name
          });
      }
    }

    private void BindTemplates()
    {
      PageManager manager = PageManager.GetManager();
      string str = string.Format("ControlType == \"{0}\" && (Condition == null || Condition == \"{1}\")", (object) typeof (RegistrationForm).FullName, (object) typeof (SitefinityProfile).FullName);
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      IQueryable<ControlPresentation> query = (multisiteContext == null ? manager.GetPresentationItems<ControlPresentation>() : manager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id)).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
      int? nullable = new int?();
      string filterExpression = str;
      string empty = string.Empty;
      int? skip = new int?(0);
      int? take = new int?(0);
      ref int? local = ref nullable;
      IQueryable<ControlPresentation> source = DataProviderBase.SetExpressions<ControlPresentation>(query, filterExpression, empty, skip, take, ref local);
      Expression<Func<ControlPresentation, string>> keySelector = (Expression<Func<ControlPresentation, string>>) (t => t.Name);
      foreach (ControlPresentation controlPresentation in (IEnumerable<ControlPresentation>) source.OrderBy<ControlPresentation, string>(keySelector))
        this.TemplateSelector.Choices.Add(new ChoiceItem()
        {
          Text = controlPresentation.Name,
          Value = controlPresentation.Id.ToString()
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (RegistrationFormGeneralView).FullName, this.ClientID);
      if (this.MembershipProviderSelector.Visible)
        controlDescriptor.AddComponentProperty("membershipProviderSelector", this.MembershipProviderSelector.ClientID);
      if (this.RadWindowManager != null)
        controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      controlDescriptor.AddComponentProperty("widgetEditorDialog", this.WidgetEditorDialog.ClientID);
      controlDescriptor.AddComponentProperty("templateSelector", this.TemplateSelector.ClientID);
      controlDescriptor.AddElementProperty("roleSelectorWrapper", this.RoleSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("pageSelectorWrapper", this.PageSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("selectRolesButton", this.SelectRolesButton.ClientID);
      controlDescriptor.AddElementProperty("selectRolesButtonLiteral", this.SelectRolesButtonLiteral.ClientID);
      controlDescriptor.AddElementProperty("selectPageButton", this.SelectPageButton.ClientID);
      controlDescriptor.AddElementProperty("selectPageButtonLiteral", this.SelectPageButtonLiteral.ClientID);
      controlDescriptor.AddElementProperty("editTemplateLink", this.EditTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createTemplateLink", this.CreateTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("pagesPanel", this.PagesPanel.ClientID);
      controlDescriptor.AddComponentProperty("confirmationTextField", this.ConfirmationTextField.ClientID);
      controlDescriptor.AddComponentProperty("cssClassField", this.CssClassTextField.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}"));
      string virtualPath = string.Format("~/Sitefinity/Services/UserProfiles/UserProfileTypesService.svc/?itemType={0}", (object) typeof (UserProfileTypeViewModel).FullName);
      controlDescriptor.AddProperty("_itemTypesServiceUrl", (object) VirtualPathUtility.ToAbsolute(virtualPath));
      controlDescriptor.AddProperty("_editTemplateViewName", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      controlDescriptor.AddProperty("_createTemplateViewName", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      controlDescriptor.AddComponentProperty("roleSelector", this.RoleSelector.ClientID);
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
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
        new ScriptReference("Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.RegistrationFormGeneralView.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources")
      };
    }
  }
}
