// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Proxy.DynamicContentProviderProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;

namespace Telerik.Sitefinity.DynamicModules.Builder.Proxy
{
  internal class DynamicContentProviderProxy : ISecuredObject
  {
    private readonly SecuredProxy securedProxy;

    public DynamicContentProviderProxy(DynamicContentProvider source)
    {
      this.Id = source.Id;
      this.Name = source.Name;
      this.ParentSecuredObjectId = source.ParentSecuredObjectId;
      this.ParentSecuredObjectType = source.ParentSecuredObjectType;
      this.securedProxy = new SecuredProxy((ISecuredObject) source);
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Guid ParentSecuredObjectId { get; private set; }

    public string ParentSecuredObjectType { get; private set; }

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
  }
}
