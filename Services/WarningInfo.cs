// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.WarningInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  internal class WarningInfo : StatusDataWrapper<IWarningData>
  {
    internal WarningInfo(IStatusProvider provider, IWarningData data)
      : base(provider, data)
    {
    }

    public string Title => this.Data.Title;

    public string Description => this.Data.Description;
  }
}
