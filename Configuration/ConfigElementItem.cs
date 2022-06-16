// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigElementItem`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Configuration
{
  public class ConfigElementItem<TElement> : IConfigElementItem where TElement : ConfigElement
  {
    protected internal string key;
    protected TElement element;
    protected IDictionary<string, object> itemProperties;

    protected ConfigElementItem()
    {
    }

    public ConfigElementItem(string key, TElement element)
    {
      this.key = key;
      if ((object) element == null)
        return;
      this.element = element;
      this.element.TagName = key;
    }

    public virtual bool IsLazy => false;

    public virtual bool IsLoaded => true;

    public virtual bool IsDeleted { get; set; }

    public virtual void Unload()
    {
    }

    public virtual void Unload(Func<ConfigElement> newLoader)
    {
    }

    public virtual string Key
    {
      get => this.key;
      set => this.key = value;
    }

    public virtual TElement Element
    {
      get => this.element;
      protected internal set => this.element = value;
    }

    public virtual object ElementProperties => (object) this.element;

    public virtual IDictionary<string, object> ItemProperties
    {
      get
      {
        if (this.itemProperties == null)
          this.itemProperties = (IDictionary<string, object>) new Dictionary<string, object>();
        return this.itemProperties;
      }
    }

    public ConfigElementLoadException LoadingError { get; set; }

    ConfigElement IConfigElementItem.Element => (ConfigElement) this.Element;
  }
}
