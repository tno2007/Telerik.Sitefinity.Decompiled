// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContextualHelp.Web.Services.DTO.Request.MarkTooltips
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.ContextualHelp.Web.Services.DTO.Request
{
  /// <summary>
  /// Request DTO for updating the user-specific tooltip data.
  /// </summary>
  internal class MarkTooltips
  {
    /// <summary>Gets or sets the ids.</summary>
    /// <value>The ids.</value>
    public IEnumerable<string> Ids { get; set; }
  }
}
