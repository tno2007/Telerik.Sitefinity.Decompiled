// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider.FolderProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider
{
  internal class FolderProvider
  {
    private Type type;
    private Type childType;
    private LibrariesManager manager;

    internal FolderProvider(LibrariesManager manager, Type type)
    {
      this.manager = manager;
      if (typeof (Telerik.Sitefinity.Libraries.Model.Image).IsAssignableFrom(type))
      {
        this.type = typeof (Telerik.Sitefinity.Libraries.Model.Album);
        this.childType = type;
      }
      else if (typeof (Telerik.Sitefinity.Libraries.Model.Document).IsAssignableFrom(type))
      {
        this.type = typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary);
        this.childType = type;
      }
      else if (typeof (Telerik.Sitefinity.Libraries.Model.Video).IsAssignableFrom(type))
      {
        this.type = typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary);
        this.childType = type;
      }
      else
        this.type = type;
    }

    internal IEnumerable<ExtendedFolder> GetFolders(
      Guid? parentId,
      string searchParam,
      string takeParam,
      string skipParam,
      ref int? totalCount,
      bool recursive = false,
      bool includeParent = false,
      Guid? excludedFoldersRootId = null,
      string orderBy = null,
      bool filterByCreateChildPermissions = false,
      IEnumerable<string> excludedItemIds = null,
      bool getMediaItems = false,
      bool useLiveData = false)
    {
      orderBy = string.IsNullOrEmpty(orderBy) ? "Title asc" : orderBy;
      IFolder folder = !parentId.HasValue ? (IFolder) null : this.manager.GetFolder(parentId.Value);
      bool flag = this.HasPermissionsToViewLibrary(folder) && (!filterByCreateChildPermissions || this.HasPermissionsToCreateChild(folder));
      int result1;
      int count1 = int.TryParse(skipParam, out result1) ? result1 : 0;
      int result2;
      int count2 = int.TryParse(takeParam, out result2) ? result2 : 50;
      recursive = string.IsNullOrEmpty(searchParam) && recursive;
      List<Guid> excludedFoldersIds = this.GetExcludedFoldersIds(excludedFoldersRootId, excludedItemIds);
      IQueryable<IFolder> source1 = Enumerable.Empty<IFolder>().AsQueryable<IFolder>();
      List<Guid> guidList = new List<Guid>();
      if (folder != null)
      {
        if (flag)
        {
          IQueryable<IFolder> source2;
          if (string.IsNullOrEmpty(searchParam) && !recursive)
            source2 = (IQueryable<IFolder>) this.manager.GetAllFolders(new Guid?(), folder).Where<Folder>((Expression<Func<Folder, bool>>) (f => (Guid?) f.RootId == parentId && f.Parent == default (object) || f.ParentId == parentId));
          else
            source2 = (IQueryable<IFolder>) Queryable.Cast<Folder>(this.manager.GetAllFolders(folder).Where<IFolder>((Expression<Func<IFolder, bool>>) (f => f.Id != folder.Id)));
          source1 = source2.OrderBy<IFolder>(orderBy);
        }
      }
      else
      {
        guidList = this.GetLibrariesWithoutPermissions(orderBy, filterByCreateChildPermissions);
        if (excludedItemIds != null)
          guidList = guidList.Concat<Guid>(excludedItemIds.Select<string, Guid>((Func<string, Guid>) (x => Guid.Parse(x)))).ToList<Guid>();
        source1 = this.GetFoldersAndAlbums(recursive, guidList, orderBy);
      }
      if (!string.IsNullOrEmpty(searchParam))
        source1 = source1.Where<IFolder>((Expression<Func<IFolder, bool>>) (f => f.Title.Contains(searchParam)));
      IQueryable<IFolder> source3 = source1.Where<IFolder>((Expression<Func<IFolder, bool>>) (x => !excludedFoldersIds.Contains(x.Id)));
      int num1 = source3.Count<IFolder>();
      if (totalCount.HasValue)
        totalCount = new int?(num1);
      IQueryable<IFolder> source4 = source3.Skip<IFolder>(count1).Take<IFolder>(count2);
      List<IFolder> list = source4.ToList<IFolder>();
      int? nullable1;
      if (folder != null & includeParent & flag)
      {
        if (string.IsNullOrEmpty(searchParam) && !excludedFoldersIds.Contains(folder.Id))
        {
          list.Insert(0, folder);
          if (totalCount.HasValue)
          {
            ref int? local = ref totalCount;
            nullable1 = totalCount;
            int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
            local = nullable2;
          }
        }
        else if (this.FolderTitleHasSearchParam(folder, searchParam) && !excludedFoldersIds.Contains(folder.Id))
        {
          list.Insert(0, folder);
          if (totalCount.HasValue)
          {
            ref int? local = ref totalCount;
            nullable1 = totalCount;
            int? nullable3 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
            local = nullable3;
          }
        }
        list = list.Take<IFolder>(count2).ToList<IFolder>();
      }
      if (folder == null && !string.IsNullOrEmpty(searchParam))
      {
        HashSet<Guid> rootIds = this.GetFoldersAndAlbums(recursive, guidList, orderBy).Select<IFolder, Guid>((Expression<Func<IFolder, Guid>>) (x => x.Id)).ToHashSet<Guid>();
        IQueryable<Folder> source5 = this.manager.GetAllFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.Title.Contains(searchParam))).Where<Folder>((Expression<Func<Folder, bool>>) (x => rootIds.Contains(x.RootId) && !excludedFoldersIds.Contains(x.Id)));
        int num2 = source4.Count<IFolder>();
        if (num2 < count2)
        {
          int count3 = count1 - num1;
          source5 = source5.Skip<Folder>(count3).Take<Folder>(count2 - num2);
          list = list.Concat<IFolder>((IEnumerable<IFolder>) source5.ToList<Folder>()).ToList<IFolder>();
        }
        if (totalCount.HasValue)
        {
          ref int? local = ref totalCount;
          nullable1 = totalCount;
          int num3 = source5.Count<Folder>();
          int? nullable4 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + num3) : new int?();
          local = nullable4;
        }
      }
      return this.GetExtendedFolderResults(list, folder, getMediaItems, useLiveData);
    }

    internal ExtendedFolder GetFolderById(Guid id)
    {
      IFolder folder = this.manager.GetFolder(id);
      int foldersCount = this.manager.GetAllFolders(folder).Where<IFolder>((Expression<Func<IFolder, bool>>) (f => f.Id != folder.Id)).Where<IFolder>((Expression<Func<IFolder, bool>>) (f => f.ParentId == folder.Id)).Count<IFolder>();
      ExtendedFolder folderById = new ExtendedFolder(folder, foldersCount, (IList<MediaContent>) new List<MediaContent>(), 0);
      IDictionary<object, object> values = new FolderBreadcrumbProperty().GetValues((IEnumerable) this.manager.GetAllFolders(), (IManager) this.manager);
      if (values.ContainsKey((object) folder))
        folderById.Breadcrumb = (IEnumerable<BreadcrumbItem>) values[(object) folder];
      return folderById;
    }

    private bool HasPermissionsToCreateChild(IFolder folder)
    {
      bool createChild = true;
      if (folder != null)
      {
        if (!(folder is Library library))
          library = this.manager.GetItem(this.type, (folder as Folder).RootId) as Library;
        if (this.type == typeof (Telerik.Sitefinity.Libraries.Model.Album))
        {
          int num;
          if (!(this.childType != (Type) null))
          {
            if (library.IsGranted("Album", "CreateAlbum"))
              num = library.IsGranted("Image", "ManageImage") ? 1 : 0;
            else
              num = 0;
          }
          else if (library.IsGranted("Image", "ManageImage"))
            num = library.IsGranted("Image", "ViewImage") ? 1 : 0;
          else
            num = 0;
          createChild = num != 0;
        }
        else if (this.type == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
        {
          int num;
          if (!(this.childType != (Type) null))
          {
            if (library.IsGranted("VideoLibrary", "CreateVideoLibrary"))
              num = library.IsGranted("Video", "ManageVideo") ? 1 : 0;
            else
              num = 0;
          }
          else if (library.IsGranted("Video", "ManageVideo"))
            num = library.IsGranted("Video", "ViewVideo") ? 1 : 0;
          else
            num = 0;
          createChild = num != 0;
        }
        else if (this.type == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
        {
          int num;
          if (!(this.childType != (Type) null))
          {
            if (library.IsGranted("DocumentLibrary", "CreateDocumentLibrary"))
              num = library.IsGranted("Document", "ManageDocument") ? 1 : 0;
            else
              num = 0;
          }
          else if (library.IsGranted("Document", "ManageDocument"))
            num = library.IsGranted("Document", "ViewDocument") ? 1 : 0;
          else
            num = 0;
          createChild = num != 0;
        }
      }
      return createChild;
    }

    private bool HasPermissionsToViewLibrary(IFolder folder)
    {
      bool viewLibrary = true;
      if (folder != null)
      {
        if (!(folder is Library library))
          library = this.manager.GetItem(this.type, (folder as Folder).RootId) as Library;
        if (this.type == typeof (Telerik.Sitefinity.Libraries.Model.Album))
          viewLibrary = library.IsGranted("Album", "ViewAlbum");
        else if (this.type == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
          viewLibrary = library.IsGranted("VideoLibrary", "ViewVideoLibrary");
        else if (this.type == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
          viewLibrary = library.IsGranted("DocumentLibrary", "ViewDocumentLibrary");
      }
      return viewLibrary;
    }

    private List<FolderChildrenCount> GetLibraryFoldersCountMappings(
      IFolder folder,
      List<Guid> folderIds)
    {
      return (folder == null ? this.manager.GetAllFolders() : this.manager.GetAllFolders(new Guid?(folder.Id), folder)).Where<Folder>((Expression<Func<Folder, bool>>) (f => folderIds.Cast<Guid?>().Contains<Guid?>(f.ParentId) || f.ParentId == new Guid?() && folderIds.Contains((f as Folder).RootId))).GroupBy(f => new
      {
        RootId = f.RootId,
        ParentId = f.ParentId
      }).Where<IGrouping<\u003C\u003Ef__AnonymousType14<Guid, Guid?>, Folder>>(chf => chf.Count<Folder>() > 0).Select<IGrouping<\u003C\u003Ef__AnonymousType14<Guid, Guid?>, Folder>, FolderChildrenCount>(chf => new FolderChildrenCount(chf.Key.ParentId.HasValue ? chf.Key.ParentId.Value : chf.Key.RootId, chf.Count<Folder>())).ToList<FolderChildrenCount>();
    }

    private List<FolderProvider.LibraryImagesMapping> GetLibraryImagesMappings(
      List<Guid> folderIds,
      bool useLiveData)
    {
      IQueryable<MediaContent> queryable = (IQueryable<MediaContent>) this.GetItems().OrderByDescending<MediaContent, DateTime>((Expression<Func<MediaContent, DateTime>>) (i => i.DateCreated));
      IQueryable<MediaContent> source;
      if (useLiveData)
        source = queryable.EnhanceQueryToFilterPublished<MediaContent>(CultureInfo.CurrentUICulture).Where<MediaContent>((Expression<Func<MediaContent, bool>>) (x => (int) x.Status == 2));
      else
        source = queryable.Where<MediaContent>((Expression<Func<MediaContent, bool>>) (x => (int) x.Status == 0));
      return source.Where<MediaContent>((Expression<Func<MediaContent, bool>>) (f => folderIds.Cast<Guid?>().Contains<Guid?>(f.FolderId) || folderIds.Contains(f.ParentId))).Distinct<MediaContent>().GroupBy(x => new
      {
        FolderId = x.FolderId,
        ParentId = x.ParentId
      }).Where<IGrouping<\u003C\u003Ef__AnonymousType15<Guid?, Guid>, MediaContent>>(chf => chf.Any<MediaContent>()).Select<IGrouping<\u003C\u003Ef__AnonymousType15<Guid?, Guid>, MediaContent>, FolderProvider.LibraryImagesMapping>(chf => new FolderProvider.LibraryImagesMapping(chf.Key.FolderId.HasValue ? chf.Key.FolderId.Value : chf.Key.ParentId, chf.Count<MediaContent>(), chf.Take<MediaContent>(4).ToList<MediaContent>())).ToList<FolderProvider.LibraryImagesMapping>();
    }

    private List<Guid> GetExcludedFoldersIds(
      Guid? excludedFoldersRootId = null,
      IEnumerable<string> excludedItemIds = null)
    {
      List<Guid> first = excludedItemIds != null ? excludedItemIds.Select<string, Guid>((Func<string, Guid>) (x => Guid.Parse(x))).ToList<Guid>() : new List<Guid>();
      IFolder parentFolder = !excludedFoldersRootId.HasValue ? (IFolder) null : this.manager.GetFolder(excludedFoldersRootId.Value);
      if (parentFolder != null)
      {
        List<Guid> list = this.manager.GetAllFolders(parentFolder).Select<IFolder, Guid>((Expression<Func<IFolder, Guid>>) (x => x.Id)).ToList<Guid>();
        first = first.Concat<Guid>((IEnumerable<Guid>) list).ToList<Guid>();
        first.Add(parentFolder.Id);
      }
      return first;
    }

    private IEnumerable<ExtendedFolder> GetExtendedFolderResults(
      List<IFolder> folders,
      IFolder parentFolder,
      bool getMediaItems = false,
      bool useLiveData = false)
    {
      List<Guid> list = folders.Select<IFolder, Guid>((Func<IFolder, Guid>) (x => x.Id)).ToList<Guid>();
      List<FolderProvider.LibraryImagesMapping> libraryImagesMappings = getMediaItems ? this.GetLibraryImagesMappings(list, useLiveData) : new List<FolderProvider.LibraryImagesMapping>();
      List<FolderChildrenCount> libraryFoldersCounts = this.GetLibraryFoldersCountMappings(parentFolder, list);
      IDictionary<object, object> foldersBreadcrumb = new FolderBreadcrumbProperty().GetValues((IEnumerable) folders, (IManager) this.manager);
      IEnumerable<Guid> rootIds = folders.Where<IFolder>((Func<IFolder, bool>) (f => f.GetType().Inherits<Folder>())).Cast<Folder>().Select<Folder, Guid>((Func<Folder, Guid>) (x => x.RootId)).Distinct<Guid>();
      List<Library> rootLibraries = this.manager.GetLibraries().Where<Library>((Expression<Func<Library, bool>>) (x => rootIds.Contains<Guid>(x.Id))).ToList<Library>();
      return folders.Select<IFolder, ExtendedFolder>((Func<IFolder, ExtendedFolder>) (x =>
      {
        FolderChildrenCount folderChildrenCount = libraryFoldersCounts.FirstOrDefault<FolderChildrenCount>((Func<FolderChildrenCount, bool>) (f => f.Id == x.Id));
        FolderProvider.LibraryImagesMapping libraryImagesMapping = libraryImagesMappings.FirstOrDefault<FolderProvider.LibraryImagesMapping>((Func<FolderProvider.LibraryImagesMapping, bool>) (f => f.Id == x.Id));
        ExtendedFolder extendedFolderResults = new ExtendedFolder(x, folderChildrenCount != null ? folderChildrenCount.Count : 0, libraryImagesMapping != null ? (IList<MediaContent>) libraryImagesMapping.TopImages : (IList<MediaContent>) new List<MediaContent>(), libraryImagesMapping != null ? libraryImagesMapping.Count : 0);
        IFolder folder = x;
        if (!x.GetType().Inherits<Library>())
        {
          Guid rootId = (x as Folder).RootId;
          folder = folders.FirstOrDefault<IFolder>((Func<IFolder, bool>) (f => f.Id == rootId)) ?? (IFolder) rootLibraries.FirstOrDefault<Library>((Func<Library, bool>) (z => z.Id == rootId));
        }
        extendedFolderResults.MaxLibrarySizeInKb = (folder as Library).MaxSize * 1024L;
        extendedFolderResults.TotalLibrarySizeInKb = folder.GetTotalSize(this.manager.Provider) / 1024L;
        if (foldersBreadcrumb.ContainsKey((object) x))
          extendedFolderResults.Breadcrumb = (IEnumerable<BreadcrumbItem>) foldersBreadcrumb[(object) x];
        return extendedFolderResults;
      }));
    }

    private IQueryable<IFolder> GetFoldersAndAlbums(
      bool recursive,
      List<Guid> excludedLibrariesIds,
      string orderBy)
    {
      IQueryable<IFolder> source = (IQueryable<IFolder>) null;
      if (typeof (Telerik.Sitefinity.Libraries.Model.Album).IsAssignableFrom(this.type))
        source = (IQueryable<IFolder>) this.manager.GetAlbums();
      else if (typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).IsAssignableFrom(this.type))
        source = (IQueryable<IFolder>) this.manager.GetVideoLibraries();
      else if (typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).IsAssignableFrom(this.type))
        source = (IQueryable<IFolder>) this.manager.GetDocumentLibraries();
      IQueryable<IFolder> foldersAndAlbums = source.OrderBy<IFolder>(orderBy).Where<IFolder>((Expression<Func<IFolder, bool>>) (x => !excludedLibrariesIds.Contains(x.Id)));
      if (recursive && foldersAndAlbums != null)
      {
        HashSet<Guid?> rootGuids = foldersAndAlbums.Select<IFolder, Guid?>((Expression<Func<IFolder, Guid?>>) (x => (Guid?) x.Id)).ToHashSet<Guid?>();
        IQueryable<Folder> source2 = this.manager.GetAllFolders().Where<Folder>((Expression<Func<Folder, bool>>) (x => rootGuids.Contains(x.ParentId) || rootGuids.Contains((Guid?) x.RootId)));
        foldersAndAlbums = foldersAndAlbums.Concat<IFolder>((IEnumerable<IFolder>) source2);
      }
      return foldersAndAlbums;
    }

    private List<Guid> GetLibrariesWithoutPermissions(
      string orderBy,
      bool filterByCreateChildPermissions = false)
    {
      IQueryable<IFolder> source = (IQueryable<IFolder>) null;
      if (typeof (Telerik.Sitefinity.Libraries.Model.Album).IsAssignableFrom(this.type))
        source = (IQueryable<IFolder>) this.manager.GetAlbums();
      else if (typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).IsAssignableFrom(this.type))
        source = (IQueryable<IFolder>) this.manager.GetVideoLibraries();
      else if (typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).IsAssignableFrom(this.type))
        source = (IQueryable<IFolder>) this.manager.GetDocumentLibraries();
      return source.OrderBy<IFolder>(orderBy).ToList<IFolder>().Where<IFolder>((Func<IFolder, bool>) (x =>
      {
        if (!this.HasPermissionsToViewLibrary(x))
          return true;
        return filterByCreateChildPermissions && !this.HasPermissionsToCreateChild(x);
      })).Select<IFolder, Guid>((Func<IFolder, Guid>) (x => x.Id)).ToList<Guid>();
    }

    private IQueryable<MediaContent> GetItems()
    {
      IQueryable<MediaContent> items = (IQueryable<MediaContent>) null;
      if (typeof (Telerik.Sitefinity.Libraries.Model.Album).IsAssignableFrom(this.type))
        items = (IQueryable<MediaContent>) this.manager.GetImages();
      else if (typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).IsAssignableFrom(this.type))
        items = (IQueryable<MediaContent>) this.manager.GetVideos();
      else if (typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).IsAssignableFrom(this.type))
        items = (IQueryable<MediaContent>) this.manager.GetDocuments();
      return items;
    }

    private bool FolderTitleHasSearchParam(IFolder folder, string searchParam) => folder.Title.Contains(searchParam);

    private class LibraryImagesMapping
    {
      internal LibraryImagesMapping(Guid id, int count, List<MediaContent> topImages)
      {
        this.Id = id;
        this.Count = count;
        this.TopImages = topImages;
      }

      /// <summary>Gets or sets folder id</summary>
      public Guid Id { get; set; }

      /// <summary>Gets or sets children count</summary>
      public int Count { get; set; }

      /// <summary>Gets or sets children count</summary>
      public List<MediaContent> TopImages { get; set; }
    }
  }
}
