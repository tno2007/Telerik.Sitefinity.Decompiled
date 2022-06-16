// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheItemProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.OutputCache;

namespace Telerik.Sitefinity.Web.OutputCache.Data
{
  /// <summary>
  /// Represents the information from OutputCacheItem that is not bound to a Context and can be persisted in cache
  /// </summary>
  /// <seealso cref="!:Telerik.Sitefinity.OutputCache.IOutputCacheItem" />
  internal class OutputCacheItemProxy
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheItemProxy" /> class.
    /// </summary>
    /// <param name="outputCacheItem">The output cache item metadata.</param>
    public OutputCacheItemProxy(OutputCacheItem outputCacheItem)
    {
      this.Key = outputCacheItem.Key;
      this.ETag = outputCacheItem.ETag;
      this.DateModified = outputCacheItem.DateModified;
      this.SiteId = outputCacheItem.SiteId;
      this.Status = outputCacheItem.Status;
      this.SiteMapNodeKey = outputCacheItem.SiteMapNodeKey;
      this.Priority = outputCacheItem.Priority;
    }

    /// <summary>Gets the output cache identifier.</summary>
    /// <value>The identifier.</value>
    public string Key { get; private set; }

    /// <summary>Gets the site identifier.</summary>
    /// <value>The site identifier.</value>
    public Guid SiteId { get; private set; }

    /// <summary>Gets the date modified.</summary>
    /// <value>The date modified.</value>
    public DateTime DateModified { get; private set; }

    /// <summary>Gets the ETag.</summary>
    /// <value>The e tag.</value>
    public string ETag { get; private set; }

    /// <summary>Gets the ID of the page node.</summary>
    /// <value>The page node key.</value>
    public string SiteMapNodeKey { get; private set; }

    /// <summary>Gets the priority.</summary>
    /// <value>
    /// The priority of the output cache item. Lower numbers are with higher priority.
    /// </value>
    public int Priority { get; private set; }

    /// <summary>Gets the status.</summary>
    public OutputCacheItemStatus Status { get; private set; }

    public bool Expired => this.Status != OutputCacheItemStatus.Live && this.DateModified < DateTime.UtcNow.AddSeconds((double) -CacheClientFactory.GetCacheInvalidationStrategy().CacheExpirationMaxDelay);
  }
}
