// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityPlaceHolder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Represents content place holder for Sitefinity pages.</summary>
  [DebuggerDisplay("[SitefinityPlaceHolder], ID={ID}")]
  public class SitefinityPlaceHolder : PlaceHolder
  {
    /// <summary>
    /// Gets or sets the text to be displayed in the placeholder design time.
    /// </summary>
    public string Text { get; set; }
  }
}
