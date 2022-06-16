// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Files.FileProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;

namespace Telerik.Sitefinity.Modules.Files
{
  /// <summary>
  /// 
  /// </summary>
  public class FileProvider : FileSystemContentProvider
  {
    /// <summary>
    /// Creates a new instance of the class. Used internally by FileManager to create
    /// instances of the content provider.
    /// </summary>
    /// <param name="context">The current HttpContext.</param>
    /// <param name="searchPatterns">The search patterns.</param>
    /// <param name="viewPaths">The view paths.</param>
    /// <param name="uploadPaths">The upload paths.</param>
    /// <param name="deletePaths">The delete paths.</param>
    /// <param name="selectedUrl">The selected URL.</param>
    /// <param name="selectedItemTag">The selected item tag.</param>
    public FileProvider(
      HttpContext context,
      string[] searchPatterns,
      string[] viewPaths,
      string[] uploadPaths,
      string[] deletePaths,
      string selectedUrl,
      string selectedItemTag)
      : base(context, searchPatterns, viewPaths, uploadPaths, deletePaths, selectedUrl, selectedItemTag)
    {
    }

    private string DateFormat => Config.Get<PagesConfig>().PageBrowserDateFormat;

    /// <summary>
    /// Resolves a root directory with the given path in tree mode.
    /// </summary>
    /// <param name="path">The virtual path of the directory.</param>
    /// <returns>A DirectoryItem, containing the root directory.</returns>
    public override DirectoryItem ResolveRootDirectoryAsTree(string path)
    {
      DirectoryItem directoryItem = base.ResolveRootDirectoryAsTree(path);
      if (directoryItem != null)
      {
        directoryItem.Tag = string.Format("hasfiles={0};", (object) this.HasFiles(directoryItem.Path));
        foreach (DirectoryItem directory in directoryItem.Directories)
        {
          DirectoryInfo directoryInfo = new DirectoryInfo(this.Context.Server.MapPath(VirtualPathUtility.AppendTrailingSlash(directory.Path)));
          directory.Attributes.Add("DateModified", directoryInfo.LastAccessTimeUtc.ToString(this.DateFormat));
          directory.Attributes.Add("Type", Res.Get<FilesResources>().FileFolderText);
          directory.Tag = string.Format("hasfiles={0};", (object) this.HasFiles(directory.Path));
        }
      }
      return directoryItem;
    }

    /// <summary>Resolves a directory with the given path.</summary>
    /// <param name="path">The virtual path of the directory.</param>
    /// <returns>A DirectoryItem, containing the directory.</returns>
    public override DirectoryItem ResolveDirectory(string path)
    {
      if (string.IsNullOrEmpty(path))
        path = this.Context.Request.ApplicationPath;
      DirectoryItem directoryItem = base.ResolveDirectory(path);
      if (directoryItem != null)
      {
        foreach (FileItem file in directoryItem.Files)
        {
          FileInfo fileInfo = new FileInfo(this.Context.Server.MapPath(VirtualPathUtility.AppendTrailingSlash(directoryItem.Path) + file.Name));
          file.Attributes.Add("DateModified", fileInfo.LastAccessTimeUtc.ToString(this.DateFormat));
          string str1 = fileInfo.Extension;
          int num = str1.IndexOf('.');
          if (num > -1)
            str1 = str1.Substring(num + 1);
          string str2 = str1 + " " + Res.Get<FilesResources>().FileText;
          file.Attributes.Add("Type", str2);
        }
      }
      return directoryItem;
    }

    /// <inheritdoc />
    public override string CreateDirectory(string path, string name)
    {
      string directory = base.CreateDirectory(path, name);
      this.Notify(new FileSystemChange(FileSystemChangeType.Created, FileSystemItemType.Directory, Path.Combine(path, name)));
      return directory;
    }

    /// <inheritdoc />
    public override string StoreBitmap(Bitmap bitmap, string url, ImageFormat format)
    {
      string str = base.StoreBitmap(bitmap, url, format);
      this.Notify(new FileSystemChange(FileSystemChangeType.CreatedOrChanged, FileSystemItemType.File, FileBrowserContentProvider.RemoveProtocolNameAndServerName(url)));
      return str;
    }

    /// <inheritdoc />
    public override string StoreFile(
      UploadedFile file,
      string path,
      string name,
      params string[] arguments)
    {
      string str = base.StoreFile(file, path, name, arguments);
      this.Notify(new FileSystemChange(FileSystemChangeType.CreatedOrChanged, FileSystemItemType.File, Path.Combine(path, name)));
      return str;
    }

    /// <inheritdoc />
    public override string DeleteDirectory(string path)
    {
      string str = base.DeleteDirectory(path);
      this.Notify(new FileSystemChange(FileSystemChangeType.Removed, FileSystemItemType.Directory, path));
      return str;
    }

    /// <inheritdoc />
    public override string DeleteFile(string path)
    {
      string str = base.DeleteFile(path);
      this.Notify(new FileSystemChange(FileSystemChangeType.Removed, FileSystemItemType.File, path));
      return str;
    }

    /// <inheritdoc />
    public override string CopyDirectory(string path, string newPath)
    {
      string str = base.CopyDirectory(path, newPath);
      this.Notify(new FileSystemChange(FileSystemChangeType.CreatedOrChanged, FileSystemItemType.Directory, newPath));
      return str;
    }

    /// <inheritdoc />
    public override string CopyFile(string path, string newPath)
    {
      string str = base.CopyFile(path, newPath);
      this.Notify(new FileSystemChange(FileSystemChangeType.CreatedOrChanged, FileSystemItemType.File, newPath));
      return str;
    }

    /// <inheritdoc />
    public override string MoveDirectory(string path, string newPath)
    {
      string str = base.MoveDirectory(path, newPath);
      this.Notify(new FileSystemChange(FileSystemChangeType.Removed, FileSystemItemType.Directory, path), new FileSystemChange(FileSystemChangeType.CreatedOrChanged, FileSystemItemType.Directory, newPath));
      return str;
    }

    /// <inheritdoc />
    public override string MoveFile(string path, string newPath)
    {
      string str = base.MoveFile(path, newPath);
      this.Notify(new FileSystemChange(FileSystemChangeType.Removed, FileSystemItemType.File, path), new FileSystemChange(FileSystemChangeType.CreatedOrChanged, FileSystemItemType.File, newPath));
      return str;
    }

    private void Notify(params FileSystemChange[] changes) => SystemMessageDispatcher.SendSystemMessage((SystemMessageBase) new FileSystemChangesMessage(changes));

    internal bool HasFiles(string path)
    {
      string[] files = Directory.GetFiles(SystemManager.CurrentHttpContext.Server.MapPath(path), "*.*", SearchOption.AllDirectories);
      return files != null && (uint) files.Length > 0U;
    }
  }
}
