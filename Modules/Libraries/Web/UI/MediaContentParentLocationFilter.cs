// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.MediaContentParentLocationFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  internal class MediaContentParentLocationFilter : 
    IContentLocationMatchingFilter,
    IContentLocationFilter
  {
    public bool ShouldApplyAdditionalFilters => true;

    public bool IsMatch(
      IContentLocationService locationService,
      IContentLocation location,
      Guid itemId)
    {
      LibrariesManager manager = LibrariesManager.GetManager(location.ItemProvider);
      Folder folder = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == Guid.Parse(this.Value)));
      IQueryable<MediaContent> queryable = manager.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (i => i.Id == itemId));
      IQueryable<MediaContent> source;
      if (folder == null)
        source = queryable.Where<MediaContent>((Expression<Func<MediaContent, bool>>) (p => p.Parent.Id == Guid.Parse(this.Value)));
      else
        source = queryable.Join((IEnumerable<Folder>) manager.GetFolders(), (Expression<Func<MediaContent, Guid?>>) (p => p.FolderId), (Expression<Func<Folder, Guid?>>) (f => (Guid?) f.Id), (p, f) => new
        {
          Media = p,
          Folder = f
        }).Where(r => r.Folder.RootId == folder.RootId && r.Folder.Path.StartsWith(folder.Path)).Select(r => r.Media);
      return source.Any<MediaContent>();
    }

    public string Value { get; set; }

    public string Name { get; set; }
  }
}
