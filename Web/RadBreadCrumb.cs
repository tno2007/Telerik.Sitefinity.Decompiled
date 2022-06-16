// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.RadBreadCrumb
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Web.UI;
using Telerik.Web;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>RadBreadCrumb - a breadcrumb implementation</summary>
  [ClientScriptResource("Telerik.Sitefinity.Web.UI.RadBreadCrumb", "Telerik.Sitefinity.Web.Scripts.RadBreadCrumb.js")]
  public class RadBreadCrumb : RadWebControl
  {
    /// <summary>
    /// Gets or sets the value, indicating whether to render links to the embedded skins or not.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If EnableEmbeddedSkins is set to false you will have to register the needed CSS files by hand.
    /// </remarks>
    [DefaultValue(false)]
    public override bool EnableEmbeddedSkins => false;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
