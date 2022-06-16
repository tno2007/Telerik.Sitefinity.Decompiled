// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.ReindexTaskSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Utilities.Json;

namespace Telerik.Sitefinity.Publishing
{
  [DataContract]
  internal class ReindexTaskSettings
  {
    public ReindexTaskSettings() => this.ProcessedPipes = new HashSet<string>();

    [DataMember]
    public Guid PublishingPointId { get; set; }

    [DataMember]
    public string PublishingPointProvider { get; set; }

    [DataMember]
    public Guid SiteId { get; set; }

    [DataMember]
    public HashSet<string> ProcessedPipes { get; set; }

    public static ReindexTaskSettings Parse(string data) => JsonUtility.FromJson<ReindexTaskSettings>(data);

    public override string ToString() => this.ToJson<ReindexTaskSettings>();
  }
}
