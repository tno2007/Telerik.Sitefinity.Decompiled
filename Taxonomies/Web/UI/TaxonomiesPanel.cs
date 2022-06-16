// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomiesPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies.Web.UI.All;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>Represents the control panel for Taxonomy module.</summary>
  public class TaxonomiesPanel : ProviderControlPanel<Page>
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomiesPanel" />.
    /// </summary>
    public TaxonomiesPanel()
      : base(false)
    {
      this.ResourceClassId = typeof (TaxonomyResources).Name;
      this.Title = Res.Get<TaxonomyResources>().Classifications;
    }

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews() => this.AddView<TaxonomiesList>();

    /// <summary>
    /// When overridden this method returns a list of custom Command Panels.
    /// </summary>
    /// <param name="viewMode">Specifies the current View Mode</param>
    /// <param name="list">A list of custom command panels.</param>
    protected override void CreateCustomCommandPanels(string viewMode, IList<ICommandPanel> list)
    {
      base.CreateCustomCommandPanels(viewMode, list);
      list.Add((ICommandPanel) new TaxonomiesSidebar());
    }
  }
}
