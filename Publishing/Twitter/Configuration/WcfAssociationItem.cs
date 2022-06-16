// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Configuration.WcfAssociationItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Publishing.Twitter.Configuration
{
  [DataContract]
  public class WcfAssociationItem : IAssociationItem
  {
    public WcfAssociationItem()
    {
    }

    public WcfAssociationItem(IAssociationItem copyFromItem) => this.CopyFrom(copyFromItem);

    public void CopyFrom(IAssociationItem copyFromItem)
    {
      this.AccessToken = copyFromItem.AccessToken;
      this.AccessTokenSecret = copyFromItem.AccessTokenSecret;
      this.AppName = copyFromItem.AppName;
      this.UserName = copyFromItem.UserName;
    }

    public void CopyTo(ref IAssociationItem copyToItem)
    {
      copyToItem.AccessToken = this.AccessToken;
      copyToItem.AccessTokenSecret = this.AccessTokenSecret;
      copyToItem.AppName = this.AppName;
      copyToItem.UserName = this.UserName;
    }

    [DataMember]
    public string AccessToken { get; set; }

    [DataMember]
    public string AccessTokenSecret { get; set; }

    [DataMember]
    public string AppName { get; set; }

    [DataMember]
    public string UserName { get; set; }
  }
}
