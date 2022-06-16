﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.TemplatesView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Reoresents the templates view.</summary>
  public class TemplatesView : PagesBaseView
  {
    /// <summary>
    ///  Gets the name of resource file representing Templates View.
    /// </summary>
    public static readonly string TemplatesViewPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.TemplatesView.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override string DefaultLayoutTemplatePath => TemplatesView.TemplatesViewPath;

    /// <summary>Gets or sets the name of the page taxonomy.</summary>
    /// <value>The name of the page taxonomy.</value>
    public override string TaxonomyName
    {
      get => (string) this.ViewState[nameof (TaxonomyName)] ?? Config.Get<PagesConfig>().PageTemplatesTaxonomyName;
      set => this.ViewState[nameof (TaxonomyName)] = (object) value;
    }

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      this.ExplorerControl.Configuration.ContentProviderTypeName = typeof (TemplateBrowserContentProvider).AssemblyQualifiedName;
      this.PageId = SiteInitializer.PagesNodeId;
      base.InitializeControls(viewContainer);
    }

    /// <summary>Gets the view paths of the explorer.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns></returns>
    protected override string[] GetViewPaths(HierarchicalTaxonomy taxonomy)
    {
      Guid backendTemplatesCategoryId = SiteInitializer.BackendTemplatesCategoryId;
      List<Taxon> list = taxonomy.Taxa.Where<Taxon>((Func<Taxon, bool>) (t => t.Id != backendTemplatesCategoryId)).OrderBy<Taxon, float>((Func<Taxon, float>) (t => t.Ordinal)).ToList<Taxon>();
      string[] viewPaths = new string[list.Count + 1];
      viewPaths[0] = "All";
      for (int index = 1; index < viewPaths.Length; ++index)
        viewPaths[index] = list[index - 1].Name;
      return viewPaths;
    }
  }
}
