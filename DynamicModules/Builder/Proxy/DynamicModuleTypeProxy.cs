// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Proxy.DynamicModuleTypeProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;

namespace Telerik.Sitefinity.DynamicModules.Builder.Proxy
{
  internal class DynamicModuleTypeProxy : 
    IDynamicModuleType,
    IMetaType,
    ISecuredObject,
    IDynamicSecuredObject
  {
    private readonly SecuredProxy securedProxy;
    private IDynamicModuleType parentType;

    public DynamicModuleTypeProxy(DynamicModuleProxy module, DynamicModuleType source)
    {
      this.Module = module;
      this.Id = source.Id;
      this.DisplayName = source.DisplayName;
      this.Sections = (IEnumerable<IFieldsBackendSection>) ((IEnumerable<FieldsBackendSection>) source.Sections).Select<FieldsBackendSection, FieldsBackendSectionProxy>((Func<FieldsBackendSection, FieldsBackendSectionProxy>) (s => new FieldsBackendSectionProxy(this, s))).ToArray<FieldsBackendSectionProxy>();
      this.Fields = (IEnumerable<IDynamicModuleField>) ((IEnumerable<DynamicModuleField>) source.Fields).Select<DynamicModuleField, DynamicModuleFieldProxy>((Func<DynamicModuleField, DynamicModuleFieldProxy>) (f => new DynamicModuleFieldProxy(this, f))).ToArray<DynamicModuleFieldProxy>();
      this.IsSelfReferencing = source.IsSelfReferencing;
      this.MainShortTextFieldName = source.MainShortTextFieldName;
      this.PageId = source.PageId;
      this.ParentTypeId = source.ParentModuleTypeId;
      this.CheckFieldPermissions = source.CheckFieldPermissions;
      this.Owner = source.Owner;
      this.TypeName = source.TypeName;
      this.TypeNamespace = source.TypeNamespace;
      this.securedProxy = new SecuredProxy((ISecuredObject) source);
    }

    public DynamicModuleProxy Module { get; private set; }

    public Guid Id { get; private set; }

    public IEnumerable<IDynamicModuleField> Fields { get; private set; }

    public string DisplayName { get; private set; }

    public IEnumerable<IFieldsBackendSection> Sections { get; private set; }

    public bool IsSelfReferencing { get; private set; }

    public string MainShortTextFieldName { get; private set; }

    public Guid PageId { get; private set; }

    public Guid ParentTypeId { get; private set; }

    public bool CheckFieldPermissions { get; private set; }

    public string ModuleName => this.Module.Name;

    public Guid Owner { get; private set; }

    public string TypeName { get; private set; }

    public string TypeNamespace { get; private set; }

    Guid IDynamicModuleType.ModuleId => this.Module.Id;

    public IDynamicModuleType ParentType
    {
      get
      {
        if (this.ParentTypeId == Guid.Empty)
          return (IDynamicModuleType) null;
        if (this.parentType == null)
          this.parentType = this.Module.Types.Single<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.Id == this.ParentTypeId));
        return this.parentType;
      }
    }

    string IMetaType.FullTypeName => this.GetFullTypeName();

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

    string IDynamicSecuredObject.GetSecurityActionTitle(string actionTitle) => string.Format(actionTitle, (object) this.DisplayName.ToLower());

    string IDynamicSecuredObject.GetTitle() => this.DisplayName.ToLower();

    /// <inheritdoc />
    string IDynamicSecuredObject.GetModuleName() => this.ModuleName;

    public string GetFullTypeName() => this.TypeNamespace + "." + this.TypeName;
  }
}
