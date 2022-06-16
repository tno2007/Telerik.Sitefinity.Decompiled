// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigElementLazyItem`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Configuration
{
  public class ConfigElementLazyItem<TElement> : ConfigElementItem<TElement>, ILazyConfigElementItem
    where TElement : ConfigElement
  {
    protected ConfigElement parent;
    protected Func<TElement> initializer;
    protected Lazy<TElement> lazyElement;
    protected ExpandoObject lazyProperties;
    private IList<ConfigSource> sources;

    public override bool IsLazy => true;

    public override bool IsLoaded => (object) this.element != null || this.lazyElement.IsValueCreated;

    public override TElement Element
    {
      get
      {
        if ((object) this.element != null)
          return this.element;
        if (!this.IsLoaded)
        {
          TElement element = this.lazyElement.Value;
          if ((object) element != null)
          {
            element.Parent = this.parent;
            element.TagName = this.key;
            Type type = element.GetType();
            foreach (KeyValuePair<string, object> lazyProperty in (IEnumerable<KeyValuePair<string, object>>) this.lazyProperties)
              type.GetProperty(lazyProperty.Key).SetValue((object) element, lazyProperty.Value, (object[]) null);
          }
        }
        return this.lazyElement.Value;
      }
      protected internal set => this.element = value;
    }

    public override object ElementProperties => (object) this.element == null ? (object) this.lazyProperties : (object) this.element;

    public ConfigElementLazyItem(ConfigElement parent, string key, Func<TElement> initializer)
    {
      ConfigElementLazyItem<TElement> configElementLazyItem = this;
      this.key = key;
      this.parent = parent;
      this.initializer = (Func<TElement>) (() => configElementLazyItem.InitializerWraper((Func<ConfigElement>) initializer));
      this.Reset();
    }

    public override void Unload() => this.Reset();

    public override void Unload(Func<ConfigElement> newLoader) => this.Reset((Func<TElement>) (() => this.InitializerWraper(newLoader)));

    internal void Load()
    {
      TElement element = this.Element;
    }

    Func<ConfigElement> ILazyConfigElementItem.GetBaseInitializer() => (Func<ConfigElement>) this.initializer;

    void ILazyConfigElementItem.AddSource(ConfigSource source)
    {
      if (this.sources == null)
        this.sources = this.GetSourcesList();
      else if (this.sources.Contains(source))
        return;
      this.sources.Add(source);
    }

    IEnumerable<ConfigSource> ILazyConfigElementItem.Sources => this.sources != null ? (IEnumerable<ConfigSource>) this.sources : (IEnumerable<ConfigSource>) this.GetSourcesList();

    private IList<ConfigSource> GetSourcesList() => (IList<ConfigSource>) new List<ConfigSource>()
    {
      this.ItemProperties.ContainsKey("configSource") ? (ConfigSource) this.ItemProperties["configSource"] : ConfigSource.Default
    };

    private void Reset(Func<TElement> newInitializer = null)
    {
      if (newInitializer != null)
        this.initializer = newInitializer;
      this.lazyElement = new Lazy<TElement>(this.initializer);
      this.lazyProperties = new ExpandoObject();
      this.LoadingError = (ConfigElementLoadException) null;
    }

    private TElement InitializerWraper(Func<ConfigElement> initializer)
    {
      try
      {
        return (TElement) initializer();
      }
      catch (Exception ex)
      {
        Exception exceptionToHandle = (Exception) new ConfigurationErrorsException(string.Format("Failed to load lazy element '{0}{1}{2}': {3}.", (object) this.parent.GetPath(), (object) '/', (object) this.Key, (object) ex.Message), ex);
        this.LoadingError = !Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions) ? new ConfigElementLoadException()
        {
          ErrorMessage = exceptionToHandle.Message
        } : throw exceptionToHandle;
      }
      return default (TElement);
    }
  }
}
