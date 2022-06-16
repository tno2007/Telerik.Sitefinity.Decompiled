// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.Services.CommentsSecurityViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Comments.Web.Services
{
  /// <summary>Define security behavior of the comments widget</summary>
  [Obsolete("Use Telerik.Sitefinity.Services.Comments.DTO.ThreadSettingsResponse instead. For the IsAuthenitcated property use UserSessionService")]
  [DataContract]
  internal class CommentsSecurityViewModel
  {
    [DataMember]
    public bool RequireCaptcha { get; set; }

    [DataMember]
    public bool IsAuthenticated { get; set; }
  }
}
