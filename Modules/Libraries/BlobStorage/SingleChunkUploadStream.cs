// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.SingleChunkUploadStream
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using Telerik.Sitefinity.BlobStorage;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  public class SingleChunkUploadStream : BlobUploadStream
  {
    private MemoryStream memoryStream;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:MediaDownloadStream" /> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public SingleChunkUploadStream(ChunksBlobStorageProvider provider, IChunksBlobContent content)
      : base(provider, content)
    {
      this.memoryStream = new MemoryStream();
    }

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
    public override void Write(byte[] buffer, int offset, int count) => this.memoryStream.Write(buffer, offset, count);

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.IO.Stream" /> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      byte[] array = this.memoryStream.ToArray();
      this.EnsureCurrentChunk(0L, array.Length);
      this.CurrentChunk.Ordinal = 0;
      this.CurrentChunk.Data = array;
      this.CurrentChunk.Size = array.Length;
      base.Dispose(disposing);
      this.memoryStream.Dispose();
    }

    protected override int GetNewChunkSize(int count) => count;
  }
}
