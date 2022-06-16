// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Configuration.WcfTwitterApplication
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Publishing.Twitter.Configuration
{
  [DataContract]
  public class WcfTwitterApplication : ITwitterApplication
  {
    public WcfTwitterApplication()
    {
    }

    public WcfTwitterApplication(ITwitterApplication copyFromApp) => this.CopyFrom(copyFromApp);

    public void CopyFrom(ITwitterApplication app)
    {
      this.OriginalAppName = this.Name = app.Name;
      this.ConsumerKey = app.ConsumerKey;
      this.ConsumerSecret = app.ConsumerSecret;
    }

    public void CopyTo(ref ITwitterApplication app)
    {
      app.Name = this.Name;
      app.ConsumerKey = this.ConsumerKey;
      app.ConsumerSecret = this.ConsumerSecret;
    }

    [DataMember]
    public virtual string Name { get; set; }

    [DataMember]
    public virtual string ConsumerKey { get; set; }

    [DataMember]
    public virtual string ConsumerSecret { get; set; }

    [DataMember]
    public virtual string OriginalAppName { get; set; }
  }
}
