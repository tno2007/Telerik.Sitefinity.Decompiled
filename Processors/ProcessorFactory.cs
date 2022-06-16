// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.ProcessorFactory`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Processors;
using Telerik.Sitefinity.Processors.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.DataProcessing
{
  /// <summary>Base class for processors factory</summary>
  /// <typeparam name="TConfig">The config where the processors definitions are stored.</typeparam>
  /// <typeparam name="TProcessor">The type of the processor</typeparam>
  internal class ProcessorFactory<TConfig, TProcessor>
    where TConfig : ConfigSection, IHaveConfigProcessors, new()
    where TProcessor : class, IProcessor
  {
    private static Lazy<ProcessorFactory<TConfig, TProcessor>> lazy = new Lazy<ProcessorFactory<TConfig, TProcessor>>((Func<ProcessorFactory<TConfig, TProcessor>>) (() => new ProcessorFactory<TConfig, TProcessor>()));
    private const string ProcessorFactoryCacheBaseKey = "ProcessorFactoryCacheBaseKey";
    private readonly Dictionary<string, IEnumerable<TProcessor>> internalCache = new Dictionary<string, IEnumerable<TProcessor>>();
    private readonly object cacheLock = new object();

    private ProcessorFactory()
    {
      SystemManager.Shutdown += new EventHandler<EventArgs>(this.SystemManager_Shutdown);
      CacheDependency.Subscribe(typeof (TConfig), new ChangedCallback(this.ConfigChangedCallback));
    }

    public static ProcessorFactory<TConfig, TProcessor> Instance => ProcessorFactory<TConfig, TProcessor>.lazy.Value;

    private void SystemManager_Shutdown(object sender, EventArgs e)
    {
      SystemManager.Shutdown -= new EventHandler<EventArgs>(this.SystemManager_Shutdown);
      CacheDependency.Unsubscribe(typeof (TConfig), new ChangedCallback(this.ConfigChangedCallback));
      ProcessorFactory<TConfig, TProcessor>.lazy = new Lazy<ProcessorFactory<TConfig, TProcessor>>((Func<ProcessorFactory<TConfig, TProcessor>>) (() => new ProcessorFactory<TConfig, TProcessor>()));
    }

    private void ConfigChangedCallback(
      ICacheDependencyHandler caller,
      Type trackedItemType,
      string trackedItemKey)
    {
      lock (this.cacheLock)
      {
        this.internalCache.Clear();
        EventHub.Raise((IEvent) new ProcessorFactoryCacheResetEvent());
      }
    }

    public IEnumerable<TProcessor> GetProcessors()
    {
      IEnumerable<TProcessor> processors1;
      if (!this.internalCache.TryGetValue("ProcessorFactoryCacheBaseKey", out processors1))
      {
        lock (this.cacheLock)
        {
          if (!this.internalCache.TryGetValue("ProcessorFactoryCacheBaseKey", out processors1))
          {
            ConfigElementDictionary<string, ProcessorConfigElement> processorConfigElements = this.GetProcessorConfigElements();
            List<TProcessor> processors2 = new List<TProcessor>();
            foreach (ProcessorConfigElement instanceElement in (IEnumerable<ProcessorConfigElement>) processorConfigElements.Values)
            {
              try
              {
                if (instanceElement.Enabled)
                {
                  IProcessor instance = this.CreateInstance(instanceElement);
                  if (instance is TProcessor)
                    processors2.Add(instance as TProcessor);
                }
              }
              catch (Exception ex)
              {
                Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);
              }
            }
            this.internalCache.Add("ProcessorFactoryCacheBaseKey", (IEnumerable<TProcessor>) processors2);
            return (IEnumerable<TProcessor>) processors2;
          }
        }
      }
      return processors1;
    }

    protected internal virtual ConfigElementDictionary<string, ProcessorConfigElement> GetProcessorConfigElements() => Config.Get<TConfig>().GetConfigProcessors();

    protected internal IProcessor GetInstanceFromType(string type) => (IProcessor) ObjectFactory.Resolve(TypeResolutionService.ResolveType(type, true));

    private IProcessor CreateInstance(ProcessorConfigElement instanceElement)
    {
      IProcessor instanceFromType = this.GetInstanceFromType(instanceElement.Type);
      instanceFromType.Initialize(instanceElement.Name, new NameValueCollection(instanceElement.Parameters));
      return instanceFromType;
    }
  }
}
