// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ThumbnailField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  ///  Represents a field control that is displaying thumbnail and has the opportunity to manual update it.
  /// </summary>
  public class ThumbnailField : ImageField
  {
    private string thumbnailDialogUrl;
    private string uploadUrl = "~/Telerik.Sitefinity.ThumbnailUploadHandler.ashx";
    private const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ThumbnailField.js";
    private const string selfExecutableScript = "Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ThumbnailField.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ThumbnailField" /> class.
    /// </summary>
    public ThumbnailField() => this.LayoutTemplatePath = ThumbnailField.layoutTemplatePath;

    /// <summary>
    /// Gets the reference to image control container that will contain thumbnail.
    /// </summary>
    /// <value>The instance of <see cref="T:System.Web.UI.WebControls.Image" /> class.</value>
    protected virtual HtmlGenericControl ThumbnailContainer => this.GetConditionalControl<HtmlGenericControl>("thumbnailContainer", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to image control that will contain thumbnail.
    /// </summary>
    /// <value>The instance of <see cref="T:System.Web.UI.HtmlControls.HtmlImage" /> class.</value>
    protected virtual HtmlImage ThumbnailImage => this.GetConditionalControl<HtmlImage>("img", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the url of the new thumbnail dialog.</summary>
    protected string ThumbnailDialogUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.thumbnailDialogUrl))
        {
          this.thumbnailDialogUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/" + typeof (ThumbnailMediaPlayerDialog).Name);
          this.thumbnailDialogUrl = VirtualPathUtility.AppendTrailingSlash(this.thumbnailDialogUrl);
        }
        return this.thumbnailDialogUrl;
      }
    }

    /// <summary>Gets the reference thumbnail selector dialog.</summary>
    /// <value>The thumbnail selector dialog.</value>
    protected internal virtual RadWindow ThumbnailSelectorDialog => this.Container.GetControl<RadWindow>("thumbnailSelectorDialog", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => this.GetConditionalControl<WebControl>("titleControl", true);

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the url of thumbnail upload handler.</summary>
    protected string UploadUrl => this.uploadUrl;

    /// <summary>Gets the reference to the command bar control.</summary>
    /// <value>The command bar.</value>
    protected virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ViewOriginalSizeButton.Visible = false;
      if (!(this.TitleControl is Label titleControl))
        return;
      titleControl.Text = this.Title;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition) => base.Configure(definition);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_thumbnailImage", (object) this.ThumbnailImage.ClientID);
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddProperty("_serviceBaseUrl", (object) UrlPath.ResolveAbsoluteUrl(this.UploadUrl));
        controlDescriptor.AddProperty("_thumbnailDialogUrl", (object) this.ThumbnailDialogUrl);
        controlDescriptor.AddComponentProperty("thumbnailSelectorDialog", this.ThumbnailSelectorDialog.ClientID);
        controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (ThumbnailField).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ThumbnailField.js", fullName));
      if (this.DisplayMode == FieldDisplayMode.Write)
        scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.ISelfExecutableField.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
