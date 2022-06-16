// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.WarningData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Contains parameters for a warning message to be shown when editing the item.
  /// </summary>
  public class WarningData : IWarningData
  {
    /// <summary>Gets or sets warning title.</summary>
    public string Title { get; set; }

    /// <summary>Gets or sets warning description.</summary>
    public string Description { get; set; }
  }
}
