// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.PropertyWrapperCachingFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Data.DataProcessing.Helpers;
using Telerik.Sitefinity.Processors;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Web.Services.Contracts;

namespace Telerik.Sitefinity.Data.DataProcessing
{
  /// <summary>
  /// Pseudo singleton instance that gets reset upon certain events and soft restart of Sitefinity.
  /// It is used for caching <see cref="T:Telerik.Sitefinity.Data.DataProcessing.Helpers.PropertyWrapper" /> that contain <see cref="T:Telerik.Sitefinity.Data.DataProcessing.Processors.IDataProcessor" /> for particular <see cref="T:System.Type" />
  /// </summary>
  internal class PropertyWrapperCachingFactory : IPropertyWrapperFactory
  {
    private static Lazy<PropertyWrapperCachingFactory> lazy = new Lazy<PropertyWrapperCachingFactory>((Func<PropertyWrapperCachingFactory>) (() => new PropertyWrapperCachingFactory()));
    private readonly Dictionary<string, IEnumerable<PropertyWrapper>> internalCache = new Dictionary<string, IEnumerable<PropertyWrapper>>();
    private object cacheLock = new object();

    private PropertyWrapperCachingFactory()
    {
      EventHub.Subscribe<ProcessorFactoryCacheResetEvent>(new SitefinityEventHandler<ProcessorFactoryCacheResetEvent>(this.OnProcessorFactoryResetCache));
      SystemManager.Shutdown += new EventHandler<EventArgs>(this.OnSystemShutdown);
      TypeSettingsProviderRepo.OnModelChange += new EventHandler<EventArgs>(this.ServiceContractProviderRepo_OnModelChange);
    }

    public static PropertyWrapperCachingFactory Instance => PropertyWrapperCachingFactory.lazy.Value;

    private void OnSystemShutdown(object sender, EventArgs e)
    {
      EventHub.Unsubscribe<ProcessorFactoryCacheResetEvent>(new SitefinityEventHandler<ProcessorFactoryCacheResetEvent>(this.OnProcessorFactoryResetCache));
      SystemManager.Shutdown -= new EventHandler<EventArgs>(this.OnSystemShutdown);
      TypeSettingsProviderRepo.OnModelChange -= new EventHandler<EventArgs>(this.ServiceContractProviderRepo_OnModelChange);
      PropertyWrapperCachingFactory.lazy = new Lazy<PropertyWrapperCachingFactory>((Func<PropertyWrapperCachingFactory>) (() => new PropertyWrapperCachingFactory()));
    }

    private void OnProcessorFactoryResetCache(ProcessorFactoryCacheResetEvent e)
    {
      lock (this.cacheLock)
        this.internalCache.Clear();
    }

    public IEnumerable<PropertyWrapper> GetPropertyWrappers(object item)
    {
      Type type = item.GetType();
      IEnumerable<PropertyWrapper> propertyWrappers;
      if (!this.internalCache.TryGetValue(this.GetCacheKey(type), out propertyWrappers))
      {
        lock (this.cacheLock)
        {
          if (!this.internalCache.TryGetValue(this.GetCacheKey(type), out propertyWrappers))
          {
            propertyWrappers = new PropertyWrapperFactory().GetPropertyWrappers(item);
            this.internalCache.Add(this.GetCacheKey(type), propertyWrappers);
          }
        }
      }
      return propertyWrappers;
    }

    private void ServiceContractProviderRepo_OnModelChange(object sender, EventArgs e)
    {
      lock (this.cacheLock)
        this.internalCache.Clear();
    }

    private string GetCacheKey(Type type) => type.FullName;
  }
}
