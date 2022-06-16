// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.BlobDownloadStream
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using Telerik.Sitefinity.BlobStorage;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  public class BlobDownloadStream : Stream
  {
    private ChunksBlobStorageProvider provider;
    private IChunksBlobContent content;
    private IChunk currentChunk;
    private int currentChunkOrdinal;
    private long currentChunkPosition;
    private object transaction;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:MediaDownloadStream" /> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public BlobDownloadStream(ChunksBlobStorageProvider provider, IChunksBlobContent content)
    {
      this.provider = provider;
      this.transaction = provider.GetNewTransaction();
      this.content = (IChunksBlobContent) BlobContentProxy.CreateFrom((IBlobContent) content);
    }

    /// <inheritdoc />
    public override bool CanRead => true;

    /// <inheritdoc />
    public override bool CanSeek => true;

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override void Flush() => throw new NotSupportedException();

    /// <inheritdoc />
    public override long Length => this.content.TotalSize;

    /// <inheritdoc />
    public override long Position
    {
      get => (long) (this.currentChunkOrdinal * this.content.ChunkSize) + this.currentChunkPosition;
      set
      {
        long num1 = 0;
        long num2 = 0;
        if (value > 0L)
        {
          int chunkSize = this.content.ChunkSize;
          num1 = value / (long) chunkSize;
          num2 = value % (long) chunkSize;
        }
        this.currentChunkOrdinal = (int) num1;
        this.currentChunkPosition = num2;
        this.currentChunk = this.GetChunk(this.currentChunkOrdinal);
      }
    }

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this.currentChunkOrdinal >= this.content.NumberOfChunks)
        return 0;
      int destinationIndex = offset;
      int num1 = 0;
      if (this.currentChunk == null)
      {
        this.currentChunk = this.GetChunk(0);
        this.currentChunkPosition = 0L;
      }
      while (count > 0)
      {
        int num2 = (int) ((long) this.currentChunk.Size - this.currentChunkPosition);
        if (num2 > 0)
        {
          int length = count >= num2 ? num2 : count;
          Array.Copy((Array) this.currentChunk.Data, this.currentChunkPosition, (Array) buffer, (long) destinationIndex, (long) length);
          int num3 = num2 - length;
          destinationIndex += length;
          this.currentChunkPosition += (long) length;
          count -= length;
          num1 += length;
        }
        else
        {
          ++this.currentChunkOrdinal;
          this.currentChunk = (IChunk) null;
          if (this.currentChunkOrdinal < this.content.NumberOfChunks)
          {
            this.currentChunk = this.GetChunk(this.currentChunkOrdinal);
            if (this.currentChunk != null)
              this.currentChunkPosition = 0L;
            else
              break;
          }
          else
            break;
        }
      }
      return num1;
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin)
    {
      int chunkSize = this.content.ChunkSize;
      long position = this.Position;
      long num1 = (long) (this.content.NumberOfChunks * chunkSize);
      long num2 = offset;
      switch (origin)
      {
        case SeekOrigin.Current:
          num2 = position + offset;
          break;
        case SeekOrigin.End:
          num2 = num1 + offset;
          break;
      }
      this.Position = num2 >= 0L && num2 <= num1 ? num2 : throw new IOException();
      return num2;
    }

    /// <inheritdoc />
    public override void SetLength(long value) => throw new NotSupportedException();

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (this.transaction is IDisposable transaction)
        transaction.Dispose();
      this.currentChunk = (IChunk) null;
      this.provider = (ChunksBlobStorageProvider) null;
      this.content = (IChunksBlobContent) null;
    }

    private IChunk GetChunk(int ordinal) => this.provider.GetChunk(this.content, ordinal, this.transaction);
  }
}
