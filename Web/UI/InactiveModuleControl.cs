// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.InactiveModuleControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents a control wrapper preventing loading a control from inactive module.
  /// </summary>
  public class InactiveModuleControl : Control
  {
    private readonly string moduleName;

    public InactiveModuleControl(string moduleName) => this.moduleName = moduleName;

    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      writer.Write("<div class=\"sf_inactiveWidget\">");
      if (string.IsNullOrEmpty(this.moduleName))
        writer.Write("This widget doesn't work, because its module has been deactivated.");
      else
        writer.Write(string.Format("This widget doesn't work, because <strong>{0}</strong> module has been deactivated.", (object) this.moduleName));
      writer.Write("</div>");
    }
  }
}
