// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.EnvironmentValue
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration
{
  internal class EnvironmentValue : SecretValue, ICloneable
  {
    public EnvironmentValue(
      string key,
      ConfigProperty configProperty,
      string resolverName,
      object originalValue)
      : base(key, configProperty, resolverName)
    {
      this.OriginalValue = originalValue;
    }

    public object OriginalValue { get; private set; }

    object ICloneable.Clone() => (object) new EnvironmentValue(this.Key, this.ConfigProperty, this.ResolverName, this.OriginalValue);

    public string GetOriginalValue() => ConfigElement.GetStringValue(this.OriginalValue, this.ConfigProperty);
  }
}
