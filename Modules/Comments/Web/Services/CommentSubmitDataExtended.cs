// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.Services.CommentSubmitDataExtended
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Comments.Web.Services
{
  [Obsolete("Use Telerik.Sitefinity.Services.Comments.DTO.CommentCreateRequest instead.")]
  [DataContract]
  internal class CommentSubmitDataExtended : CommentSubmitData
  {
    [DataMember]
    public string GroupKey { get; set; }

    [DataMember]
    public string ThreadType { get; set; }

    [DataMember]
    public string ThreadTitle { get; set; }

    [DataMember]
    public string DataSource { get; set; }
  }
}
