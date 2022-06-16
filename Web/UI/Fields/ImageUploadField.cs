// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ImageUploadField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Images;
using Telerik.Sitefinity.Web.UI.ItemLists;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  ///  Represents a file field control for presenting and managing image files.
  /// </summary>
  [RequiresDataItem]
  public class ImageUploadField : FileField, IRequiresDataItem
  {
    private IDataItem dataItem;
    internal const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ImageUploadField.js";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ImageUploadField.ascx");
    private const string resourceAssemblyName = "Telerik.Sitefinity.Resources";
    private const string itemTemplateName = "ImageDescription";
    private const string imageDesriptionTemplate = "Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.ImageItemMultilingualTemplate.htm";
    private static readonly string serviceUrlBase = VirtualPathUtility.ToAbsolute(VirtualPathUtility.RemoveTrailingSlash("~/Sitefinity/Services/Content/ImageService.svc/"));
    private readonly string getMediaFileLinksServiceUrl = ImageUploadField.serviceUrlBase + "/mediaFileLinks";
    private readonly string copyMediaFileLinkServiceUrl = ImageUploadField.serviceUrlBase + "/copyFileLink";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ImageUploadField" /> class.
    /// </summary>
    public ImageUploadField() => this.LayoutTemplatePath = ImageUploadField.layoutTemplatePath;

    /// <summary>Gets the reference to mediaContentItemsList control.</summary>
    protected new virtual ItemsList MediaContentItemsList => this.GetConditionalControl<ItemsList>("mediaContentItemsList", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference image control which should be displayed
    /// </summary>
    protected internal virtual HtmlImage ImageControl => this.GetConditionalControl<HtmlImage>("img", true);

    /// <summary>
    /// Gets a reference to the RadMenu containing the menu options that initiate the respective commands.
    /// </summary>
    protected internal new virtual RadMenu OptionsMenu => this.Menu.Container.GetControl<RadMenu>("optionsMenu", this.DisplayMode == FieldDisplayMode.Write);

    public new ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    protected internal new virtual MenuList Menu => this.GetConditionalControl<MenuList>("menuList", this.DisplayMode == FieldDisplayMode.Write);

    protected internal new virtual Control MediaContainer => this.GetConditionalControl<Control>("mediaContainer", this.DisplayMode == FieldDisplayMode.Write);

    protected internal new virtual Control RadioButtonList => this.GetConditionalControl<Control>("radioButtonList", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets a reference to the WindowManager contains the ImageEditor dialog.
    /// </summary>
    protected internal virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", this.DisplayMode == FieldDisplayMode.Write);

    protected internal new virtual Label MoreTranslationsLabel => this.GetConditionalControl<Label>("moreTranslationsLbl", this.DisplayMode == FieldDisplayMode.Write);

    protected new virtual HtmlInputRadioButton UploadNewItemButton => this.GetConditionalControl<HtmlInputRadioButton>("uploadNewItemButton", this.DisplayMode == FieldDisplayMode.Write);

    protected new virtual Control CancelUploadImageContainer => this.GetConditionalControl<Control>("cancelUploadContainer", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets or sets the name of the ItemDescription template.
    /// </summary>
    public string MediaContentItemsListDescriptionTemplate { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

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
      if (this.DisplayMode != FieldDisplayMode.Write)
        return;
      this.InstantiateTemplate("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.ImageItemMultilingualTemplate.htm", "ImageDescription");
      this.PrepareMenuItems();
    }

    protected override void PrepareMenuItems()
    {
      this.Menu.Items.Add(new MenuListItem(Res.Get<LibrariesResources>().OpenTheFile, "openFile"));
      this.Menu.Items.Add(new MenuListItem(Res.Get<LibrariesResources>().ViewAllThumbnailSizes, "allSizes"));
      this.Menu.Items.Add(new MenuListItem(Res.Get<LibrariesResources>().CropResizeRotate, "editImage"));
      this.Menu.Items.Add(new MenuListItem(Res.Get<LibrariesResources>().ReplaceFile, "replace"));
      if (!SystemManager.CurrentContext.AppSettings.Current.Multilingual)
        return;
      this.Menu.Items.Add(new MenuListItem(Res.Get<LibrariesResources>().UseAnotherFile, "useAnother"));
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_mediaContentBinderServiceUrl", (object) this.getMediaFileLinksServiceUrl);
      controlDescriptor.AddProperty("_copyMediaFileLinkServiceUrl", (object) this.copyMediaFileLinkServiceUrl);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("imageControl", this.ImageControl.ClientID);
      if (this.WindowManager != null)
        controlDescriptor.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      if (!string.IsNullOrEmpty(MediaContentExtensions.UrlVersionQueryParam))
        controlDescriptor.AddProperty("_urlVersionQueryParam", (object) MediaContentExtensions.UrlVersionQueryParam);
      controlDescriptor.AddProperty("_thumbnailListDialogCommand", (object) "viewAllThumbnailSizes");
      if (this.DisplayMode != FieldDisplayMode.Write)
        return scriptDescriptors;
      controlDescriptor.AddElementProperty("moreTranslationsLbl", this.MoreTranslationsLabel.ClientID);
      controlDescriptor.AddElementProperty("mediaContainer", this.MediaContainer.ClientID);
      controlDescriptor.AddElementProperty("radioButtonList", this.RadioButtonList.ClientID);
      controlDescriptor.AddElementProperty("uploadNewItemButton", this.UploadNewItemButton.ClientID);
      controlDescriptor.AddComponentProperty("mediaContentItemsList", this.MediaContentItemsList.ClientID);
      controlDescriptor.AddComponentProperty("mediaContentBinder", this.MediaContentItemsList.Binder.ClientID);
      controlDescriptor.AddComponentProperty("menuList", this.OptionsMenu.ClientID);
      return scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = this.GetType().Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItem.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ImageUploadField.js", fullName)
      };
    }
  }
}
