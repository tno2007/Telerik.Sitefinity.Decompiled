// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomySidebar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>Sidebar control for the single taxonomy</summary>
  public class TaxonomySidebar : ViewModeControl<TaxonomyBasePanel>, ICommandPanel
  {
    private Taxonomy taxonomy;
    private bool? supportsMultiligual;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.TaxonomySidebar.ascx");
    private const string taxonomyServiceUrl = "~/Sitefinity/Services/Taxonomies/Taxonomy.svc";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomySidebar" /> class.
    /// </summary>
    /// <param name="taxonomy">The taxonomy.</param>
    public TaxonomySidebar(Taxonomy taxonomy)
    {
      this.taxonomy = taxonomy;
      this.SetPanel();
    }

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the default layout template path.</summary>
    /// <value>The default layout template path.</value>
    protected override string DefaultLayoutTemplatePath => TaxonomySidebar.layoutTemplatePath;

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
    /// Gets the reference to the text control which displayes the title of the currently
    /// managed taxonomy.
    /// </summary>
    protected virtual ITextControl TaxonomyTitle => this.Container.GetControl<ITextControl>("taxonomyTitle", true);

    /// <summary>
    /// Gets the reference to the text control which displayes the name of the single item
    /// of the currently managed taxonomy.
    /// </summary>
    protected virtual ITextControl SingleItem => this.Container.GetControl<ITextControl>("singleItem", true);

    /// <summary>
    /// Gets the reference to the text control which displayes the type of the currently managed
    /// taxonomy.
    /// </summary>
    protected virtual ITextControl Type => this.Container.GetControl<ITextControl>("type", true);

    /// <summary>
    /// Gets the reference to the text control which displayes the description of the sidebar.
    /// </summary>
    protected virtual ITextControl SidebarDescription => this.Container.GetControl<ITextControl>("description", true);

    /// <summary>
    /// Gets the reference to the text control which displayes the name of the currently
    /// managed taxonomy.
    /// </summary>
    protected virtual ITextControl TaxonomyName => this.Container.GetControl<ITextControl>("taxonomyName", true);

    /// <summary>
    /// Gets the reference to the hidden field that stores the url of the web service
    /// used to manage the taxonomy and its taxons.
    /// </summary>
    protected virtual HiddenField TaxonomyServiceUrl => this.Container.GetControl<HiddenField>("taxonomyServiceUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field that stores the pageId of the currently managed taxonomy.
    /// </summary>
    protected virtual HiddenField TaxonomyId => this.Container.GetControl<HiddenField>("taxonomyId", true);

    /// <summary>
    /// Gets the reference to the hidden field that stores serialized version of the
    /// currently managed taxonomy.
    /// </summary>
    protected virtual HiddenField TaxonomyObject => this.Container.GetControl<HiddenField>("taxonomyObject", true);

    /// <summary>
    /// Gets the reference to the hidden field that stores taxonomy title.
    /// </summary>
    protected virtual HiddenField hTaxonomyTitle => this.Container.GetControl<HiddenField>(nameof (hTaxonomyTitle), true);

    /// <summary>
    /// Gets the reference to the hidden field which is used to store the url of the
    /// classifications module page.
    /// </summary>
    protected virtual HiddenField ClassificationsPageUrl => this.Container.GetControl<HiddenField>("classificationsPageUrl", true);

    /// <summary>
    /// Gets the reference to the link control which is used to delete a currently managed taxonomy.
    /// </summary>
    protected virtual LinkButton DeleteButton => this.Container.GetControl<LinkButton>("deleteTaxonomy", true);

    /// <summary>Gets the edit button.</summary>
    /// <value>The edit button.</value>
    protected virtual LinkButton EditButton => this.Container.GetControl<LinkButton>("editProperties", true);

    /// <summary>Gets the languages selector.</summary>
    /// <value>The languages selector.</value>
    protected virtual LanguagesDropDownListWidget LanguagesSelector => this.Container.GetControl<LanguagesDropDownListWidget>("languageSelector", false);

    /// <summary>Gets the language selector wrapper.</summary>
    /// <value>The language selector wrapper.</value>
    protected virtual HtmlGenericControl LanguageSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("languageSelectorWrapper", false);

    private void SetPanel()
    {
      this.TaxonomyTitle.Text = (string) this.taxonomy.Title;
      this.SingleItem.Text = (string) this.taxonomy.TaxonName;
      this.Type.Text = TaxonomyManager.GetTaxonomyUserFriendlyName((ITaxonomy) this.taxonomy);
      this.SidebarDescription.Text = (string) this.taxonomy.Description;
      this.TaxonomyName.Text = this.taxonomy.Name;
      if (TaxonomyManager.IsTaxonomyBuiltIn((ITaxonomy) this.taxonomy))
      {
        this.DeleteButton.CssClass += "sfDisabled";
        this.DeleteButton.OnClientClick = "javascript:void(0);return false;";
        this.EditButton.CssClass += "sfDisabled";
        this.EditButton.OnClientClick = "javascript:void(0);return false;";
      }
      this.LanguagesSelector.CommandName = "changeLanguage";
      this.LanguageSelectorWrapper.Visible = this.SupportsMultiligual;
      this.TaxonomyServiceUrl.Value = this.ResolveClientUrl("~/Sitefinity/Services/Taxonomies/Taxonomy.svc");
      this.TaxonomyId.Value = this.taxonomy.Id.ToString();
      this.hTaxonomyTitle.Value = (string) this.taxonomy.Title;
      PageNode pageNode = PageManager.GetManager().GetPageNode(SiteInitializer.TaxonomiesNodeId);
      if (pageNode == null)
        return;
      this.ClassificationsPageUrl.Value = RouteHelper.ResolveUrl(pageNode.GetUrl(), UrlResolveOptions.Rooted);
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
