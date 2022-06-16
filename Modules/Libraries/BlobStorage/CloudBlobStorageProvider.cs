// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.CloudBlobStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.BlobStorage;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  /// <summary>
  /// Implements a basic logic for persisting BLOB data in any cloud Blob Storage
  /// </summary>
  public abstract class CloudBlobStorageProvider : BlobStorageProvider, IExternalBlobStorageProvider
  {
    private UrlMode urlMode;

    /// <summary>Resolves the item URL.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public abstract string GetItemUrl(IBlobContentLocation content);

    /// <summary>Copies one BLOB to another.</summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    public abstract void Copy(IBlobContentLocation source, IBlobContentLocation destination);

    /// <inheritdoc />
    public abstract void SetProperties(IBlobContentLocation location, IBlobProperties properties);

    /// <inheritdoc />
    public abstract IBlobProperties GetProperties(IBlobContentLocation location);

    /// <summary>Moves the specified source.</summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    public virtual void Move(IBlobContentLocation source, IBlobContentLocation destination)
    {
      this.Copy(source, destination);
      this.Delete(source);
    }

    /// <summary>Gets the name of the BLOB from IBlobContentLocation.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    protected virtual string GetBlobName(IBlobContentLocation content)
    {
      string filePath = content.FilePath;
      return string.IsNullOrEmpty(filePath) ? content.FileId.ToString() : filePath;
    }

    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType,
      bool initializeDecorator)
    {
      base.Initialize(providerName, config, managerType, initializeDecorator);
      Enum.TryParse<UrlMode>(config["urlMode"], out this.urlMode);
    }

    /// <summary>Gets the URL mode for the cloud blob storage provider</summary>
    internal UrlMode UrlMode => this.urlMode;
  }
}
