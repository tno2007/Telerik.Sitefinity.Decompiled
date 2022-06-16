// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.SitefinityBinaryStream
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data.OA;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  internal class SitefinityBinaryStream : Stream
  {
    private readonly SitefinityOAContext context;
    private readonly BinaryStream originalStream;
    private int streamFlushSize;
    private int maxStreamFlushSize;

    public SitefinityBinaryStream(
      SitefinityOAContext context,
      BinaryStream stream,
      int maxStreamFlushSize)
    {
      this.context = context;
      this.originalStream = stream;
      this.maxStreamFlushSize = maxStreamFlushSize;
    }

    public int MaxStreamFlushSize
    {
      get => this.maxStreamFlushSize;
      set => this.maxStreamFlushSize = value;
    }

    public override bool CanSeek => this.originalStream.CanSeek;

    public override bool CanWrite => this.originalStream.CanWrite;

    public override bool CanRead => this.originalStream.CanRead;

    public override bool CanTimeout => this.originalStream.CanTimeout;

    public override void Flush()
    {
      this.originalStream.Flush();
      this.context.SaveChanges();
    }

    public override long Position
    {
      get => this.originalStream.Position;
      set => this.originalStream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count) => this.originalStream.Read(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin) => this.originalStream.Seek(offset, origin);

    public override void SetLength(long value) => this.originalStream.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count)
    {
      this.originalStream.Write(buffer, offset, count);
      this.streamFlushSize += count;
      if (this.streamFlushSize < this.maxStreamFlushSize)
        return;
      this.originalStream.Flush();
      this.context.SaveChanges();
      this.streamFlushSize = 0;
    }

    public override long Length => this.originalStream.Length;

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.originalStream.Flush();
        this.context.SaveChanges();
      }
      this.originalStream.Dispose();
      base.Dispose(disposing);
    }

    public bool Append
    {
      get => this.originalStream.Append;
      set => this.originalStream.Append = true;
    }
  }
}
