// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.CssEmbedControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.Designers;
using Telerik.Sitefinity.Web.UI.PublicControls.Enums;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>
  /// 
  /// </summary>
  [ControlDesigner(typeof (CssEmbedControlDesigner))]
  [PropertyEditorTitle(typeof (Labels), "CSS")]
  public class CssEmbedControl : SimpleView, ICustomWidgetVisualization
  {
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.FileEmbedControl.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.CssEmbedControl" /> class.
    /// </summary>
    public CssEmbedControl() => this.LayoutTemplatePath = CssEmbedControl.layoutTemplatePath;

    /// <summary>Gets or sets the URL of the style sheet</summary>
    /// <value>The URL.</value>
    public string Url { get; set; }

    /// <summary>Gets or sets the media type.</summary>
    public MediaType MediaType { get; set; }

    /// <summary>Custom CSS code for linking style sheets</summary>
    public string CustomCssCode { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal || this.Page == null)
        return;
      SitefinityStyleSheetManager current = SitefinityStyleSheetManager.GetCurrent(this.Page);
      if (current != null)
        current.RegisterCssWidgetControl(this);
      else if (!string.IsNullOrEmpty(this.Url))
      {
        HtmlLink child = new HtmlLink()
        {
          Href = this.Url
        };
        child.Attributes["rel"] = "stylesheet";
        child.Attributes["type"] = "text/css";
        child.Attributes["media"] = CssEmbedControl.ConvertMediaTypeToString(this.MediaType);
        if (this.Page.Header != null)
          this.Page.Header.Controls.Add((Control) child);
        else
          this.Controls.Add((Control) child);
      }
      else
      {
        if (string.IsNullOrEmpty(this.CustomCssCode))
          return;
        Literal child = new Literal()
        {
          Text = string.Format("<style type=\"text/css\" media=\"{1}\">{0}</style>", (object) this.CustomCssCode, (object) CssEmbedControl.ConvertMediaTypeToString(this.MediaType))
        };
        if (this.Page.Header != null)
          this.Page.Header.Controls.Add((Control) child);
        else
          this.Controls.Add((Control) child);
      }
    }

    /// <summary>Remove the outer span on the control</summary>
    /// <param name="writer"></param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      this.RenderContents(writer);
    }

    /// <summary>Indicates if the control is empty.</summary>
    /// <value></value>
    public bool IsEmpty => string.IsNullOrEmpty(this.Url) && string.IsNullOrEmpty(this.CustomCssCode);

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public string EmptyLinkText => Res.Get<PublicControlsResources>().SetCss;

    /// <summary>
    /// Returns a comma separated string with all the mediaTypes set as a bitmask
    /// </summary>
    /// <param name="mediaType">Type of the media.</param>
    internal static string ConvertMediaTypeToString(MediaType mediaType)
    {
      Array values = Enum.GetValues(typeof (MediaType));
      if (mediaType == MediaType.all)
        return mediaType.ToString();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (MediaType mediaType1 in values)
      {
        if (mediaType1 != MediaType.all && (mediaType & mediaType1) == mediaType1)
          stringBuilder.AppendFormat("{0}, ", (object) mediaType1);
      }
      if (stringBuilder.Length >= 2)
        stringBuilder.Remove(stringBuilder.Length - 2, 2);
      return stringBuilder.ToString();
    }
  }
}
