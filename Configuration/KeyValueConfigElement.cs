// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.KeyValueConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// A key/value pair configuration element, to represent a simple value in a <see cref="T:Telerik.Sitefinity.Configuration.ConfigElementCollection" />.
  /// </summary>
  public class KeyValueConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Configuration.KeyValueConfigElement" />
    /// </summary>
    /// <param name="parent">The configuration element that contains this one.</param>
    public KeyValueConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>The key of the element.</summary>
    [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
    public string Key
    {
      get => (string) this["key"];
      set => this["key"] = (object) value;
    }

    /// <summary>The value of the element.</summary>
    [ConfigurationProperty("value", IsRequired = true)]
    public string Value
    {
      get => (string) this["value"];
      set => this[nameof (value)] = (object) value;
    }
  }
}
