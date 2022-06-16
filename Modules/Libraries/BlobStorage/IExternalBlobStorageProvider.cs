// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.IExternalBlobStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.BlobStorage;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  public interface IExternalBlobStorageProvider
  {
    /// <summary>Gets the item external URL.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    string GetItemUrl(IBlobContentLocation content);

    /// <summary>
    /// Copies the specified source to the specified destination.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    void Copy(IBlobContentLocation source, IBlobContentLocation destination);

    /// <summary>
    /// Moves the specified source to the specified destination and then deletes the source.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    void Move(IBlobContentLocation source, IBlobContentLocation destination);

    /// <summary>
    /// Sets the properties, like cacheControl, content type, etc.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <param name="properties">The properties.</param>
    void SetProperties(IBlobContentLocation location, IBlobProperties properties);

    /// <summary>
    /// Gets the content type, cache control settings, etc. of a blob.
    /// </summary>
    /// <param name="location">The location of the blob.</param>
    /// <returns>The properties of the blob.</returns>
    IBlobProperties GetProperties(IBlobContentLocation location);
  }
}
