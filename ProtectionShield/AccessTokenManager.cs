// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.AccessTokenManager
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
  /// <summary>Manages the data layer for the Access tokens.</summary>
  /// <seealso cref="!:Telerik.Sitefinity.Data.ManagerBase&lt;Telerik.Sitefinity.ProtectionShield.Data.AccessTokenDataProvider&gt;" />
  internal class AccessTokenManager : ManagerBase<AccessTokenDataProvider>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" /> class.
    /// </summary>
    public AccessTokenManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public AccessTokenManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">The name of a distributed transaction. If empty string or null this manager will use separate transaction.</param>
    public AccessTokenManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>The name of the module that this manager belongs to.</summary>
    /// <value></value>
    public override string ModuleName => "ProtectionShield";

    /// <summary>Collection of data provider settings.</summary>
    /// <value></value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<ProtectionShieldConfig>().AccessTokenProviders;

    /// <summary>Gets the default provider for this manager.</summary>
    /// <value></value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<ProtectionShieldConfig>().DefaultAccessTokenProvider);

    /// <summary>
    /// Get an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" /> using the default provider
    /// </summary>
    /// <returns>Instance of <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" /></returns>
    public static AccessTokenManager GetManager() => ManagerBase<AccessTokenDataProvider>.GetManager<AccessTokenManager>();

    /// <summary>
    /// Get an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" /> by explicitly specifying the provider name to be used.
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" /></returns>
    public static AccessTokenManager GetManager(string providerName) => ManagerBase<AccessTokenDataProvider>.GetManager<AccessTokenManager>(providerName);

    /// <summary>
    /// Get an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" /> by explicitly specifying the provider and transaction name to be used.
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">The name of a named transaction to be used.</param>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" /></returns>
    public static AccessTokenManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<AccessTokenDataProvider>.GetManager<AccessTokenManager>(providerName, transactionName);
    }

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" />.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <param name="email">The email.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="issuedBy">The issuer's id.</param>
    /// <param name="expiresOn">The expiration date.</param>
    /// <param name="reason">The reason.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> item.</returns>
    public AccessToken CreateAccessToken(
      string token,
      string email,
      Guid siteId,
      Guid issuedBy,
      DateTime expiresOn,
      AccessTokenReason reason = AccessTokenReason.FrondEndUser)
    {
      return this.Provider.CreateAccessToken(token, email, siteId, issuedBy, expiresOn, reason);
    }

    /// <summary>
    /// Deletes the provided <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" />.
    /// </summary>
    /// <param name="accessToken">The <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> to delete.</param>
    public void DeleteAccessToken(AccessToken accessToken) => this.Provider.DeleteAccessToken(accessToken);

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" />.
    /// </summary>
    /// <param name="id">The id of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /></param>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /></returns>
    public AccessToken GetAccessToken(Guid id) => this.Provider.GetAccessToken(id);

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> items.
    /// </summary>
    /// <returns>Queryable object for all <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> items</returns>
    public IQueryable<AccessToken> GetAccessTokens() => this.Provider.GetAccessTokens();
  }
}
