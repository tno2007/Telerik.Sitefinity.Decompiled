// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.BlobContentLocation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.BlobStorage;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  public class BlobContentLocation : IBlobContentLocation
  {
    private readonly Guid contentId;
    private readonly Guid fileId;
    private readonly string filePath;
    private readonly string mimeType;
    private readonly string extension;

    public BlobContentLocation(IBlobContent content)
    {
      this.contentId = content.Id;
      this.fileId = content.FileId;
      this.filePath = content.FilePath;
      this.mimeType = content.MimeType;
      this.extension = content.Extension;
    }

    public Guid Id
    {
      get => this.contentId;
      set
      {
      }
    }

    public Guid FileId
    {
      get => this.fileId;
      set
      {
      }
    }

    public string FilePath
    {
      get => this.filePath;
      set
      {
      }
    }

    public string MimeType
    {
      get => this.mimeType;
      set
      {
      }
    }

    public string Extension
    {
      get => this.extension;
      set
      {
      }
    }
  }
}
