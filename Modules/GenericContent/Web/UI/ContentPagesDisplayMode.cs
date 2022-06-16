// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  /// <summary>Represents display mode of the Content Pages dialog</summary>
  [Obsolete("It is only used in the DisplayMode property of ContentPagesDialog which is now also obsolete.")]
  public enum ContentPagesDisplayMode
  {
    /// <summary>The default mode</summary>
    None,
    /// <summary>
    /// Lists all pages - both successfully updated and out-dated
    /// </summary>
    AllPages,
    /// <summary>
    /// Lists all pages and out-dated are highlighted in yellow
    /// </summary>
    AllPagesWithOutDatedHighlighted,
    /// <summary>Lists out-dated pages</summary>
    OutDatedPages,
  }
}
