// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.ISiteOfflineInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Multisite
{
  public interface ISiteOfflineInfo
  {
    /// <summary>
    /// Gets or sets a the textual message to display when the site if offline.
    /// </summary>
    /// <value>The offline site message.</value>
    string OfflineSiteMessage { get; }

    /// <summary>
    /// ets or sets the ID of the page to redirect to when the site if offline.
    /// </summary>
    /// <value>The offline page to redirect.</value>
    Guid OfflinePageToRedirect { get; }

    /// <summary>
    /// Gets or sets the indicating whether to redirect the user when the site if offline. Otherwise a textual message is displayed.
    /// </summary>
    /// <value><c>true</c> if [redirect if offline]; otherwise, <c>false</c>.</value>
    bool RedirectIfOffline { get; }
  }
}
