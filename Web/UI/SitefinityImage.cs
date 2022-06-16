// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityImage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Control which wraps up the standard <see cref="T:System.Web.UI.WebControls.Image" /> control
  /// </summary>
  public class SitefinityImage : SimpleView
  {
    private ITemplate layoutTemplate;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.SitefinityImage.ascx");

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.Image" /> control
    /// </summary>
    /// <value>The Image item.</value>
    protected virtual Image Image1 => this.Container.GetControl<Image>(nameof (Image1), true);

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template
    /// this property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SitefinityImage.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the layout template for the control.</summary>
    /// <value>The layout template of the control.</value>
    public override ITemplate LayoutTemplate
    {
      get
      {
        if (this.layoutTemplate == null)
          this.layoutTemplate = ControlUtilities.GetTemplate(this.LayoutTemplatePath, (string) null, Config.Get<ControlsConfig>().ResourcesAssemblyInfo, (string) null);
        return this.layoutTemplate;
      }
    }

    /// <summary>
    /// Gets or sets the alternate text displayed in the System.Web.UI.WebControls.Image
    ///     control when the image is unavailable. Browsers that support the ToolTips
    ///     feature display this text as a ToolTip.
    ///  Returns:
    ///     The alternate text displayed in the System.Web.UI.WebControls.Image control
    ///     when the image is unavailable.
    /// </summary>
    [Localizable(true)]
    [Bindable(true)]
    [DefaultValue("")]
    public virtual string AlternateText
    {
      get => this.Image1.AlternateText;
      set => this.Image1.AlternateText = value;
    }

    /// <summary>
    /// Gets or sets the location to a detailed description for the image.
    /// Returns:
    ///     The URL for the file that contains a detailed description for the image.
    ///     The default is an empty string ("").
    /// </summary>
    [UrlProperty]
    [DefaultValue("")]
    [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public virtual string DescriptionUrl
    {
      get => this.Image1.DescriptionUrl;
      set => this.Image1.DescriptionUrl = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control generates an alternate
    ///     text attribute for an empty string value.
    ///  Returns:
    ///     true if the control generates the alternate text attribute for an empty string
    ///     value; otherwise, false. The default is false.
    /// </summary>
    [DefaultValue(false)]
    public virtual bool GenerateEmptyAlternateText
    {
      get => this.Image1.GenerateEmptyAlternateText;
      set => this.Image1.GenerateEmptyAlternateText = value;
    }

    /// <summary>
    /// Gets or sets the alignment of the System.Web.UI.WebControls.Image control
    ///     in relation to other elements on the Web page.
    /// Returns:
    ///     One of the System.Web.UI.WebControls.ImageAlign values. The default is NotSet.
    /// </summary>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// The specified value is not one of the System.Web.UI.WebControls.ImageAlign values.
    /// </exception>
    public virtual ImageAlign ImageAlign
    {
      get => this.Image1.ImageAlign;
      set => this.Image1.ImageAlign = value;
    }

    /// <summary>
    /// Gets or sets the location of an image to display in the System.Web.UI.WebControls.Image
    ///     control.
    /// Returns:
    ///     The location of an image to display in the System.Web.UI.WebControls.Image
    ///     control.
    /// </summary>
    [DefaultValue("")]
    [Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [UrlProperty]
    [Bindable(true)]
    public virtual string ImageUrl
    {
      get => this.Image1.ImageUrl;
      set => this.Image1.ImageUrl = value;
    }

    /// <summary>
    /// Overridden. Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="controlContainer">The container that will host the template of this control.</param>
    protected override void InitializeControls(GenericContainer controlContainer)
    {
    }

    /// <summary>
    /// Used to parse dynamic url link and convert it to
    /// url that can be shown in the browser
    /// </summary>
    /// <param name="writer"></param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!string.IsNullOrEmpty(this.ImageUrl) && (this.ImageUrl.StartsWith("~/") || this.ImageUrl.StartsWith("[")))
        this.ImageUrl = SitefinityImage.ResolveImageUrl((Control) this, this.ImageUrl);
      base.Render(writer);
    }

    private static string ResolveImageUrl(Control control, string val)
    {
      if (val.StartsWith("~/"))
        return control.ResolveUrl(val);
      int num = val.IndexOf("]");
      if (num < 0)
        return val;
      string providerName = val.Substring(1, num - 1);
      Guid id = new Guid(val.Substring(num + 1));
      ContentManager manager = ContentManager.GetManager();
      if (manager.GetProviderNames(ProviderBindingOptions.NoFilter).Contains<string>(providerName))
      {
        if (manager.Provider.Name != providerName)
          manager = ContentManager.GetManager(providerName);
        manager.GetContent(id);
      }
      return "Item not found: [" + providerName + "]" + (object) id;
    }
  }
}
