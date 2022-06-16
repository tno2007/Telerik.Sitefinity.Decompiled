// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.SecurityDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>Represents base class for security data providers.</summary>
  public abstract class SecurityDataProvider : DataProviderBase
  {
    private static SecurityConfig config = Telerik.Sitefinity.Configuration.Config.Get<SecurityConfig>();

    /// <summary>Gets the security root with the provided key.</summary>
    /// <param name="key">The key of the security root to retrieve.</param>
    /// <returns></returns>
    public abstract SecurityRoot GetSecurityRoot(string key);

    /// <summary>Gets the security root with the specified ID.</summary>
    /// <param name="pageId">The ID of the root.</param>
    /// <returns></returns>
    public abstract SecurityRoot GetSecurityRoot(Guid id);

    /// <summary>Gets a query for security roots.</summary>
    /// <returns></returns>
    public abstract IQueryable<SecurityRoot> GetSecurityRoots();

    /// <summary>Creates new security root with the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public abstract SecurityRoot CreateSecurityRoot(string key);

    /// <summary>
    /// Creates a security root by secifying a key and supported permission sets
    /// </summary>
    /// <param name="key">Security root key name</param>
    /// <param name="supportedPermissionSets">Permission sets that should be supported by the security root</param>
    /// <returns>Security root that has a proper <paramref name="key" /> and <paramref name="supportedPermissionSets" /> </returns>
    public abstract SecurityRoot CreateSecurityRoot(
      string key,
      params string[] supportedPermissionSets);

    /// <summary>Deletes the security root.</summary>
    /// <param name="root">The root.</param>
    public abstract void DeleteSecurityRoot(SecurityRoot root);

    /// <summary>Gets the config.</summary>
    /// <value>The config.</value>
    protected static SecurityConfig Config => SecurityDataProvider.config;

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (SecurityDataProvider);

    /// <summary>
    /// Create a new user action with random pageId that is boud to this provider's application
    /// </summary>
    /// <returns>New user action</returns>
    public abstract UserAction CreateUserAction();

    /// <summary>
    /// Create a new user action with <paramref name="pageId" />. The action is bound to the
    /// current provider's application
    /// </summary>
    /// <param name="pageId">Id to set to the user action</param>
    /// <returns>New user action</returns>
    public abstract UserAction CreateUserAction(Guid id);

    /// <summary>
    /// Mark an <paramref name="action" /> for deletion
    /// </summary>
    /// <param name="action">User action to be marked for deletion</param>
    public abstract void DeleteUserAction(UserAction action);

    /// <summary>Get action by pageId</summary>
    /// <param name="pageId">Id of the action to retrieve</param>
    /// <returns></returns>
    public abstract UserAction GetUserAction(Guid id);

    /// <summary>Get query for all user actions</summary>
    /// <returns>Queryable objects containing all user actions</returns>
    public abstract IQueryable<UserAction> GetUserActions();

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Totlal count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item) => throw new NotSupportedException();

    public override object GetItem(Type itemType, Guid id)
    {
      if (!(itemType == typeof (SecurityRoot)))
        return (object) null;
      return (object) (this.GetSecurityRoot(id) ?? throw new ArgumentException("Invalid item type \"{0}\".".Arrange((object) itemType.FullName)));
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      return itemType == typeof (SecurityRoot) ? (object) this.GetSecurityRoot(id) : base.GetItem(itemType, id);
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[2]
    {
      typeof (SecurityRoot),
      typeof (UserAction)
    };
  }
}
