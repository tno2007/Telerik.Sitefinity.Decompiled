// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomiesSidebar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>
  /// Sidebar control for the taxonomies module, when displaying list of taxonomies
  /// </summary>
  public class TaxonomiesSidebar : ViewModeControl<TaxonomiesPanel>, ICommandPanel
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.TaxonomiesSidebar.ascx");
    private bool? supportsMultiligual;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomiesSidebar" /> class.
    /// </summary>
    public TaxonomiesSidebar() => this.LayoutTemplatePath = TaxonomiesSidebar.layoutTemplatePath;

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets a value indicating whether this control supports multiligual.
    /// </summary>
    /// <value><c>true</c> if supports multiligual; otherwise, <c>false</c>.</value>
    public bool SupportsMultiligual
    {
      get
      {
        if (!this.supportsMultiligual.HasValue)
          this.supportsMultiligual = new bool?(AppSettings.CurrentSettings.Multilingual);
        return this.supportsMultiligual.Value;
      }
      set => this.supportsMultiligual = new bool?(value);
    }

    /// <summary>
    /// Gets the reference to the link control which is used to clear the filter and display
    /// all taxonomies.
    /// </summary>
    protected virtual LinkButton AllTaxonomiesFilter => this.Container.GetControl<LinkButton>("allTaxonomiesFilter", true);

    /// <summary>
    /// Gets the reference to the link control which is used to filter taxonomies and show
    /// only flat taxonomies.
    /// </summary>
    protected virtual LinkButton FlatTaxonomiesFilter => this.Container.GetControl<LinkButton>("flatTaxonomiesFilter", true);

    /// <summary>
    /// Gets the reference to the link control which is used to filter taxonomies and show
    /// only hierarchical taxonomies.
    /// </summary>
    protected virtual LinkButton HierarchicalTaxonomiesFilter => this.Container.GetControl<LinkButton>("hierarchicalTaxonomiesFilter", true);

    /// <summary>
    /// Gets the reference to the link control which is used to show the permissions management
    /// screen for the taxonomies module.
    /// </summary>
    protected virtual LinkButton PermissionsLink => this.Container.GetControl<LinkButton>("permissionsButton", true);

    /// <summary>Gets the permissions dialog URL.</summary>
    /// <value>The permissions dialog URL.</value>
    protected virtual HiddenField PermissionsDialogUrl => this.Container.GetControl<HiddenField>("hPermissionsDialogUrl", true);

    /// <summary>Gets the title to appear on the permissions list.</summary>
    protected virtual HiddenField ClassificationsTitle => this.Container.GetControl<HiddenField>("hClassificationsTitle", true);

    /// <summary>
    /// Gets the full assembly name of the TaxonomyManager class
    /// </summary>
    protected virtual HiddenField PermissionManagerClassName => this.Container.GetControl<HiddenField>("hPermissionManagerClassName", true);

    /// <summary>Gets the languages selector.</summary>
    /// <value>The languages selector.</value>
    protected virtual LanguagesDropDownListWidget LanguagesSelector => this.Container.GetControl<LanguagesDropDownListWidget>("languageSelector", false);

    /// <summary>Gets the language selector wrapper.</summary>
    /// <value>The language selector wrapper.</value>
    protected virtual HtmlGenericControl LanguageSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("languageSelectorWrapper", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      this.PermissionsDialogUrl.Value = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ModulePermissionsDialog", UrlResolveOptions.Rooted);
      this.ClassificationsTitle.Value = Res.Get<TaxonomyResources>().ModuleTitle;
      this.PermissionManagerClassName.Value = typeof (TaxonomyManager).AssemblyQualifiedName;
      this.AllTaxonomiesFilter.OnClientClick = string.Format("filterTaxonomies(this, '{0}'); return false;", (object) typeof (Taxonomy).Name);
      this.FlatTaxonomiesFilter.OnClientClick = string.Format("filterTaxonomies(this, '{0}'); return false;", (object) typeof (FlatTaxonomy).Name);
      this.HierarchicalTaxonomiesFilter.OnClientClick = string.Format("filterTaxonomies(this, '{0}'); return false;", (object) typeof (HierarchicalTaxonomy).Name);
      this.LanguagesSelector.CommandName = "changeLanguage";
      this.LanguageSelectorWrapper.Visible = this.SupportsMultiligual;
    }

    /// <summary>
    /// Reference to the control panel tied to the command panel instance.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// This property is used for communication between the command panel and its control
    /// panel.
    /// </remarks>
    /// <example>
    /// You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    /// complicated example implementing the whole
    /// <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    public IControlPanel ControlPanel { get; set; }
  }
}
