// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.SitePropertiesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>Represemts the view for setting site properties.</summary>
  public class SitePropertiesView : SimpleScriptView
  {
    private bool _isAllowedStartStopSite = true;
    private bool _isAllowedToConfigureModules = true;
    internal const string scriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.SitePropertiesView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.SitePropertiesView.ascx");
    private const string localizationServiceUrl = "~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/localization/";
    private const string languageBasicSettingsUrl = "~/Sitefinity/Administration/Settings/Basic/Languages/";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.SitePropertiesView" /> class.
    /// </summary>
    public SitePropertiesView() => this.LayoutTemplatePath = SitePropertiesView.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (SitePropertiesView).FullName;

    /// <summary>Gets a reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets a reference to the dialog title.</summary>
    protected virtual HtmlGenericControl DialogTitle => this.Container.GetControl<HtmlGenericControl>("dialogTitle", true);

    /// <summary>
    /// Gets a reference to the site configuration mode container.
    /// </summary>
    protected virtual HtmlGenericControl SiteConfigurationModeContainer => this.Container.GetControl<HtmlGenericControl>("siteConfiguration", true);

    /// <summary>
    /// Gets a reference to the manually configured mode container.
    /// </summary>
    protected virtual HtmlGenericControl ManuallyConfiguredModeContainer => this.Container.GetControl<HtmlGenericControl>("manuallyConfigured", true);

    /// <summary>
    /// Gets a reference to the configured by deployment mode container.
    /// </summary>
    protected virtual HtmlGenericControl ConfiguredByDeploymentModeContainer => this.Container.GetControl<HtmlGenericControl>("configuredByDeployment", true);

    /// <summary>Gets a reference to configure manually link.</summary>
    protected virtual HtmlAnchor ConfigureManuallyBtn => this.Container.GetControl<HtmlAnchor>("configureManuallyBtn", true);

    /// <summary>Gets a reference to configure by deployment link.</summary>
    protected virtual HtmlAnchor ConfigureByDeploymentBtn => this.Container.GetControl<HtmlAnchor>("configureWithDeploymentBtn", true);

    /// <summary>Gets a reference to the properties view wrapper.</summary>
    protected virtual HtmlGenericControl PropertiesViewWrapper => this.Container.GetControl<HtmlGenericControl>("propertiesViewWrapper", true);

    /// <summary>Gets a reference to the domain name view wrapper.</summary>
    protected virtual HtmlGenericControl DomainNameWrapper => this.Container.GetControl<HtmlGenericControl>("domainNameWrapper", true);

    /// <summary>Gets a reference to the languages field view wrapper.</summary>
    protected virtual HtmlGenericControl LanguagesFieldWrapper => this.Container.GetControl<HtmlGenericControl>("languagesFieldWrapper", true);

    /// <summary>
    /// Gets a reference to the use selected languages field view wrapper.
    /// </summary>
    protected virtual HtmlGenericControl UseSelectedLanguagesWrapper => this.Container.GetControl<HtmlGenericControl>("useSelectedLanguagesWrapper", true);

    /// <summary>
    /// Gets a reference to the use all languages field view wrapper.
    /// </summary>
    protected virtual HtmlGenericControl UseAllLanguagesWrapper => this.Container.GetControl<HtmlGenericControl>("useAllLanguagesWrapper", true);

    /// <summary>Gets a reference to the site name text field.</summary>
    protected virtual TextField NameField => this.Container.GetControl<TextField>("nameField", true);

    /// <summary>Gets a reference to the domain text field.</summary>
    protected virtual TextField DomainField => this.Container.GetControl<TextField>("domainField", true);

    /// <summary>Gets a reference to the staging domain text field.</summary>
    protected virtual TextField StagingDomainField => this.Container.GetControl<TextField>("stagingDomainField", true);

    /// <summary>
    /// Gets a reference to the field setting if the site is in process of development.
    /// </summary>
    protected virtual ChoiceField IsSiteInDevelopmentField => this.Container.GetControl<ChoiceField>("isSiteInDevelopmentField", true);

    /// <summary>Gets a reference to details link.</summary>
    protected virtual HtmlAnchor DetailsLink => this.Container.GetControl<HtmlAnchor>("detailsLink", true);

    /// <summary>Gets a reference to details content control.</summary>
    protected virtual HtmlGenericControl DetailsContent => this.Container.GetControl<HtmlGenericControl>("detailsContent", true);

    /// <summary>
    /// Gets a reference to the control wrapping page settings.
    /// </summary>
    protected virtual HtmlGenericControl PageSettingsWrapper => this.Container.GetControl<HtmlGenericControl>("pageSettingsWrapper", true);

    /// <summary>
    /// Gets a reference to the page settings radio button list.
    /// </summary>
    protected virtual RadioButtonList PageSettingsList => this.Container.GetControl<RadioButtonList>("pageSettingsList", true);

    /// <summary>Gets a reference to the generic collection binder.</summary>
    protected virtual GenericCollectionBinder GenericBinder => this.Container.GetControl<GenericCollectionBinder>("genericBinder", true);

    /// <summary>Gets a reference to the sites drop down.</summary>
    protected virtual DropDownList SitesDropDown => this.Container.GetControl<DropDownList>("ddlSites", true);

    /// <summary>Gets a reference to the languages field.</summary>
    protected virtual LanguagesOrderedListField LanguagesField => this.Container.GetControl<LanguagesOrderedListField>("languagesField", true);

    /// <summary>
    /// Gets a reference to the languages field that displays all system cultures.
    /// </summary>
    protected virtual LanguagesOrderedListField AllLanguagesField => this.Container.GetControl<LanguagesOrderedListField>("allLanguagesField", true);

    /// <summary>Gets a reference to the is offline choice field.</summary>
    protected virtual ChoiceField IsOfflineField => this.Container.GetControl<ChoiceField>("isOfflineField", true);

    /// <summary>Gets a reference to the page behaviour field.</summary>
    protected virtual PageBehaviourControl PageBehaviourField => this.Container.GetControl<PageBehaviourControl>("pageBehaviourField", true);

    /// <summary>Gets a reference to the domain aliases text field.</summary>
    protected virtual MultilineTextField DomainAliasesField => this.Container.GetControl<MultilineTextField>("domainAliasesField", true);

    /// <summary>
    /// Gets a reference to the default protocol choice field.
    /// </summary>
    protected virtual ChoiceField DefaultProtocolField => this.Container.GetControl<ChoiceField>("defaultProtocolField", true);

    /// <summary>Gets a reference to the save button.</summary>
    protected virtual HtmlAnchor SaveButton => this.Container.GetControl<HtmlAnchor>("saveButton", true);

    /// <summary>Gets a reference to the continue button.</summary>
    protected virtual HtmlAnchor ContinueButton => this.Container.GetControl<HtmlAnchor>("continueButton", true);

    /// <summary>Gets a reference to the or label.</summary>
    protected virtual HtmlGenericControl OrLabel => this.Container.GetControl<HtmlGenericControl>("orLabel", true);

    /// <summary>Gets a reference to the cancel link.</summary>
    protected virtual HtmlAnchor CancelLink => this.Container.GetControl<HtmlAnchor>("cancelLink", true);

    /// <summary>Gets a reference to the back to sites link.</summary>
    protected virtual HtmlAnchor BackLink => this.Container.GetControl<HtmlAnchor>("backLink", true);

    /// <summary>
    /// Gets a reference to the link expanding Advanced section.
    /// </summary>
    protected virtual HtmlAnchor ExpandLink => this.Container.GetControl<HtmlAnchor>("expandLink", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the buttons panel.</summary>
    protected virtual HtmlGenericControl ButtonsPanel => this.Container.GetControl<HtmlGenericControl>("buttonsPanel", true);

    /// <summary>Gets the loading view.</summary>
    protected virtual HtmlGenericControl LoadingView => this.Container.GetControl<HtmlGenericControl>("loadingView", true);

    /// <summary>
    /// Gets the section that contains the isOffline ChoiceField and the pageBehaviourControl.
    /// </summary>
    protected virtual HtmlGenericControl IsOfflineSection => this.Container.GetControl<HtmlGenericControl>("isOfflineSection", true);

    /// <summary>Gets a reference to select a page radio button.</summary>
    protected virtual RadioButton SelectPageRadio => this.Container.GetControl<RadioButton>("selectPageRadio", true);

    /// <summary>
    /// Gets a reference to use all enabled languages radio button.
    /// </summary>
    protected virtual RadioButton UseAllLanguagesRadio => this.Container.GetControl<RadioButton>("useAllLanguagesRadio", true);

    /// <summary>
    /// Gets a reference to use selected languages a page radio button.
    /// </summary>
    protected virtual RadioButton UseSelectedLanguagesRadio => this.Container.GetControl<RadioButton>("useSelectedLanguagesRadio", true);

    /// <summary>Gets a reference to page selector.</summary>
    protected virtual PageField PageField => this.Container.GetControl<PageField>("pageField", true);

    /// <summary>Gets a reference to enter url radio button.</summary>
    protected virtual RadioButton EnterUrlRadio => this.Container.GetControl<RadioButton>("enterUrlRadio", true);

    /// <summary>Gets a reference to login page url field.</summary>
    protected virtual TextField LoginPageUrlField => this.Container.GetControl<TextField>("loginPageUrlField", true);

    /// <summary>Gets the additional domain validation error.</summary>
    protected virtual HtmlGenericControl AdditionalDomainValidationError => this.Container.GetControl<HtmlGenericControl>("additionalDomainValidationError", true);

    /// <summary>Gets the additional staging domain validation error.</summary>
    protected virtual HtmlGenericControl AdditionalStagingDomainValidationError => this.Container.GetControl<HtmlGenericControl>("additionalStagingDomainValidationError", true);

    /// <summary>Gets a reference to configure modules link.</summary>
    protected virtual HtmlAnchor ConfigureModulesBtn => this.Container.GetControl<HtmlAnchor>("configureModulesBtn", false);

    protected virtual SitefinityToolTip EnableLanguagesTooltip => this.Container.GetControl<SitefinityToolTip>("enableLanguagesTooltip", false);

    protected virtual HtmlGenericControl CultureValidationError => this.Container.GetControl<HtmlGenericControl>("cultureValidationError", false);

    protected virtual HtmlGenericControl EnableMoreLanguagesUrlLocation => this.Container.GetControl<HtmlGenericControl>("enableMoreLanguagesUrlLocation", false);

    protected virtual Literal ConfigureModulesLabel => this.Container.GetControl<Literal>("configureModulesLabel", false);

    protected virtual string AddLanguagesLabel => Res.Get<Labels>().AddLanguagesHellip;

    protected virtual string SelectLanguagesLabel => Res.Get<Labels>().SelectLanguages;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      MultisiteManager manager = MultisiteManager.GetManager();
      this._isAllowedStartStopSite = manager.Provider.GetSecurityRoot().IsGranted("Site", "StartStopSite");
      this._isAllowedToConfigureModules = manager.Provider.GetSecurityRoot().IsGranted("Site", "ConfigureModules");
      if (LicenseState.CheckIsModuleLicensed("FBD4773B-8688-4C75-8563-28BFDA27A185"))
        this.BackLink.InnerText = Res.Get<MultisiteResources>().BackToSites;
      else
        this.BackLink.InnerText = Res.Get<MultisiteResources>().BackToSite;
      this.LanguagesField.UseGlobalLocalization = true;
      this.AllLanguagesField.UseGlobalLocalization = true;
      if (PackagingOperations.IsMultisiteImportExportDisabled())
        this.SiteConfigurationModeContainer.Visible = false;
      this.EnableLanguagesTooltip.Content = string.Format(Res.Get<Labels>().EnableLanguagesTooltip, (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Administration/Settings/Basic/Languages/"));
      this.EnableMoreLanguagesUrlLocation.InnerHtml = string.Format(Res.Get<Labels>().EnableMoreLanguagesUrlLocation, (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Administration/Settings/Basic/Languages/"));
      if (SecurityManager.AllowSeparateUsersPerSite)
        this.ConfigureModulesLabel.Text = Res.Get<MultisiteResources>().ConfigureModulesAndAccess;
      else
        this.ConfigureModulesLabel.Text = Res.Get<MultisiteResources>().ConfigureModules;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("domainNameWrapper", this.DomainNameWrapper.ClientID);
      controlDescriptor.AddElementProperty("languagesFieldWrapper", this.LanguagesFieldWrapper.ClientID);
      controlDescriptor.AddElementProperty("useAllLanguagesWrapper", this.UseAllLanguagesWrapper.ClientID);
      controlDescriptor.AddElementProperty("useSelectedLanguagesWrapper", this.UseSelectedLanguagesWrapper.ClientID);
      controlDescriptor.AddComponentProperty("nameField", this.NameField.ClientID);
      controlDescriptor.AddComponentProperty("domainField", this.DomainField.ClientID);
      controlDescriptor.AddComponentProperty("stagingDomainField", this.StagingDomainField.ClientID);
      controlDescriptor.AddComponentProperty("isSiteInDevelopmentField", this.IsSiteInDevelopmentField.ClientID);
      controlDescriptor.AddElementProperty("detailsLink", this.DetailsLink.ClientID);
      controlDescriptor.AddElementProperty("detailsContent", this.DetailsContent.ClientID);
      controlDescriptor.AddElementProperty("pageSettingsWrapper", this.PageSettingsWrapper.ClientID);
      controlDescriptor.AddElementProperty("pageSettingsList", this.PageSettingsList.ClientID);
      controlDescriptor.AddElementProperty("sitesDropDown", this.SitesDropDown.ClientID);
      controlDescriptor.AddComponentProperty("genericBinder", this.GenericBinder.ClientID);
      controlDescriptor.AddComponentProperty("languagesField", this.LanguagesField.ClientID);
      controlDescriptor.AddComponentProperty("allLanguagesField", this.AllLanguagesField.ClientID);
      controlDescriptor.AddComponentProperty("isOfflineField", this.IsOfflineField.ClientID);
      controlDescriptor.AddComponentProperty("pageBehaviourField", this.PageBehaviourField.ClientID);
      controlDescriptor.AddComponentProperty("domainAliasesField", this.DomainAliasesField.ClientID);
      controlDescriptor.AddComponentProperty("defaultProtocolField", this.DefaultProtocolField.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("dialogTitle", this.DialogTitle.ClientID);
      controlDescriptor.AddElementProperty("propertiesViewWrapper", this.PropertiesViewWrapper.ClientID);
      controlDescriptor.AddElementProperty("saveButton", this.SaveButton.ClientID);
      controlDescriptor.AddElementProperty("continueButton", this.ContinueButton.ClientID);
      controlDescriptor.AddElementProperty("orLabel", this.OrLabel.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("backLink", this.BackLink.ClientID);
      controlDescriptor.AddElementProperty("expandLink", this.ExpandLink.ClientID);
      controlDescriptor.AddElementProperty("buttonsPanel", this.ButtonsPanel.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddElementProperty("isOfflineSection", this.IsOfflineSection.ClientID);
      controlDescriptor.AddElementProperty("selectPageRadio", this.SelectPageRadio.ClientID);
      controlDescriptor.AddElementProperty("useSelectedLanguagesRadio", this.UseSelectedLanguagesRadio.ClientID);
      controlDescriptor.AddElementProperty("useAllLanguagesRadio", this.UseAllLanguagesRadio.ClientID);
      controlDescriptor.AddComponentProperty("pageField", this.PageField.ClientID);
      controlDescriptor.AddElementProperty("enterUrlRadio", this.EnterUrlRadio.ClientID);
      controlDescriptor.AddComponentProperty("loginPageUrlField", this.LoginPageUrlField.ClientID);
      controlDescriptor.AddElementProperty("additionalDomainValidationError", this.AdditionalDomainValidationError.ClientID);
      controlDescriptor.AddElementProperty("additionalStagingDomainValidationError", this.AdditionalStagingDomainValidationError.ClientID);
      controlDescriptor.AddElementProperty("configureModulesBtn", this.ConfigureModulesBtn.ClientID);
      controlDescriptor.AddComponentProperty("enableLanguagesTooltip", this.EnableLanguagesTooltip.ClientID);
      controlDescriptor.AddElementProperty("cultureValidationError", this.CultureValidationError.ClientID);
      if (this.SiteConfigurationModeContainer.Visible)
      {
        controlDescriptor.AddElementProperty("manuallyConfiguredModeContainer", this.ManuallyConfiguredModeContainer.ClientID);
        controlDescriptor.AddElementProperty("configureByDeploymentBtn", this.ConfigureByDeploymentBtn.ClientID);
        controlDescriptor.AddElementProperty("configuredByDeploymentModeContainer", this.ConfiguredByDeploymentModeContainer.ClientID);
        controlDescriptor.AddElementProperty("configureManuallyBtn", this.ConfigureManuallyBtn.ClientID);
      }
      controlDescriptor.AddProperty("_isAllowedStartStop", (object) this._isAllowedStartStopSite);
      controlDescriptor.AddProperty("_isAllowedToConfigureModules", (object) this._isAllowedToConfigureModules);
      string str1 = this.Page.ResolveUrl("~/Sitefinity/Services/Multisite/Multisite.svc/");
      controlDescriptor.AddProperty("webServiceUrl", (object) str1);
      string str2 = this.Page.ResolveUrl("~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/localization/");
      controlDescriptor.AddProperty("_localizationServiceUrl", (object) str2);
      controlDescriptor.AddProperty("addLanguagesLabel", (object) this.AddLanguagesLabel);
      controlDescriptor.AddProperty("selectLanguagesLabel", (object) this.SelectLanguagesLabel);
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
      new ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.SitePropertiesView.js", typeof (SitePropertiesView).Assembly.FullName)
    };
  }
}
