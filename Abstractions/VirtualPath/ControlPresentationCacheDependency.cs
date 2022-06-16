// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.ControlPresentationCacheDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  /// <summary>
  /// Establishes a dependency relationship between an Sitefinity control presentation item with specified id and an object of type
  /// <see cref="T:System.Web.Caching.CacheDependency" />.
  /// The <see cref="T:System.Web.Caching.CacheDependency" /> class monitors
  /// the dependency relationships so that when any of them changes, the cached item
  /// will be automatically removed.
  /// </summary>
  public class ControlPresentationCacheDependency : DataItemSystemCacheDependency
  {
    public ControlPresentationCacheDependency()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.VirtualPath.ControlPresentationCacheDependency" /> class.
    /// </summary>
    /// <param name="pageId">The page id.</param>
    public ControlPresentationCacheDependency(string itemId, string uniquePrefix = "")
      : base(typeof (ControlPresentation), itemId, uniquePrefix)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.VirtualPath.ControlPresentationCacheDependency" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="uniquePrefix">The unique prefix.</param>
    public ControlPresentationCacheDependency(Type itemType, string uniquePrefix = "")
      : base(itemType, uniquePrefix)
    {
    }

    public void AddAdditionalKey(string key) => CacheDependency.Subscribe(typeof (ControlPresentation), key, this.Expire);
  }
}
