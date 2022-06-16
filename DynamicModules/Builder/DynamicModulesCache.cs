// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.DynamicModulesCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.OpenAccess.FetchOptimization;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Proxy;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>
  /// This class represents holds all the meta information regarding the dynamic modules
  /// </summary>
  public class DynamicModulesCache : IEnumerable<IDynamicModule>, IEnumerable
  {
    private bool indexBuilt;
    private List<IDynamicModule> modulesList = new List<IDynamicModule>();
    private Dictionary<Guid, IDynamicModuleType> typeByIdIndex;
    private Dictionary<string, IDynamicModuleType> typeByFullNameIndex;
    private object indexLock = new object();
    private List<DynamicContentProviderProxy> securedObjectsList = new List<DynamicContentProviderProxy>();

    internal DynamicModulesCache(ModuleBuilderManager manager)
    {
      FetchStrategy fetchStrategy1 = (FetchStrategy) null;
      if (manager.Provider.GetTransaction() is SitefinityOAContext transaction)
      {
        fetchStrategy1 = transaction.FetchStrategy;
        FetchStrategy fetchStrategy2 = new FetchStrategy();
        fetchStrategy2.LoadWith<DynamicModule>((Expression<Func<DynamicModule, object>>) (m => m.Permissions));
        fetchStrategy2.LoadWith<DynamicModuleType>((Expression<Func<DynamicModuleType, object>>) (t => t.Permissions));
        fetchStrategy2.LoadWith<DynamicModuleField>((Expression<Func<DynamicModuleField, object>>) (f => f.Permissions));
        transaction.FetchStrategy = fetchStrategy2;
      }
      DynamicModule[] array = manager.GetDynamicModules().Include<DynamicModule>((Expression<Func<DynamicModule, object>>) (m => m.Permissions)).ToArray<DynamicModule>();
      manager.LoadDynamicModulesGraph(array, true);
      foreach (DynamicModule source in array)
        this.modulesList.Add((IDynamicModule) new DynamicModuleProxy(source));
      this.securedObjectsList = manager.GetDynamicContentProviders().Select<DynamicContentProvider, DynamicContentProviderProxy>((Expression<Func<DynamicContentProvider, DynamicContentProviderProxy>>) (x => new DynamicContentProviderProxy(x))).ToList<DynamicContentProviderProxy>();
      if (fetchStrategy1 == null)
        return;
      transaction.FetchStrategy = fetchStrategy1;
    }

    internal IDynamicModuleType GetTypeById(Guid typeId)
    {
      this.EnsureIndex();
      return this.typeByIdIndex[typeId];
    }

    internal IEnumerable<IDynamicModuleType> AllTypes()
    {
      this.EnsureIndex();
      return (IEnumerable<IDynamicModuleType>) this.typeByIdIndex.Values;
    }

    internal IDynamicModuleType GetTypeByFullName(string typeFullName)
    {
      this.EnsureIndex();
      IDynamicModuleType typeByFullName = (IDynamicModuleType) null;
      this.typeByFullNameIndex.TryGetValue(typeFullName, out typeByFullName);
      return typeByFullName;
    }

    internal Dictionary<string, ISecuredObject> GetSecuretObjectsByType(
      Guid typeId)
    {
      Dictionary<string, ISecuredObject> securetObjectsByType = new Dictionary<string, ISecuredObject>();
      foreach (DynamicContentProviderProxy contentProviderProxy in this.securedObjectsList.Where<DynamicContentProviderProxy>((Func<DynamicContentProviderProxy, bool>) (dc => dc.ParentSecuredObjectType == typeof (DynamicModuleType).FullName && dc.ParentSecuredObjectId == typeId)))
        securetObjectsByType.Add(contentProviderProxy.Name, (ISecuredObject) contentProviderProxy);
      return securetObjectsByType;
    }

    internal Dictionary<string, ISecuredObject> GetSecuretObjectsByModule(
      Guid moduleId)
    {
      Dictionary<string, ISecuredObject> securetObjectsByModule = new Dictionary<string, ISecuredObject>();
      foreach (DynamicContentProviderProxy contentProviderProxy in this.securedObjectsList.Where<DynamicContentProviderProxy>((Func<DynamicContentProviderProxy, bool>) (dc => dc.ParentSecuredObjectType == typeof (DynamicModule).FullName && dc.ParentSecuredObjectId == moduleId)))
        securetObjectsByModule.Add(contentProviderProxy.Name, (ISecuredObject) contentProviderProxy);
      return securetObjectsByModule;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator</returns>
    public IEnumerator<IDynamicModule> GetEnumerator() => (IEnumerator<IDynamicModule>) this.modulesList.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    internal IEnumerable<IDynamicModule> Active() => this.Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Status == DynamicModuleStatus.Active));

    internal IEnumerable<IDynamicModule> NotInstalled() => this.Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Status == DynamicModuleStatus.NotInstalled));

    private void EnsureIndex()
    {
      if (this.indexBuilt)
        return;
      lock (this.indexLock)
      {
        if (this.indexBuilt)
          return;
        this.typeByIdIndex = new Dictionary<Guid, IDynamicModuleType>();
        this.typeByFullNameIndex = new Dictionary<string, IDynamicModuleType>();
        foreach (IDynamicModule modules in this.modulesList)
        {
          foreach (IDynamicModuleType type in modules.Types)
          {
            this.typeByIdIndex.Add(type.Id, type);
            this.typeByFullNameIndex.Add(type.GetFullTypeName(), type);
          }
        }
        this.indexBuilt = true;
      }
    }
  }
}
