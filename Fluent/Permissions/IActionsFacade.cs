// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.IActionsFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  /// <summary>
  /// Defines the available actions in the General permission set.
  /// Inheritors can grant or deny the chosen action.
  /// </summary>
  public interface IActionsFacade
  {
    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> item which the facade is using.
    /// </summary>
    /// <value>The securable item that the facade is operating on.</value>
    ISecuredObject SecuredItem { get; set; }

    /// <summary>Gets or sets the principal ids.</summary>
    /// <value>The principal ids.</value>
    IList<Guid> PrincipalIds { get; set; }

    /// <summary>
    /// Depending on the implementation grants or denies the view action
    /// for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade View();

    /// <summary>
    /// Grants or denies create action for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    /// <remarks>Create action only applies to parent secured objects(e.g. dynamic content type) to define the child object behavior.</remarks>
    IPermissionsForPrincipalFacade Create();

    /// <summary>
    /// Depending on the implementation grants or denies the modify action
    /// for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade Modify();

    /// <summary>
    /// Depending on the implementation grants or denies the delete action
    /// for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade Delete();

    /// <summary>
    /// Depending on the implementation grants or denies the change owner action
    /// for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ChangeOwner();

    /// <summary>
    /// Depending on the implementation grants or denies the change permissions action
    /// for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ChangePermissions();
  }
}
