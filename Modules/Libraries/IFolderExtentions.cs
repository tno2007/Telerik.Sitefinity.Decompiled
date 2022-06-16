// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.IFolderExtentions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Libraries.Model;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// This class contains extension methods for IFolder class
  /// </summary>
  internal static class IFolderExtentions
  {
    /// <summary>Gets the total size for a library/folder</summary>
    /// <param name="folder">The library/folder to get total size for</param>
    /// <param name="provider">The libraries provider to be used</param>
    /// <param name="excludedFileIds">File ids to be excluded from the calculation</param>
    /// <returns>The total size of the library/folder</returns>
    public static long GetTotalSize(
      this IFolder folder,
      LibrariesDataProvider provider,
      Guid[] excludedFileIds = null)
    {
      bool flag = !(folder is Library);
      Guid libraryId = flag ? folder.ParentId : folder.Id;
      Guid folderId = flag ? folder.Id : Guid.Empty;
      IQueryable<MediaFileLink> source = provider.GetMediaFileLinks().Where<MediaFileLink>((Expression<Func<MediaFileLink, bool>>) (l => l.MediaContent != default (object) && (int) l.MediaContent.Status == 0 && l.MediaContent.Parent.Id == libraryId));
      if (flag)
        source = source.Where<MediaFileLink>((Expression<Func<MediaFileLink, bool>>) (i => i.MediaContent.FolderId == (Guid?) folderId));
      if (excludedFileIds != null)
        source = source.Where<MediaFileLink>((Expression<Func<MediaFileLink, bool>>) (i => !excludedFileIds.Contains<Guid>(i.FileId)));
      return source.AsEnumerable<MediaFileLink>().GroupBy<MediaFileLink, Guid>((Func<MediaFileLink, Guid>) (i => i.FileId)).Select<IGrouping<Guid, MediaFileLink>, MediaFileLink>((Func<IGrouping<Guid, MediaFileLink>, MediaFileLink>) (g => g.First<MediaFileLink>())).Sum<MediaFileLink>((Func<MediaFileLink, long>) (l => l.TotalSize));
    }
  }
}
