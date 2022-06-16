// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.CreateSiteTaskSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Utilities.Json;

namespace Telerik.Sitefinity.Multisite
{
  [DataContract]
  internal class CreateSiteTaskSettings
  {
    [DataMember]
    public SiteConfigurationViewModel Model { get; set; }

    [DataMember]
    public Guid CurrentUserId { get; set; }

    public static CreateSiteTaskSettings Parse(string data) => JsonUtility.FromJson<CreateSiteTaskSettings>(data);

    public override string ToString() => this.ToJson<CreateSiteTaskSettings>();
  }
}
