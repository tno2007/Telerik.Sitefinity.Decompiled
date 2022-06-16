// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.Services.ThreadViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Comments.Web.Services
{
  [Obsolete("Use Telerik.Sitefinity.Services.Comments.DTO.ThreadResponse and ThreadSettingsResponse instead.")]
  [DataContract]
  internal class ThreadViewModel
  {
    [DataMember]
    public string Key { get; set; }

    [DataMember]
    public string Type { get; set; }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string Language { get; set; }

    [DataMember]
    public bool IsClosed { get; set; }

    [DataMember]
    public bool RequireApproval { get; set; }

    [DataMember]
    public bool RequireAuthentication { get; set; }

    [DataMember]
    public string GroupKey { get; set; }

    [DataMember]
    public string AuthorKey { get; set; }

    [DataMember]
    public DateTime DateCreated { get; set; }
  }
}
