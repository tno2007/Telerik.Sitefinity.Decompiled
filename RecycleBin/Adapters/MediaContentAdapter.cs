// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Adapters.MediaContentAdapter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.RecycleBin.Adapters
{
  /// <summary>
  /// Populates the properties of a specified <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> from the properties of a <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" /> item.
  /// </summary>
  public class MediaContentAdapter : IRecycleBinItemAdapter
  {
    /// <summary>
    /// Populates the properties of the specified <paramref name="recycleBinItem" /> from the specified <paramref name="dataItem" /> casting it to <see cref="T:Telerik.Sitefinity.Model.IContent" />.
    /// </summary>
    /// <param name="recycleBinItem">The recycle bin item which properties will be populated.</param>
    /// <param name="dataItem">The data item to get values from.</param>
    public virtual void FillProperties(IRecycleBinDataItem recycleBinItem, IDataItem dataItem)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (recycleBinItem == null)
        throw new ArgumentNullException(nameof (recycleBinItem));
      if (!(dataItem is MediaContent mediaContentItem))
        throw new Exception(string.Format("MediaContentAdapter received data item with id: {0} and type:{1} that is not MediaContent.", (object) dataItem.Id, (object) dataItem.GetType().FullName));
      recycleBinItem.DeletedItemParentType = mediaContentItem.Parent.GetType().FullName;
      string folderPath = this.GetFolderPath(mediaContentItem);
      recycleBinItem.DeletedItemParentTitlesPath = folderPath;
    }

    private string GetFolderPath(MediaContent mediaContentItem)
    {
      string folderPath = (string) null;
      if (mediaContentItem.FolderId.HasValue)
      {
        if (mediaContentItem.Parent.GetType() == typeof (Album))
          folderPath = this.GetFolderPathByFolderId<Album>(mediaContentItem.FolderId);
        else if (mediaContentItem.Parent.GetType() == typeof (DocumentLibrary))
          folderPath = this.GetFolderPathByFolderId<DocumentLibrary>(mediaContentItem.FolderId);
        else if (mediaContentItem.Parent.GetType() == typeof (VideoLibrary))
          folderPath = this.GetFolderPathByFolderId<VideoLibrary>(mediaContentItem.FolderId);
      }
      else
        folderPath = mediaContentItem.Parent.Title.ToString();
      return folderPath;
    }

    private string GetFolderPathByFolderId<TMediaContent>(Guid? folderId) where TMediaContent : IContent
    {
      LibrariesManager manager = LibrariesManager.GetManager();
      return manager.FindFolderById(folderId.Value) is Folder folderById ? manager.GetFolderTitlePath<TMediaContent>(folderById) : (string) null;
    }
  }
}
