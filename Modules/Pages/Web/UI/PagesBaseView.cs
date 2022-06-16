// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PagesBaseView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI.FileExplorer;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// 
  /// </summary>
  public class PagesBaseView : ViewModeControl<PagesPanel>
  {
    private Guid pageId = Guid.Empty;

    /// <summary>Gets or sets the taxonomy provider name.</summary>
    /// <value>The taxonomy provider.</value>
    public virtual string TaxonomyProvider
    {
      get => (string) this.ViewState[nameof (TaxonomyProvider)] ?? Config.Get<PagesConfig>().PageTaxonomyProvider;
      set => this.ViewState[nameof (TaxonomyProvider)] = (object) value;
    }

    /// <summary>Gets or sets the name of the page taxonomy.</summary>
    /// <value>The name of the page taxonomy.</value>
    public virtual string TaxonomyName
    {
      get => (string) this.ViewState[nameof (TaxonomyName)] ?? Config.Get<PagesConfig>().PageTaxonomyName;
      set => this.ViewState[nameof (TaxonomyName)] = (object) value;
    }

    /// <summary>Gets or sets the page pageId.</summary>
    /// <value>The page pageId.</value>
    public Guid PageId
    {
      get => this.pageId;
      set => this.pageId = value;
    }

    /// <summary>Gets the explorer control.</summary>
    /// <value>The explorer control.</value>
    protected virtual BaseExplorer ExplorerControl => this.Container.GetControl<BaseExplorer>("explorer", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.EnsureChildControls();
    }

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      PageManager manager1 = PageManager.GetManager(this.Host.ProviderName);
      TaxonomyManager manager2 = TaxonomyManager.GetManager(this.TaxonomyProvider);
      string taxName = this.TaxonomyName;
      this.Context.Items[(object) "SfCurrentPagesProvider"] = (object) this.Host.ProviderName;
      this.Context.Items[(object) "SfCurrentTaxonomyName"] = (object) taxName;
      this.Context.Items[(object) "SfCurrentTaxonomyProvider"] = (object) manager2.Provider.Name;
      string[] viewPaths = this.GetViewPaths(manager2.GetTaxonomies<HierarchicalTaxonomy>().Single<HierarchicalTaxonomy>((Expression<Func<HierarchicalTaxonomy, bool>>) (t => t.Name == taxName)));
      this.ExplorerControl.Configuration.ViewPaths = this.ExplorerControl.Configuration.DeletePaths = this.ExplorerControl.Configuration.UploadPaths = viewPaths;
      this.ExplorerControl.VisibleControls = FileExplorerControls.TreeView | FileExplorerControls.Grid | FileExplorerControls.Toolbar;
      this.ExplorerControl.InitialPath = viewPaths[0];
      this.ExplorerControl.EnableOpenFile = false;
      PageNode pageNode = manager1.GetPageNode(this.PageId);
      if (pageNode == null)
        return;
      this.ExplorerControl.ViewUrl = RouteHelper.ResolveUrl(pageNode.GetUrl(), UrlResolveOptions.Rooted);
    }

    /// <summary>Gets the view paths of the explorer.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns></returns>
    protected virtual string[] GetViewPaths(HierarchicalTaxonomy taxonomy)
    {
      string[] viewPaths = new string[taxonomy.Taxa.Count];
      for (int index = 0; index < viewPaths.Length; ++index)
        viewPaths[index] = taxonomy.Taxa[index].Name;
      return viewPaths;
    }
  }
}
