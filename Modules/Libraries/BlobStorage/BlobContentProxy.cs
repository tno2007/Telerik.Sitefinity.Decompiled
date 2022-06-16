// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.BlobContentProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.BlobStorage;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  internal class BlobContentProxy : IChunksBlobContent, IBlobContent, IBlobContentLocation
  {
    public BlobContentProxy()
    {
    }

    public BlobContentProxy(IBlobContent content)
    {
      this.Id = content.Id;
      this.FileId = content.FileId;
      this.Url = content.Url;
      this.FilePath = content.FilePath;
      this.TotalSize = content.TotalSize;
      this.Extension = content.Extension;
      this.MimeType = content.MimeType;
      if (content is IChunksBlobContent chunksBlobContent)
      {
        this.Uploaded = chunksBlobContent.Uploaded;
        this.ChunkSize = chunksBlobContent.ChunkSize;
        this.NumberOfChunks = chunksBlobContent.NumberOfChunks;
      }
      else
        this.NumberOfChunks = 1;
    }

    public Guid FileId { get; set; }

    public string FilePath { get; set; }

    public Guid Id { get; set; }

    public string MimeType { get; set; }

    public string Extension { get; set; }

    public int ChunkSize { get; set; }

    public int NumberOfChunks { get; set; }

    public long TotalSize { get; set; }

    public bool Uploaded { get; set; }

    public string Url { get; set; }

    internal static BlobContentProxy CreateFrom(IBlobContent content) => new BlobContentProxy(content);
  }
}
