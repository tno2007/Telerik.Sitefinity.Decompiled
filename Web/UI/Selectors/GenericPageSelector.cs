// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.GenericPageSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Control for selecting pages</summary>
  public class GenericPageSelector : SimpleScriptView
  {
    private const string SelectorScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.GenericPageSelector.js";
    /// <summary>The layout template path.</summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.GenericPageSelector.ascx");
    private Dictionary<string, string> allSitesAvailableCultures = new Dictionary<string, string>();
    private string constantFilter;
    private bool constantFilterSet;
    private bool showOnlySelectedAsGridOnLoad;
    private bool includeSiteSelector;
    private bool includeLanguageSelector;
    private bool hierarchicalTreeRootBindModeEnabled = true;
    private string unselectableClass = "sfDisabled";
    private bool bindOnLoad = true;
    private string parentDataKeyName;
    private string uiCulture;
    private bool allowSearch = true;
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    private static readonly Lazy<JavaScriptSerializer> serializer = new Lazy<JavaScriptSerializer>((Func<JavaScriptSerializer>) (() => new JavaScriptSerializer()), LazyThreadSafetyMode.ExecutionAndPublication);
    private readonly string treeViewItemTemplate = "<span sys:if=\"typeof(WasPublished) == 'undefined' || WasPublished\">\r\n                        {{(Title.hasOwnProperty('Value')) ? Title.Value.htmlEncode() : Title.htmlEncode()}} \r\n                    </span>\r\n                    <span class=\"pageInDraftLabel\" sys:if=\"typeof(WasPublished) != 'undefined' && !WasPublished\">\r\n                        {{(Title.hasOwnProperty('Value')) ? Title.Value.htmlEncode() : Title.htmlEncode()}} ({0})\r\n                    </span>";
    private readonly string treeViewDetailedItemTemplate = "\r\n                    <span sys:class=\"{{'sfTextEllipsis sfItemTitle sf' + Status.toLowerCase() + ((typeof(WasPublished) != 'undefined' && !WasPublished) ? ' pageInDraftLabel' : '') + (!PageEditUrl ? ' sfNotTranslated' : '')}}\" >\r\n                        {{(Title.hasOwnProperty('Value')) ? Title.Value.htmlEncode() : Title.htmlEncode()}} \r\n                        <span class=\"sfStatusLocation\">\r\n                            {{ StatusText }}\r\n                        </span>\r\n                    </span>";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? GenericPageSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the name of the provider from which the page node ought to be selected.
    /// </summary>
    public string Provider { get; set; }

    /// <summary>
    /// Gets or sets the ID of the page node from which the binding should start.
    /// </summary>
    /// <value>The root page node ID.</value>
    public Guid RootNodeID { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the base url for retrieving the child items used by the tree binder
    /// </summary>
    /// <remarks>See RadTreeBinder.ServiceChildItemsBaseUrl</remarks>
    public string ServiceChildItemsBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the service predecessor base URL for the tree binder.
    /// </summary>
    /// <value>The service predecessor base URL.</value>
    /// <remarks>See RadTreeBinder.ServicePredecessorBaseUrl</remarks>
    public string ServicePredecessorBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the service URL that is serving the tree binder.
    /// </summary>
    /// <value>The service tree URL.</value>
    /// <remarks>See RadTreeBinder.ServiceTreeUrl</remarks>
    public string ServiceTreeUrl { get; set; }

    /// <summary>
    /// Gets or sets the original service URL that is serving the tree binder.
    /// </summary>
    /// <value>The original service tree URL.</value>
    public string OrginalServiceBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the service URL that is serving the grid binder.
    /// </summary>
    /// <remarks>
    /// Use this property only if the service, binding the grid is different than the base service url.
    /// </remarks>
    public string ServiceGridUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow multiple selection.
    /// </summary>
    public virtual bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the search.
    /// </summary>
    public virtual bool AllowSearch
    {
      get => this.allowSearch;
      set => this.allowSearch = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show only selected item in a grid, when the loaded with a previously selected item, or show the whole tree.
    /// </summary>
    public bool ShowOnlySelectedAsGridOnLoad
    {
      get => this.showOnlySelectedAsGridOnLoad;
      set => this.showOnlySelectedAsGridOnLoad = value;
    }

    /// <summary>
    /// Gets a filter expression that is always applied to the item selector
    /// </summary>
    public string ConstantFilter
    {
      get => this.constantFilter;
      internal set
      {
        this.constantFilter = value;
        this.constantFilterSet = true;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically bind the selector when the control loads
    /// </summary>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to append the <see cref="P:Telerik.Sitefinity.Web.UI.GenericPageSelector.ConstantFilter" /> value.
    /// </summary>
    public bool AppendConstantFilter { get; set; }

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture
    {
      get
      {
        IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
        if (appSettings.Multilingual)
        {
          if (string.IsNullOrEmpty(this.uiCulture))
            this.uiCulture = appSettings.DefaultFrontendLanguage.Name;
        }
        else
          this.uiCulture = (string) null;
        return this.uiCulture;
      }
      set => this.uiCulture = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to enable or disable BindingMode = HierarchicalTreeRootBind. Default is true.
    /// </summary>
    public bool HierarchicalTreeRootBindModeEnabled
    {
      get => this.hierarchicalTreeRootBindModeEnabled;
      set => this.hierarchicalTreeRootBindModeEnabled = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to grey the items that have not a translation for the current language.
    /// The default is false. Works only in multilingual mode.
    /// </summary>
    public bool MarkItemsWithoutTranslation { get; set; }

    /// <summary>
    /// Gets or sets the css class of the items that cannot be selected.
    /// </summary>
    public string UnselectableClass
    {
      get => this.unselectableClass;
      set => this.unselectableClass = value;
    }

    /// <summary>
    /// Gets or sets the name of the node property that contains the parent id
    /// </summary>
    public string ParentDataKeyName
    {
      get => string.IsNullOrEmpty(this.parentDataKeyName) ? "ParentId" : this.parentDataKeyName;
      set => this.parentDataKeyName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include site selector. False by default
    /// </summary>
    public bool IncludeSiteSelector
    {
      get => this.includeSiteSelector;
      set => this.includeSiteSelector = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include language selector. False by default
    /// </summary>
    public bool IncludeLanguageSelector
    {
      get => this.includeLanguageSelector;
      set => this.includeLanguageSelector = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [show pages details].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [show pages details]; otherwise, <c>false</c>.
    /// </value>
    public bool ShowPagesDetails { get; set; }

    /// <summary>Gets the JavaScriptSerializer for the current class</summary>
    internal static JavaScriptSerializer Serializer => GenericPageSelector.serializer.Value;

    /// <summary>
    /// Gets or sets the name of the CSS class applied to the tree view.
    /// </summary>
    /// <value>The CSS class.</value>
    internal string TreeViewCssClass { get; set; }

    /// <summary>
    /// Gets or sets a collection with all sites and available languages per site.
    /// </summary>
    internal Dictionary<string, string> AllSitesAvailableCultures
    {
      get => this.allSitesAvailableCultures;
      set => this.SetAvailableLanguagesPerSite();
    }

    /// <summary>Gets or sets the target library id</summary>
    public Guid TargetLibraryId { get; set; }

    /// <summary>Gets the reference to the itemsGrid control.</summary>
    protected virtual ItemsGrid ItemsGrid => this.Container.GetControl<ItemsGrid>("itemsGrid", true);

    /// <summary>Gets the reference to the itemsTreeTable control.</summary>
    protected virtual ItemsTreeTable ItemsTreeTable => this.Container.GetControl<ItemsTreeTable>("itemsTreeTable", true);

    /// <summary>
    /// Gets the reference to the dropdown list label control.
    /// </summary>
    protected virtual Label DropDownListLabel => this.Container.GetControl<Label>("dropDownListLbl", false);

    /// <summary>Gets the reference to the searchBox control.</summary>
    protected internal BinderSearchBox SearchBox => this.Container.GetControl<BinderSearchBox>("searchBox", true);

    /// <summary>
    /// Gets the reference to the language selector dropdown control.
    /// </summary>
    protected internal LanguagesDropDownList LanguageSelector => this.Container.GetControl<LanguagesDropDownList>("languageSelector", false);

    /// <summary>Gets the site selector control.</summary>
    /// <value>The site selector control.</value>
    public virtual SiteSelector SiteSelector => this.Container.GetControl<SiteSelector>("siteSelector", false);

    /// <summary>Gets the language selector wrapper div control.</summary>
    /// <value>The site selector control.</value>
    protected internal Control LanguageSelectorWrp => this.Container.GetControl<Control>("languageSelectorWrp", true);

    /// <summary>
    /// Set a filter expression that is always applied to the item selector
    /// </summary>
    /// <param name="constantFilter">The filter expression.</param>
    public void SetConstantFilter(string constantFilter) => this.ConstantFilter = constantFilter;

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (!(this.RootNodeID == Guid.Empty))
        return;
      this.RootNodeID = SiteInitializer.CurrentFrontendRootNodeId;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.IncludeLanguageSelector &= AppSettings.CurrentSettings.Multilingual;
      if (!this.IncludeLanguageSelector && this.DropDownListLabel != null)
        this.DropDownListLabel.Visible = false;
      if (!this.IncludeSiteSelector && this.SiteSelector != null)
        this.SiteSelector.Visible = false;
      this.ItemsGrid.AllowMultipleSelection = this.AllowMultipleSelection;
      this.ItemsTreeTable.AllowMultipleSelection = this.AllowMultipleSelection;
      this.ItemsTreeTable.HierarchicalTreeRootBindModeEnabled = this.HierarchicalTreeRootBindModeEnabled;
      this.ItemsTreeTable.ParentDataKeyName = this.ParentDataKeyName;
      if (this.TreeViewCssClass != null)
        this.ItemsTreeTable.TreeViewCssClass = this.TreeViewCssClass;
      this.SetConstantFilterToItemsLists();
      this.ItemsGrid.UICulture = this.ItemsTreeTable.UICulture = this.UICulture;
      this.ItemsGrid.ServiceBaseUrl = !string.IsNullOrEmpty(this.ServiceGridUrl) ? this.ServiceGridUrl : this.WebServiceUrl;
      if (this.LanguageSelector != null)
      {
        this.LanguageSelector.Visible = this.IncludeLanguageSelector;
        this.LanguageSelectorWrp.Visible = this.IncludeLanguageSelector;
        if (this.IncludeLanguageSelector)
        {
          this.LanguageSelector.LanguageSource = LanguageSource.Frontend;
          this.LanguageSelector.AddAllLanguagesOption = false;
          this.LanguageSelector.ShowInMonolingual = this.IncludeLanguageSelector;
        }
      }
      if (this.AllowSearch)
      {
        this.SearchBox.Visible = true;
        this.ItemsGrid.PreRender += (EventHandler) ((s, a) => this.SearchBox.Binder = (Control) this.ItemsGrid.Binder);
      }
      else
        this.SearchBox.Visible = false;
      this.SetTreeViewItemTemplate(this.ItemsTreeTable.Items.First<ItemDescription>());
    }

    /// <summary>Sets the TreeView client side item template.</summary>
    /// <param name="itemDescription">The item description.</param>
    protected virtual void SetTreeViewItemTemplate(ItemDescription itemDescription)
    {
      if (this.ShowPagesDetails)
        itemDescription.Markup = this.treeViewDetailedItemTemplate;
      else
        itemDescription.Markup = this.treeViewItemTemplate.Replace("{0}", Res.Get<PageResources>().Draft);
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (GenericPageSelector).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("itemsGrid", this.ItemsGrid.ClientID);
      controlDescriptor.AddComponentProperty("itemsTree", this.ItemsTreeTable.ClientID);
      if (this.AllowSearch)
        controlDescriptor.AddComponentProperty("searchBox", this.SearchBox.ClientID);
      controlDescriptor.AddProperty("_showOnlySelectedAsGridOnLoad", (object) this.ShowOnlySelectedAsGridOnLoad.ToString().ToLower());
      controlDescriptor.AddProperty("_rootNodeId", (object) this.RootNodeID);
      string absolute = VirtualPathUtility.ToAbsolute(this.WebServiceUrl);
      if (!absolute.EndsWith("/"))
        absolute += "/";
      controlDescriptor.AddProperty("_baseServiceUrl", (object) absolute);
      controlDescriptor.AddProperty("_provider", (object) this.Provider);
      if (!string.IsNullOrEmpty(this.ServiceChildItemsBaseUrl))
        controlDescriptor.AddProperty("serviceChildItemsBaseUrl", (object) VirtualPathUtility.ToAbsolute(this.ServiceChildItemsBaseUrl));
      if (!string.IsNullOrEmpty(this.ServicePredecessorBaseUrl))
        controlDescriptor.AddProperty("servicePredecessorBaseUrl", (object) VirtualPathUtility.ToAbsolute(this.ServicePredecessorBaseUrl));
      if (!string.IsNullOrEmpty(this.ServiceTreeUrl))
        controlDescriptor.AddProperty("serviceTreeUrl", (object) VirtualPathUtility.ToAbsolute(this.ServiceTreeUrl));
      if (!string.IsNullOrEmpty(this.OrginalServiceBaseUrl))
        controlDescriptor.AddProperty("orginalServiceBaseUrl", (object) VirtualPathUtility.ToAbsolute(this.OrginalServiceBaseUrl));
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        controlDescriptor.AddProperty("markItemsWithoutTranslation", (object) this.MarkItemsWithoutTranslation);
      controlDescriptor.AddProperty("unselectableClass", (object) this.UnselectableClass);
      controlDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      controlDescriptor.AddProperty("targetLibraryId", (object) this.TargetLibraryId);
      if (this.IncludeSiteSelector && this.SiteSelector != null)
        controlDescriptor.AddComponentProperty("siteSelector", this.SiteSelector.ClientID);
      if (this.IncludeLanguageSelector && this.LanguageSelector != null)
      {
        controlDescriptor.AddComponentProperty("languageSelector", this.LanguageSelector.ClientID);
        this.SetAvailableLanguagesPerSite();
        if (this.allSitesAvailableCultures.Count != 0)
        {
          string str = GenericPageSelector.Serializer.Serialize((object) this.allSitesAvailableCultures);
          controlDescriptor.AddProperty("_allSitesAvailableCultures", (object) str);
        }
      }
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
      new ScriptReference("Telerik.Sitefinity.Web.UI.Selectors.Scripts.GenericPageSelector.js", typeof (GenericPageSelector).Assembly.FullName)
    };

    private void SetAvailableLanguagesPerSite()
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext == null)
        return;
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      IEnumerable<Guid> allowedSites = multisiteContext.GetAllowedSites(currentIdentity.UserId, currentIdentity.MembershipProvider);
      foreach (ISite site in multisiteContext.GetSites().Where<ISite>((Func<ISite, bool>) (s => allowedSites.Contains<Guid>(s.Id))))
      {
        string str = GenericPageSelector.Serializer.Serialize((object) ((IEnumerable<CultureInfo>) site.PublicContentCultures).ToDictionary<CultureInfo, string, string>((Func<CultureInfo, string>) (c => c.Name), (Func<CultureInfo, string>) (c => c.DisplayName)));
        this.allSitesAvailableCultures.Add(site.Id.ToString(), str);
      }
    }

    private void SetConstantFilterToItemsLists()
    {
      if (!this.constantFilterSet)
        return;
      if (!this.AppendConstantFilter)
      {
        this.ItemsGrid.ConstantFilter = this.ConstantFilter;
        this.ItemsTreeTable.ConstantFilter = this.ConstantFilter;
      }
      else
      {
        this.SetConstantFilter((ItemsListBase) this.ItemsGrid);
        this.SetConstantFilter((ItemsListBase) this.ItemsTreeTable);
      }
    }

    private void SetConstantFilter(ItemsListBase itemsList)
    {
      if (!string.IsNullOrEmpty(itemsList.ConstantFilter))
        itemsList.ConstantFilter = string.Format("({0}) AND ({1})", (object) itemsList.ConstantFilter, (object) this.ConstantFilter);
      else
        itemsList.ConstantFilter = this.ConstantFilter;
    }
  }
}
