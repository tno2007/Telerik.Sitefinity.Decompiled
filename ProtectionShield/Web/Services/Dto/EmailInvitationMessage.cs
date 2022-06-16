// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Web.Services.Dto.EmailInvitationMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.ProtectionShield.Web.Services.Dto
{
  /// <summary>Access email invitation message</summary>
  internal class EmailInvitationMessage
  {
    /// <summary>Gets or sets an expiration date</summary>
    public DateTime? ExpiresOn { get; set; }

    /// <summary>Gets or sets a collection of emails</summary>
    public ICollection<string> Emails { get; set; }
  }
}
