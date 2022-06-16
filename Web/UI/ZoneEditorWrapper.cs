// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ZoneEditorWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI
{
  internal class ZoneEditorWrapper : WebControl
  {
    private HtmlGenericControl toolboxContainer;
    private HtmlGenericControl contentContainer;
    private HtmlGenericControl hideShowToobox;

    internal Control ToolboxContainer
    {
      get
      {
        if (this.toolboxContainer == null)
        {
          this.toolboxContainer = new HtmlGenericControl("div");
          this.toolboxContainer.ID = "ZoneEditorToolboxContainer";
          this.toolboxContainer.Attributes.Add("class", "ZoneEditorToolboxContainer");
        }
        return (Control) this.toolboxContainer;
      }
    }

    internal HtmlGenericControl ContentContainer
    {
      get
      {
        if (this.contentContainer == null)
        {
          this.contentContainer = new HtmlGenericControl("div");
          this.contentContainer.ID = "sfPageContainer";
          this.contentContainer.Attributes.Add("class", "sfPageContainer");
        }
        return this.contentContainer;
      }
    }

    internal HtmlGenericControl HideShowToobox
    {
      get
      {
        if (this.hideShowToobox == null)
        {
          this.hideShowToobox = new HtmlGenericControl("a");
          this.hideShowToobox.ID = "sfHideShowToolbox";
          this.hideShowToobox.InnerText = Res.Get<PageResources>().ToggleToolbox;
          this.hideShowToobox.Attributes.Add("class", "sfHideShowToolbox");
          this.hideShowToobox.Attributes.Add("title", "Toggle toolbox");
        }
        return this.hideShowToobox;
      }
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected override void AddAttributesToRender(HtmlTextWriter writer)
    {
      writer.AddAttribute(HtmlTextWriterAttribute.Class, "sfPageWrapper");
      base.AddAttributesToRender(writer);
    }

    protected override void CreateChildControls()
    {
      this.Controls.Add((Control) this.ContentContainer);
      this.Controls.Add((Control) this.HideShowToobox);
      this.Controls.Add(this.ToolboxContainer);
    }

    protected override void Render(HtmlTextWriter writer) => base.Render(writer);
  }
}
