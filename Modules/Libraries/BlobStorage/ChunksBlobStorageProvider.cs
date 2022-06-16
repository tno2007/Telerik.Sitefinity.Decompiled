// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.ChunksBlobStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Telerik.Sitefinity.BlobStorage;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  /// <summary>Implements a logic for persisting BLOB data as chunks</summary>
  public abstract class ChunksBlobStorageProvider : BlobStorageProvider
  {
    private int chunkSize = 81920;

    /// <summary>Initializes the storage.</summary>
    /// <param name="config">The config.</param>
    protected override void InitializeStorage(NameValueCollection config)
    {
      string s = config["chunkSize"];
      if (string.IsNullOrEmpty(s))
        return;
      this.chunkSize = int.Parse(s);
    }

    /// <summary>Gets the size of chunk.</summary>
    /// <value>The size of chunk.</value>
    public int ChunkSize
    {
      get => this.chunkSize;
      internal set => this.chunkSize = value;
    }

    /// <summary>Gets the upload stream.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public override Stream GetUploadStream(IBlobContent content) => !(content is IChunksBlobContent content1) ? (Stream) new SingleChunkUploadStream(this, (IChunksBlobContent) BlobContentProxy.CreateFrom(content)) : (Stream) new BlobUploadStream(this, content1);

    /// <summary>Gets the download stream.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public override Stream GetDownloadStream(IBlobContent content)
    {
      if (!(content is IChunksBlobContent content1))
        content1 = (IChunksBlobContent) BlobContentProxy.CreateFrom(content);
      return (Stream) new BlobDownloadStream(this, content1);
    }

    public override void Delete(IBlobContentLocation location)
    {
      object newTransaction = this.GetNewTransaction();
      try
      {
        this.ClearChunks(location, newTransaction);
      }
      finally
      {
        if (newTransaction is IDisposable disposable)
          disposable.Dispose();
      }
    }

    public override bool BlobExists(IBlobContentLocation location)
    {
      object newTransaction = this.GetNewTransaction();
      try
      {
        return this.GetChunksCount(location, newTransaction) > 0;
      }
      finally
      {
        if (newTransaction is IDisposable disposable)
          disposable.Dispose();
      }
    }

    protected internal abstract object GetNewTransaction();

    protected internal virtual IChunk GetChunk(
      IChunksBlobContent content,
      int ordinal,
      object transaction)
    {
      return this.GetChunks(content, ordinal, 1, transaction).FirstOrDefault<IChunk>();
    }

    protected internal virtual void ClearChunks(IChunksBlobContent content, object transaction) => this.ClearChunks(this.ResolveBlobContentLocation((IBlobContent) content), transaction);

    protected internal abstract IChunk CreateChunk(
      IChunksBlobContent content,
      object transaction);

    protected internal abstract IEnumerable<IChunk> GetChunks(
      IChunksBlobContent content,
      int ordinal,
      int count,
      object transaction);

    protected internal abstract void SaveChunk(
      IChunk chunk,
      IChunksBlobContent content,
      object transaction);

    protected internal abstract void ClearChunks(IBlobContentLocation location, object transaction);

    protected internal abstract int GetChunksCount(
      IBlobContentLocation location,
      object transaction);

    protected internal abstract void Finalize(IChunksBlobContent content, object transaction);
  }
}
