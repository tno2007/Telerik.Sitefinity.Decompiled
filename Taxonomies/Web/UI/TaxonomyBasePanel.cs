// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomyBasePanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>
  /// Represents the base class for control panels that manage single taxonomy.
  /// </summary>
  public class TaxonomyBasePanel : ProviderControlPanel<Page>
  {
    private TaxonomyManager taxonomyManager;
    private Taxonomy taxonomy;
    private string templatePath;
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.TaxonomyControlPanel.ascx");

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomyBasePanel" />.
    /// </summary>
    public TaxonomyBasePanel()
      : base(false)
    {
      this.ResourceClassId = typeof (TaxonomyResources).Name;
    }

    /// <summary>
    /// Gets the name of the embedded layout template. This property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(this.templatePath) ? TaxonomyBasePanel.layoutTemplatePath : this.templatePath;
      set => this.templatePath = value;
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomyBasePanel.TaxonomyManager" /> to be used by the
    /// control
    /// </summary>
    protected virtual TaxonomyManager TaxonomyManager
    {
      get
      {
        if (this.taxonomyManager == null)
          this.taxonomyManager = TaxonomyManager.GetManager(this.ProviderName);
        return this.taxonomyManager;
      }
    }

    /// <summary>Gets or sets the title of the panel.</summary>
    public override string Title
    {
      get
      {
        if (this.Taxonomy == null)
          return string.Empty;
        return !string.IsNullOrEmpty((string) this.Taxonomy.Title) ? HttpUtility.HtmlEncode(this.Taxonomy.Title.ToString()) : this.Taxonomy.Name;
      }
      set => base.Title = value;
    }

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomyBasePanel.Taxonomy" /> currently being managed
    /// by the concrete implementation of taxonomy panel.
    /// </summary>
    protected internal virtual Taxonomy Taxonomy
    {
      get
      {
        if (this.taxonomy == null)
          this.taxonomy = this.GetTaxonomy();
        return this.taxonomy;
      }
    }

    protected virtual HyperLink AllTaxonomiesLink => this.Container.GetControl<HyperLink>("allTaxonomiesLink", true);

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      this.AllTaxonomiesLink.NavigateUrl = BackendSiteMap.FindSiteMapNode(SiteInitializer.TaxonomiesNodeId, false).Url;
    }

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews()
    {
    }

    /// <summary>
    /// When overridden this method returns a list of custom Command Panels.
    /// </summary>
    /// <param name="viewMode">Specifies the current View Mode</param>
    /// <param name="list">A list of custom command panels.</param>
    protected override void CreateCustomCommandPanels(string viewMode, IList<ICommandPanel> list)
    {
      base.CreateCustomCommandPanels(viewMode, list);
      list.Add((ICommandPanel) new TaxonomySidebar(this.Taxonomy));
    }

    private Taxonomy GetTaxonomy()
    {
      int num = ((IEnumerable<string>) SystemManager.CurrentHttpContext.Request.Url.Segments).Count<string>();
      for (int index = num; index > 0; --index)
      {
        string taxonomyName = SystemManager.CurrentHttpContext.Request.Url.Segments[index - 1].ToString();
        if (taxonomyName.EndsWith("/"))
          taxonomyName = taxonomyName.Substring(0, taxonomyName.Length - 1);
        taxonomyName = HttpUtility.UrlDecode(taxonomyName);
        Taxonomy taxonomy = this.TaxonomyManager.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Name == taxonomyName)).SingleOrDefault<Taxonomy>();
        if (taxonomy != null)
          return taxonomy;
      }
      string str = VirtualPathUtility.RemoveTrailingSlash(SystemManager.CurrentHttpContext.Request.Url.Segments[num - 1]);
      if (str.Equals(SiteInitializer.FlatTaxonomyUrlName, StringComparison.CurrentCultureIgnoreCase))
        return this.GetTaxa<FlatTaxonomy>("Tags");
      if (str.Equals(SiteInitializer.HierarchicalTaxonomyUrlName, StringComparison.CurrentCultureIgnoreCase))
        return this.GetTaxa<HierarchicalTaxonomy>("Categories");
      if (this.Page != null)
        this.Page.Response.Redirect(BackendSiteMap.FindSiteMapNode(SiteInitializer.TaxonomiesNodeId, false).Url);
      return this.TaxonomyManager.GetTaxonomies<Taxonomy>().FirstOrDefault<Taxonomy>();
    }

    private Taxonomy GetTaxa<T>(string taxaName) where T : Taxonomy => (Taxonomy) (Queryable.SingleOrDefault<T>(this.TaxonomyManager.GetTaxonomies<T>().Where<T>((Expression<Func<T, bool>>) (t => t.Name == taxaName))) ?? Queryable.FirstOrDefault<T>(this.TaxonomyManager.GetTaxonomies<T>()));
  }
}
