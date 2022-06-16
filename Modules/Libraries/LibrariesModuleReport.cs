// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesModuleReport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Modules.Libraries
{
  internal class LibrariesModuleReport
  {
    public string ModuleName { get; set; }

    public int ImagesCount { get; set; }

    public int ImageLibrariesCount { get; set; }

    public int ImageFoldersCount { get; set; }

    public int VideosCount { get; set; }

    public int VideoLibrariesCount { get; set; }

    public int VideoFoldersCount { get; set; }

    public int DocumentsCount { get; set; }

    public int DocumentLibrariesCount { get; set; }

    public int DocumentFoldersCount { get; set; }

    public string ImageStorageProviders { get; set; }

    public string VideoStorageProviders { get; set; }

    public string DocumentStorageProviders { get; set; }

    public IDictionary<string, int> ImagesThumbnailProfilesAppliedTo { get; set; }

    public IDictionary<string, int> VideoSizes { get; set; }

    public IDictionary<string, int> DocumentExtensions { get; set; }

    public bool FilterQueriesByViewPermissions { get; set; }
  }
}
