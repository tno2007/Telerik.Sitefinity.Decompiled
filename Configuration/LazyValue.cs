// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.LazyValue
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Configuration
{
  internal class LazyValue : ICloneable
  {
    private Lazy<object> lazy;
    private string key;
    private Type actualType;
    private ConfigProperty configProperty;

    public LazyValue(string key, ConfigProperty configProperty)
      : this(key, configProperty, (Type) null)
    {
    }

    public LazyValue(string key, ConfigProperty configProperty, Type actualType)
    {
      this.key = key;
      this.configProperty = configProperty;
      Type type = actualType;
      if ((object) type == null)
        type = configProperty.Type;
      this.actualType = type;
      this.lazy = new Lazy<object>(new Func<object>(this.GetValue));
    }

    public object Value
    {
      get => this.lazy.Value;
      set
      {
        this.key = this.GetKey(this.lazy.Value);
        if (!this.lazy.IsValueCreated)
          return;
        this.lazy = new Lazy<object>(new Func<object>(this.GetValue));
      }
    }

    public string Key
    {
      get
      {
        if (this.key == null && this.lazy.IsValueCreated && !this.KeepKeyOnResolve)
          this.key = this.GetKey(this.lazy.Value);
        return this.key;
      }
    }

    public override bool Equals(object obj) => obj is LazyValue lazyValue && this.Key.Equals(lazyValue.Key);

    object ICloneable.Clone() => (object) new LazyValue(this.Key, this.configProperty, this.actualType);

    protected ConfigProperty ConfigProperty => this.configProperty;

    protected virtual bool KeepKeyOnResolve => false;

    protected virtual string GetKey(object value) => ConfigElement.GetStringValue(value, this.configProperty);

    protected virtual object GetValue(string key) => ConfigElement.GetValueFromString(key, this.actualType, this.configProperty);

    internal bool IsLoaded => this.lazy.IsValueCreated;

    private object GetValue()
    {
      try
      {
        object obj = this.GetValue(this.key);
        if (!this.KeepKeyOnResolve)
          this.key = (string) null;
        return obj;
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw;
      }
      return (object) null;
    }
  }
}
