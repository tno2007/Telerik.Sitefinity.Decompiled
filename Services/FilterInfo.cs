// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.FilterInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  internal class FilterInfo : StatusDataWrapper<IStatusFilter>
  {
    internal FilterInfo(IStatusProvider provider, IStatusFilter statusFilter)
      : base(provider, statusFilter)
    {
    }

    public string Title => this.Data.Title;

    public string Key => FilterInfo.GetFormattedKey(this.Source, this.Data.Key);

    internal static string GetFormattedKey(string source, string filterCommand) => string.Format("sf_status_filter_{0}_{1}", (object) source, (object) filterCommand);
  }
}
