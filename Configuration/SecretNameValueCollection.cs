// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.SecretNameValueCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.Configuration
{
  internal class SecretNameValueCollection : NameValueCollection, ICloneable
  {
    private Dictionary<string, SecretNameValueCollection.LazyParameterValue> resolvers = new Dictionary<string, SecretNameValueCollection.LazyParameterValue>();

    public SecretNameValueCollection()
    {
    }

    public SecretNameValueCollection(NameValueCollection col)
      : base(col)
    {
    }

    public string GetEnvironmentOriginalValue(string name)
    {
      SecretNameValueCollection.LazyParameterValue lazyParameterValue;
      return this.resolvers.TryGetValue(name, out lazyParameterValue) && lazyParameterValue is SecretNameValueCollection.EnvironmentParameterValue environmentParameterValue ? environmentParameterValue.OriginalValue : (string) null;
    }

    public void SetEnvironmentValue(
      string key,
      string value,
      string secretResolver,
      string originalValue)
    {
      this.resolvers[key] = (SecretNameValueCollection.LazyParameterValue) new SecretNameValueCollection.EnvironmentParameterValue(secretResolver, originalValue);
      base.Set(key, value);
    }

    public void SetSecretValue(string key, string value, string secretResolver)
    {
      this.resolvers[key] = new SecretNameValueCollection.LazyParameterValue(secretResolver);
      base.Set(key, value);
    }

    public override string Get(int index) => this.Get(this.GetKey(index));

    public bool IsSecret(string name, out string resolver)
    {
      SecretNameValueCollection.LazyParameterValue lazyParameterValue;
      if (this.resolvers.TryGetValue(name, out lazyParameterValue))
      {
        resolver = lazyParameterValue.Name;
        return true;
      }
      resolver = (string) null;
      return false;
    }

    public override string Get(string name)
    {
      SecretNameValueCollection.LazyParameterValue lazyParameterValue;
      return this.resolvers.TryGetValue(name, out lazyParameterValue) ? lazyParameterValue.Resolve((Func<string>) (() => base.Get(name))) : base.Get(name);
    }

    public override void Set(string name, string value)
    {
      SecretNameValueCollection.LazyParameterValue lazyParameterValue;
      if (this.resolvers.TryGetValue(name, out lazyParameterValue))
        value = lazyParameterValue.Unresolve(value);
      base.Set(name, value);
    }

    public override void Add(string name, string value)
    {
      SecretNameValueCollection.LazyParameterValue lazyParameterValue;
      if (this.resolvers.TryGetValue(name, out lazyParameterValue))
      {
        value = lazyParameterValue.Unresolve(value);
        base.Set(name, value);
      }
      else
        base.Add(name, value);
    }

    public override void Clear()
    {
      base.Clear();
      this.resolvers.Clear();
    }

    public override void Remove(string name)
    {
      base.Remove(name);
      this.resolvers.Remove(name);
    }

    public object Clone()
    {
      SecretNameValueCollection nameValueCollection = new SecretNameValueCollection((NameValueCollection) this);
      foreach (KeyValuePair<string, SecretNameValueCollection.LazyParameterValue> resolver in this.resolvers)
        nameValueCollection.resolvers.Add(resolver.Key, resolver.Value);
      return (object) nameValueCollection;
    }

    private class LazyParameterValue
    {
      private string value;
      private string resolver;

      public LazyParameterValue(string resolver) => this.resolver = resolver;

      public string Name => this.resolver;

      public string Resolve(Func<string> keyFactory)
      {
        if (this.value == null)
        {
          lock (this)
          {
            if (this.value == null)
              this.value = Config.ResolveSecretValue(this.resolver, keyFactory());
          }
        }
        return this.value;
      }

      public string Unresolve(string value)
      {
        lock (this)
        {
          this.value = value;
          return Config.GenerateSecretKey(this.resolver, value);
        }
      }
    }

    private class EnvironmentParameterValue : SecretNameValueCollection.LazyParameterValue
    {
      private string originalValue;

      public EnvironmentParameterValue(string resolver, string originalValue)
        : base(resolver)
      {
        this.originalValue = originalValue;
      }

      public string OriginalValue => this.originalValue;
    }
  }
}
