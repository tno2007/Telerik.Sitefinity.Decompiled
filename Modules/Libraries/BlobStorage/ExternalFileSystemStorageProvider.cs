// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.ExternalFileSystemStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.IO;
using System.Web.Hosting;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  /// <summary>External file system storage providers for Libraries</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Modules.Libraries.BlobStorage.FileSystemStorageProvider" />
  /// <seealso cref="T:Telerik.Sitefinity.Modules.Libraries.BlobStorage.IExternalBlobStorageProvider" />
  public class ExternalFileSystemStorageProvider : 
    FileSystemStorageProvider,
    IExternalBlobStorageProvider
  {
    private string rootUrl;

    /// <summary>Initializes the storage.</summary>
    /// <param name="config">The config.</param>
    protected override void InitializeStorage(NameValueCollection config)
    {
      base.InitializeStorage(config);
      this.rootUrl = config["rootUrl"];
      if (!this.rootUrl.IsNullOrEmpty())
        return;
      this.rootUrl = SystemManager.RootUrl;
      if (!this.StorageFolder.StartsWith(HostingEnvironment.ApplicationPhysicalPath, StringComparison.OrdinalIgnoreCase))
        return;
      string str = this.StorageFolder.Substring(HostingEnvironment.ApplicationPhysicalPath.Length);
      str.Replace('\\', '/');
      this.rootUrl += str;
    }

    /// <summary>
    /// Copies the specified source to the specified destination.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    public void Copy(IBlobContentLocation source, IBlobContentLocation destination) => File.Copy(this.GetFilePath(source), this.GetFilePath(destination), true);

    /// <summary>Gets the item external URL.</summary>
    /// <param name="content">The content.</param>
    /// <returns>The item's external URL</returns>
    public string GetItemUrl(IBlobContentLocation content) => this.rootUrl + "/" + content.FilePath;

    /// <summary>
    /// Gets the content type, cache control settings, etc. of a blob.
    /// </summary>
    /// <param name="location">The location of the blob.</param>
    /// <returns>The properties of the blob.</returns>
    public IBlobProperties GetProperties(IBlobContentLocation location) => (IBlobProperties) null;

    /// <summary>
    /// Moves the specified source to the specified destination and then deletes the source.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    public void Move(IBlobContentLocation source, IBlobContentLocation destination)
    {
      this.EnsureFilePath(destination);
      File.Move(this.GetFilePath(source), this.GetFilePath(destination));
    }

    /// <summary>
    /// Sets the properties, like cacheControl, content type, etc.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="properties">The properties.</param>
    public void SetProperties(IBlobContentLocation location, IBlobProperties properties)
    {
    }

    /// <summary>Gets the upload stream.</summary>
    /// <param name="content">The content.</param>
    /// <returns>The upload stream.</returns>
    public override Stream GetUploadStream(IBlobContent content)
    {
      this.EnsureFilePath((IBlobContentLocation) content);
      return base.GetUploadStream(content);
    }

    /// <summary>Gets the file path.</summary>
    /// <param name="location">The location.</param>
    /// <returns>The file path.</returns>
    protected override string GetFilePath(IBlobContentLocation location) => Path.Combine(this.StorageFolder, location.FilePath);

    /// <inheritdoc />
    public override bool HasSameLocation(BlobStorageProvider other)
    {
      if (!(other is ExternalFileSystemStorageProvider other1))
        return base.HasSameLocation(other);
      return base.HasSameLocation((BlobStorageProvider) other1) && this.rootUrl.Equals(other1.rootUrl, StringComparison.OrdinalIgnoreCase);
    }

    private void EnsureFilePath(IBlobContentLocation location)
    {
      FileInfo fileInfo = new FileInfo(this.GetFilePath(location));
      if (fileInfo.Directory.Exists)
        return;
      fileInfo.Directory.Create();
    }
  }
}
