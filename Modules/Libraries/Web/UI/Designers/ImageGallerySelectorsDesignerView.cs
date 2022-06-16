// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySelectorsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Represents the designer view used to select and filter images for the ImageGallery.
  /// You can select images from an album or according to specified citeria.
  /// </summary>
  public class ImageGallerySelectorsDesignerView : MediaContentSelectorsDesignerView
  {
    private string noLibrarySelectedText;
    private string uploadDialogUrl;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.ImageGallerySelectorsDesignerView.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ImageGallerySelectorsDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "imageGallerySelectorDesignerView";

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<ImagesResources>().Images;

    /// <summary>Gets a value indicating if there are libraries.</summary>
    /// <value><c>true</c> if there are libraries; otherwise, <c>false</c>.</value>
    protected override bool HaveLibraries
    {
      get
      {
        try
        {
          return this.Manager.GetAlbums().Count<Album>() > 0;
        }
        catch (ConfigurationErrorsException ex)
        {
        }
        return false;
      }
    }

    /// <summary>
    /// Gets the localizable string that represents the name of the library.
    /// </summary>
    /// <value></value>
    protected override string LibraryName => Res.Get<ImagesResources>().Album;

    /// <summary>
    /// Gets the localizable string that represents the name of the library in plural.
    /// </summary>
    /// <value></value>
    protected override string LibrariesName => Res.Get<ImagesResources>().Albums;

    /// <summary>Gets the url of the upload dialog.</summary>
    protected string UploadDialogUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.uploadDialogUrl))
        {
          string str = string.Format("?contentType={0}&providerName={1}&itemName={2}&itemsName={3}&libraryTypeName={4}&libraryType={5}&childServiceUrl={6}&parentServiceUrl={7}", (object) typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName, this.CurrentContentView.ControlDefinition.ProviderName.IsNullOrEmpty() ? (object) this.Manager.Provider.Name : (object) this.CurrentContentView.ControlDefinition.ProviderName, (object) Res.Get<ImagesResources>().Image, (object) Res.Get<ImagesResources>().Images, (object) Res.Get<ImagesResources>().Album, (object) typeof (Album).FullName, (object) HttpUtility.UrlEncode("~/Sitefinity/Services/Content/ImageService.svc/"), (object) HttpUtility.UrlEncode("~/Sitefinity/Services/Content/AlbumService.svc/")) + "&LibraryId={{Id}}";
          this.uploadDialogUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/" + typeof (UploadDialog).Name) + str;
        }
        return this.uploadDialogUrl;
      }
    }

    /// <summary>
    /// Gets or sets the text displayed when no library is selected.
    /// </summary>
    /// <value>The no library selected text.</value>
    public override string NoLibrarySelectedText
    {
      get
      {
        if (this.noLibrarySelectedText.IsNullOrEmpty())
          this.noLibrarySelectedText = Res.Get<LibrariesResources>().NoAlbumSelected;
        return this.noLibrarySelectedText;
      }
      set => this.noLibrarySelectedText = value;
    }

    /// <summary>
    /// Gets the localizable string that represents the content type.
    /// </summary>
    protected override string ContentType => typeof (Album).FullName;

    /// <summary>Gets the upload dialog.</summary>
    /// <value>The upload dialog.</value>
    protected internal virtual LinkButton UploadButton => this.Container.GetControl<LinkButton>("btnUpload", false);

    protected override Library GetSelectedLibrary()
    {
      Library selectedLibrary = (Library) null;
      Guid parentId = this.CurrentContentView.MasterViewDefinition.ItemsParentId;
      if (parentId != Guid.Empty)
      {
        try
        {
          selectedLibrary = (Library) this.Manager.GetAlbums().SingleOrDefault<Album>((Expression<Func<Album, bool>>) (l => l.Id == parentId));
        }
        catch (ConfigurationErrorsException ex)
        {
        }
      }
      return selectedLibrary;
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().First<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_uploadDialogUrl", (object) this.UploadDialogUrl);
      controlDescriptor.AddElementProperty("uploadButton", this.UploadButton.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
