// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.NavigationNodeContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  public class NavigationNodeContainer : WebControl, INamingContainer
  {
    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// When implemented, gets an object that is used in simplified data-binding operations.
    /// </summary>
    /// <returns>An object that represents the value to use when data-binding operations are performed.</returns>
    public virtual object DataItem { get; set; }

    /// <summary>
    /// Binds a data source to the invoked server control and all its child controls.
    /// </summary>
    public override void DataBind()
    {
      base.DataBind();
      if (this.DataBound == null)
        return;
      this.DataBound((object) this, EventArgs.Empty);
    }

    public event EventHandler DataBound;
  }
}
