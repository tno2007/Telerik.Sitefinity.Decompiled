// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.PersistedValueWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Wraps a configuration property value, to allow change tracking.
  /// </summary>
  internal sealed class PersistedValueWrapper : ICloneable
  {
    private object value;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.PersistedValueWrapper" />, wrapping <paramref name="value" />.
    /// </summary>
    /// <param name="value">Configuration property value to be wrapped.</param>
    public PersistedValueWrapper(object value)
      : this(value, ConfigSource.NotSet)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.PersistedValueWrapper" />, wrapping <paramref name="value" />.
    /// </summary>
    /// <param name="value">Configuration property value to be wrapped.</param>
    /// <param name="source">The source of the value.</param>
    public PersistedValueWrapper(object value, ConfigSource source)
    {
      this.value = !(value is PersistedValueWrapper) ? value : ((PersistedValueWrapper) value).Value;
      this.Source = source;
    }

    /// <summary>Gets or sets the wrapped value.</summary>
    public object Value
    {
      get => this.value;
      set => this.value = value;
    }

    public ConfigSource Source { get; private set; }

    public object Clone()
    {
      object obj = this.value;
      if (obj != null)
        obj = ConfigElement.ClonePropertyValue(this.value);
      return (object) new PersistedValueWrapper(obj);
    }
  }
}
