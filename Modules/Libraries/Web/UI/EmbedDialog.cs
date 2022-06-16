// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>Dialog for embedding images, videos and documents.</summary>
  public class EmbedDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.EmbedDialog.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EmbedDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the reference to the EmbedControl</summary>
    protected virtual EmbedControl EmbedControl => this.Container.GetControl<EmbedControl>("embedControl", true);

    /// <summary>Gets the dialog instance pageId.</summary>
    /// <value>The dialog instance pageId.</value>
    protected virtual HiddenField EmbedControlInstanceId => this.Container.GetControl<HiddenField>("embedControlInstanceId", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string str = this.Page.Request.QueryString["mode"];
      if (!(str == "documents"))
      {
        if (str == "videos")
        {
          this.EmbedControl.SizesChoiceFieldDefinition = (IChoiceFieldDefinition) VideosDefinitions.DefineEmbedVideoSizesChoiceField(new ConfigElement(false));
          this.EmbedControl.CustomizeButtonTitle = Res.Get<VideosResources>().CustomizeEmbeddedVideo;
        }
        else
        {
          this.EmbedControl.EmbedStringTemplate = "<img width=\"{0}\" height=\"{1}\" src=\"{2}\" alt=\"{3}\"/>";
          this.EmbedControl.SizesChoiceFieldDefinition = (IChoiceFieldDefinition) ImagesDefinitions.DefineEmbedImageSizesChoiceField(new ConfigElement(false));
        }
      }
      else
        this.EmbedControl.HideEmbedTextBox = true;
      this.EmbedControl.Mode = str;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.EmbedControlInstanceId.Value = this.EmbedControl.ClientID;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => base.GetScriptDescriptors();

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => base.GetScriptReferences();
  }
}
