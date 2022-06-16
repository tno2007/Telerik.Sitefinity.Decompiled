// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Proxy.DynamicModuleProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;

namespace Telerik.Sitefinity.DynamicModules.Builder.Proxy
{
  internal class DynamicModuleProxy : IDynamicModule, ISecuredObject, IDynamicSecuredObject
  {
    private readonly SecuredProxy securedProxy;

    public DynamicModuleProxy(DynamicModule source)
    {
      this.Name = source.Name;
      this.Id = source.Id;
      this.Status = source.Status;
      this.Title = source.Title;
      this.DefaultBackendDefinitionName = source.DefaultBackendDefinitionName;
      this.Description = source.Description;
      this.UrlName = source.UrlName;
      this.Owner = source.Owner;
      this.securedProxy = new SecuredProxy((ISecuredObject) source);
      this.LastModified = source.LastModified;
      this.Types = (IEnumerable<IDynamicModuleType>) ((IEnumerable<DynamicModuleType>) source.Types).Select<DynamicModuleType, DynamicModuleTypeProxy>((Func<DynamicModuleType, DynamicModuleTypeProxy>) (t => new DynamicModuleTypeProxy(this, t))).ToArray<DynamicModuleTypeProxy>();
    }

    public string Name { get; private set; }

    public Guid Id { get; private set; }

    public DynamicModuleStatus Status { get; private set; }

    public IEnumerable<IDynamicModuleType> Types { get; private set; }

    public string DefaultBackendDefinitionName { get; private set; }

    public string Description { get; private set; }

    public Guid Owner { get; private set; }

    public Guid PageId { get; private set; }

    public string Title { get; private set; }

    public string UrlName { get; private set; }

    public DateTime LastModified { get; private set; }

    public bool InheritsPermissions
    {
      get => this.securedProxy.InheritsPermissions;
      set => this.securedProxy.InheritsPermissions = value;
    }

    public bool CanInheritPermissions
    {
      get => this.securedProxy.CanInheritPermissions;
      set => this.securedProxy.CanInheritPermissions = value;
    }

    public IList<Permission> Permissions => this.securedProxy.Permissions;

    public string[] SupportedPermissionSets
    {
      get => this.securedProxy.SupportedPermissionSets;
      set => this.securedProxy.SupportedPermissionSets = value;
    }

    public IDictionary<string, string> PermissionsetObjectTitleResKeys
    {
      get => this.securedProxy.PermissionsetObjectTitleResKeys;
      set => this.securedProxy.PermissionsetObjectTitleResKeys = value;
    }

    string IDynamicSecuredObject.GetSecurityActionTitle(string actionTitle) => string.Format(actionTitle, (object) this.Title.ToLower());

    string IDynamicSecuredObject.GetTitle() => this.Title.ToLower();

    /// <inheritdoc />
    string IDynamicSecuredObject.GetModuleName() => this.Name;
  }
}
