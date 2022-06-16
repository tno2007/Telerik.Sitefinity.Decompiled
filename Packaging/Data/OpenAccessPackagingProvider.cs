// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Data.OpenAccessPackagingProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Packaging.Data
{
  /// <summary>
  /// An implementation of the <see cref="T:Telerik.Sitefinity.Packaging.Data.PackagingDataProvider" /> based on OpenAccess ORM persistence framework.
  /// </summary>
  internal class OpenAccessPackagingProvider : 
    PackagingDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Packaging.Data.PackagingMetadataSource" /> object.
    /// </summary>
    /// <param name="context">IDatabaseMappingContext context</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Packaging.Data.PackagingMetadataSource" /></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new PackagingMetadataSource(context);

    /// <inheritdoc />
    internal override Package CreatePackage() => this.CreatePackage(this.GetNewGuid());

    /// <inheritdoc />
    internal override Package CreatePackage(Guid id)
    {
      Package entity = new Package(id);
      entity.Owner = SecurityManager.GetCurrentUserId();
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override Package GetPackage(Guid id)
    {
      this.GetContext();
      return this.GetContext().GetItemById<Package>(id.ToString());
    }

    /// <inheritdoc />
    internal override void DeletePackage(Package package) => this.GetContext().Remove((object) package);

    internal override IQueryable<Package> GetPackages() => this.GetContext().GetAll<Package>();

    /// <inheritdoc />
    internal override Addon CreateAddon() => this.CreateAddon(this.GetNewGuid());

    /// <inheritdoc />
    internal override Addon CreateAddon(Guid id)
    {
      Addon entity = new Addon(id);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override Addon GetAddon(Guid id)
    {
      this.GetContext();
      return this.GetContext().GetItemById<Addon>(id.ToString());
    }

    /// <inheritdoc />
    internal override void DeleteAddon(Addon addon) => this.GetContext().Remove((object) addon);

    internal override IQueryable<Addon> GetAddons() => this.GetContext().GetAll<Addon>();

    /// <inheritdoc />
    internal override AddonLink CreateAddonLink() => this.CreateAddonLink(this.GetNewGuid());

    /// <inheritdoc />
    internal override AddonLink CreateAddonLink(Guid id)
    {
      AddonLink entity = new AddonLink(id);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override AddonLink GetAddonLink(Guid id)
    {
      this.GetContext();
      return this.GetContext().GetItemById<AddonLink>(id.ToString());
    }

    /// <inheritdoc />
    internal override void DeleteAddonLink(AddonLink addonLink) => this.GetContext().Remove((object) addonLink);

    /// <inheritdoc />
    internal override IQueryable<AddonLink> GetAddonLinks() => this.GetContext().GetAll<AddonLink>();

    /// <inheritdoc />
    internal override Lock CreateLock(string lockId)
    {
      Lock entity = new Lock(lockId);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override IQueryable<Lock> GetLocks() => this.GetContext().GetAll<Lock>();

    /// <inheritdoc />
    internal override void DeleteLock(Lock obj) => this.GetContext().Remove((object) obj);
  }
}
