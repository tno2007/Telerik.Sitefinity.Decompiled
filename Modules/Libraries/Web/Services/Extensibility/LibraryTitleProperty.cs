// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility.LibraryTitleProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.Extensibility
{
  internal class LibraryTitleProperty : CalculatedProperty
  {
    public override Type ReturnType => typeof (string);

    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      List<MediaContent> list1 = items.OfType<MediaContent>().ToList<MediaContent>();
      List<Guid?> folderIds = list1.Where<MediaContent>((Func<MediaContent, bool>) (x => x.FolderId.HasValue)).Select<MediaContent, Guid?>((Func<MediaContent, Guid?>) (x => x.FolderId)).Distinct<Guid?>().ToList<Guid?>();
      List<Guid> libraryIds = list1.Where<MediaContent>((Func<MediaContent, bool>) (x => !x.FolderId.HasValue)).Select<MediaContent, Guid>((Func<MediaContent, Guid>) (x => x.ParentId)).Distinct<Guid>().ToList<Guid>();
      LibrariesManager manager1 = manager as LibrariesManager;
      List<Folder> list2 = manager1.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (x => folderIds.Contains((Guid?) x.Id))).ToList<Folder>();
      List<Library> list3 = manager1.GetLibraries().Where<Library>((Expression<Func<Library, bool>>) (x => libraryIds.Contains(x.Id))).ToList<Library>();
      foreach (MediaContent mediaContent in list1)
      {
        MediaContent item = mediaContent;
        if (item.FolderId.HasValue)
        {
          Folder folder = list2.FirstOrDefault<Folder>((Func<Folder, bool>) (x =>
          {
            Guid id = x.Id;
            Guid? folderId = item.FolderId;
            return folderId.HasValue && id == folderId.GetValueOrDefault();
          }));
          values.Add((object) item, (object) folder.Title);
        }
        else
        {
          Library library = list3.FirstOrDefault<Library>((Func<Library, bool>) (x => x.Id == item.ParentId));
          values.Add((object) item, (object) library.Title);
        }
      }
      return (IDictionary<object, object>) values;
    }
  }
}
