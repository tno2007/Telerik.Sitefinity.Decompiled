// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Configuration.TwitterUser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Publishing.Twitter.Configuration
{
  [DataContract]
  public class TwitterUser : ConfigElement, ITwitterUser
  {
    public TwitterUser(ConfigElement parent)
      : base(parent)
    {
    }

    [DataMember]
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    public virtual string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    [DataMember]
    [ConfigurationProperty("xxx", DefaultValue = "Yyy", IsRequired = false)]
    public virtual string Xxx
    {
      get => (string) this["xxx"];
      set => this["xxx"] = (object) value;
    }
  }
}
