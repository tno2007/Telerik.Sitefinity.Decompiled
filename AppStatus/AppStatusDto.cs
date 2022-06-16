// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.AppStatus.AppStatusDto
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.AppStatus
{
  internal class AppStatusDto
  {
    public List<AppStatusEntry> Info { get; set; }

    public string State { get; set; }

    public bool AdminMode { get; set; }
  }
}
