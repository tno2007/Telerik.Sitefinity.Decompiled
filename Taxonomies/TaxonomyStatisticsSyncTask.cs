// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyStatisticsSyncTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Taxonomies
{
  internal class TaxonomyStatisticsSyncTask : ScheduledTask
  {
    public TaxonomyStatisticsSyncTask() => this.ExecuteTime = DateTime.UtcNow;

    public string TypeName { get; set; }

    public string[] Providers { get; set; }

    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(';');
      this.TypeName = strArray[0];
      string str = strArray[1];
      if (string.IsNullOrEmpty(str))
        return;
      this.Providers = str.Split(',');
    }

    public override string GetCustomData() => this.TypeName + ";" + (this.Providers == null || this.Providers.Length == 0 ? "" : string.Join(",", this.Providers));

    public override void ExecuteTask()
    {
      Type itemType = TypeResolutionService.ResolveType(this.TypeName, false);
      if (!(itemType != (Type) null))
        return;
      TaxonomyManager.RecalculateStatistics(itemType, this.Providers);
    }
  }
}
