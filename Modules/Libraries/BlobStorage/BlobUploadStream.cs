// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.BlobUploadStream
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using Telerik.Sitefinity.BlobStorage;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  public class BlobUploadStream : Stream
  {
    private ChunksBlobStorageProvider provider;
    private IChunksBlobContent content;
    private IChunk currentChunk;
    private long currentChunkPosition;
    private long position;
    private object transaction;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:MediaDownloadStream" /> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public BlobUploadStream(ChunksBlobStorageProvider provider, IChunksBlobContent content)
    {
      this.provider = provider;
      this.content = content;
      this.transaction = provider.GetNewTransaction();
    }

    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
    /// </summary>
    /// <value></value>
    /// <returns>true if the stream supports reading; otherwise, false.</returns>
    public override bool CanRead => false;

    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
    /// </summary>
    /// <value></value>
    /// <returns>true if the stream supports seeking; otherwise, false.</returns>
    public override bool CanSeek => false;

    /// <summary>
    /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
    /// </summary>
    /// <value></value>
    /// <returns>true if the stream supports writing; otherwise, false.</returns>
    public override bool CanWrite => true;

    /// <summary>
    /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">
    /// An I/O error occurs.
    /// </exception>
    public override void Flush() => this.SaveCurrentChunk();

    /// <summary>
    /// When overridden in a derived class, gets the length in bytes of the stream.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A long value representing the length of the stream in bytes.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    /// A class derived from Stream does not support seeking.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// Methods were called after the stream was closed.
    /// </exception>
    public override long Length => this.content.TotalSize;

    /// <summary>
    /// When overridden in a derived class, gets or sets the position within the current stream.
    /// </summary>
    /// <value></value>
    /// <returns>The current position within the stream.</returns>
    /// <exception cref="T:System.IO.IOException">
    /// An I/O error occurs.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The stream does not support seeking.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// Methods were called after the stream was closed.
    /// </exception>
    public override long Position
    {
      get => this.position;
      set
      {
        this.position = value;
        this.currentChunk = (IChunk) null;
        this.currentChunkPosition = 0L;
      }
    }

    /// <summary>
    /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
    /// </summary>
    /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
    /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
    /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
    /// <returns>
    /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    /// The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="buffer" /> is null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="offset" /> or <paramref name="count" /> is negative.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    /// An I/O error occurs.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The stream does not support reading.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// Methods were called after the stream was closed.
    /// </exception>
    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    /// <summary>
    /// When overridden in a derived class, sets the position within the current stream.
    /// </summary>
    /// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
    /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
    /// <returns>The new position within the current stream.</returns>
    /// <exception cref="T:System.IO.IOException">
    /// An I/O error occurs.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The stream does not support seeking, such as if the stream is constructed from a pipe or console output.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// Methods were called after the stream was closed.
    /// </exception>
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    /// <summary>
    /// When overridden in a derived class, sets the length of the current stream.
    /// </summary>
    /// <param name="value">The desired length of the current stream in bytes.</param>
    /// <exception cref="T:System.IO.IOException">
    /// An I/O error occurs.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// Methods were called after the stream was closed.
    /// </exception>
    public override void SetLength(long value) => throw new NotSupportedException();

    /// <summary>
    /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
    /// </summary>
    /// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
    /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
    /// <param name="count">The number of bytes to be written to the current stream.</param>
    /// <exception cref="T:System.ArgumentException">
    /// The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="buffer" /> is null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="offset" /> or <paramref name="count" /> is negative.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    /// An I/O error occurs.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The stream does not support writing.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    /// Methods were called after the stream was closed.
    /// </exception>
    public override void Write(byte[] buffer, int offset, int count)
    {
      int sourceIndex = offset;
      while (count > 0)
      {
        this.EnsureCurrentChunk(this.position, count);
        int num = this.currentChunk.Data.Length - this.currentChunk.Size;
        if (num > 0)
        {
          int length = count >= num ? num : count;
          Array.Copy((Array) buffer, (long) sourceIndex, (Array) this.currentChunk.Data, this.currentChunkPosition, (long) length);
          sourceIndex += length;
          this.currentChunkPosition += (long) length;
          this.currentChunk.Size += length;
          count -= length;
        }
        else
          this.SaveCurrentChunk();
      }
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.IO.Stream" /> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.SaveCurrentChunk();
      this.content.ChunkSize = this.provider.ChunkSize;
      this.provider.Finalize(this.content, this.transaction);
      if (this.transaction is IDisposable transaction)
        transaction.Dispose();
      this.currentChunk = (IChunk) null;
      this.provider = (ChunksBlobStorageProvider) null;
      this.content = (IChunksBlobContent) null;
    }

    protected IChunk GetChunk(int ordinal) => this.provider.GetChunk(this.content, ordinal, this.transaction);

    protected void CreateNewChunk(int count)
    {
      this.currentChunkPosition = 0L;
      this.currentChunk = this.provider.CreateChunk(this.content, this.transaction);
      if (this.currentChunk.Data != null)
        return;
      this.currentChunk.Data = new byte[this.GetNewChunkSize(count)];
    }

    protected void SaveCurrentChunk()
    {
      if (this.currentChunk == null)
        return;
      this.provider.SaveChunk(this.currentChunk, this.content, this.transaction);
      ++this.content.NumberOfChunks;
      this.currentChunk = (IChunk) null;
    }

    protected virtual int GetNewChunkSize(int count) => count > this.provider.ChunkSize && this.provider.ChunkSize != 0 ? this.provider.ChunkSize : count;

    protected void EnsureCurrentChunk(long position, int count)
    {
      if (this.currentChunk != null)
        return;
      if (position > 0L)
      {
        if (position == this.content.TotalSize)
          this.CreateNewChunk(count);
        else
          this.currentChunk = this.GetChunk((int) Math.DivRem(this.content.TotalSize, this.position, out this.currentChunkPosition));
      }
      else
      {
        if (this.content.Uploaded && this.content.NumberOfChunks > 0)
        {
          this.provider.ClearChunks(this.content, this.transaction);
          this.content.Uploaded = false;
          this.content.NumberOfChunks = 0;
        }
        this.CreateNewChunk(count);
      }
    }

    protected IChunksBlobContent Content => this.content;

    protected ChunksBlobStorageProvider Provider => this.provider;

    protected IChunk CurrentChunk => this.currentChunk;

    protected object Transaction => this.transaction;
  }
}
