// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner
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
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Security.Principals;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI.Designers
{
  /// <summary>A designer for the User Profile widget.</summary>
  public class UserProfileDesigner : ControlDesignerBase
  {
    private string designedReadDetailViewType;
    private string designedWriteDetailViewType;
    private string designedChangePasswordViewType;
    private string designedChangePasswordQuestionAndAnswerViewType;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.UserProfiles.UserProfileDesigner.ascx");
    private const string designerScriptName = "Telerik.Sitefinity.Security.Web.UI.Scripts.UserProfileDesigner.js";
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner" /> class.
    /// </summary>
    public UserProfileDesigner() => this.LayoutTemplatePath = UserProfileDesigner.layoutTemplatePath;

    /// <summary>
    /// The type name of the view used to display the designed control in read details mode.
    /// </summary>
    public string DesignedReadViewDetailType
    {
      get => this.designedReadDetailViewType.IsNullOrEmpty() ? typeof (UserProfileDetailReadView).FullName : this.designedReadDetailViewType;
      set => this.designedReadDetailViewType = value;
    }

    /// <summary>
    /// The type name of the view used to display the designed control in detail write mode.
    /// </summary>
    public string DesignedWriteViewDetailType
    {
      get => this.designedWriteDetailViewType.IsNullOrEmpty() ? typeof (UserProfileDetailWriteView).FullName : this.designedWriteDetailViewType;
      set => this.designedWriteDetailViewType = value;
    }

    /// <summary>
    /// The type name of the view used to display the designed control in change password mode.
    /// </summary>
    public string DesignedChangePasswordViewType
    {
      get => this.designedChangePasswordViewType.IsNullOrEmpty() ? typeof (UserChangePasswordView).FullName : this.designedChangePasswordViewType;
      set => this.designedChangePasswordViewType = value;
    }

    /// <summary>
    /// The type name of the view used to display the designed control in change password question and answer mode.
    /// </summary>
    public string DesignedChangePasswordQuestionAndAnswerViewType
    {
      get => this.designedChangePasswordQuestionAndAnswerViewType.IsNullOrEmpty() ? typeof (UserChangePasswordQuestionAndAnswerView).FullName : this.designedChangePasswordQuestionAndAnswerViewType;
      set => this.designedChangePasswordQuestionAndAnswerViewType = value;
    }

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

    /// <summary>Gets the user selector.</summary>
    protected UserSelector UserSelector => this.Container.GetControl<UserSelector>("userSelector", true);

    /// <summary>Gets the user select button.</summary>
    protected HyperLink UserSelectButton => this.Container.GetControl<HyperLink>("userSelectButton", true);

    /// <summary>Gets the user select button.</summary>
    protected HtmlGenericControl UserSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("userSelectorWrapper", true);

    /// <summary>Gets the more options link.</summary>
    protected Control MoreOptionsLink => this.Container.GetControl<Control>("moreOptions", true);

    /// <summary>Gets the more wrapper of the more options section.</summary>
    protected HtmlGenericControl MoreOptionsWrapper => this.Container.GetControl<HtmlGenericControl>("moreOptionsWrapper", true);

    /// <summary>
    /// Gets the more wrapper of the more page selector controls.
    /// </summary>
    protected HtmlGenericControl PageSelectorsWrapper => this.Container.GetControl<HtmlGenericControl>("pageSelectorsWrapper", true);

    /// <summary>
    /// Gets the more wrapper of controls defining the action to be executed when a user profile is being successfully submitted.
    /// </summary>
    protected HtmlGenericControl SubmittingUserProfileSuccessActionWrapper => this.Container.GetControl<HtmlGenericControl>("submittingUserProfileSuccessActionWrapper", true);

    /// <summary>Gets the wrapper of the page selector control.</summary>
    protected HtmlGenericControl PageSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("pageSelectorWrapper", true);

    /// <summary>Gets the wrapper of the page selector control.</summary>
    protected HtmlGenericControl RedirectOnSubmitChoiceWrapper => this.Container.GetControl<HtmlGenericControl>("redirectOnSubmitChoiceWrapper", true);

    /// <summary>
    /// Gets the text area displaying the message that should be show when a user profile submission is made.
    /// </summary>
    protected TextBox ProfileUpdatedMessageTextArea => this.Container.GetControl<TextBox>("profileUpdatedMessageTextArea", true);

    /// <summary>
    /// Gets the more wrapper of the controls template selecting controls for the read-write mode.
    /// </summary>
    protected HtmlGenericControl AnonymousUserSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("anonymousUserSelectorWrapper", true);

    /// <summary>
    /// Gets the more wrapper of the controls template selecting controls for the read-write mode.
    /// </summary>
    protected HtmlGenericControl ChangePasswordTemplateSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("changePasswordTemplateSelectorWrapper", true);

    /// <summary>
    /// Gets the more wrapper of the controls template selecting controls for the read-write mode.
    /// </summary>
    protected HtmlGenericControl ChangePasswordQuestionAndAnswerTemplateSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("changePasswordQuestionAndAnswerTemplateSelectorWrapper", true);

    /// <summary>
    /// Gets the more wrapper of the controls displaying the user to show selection.
    /// </summary>
    protected HtmlGenericControl UserSelectionWrapper => this.Container.GetControl<HtmlGenericControl>("userSelectionWrapper", true);

    /// <summary>
    /// Gets the more wrapper of the read mode template selector.
    /// </summary>
    protected HtmlGenericControl EditModeTemplateSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("editModeTemplateSelectorWrapper", true);

    /// <summary>
    /// Gets the more wrapper of the write mode template selector.
    /// </summary>
    protected HtmlGenericControl WriteModeTemplateSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("writeModeTemplateSelectorWrapper", true);

    /// <summary>
    /// Gets the more wrapper of the write mode template selector.
    /// </summary>
    protected HtmlGenericControl SwitchToReadModeRadioWrapper => this.Container.GetControl<HtmlGenericControl>("switchToReadModeRadioWrapper", true);

    /// <summary>
    /// Gets the text field used to configure the CSS class of the designed control.
    /// </summary>
    protected TextField CssClassTextBox => this.Container.GetControl<TextField>("cssClassTextBox", true);

    /// <summary>
    /// Gets the choice field responsible for displaying user profile types.
    /// </summary>
    protected ChoiceField ProfileTypeSelector => this.Container.GetControl<ChoiceField>("profileTypeSelector", true);

    /// <summary>
    /// Gets the choice field responsible for displaying templates for the widget.
    /// </summary>
    protected ChoiceField TemplateSelector => this.Container.GetControl<ChoiceField>("templateSelector", true);

    /// <summary>
    /// Gets the choice field responsible for displaying templates for the widget when the viewing user is not authenticated.
    /// </summary>
    protected ChoiceField TemplateAnonymousUserSelector => this.Container.GetControl<ChoiceField>("templateAnonymousUserSelector", true);

    /// <summary>
    /// Gets the choice field responsible for displaying edit mode templates for the widget.
    /// </summary>
    protected ChoiceField EditModeTemplateSelector => this.Container.GetControl<ChoiceField>("editModeTemplateSelector", true);

    /// <summary>
    /// Gets the choice field responsible for displaying change password mode templates for the widget.
    /// </summary>
    protected ChoiceField ChangePasswordTemplateSelector => this.Container.GetControl<ChoiceField>("changePasswordTemplateSelector", true);

    protected ChoiceField ChangePasswordQuestionAndAnswerTemplateSelector => this.Container.GetControl<ChoiceField>("changePasswordQuestionAndAnswerTemplateSelector", true);

    /// <summary>
    /// Gets the choice field responsible for displaying choosing whether the different view modes will be opened in a different page or not.
    /// </summary>
    protected ChoiceField OpenViewsInExternalPages => this.Container.GetControl<ChoiceField>("openViewsInExternalPages", true);

    /// <summary>Gets the page selector of the designer.</summary>
    protected PagesSelector PageSelector => this.Container.GetControl<PagesSelector>("pageSelector", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the template displayed when the user viewing the control is anonymous.
    /// </summary>
    protected virtual HyperLink EditAnonymousUserTemplateLink => this.Container.GetControl<HyperLink>("editAnonymousUserTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the template displayed when the user viewing the control is anonymous.
    /// </summary>
    protected virtual HyperLink CreateAnonymousUserTemplateLink => this.Container.GetControl<HyperLink>("createAnonymousUserTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the edit mode template.
    /// </summary>
    protected virtual HyperLink EditEditModeTemplateLink => this.Container.GetControl<HyperLink>("editEditModeTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the edit mode template
    /// </summary>
    protected virtual HyperLink CreateEditModeTemplateLink => this.Container.GetControl<HyperLink>("createEditModeTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the change password mode template.
    /// </summary>
    protected virtual HyperLink EditChangePasswordTemplateLink => this.Container.GetControl<HyperLink>("editChangePasswordTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the change password mode template
    /// </summary>
    protected virtual HyperLink CreateChangePasswordTemplateLink => this.Container.GetControl<HyperLink>("createChangePasswordTemplateLink", true);

    protected virtual HyperLink EditChangePasswordQuestionAndAnswerTemplateLink => this.Container.GetControl<HyperLink>("editChangePasswordQuestionAndAnswerTemplateLink", true);

    protected virtual HyperLink CreateChangePasswordQuestionAndAnswerTemplateLink => this.Container.GetControl<HyperLink>("createChangePasswordQuestionAndAnswerTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template
    /// </summary>
    protected virtual HyperLink EditTemplateLink => this.Container.GetControl<HyperLink>("editTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected control template
    /// </summary>
    protected virtual HyperLink CreateTemplateLink => this.Container.GetControl<HyperLink>("createTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the page selector for the edit mode page.
    /// </summary>
    protected virtual HyperLink ShowEditProfilePageSelectorLink => this.Container.GetControl<HyperLink>("showEditProfilePageSelectorLink", true);

    /// <summary>
    /// Gets the link that opens up page selector for the change password page.
    /// </summary>
    protected virtual HyperLink ShowChangePasswordProfilePageSelectorLink => this.Container.GetControl<HyperLink>("showChangePasswordProfilePageSelectorLink", true);

    /// <summary>
    /// Gets the link that opens up page selector for the redirect page.
    /// </summary>
    protected virtual HyperLink ShowRedirectPageSelector => this.Container.GetControl<HyperLink>("showRedirectPageSelector", true);

    /// <summary>
    /// Gets the link showing the chosen page with the edit view.
    /// </summary>
    protected virtual Label SelectedEditProfilePageLabel => this.Container.GetControl<Label>("selectedEditProfilePageLabel", true);

    /// <summary>
    /// Gets the label showing the chosen page with the change password view.
    /// </summary>
    protected virtual Label SelectedChangePasswordPageSelectorLabel => this.Container.GetControl<Label>("selectedChangePasswordPageSelectorLabel", true);

    /// <summary>
    /// Gets the label showing the chosen page with the change password view.
    /// </summary>
    protected virtual HtmlGenericControl ShowMessageRadioLabel => this.Container.GetControl<HtmlGenericControl>("showMessageRadioLabel", true);

    /// <summary>
    /// Gets the label showing the chosen page that the control will redirect to.
    /// </summary>
    protected virtual Label SelectedRedirectPageLabel => this.Container.GetControl<Label>("selectedRedirectPageLabel", true);

    /// <summary>Gets the instance of the RadWindowManager class</summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>Gets the client label manager.</summary>
    /// <value>The client label manager.</value>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.BindUserProfilesTypes();
      this.BindTemplates();
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Raises the <see cref="E:Init" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (FormManager.GetCurrent(this.Page) == null)
        this.Controls.Add((Control) new FormManager());
      base.OnInit(e);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("userSelector", this.UserSelector.ClientID);
      controlDescriptor.AddComponentProperty("templateSelector", this.TemplateSelector.ClientID);
      controlDescriptor.AddComponentProperty("templateAnonymousUserSelector", this.TemplateAnonymousUserSelector.ClientID);
      controlDescriptor.AddComponentProperty("editModeTemplateSelector", this.EditModeTemplateSelector.ClientID);
      controlDescriptor.AddComponentProperty("changePasswordTemplateSelector", this.ChangePasswordTemplateSelector.ClientID);
      controlDescriptor.AddComponentProperty("changePasswordQuestionAndAnswerTemplateSelector", this.ChangePasswordQuestionAndAnswerTemplateSelector.ClientID);
      controlDescriptor.AddComponentProperty("profileTypeSelector", this.ProfileTypeSelector.ClientID);
      controlDescriptor.AddComponentProperty("pageSelector", this.PageSelector.ClientID);
      controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      controlDescriptor.AddComponentProperty("cssClassTextBox", this.CssClassTextBox.ClientID);
      controlDescriptor.AddElementProperty("profileUpdatedMessageTextArea", this.ProfileUpdatedMessageTextArea.ClientID);
      controlDescriptor.AddComponentProperty("openViewsInExternalPages", this.OpenViewsInExternalPages.ClientID);
      controlDescriptor.AddElementProperty("userSelectorButton", this.UserSelectButton.ClientID);
      controlDescriptor.AddElementProperty("moreOptionsLink", this.MoreOptionsLink.ClientID);
      controlDescriptor.AddElementProperty("moreOptionsWrapper", this.MoreOptionsWrapper.ClientID);
      controlDescriptor.AddElementProperty("pageSelectorsWrapper", this.PageSelectorsWrapper.ClientID);
      controlDescriptor.AddElementProperty("userSelectorWrapper", this.UserSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("submittingUserProfileSuccessActionWrapper", this.SubmittingUserProfileSuccessActionWrapper.ClientID);
      controlDescriptor.AddElementProperty("userSelectorWrapper", this.UserSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("submittingUserProfileSuccessActionWrapper", this.SubmittingUserProfileSuccessActionWrapper.ClientID);
      controlDescriptor.AddElementProperty("pageSelectorWrapper", this.PageSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("anonymousUserSelectorWrapper", this.AnonymousUserSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("changePasswordTemplateSelectorWrapper", this.ChangePasswordTemplateSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("changePasswordQuestionAndAnswerTemplateSelectorWrapper", this.ChangePasswordQuestionAndAnswerTemplateSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("redirectOnSubmitChoiceWrapper", this.RedirectOnSubmitChoiceWrapper.ClientID);
      controlDescriptor.AddElementProperty("userSelectionWrapper", this.UserSelectionWrapper.ClientID);
      controlDescriptor.AddElementProperty("editModeTemplateSelectorWrapper", this.EditModeTemplateSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("writeModeTemplateSelectorWrapper", this.WriteModeTemplateSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("switchToReadModeRadioWrapper", this.SwitchToReadModeRadioWrapper.ClientID);
      controlDescriptor.AddElementProperty("editTemplateLink", this.EditTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createTemplateLink", this.CreateTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editAnonymousUserTemplateLink", this.EditAnonymousUserTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createAnonymousUserTemplateLink", this.CreateAnonymousUserTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editEditModeTemplateLink", this.EditEditModeTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createEditModeTemplateLink", this.CreateEditModeTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editChangePasswordTemplateLink", this.EditChangePasswordTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createChangePasswordTemplateLink", this.CreateChangePasswordTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editChangePasswordQuestionAndAnswerTemplateLink", this.EditChangePasswordQuestionAndAnswerTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createChangePasswordQuestionAndAnswerTemplateLink", this.CreateChangePasswordQuestionAndAnswerTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("showEditProfilePageSelectorLink", this.ShowEditProfilePageSelectorLink.ClientID);
      controlDescriptor.AddElementProperty("showChangePasswordProfilePageSelectorLink", this.ShowChangePasswordProfilePageSelectorLink.ClientID);
      controlDescriptor.AddElementProperty("showRedirectPageSelector", this.ShowRedirectPageSelector.ClientID);
      controlDescriptor.AddElementProperty("selectedEditProfilePageLabel", this.SelectedEditProfilePageLabel.ClientID);
      controlDescriptor.AddElementProperty("selectedChangePasswordPageSelectorLabel", this.SelectedChangePasswordPageSelectorLabel.ClientID);
      controlDescriptor.AddElementProperty("showMessageRadioLabel", this.ShowMessageRadioLabel.ClientID);
      controlDescriptor.AddElementProperty("selectedRedirectPageLabel", this.SelectedRedirectPageLabel.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}"));
      controlDescriptor.AddProperty("_editTemplateViewName", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      controlDescriptor.AddProperty("_createTemplateViewName", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      controlDescriptor.AddProperty("_readViewName", (object) "UserProfilesFrontendDetailsRead");
      controlDescriptor.AddProperty("_writeViewName", (object) "UserProfilesFrontendDetailsWrite");
      controlDescriptor.AddProperty("_changePasswordViewName", (object) "ChangePasswordDetailView");
      controlDescriptor.AddProperty("_changePasswordQuestionAndAnswerViewName", (object) "ChangeQuestionAndAnswerDetailView");
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Security.Web.UI.Scripts.UserProfileDesigner.js", typeof (UserProfileDesigner).Assembly.FullName),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", name)
      };
    }

    private void BindUserProfilesTypes()
    {
      foreach (UserProfileTypeViewModel userProfileType in (IEnumerable<UserProfileTypeViewModel>) UserProfilesHelper.GetUserProfileTypes((string) null))
        this.ProfileTypeSelector.Choices.Add(new ChoiceItem()
        {
          Text = userProfileType.Title,
          Value = userProfileType.DynamicTypeName
        });
      if (this.ProfileTypeSelector.Choices.Count >= 2)
        return;
      this.ProfileTypeSelector.Attributes.Add("style", "display:none");
    }

    private void BindTemplates()
    {
      PageManager manager = PageManager.GetManager();
      string filterExpression1 = string.Format("ControlType == \"{0}\" && (Condition == null || Condition == \"{1}\")", (object) this.DesignedReadViewDetailType, (object) typeof (SitefinityProfile).FullName);
      this.BindTemplate(manager, this.TemplateSelector, filterExpression1);
      string filterExpression2 = this.GetTemplateFilterExpression(this.DesignedReadViewDetailType, "anonymousUser");
      this.BindTemplate(manager, this.TemplateAnonymousUserSelector, filterExpression2);
      string filterExpression3 = this.GetTemplateFilterExpression(this.DesignedWriteViewDetailType, "editMode");
      this.BindTemplate(manager, this.EditModeTemplateSelector, filterExpression3);
      string filterExpression4 = this.GetTemplateFilterExpression(this.DesignedChangePasswordViewType, "changePasswordMode");
      this.BindTemplate(manager, this.ChangePasswordTemplateSelector, filterExpression4);
      string filterExpression5 = this.GetTemplateFilterExpression(this.DesignedChangePasswordQuestionAndAnswerViewType, "changeQuestionAndAnswerMode");
      this.BindTemplate(manager, this.ChangePasswordQuestionAndAnswerTemplateSelector, filterExpression5);
    }

    private string GetTemplateFilterExpression(string controlTypeName, string condition) => string.Format("ControlType == \"{0}\" && Condition==\"{1}\"", (object) controlTypeName, (object) condition);

    private void BindTemplate(
      PageManager pageManager,
      ChoiceField templateSelector,
      string filterExpression)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      IQueryable<ControlPresentation> query = (multisiteContext == null ? pageManager.GetPresentationItems<ControlPresentation>() : pageManager.GetPresentationItems<ControlPresentation>(multisiteContext.CurrentSite.Id)).Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
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
  }
}
