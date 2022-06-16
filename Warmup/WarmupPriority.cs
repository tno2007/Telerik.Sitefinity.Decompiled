// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Warmup.WarmupPriority
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Warmup
{
  /// <summary>
  /// Defines priorities for <see cref="!:IWarmupUrl" /> instances. Used by the <see cref="T:Telerik.Sitefinity.Warmup.WarmupModule" /> to determine which instances should be warmed up first.
  /// </summary>
  public enum WarmupPriority
  {
    /// <summary>
    /// Defines low priority for <see cref="!:IWarmupUrl" /> instances.
    /// </summary>
    Low,
    /// <summary>
    /// Defines normal priority for <see cref="!:IWarmupUrl" /> instances.
    /// </summary>
    Normal,
    /// <summary>
    /// Defines high priority for <see cref="!:IWarmupUrl" /> instances.
    /// </summary>
    High,
  }
}
