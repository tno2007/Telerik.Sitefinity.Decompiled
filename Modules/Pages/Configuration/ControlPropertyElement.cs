﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ControlPropertyElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// 
  /// </summary>
  public class ControlPropertyElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ControlPropertyElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("value")]
    public object Value
    {
      get => this["value"];
      set => this[nameof (value)] = value;
    }
  }
}
