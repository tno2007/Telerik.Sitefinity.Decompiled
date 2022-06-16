// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.System.SystemOperatonsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.System
{
  internal class SystemOperatonsProvider : IOperationProvider
  {
    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if ((!(clrType != (Type) null) ? 0 : (clrType.IsDefined(typeof (ManagerTypeAttribute), true) ? 1 : (clrType.IsDefined(typeof (InheritableManagerTypeAttribute), true) ? 1 : 0))) == 0)
        return Enumerable.Empty<OperationData>();
      OperationData operationData = OperationData.Create<IEnumerable<DataProviderModel>>(new Func<OperationContext, IEnumerable<DataProviderModel>>(this.SfProviders));
      operationData.OperationType = OperationType.Collection;
      return (IEnumerable<OperationData>) new OperationData[1]
      {
        operationData
      };
    }

    private IEnumerable<DataProviderModel> SfProviders(
      OperationContext context)
    {
      Type type = (Type) context.Data["clrType"];
      return ((IEnumerable<object>) type.GetCustomAttributes(true)).Select<object, Type>((Func<object, Type>) (a => a.GetType())).Any<Type>((Func<Type, bool>) (t => t == typeof (InheritableManagerTypeAttribute) || t == typeof (ManagerTypeAttribute))) ? this.GetProvidersForContentType(type) : Enumerable.Empty<DataProviderModel>();
    }

    private IEnumerable<DataProviderModel> GetProvidersForContentType(
      Type type)
    {
      IManager mappedManager = ManagerBase.GetMappedManager(type.FullName);
      IEnumerable<DataProviderBase> source = Enumerable.Empty<DataProviderBase>();
      string defaultProviderName = (string) null;
      Dictionary<string, ISecuredObject> securityRoots = new Dictionary<string, ISecuredObject>();
      IDynamicModuleType moduleType = (IDynamicModuleType) null;
      if (mappedManager is DynamicModuleManager)
      {
        DynamicModulesCache modules = ModuleBuilderManager.GetModules();
        IDynamicModule dynamicModule = modules.Single<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Types.Any<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.FullTypeName == type.FullName))));
        moduleType = modules.GetTypeByFullName(type.FullName);
        string name = dynamicModule.Name;
        source = ((DynamicModuleManager) mappedManager).GetContextProviders(name);
        defaultProviderName = DynamicModuleManager.GetDefaultProviderName(name);
        securityRoots = modules.GetSecuretObjectsByType(moduleType.Id);
      }
      else if (mappedManager != null)
      {
        IManager manager = mappedManager;
        defaultProviderName = manager.GetDefaultContextProvider().Name;
        source = manager.GetContextProviders();
      }
      return source.Where<DataProviderBase>((Func<DataProviderBase, bool>) (p =>
      {
        ISecuredObject secObj;
        if (!securityRoots.TryGetValue(p.Name, out secObj))
          secObj = moduleType == null ? p.SecurityRoot : (ISecuredObject) moduleType;
        if ((!secObj.IsSecurityActionSupported(SecurityActionTypes.View) ? 0 : (secObj.IsSecurityActionTypeGranted(SecurityActionTypes.View) ? 1 : 0)) != 0)
          return true;
        string permissionSet = ((IEnumerable<string>) secObj.SupportedPermissionSets).FirstOrDefault<string>((Func<string, bool>) (s => s.EndsWith("SitemapGeneration")));
        if (string.IsNullOrEmpty(permissionSet))
          return true;
        return secObj.IsGranted(permissionSet, "ViewBackendLink");
      })).Where<DataProviderBase>((Func<DataProviderBase, bool>) (p => !p.IsSystemProvider())).Select<DataProviderBase, DataProviderModel>((Func<DataProviderBase, DataProviderModel>) (p => new DataProviderModel()
      {
        Name = p.Name,
        Title = p.Title,
        IsDefault = p.Name == defaultProviderName
      }));
    }
  }
}
