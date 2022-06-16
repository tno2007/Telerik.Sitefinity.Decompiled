// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.MultiImageField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Field control for displaying and uploading multiple images
  /// </summary>
  [FieldDefinitionElement(typeof (MultiImageFieldElement))]
  [RequiresDataItem]
  public class MultiImageField : FieldControl, IRequiresDataItem
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.MultiImageField.ascx");
    private IDataItem dataItem;
    private bool? boundOnServer;
    private bool displayEmptyImage = true;
    private const string ajaxUploadScript = "Telerik.Sitefinity.Resources.Scripts.ajaxupload.js";
    private const string jqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private const string multiImageFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.MultiImageField.js";
    private const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";
    private const string uploadServiceUrl = "~/Telerik.Sitefinity.AsyncImageUploadHandler.ashx";
    private const string reqDataItemScriptFileName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js";
    private Type dataFieldType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.MultiImageField" /> class.
    /// </summary>
    public MultiImageField() => this.LayoutTemplatePath = MultiImageField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the name of the provider used to retrieve the default image
    /// </summary>
    public string ProviderNameForDefaultImage { get; set; }

    /// <summary>
    /// Ges or sets the Id of the image used as default image (when there is no ImageId specified).
    /// </summary>
    public Guid DefaultImageId { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider used to retrieve/store the image
    /// </summary>
    public string ImageProviderName { get; set; }

    /// <summary>Gets or sets the maximum image width</summary>
    public int? MaxWidth { get; set; }

    /// <summary>Gets or sets maximum image height</summary>
    public int? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the text of the label for the Replace photo button.
    /// </summary>
    public string ReplacePhotoButtonLabel { get; set; }

    /// <summary>
    /// Gets or sets the text of the label for the Don't upload photo button.
    /// </summary>
    public string CancelUploadButtonLabel { get; set; }

    /// <summary>
    /// Represents the type of the value which the control should display - content link, image id - guid or string - url
    /// </summary>
    [TypeConverter(typeof (StringTypeConverter))]
    public Type DataFieldType
    {
      get => this.dataFieldType;
      set
      {
        if (value == (Type) null)
          value = typeof (string);
        this.dataFieldType = typeof (ContentLink).IsAssignableFrom(value) || !(value != typeof (Guid)) || !(value != typeof (string)) ? value : throw new NotSupportedException("Multi Image field cannot be bound to a value of type {0}".Arrange((object) this.DataFieldType.FullName));
      }
    }

    /// <summary>Gets or sets the default src for the image.</summary>
    /// <value>The default src for the image.</value>
    public virtual string DefaultSrc { get; set; }

    /// <summary>Gets or sets the default value for the image ALT tag.</summary>
    public virtual string DefaultAlt { get; set; }

    /// <summary>
    /// Set to false when you wish to hide the image placeholder
    /// </summary>
    public virtual bool DisplayEmptyImage
    {
      get => this.displayEmptyImage;
      set => this.displayEmptyImage = value;
    }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) null;

    /// <summary>
    /// Gets a reference to the div tag containing the AsyncUpload panel.
    /// </summary>
    protected internal virtual HtmlGenericControl AsyncUploadPanel => this.Container.GetControl<HtmlGenericControl>(nameof (AsyncUploadPanel), this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets a reference to the div tag containing the EditImageProperties panel.
    /// </summary>
    protected internal virtual HtmlGenericControl EditImagePropertiesPanel => this.Container.GetControl<HtmlGenericControl>(nameof (EditImagePropertiesPanel), this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets a reference to the ReplaceImage button.</summary>
    protected internal virtual LinkButton ReplaceImageButton => this.Container.GetControl<LinkButton>("replaceImage", false);

    /// <summary>
    /// Gets a reference to the literal inside the ReplaceImage button.
    /// </summary>
    protected internal virtual Literal ReplaceImageButtonLiteral => this.Container.GetControl<Literal>("AddImageLiteral", false);

    protected internal virtual SingleMediaContentItemDialog AsyncImageSelector => this.Container.GetControl<SingleMediaContentItemDialog>("asyncImageSelector", false);

    protected internal virtual EditImagePropertiesDialog ImageProperties => this.Container.GetControl<EditImagePropertiesDialog>("imageProperties", false);

    /// <summary>
    /// The DataItem field from the IRequiresDataItem interface.
    /// </summary>
    public IDataItem DataItem
    {
      get => this.dataItem;
      set => this.dataItem = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.DisplayMode == FieldDisplayMode.Read)
        return;
      if (!string.IsNullOrEmpty(this.ReplacePhotoButtonLabel))
        this.ReplaceImageButtonLiteral.Text = this.ReplacePhotoButtonLabel;
      this.AsyncUploadPanel.Visible = true;
      this.EditImagePropertiesPanel.Visible = true;
      this.AsyncImageSelector.Style.Add("display", "none");
      this.ImageProperties.Style.Add("display", "none");
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IMultiImageFieldDefinition imageFieldDefinition))
        return;
      this.ImageProviderName = imageFieldDefinition.ImageProviderName;
      this.ProviderNameForDefaultImage = imageFieldDefinition.ProviderNameForDefaultImage;
      this.DefaultImageId = imageFieldDefinition.DefaultImageId;
      this.MaxWidth = imageFieldDefinition.MaxWidth;
      this.MaxHeight = imageFieldDefinition.MaxHeight;
      this.DataFieldType = imageFieldDefinition.DataFieldType;
      this.DefaultSrc = imageFieldDefinition.DefaultSrc;
      this.DisplayEmptyImage = imageFieldDefinition.DisplayEmptyImage;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_dataFieldType", (object) this.DataFieldType.Name);
      if (!string.IsNullOrEmpty(this.DefaultSrc))
      {
        string str = VirtualPathUtility.IsAppRelative(this.DefaultSrc) ? UrlPath.ResolveAbsoluteUrl(this.DefaultSrc) : this.DefaultSrc;
        controlDescriptor.AddProperty("_defaultSrc", (object) str);
      }
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        if (this.ReplaceImageButton != null)
          controlDescriptor.AddElementProperty("replaceImageButtonElement", this.ReplaceImageButton.ClientID);
        controlDescriptor.AddProperty("_uploadServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Telerik.Sitefinity.AsyncImageUploadHandler.ashx"));
        controlDescriptor.AddProperty("_contentType", (object) typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName);
        controlDescriptor.AddProperty("_providerName", (object) this.ImageProviderName);
        controlDescriptor.AddProperty("firstItemText", (object) Res.Get<LibrariesResources>().AllItems1);
        controlDescriptor.AddComponentProperty("asyncImageSelector", this.AsyncImageSelector.ClientID);
        controlDescriptor.AddComponentProperty("imageProperties", this.ImageProperties.ClientID);
      }
      controlDescriptor.AddProperty("actionsMenuText", (object) Res.Get<Labels>().Actions);
      controlDescriptor.AddProperty("_imageEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ImageEditorDialog"));
      controlDescriptor.AddProperty("removeText", (object) Res.Get<Labels>().Remove);
      controlDescriptor.AddProperty("viewOriginalText", (object) Res.Get<LibrariesResources>().ViewOriginalSize);
      controlDescriptor.AddProperty("editPropertiesText", (object) Res.Get<Labels>().EditProperties);
      controlDescriptor.AddProperty("setAsPrimaryImageText", (object) Res.Get<LibrariesResources>().SetAsPrimaryImage);
      controlDescriptor.AddProperty("_scalableText", (object) Res.Get<LibrariesResources>().Scalable);
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
      string fullName = typeof (TextField).Assembly.FullName;
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", fullName));
      string name = Telerik.Sitefinity.Configuration.Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", name));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.ajaxupload.js", name));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.MultiImageField.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();
  }
}
