// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Model.TaxonomiesReportModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.UsageTracking.Model
{
  internal class TaxonomiesReportModel
  {
    public string ModuleName { get; set; }

    public int CustomHierarchicalTaxonomiesCount { get; set; }

    public int CustomFlatTaxonomiesCount { get; set; }

    public int TagsCount { get; set; }

    public int CategoriesCount { get; set; }

    public int DepartmentsCount { get; set; }

    public int HierarchicalTaxaCount { get; set; }

    public int FlatTaxaCount { get; set; }

    public int SynonymsTotalCount { get; set; }

    public long AppliedToItemsTotalCount { get; set; }

    public int SplitTaxonomiesCount { get; set; }
  }
}
