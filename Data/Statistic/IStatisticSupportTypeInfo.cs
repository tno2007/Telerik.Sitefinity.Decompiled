// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Statistic.IStatisticSupportTypeInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Data.Statistic
{
  internal interface IStatisticSupportTypeInfo
  {
    Type Type { get; }

    IEnumerable<string> SupportedStatistics { get; }

    IEnumerable<StatisticLandingPageInfo> LandingPages { get; }
  }
}
