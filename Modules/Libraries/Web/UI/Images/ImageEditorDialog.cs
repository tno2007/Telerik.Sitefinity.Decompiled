// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImageEditorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;
using Telerik.Web.UI.ImageEditor;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Images
{
  /// <summary>
  /// Represents a modal dialog that loads an Image Editor control.
  /// </summary>
  public class ImageEditorDialog : AjaxDialogBase
  {
    private Telerik.Sitefinity.Libraries.Model.Image _image;
    private static string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.ImageEditorDialog.ascx");
    private static string script = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ImageEditorDialog.js";
    private static string saveAsScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ImageEditorSaveAsDialog.js";
    private bool shouldLoadSaveAsScript;

    /// <summary>Creates new instance of the ImageEditor dialog.</summary>
    public ImageEditorDialog()
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      this.ImageId = currentHttpContext.Request.QueryString["Id"];
      if (this.ImageId.Length != 36)
        this.ImageId = "00000000-0000-0000-0000-000000000000";
      this.Status = currentHttpContext.Request.QueryString[nameof (Status)];
      this.ProviderName = currentHttpContext.Request.QueryString["provider"];
    }

    /// <summary>
    /// The tag name of the base HTML element rendered by the control.
    /// </summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or the image that is loaded within the ImageEditor
    /// </summary>
    public Telerik.Sitefinity.Libraries.Model.Image Image
    {
      get
      {
        if (this._image == null)
          this._image = this.GetImage();
        return this._image;
      }
    }

    /// <summary>The ID of the image to load</summary>
    public string ImageId { get; set; }

    /// <summary>
    /// Indicates the status of the image: is it Live, Temp, Master
    /// </summary>
    public string Status { get; set; }

    /// <summary>Gets or sets the name of the libraries provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => base.LayoutTemplatePath ?? ImageEditorDialog.layoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Represents the editor for modifying images.</summary>
    public RadImageEditor ImageEditor => this.Container.GetControl<RadImageEditor>("theImageEditor", true);

    /// <summary>The container for the title value of the dialog</summary>
    public Literal DialogTitle => this.Container.GetControl<Literal>("imageEditorTitle", true);

    /// <summary>Represents the Save button.</summary>
    public HyperLink SaveLink => this.Container.GetControl<HyperLink>("saveLink", true);

    /// <summary>Represents the SaveAs button.</summary>
    public HyperLink SaveAsLink => this.Container.GetControl<HyperLink>("saveAsLink", true);

    /// <summary>Represents the Cancel button.</summary>
    public HyperLink CancelLink => this.Container.GetControl<HyperLink>("cancelLink", true);

    /// <summary>
    /// Represents a hidden input used for storing the cache key.
    /// </summary>
    public HtmlInputHidden StorageKeyHiddenInput => this.Container.GetControl<HtmlInputHidden>("storageKey", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.Type = this.GetType().FullName;
      controlDescriptor.AddProperty("imageId", (object) this.ImageId);
      controlDescriptor.AddElementProperty("saveButton", this.SaveLink.ClientID);
      controlDescriptor.AddElementProperty("saveAsButton", this.SaveAsLink.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelLink.ClientID);
      controlDescriptor.AddComponentProperty("imageEditor", this.ImageEditor.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference(ImageEditorDialog.script, typeof (ImageEditorDialog).Assembly.FullName),
      new ScriptReference(ImageEditorDialog.saveAsScript, typeof (ImageEditorDialog).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SetCulture();
      this.DialogTitle.Text = string.Format("{0}: {1}", (object) Res.Get<ImagesResources>("EditImage"), (object) HttpUtility.HtmlEncode((string) this.Image.Title));
      this.ImageEditor.ImageManager.ImageProviderTypeName = typeof (CacheImageProvider).AssemblyQualifiedName;
      this.ImageEditor.ImageLoading += new ImageEditorLoadingEventHandler(this.ImageEditor_ImageLoading);
      this.ImageEditor.DialogLoading += new ImageEditorDialogEventHandler(this.ImageEditor_DialogLoading);
      this.ImageEditor.ImageSaving += new ImageEditorSavingEventHandler(this.ImageEditor_ImageSaving);
      this.ImageEditor.ImageUrl = string.Empty;
      if (!(this.ImageEditor.FindControl("eiXHPanel") is RadXmlHttpPanel control))
        return;
      control.ServiceRequest += new XmlHttpPanelEventHandler(this.xmlHttpPanel_ServiceRequest);
    }

    private void SetCulture()
    {
      NameValueCollection nameValueCollection = HttpContext.Current.Request.Params;
      if (!nameValueCollection.Keys.Contains("SF_UI_CULTURE"))
        return;
      SystemManager.CurrentContext.Culture = CultureInfo.GetCultureInfo(nameValueCollection["SF_UI_CULTURE"]);
    }

    private void xmlHttpPanel_ServiceRequest(object sender, RadXmlHttpPanelEventArgs e)
    {
      string str = e.Value;
      if (str.StartsWith("{"))
      {
        int startIndex = str.IndexOf("name") + 7;
        str = str.Substring(startIndex, str.IndexOf('"', startIndex) - startIndex);
      }
      if (!(str == "Save") && !(str == "cancel"))
        return;
      this.ImageEditor.ResetChanges();
    }

    private void ImageEditor_DialogLoading(object sender, ImageEditorDialogEventArgs args)
    {
      if (!(args.DialogName == "SaveAs"))
        return;
      this.shouldLoadSaveAsScript = true;
      args.Panel.Controls.Clear();
      args.Panel.Controls.Add(this.CreateSaveAsDialog());
    }

    private Control CreateSaveAsDialog()
    {
      HtmlGenericControl saveAsDialog = new HtmlGenericControl("div");
      saveAsDialog.Attributes.Add("class", "saveAsContainer");
      Label label1 = new Label();
      label1.Text = Res.Get<ContentResources>("Title");
      label1.CssClass = "sfSaveAsLabel";
      label1.AssociatedControlID = "titleInput";
      Label child1 = label1;
      TextBox textBox = new TextBox();
      textBox.ID = "titleInput";
      textBox.CssClass = "saveAsInput";
      textBox.Text = (string) this.Image.Title + "-1";
      TextBox child2 = textBox;
      Label label2 = new Label();
      label2.Text = Res.Get<ImagesResources>("Album");
      label2.CssClass = "sfSaveAsLabel";
      label2.AssociatedControlID = "librariesDD";
      Label child3 = label2;
      DropDownList dropDownList = new DropDownList();
      dropDownList.ID = "librariesDD";
      dropDownList.CssClass = "saveAsInput";
      dropDownList.DataSource = (object) this.GetManager().GetAlbums();
      dropDownList.DataTextField = "Title";
      dropDownList.DataValueField = "Id";
      dropDownList.SelectedValue = this.Image.Album.Id.ToString();
      DropDownList child4 = dropDownList;
      child4.DataBind();
      HtmlGenericControl child5 = new HtmlGenericControl("div");
      child5.Attributes.Add("class", "buttonsContainer");
      RadButton radButton = new RadButton();
      radButton.ID = "btnApply";
      radButton.Text = Res.Get<Labels>("Save");
      radButton.AutoPostBack = false;
      radButton.Skin = "Default";
      RadButton child6 = radButton;
      LiteralControl child7 = new LiteralControl(string.Format(" {0} ", (object) Res.Get<Labels>("or")));
      HyperLink hyperLink = new HyperLink();
      hyperLink.ID = "btnCancel";
      hyperLink.CssClass = "sfCancel";
      hyperLink.Text = Res.Get<Labels>("Cancel");
      hyperLink.NavigateUrl = "javascript: void(0);";
      HyperLink child8 = hyperLink;
      child5.Controls.Add((Control) child6);
      child5.Controls.Add((Control) child7);
      child5.Controls.Add((Control) child8);
      saveAsDialog.Controls.Add((Control) child1);
      saveAsDialog.Controls.Add((Control) child2);
      saveAsDialog.Controls.Add((Control) child3);
      saveAsDialog.Controls.Add((Control) child4);
      saveAsDialog.Controls.Add((Control) child5);
      return (Control) saveAsDialog;
    }

    private void ImageEditor_ImageLoading(object sender, ImageEditorLoadingEventArgs args)
    {
      args.Cancel = true;
      if (this.Image.IsVectorGraphics())
        return;
      using (LibrariesManager manager = this.GetManager())
      {
        using (Stream stream = manager.Download((MediaContent) this.Image))
        {
          MemoryStream destination = new MemoryStream();
          stream.CopyTo((Stream) destination);
          args.Image = new EditableImage((Stream) destination);
        }
      }
    }

    private void ImageEditor_ImageSaving(object sender, ImageEditorSavingEventArgs args)
    {
      args.Cancel = true;
      using (LibrariesManager manager = this.GetManager())
      {
        System.Drawing.Image image1 = args.Image.Image;
        Stream source = (Stream) new MemoryStream();
        Stream stream = source;
        ImageFormat rawFormat = args.Image.RawFormat;
        image1.Save(stream, rawFormat);
        Telerik.Sitefinity.Libraries.Model.Image image2;
        if (string.IsNullOrEmpty(args.FileName))
        {
          Guid id = !(this.Status == "Live") ? new Guid(this.ImageId) : manager.GetImage(new Guid(this.ImageId)).OriginalContentId;
          image2 = manager.GetImage(id);
        }
        else
        {
          int length = args.FileName.IndexOf('/');
          string g = args.FileName.Substring(0, length);
          string title = args.FileName.Substring(length + 1);
          if (string.IsNullOrEmpty(title))
            title = (string) this.Image.Title + "-1";
          image2 = manager.CreateImage();
          Telerik.Sitefinity.Libraries.Model.Album album = manager.GetAlbum(new Guid(g));
          ((IHasParent) image2).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) album;
          image2.Title = (Lstring) title;
          Guid currentUserId = SecurityManager.GetCurrentUserId();
          image2.Owner = currentUserId;
          image2.UrlName = (Lstring) CommonMethods.TitleToUrl(title);
          image2.ApprovalWorkflowState = (Lstring) "Published";
          CommonMethods.RecompileItemUrls((Telerik.Sitefinity.GenericContent.Model.Content) image2, (IManager) manager);
        }
        manager.Upload((MediaContent) image2, source, "." + args.Image.Format, true);
        ++image2.Version;
        args.Argument = manager.Provider.Name;
        if (this.Status == "Live" || !string.IsNullOrEmpty(args.FileName))
        {
          Telerik.Sitefinity.Libraries.Model.Image image3 = manager.Publish(image2);
          this.ImageEditor.ImageUrl = this.Status == "Master" ? image3.OriginalContentId.ToString() : image3.Id.ToString();
          args.Argument = manager.Provider.Name + "|saveAs";
        }
        else
          this.ImageEditor.ImageUrl = image2.Id.ToString();
        manager.SaveChanges();
      }
    }

    private LibrariesManager GetManager() => LibrariesManager.GetManager(this.ProviderName);

    private Telerik.Sitefinity.Libraries.Model.Image GetImage() => this.GetManager().GetImage(new Guid(this.ImageId));
  }
}
