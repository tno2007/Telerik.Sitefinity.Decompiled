// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibraryThumbnailsDeleteTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Libraries.Model;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// 
  /// </summary>
  public class LibraryThumbnailsDeleteTask : LibraryThumbnailsTaskBase
  {
    protected override bool UpdateItem(LibrariesManager manager, MediaContent content)
    {
      bool flag = false;
      if (content.Thumbnails.Count > 0)
      {
        foreach (Thumbnail thumbnail in new List<Thumbnail>(content.GetThumbnails().Where<Thumbnail>((Func<Thumbnail, bool>) (t => t.Type == ThumbnailTypes.Autogenerated && ((IEnumerable<string>) this.ProfilesFilter).Contains<string>(t.Name)))))
        {
          manager.Provider.Delete(thumbnail);
          flag = true;
        }
      }
      return flag;
    }
  }
}
