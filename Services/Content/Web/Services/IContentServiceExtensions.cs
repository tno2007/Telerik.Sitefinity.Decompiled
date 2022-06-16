// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Web.Services.IContentServiceExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace Telerik.Sitefinity.Services.Content.Web.Services
{
  internal class IContentServiceExtensions
  {
    internal static bool SetLibraryRunningTaskId(
      IFolder folderObj,
      LibrariesManager manager,
      Guid taskId)
    {
      switch (folderObj)
      {
        case Library library2:
          library2.RunningTask = taskId;
          return true;
        case Folder folder:
          Library library1 = manager.GetLibrary(folder.RootId);
          if (library1 != null)
          {
            library1.RunningTask = taskId;
            return true;
          }
          break;
      }
      return false;
    }
  }
}
