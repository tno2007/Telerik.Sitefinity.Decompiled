// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemUploaderView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A base upload view for all media content controls, letting you upload a new file.
  /// </summary>
  public class SingleMediaContentItemUploaderView : SimpleScriptView
  {
    private Guid targetLibraryId = Guid.Empty;
    private string uiCulture;
    private string contentType;
    private string itemsName;
    private string itemName;
    private string librarySelectorItemName;
    private string itemNameWithArticle;
    private string parentType;
    private string libraryTypeName;
    private string folderFieldServiceUrl;
    private string createLibraryServiceUrl;
    private string mediaItemServiceUrl;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.SingleMediaContentItemUploaderView.ascx");
    private const string MediaContentJs = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemUploaderView.js";
    private static readonly IDictionary<string, string> mediaItemsServices = (IDictionary<string, string>) new Dictionary<string, string>()
    {
      {
        typeof (Image).FullName,
        "~/Sitefinity/Services/Content/ImageService.svc/"
      },
      {
        typeof (Video).FullName,
        "~/Sitefinity/Services/Content/VideoService.svc/"
      },
      {
        typeof (Document).FullName,
        "~/Sitefinity/Services/Content/DocumentService.svc/"
      }
    };
    private static readonly IDictionary<string, string> libraryServices = (IDictionary<string, string>) new Dictionary<string, string>()
    {
      {
        typeof (Album).FullName,
        "~/Sitefinity/Services/Content/AlbumService.svc/"
      },
      {
        typeof (VideoLibrary).FullName,
        "~/Sitefinity/Services/Content/VideoLibraryService.svc/"
      },
      {
        typeof (DocumentLibrary).FullName,
        "~/Sitefinity/Services/Content/DocumentLibraryService.svc/"
      }
    };

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SingleMediaContentItemUploaderView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the type of the media content.
    /// This property sets the default values for most of the other properties.
    /// </summary>
    /// <value>The type of the media content.</value>
    public EditorExternalDialogModes DialogMode { get; set; }

    /// <summary>Gets or sets the UI culture.</summary>
    /// <value>The UI culture.</value>
    public string UICulture
    {
      get
      {
        if (this.uiCulture == null)
          this.uiCulture = SystemManager.CurrentHttpContext.Request.QueryString["uiCulture"];
        return this.uiCulture;
      }
      set => this.uiCulture = value;
    }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public string ContentType
    {
      get
      {
        if (!string.IsNullOrEmpty(this.contentType))
          return this.contentType;
        switch (this.DialogMode)
        {
          case EditorExternalDialogModes.Image:
            return typeof (Image).FullName;
          case EditorExternalDialogModes.Document:
            return typeof (Document).FullName;
          case EditorExternalDialogModes.Media:
            return typeof (Video).FullName;
          default:
            return this.contentType;
        }
      }
      set => this.contentType = value;
    }

    /// <summary>Gets or sets the type of the parent.</summary>
    /// <value>The type of the parent.</value>
    public string ParentType
    {
      get => !string.IsNullOrEmpty(this.parentType) ? this.parentType : this.LibraryTypeName;
      set => this.parentType = value;
    }

    /// <summary>Gets or sets the provider name.</summary>
    /// <value>The type of the content.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// A localized string representing the item in plural (for example Images).
    /// </summary>
    public string ItemsName
    {
      get
      {
        if (!string.IsNullOrEmpty(this.itemsName))
          return this.itemsName;
        switch (this.DialogMode)
        {
          case EditorExternalDialogModes.Image:
            return Res.Get<ImagesResources>().Images;
          case EditorExternalDialogModes.Document:
            return Res.Get<DocumentsResources>().Documents;
          case EditorExternalDialogModes.Media:
            return Res.Get<VideosResources>().Videos;
          default:
            return this.itemsName;
        }
      }
      set => this.itemsName = value;
    }

    /// <summary>
    /// A localized string representing the item in singular (for example Image).
    /// </summary>
    public string ItemName
    {
      get
      {
        if (!string.IsNullOrEmpty(this.itemName))
          return this.itemName;
        switch (this.DialogMode)
        {
          case EditorExternalDialogModes.Image:
            return Res.Get<ImagesResources>().Image;
          case EditorExternalDialogModes.Document:
            return Res.Get<DocumentsResources>().Document;
          case EditorExternalDialogModes.Media:
            return Res.Get<VideosResources>().Video;
          default:
            return this.itemName;
        }
      }
      set => this.itemName = value;
    }

    /// <summary>
    /// A localized string representing the item in singular (for example Image).
    /// </summary>
    internal string LibrarySelectorItemName
    {
      get
      {
        if (!string.IsNullOrEmpty(this.librarySelectorItemName))
          return this.librarySelectorItemName;
        switch (this.DialogMode)
        {
          case EditorExternalDialogModes.Image:
            return Res.Get<LibrariesResources>().ImageItemName;
          case EditorExternalDialogModes.Document:
            return Res.Get<LibrariesResources>().DocumentItemName;
          case EditorExternalDialogModes.Media:
            return Res.Get<LibrariesResources>().VideoItemName;
          default:
            return this.librarySelectorItemName;
        }
      }
      set => this.librarySelectorItemName = value;
    }

    /// <summary>
    /// A localized string representing the item in singular with an article in front of it (for example an image).
    /// </summary>
    public string ItemNameWithArticle
    {
      get
      {
        if (!string.IsNullOrEmpty(this.itemNameWithArticle))
          return this.itemNameWithArticle;
        switch (this.DialogMode)
        {
          case EditorExternalDialogModes.Image:
            return Res.Get<ImagesResources>().ImageWithArticle;
          case EditorExternalDialogModes.Document:
            return Res.Get<DocumentsResources>().DocumentWithArticle;
          case EditorExternalDialogModes.Media:
            return Res.Get<VideosResources>().VideoWithArticle;
          default:
            return this.itemNameWithArticle;
        }
      }
      set => this.itemNameWithArticle = value;
    }

    /// <summary>
    /// Gets or sets the service URL used for the folder field.
    /// </summary>
    public string FolderFieldServiceUrl
    {
      get => !string.IsNullOrEmpty(this.folderFieldServiceUrl) ? this.folderFieldServiceUrl : this.CreateLibraryServiceUrl + "folders/";
      set => this.folderFieldServiceUrl = value;
    }

    /// <summary>
    /// Gets or sets a custom value indicating where to save the uploaded file.
    /// </summary>
    public Guid TargetLibraryId
    {
      get => this.targetLibraryId;
      set => this.targetLibraryId = value;
    }

    /// <summary>Gets or sets the name of the library type.</summary>
    public string LibraryTypeName
    {
      get
      {
        if (!string.IsNullOrEmpty(this.libraryTypeName))
          return this.libraryTypeName;
        switch (this.DialogMode)
        {
          case EditorExternalDialogModes.Image:
            return typeof (Album).FullName;
          case EditorExternalDialogModes.Document:
            return typeof (DocumentLibrary).FullName;
          case EditorExternalDialogModes.Media:
            return typeof (VideoLibrary).FullName;
          default:
            return this.libraryTypeName;
        }
      }
      set => this.libraryTypeName = value;
    }

    /// <summary>Gets or sets the create library service URL.</summary>
    public string CreateLibraryServiceUrl
    {
      get
      {
        string str;
        return !string.IsNullOrEmpty(this.createLibraryServiceUrl) || !SingleMediaContentItemUploaderView.libraryServices.TryGetValue(this.LibraryTypeName, out str) ? this.createLibraryServiceUrl : str;
      }
      set => this.createLibraryServiceUrl = value;
    }

    /// <summary>Gets or sets the media item service URL.</summary>
    /// <value>The media item service URL.</value>
    public string MediaItemServiceUrl
    {
      get
      {
        string str;
        return !string.IsNullOrEmpty(this.mediaItemServiceUrl) || !SingleMediaContentItemUploaderView.mediaItemsServices.TryGetValue(this.ContentType, out str) ? this.mediaItemServiceUrl : str;
      }
      set => this.mediaItemServiceUrl = value;
    }

    protected virtual MediaItemPropertiesView MediaItemFieldsControl => this.Container.GetControl<MediaItemPropertiesView>("itemFieldsControl", true);

    /// <summary>Gets the reference to the FileField control.</summary>
    /// <value>The file upload.</value>
    protected FileField FileUpload => this.Container.GetControl<FileField>("fileUpload", false);

    /// <summary>
    /// Gets the reference to the fileUpload wrapper div control.
    /// </summary>
    /// <value>The file upload.</value>
    protected Control FileUploadContainerWrp => this.Container.GetControl<Control>("sf_fileUploadWrp", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.FileUpload.LibraryContentType = TypeResolutionService.ResolveType(this.ContentType);
      this.FileUpload.ItemName = this.ItemName;
      this.FileUpload.ItemNamePlural = this.ItemsName.ToLower();
      this.FileUpload.LibraryProviderName = this.ProviderName;
      this.FileUpload.IsMultiselect = false;
      this.FileUpload.MaxFileCount = 1;
      this.FileUpload.UsedByMediaContentUploader = true;
      this.MediaItemFieldsControl.MediaItemServiceUrl = this.MediaItemServiceUrl;
      this.MediaItemFieldsControl.ContentType = this.ContentType;
      this.MediaItemFieldsControl.LibraryTypeName = this.LibraryTypeName;
      this.MediaItemFieldsControl.TargetLibraryId = this.TargetLibraryId;
      this.MediaItemFieldsControl.ProviderName = this.ProviderName;
      this.MediaItemFieldsControl.UICulture = this.UICulture;
    }

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      controlDescriptor.AddProperty("_uiCulture", (object) this.UICulture);
      controlDescriptor.AddProperty("_contentWebServiceUrl", (object) VirtualPathUtility.ToAbsolute(this.MediaItemServiceUrl));
      controlDescriptor.AddProperty("_contentType", (object) this.ContentType);
      controlDescriptor.AddProperty("_parentType", (object) this.ParentType);
      controlDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      Type contentType = TypeResolutionService.ResolveType(this.ContentType);
      controlDescriptor.AddProperty("_blankDataItem", (object) this.CreateBlankDataItem(contentType));
      controlDescriptor.AddComponentProperty("mediaItemFieldsControl", this.MediaItemFieldsControl.ClientID);
      controlDescriptor.AddComponentProperty("fileUpload", this.FileUpload.ClientID);
      controlDescriptor.AddElementProperty("fileUploadWrapper", this.FileUploadContainerWrp.ClientID);
      controlDescriptor.AddProperty("_cantUploadFilesErrorMessage", (object) Res.Get<LibrariesResources>().CantUploadFiles);
      Type type = TypeResolutionService.ResolveType(this.ContentType);
      controlDescriptor.AddProperty("_fileAllowedExtensions", (object) this.GetFileExtensionFilter(type.Name));
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemUploaderView.js", typeof (SingleMediaContentItemUploaderView).Assembly.GetName().ToString())
    };

    /// <summary>Gets the filter for the allowed file extensions.</summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    protected string GetFileExtensionFilter(string contentType)
    {
      string fileExtensionFilter = string.Empty;
      LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
      if (contentType != null)
      {
        string lower = contentType.ToLower();
        if (!(lower == "image"))
        {
          if (!(lower == "video"))
          {
            if (lower == "document")
            {
              bool? allowedExensions = librariesConfig.Documents.AllowedExensions;
              bool flag = true;
              if (allowedExensions.GetValueOrDefault() == flag & allowedExensions.HasValue)
                fileExtensionFilter = librariesConfig.Documents.AllowedExensionsSettings;
            }
          }
          else
            fileExtensionFilter = librariesConfig.Videos.AllowedExensionsSettings;
        }
        else
          fileExtensionFilter = librariesConfig.Images.AllowedExensionsSettings;
      }
      return fileExtensionFilter;
    }

    /// <summary>
    /// Creates a blank data item of the selected content type.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    protected string CreateBlankDataItem(Type contentType)
    {
      string transactionName = Guid.NewGuid().ToString();
      IManager managerInTransaction = ManagerBase.GetMappedManagerInTransaction(contentType, transactionName);
      bool suppressSecurityChecks = managerInTransaction.Provider.SuppressSecurityChecks;
      try
      {
        managerInTransaction.Provider.SuppressSecurityChecks = true;
        return managerInTransaction.CreateItem(contentType, Guid.Empty).ToJson(contentType);
      }
      finally
      {
        managerInTransaction.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        TransactionManager.DisposeTransaction(transactionName);
      }
    }
  }
}
