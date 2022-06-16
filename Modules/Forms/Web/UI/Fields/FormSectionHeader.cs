// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormSectionHeader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  [ControlDesigner(typeof (FormSectionHeaderDesigner))]
  [PropertyEditorTitle(typeof (FormsResources), "SectionHeader")]
  [FormControlDisplayMode(FormControlDisplayMode.ReadAndWrite)]
  public class FormSectionHeader : Label
  {
    public FormSectionHeader()
    {
      this.TitleFontSize = FormControlSize.Small;
      this.WrapperTag = HtmlTextWriterTag.H2;
      this.Title = Res.Get<FormsResources>().SectionHeaderGoesHere;
    }

    /// <summary>Gets or sets the title of the field control.</summary>
    /// <value>The title of the field control.</value>
    [MultilingualProperty]
    public string Title { get; set; }

    /// <summary>Represents the differnet font sizes for the title</summary>
    public FormControlSize TitleFontSize { get; set; }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    public HtmlTextWriterTag WrapperTag { get; set; }

    protected override void OnPreRender(EventArgs e)
    {
      this.Text = ControlUtilities.Sanitize(this.Title);
      this.AddCssClass("sfFormTitle");
      if (this.TitleFontSize != FormControlSize.None)
        this.AddCssClass("sfTitle" + (object) this.TitleFontSize);
      base.OnPreRender(e);
    }

    protected override HtmlTextWriterTag TagKey => this.WrapperTag;
  }
}
