// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.IPermissionsForPrincipalFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  /// <summary>
  /// Defines principal specific operations on permissions of an <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" />.
  /// </summary>
  public interface IPermissionsForPrincipalFacade
  {
    /// <summary>
    /// Use this method if you want to grant a specific action on the configured secured object
    /// for the already specified principal (user or role).
    /// </summary>
    /// <remarks>
    /// If a specific action is both granted and denied the deny will take precedents.
    /// </remarks>
    /// <returns>An facade from which the action to grant is chosen.</returns>
    IActionsFacade Grant();

    /// <summary>
    /// Use this method if you want to deny a specific action on the configured secured object
    /// for the already specified principal (user or role).
    /// </summary>
    /// <remarks>
    /// If a specific action is both granted and denied the deny will take precedents.
    /// </remarks>
    /// <returns>An facade from which the action to deny is chosen.</returns>
    IActionsFacade Deny();

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> item which the facade is using.
    /// </summary>
    /// <value>The securable item that the facade is operating on.</value>
    ISecuredObject SecuredItem { get; set; }

    /// <summary>
    /// Gets or sets the list of principal ids for which the corresponding permissions will be modified.
    /// </summary>
    /// <value>The principal ids.</value>
    IList<Guid> PrincipalIds { get; set; }
  }
}
