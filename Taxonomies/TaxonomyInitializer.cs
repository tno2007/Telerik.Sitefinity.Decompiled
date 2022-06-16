// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyInitializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Taxonomies.ScheduledTasks;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// Component performing taxonomy related initialization operations.
  /// </summary>
  internal class TaxonomyInitializer
  {
    public void Initialize()
    {
      this.RegisterIoCTypes();
      this.InterceptDataEvents();
      this.RegisterStatisticSupport();
    }

    private void RegisterStatisticSupport() => StatisticCache.Current.RegisterStatisticSupport<TaxonomyStatisticSupport>();

    private void RegisterIoCTypes()
    {
      ObjectFactory.Container.RegisterType<MultisiteTaxonomyGuard>((InjectionMember) new InjectionConstructor(new object[1]
      {
        (object) new InjectionParameter<TaxonomyManager>((TaxonomyManager) null)
      }));
      ObjectFactory.Container.RegisterType<ISiteTaxonomyLinker, SiteTaxonomyLinker>((InjectionMember) new InjectionConstructor(new object[1]
      {
        (object) new InjectionParameter<TaxonomyManager>((TaxonomyManager) null)
      }));
      ObjectFactory.Container.RegisterType<ITaxonomyMultisiteTaskService, TaxonomyMultisiteTaskService>();
      ObjectFactory.Container.RegisterType<ITaxonomiesNamedFilterHandler, TaxonomiesNamedFilterHandler>((InjectionMember) new InjectionConstructor(new object[1]
      {
        (object) new InjectionParameter<TaxonomyManager>((TaxonomyManager) null)
      }));
      if (!ObjectFactory.IsTypeRegistered<ITaxonomyEventInterceptor>())
        ObjectFactory.Container.RegisterType<ITaxonomyEventInterceptor, TaxonomyEventInterceptor>((LifetimeManager) new ContainerControlledLifetimeManager());
      ContainerControlledLifetimeManager controlledLifetimeManager = new ContainerControlledLifetimeManager();
      try
      {
        ObjectFactory.Container.RegisterType<IStructureTransfer, TaxonomiesStructureTransfer>(new TaxonomiesStructureTransfer().Area, (LifetimeManager) controlledLifetimeManager);
      }
      catch
      {
        controlledLifetimeManager.Dispose();
        throw;
      }
      ObjectFactory.Container.RegisterType<IContentTransfer, TaxonomiesContentTransfer>(new TaxonomiesContentTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
    }

    private void InterceptDataEvents() => ObjectFactory.Container.Resolve<ITaxonomyEventInterceptor>().Subscribe();
  }
}
