// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Web.Services.WcfItemContentLocation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.ContentLocations.Web.Services
{
  /// <summary>
  /// WCF context object for a ContentLocation used by Content Item Locations field
  /// </summary>
  [DataContract]
  public class WcfItemContentLocation : WcfContentLocationBase
  {
    /// <summary>Gets or sets the item live URL.</summary>
    /// <value>The item live URL.</value>
    [DataMember]
    public string ItemLiveUrl { get; set; }
  }
}
