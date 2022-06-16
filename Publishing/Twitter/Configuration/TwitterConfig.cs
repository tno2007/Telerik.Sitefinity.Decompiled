// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Configuration.TwitterConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Publishing.Twitter.Configuration
{
  public class TwitterConfig : ConfigSection
  {
    [ConfigurationProperty("twitterApplications")]
    [ConfigurationCollection(typeof (TwitterApplication), AddItemName = "twitterApplication")]
    public ConfigElementDictionary<string, TwitterApplication> TwitterApplications => (ConfigElementDictionary<string, TwitterApplication>) this["twitterApplications"];

    [ConfigurationProperty("twitterUsers")]
    [ConfigurationCollection(typeof (TwitterUser), AddItemName = "twitterUser")]
    public ConfigElementDictionary<string, TwitterUser> TwitterUsers => (ConfigElementDictionary<string, TwitterUser>) this["twitterUsers"];

    [ConfigurationProperty("associations")]
    [ConfigurationCollection(typeof (AssociationItem), AddItemName = "association")]
    public ConfigElementList<AssociationItem> Associations => (ConfigElementList<AssociationItem>) this["associations"];

    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      if (this.TwitterApplications.ContainsKey("Sitefinity"))
        return;
      this.TwitterApplications.Add(new TwitterApplication((ConfigElement) this.TwitterApplications)
      {
        ConsumerKey = "c1X72sDtHOwYiUVtTHCw7A",
        ConsumerSecret = "pDA8im7DsnHDHtxinvCARHUR2ttgopGMbZFHEtSE",
        Name = "Sitefinity"
      });
    }
  }
}
