// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView
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
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>View for comments basic settings.</summary>
  public class LocalizationBasicSettingsView : BasicSettingsView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.LocalizationBasicSettingsView.ascx");
    private const string viewScript = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.LocalizationBasicSettingsView.js";
    private const string domainRegex = "^(http[s]?\\:\\/\\/)?((([a-zA-Z0-9]([a-zA-Z0-9\\-]{0,61}[a-zA-Z0-9])?\\.)+[a-zA-Z]{2,6})|([a-zA-Z0-9]([a-zA-Z0-9\\-]{0,61}[a-zA-Z0-9])?))(\\:[0-9]{2,5})?[\\/]?$";
    private const string javascriptVoid = "javascript:void(0)";

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LocalizationBasicSettingsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a reference to the languages field that lists the frontend cultures.
    /// </summary>
    protected virtual LanguagesOrderedListField LanguagesField => this.Container.GetControl<LanguagesOrderedListField>("languagesField", true);

    /// <summary>
    /// Gets a reference to the languages field that lists the backend cultures.
    /// </summary>
    protected virtual LanguagesOrderedListField BackendLanguages => this.Container.GetControl<LanguagesOrderedListField>("backendLanguagesField", true);

    /// <summary>
    /// Gets a reference to the choice field control that lists the backend cultures.
    /// </summary>
    protected virtual ChoiceField BackendLanguagesSelect => this.Container.GetControl<ChoiceField>("backendLanguagesSelect", true);

    /// <summary>
    /// Gets a reference to the container control of the <see cref="P:Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.BackendLanguagesSelect" /> control.
    /// </summary>
    protected virtual Control LanguagesSelectContainer => this.Container.GetControl<Control>("languagesSelectContainer", true);

    /// <summary>
    /// Gets a reference to the container control of the <see cref="P:Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.BackendLanguages" /> control.
    /// </summary>
    /// <value>The languages list container.</value>
    protected virtual Control LanguagesListContainer => this.Container.GetControl<Control>("languagesListContainer", true);

    /// <summary>
    /// Gets a reference to the link element that shows the <see cref="P:Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.BackendLanguages" /> control.
    /// </summary>
    protected virtual Control ManageLanguagesList => this.Container.GetControl<Control>("manageLanguagesList", true);

    /// <summary>
    /// Gets a reference to the link element that hides the <see cref="P:Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.BackendLanguages" /> control.
    /// </summary>
    protected virtual Control CloseLanguagesList => this.Container.GetControl<Control>("closeLanguagesList", true);

    /// <summary>
    /// Gets a reference to the control that is used by the dataview component.
    /// </summary>
    protected virtual HtmlGenericControl SubdomainsList => this.Container.GetControl<HtmlGenericControl>("subdomainsList", true);

    /// <summary>
    /// Gets a reference to the container control of the <see cref="P:Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.SubdomainsList" /> control.
    /// </summary>
    protected virtual Control SubdomainsContainer => this.Container.GetControl<Control>("subdomainsContainer", true);

    /// <summary>
    /// Gets a reference to the radio button that represents the subfolder URL localization strategy.
    /// </summary>
    protected virtual HtmlInputRadioButton DirectoriesButton => this.Container.GetControl<HtmlInputRadioButton>("directoriesButton", true);

    /// <summary>
    /// Gets a reference to the radio button that represents the subdomains URL localization strategy.
    /// </summary>
    protected virtual HtmlInputRadioButton DomainsButton => this.Container.GetControl<HtmlInputRadioButton>("domainsButton", true);

    /// <summary>
    /// Gets a reference to the container of the radio button controls.
    /// </summary>
    protected virtual Control RadioButtonList => this.Container.GetControl<Control>("radioButtonList", true);

    /// <summary>
    /// Gets a reference to the container of the languages public content holder.
    /// </summary>
    protected virtual HtmlGenericControl LanguagesPublicContentHolder => this.Container.GetControl<HtmlGenericControl>("languagesPublicContentHolder", true);

    protected virtual Label GlobalLanguagesManagementMessage => this.Container.GetControl<Label>("globalLanguagesManagementMessage", true);

    protected virtual Label DomainLocalizationStrategyMessage => this.Container.GetControl<Label>("domainLocalizationStrategyMessage", true);

    /// <summary>
    /// Gets a reference to the container of the manage backend languages description holder.
    /// </summary>
    protected virtual HtmlGenericControl ManageBackendLanguagesDescHolder => this.Container.GetControl<HtmlGenericControl>("manageBackendLanguagesDescHolder", true);

    /// <summary>
    /// Gets the literal control displaying the languages for public content section title.
    /// </summary>
    protected virtual Literal LanguagesForPublicSiteText => this.Container.GetControl<Literal>("lLanguagesForPublicSite", true);

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      this.LanguagesForPublicSiteText.Text = Res.Get<Labels>("EnabledLanguagesForPublicContent");
      string manageSitesUrl = this.GetManageSitesUrl();
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      MultisiteManager manager = MultisiteManager.GetManager();
      manager.GetSite(id);
      if (manager.GetSites().Any<Site>((Expression<Func<Site, bool>>) (x => x.CultureKeys.Any<string>())))
        this.GlobalLanguagesManagementMessage.Text = string.Format(!LicenseState.CheckIsModuleLicensedInAnyDomain("FBD4773B-8688-4C75-8563-28BFDA27A185") ? Res.Get<Labels>().GlobalLanguagesManagementMessageSingleSite : Res.Get<Labels>().GlobalLanguagesManagementMessage, (object) manageSitesUrl);
      else
        this.GlobalLanguagesManagementMessage.Visible = false;
      this.DomainLocalizationStrategyMessage.Text = string.Format(Res.Get<Labels>().DomainLocalizationStrategyMessage, (object) manageSitesUrl);
      this.DirectoriesButton.Attributes["value"] = typeof (SubFolderUrlLocalizationStrategy).Name;
      this.DomainsButton.Attributes["value"] = typeof (DomainUrlLocalizationStrategy).Name;
      if (!this.IsLanguagesPublicContentLicensed())
      {
        this.LanguagesPublicContentHolder.Parent.Controls.Remove((Control) this.LanguagesPublicContentHolder);
        this.ManageBackendLanguagesDescHolder.Visible = true;
      }
      else
        this.ManageBackendLanguagesDescHolder.Visible = false;
    }

    private string GetManageSitesUrl()
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(!LicenseState.CheckIsModuleLicensed("FBD4773B-8688-4C75-8563-28BFDA27A185") ? MultisiteModule.SiteSettingsPageId : MultisiteModule.HomePageId, false);
      return siteMapNode != null ? RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) : "javascript:void(0)";
    }

    private bool IsLanguagesPublicContentLicensed() => LicenseState.Current.IsInTrialMode || !(LicenseState.Current.LicenseInfo.LicenseType == "SB");

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      if (this.IsLanguagesPublicContentLicensed())
        controlDescriptor.AddComponentProperty("frontendLanguagesField", this.LanguagesField.ClientID);
      controlDescriptor.AddComponentProperty("backendLanguagesField", this.BackendLanguages.ClientID);
      controlDescriptor.AddComponentProperty("backendLanguagesSelect", this.BackendLanguagesSelect.ClientID);
      controlDescriptor.AddElementProperty("languagesSelectContainer", this.LanguagesSelectContainer.ClientID);
      controlDescriptor.AddElementProperty("languagesListContainer", this.LanguagesListContainer.ClientID);
      controlDescriptor.AddElementProperty("manageLanguagesList", this.ManageLanguagesList.ClientID);
      controlDescriptor.AddElementProperty("closeLanguagesList", this.CloseLanguagesList.ClientID);
      controlDescriptor.AddElementProperty("subdomainsContainer", this.SubdomainsContainer.ClientID);
      controlDescriptor.AddElementProperty("directoriesButton", this.DirectoriesButton.ClientID);
      controlDescriptor.AddElementProperty("domainsButton", this.DomainsButton.ClientID);
      controlDescriptor.AddElementProperty("radioButtonList", this.RadioButtonList.ClientID);
      controlDescriptor.AddElementProperty("subdomainsListElement", this.SubdomainsList.ClientID);
      controlDescriptor.AddProperty("domainStrategyValue", (object) typeof (DomainUrlLocalizationStrategy).Name);
      controlDescriptor.AddProperty("domainRegex", (object) "^(http[s]?\\:\\/\\/)?((([a-zA-Z0-9]([a-zA-Z0-9\\-]{0,61}[a-zA-Z0-9])?\\.)+[a-zA-Z]{2,6})|([a-zA-Z0-9]([a-zA-Z0-9\\-]{0,61}[a-zA-Z0-9])?))(\\:[0-9]{2,5})?[\\/]?$");
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.LocalizationBasicSettingsView.js", typeof (LocalizationBasicSettingsView).Assembly.FullName)
    };

    /// <summary>
    /// Returns an initialized <see cref="T:System.Web.UI.ScriptControlDescriptor" /> that will be used by <see cref="M:Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.GetScriptDescriptors" />.
    /// Provides a way for inheriting types to initialize their own <see cref="T:System.Web.UI.ScriptControlDescriptor" /> object
    /// and use it in a script component.
    /// </summary>
    protected override ScriptControlDescriptor GetScriptDescriptor() => new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
  }
}
