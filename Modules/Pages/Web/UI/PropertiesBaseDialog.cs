// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PropertiesBaseDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Represents the dialog for creating/editing pages.</summary>
  public abstract class PropertiesBaseDialog : DialogBase
  {
    private PageManager pageManager;
    private TaxonomyManager taxonomyManager;

    /// <summary>Gets or sets the page provider.</summary>
    /// <value>The page provider.</value>
    public virtual string PageProvider
    {
      get => (string) this.ViewState[nameof (PageProvider)] ?? Config.Get<PagesConfig>().DefaultProvider;
      set => this.ViewState[nameof (PageProvider)] = (object) value;
    }

    /// <summary>Gets the page manager.</summary>
    /// <value>The page manager.</value>
    protected PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager(this.PageProvider, "sitefinityPagePermissionsInheritanceTransactionName");
        return this.pageManager;
      }
    }

    /// <summary>Gets or sets the taxonomy provider name.</summary>
    /// <value>The taxonomy provider.</value>
    public virtual string TaxonomyProvider
    {
      get => (string) this.ViewState[nameof (TaxonomyProvider)] ?? Config.Get<PagesConfig>().PageTaxonomyProvider;
      set => this.ViewState[nameof (TaxonomyProvider)] = (object) value;
    }

    /// <summary>Gets the taxonomy manager.</summary>
    /// <value>The taxonomy manager.</value>
    protected TaxonomyManager TaxonomyManager
    {
      get
      {
        if (this.taxonomyManager == null)
          this.taxonomyManager = TaxonomyManager.GetManager(this.TaxonomyProvider, "sitefinityPagePermissionsInheritanceTransactionName");
        return this.taxonomyManager;
      }
    }

    /// <summary>Gets or sets the name of the taxonomy.</summary>
    /// <value>The name of the taxonomy.</value>
    public virtual string TaxonomyName { get; set; }

    /// <summary>Gets or sets the mode.</summary>
    /// <value>The mode.</value>
    public abstract DialogModes Mode { get; set; }

    /// <summary>
    /// Gets or sets the taxon used as root for page navigation.
    /// </summary>
    /// <value>The root taxon.</value>
    public RootTaxonType RootTaxon
    {
      get
      {
        string str = this.Page.Request.QueryString["rootTaxon"];
        return !string.IsNullOrEmpty(str) && str.Equals("Backend", StringComparison.OrdinalIgnoreCase) ? RootTaxonType.Backend : RootTaxonType.Frontend;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.Page.RegisterRequiresControlState((Control) this);
    }
  }
}
