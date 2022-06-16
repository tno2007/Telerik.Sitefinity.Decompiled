// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Level2CacheSyncMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>Message for DataAccess L2 cache invalidation.</summary>
  public class Level2CacheSyncMessage : SystemMessageBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.Level2CacheSyncMessage" /> class.
    /// </summary>
    public Level2CacheSyncMessage() => this.Key = "InvalidateOpenAccessL2CacheItems";
  }
}
