// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.WarmupLevel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.OutputCache
{
  /// <summary>Defines warmup strategy modes</summary>
  public enum WarmupLevel
  {
    /// <summary>No warmup</summary>
    None,
    /// <summary>Warmup only URLs with high priority</summary>
    Light,
    /// <summary>Warmup all URLs</summary>
    Full,
  }
}
