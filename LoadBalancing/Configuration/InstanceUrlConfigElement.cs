// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Configuration.InstanceUrlConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.LoadBalancing.Configuration
{
  /// <summary>Represents the base URL of a server instance.</summary>
  public class InstanceUrlConfigElement : ConfigElement
  {
    internal InstanceUrlConfigElement()
      : base(false)
    {
    }

    public InstanceUrlConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal InstanceUrlConfigElement(bool check)
      : base(check)
    {
    }

    /// <summary>Represents the base URL of a server instance.</summary>
    [ConfigurationProperty("value", IsKey = true, IsRequired = true)]
    public string Value
    {
      get => (string) this["value"];
      set => this[nameof (value)] = (object) value;
    }

    protected internal override void Validate()
    {
      base.Validate();
      Uri uri = new Uri(WebServiceSystemMessageSender.GetServiceEndpoint(this.Value));
    }
  }
}
