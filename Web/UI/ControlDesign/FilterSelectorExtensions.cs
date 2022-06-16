// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  public static class FilterSelectorExtensions
  {
    /// <summary>
    /// Sets the TaxonomyId property of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem" /> instance with the specified query data name.
    /// </summary>
    /// <param name="queryDataName">The value of QueryDataName property.</param>
    /// <param name="taxonomyId">The id of the taxonomy.</param>
    public static void SetTaxonomyId(
      this FilterSelector filterSelector,
      string queryDataName,
      Guid taxonomyId)
    {
      FilterSelectorItem itemByQueryDataName = filterSelector.FindItemByQueryDataName(queryDataName);
      if (itemByQueryDataName == null || !(itemByQueryDataName.ActualSelectorResultView is TaxonSelectorResultView selectorResultView))
        return;
      selectorResultView.TaxonomyId = taxonomyId;
    }
  }
}
