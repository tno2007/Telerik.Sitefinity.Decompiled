// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.PermissionsForPrincipalFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  /// <summary>
  /// Can execute principal specific operations on permissions of an <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" />.
  /// </summary>
  public class PermissionsForPrincipalFacade : IPermissionsForPrincipalFacade
  {
    private IList<Guid> principalIds;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Permissions.PermissionsForPrincipalFacade" /> class.
    /// </summary>
    public PermissionsForPrincipalFacade()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Permissions.PermissionsForPrincipalFacade" /> class.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    [InjectionConstructor]
    public PermissionsForPrincipalFacade(ISecuredObject securedObject) => this.SecuredItem = securedObject;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Permissions.PermissionsForPrincipalFacade" /> class.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="principalId">The principal id.</param>
    public PermissionsForPrincipalFacade(ISecuredObject securedObject, Guid principalId)
      : this(securedObject)
    {
      this.PrincipalIds.Add(principalId);
    }

    /// <summary>
    /// Use this method if you want to grant a specific action on the configured secured object
    /// for the already specified principal (user or role).
    /// </summary>
    /// <remarks>
    /// If a specific action is both granted and denied, the deny action will take precedents.
    /// </remarks>
    /// <returns>An facade from which the action to grant is chosen.</returns>
    public IActionsFacade Grant()
    {
      this.BreakInheritance();
      return this.GetFacade(nameof (Grant));
    }

    /// <summary>
    /// Use this method if you want to deny a specific action on the configured secured object
    /// for the already specified principal (user or role).
    /// </summary>
    /// <remarks>
    /// If a specific action is both granted and denied the deny will take precedents.
    /// </remarks>
    /// <returns>An facade from which the action to deny is chosen.</returns>
    public IActionsFacade Deny()
    {
      this.BreakInheritance();
      return this.GetFacade(nameof (Deny));
    }

    private IActionsFacade GetFacade(string facadeName)
    {
      IActionsFacade facade = ObjectFactory.Container.Resolve<IActionsFacade>(facadeName, (ResolverOverride) new ParameterOverride("securedObject", (object) this.SecuredItem));
      facade.SecuredItem = this.SecuredItem;
      facade.PrincipalIds = this.PrincipalIds;
      return facade;
    }

    private void BreakInheritance()
    {
      if (!this.PrincipalIds.Any<Guid>())
        throw new InvalidOperationException("No principals configured.");
      ObjectFactory.Resolve<PermissionInheritanceManagementFacade>().BreakInheritance(this.SecuredItem);
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> item which the facade is using.
    /// </summary>
    /// <value>The securable item that the facade is operating on.</value>
    public ISecuredObject SecuredItem { get; set; }

    /// <summary>
    /// Gets or sets the list of principal ids for which the corresponding permissions will be modified.
    /// </summary>
    /// <value>The principal ids.</value>
    public IList<Guid> PrincipalIds
    {
      get
      {
        if (this.principalIds == null)
          this.principalIds = (IList<Guid>) new List<Guid>();
        return this.principalIds;
      }
      set => this.principalIds = value;
    }
  }
}
