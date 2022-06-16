// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A control for configuring how to display a media content item.
  /// </summary>
  public class SingleMediaContentItemSettingsView : SimpleScriptView
  {
    internal const string Script = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemSettingsView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.SingleMediaContentItemSettingsView.ascx");
    private bool showAlignmentOptions;
    private string alignment = "None";

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SingleMediaContentItemSettingsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the alignment options will be displayed
    /// </summary>
    public virtual bool ShowAlignmentOptions
    {
      get => this.showAlignmentOptions;
      set => this.showAlignmentOptions = value;
    }

    /// <summary>
    /// Gets or sets the value of the Alignment RadioButton List.
    /// </summary>
    public string Alignment
    {
      get => this.alignment;
      set => this.alignment = value;
    }

    /// <summary>
    /// Gets or sets the display video resizing options control.
    /// </summary>
    /// <value>The display resizing options control.</value>
    public bool DisplayResizingOptionsControl { get; set; }

    /// <summary>Get or sets the type of the control</summary>
    public virtual string ViewType { get; set; }

    /// <summary>
    /// Gets or sets the full name of the media item type that this view will work with.
    /// </summary>
    /// <value>The name of the media item type.</value>
    public string MediaItemTypeName { get; set; }

    /// <summary>Gets a reference to the Size Label.</summary>
    protected virtual Label SizeLabel => this.Container.GetControl<Label>("lblSize", true);

    /// <summary>Gets a reference to the Date Uploaded Label.</summary>
    protected virtual Label DateLabel => this.Container.GetControl<Label>("lblDateUploaded", true);

    /// <summary>
    /// Represents the textbox containing the custom Width value.
    /// </summary>
    protected virtual TextBox TextBoxWidth => this.Container.GetControl<TextBox>("txtWidth", true);

    /// <summary>
    /// Represents the textbox containing the custom Height value.
    /// </summary>
    protected virtual TextBox TextBoxHeight => this.Container.GetControl<TextBox>("txtHeight", true);

    /// <summary>The container with the alignments options</summary>
    protected virtual Control AlignmentOptionsContainer => this.Container.GetControl<Control>("AlignPanel", true);

    /// <summary>
    /// Represents the drop down list from which the image-size is chosen.
    /// </summary>
    protected virtual ChoiceField SizesChoiceField => this.Container.GetControl<ChoiceField>("sizesChoiceField", true);

    /// <summary>
    /// Represents the DIV containing the Width and Height textboxes.
    /// </summary>
    protected virtual HtmlGenericControl DivWidthHeight => this.Container.GetControl<HtmlGenericControl>("divWH", true);

    /// <summary>
    /// Represents the checkbox that constrains the ratio between the width and the height.
    /// </summary>
    protected virtual HtmlInputCheckBox TieRatioCheckBox => this.Container.GetControl<HtmlInputCheckBox>("tieRatio", true);

    /// <summary>
    /// Represents the checkbox which when checked will make the resized item to open the original one.
    /// </summary>
    protected virtual ChoiceField OpenOriginalChoiceField => this.Container.GetControl<ChoiceField>("openOriginalChoiceField", true);

    /// <summary>
    /// Represents the manager that controls the localization strings.
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Represents the textbox containing the top margin property to be set to the image.
    /// </summary>
    protected virtual TextField MarginTopField => this.Container.GetControl<TextField>("marginTopField", true);

    /// <summary>
    /// Represents the textbox containing the bottom margin property to be set to the image.
    /// </summary>
    protected virtual TextField MarginBottomField => this.Container.GetControl<TextField>("marginBottomField", true);

    /// <summary>
    /// Represents the textbox containing the right margin property to be set to the image.
    /// </summary>
    protected virtual TextField MarginRightField => this.Container.GetControl<TextField>("marginRightField", true);

    /// <summary>
    /// Represents the textbox containing the left margin property to be set to the image.
    /// </summary>
    protected virtual TextField MarginLeftField => this.Container.GetControl<TextField>("marginLeftField", true);

    /// <summary>The control that manages the custom image size case.</summary>
    protected virtual CustomImageSizeView CustomImageSizeViewControl => this.Container.GetControl<CustomImageSizeView>("customImageSizeView", true);

    /// <summary>Gets the reference to resizingOptionsControl.</summary>
    protected virtual ResizingOptionsControl ResizingOptionsControl => this.Container.GetControl<ResizingOptionsControl>("resizingOptionsControl", true);

    /// <summary>Gets the video aspect ratio.</summary>
    /// <value>The video aspect ratio.</value>
    protected virtual AspectRatioControl VideoAspectRatio => this.Container.GetControl<AspectRatioControl>("videoAspectRatio", true);

    /// <summary>
    /// Gets the the wrapper which contains all controls for setting image properties.
    /// </summary>
    /// <value>The image settings wrapper.</value>
    protected virtual HtmlGenericControl ImageSettingsWrapper => this.Container.GetControl<HtmlGenericControl>("imageSettingsWrapper", true);

    /// <summary>
    /// Gets the the wrapper which contains all controls for setting video properties.
    /// </summary>
    /// <value>The video settings wrapper.</value>
    protected virtual HtmlGenericControl VideoSettingsWrapper => this.Container.GetControl<HtmlGenericControl>("videoSettingsWrapper", true);

    /// <summary>Gets the button for expanding the Margin section.</summary>
    protected virtual LinkButton ExpandMarginButton => this.Container.GetControl<LinkButton>("btnMargin", false);

    /// <summary>Gets the margin section container.</summary>
    /// <value>The narrow selection container.</value>
    protected virtual Control MarginSectionContainer => this.Container.GetControl<Control>("marginSection", false);

    /// <summary>
    /// Gets the the wrapper which contains all controls for setting document properties.
    /// </summary>
    /// <value>The video settings wrapper.</value>
    protected virtual HtmlGenericControl DocumentSettingsWrapper => this.Container.GetControl<HtmlGenericControl>("documentSettingsWrapper", true);

    /// <summary>Radio button representing the big icons thumbnails</summary>
    protected internal virtual RadioButton BigIconsRadioButton => this.Container.GetControl<RadioButton>("rbBigIcons", true);

    /// <summary>Radio button representing the small icons thumbnails</summary>
    protected internal virtual RadioButton SmallIconsRadioButton => this.Container.GetControl<RadioButton>("rbSmallIcons", true);

    /// <summary>
    /// Radio button that disables the showing of the thumbnails
    /// </summary>
    protected internal virtual RadioButton NoIconsRadioButton => this.Container.GetControl<RadioButton>("rbNoIcons", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor descriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      switch (this.GetViewMode())
      {
        case EditorExternalDialogModes.NotSet:
        case EditorExternalDialogModes.Image:
          this.AddImageControlsDescriptors(descriptor);
          break;
        case EditorExternalDialogModes.Document:
          descriptor.AddProperty("_iconSizesRadioButtonsClientIDs", (object) new string[3]
          {
            this.BigIconsRadioButton.ClientID,
            this.SmallIconsRadioButton.ClientID,
            this.NoIconsRadioButton.ClientID
          });
          break;
        case EditorExternalDialogModes.Media:
          if (this.DisplayResizingOptionsControl)
          {
            descriptor.AddComponentProperty("resizingOptionsControl", this.ResizingOptionsControl.ClientID);
            break;
          }
          descriptor.AddComponentProperty("videoAspectRatio", this.VideoAspectRatio.ClientID);
          break;
      }
      descriptor.AddProperty("viewMode", (object) this.GetViewMode().ToString());
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        descriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemSettingsView.js", typeof (SingleMediaContentItemSettingsView).Assembly.GetName().ToString())
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      this.VideoAspectRatio.Visible = false;
      switch (this.GetViewMode())
      {
        case EditorExternalDialogModes.NotSet:
        case EditorExternalDialogModes.Image:
          this.VideoSettingsWrapper.Visible = false;
          this.DocumentSettingsWrapper.Visible = false;
          this.ImageSettingsWrapper.Visible = true;
          break;
        case EditorExternalDialogModes.Document:
          this.VideoSettingsWrapper.Visible = false;
          this.ImageSettingsWrapper.Visible = false;
          this.DocumentSettingsWrapper.Visible = true;
          break;
        case EditorExternalDialogModes.Media:
          this.ImageSettingsWrapper.Visible = false;
          this.DocumentSettingsWrapper.Visible = false;
          this.VideoSettingsWrapper.Visible = true;
          if (this.DisplayResizingOptionsControl)
          {
            this.ResizingOptionsControl.ItemName = Res.Get<VideosResources>().Video;
            this.ResizingOptionsControl.ItemsName = Res.Get<VideosResources>().Videos;
            this.ResizingOptionsControl.ShowOpenOriginalSizeCheckBox = false;
            break;
          }
          this.ResizingOptionsControl.Visible = false;
          this.VideoAspectRatio.Visible = true;
          break;
      }
      this.AlignmentOptionsContainer.Visible = this.ShowAlignmentOptions;
      this.OpenOriginalChoiceField.Choices[0].Text = string.Format(Res.Get<LibrariesResources>().ClickingTheResizedItemOpensTheOriginal, (object) Res.Get<ImagesResources>().Image.ToLower(), (object) Res.Get<ImagesResources>().Image.ToLower());
    }

    private EditorExternalDialogModes GetViewMode()
    {
      if (this.MediaItemTypeName == typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName)
        return EditorExternalDialogModes.Image;
      if (this.MediaItemTypeName == typeof (Video).FullName)
        return EditorExternalDialogModes.Media;
      return this.MediaItemTypeName == typeof (Document).FullName ? EditorExternalDialogModes.Document : EditorExternalDialogModes.NotSet;
    }

    private void AddImageControlsDescriptors(ScriptControlDescriptor descriptor)
    {
      descriptor.AddProperty("alignment", (object) this.Alignment);
      descriptor.AddComponentProperty("sizesChoiceField", this.SizesChoiceField.ClientID);
      descriptor.AddComponentProperty("customImageSizeViewControl", this.CustomImageSizeViewControl.ClientID);
      descriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      descriptor.AddComponentProperty("openOriginalChoiceField", this.OpenOriginalChoiceField.ClientID);
      descriptor.AddComponentProperty("marginTopField", this.MarginTopField.ClientID);
      descriptor.AddComponentProperty("marginBottomField", this.MarginBottomField.ClientID);
      descriptor.AddComponentProperty("marginLeftField", this.MarginLeftField.ClientID);
      descriptor.AddComponentProperty("marginRightField", this.MarginRightField.ClientID);
      descriptor.AddProperty("libraryType", (object) typeof (Album).FullName);
      string virtualPath1 = string.Format("~/{0}", (object) "Sitefinity/Services/ThumbnailService.svc");
      descriptor.AddProperty("thumbnailServiceUrl", (object) VirtualPathUtility.ToAbsolute(virtualPath1));
      string virtualPath2 = string.Format("~/{0}", (object) "Sitefinity/Services/Content/BlobStorage.svc");
      descriptor.AddProperty("blobStorageServiceUrl", (object) VirtualPathUtility.ToAbsolute(virtualPath2));
      descriptor.AddProperty("viewType", (object) this.ViewType);
      if (this.ExpandMarginButton != null)
        descriptor.AddElementProperty(this.ExpandMarginButton.ID, this.ExpandMarginButton.ClientID);
      if (this.MarginSectionContainer == null)
        return;
      descriptor.AddElementProperty(this.MarginSectionContainer.ID, this.MarginSectionContainer.ClientID);
    }
  }
}
