// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.InMemoryOutputCacheDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Web
{
  internal class InMemoryOutputCacheDependency : System.Web.Caching.CacheDependency
  {
    private string cacheDependencyId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.InMemoryOutputCacheDependency" /> class.
    /// </summary>
    /// <param name="key">The key of the output cache item.</param>
    public InMemoryOutputCacheDependency(string key)
    {
      this.cacheDependencyId = key;
      this.ExpirePage = new ChangedCallback(this.ExpirePageMethod);
      Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof (CacheDependencyHandler), typeof (InMemoryOutputCacheDependency), key, this.ExpirePage);
    }

    /// <summary>Gets a delegate to the expire page method.</summary>
    /// <value>The expire page.</value>
    public ChangedCallback ExpirePage { get; private set; }

    /// <summary>Notifies the base <see cref="T:System.Web.Caching.CacheDependency" />
    /// object that the dependency represented by a derived <see cref="T:System.Web.Caching.CompositeCacheDependency" />
    /// class has changed.</summary>
    /// <param name="caller">The caller.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemKey">The item key.</param>
    private void ExpirePageMethod(ICacheDependencyHandler caller, Type itemType, string itemKey)
    {
      this.NotifyDependencyChanged((object) caller, EventArgs.Empty);
      this.ExpirePage = (ChangedCallback) null;
    }

    /// <summary>Gets the unique ID.</summary>
    /// <returns>
    /// The unique identifier for the <see cref="T:System.Web.Caching.CacheDependency" /> object.
    /// </returns>
    public override string GetUniqueID() => this.cacheDependencyId.ToString();
  }
}
