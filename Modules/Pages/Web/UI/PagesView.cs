// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PagesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI.FileExplorer;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Represents the pages view.</summary>
  public class PagesView : PagesBaseView
  {
    /// <summary>
    ///  Gets the name of resource file representing Pages View.
    /// </summary>
    public static readonly string PagesViewPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.PagesView.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      this.ExplorerControl.Configuration.ContentProviderTypeName = typeof (PageBrowserContentProvider).AssemblyQualifiedName;
      this.PageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000010");
      base.InitializeControls(viewContainer);
      this.ExplorerControl.VisibleControls |= FileExplorerControls.ContextMenus;
    }

    /// <inheritdoc />
    protected override string DefaultLayoutTemplatePath => PagesView.PagesViewPath;

    /// <summary>Gets the view paths of the explorer.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns></returns>
    protected override string[] GetViewPaths(HierarchicalTaxonomy taxonomy)
    {
      Guid backendRootNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000003");
      List<Taxon> list = taxonomy.Taxa.Where<Taxon>((Func<Taxon, bool>) (t => t.Id != backendRootNodeId)).OrderBy<Taxon, float>((Func<Taxon, float>) (t => t.Ordinal)).ToList<Taxon>();
      string[] viewPaths = new string[list.Count];
      for (int index = 0; index < viewPaths.Length; ++index)
        viewPaths[index] = list[index].Name;
      return viewPaths;
    }
  }
}
