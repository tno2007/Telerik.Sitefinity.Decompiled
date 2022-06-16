// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.MarketingPropertyValue
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  [DataContract]
  internal class MarketingPropertyValue
  {
    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public string Link { get; set; }

    [DataMember]
    public string LinkTitle { get; set; }
  }
}
