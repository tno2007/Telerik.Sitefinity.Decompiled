// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.MediaEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Data.Events
{
  internal class MediaEvent : DataEvent
  {
    public MediaEvent(MediaContent media)
    {
      this.ParentId = media.ParentId;
      if (!this.ParentId.IsEmpty())
      {
        Guid parentId = this.ParentId;
      }
      else
      {
        Guid? folderId = media.FolderId;
        Guid empty = Guid.Empty;
        if ((folderId.HasValue ? (folderId.HasValue ? (folderId.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) != 0)
          return;
        folderId = media.FolderId;
        if (!folderId.HasValue)
          return;
        folderId = media.FolderId;
        Guid id = folderId.Value;
        if (!(LibrariesManager.GetManager(media.GetProviderName()).FindFolderById(id) is Folder folderById))
          return;
        this.ParentId = folderById.RootId;
      }
    }

    public Guid ParentId { get; set; }
  }
}
