// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SitefinityTypeRegistry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// A registry of "content" types in the most generic sense,
  /// containing information such as human-friendly names, parent/child relationships and possibly other.
  /// </summary>
  /// <remarks>
  /// The system-wide instance is returned by the <see cref="P:Telerik.Sitefinity.Services.SystemManager.TypeRegistry" /> property.
  /// </remarks>
  public sealed class SitefinityTypeRegistry
  {
    private volatile bool initialized;
    private object initLock = new object();
    private ConcurrentBag<SitefinityTypeRegistryInitializer> initializers = new ConcurrentBag<SitefinityTypeRegistryInitializer>();
    private ConcurrentDictionary<string, SitefinityType> types = new ConcurrentDictionary<string, SitefinityType>();

    /// <summary>
    /// Checks if a type with the specified <paramref name="typeKey" /> is registered.
    /// </summary>
    public bool IsRegistered(string typeKey)
    {
      this.EnsureInitialized();
      return this.types.ContainsKey(typeKey);
    }

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.Services.SitefinityType" /> instance registered for the specified <paramref name="typeKey" />.
    /// </summary>
    public SitefinityType GetType(string typeKey)
    {
      this.EnsureInitialized();
      SitefinityType type;
      this.types.TryGetValue(typeKey, out type);
      return type;
    }

    /// <summary>
    /// Gets the user-friendly (singular) title, if the type is registered and <c>null</c> otherwise.
    /// </summary>
    public string GetTitle(string typeKey)
    {
      this.EnsureInitialized();
      SitefinityType sitefinityType;
      return this.types.TryGetValue(typeKey, out sitefinityType) ? sitefinityType.SingularTitle : (string) null;
    }

    /// <summary>
    /// Registers a <see cref="T:Telerik.Sitefinity.Services.SitefinityType" /> instance with the specified <paramref name="typeKey" />.
    /// </summary>
    public void Register(string typeKey, SitefinityType type)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      if (typeKey.IsNullOrEmpty())
        throw new ArgumentNullException(nameof (typeKey));
      if (type.Key == null)
        type.Key = typeKey;
      this.types.TryAdd(typeKey, type);
    }

    /// <summary>
    /// Unregisters a <see cref="T:Telerik.Sitefinity.Services.SitefinityType" /> instance with the specified <paramref name="typeKey" />.
    /// </summary>
    internal void Unregister(string typeKey)
    {
      if (typeKey.IsNullOrEmpty())
        throw new ArgumentNullException(nameof (typeKey));
      this.types.TryRemove(typeKey, out SitefinityType _);
    }

    internal void RegisterInitializer(SitefinityTypeRegistryInitializer initializer) => this.initializers.Add(initializer);

    internal void Clear() => this.types.Clear();

    internal IEnumerable<SitefinityType> GetChildTypes(string parentTypeKey) => this.GetType(parentTypeKey) != null ? this.types.Where<KeyValuePair<string, SitefinityType>>((Func<KeyValuePair<string, SitefinityType>, bool>) (x => x.Value.Parent == parentTypeKey)).Select<KeyValuePair<string, SitefinityType>, SitefinityType>((Func<KeyValuePair<string, SitefinityType>, SitefinityType>) (x => x.Value)) : (IEnumerable<SitefinityType>) null;

    private void EnsureInitialized()
    {
      if (this.initialized)
        return;
      lock (this.initLock)
      {
        if (this.initialized)
          return;
        this.Initialize();
      }
    }

    private void Initialize()
    {
      this.RegisterDefaults();
      foreach (SitefinityTypeRegistryInitializer initializer in this.initializers)
        initializer(this);
      this.initialized = true;
    }

    private void RegisterDefaults()
    {
      this.Register(typeof (PageTemplate).FullName, new SitefinityType()
      {
        SingularTitle = Res.Get<PageResources>().PageTemplatesTitle,
        PluralTitle = Res.Get<PageResources>().PageTemplatesTitle,
        Parent = (string) null,
        Kind = SitefinityTypeKind.Type
      });
      this.Register(typeof (PageNode).FullName, new SitefinityType()
      {
        ModuleName = "Pages",
        SingularTitle = Res.Get<PageResources>().Page,
        PluralTitle = Res.Get<PageResources>().Pages,
        Parent = typeof (PageNode).FullName,
        Kind = SitefinityTypeKind.Type,
        Icon = "sitemap"
      });
      this.Register(typeof (Taxonomy).FullName, new SitefinityType()
      {
        ModuleName = "Taxonomy",
        SingularTitle = Res.Get<TaxonomyResources>().Classification,
        PluralTitle = Res.Get<TaxonomyResources>().Classifications,
        Parent = (string) null,
        Kind = SitefinityTypeKind.Type,
        Icon = "list"
      });
      this.Register(typeof (Taxon).FullName, new SitefinityType()
      {
        ModuleName = "Taxonomy",
        SingularTitle = Res.Get<TaxonomyResources>().Taxon,
        PluralTitle = Res.Get<TaxonomyResources>().TaxonTypeNames,
        Parent = typeof (Taxonomy).FullName,
        Kind = SitefinityTypeKind.Type
      });
      this.Register(typeof (FlatTaxon).FullName, new SitefinityType()
      {
        ModuleName = "Taxonomy",
        SingularTitle = Res.Get<TaxonomyResources>().Taxon,
        PluralTitle = Res.Get<TaxonomyResources>().Flat,
        Parent = typeof (Taxonomy).FullName,
        Kind = SitefinityTypeKind.Type,
        Icon = "tags"
      });
      this.Register(typeof (HierarchicalTaxon).FullName, new SitefinityType()
      {
        ModuleName = "Taxonomy",
        SingularTitle = Res.Get<TaxonomyResources>().Taxon,
        PluralTitle = Res.Get<TaxonomyResources>().Hierarchical,
        Parent = typeof (Taxonomy).FullName,
        Kind = SitefinityTypeKind.Type,
        Icon = "sitemap"
      });
      this.Register(typeof (User).FullName, new SitefinityType()
      {
        SingularTitle = Res.Get<Labels>().User,
        PluralTitle = Res.Get<Labels>().Users,
        Parent = (string) null,
        Kind = SitefinityTypeKind.Type
      });
      this.Register(typeof (Role).FullName, new SitefinityType()
      {
        SingularTitle = Res.Get<Labels>().Role,
        PluralTitle = Res.Get<Labels>().Roles,
        Parent = (string) null,
        Kind = SitefinityTypeKind.Type
      });
      this.Register(typeof (Folder).FullName, new SitefinityType()
      {
        SingularTitle = "Folder",
        PluralTitle = "Folders",
        Parent = (string) null,
        Kind = SitefinityTypeKind.Type
      });
      this.Register(typeof (SitefinityProfile).FullName, new SitefinityType()
      {
        SingularTitle = "User profile",
        PluralTitle = "User profiles",
        Parent = (string) null,
        Kind = SitefinityTypeKind.Type
      });
    }
  }
}
