﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonomyPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical
{
  /// <summary>
  /// Represents the control panel for managing Hierarchical taxonomy.
  /// </summary>
  public class HierarchicalTaxonomyPanel : TaxonomyBasePanel
  {
    /// <summary>Loads configured views.</summary>
    protected override void CreateViews() => this.AddView<HierarchicalTaxonList>();
  }
}