// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Statistic.StatisticLandingPageInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data.Statistic
{
  internal class StatisticLandingPageInfo
  {
    public StatisticLandingPageInfo(Guid pageId, string filter = null)
    {
      this.PageId = pageId;
      this.Filter = filter;
    }

    public Guid PageId { get; private set; }

    public string Filter { get; private set; }
  }
}
