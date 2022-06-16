// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.DynamicContentFluent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Fluent.Permissions;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// A common facade for all <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> fluent APIs.
  /// </summary>
  public class DynamicContentFluent
  {
    private bool enabled = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicContentFluent" /> class.
    /// </summary>
    /// <param name="item">The item.</param>
    public DynamicContentFluent(DynamicContent item) => this.SecuredItem = item;

    /// <summary>
    /// Exposes permissions related operations with the specified <paramref name="dynamicContent" />.
    /// </summary>
    /// <returns>A facade for permissions related operations.</returns>
    public IPermissionsFacade ManagePermissions()
    {
      if (!this.Enabled)
        return ObjectFactory.Resolve<IPermissionsFacade>("Empty");
      IPermissionsFacade permissionsFacade = ObjectFactory.Container.Resolve<IPermissionsFacade>((ResolverOverride) new ParameterOverride("securedObject", (object) this.SecuredItem));
      permissionsFacade.SecuredItem = (ISecuredObject) this.SecuredItem;
      return permissionsFacade;
    }

    /// <summary>
    /// Gets or sets the underlying item that is used by this facade.
    /// </summary>
    /// <value>The item to use.</value>
    public DynamicContent SecuredItem { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the facade will perform actions.
    /// Useful when a condition hasn't been met and from there on, all actions
    /// that have been ordered from this or child facades, should not be executed.
    /// </summary>
    /// <value><c>false</c> if no actions should be executed by the child facades, otherwise <c>true</c>.</value>
    public bool Enabled
    {
      get => this.enabled;
      set => this.enabled = value;
    }
  }
}
