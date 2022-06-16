// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Versioning.MediaContentDependentItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Versioning;

namespace Telerik.Sitefinity.Modules.Libraries.Versioning
{
  internal class MediaContentDependentItem : IDependentItem
  {
    private MediaFileLink mediaFileLink;

    public string Key { get; }

    public Type CleanUpTaskType { get; }

    public int Culture { get; }

    public string GetData() => BlobContentCleanerTask.ConvertData((IDictionary<string, object>) new Dictionary<string, object>()
    {
      {
        "filePath",
        (object) this.mediaFileLink.FilePath
      },
      {
        "blobStorageProvider",
        (object) this.mediaFileLink.MediaContent.GetStorageProviderName()
      },
      {
        "libraryProviderName",
        (object) this.mediaFileLink.MediaContent.GetProviderName()
      },
      {
        "numberOfChunks",
        (object) this.mediaFileLink.NumberOfChunks
      },
      {
        "totalSize",
        (object) this.mediaFileLink.TotalSize
      },
      {
        "width",
        (object) this.mediaFileLink.Width
      },
      {
        "height",
        (object) this.mediaFileLink.Height
      },
      {
        "extension",
        (object) this.mediaFileLink.Extension
      },
      {
        "mimeType",
        (object) this.mediaFileLink.MimeType
      },
      {
        "defaultUrl",
        (object) this.mediaFileLink.DefaultUrl
      }
    });

    internal MediaContentDependentItem(MediaFileLink mediaFileLink)
    {
      this.Key = mediaFileLink.FileId.ToString();
      this.CleanUpTaskType = typeof (BlobContentCleanerTask);
      this.Culture = mediaFileLink.Culture;
      this.mediaFileLink = mediaFileLink;
    }
  }
}
