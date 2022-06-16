// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.SecurityTokenKeyElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  public class SecurityTokenKeyElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public SecurityTokenKeyElement(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("realm", DefaultValue = "", IsKey = true, IsRequired = true)]
    public virtual string Realm
    {
      get => (string) this["realm"];
      set => this["realm"] = (object) value;
    }

    [ConfigurationProperty("key", DefaultValue = "", IsRequired = true)]
    public virtual string Key
    {
      get => (string) this["key"];
      set => this["key"] = (object) value;
    }

    [ConfigurationProperty("encoding", DefaultValue = BinaryEncoding.Hexadecimal)]
    public virtual BinaryEncoding Encoding
    {
      get => (BinaryEncoding) this["encoding"];
      set => this["encoding"] = (object) value;
    }

    [ConfigurationProperty("membershipProvider", DefaultValue = "", IsRequired = true)]
    public virtual string MembershipProvider
    {
      get => (string) this["membershipProvider"];
      set => this["membershipProvider"] = (object) value;
    }
  }
}
