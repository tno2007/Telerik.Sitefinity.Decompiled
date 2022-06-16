// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.FileSystemStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.IO;
using System.Web.Hosting;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  /// <summary>
  /// Implements a logic for persisting BLOB data as chunks in the File System
  /// </summary>
  public class FileSystemStorageProvider : BlobStorageProvider
  {
    public const string FolderNameKey = "storageFolder";
    private string storageFolder;

    /// <summary>Gets the root storage folder.</summary>
    /// <value>The storage folder.</value>
    public string StorageFolder => this.storageFolder;

    /// <summary>Initializes the storage.</summary>
    /// <param name="config">The config.</param>
    protected override void InitializeStorage(NameValueCollection config)
    {
      this.storageFolder = config["storageFolder"];
      if (string.IsNullOrEmpty(this.storageFolder))
        this.storageFolder = "~/App_Data/Storage/" + this.Name;
      config.Remove("storageFolder");
      if (this.storageFolder.StartsWith("~/"))
      {
        if (HostingEnvironment.IsHosted)
        {
          this.storageFolder = HostingEnvironment.MapPath(this.storageFolder);
        }
        else
        {
          string path2 = this.storageFolder.Substring(1);
          path2.Replace('/', '\\');
          this.storageFolder = Path.Combine(SystemManager.AppDataFolderPhysicalPath, path2);
        }
      }
      if (Directory.Exists(this.storageFolder))
        return;
      Directory.CreateDirectory(this.storageFolder);
    }

    /// <summary>Gets the upload stream.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public override Stream GetUploadStream(IBlobContent content) => (Stream) File.OpenWrite(this.GetFilePath(content));

    /// <summary>Gets the download stream.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public override Stream GetDownloadStream(IBlobContent content) => (Stream) File.OpenRead(this.GetFilePath(content));

    /// <summary>Deletes the specified location.</summary>
    /// <param name="location">The location.</param>
    public override void Delete(IBlobContentLocation content)
    {
      try
      {
        File.Delete(this.GetFilePath(content));
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    /// <summary>
    /// Determines whether a blob items under the specified location is exists.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <returns>True if the blob exists, otherwise - false</returns>
    public override bool BlobExists(IBlobContentLocation location) => File.Exists(this.GetFilePath(location));

    /// <summary>Gets the file path.</summary>
    /// <param name="location">The location.</param>
    /// <returns></returns>
    protected virtual string GetFilePath(IBlobContentLocation location) => Path.Combine(this.StorageFolder, location.FileId.ToString());

    private string GetFilePath(IBlobContent content) => this.GetFilePath(this.ResolveBlobContentLocation(content));

    internal bool FileExists(Guid fileId) => File.Exists(Path.Combine(this.StorageFolder, fileId.ToString()));

    /// <inheritdoc />
    public override bool HasSameLocation(BlobStorageProvider other) => other is FileSystemStorageProvider systemStorageProvider ? this.StorageFolder.Equals(systemStorageProvider.StorageFolder, StringComparison.OrdinalIgnoreCase) : base.HasSameLocation(other);
  }
}
