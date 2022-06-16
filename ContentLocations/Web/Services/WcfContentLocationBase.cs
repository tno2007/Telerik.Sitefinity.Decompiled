// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Web.Services.WcfContentLocationBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.ContentLocations.Web.Services
{
  /// <summary>
  /// The base WCF context object for a ContentLocation used the UI
  /// </summary>
  [DataContract]
  public class WcfContentLocationBase
  {
    /// <summary>Gets or sets the URL.</summary>
    /// <value>The URL.</value>
    [DataMember]
    public string Url { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }
  }
}
