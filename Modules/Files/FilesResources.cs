// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Files.FilesResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Files
{
  /// <summary>Represents the string resources for files</summary>
  [ObjectInfo("FilesResources", ResourceClassId = "FilesResources")]
  public class FilesResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Files.FilesResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public FilesResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Files.FilesResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public FilesResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Files</summary>
    [ResourceEntry("FilesResourcesTitle", Description = "The title of this class.", LastModified = "2009/09/10", Value = "Files")]
    public string FilesResourcesTitle => this[nameof (FilesResourcesTitle)];

    /// <summary>Files</summary>
    [ResourceEntry("FilesResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2009/09/10", Value = "Files")]
    public string FilesResourcesTitlePlural => this[nameof (FilesResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Files user interface.
    /// </summary>
    [ResourceEntry("FilesResourcesDescription", Description = "The description of this class.", LastModified = "2009/09/10", Value = "Contains localizable resources for Files user interface.")]
    public string FilesResourcesDescription => this[nameof (FilesResourcesDescription)];

    /// <summary>Files</summary>
    [ResourceEntry("ModuleTitle", Description = "The title of the Files module.", LastModified = "2009/09/10", Value = "Files")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>Type</summary>
    [ResourceEntry("TypeGridColumnHeaderText", Description = "The title of the Type column in FileBrowser grid.", LastModified = "2009/09/11", Value = "Type")]
    public string TypeGridColumnHeaderText => this[nameof (TypeGridColumnHeaderText)];

    /// <summary>Date Modified</summary>
    [ResourceEntry("DateModifiedGridColumnHeaderText", Description = "The title of the Date Modified column in FileBrowser grid.", LastModified = "2009/09/11", Value = "Date Modified")]
    public string DateModifiedGridColumnHeaderText => this[nameof (DateModifiedGridColumnHeaderText)];

    /// <summary>File Folder</summary>
    [ResourceEntry("FileFolderText", Description = "The text displayed for the folders in the FileBrowser grid Type column.", LastModified = "2009/09/11", Value = "File Folder")]
    public string FileFolderText => this[nameof (FileFolderText)];

    /// <summary>File</summary>
    [ResourceEntry("FileText", Description = "The text displayed for the files in the FileBrowser grid Type column.", LastModified = "2009/09/11", Value = "File")]
    public string FileText => this[nameof (FileText)];

    /// <summary>Name</summary>
    [ResourceEntry("FileNameText", Description = "The text displayed in the FileBrowser grid Filename column.", LastModified = "2009/09/14", Value = "Name")]
    public string FileNameText => this[nameof (FileNameText)];

    /// <summary>Download</summary>
    [ResourceEntry("DownloadToolBarButtonText", Description = "The text of the Download button in the toolbar.", LastModified = "2009/09/15", Value = "Download")]
    public string DownloadToolBarButtonText => this[nameof (DownloadToolBarButtonText)];

    /// <summary>Download</summary>
    [ResourceEntry("DownloadContextMenuText", Description = "The text of the Download option in the context menu.", LastModified = "2009/10/08", Value = "Download")]
    public string DownloadContextMenuText => this[nameof (DownloadContextMenuText)];

    /// <summary>Rename</summary>
    [ResourceEntry("RenameToolBarButtonText", Description = "The text of the Rename button in the toolbar.", LastModified = "2009/09/15", Value = "Rename")]
    public string RenameToolBarButtonText => this[nameof (RenameToolBarButtonText)];

    /// <summary>Copy</summary>
    [ResourceEntry("CopyToolBarButtonText", Description = "The text of the Copy button in the toolbar.", LastModified = "2009/09/15", Value = "Copy")]
    public string CopyToolBarButtonText => this[nameof (CopyToolBarButtonText)];

    /// <summary>Paste</summary>
    [ResourceEntry("PasteToolBarButtonText", Description = "The text of the Paste button in the toolbar.", LastModified = "2009/09/15", Value = "Paste")]
    public string PasteToolBarButtonText => this[nameof (PasteToolBarButtonText)];

    /// <summary>Folder Up</summary>
    [ResourceEntry("FolderUpToolBarButtonText", Description = "The text of the FolderUp button in the toolbar.", LastModified = "2009/09/15", Value = "Folder Up")]
    public string FolderUpToolBarButtonText => this[nameof (FolderUpToolBarButtonText)];

    /// <summary>Delete</summary>
    [ResourceEntry("DeleteToolBarButtonText", Description = "The text of the Delete button in the toolbar.", LastModified = "2009/10/01", Value = "Delete")]
    public string DeleteToolBarButtonText => this[nameof (DeleteToolBarButtonText)];

    /// <summary>New Folder</summary>
    [ResourceEntry("NewFolderToolBarButtonText", Description = "The text of the NewFolder button in the toolbar.", LastModified = "2009/10/01", Value = "New Folder")]
    public string NewFolderToolBarButtonText => this[nameof (NewFolderToolBarButtonText)];

    /// <summary>Upload</summary>
    [ResourceEntry("UploadToolBarButtonText", Description = "The text of the Upload button in the toolbar.", LastModified = "2009/10/01", Value = "Upload")]
    public string UploadToolBarButtonText => this[nameof (UploadToolBarButtonText)];
  }
}
