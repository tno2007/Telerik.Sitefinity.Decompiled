// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.EmptyPermissionsForPrincipalFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  internal class EmptyPermissionsForPrincipalFacade : IPermissionsForPrincipalFacade
  {
    /// <summary>
    /// Use this method if you want to grant a specific action on the configured secured object
    /// for the already specified principal (user or role).
    /// </summary>
    /// <remarks>
    /// If a specific action is both granted and denied, the deny action will take precedents.
    /// </remarks>
    /// <returns>An facade from which the action to grant is chosen.</returns>
    public IActionsFacade Grant() => this.GetEmptyActionsFacade();

    /// <summary>
    /// Use this method if you want to deny a specific action on the configured secured object
    /// for the already specified principal (user or role).
    /// </summary>
    /// <remarks>
    /// If a specific action is both granted and denied the deny will take precedents.
    /// </remarks>
    /// <returns>An facade from which the action to deny is chosen.</returns>
    public IActionsFacade Deny() => this.GetEmptyActionsFacade();

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
      get => (IList<Guid>) new List<Guid>();
      set
      {
      }
    }

    private IActionsFacade GetEmptyActionsFacade() => ObjectFactory.Resolve<IActionsFacade>("Empty");
  }
}
