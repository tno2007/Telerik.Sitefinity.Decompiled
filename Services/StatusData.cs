// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.StatusData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services
{
  internal class StatusData : IItemStatusData
  {
    public StatusData(Guid itemId, string status)
    {
      this.ItemId = itemId;
      this.Status = status;
    }

    public Guid ItemId { get; private set; }

    public string Status { get; private set; }

    public string GetTextFormat(out object[] arguments)
    {
      arguments = (object[]) null;
      return this.Status;
    }
  }
}
