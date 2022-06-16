// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Conditional
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  public class Conditional : DataBoundTemplateControl
  {
    private bool bound;
    private ITemplate template;

    public bool If { get; set; }

    [TemplateContainer(typeof (IDataItemContainer))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ITemplate Then { get; set; }

    [TemplateContainer(typeof (IDataItemContainer))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ITemplate Else { get; set; }

    protected override ITemplate Template
    {
      get
      {
        if (!this.bound)
        {
          if (this.If && this.Then != null)
            this.template = this.Then;
          else if (this.Else != null)
            this.template = this.Else;
          this.bound = true;
        }
        return this.template;
      }
      set => this.template = value;
    }

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      this.AddAttributesToRender(writer);
      writer.RenderBeginTag(this.TagKey);
    }

    /// <inheritdoc />
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      base.RenderEndTag(writer);
    }
  }
}
