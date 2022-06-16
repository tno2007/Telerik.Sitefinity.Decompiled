// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.GoogleAnalyticsEmbedControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.Designers;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>
  /// Public control that embeds the google analytics code in the page (before the closing body tag)
  /// </summary>
  [ControlDesigner(typeof (GoogleAnalyticsEmbedControlDesigner))]
  [PropertyEditorTitle(typeof (Labels), "GoogleAnalytics")]
  public class GoogleAnalyticsEmbedControl : SimpleView, ICustomWidgetVisualization
  {
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.FileEmbedControl.ascx");
    internal const string googleAnalyticsKey = "googleAnalytics";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.GoogleAnalyticsEmbedControl" /> class.
    /// </summary>
    public GoogleAnalyticsEmbedControl() => this.LayoutTemplatePath = GoogleAnalyticsEmbedControl.layoutTemplatePath;

    /// <summary>
    /// Gets or sets the Google Analytics Code to be stored before the closing body tag
    /// </summary>
    public string GoogleAnalyticsCode { get; set; }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null || this.IsDesignMode() || this.IsPreviewMode() || ((IContentLocatableView) null).IsPreviewRequested() || string.IsNullOrEmpty(this.GoogleAnalyticsCode))
        return;
      if (!this.Page.ClientScript.IsStartupScriptRegistered(typeof (GoogleAnalyticsEmbedControl), "googleAnalytics"))
        this.Page.ClientScript.RegisterStartupScript(typeof (GoogleAnalyticsEmbedControl), "googleAnalytics", this.GoogleAnalyticsCode, !this.GoogleAnalyticsCode.Contains("<script"));
      else
        this.Controls.Add((Control) new Label()
        {
          Text = "More than one Google Analytics scripts are registered. Please verify your pages and templates."
        });
    }

    /// <summary>Remove the outer span on the control</summary>
    /// <param name="writer"></param>
    protected override void Render(HtmlTextWriter writer) => this.RenderContents(writer);

    /// <summary>Indicates if the control is empty.</summary>
    /// <value></value>
    public bool IsEmpty => string.IsNullOrEmpty(this.GoogleAnalyticsCode);

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public string EmptyLinkText => Res.Get<PublicControlsResources>().SetGoogleAnalytics;
  }
}
