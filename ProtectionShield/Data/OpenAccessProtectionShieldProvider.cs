// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Data.OpenAccessProtectionShieldProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ProtectionShield.Model;

namespace Telerik.Sitefinity.ProtectionShield.Data
{
  /// <summary>
  /// An implementation of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Data.OpenAccessProtectionShieldProvider" /> based on OpenAccess ORM persistence framework.
  /// </summary>
  internal class OpenAccessProtectionShieldProvider : 
    ProtectionShieldDataProvider,
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
    /// Gets the <see cref="T:Telerik.Sitefinity.ProtectionShield.Data.ProtectionShieldMetadataSource" /> object.
    /// </summary>
    /// <param name="context">IDatabaseMappingContext context</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.ProtectionShield.Data.ProtectionShieldMetadataSource" /></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new ProtectionShieldMetadataSource(context);

    /// <inheritdoc />
    internal override AccessTokenDevice CreateAccessTokenDevice(
      string token,
      string platform,
      string fullBrowserName,
      string userIpAddress)
    {
      AccessTokenDevice entity = new AccessTokenDevice(token, platform, fullBrowserName, userIpAddress);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override IQueryable<AccessTokenDevice> GetAccessTokenDevices() => this.GetContext().GetAll<AccessTokenDevice>();

    /// <inheritdoc />
    internal override AccessTokenDevice GetAccessTokenDevice(Guid id) => this.GetContext().GetItemById<AccessTokenDevice>(id.ToString());

    /// <inheritdoc />
    internal override void DeleteAccessTokenDevice(AccessTokenDevice accessTokenDevice) => this.GetContext().Remove((object) accessTokenDevice);
  }
}
