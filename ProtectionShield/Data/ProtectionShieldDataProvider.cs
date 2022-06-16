// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Data.ProtectionShieldDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.ProtectionShield.Model;

namespace Telerik.Sitefinity.ProtectionShield.Data
{
  /// <summary>
  /// Defines the basic functionality that should be implemented by Protection shield data providers.
  /// </summary>
  [ApplyNoPolicies]
  internal abstract class ProtectionShieldDataProvider : DataProviderBase
  {
    /// <inheritdoc />
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (AccessTokenDevice)
    };

    /// <inheritdoc />
    public override string RootKey => typeof (ProtectionShieldDataProvider).Name;

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" />.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <param name="platform">The platform</param>
    /// <param name="fullBrowserName">The full browser name.</param>
    /// <param name="userIpAddress">The user IP address.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> item.</returns>
    internal abstract AccessTokenDevice CreateAccessTokenDevice(
      string token,
      string platform,
      string fullBrowserName,
      string userIpAddress);

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> items.
    /// </summary>
    /// <returns>IQueryable object for all <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> items</returns>
    internal abstract IQueryable<AccessTokenDevice> GetAccessTokenDevices();

    /// <summary>
    /// Search for a <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> by its identity.
    /// </summary>
    /// <param name="id">The id of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /></param>
    /// <returns>The found <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> item.</returns>
    internal abstract AccessTokenDevice GetAccessTokenDevice(Guid id);

    /// <summary>
    /// Deletes the provided <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" />.
    /// </summary>
    /// <param name="accessTokenDevice">The <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> to delete.</param>
    internal abstract void DeleteAccessTokenDevice(AccessTokenDevice accessTokenDevice);
  }
}
