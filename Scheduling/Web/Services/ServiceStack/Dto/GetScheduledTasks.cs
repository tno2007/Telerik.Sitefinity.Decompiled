﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack.Dto.GetScheduledTasks
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Scheduling.Web.Services.ServiceStack.Dto
{
  internal class GetScheduledTasks
  {
    public int Skip { get; set; }

    public int Take { get; set; }

    public string SearchTerm { get; set; }

    public OrderByType OrderBy { get; set; }

    public FilterByType FilterBy { get; set; }
  }
}
