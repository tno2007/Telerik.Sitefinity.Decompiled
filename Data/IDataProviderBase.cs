// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IDataProviderBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Data
{
  /// <summary>Interface for data providers.</summary>
  public interface IDataProviderBase : IDisposable, ICloneable, IDataProviderEventsCall
  {
    /// <summary>
    /// Gets the friendly name used to refer to the provider during configuration.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets a brief, friendly description suitable for display in administrative tools or other user interfaces (UIs).
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets a value indicating whether database operations are temporary suspended.
    /// Database might be suspended for maintenance or schema updates.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if database operations are suspended; otherwise, <c>false</c>.
    /// </value>
    bool Suspended { get; }

    /// <summary>Gets a query for permissions.</summary>
    /// <typeparam name="TPermission">The type of the permission.</typeparam>
    /// <returns></returns>
    IQueryable<TPermission> GetPermissions<TPermission>() where TPermission : Permission;

    /// <summary>Gets a query for permissions.</summary>
    /// <param name="actualType">The actual type.</param>
    /// <returns></returns>
    IQueryable<Permission> GetPermissions(Type actualType);

    /// <summary>
    /// Gets a queryable collection of permissions inheritance maps .
    /// </summary>
    /// <returns>A queryable collection of permissions inheritance maps .</returns>
    IQueryable<PermissionsInheritanceMap> GetInheritanceMaps();

    /// <summary>Creates a permissions inheritance map object.</summary>
    /// <param name="objectId"></param>
    /// <param name="childObjectId"></param>
    /// <param name="childObjectTypeName"></param>
    /// <returns></returns>
    PermissionsInheritanceMap CreatePermissionsInheritanceMap(
      Guid objectId,
      Guid childObjectId,
      string childObjectTypeName);

    /// <summary>Deletes a permissions inheritance map.</summary>
    /// <param name="permissionsInheritanceMap">The permissions inheritance map to delete.</param>
    void DeletePermissionsInheritanceMap(
      PermissionsInheritanceMap permissionsInheritanceMap);

    /// <summary>Gets permission children for the given parent id.</summary>
    /// <returns></returns>
    IQueryable<PermissionsInheritanceMap> GetPermissionChildren(
      Guid parentId);

    /// <summary>
    /// Gets or sets the provider abilities for the current principal. Gets the provider abilities. E.g. which operations are supported and allowed.
    /// </summary>
    /// <value>The provider abilities.</value>
    ProviderAbilities Abilities { get; }

    /// <summary>Gets the provider abilities for a user.</summary>
    /// <param name="userID">The user ID.</param>
    /// <returns></returns>
    ProviderAbilities GetAbilitiesForUser(Guid userID);
  }
}
