// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.CompositeCacheDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  /// <summary>Establishes a dependency relationship between an Sitefinity page item with specified pageId and an object of type
  /// <see cref="T:System.Web.Caching.CacheDependency" />.
  /// The <see cref="T:System.Web.Caching.CacheDependency" /> class monitors
  /// the dependency relationships so that when any of them changes, the cached item
  /// will be automatically removed.</summary>
  public class CompositeCacheDependency : System.Web.Caching.CacheDependency
  {
    private string cacheDependencyId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.VirtualPath.CompositeCacheDependency" /> class.
    /// </summary>
    public CompositeCacheDependency()
    {
      this.cacheDependencyId = Guid.NewGuid().ToString();
      this.ExpirePage = new ChangedCallback(this.ExpirePageMethod);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.VirtualPath.CompositeCacheDependency" /> class.
    /// </summary>
    /// <param name="pageId">The page id.</param>
    public CompositeCacheDependency(string pageId)
      : this()
    {
      this.AddCacheDependencyKey(CompositeCacheDependency.CacheDependencyType, pageId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.VirtualPath.CompositeCacheDependency" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    public CompositeCacheDependency(Type itemType)
      : this()
    {
      this.AddCacheDependencyKey(itemType);
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

    /// <summary>
    /// Subscribes for notification when an item of the provided type and key is changed.
    /// This overload subscribes only to handlers that inherit from the specified type and has specified key.
    /// </summary>
    /// <param name="trackedType">Type of the tracked.</param>
    /// <param name="key">The key.</param>
    public void AddCacheDependencyKey(Type trackedType, string key) => Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof (CacheDependencyHandler), trackedType, key, this.ExpirePage);

    /// <summary>
    /// Subscribes for notification when an item of the provided type is changed.
    /// This overload subscribes only to handlers that inherit from the specified type.
    /// </summary>
    /// <param name="trackedType">Type to track.</param>
    public void AddCacheDependencyKey(Type trackedType) => Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof (CacheDependencyHandler), trackedType, this.ExpirePage);

    /// <summary>
    /// Subscribes for notification when an item of the provided type is changed.
    /// This overload subscribes only to handlers that inherit from the specified type.
    /// </summary>
    /// <param name="cacheDependencyNotifiedObjects">The cache dependency notified objects.</param>
    public void AddCacheDependencyKeys(
      IList<CacheDependencyKey> cacheDependencyNotifiedObjects)
    {
      foreach (CacheDependencyKey dependencyNotifiedObject in (IEnumerable<CacheDependencyKey>) cacheDependencyNotifiedObjects)
      {
        if (dependencyNotifiedObject.Key.IsNullOrEmpty())
          Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof (CacheDependencyHandler), dependencyNotifiedObject.Type, this.ExpirePage);
        else
          Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof (CacheDependencyHandler), dependencyNotifiedObject.Type, dependencyNotifiedObject.Key, this.ExpirePage);
      }
    }

    /// <summary>Gets the unique ID.</summary>
    /// <returns>
    /// The unique identifier for the <see cref="T:System.Web.Caching.CacheDependency" /> object.
    /// </returns>
    public override string GetUniqueID() => this.cacheDependencyId.ToString();

    internal static Type CacheDependencyType => typeof (CacheDependencyPageDataObject);

    /// <inheritdoc />
    protected override void DependencyDispose() => base.DependencyDispose();
  }
}
