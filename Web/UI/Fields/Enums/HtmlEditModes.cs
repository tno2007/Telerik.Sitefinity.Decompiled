// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Enums.HtmlEditModes
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.Fields.Enums
{
  /// <summary>Defines the display mode of the field control.</summary>
  [Flags]
  public enum HtmlEditModes
  {
    /// <summary>
    /// Design mode. The default edit mode in the editor, where you could edit HTML in
    /// WYSIWYG fashion.
    /// </summary>
    Design = 1,
    /// <summary>
    /// HTML mode. Advanced edit mode where you could directly modify the HTML.
    /// </summary>
    Html = 2,
    /// <summary>
    /// Preview mode. In this mode the editor will display its content the same way as
    /// it should be displayed when placed outside of the control.
    /// </summary>
    Preview = 4,
    All = Preview | Html | Design, // 0x00000007
  }
}
