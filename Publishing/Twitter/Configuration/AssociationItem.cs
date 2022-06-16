// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Configuration.AssociationItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Publishing.Twitter.Configuration
{
  [DataContract]
  public class AssociationItem : ConfigElement, IAssociationItem
  {
    public AssociationItem(ConfigElement parent)
      : base(parent)
    {
    }

    [DataMember]
    [ConfigurationProperty("appName", IsRequired = true)]
    public virtual string AppName
    {
      get => (string) this["appName"];
      set => this["appName"] = (object) value;
    }

    [DataMember]
    [ConfigurationProperty("userName", IsRequired = true)]
    public virtual string UserName
    {
      get => (string) this["userName"];
      set => this["userName"] = (object) value;
    }

    [DataMember]
    [ConfigurationProperty("accessToken", IsRequired = true)]
    public virtual string AccessToken
    {
      get => (string) this["accessToken"];
      set => this["accessToken"] = (object) value;
    }

    [DataMember]
    [ConfigurationProperty("accessTokenSecret", IsRequired = true)]
    public virtual string AccessTokenSecret
    {
      get => (string) this["accessTokenSecret"];
      set => this["accessTokenSecret"] = (object) value;
    }
  }
}
