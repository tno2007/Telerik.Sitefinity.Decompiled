﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.StatusFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  internal class StatusFilter : IStatusFilter
  {
    public StatusFilter(string key, string title)
    {
      this.Key = key;
      this.Title = title;
    }

    public string Title { get; private set; }

    public string Key { get; private set; }
  }
}
