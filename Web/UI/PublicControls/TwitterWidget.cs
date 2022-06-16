// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.TwitterWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.Designers;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  [ControlDesigner(typeof (TwitterWidgetDesigner))]
  [PropertyEditorTitle(typeof (Labels), "TwitterWidget")]
  public class TwitterWidget : SimpleView, ICustomWidgetVisualization
  {
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.FileEmbedControl.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.GoogleAnalyticsEmbedControl" /> class.
    /// </summary>
    public TwitterWidget() => this.LayoutTemplatePath = TwitterWidget.layoutTemplatePath;

    /// <summary>
    /// Gets or sets the Twitter Widget Code to be stored before the closing body tag
    /// </summary>
    public string TwitterWidgetCode { get; set; }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null || string.IsNullOrEmpty(this.TwitterWidgetCode))
        return;
      Literal child = new Literal();
      if (!this.IsDesignMode())
        child.Text = this.TwitterWidgetCode;
      else
        child.Text = Res.Get<PublicControlsResources>().ControlCannotBeRenderedInDesignMode.Arrange((object) Res.Get<UserProfilesResources>().TwitterWidgetTitle);
      this.Container.Controls.Add((Control) child);
    }

    /// <summary>Remove the outer span on the control</summary>
    /// <param name="writer"></param>
    protected override void Render(HtmlTextWriter writer) => this.RenderContents(writer);

    /// <summary>Indicates if the control is empty.</summary>
    /// <value></value>
    public bool IsEmpty => string.IsNullOrEmpty(this.TwitterWidgetCode);

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public string EmptyLinkText => Res.Get<PublicControlsResources>().SetTwitterWidget;
  }
}
