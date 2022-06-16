// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.InsertEditImageView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Component that represents a simple script view for changing image properties, and editing and inserting the image.
  /// </summary>
  public class InsertEditImageView : AjaxDialogBase
  {
    private const string viewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.InsertEditImageView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.InsertEditImageView.ascx");
    private const string imageServiceUrl = "~/Sitefinity/Services/Content/ImageService.svc";
    private bool _showAlignmentOptions = true;
    private string _dateFormat = "dddd, MMM d, yyyy";
    private static string replaceWith = "-";
    private static bool toLower = true;
    private static bool trim = true;
    private string _alignment = "None";
    private Telerik.Sitefinity.Libraries.Model.Image imageInfo;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? InsertEditImageView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The tag name of the base HTML element rendered by the control.
    /// </summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the DateTime format of the Label displaying the uploaded time of the image.
    /// </summary>
    public virtual string DateFormat
    {
      get => this._dateFormat;
      set => this._dateFormat = value;
    }

    /// <summary>The id of the image that will be displayed.</summary>
    public Guid ImageId { get; set; }

    private Telerik.Sitefinity.Libraries.Model.Image ImageInfo
    {
      get
      {
        if (this.imageInfo == null && this.ImageId != Guid.Empty)
          this.imageInfo = LibrariesManager.GetManager(this.ProviderName).GetImage(this.ImageId);
        return this.imageInfo;
      }
    }

    /// <summary>Gets or sets the Value of the AltTextField.</summary>
    public string AlternateText { get; set; }

    /// <summary>Gets or sets the name of the thumbnail profile.</summary>
    /// <value>The name of the thumbnail.</value>
    public string ThumbnailName { get; set; }

    /// <summary>
    /// Gets or sets values of the margin options (top, bottom, left, right).
    /// </summary>
    public int? MarginTop { get; set; }

    public int? MarginBottom { get; set; }

    public int? MarginLeft { get; set; }

    public int? MarginRight { get; set; }

    /// <summary>Specifies if the alignment options will be displayed</summary>
    public virtual bool ShowAlignmentOptions
    {
      get => this._showAlignmentOptions;
      set => this._showAlignmentOptions = value;
    }

    /// <summary>
    /// Gets or sets the value of the Alignment RadioButton List.
    /// </summary>
    public string Alignment
    {
      get => this._alignment;
      set => this._alignment = value;
    }

    /// <summary>
    /// Gets or sets a bool value that indicates whether clicking the image will open the original image.
    /// </summary>
    public virtual bool OpenOriginalImageOnClick { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Get or sets the type of the control</summary>
    public virtual string ViewType { get; set; }

    /// <summary>Gets a reference to the thumbnail Image</summary>
    protected virtual SfImage Image => this.Container.GetControl<SfImage>("imageThumb", true);

    /// <summary>Gets a reference to the Size Label.</summary>
    protected virtual Label ImageExtensionLabel => this.Container.GetControl<Label>("lblExtension", true);

    /// <summary>Gets a reference to the Size Label.</summary>
    protected virtual Label SizeLabel => this.Container.GetControl<Label>("lblSize", true);

    /// <summary>Gets a reference to the Date Uploaded Label.</summary>
    protected virtual Label DateLabel => this.Container.GetControl<Label>("lblDateUploaded", true);

    /// <summary>
    /// Gets a reference to the Button that changes the image.
    /// </summary>
    protected virtual WebControl ChangeImageButton => this.Container.GetControl<WebControl>("changeImageButton", true);

    /// <summary>
    /// Gets a reference to the Button that initiates editing of the image.
    /// </summary>
    protected virtual WebControl EditImageButton => this.Container.GetControl<WebControl>("editImageButton", true);

    /// <summary>
    /// Represents the textbox containing the AlternativeText to be set to the image.
    /// </summary>
    protected virtual TextField AltTextField => this.Container.GetControl<TextField>("altTextField", true);

    /// <summary>Gets the title text field.</summary>
    /// <value>The title text field.</value>
    protected virtual TextField TitleTextField => this.Container.GetControl<TextField>("titleTextField", true);

    /// <summary>The container with the alignments options</summary>
    protected virtual Control AlignmentOptionsContainer => this.Container.GetControl<Control>("AlignPanel", true);

    /// <summary>
    /// Represents the drop down list from which the image-size is chosen.
    /// </summary>
    protected virtual ChoiceField SizesChoiceField => this.Container.GetControl<ChoiceField>("sizesChoiceField", true);

    /// <summary>
    /// Represents the checkbox which when checked will make the resized item to open the original one.
    /// </summary>
    protected virtual ChoiceField OpenOriginalChoiceField => this.Container.GetControl<ChoiceField>("openOriginalChoiceField", true);

    /// <summary>
    /// Represents the manager that controls the localization strings.
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Represents the window manager which opens the window with the ImageEditor
    /// </summary>
    protected virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Represents the button that expands/collapses the more options group
    /// </summary>
    protected virtual HtmlAnchor MarginExpander => this.Container.GetControl<HtmlAnchor>("marginExpander", true);

    /// <summary>Represents the container for margin elements</summary>
    protected virtual Control MarginSection => this.Container.GetControl<Control>("marginSection", true);

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

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (InsertEditImageView).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("extensionLabel", this.ImageExtensionLabel.ClientID);
      controlDescriptor.AddElementProperty("sizeLabel", this.SizeLabel.ClientID);
      controlDescriptor.AddElementProperty("dateLabel", this.DateLabel.ClientID);
      controlDescriptor.AddElementProperty("changeImageButton", this.ChangeImageButton.ClientID);
      controlDescriptor.AddElementProperty("editImageButton", this.EditImageButton.ClientID);
      controlDescriptor.AddElementProperty("imageElement", this.Image.ClientID);
      controlDescriptor.AddComponentProperty("customImageSizeViewControl", this.CustomImageSizeViewControl.ClientID);
      controlDescriptor.AddProperty("alignment", (object) this.Alignment);
      controlDescriptor.AddProperty("dateFormat", (object) this.DateFormat);
      controlDescriptor.AddProperty("_imageServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ImageService.svc"));
      controlDescriptor.AddProperty("_regularExpression", (object) DefinitionsHelper.UrlRegularExpressionFilter);
      controlDescriptor.AddProperty("_replaceWith", (object) InsertEditImageView.replaceWith);
      controlDescriptor.AddProperty("_toLower", (object) InsertEditImageView.toLower);
      controlDescriptor.AddProperty("_trim", (object) InsertEditImageView.trim);
      if (!string.IsNullOrEmpty(MediaContentExtensions.UrlVersionQueryParam))
        controlDescriptor.AddProperty("_urlVersionQueryParam", (object) MediaContentExtensions.UrlVersionQueryParam);
      controlDescriptor.AddProperty("_thumbnailExtensionPrefix", (object) LibraryRoute.ThumbnailExtensionPrefix);
      controlDescriptor.AddProperty("_imageEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ImageEditorDialog"));
      if (!string.IsNullOrEmpty(this.ProviderName))
        controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      controlDescriptor.AddComponentProperty("titleTxt", this.TitleTextField.ClientID);
      controlDescriptor.AddComponentProperty("altTextField", this.AltTextField.ClientID);
      controlDescriptor.AddComponentProperty("sizesChoiceField", this.SizesChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("openOriginalChoiceField", this.OpenOriginalChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      controlDescriptor.AddElementProperty("marginExpander", this.MarginExpander.ClientID);
      controlDescriptor.AddElementProperty("marginSection", this.MarginSection.ClientID);
      controlDescriptor.AddComponentProperty("marginTopField", this.MarginTopField.ClientID);
      controlDescriptor.AddComponentProperty("marginBottomField", this.MarginBottomField.ClientID);
      controlDescriptor.AddComponentProperty("marginLeftField", this.MarginLeftField.ClientID);
      controlDescriptor.AddComponentProperty("marginRightField", this.MarginRightField.ClientID);
      controlDescriptor.AddProperty("libraryType", (object) typeof (Album).FullName);
      string virtualPath = string.Format("~/{0}", (object) "Sitefinity/Services/ThumbnailService.svc");
      controlDescriptor.AddProperty("thumbnailServiceUrl", (object) VirtualPathUtility.ToAbsolute(virtualPath));
      controlDescriptor.AddProperty("viewType", (object) this.ViewType);
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
      List<ScriptReference> list = base.GetScriptReferences().ToList<ScriptReference>();
      list.Add(new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.InsertEditImageView.js", typeof (InsertEditImageView).Assembly.GetName().ToString()));
      return (IEnumerable<ScriptReference>) list;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      LibrariesManager manager = LibrariesManager.GetManager(this.ProviderName);
      if (this.ImageInfo != null)
      {
        this.Image.ImageUrl = "~" + manager.Provider.GetItemUrl((ILocatable) this.ImageInfo) + this.ImageInfo.Extension;
        this.DateLabel.Text = this.ImageInfo.DateCreated.ToString(this.DateFormat);
        this.ImageExtensionLabel.Text = this.ImageInfo.Extension;
        this.SizeLabel.Text = this.GetSizeString(this.ImageInfo.TotalSize);
        this.AltTextField.Value = string.IsNullOrEmpty(this.AlternateText) ? (object) this.ImageInfo.AlternativeText.ToString() : (object) this.AlternateText;
        this.AltTextField.CharacterCounterDescriptionLabel.Text = string.Format(this.AltTextField.CharacterCounterDescription, (object) this.AltTextField.RecommendedCharactersCount);
        this.TitleTextField.Value = string.IsNullOrEmpty(this.ToolTip) ? (object) this.ImageInfo.Title.ToString() : (object) this.ToolTip;
      }
      this.AlignmentOptionsContainer.Visible = this.ShowAlignmentOptions;
      this.OpenOriginalChoiceField.Value = (object) this.OpenOriginalImageOnClick.ToString().ToLower();
      this.OpenOriginalChoiceField.Choices[0].Text = string.Format(Res.Get<LibrariesResources>().ClickingTheResizedItemOpensTheOriginal, (object) Res.Get<ImagesResources>().Image.ToLower(), (object) Res.Get<ImagesResources>().Image.ToLower());
    }

    private string GetSizeString(long totalSize) => totalSize > 1048576L ? (totalSize / 1048576L).ToString() + " " + Res.Get<LibrariesResources>().Mb : (totalSize / 1024L).ToString() + " " + Res.Get<LibrariesResources>().Kb;
  }
}
