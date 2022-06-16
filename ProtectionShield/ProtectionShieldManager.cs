// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.ProtectionShield.Configuration;
using Telerik.Sitefinity.ProtectionShield.Data;
using Telerik.Sitefinity.ProtectionShield.Model;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>
  /// Manages the data layer for the Protection shield module.
  /// </summary>
  /// <seealso cref="!:Telerik.Sitefinity.Data.ManagerBase&lt;Telerik.Sitefinity.ProtectionShield.Data.ProtectionShieldDataProvider&gt;" />
  internal class ProtectionShieldManager : ManagerBase<ProtectionShieldDataProvider>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /> class.
    /// </summary>
    public ProtectionShieldManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public ProtectionShieldManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">The name of a distributed transaction. If empty string or null this manager will use separate transaction.</param>
    public ProtectionShieldManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>The name of the module that this manager belongs to.</summary>
    /// <value></value>
    public override string ModuleName => "ProtectionShield";

    /// <summary>Collection of data provider settings.</summary>
    /// <value></value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<ProtectionShieldConfig>().Providers;

    /// <summary>Gets the default provider for this manager.</summary>
    /// <value></value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<ProtectionShieldConfig>().DefaultProvider);

    /// <summary>
    /// Get an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /> using the default provider
    /// </summary>
    /// <returns>Instance of <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /></returns>
    public static ProtectionShieldManager GetManager() => ManagerBase<ProtectionShieldDataProvider>.GetManager<ProtectionShieldManager>();

    /// <summary>
    /// Get an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /> by explicitly specifying the provider name to be used.
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /></returns>
    public static ProtectionShieldManager GetManager(string providerName) => ManagerBase<ProtectionShieldDataProvider>.GetManager<ProtectionShieldManager>(providerName);

    /// <summary>
    /// Get an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /> by explicitly specifying the provider and transaction name to be used.
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">The name of a named transaction to be used.</param>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /></returns>
    public static ProtectionShieldManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<ProtectionShieldDataProvider>.GetManager<ProtectionShieldManager>(providerName, transactionName);
    }

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" />.
    /// </summary>
    /// <param name="token">The access token.</param>
    /// <param name="platform">The platform</param>
    /// <param name="fullBrowserName">The full browser name.</param>
    /// <param name="userIpAddress">The user IP address.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> item.</returns>
    public AccessTokenDevice CreateAccessTokenDevice(
      string token,
      string platform,
      string fullBrowserName,
      string userIpAddress)
    {
      return this.Provider.CreateAccessTokenDevice(token, platform, fullBrowserName, userIpAddress);
    }

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> items.
    /// </summary>
    /// <returns>IQueryable object for all <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> items</returns>
    public IQueryable<AccessTokenDevice> GetAccessTokenDevices() => this.Provider.GetAccessTokenDevices();

    /// <summary>
    /// Search for a <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> by its identity.
    /// </summary>
    /// <param name="id">The id of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /></param>
    /// <returns>The found <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> item.</returns>
    public AccessTokenDevice GetAccessTokenDevice(Guid id) => this.Provider.GetAccessTokenDevice(id);

    /// <summary>
    /// Deletes the provided <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" />.
    /// </summary>
    /// <param name="accessTokenDevice">The <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> to delete.</param>
    public void DeleteAccessTokenDevice(AccessTokenDevice accessTokenDevice) => this.Provider.DeleteAccessTokenDevice(accessTokenDevice);
  }
}
