// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.FlatProviderSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  public class FlatProviderSelector : SimpleScriptView
  {
    private bool autoHideIfLessThanTwoProviders = true;
    private int mainMenuProvidersCount = 5;
    private int moreProvidersCount = 15;
    private bool showAllProvidersLink = true;
    private IList<DataProviderBase> providers;
    private IManager manager;
    private List<FlatProviderSelector.ProviderInfo> listedProviders = new List<FlatProviderSelector.ProviderInfo>();
    private List<string> hiddenProviderNames;
    private string allProvidersTitle = Res.Get<Labels>().AllProvidersText;
    private string selectedProvider;
    private string showProviderGroups;
    private string dataSourceName;
    private string webServiceUrlFormat = "~/" + "Sitefinity/Services/DataSourceService" + "/providers/?siteId={0}&dataSourceName={1}&sortExpression=Title";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.FlatProviderSelector.ascx");
    private const string ClickMenuScript = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js";

    /// <summary>
    /// Gets or sets the manager associated with the providers listed by this selector.
    /// </summary>
    /// <value>Instance of the manager.</value>
    public IManager Manager
    {
      get => this.manager;
      set
      {
        this.manager = value;
        if (this.manager == null)
          return;
        this.listedProviders.Clear();
        List<string> stringList = (List<string>) null;
        if (!string.IsNullOrWhiteSpace(this.ShowProviderGroups))
          stringList = new List<string>((IEnumerable<string>) this.showProviderGroups.Split(new char[2]
          {
            ' ',
            ','
          }, StringSplitOptions.RemoveEmptyEntries));
        foreach (DataProviderBase provider in (IEnumerable<DataProviderBase>) this.Providers)
        {
          if ((this.HiddenProviderNames == null || !this.HiddenProviderNames.Contains(provider.Name)) && (string.IsNullOrWhiteSpace(this.ShowProviderGroups) && string.IsNullOrWhiteSpace(provider.ProviderGroup) || !string.IsNullOrWhiteSpace(this.ShowProviderGroups) && !string.IsNullOrWhiteSpace(provider.ProviderGroup) && stringList != null && stringList.Contains(provider.ProviderGroup)))
            this.listedProviders.Add(new FlatProviderSelector.ProviderInfo(string.Empty, provider.Name, provider.Title, provider.Id));
        }
        if (!this.ShowAllProvidersLink)
          return;
        this.listedProviders.Add(new FlatProviderSelector.ProviderInfo(string.Empty, string.Empty, this.AllProvidersTitle, Guid.Empty));
      }
    }

    /// <summary>
    /// Gets all providers for the current site and specified manager.
    /// </summary>
    public IList<DataProviderBase> Providers
    {
      get
      {
        if (this.providers == null)
          this.providers = !(this.Manager is DynamicModuleManager) ? (!string.IsNullOrWhiteSpace(this.ShowProviderGroups) ? (IList<DataProviderBase>) this.Manager.GetAllProviders().OrderBy<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Title)).ToList<DataProviderBase>() : (IList<DataProviderBase>) new List<DataProviderBase>((IEnumerable<DataProviderBase>) this.Manager.GetContextProviders().OrderBy<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Title)))) : (IList<DataProviderBase>) ((DynamicModuleManager) this.Manager).GetContextProviders(this.DynamicModuleName).ToList<DataProviderBase>();
        return this.providers;
      }
      set => this.providers = value;
    }

    public string ShowProviderGroups
    {
      get => this.showProviderGroups;
      set => this.showProviderGroups = value.Trim();
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the "all providers" link.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the "all providers" link is visible. otherwise, <c>false</c>.
    /// </value>
    public bool ShowAllProvidersLink
    {
      get => this.showAllProvidersLink;
      set => this.showAllProvidersLink = value;
    }

    /// <summary>Gets or sets the title for the "all providers" item.</summary>
    /// <value>All providers title.</value>
    public string AllProvidersTitle
    {
      get => this.allProvidersTitle;
      set => this.allProvidersTitle = value;
    }

    /// <summary>Gets the default provider of the manager</summary>
    /// <value>Default provider title.</value>
    public string DefaultProvider
    {
      get
      {
        string defaultProvider = string.Empty;
        if (this.Manager != null)
          defaultProvider = !(this.Manager is DynamicModuleManager) ? this.Manager.GetDefaultContextProvider().Name : DynamicModuleManager.GetDefaultProviderName(this.DynamicModuleName);
        else if (this.Manager != null)
          defaultProvider = this.Manager.Provider.Name;
        if (this.listedProviders.Count > 0 && !this.listedProviders.Any<FlatProviderSelector.ProviderInfo>((Func<FlatProviderSelector.ProviderInfo, bool>) (p => p.ProviderName == defaultProvider)))
        {
          if (this.listedProviders.FirstOrDefault<FlatProviderSelector.ProviderInfo>((Func<FlatProviderSelector.ProviderInfo, bool>) (p => p.ProviderName == this.SelectedProvider)) == null)
            this.SelectedProvider = this.listedProviders.First<FlatProviderSelector.ProviderInfo>().ProviderName;
          defaultProvider = this.listedProviders.First<FlatProviderSelector.ProviderInfo>().ProviderName;
        }
        return defaultProvider;
      }
    }

    /// <summary>Gets or sets the selected provider in the UI</summary>
    /// <value>Selected provider.</value>
    public string SelectedProvider
    {
      get => this.selectedProvider;
      set => this.selectedProvider = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the selector should be hidden automatically, if it contains less than two providers.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the selector should be hidden automatically, if it contains less than two providers; otherwise, <c>false</c>.
    /// </value>
    public bool AutoHideIfLessThanTwoProviders
    {
      get => this.autoHideIfLessThanTwoProviders;
      set => this.autoHideIfLessThanTwoProviders = value;
    }

    /// <summary>
    /// Gets or sets the on provider selected client event handler.
    /// </summary>
    /// <value>The on provider selected client event handler.</value>
    public string OnProviderSelected { get; set; }

    /// <summary>
    /// Gets or sets an array of providers which should not be displayed.
    /// </summary>
    /// <value>The hidden provider names.</value>
    public List<string> HiddenProviderNames
    {
      get
      {
        if (this.hiddenProviderNames == null)
          this.hiddenProviderNames = new List<string>();
        return this.hiddenProviderNames;
      }
      set => this.hiddenProviderNames = value;
    }

    /// <summary>
    /// Gets or sets the number of providers located in the main menu.
    /// If there is no providers located in the main menu the first 5 will be displayed by default.
    /// </summary>
    /// <value>The number of providers located in the main menu.</value>
    public int MainMenuProvidersCount
    {
      get => this.mainMenuProvidersCount;
      set => this.mainMenuProvidersCount = value;
    }

    /// <summary>
    /// Gets or sets the number more of providers displayed under more providers section.
    /// 15 by default
    /// </summary>
    /// <value>The more providers count.</value>
    public int MoreProvidersCount
    {
      get => this.moreProvidersCount;
      set => this.moreProvidersCount = value;
    }

    /// <summary>Gets or sets the web service URL format.</summary>
    /// <value>The web service URL format.</value>
    public string WebServiceUrlFormat
    {
      get => this.webServiceUrlFormat;
      set => this.webServiceUrlFormat = value;
    }

    /// <summary>Gets or sets the name of the data source.</summary>
    /// <value>The name of the data source.</value>
    public string DataSourceName
    {
      get
      {
        if (this.dataSourceName.IsNullOrEmpty())
          this.dataSourceName = !(this.Manager is DynamicModuleManager) ? this.Manager.GetType().FullName : this.DynamicModuleName;
        return this.dataSourceName;
      }
      set => this.dataSourceName = value;
    }

    /// <summary>Gets or sets the name of the dynamic module.</summary>
    /// <value>The name of the dynamic module.</value>
    public string DynamicModuleName { get; set; }

    /// <summary>Gets the providers list panel.</summary>
    /// <value>The providers list panel.</value>
    protected virtual Panel ProvidersListPanel => this.Container.GetControl<Panel>("pnlProvidersList", true);

    /// <summary>
    /// Gets the html control that displays more providers menu.
    /// </summary>
    protected virtual HtmlGenericControl MoreProvidersMenu => this.Container.GetControl<HtmlGenericControl>("moreProvidersMenu", false);

    /// <summary>Gets the repeater that lists the more providers.</summary>
    protected virtual Repeater MoreProvidersRepeater => this.Container.GetControl<Repeater>("moreProvidersRepeater", true);

    /// <summary>Gets the HyperLink for all providers.</summary>
    protected virtual HyperLink AllProvidersLink => this.Container.GetControl<HyperLink>("allProvidersLink", true);

    /// <summary>Gets the HyperLink for more providers.</summary>
    protected virtual HyperLink MoreLink => this.Container.GetControl<HyperLink>("moreLink", true);

    /// <summary>
    /// Gets the html control that displays all providers selector.
    /// </summary>
    protected virtual HtmlGenericControl AllProvidersMenu => this.Container.GetControl<HtmlGenericControl>("allProvidersMenu", true);

    /// <summary>
    /// Gets the html control that displays all providers selector dialog.
    /// </summary>
    protected virtual HtmlGenericControl AllProviders => this.Container.GetControl<HtmlGenericControl>("allProviders", false);

    /// <summary>Gets all providers done selecting button.</summary>
    protected virtual LinkButton AllProvidersDoneSelectingButton => this.Container.GetControl<LinkButton>("allProvidersDoneSelectingButton", true);

    /// <summary>Gets all providers cancel selecting button.</summary>
    protected virtual LinkButton AllProvidersCancelSelectingButton => this.Container.GetControl<LinkButton>("allProvidersCancelSelectingButton", true);

    /// <summary>Gets the selector control for all providers.</summary>
    protected FlatSelector AllProvidersSelector => this.Container.GetControl<FlatSelector>("allProvidersSelector", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FlatProviderSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      this.AddAttributesToRender(writer);
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      if (this.SelectedProvider == null)
        this.SelectedProvider = this.DefaultProvider;
      if (this.listedProviders.Count<FlatProviderSelector.ProviderInfo>() > this.MainMenuProvidersCount && this.MoreProvidersMenu != null)
      {
        IEnumerable<FlatProviderSelector.ProviderInfo> source = this.listedProviders.Skip<FlatProviderSelector.ProviderInfo>(this.MainMenuProvidersCount);
        if (source.Count<FlatProviderSelector.ProviderInfo>() > this.MoreProvidersCount)
        {
          this.MoreProvidersRepeater.DataSource = (object) source.Take<FlatProviderSelector.ProviderInfo>(this.MoreProvidersCount);
          this.AllProvidersLink.Text = string.Format(Res.Get<Labels>().AllSources, (object) this.Providers.Count<DataProviderBase>());
          this.AllProvidersSelector.ServiceUrl = string.Format(this.WebServiceUrlFormat, (object) (SystemManager.MultisiteContext == null || SystemManager.CurrentContext.CurrentSite == null ? Guid.Empty : SystemManager.CurrentContext.CurrentSite.Id), (object) this.DataSourceName);
        }
        else
        {
          this.MoreProvidersRepeater.DataSource = (object) source;
          this.AllProvidersMenu.Visible = false;
          this.AllProviders.Visible = false;
        }
        this.MoreLink.Text = string.Format(Res.Get<Labels>().ShowMore, (object) source.Count<FlatProviderSelector.ProviderInfo>());
        this.MoreProvidersRepeater.DataBind();
      }
      else if (this.MoreProvidersMenu != null)
      {
        this.MoreProvidersMenu.Visible = false;
        this.AllProviders.Visible = false;
      }
      base.OnPreRender(e);
    }

    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.MicrosoftAjaxTemplates;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("_providersListPanelID", (object) this.ProvidersListPanel.ClientID);
      behaviorDescriptor.AddProperty("_showAllProvidersLink", (object) this.showAllProvidersLink.ToString().ToLower());
      behaviorDescriptor.AddProperty("_listedProviders", (object) new JavaScriptSerializer().Serialize((object) this.listedProviders.ToArray()));
      behaviorDescriptor.AddProperty("autoHideIfLessThanTwoProviders", (object) this.autoHideIfLessThanTwoProviders.ToString().ToLower());
      behaviorDescriptor.AddProperty("hiddenProviderNames", (object) new JavaScriptSerializer().Serialize((object) this.HiddenProviderNames.ToArray()));
      behaviorDescriptor.AddProperty("_defaultProvider", (object) this.DefaultProvider);
      behaviorDescriptor.AddProperty("_selectedProvider", (object) this.SelectedProvider);
      behaviorDescriptor.AddProperty("mainMenuProvidersCount", (object) this.MainMenuProvidersCount);
      if (!string.IsNullOrEmpty(this.OnProviderSelected))
        behaviorDescriptor.AddEvent("onProviderSelected", this.OnProviderSelected);
      if (this.MoreProvidersMenu != null && this.MoreProvidersMenu.Visible)
      {
        behaviorDescriptor.AddElementProperty("moreProvidersMenu", this.MoreProvidersMenu.ClientID);
        if (this.AllProviders != null && this.AllProvidersMenu.Visible)
        {
          behaviorDescriptor.AddElementProperty("allProvidersLink", this.AllProvidersLink.ClientID);
          behaviorDescriptor.AddElementProperty("allProviders", this.AllProviders.ClientID);
          behaviorDescriptor.AddComponentProperty("allProvidersSelector", this.AllProvidersSelector.ClientID);
          behaviorDescriptor.AddElementProperty("allProvidersDoneSelectingButton", this.AllProvidersDoneSelectingButton.ClientID);
          behaviorDescriptor.AddElementProperty("allProvidersCancelSelectingButton", this.AllProvidersCancelSelectingButton.ClientID);
        }
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>((IEnumerable<ScriptReference>) PageManager.GetScriptReferences(ScriptRef.JQueryUI));
      string str = typeof (FlatProviderSelector).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name));
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Scripts.FlatProviderSelector.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// The ProviderInfo class is used by the client to store information about the listed providers and their association with the listed controls.
    /// </summary>
    private class ProviderInfo
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.FlatProviderSelector.ProviderInfo" /> class.
      /// </summary>
      /// <param name="cliendID">The cliend ID associated with the listed provider.</param>
      /// <param name="providerName">Name of the provider.</param>
      /// <param name="providerTitle">The provider title.</param>
      /// <param name="providerID">The provider ID.</param>
      public ProviderInfo(
        string cliendID,
        string providerName,
        string providerTitle,
        Guid providerID)
      {
        this.CliendID = cliendID;
        this.ProviderName = providerName;
        this.ProviderTitle = providerTitle;
        this.ProviderID = providerID.ToString();
      }

      /// <summary>
      /// Gets or sets the cliend ID associated with the listed provider.
      /// </summary>
      /// <value>The cliend ID.</value>
      public string CliendID { get; set; }

      /// <summary>Gets or sets the name of the provider.</summary>
      /// <value>The name of the provider.</value>
      public string ProviderName { get; set; }

      /// <summary>Gets or sets the provider title.</summary>
      /// <value>The provider title.</value>
      public string ProviderTitle { get; set; }

      /// <summary>Gets or sets the provider ID.</summary>
      /// <value>The provider ID.</value>
      public string ProviderID { get; set; }
    }
  }
}
