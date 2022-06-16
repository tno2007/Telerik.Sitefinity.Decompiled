// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.DynamicTypeInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Services
{
  internal class DynamicTypeInfo : IStatisticSupportTypeInfo
  {
    public DynamicTypeInfo(IDynamicModuleType dt)
    {
      this.Id = dt.Id;
      this.TypeFullName = dt.GetFullTypeName();
      this.DisplayName = dt.DisplayName;
      this.LandingPages = (IEnumerable<StatisticLandingPageInfo>) new StatisticLandingPageInfo[1]
      {
        new StatisticLandingPageInfo(dt.PageId)
      };
      this.SupportedStatistics = (IEnumerable<string>) new string[1]
      {
        "Count"
      };
    }

    public Guid Id { get; private set; }

    public string DisplayName { get; private set; }

    public Type Type => TypeResolutionService.ResolveType(this.TypeFullName);

    public string TypeFullName { get; private set; }

    public IEnumerable<string> SupportedStatistics { get; private set; }

    public IEnumerable<StatisticLandingPageInfo> LandingPages { get; private set; }
  }
}
