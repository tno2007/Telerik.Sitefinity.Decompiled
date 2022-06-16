// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.DynamicModulePermissionHolderResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.Unity.Utility;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.DynamicModules
{
  /// <summary>
  /// Resolves the corresponding permission holders for <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> secured objects for specific <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleDataProvider" />.
  /// </summary>
  internal class DynamicModulePermissionHolderResolver : IDynamicModulePermissionHolderResolver
  {
    /// <summary>
    /// Resolves the corresponding permission holder for a given <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> secured object and a given <see cref="!:dynamicModuleDataProvider" />.
    /// The resolved object should hold the permissions for the given secured object but specified for the given <see cref="!:dynamicModuleDataProvider" />.
    /// </summary>
    /// <remarks>
    /// The method tries to find the permission holder (<see cref="!:DynamicContentProvider" />) for the given secured object and if it fails it creates the permission holder and then returns it.
    /// </remarks>
    /// <param name="mainSecuredObject">The secured object for which the permission holder will be resolved.</param>
    /// <param name="moduleBuilderManager">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> from which the <paramref name="mainSecuredObject" /> is fetched.</param>
    /// <param name="dynamicModuleDataProvider">The <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleDataProvider" /> for which the permission holder will be resolved.</param>
    /// <returns>
    /// The object holding the permissions for the given <paramref name="mainSecuredObject" /> and the given <paramref name="dynamicModuleDataProvider" />
    /// </returns>
    public ISecuredObject Resolve(
      ISecuredObject mainSecuredObject,
      ModuleBuilderManager moduleBuilderManager,
      DynamicModuleDataProvider dynamicModuleDataProvider)
    {
      Guard.ArgumentNotNull((object) mainSecuredObject, nameof (mainSecuredObject));
      Guard.ArgumentNotNull((object) moduleBuilderManager, nameof (moduleBuilderManager));
      Guard.ArgumentNotNull((object) dynamicModuleDataProvider, nameof (dynamicModuleDataProvider));
      return DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) moduleBuilderManager, mainSecuredObject, dynamicModuleDataProvider.Name);
    }
  }
}
