// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Data.AccessTokenDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.ProtectionShield.Model;

namespace Telerik.Sitefinity.ProtectionShield.Data
{
  /// <summary>
  /// Defines the basic functionality that should be implemented by Access token data providers.
  /// </summary>
  [ApplyNoPolicies]
  internal abstract class AccessTokenDataProvider : DataProviderBase
  {
    /// <inheritdoc />
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (AccessToken)
    };

    /// <inheritdoc />
    public override string RootKey => typeof (AccessTokenDataProvider).Name;

    /// <inheritdoc />
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return itemType == typeof (AccessToken) ? (IEnumerable) DataProviderBase.SetExpressions<AccessToken>(this.GetAccessTokens(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount) : base.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <inheritdoc />
    public override void DeleteItem(object item)
    {
      if (item.GetType() == typeof (AccessToken))
        this.DeleteAccessToken((AccessToken) item);
      else
        base.DeleteItem(item);
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
    internal abstract AccessToken CreateAccessToken(
      string token,
      string email,
      Guid siteId,
      Guid issuedBy,
      DateTime expiresOn,
      AccessTokenReason reason);

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> items.
    /// </summary>
    /// <returns>IQueryable object for all <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> items</returns>
    internal abstract IQueryable<AccessToken> GetAccessTokens();

    /// <summary>
    /// Search for a <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> by its identity.
    /// </summary>
    /// <param name="id">The id of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /></param>
    /// <returns>The found <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> item.</returns>
    internal abstract AccessToken GetAccessToken(Guid id);

    /// <summary>
    /// Deletes the provided <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" />.
    /// </summary>
    /// <param name="accessToken">The <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> to delete.</param>
    internal abstract void DeleteAccessToken(AccessToken accessToken);
  }
}
