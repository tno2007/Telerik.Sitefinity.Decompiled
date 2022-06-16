// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.InvalidateSitemapCacheMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.LoadBalancing
{
  internal class InvalidateSitemapCacheMessage : SystemMessageBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.ResetApplicationMessage" /> class.
    /// </summary>
    /// <param name="attemptFullRestart">if set to <c>true</c> [attempt full restart].</param>
    public InvalidateSitemapCacheMessage() => this.Key = "InvalidateSitemapCacheKey";
  }
}
