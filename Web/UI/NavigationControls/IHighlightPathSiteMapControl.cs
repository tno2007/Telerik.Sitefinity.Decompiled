// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.IHighlightPathSiteMapControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  public interface IHighlightPathSiteMapControl
  {
    /// <summary>Gets or sets whether to highlight item's path</summary>
    bool HighlightPath { get; set; }

    /// <summary>Gets or sets guid of the current page</summary>
    Guid CurrentPageNodeId { get; set; }
  }
}
