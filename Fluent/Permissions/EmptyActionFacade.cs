// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.EmptyActionFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  internal class EmptyActionFacade : IActionsFacade
  {
    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> item which the facade is using.
    /// </summary>
    /// <value>The securable item that the facade is operating on.</value>
    public ISecuredObject SecuredItem
    {
      get => (ISecuredObject) null;
      set
      {
      }
    }

    /// <summary>Gets or sets the principal id.</summary>
    /// <value>The principal id.</value>
    public IList<Guid> PrincipalIds
    {
      get => (IList<Guid>) null;
      set
      {
      }
    }

    /// <summary>Grants view action for the configured principals.</summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade View() => this.GetEmptyPermissionsForPrincipalFacade();

    /// <summary>
    /// Grants or denies create action for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    /// <remarks>Create action only applies to parent secured objects(e.g. dynamic content type) to define the child object behavior.</remarks>
    public IPermissionsForPrincipalFacade Create() => this.GetEmptyPermissionsForPrincipalFacade();

    /// <summary>Grants modify action for the configured principals.</summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade Modify() => this.GetEmptyPermissionsForPrincipalFacade();

    /// <summary>Grants delete action for the configured principals.</summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade Delete() => this.GetEmptyPermissionsForPrincipalFacade();

    /// <summary>
    /// Grants 'change owner' action for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ChangeOwner() => this.GetEmptyPermissionsForPrincipalFacade();

    /// <summary>
    /// Grants 'change permissions' action for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ChangePermissions() => this.GetEmptyPermissionsForPrincipalFacade();

    private IPermissionsForPrincipalFacade GetEmptyPermissionsForPrincipalFacade() => ObjectFactory.Resolve<IPermissionsForPrincipalFacade>("Empty");
  }
}
