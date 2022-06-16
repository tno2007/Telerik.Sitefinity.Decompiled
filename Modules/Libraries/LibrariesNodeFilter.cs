// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Libraries.UserFiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Libraries
{
  internal class LibrariesNodeFilter : ISitemapNodeFilter
  {
    private static readonly Dictionary<string, string[]> imagesSecuritySets = new Dictionary<string, string[]>()
    {
      {
        "Album",
        new string[4]
        {
          "ChangeAlbumOwner",
          "ChangeAlbumPermissions",
          "CreateAlbum",
          "DeleteAlbum"
        }
      },
      {
        "Image",
        new string[3]
        {
          "ChangeImageOwner",
          "ChangeImagePermissions",
          "ManageImage"
        }
      },
      {
        "ImagesSitemapGeneration",
        new string[1]{ "ViewBackendLink" }
      }
    };
    private static readonly Dictionary<string, string[]> documentsSecuritySets = new Dictionary<string, string[]>()
    {
      {
        "DocumentLibrary",
        new string[4]
        {
          "ChangeDocumentLibraryOwner",
          "ChangeDocumentLibraryPermissions",
          "CreateDocumentLibrary",
          "DeleteDocumentLibrary"
        }
      },
      {
        "Document",
        new string[3]
        {
          "ChangeDocumentOwner",
          "ChangeDocumentPermissions",
          "ManageDocument"
        }
      },
      {
        "DocumentsSitemapGeneration",
        new string[1]{ "ViewBackendLink" }
      }
    };
    private static readonly Dictionary<string, string[]> videosSecuritySets = new Dictionary<string, string[]>()
    {
      {
        "VideoLibrary",
        new string[4]
        {
          "ChangeVideoLibraryOwner",
          "ChangeVideoLibraryPermissions",
          "CreateVideoLibrary",
          "DeleteVideoLibrary"
        }
      },
      {
        "Video",
        new string[3]
        {
          "ChangeVideoOwner",
          "ChangeVideoPermissions",
          "ManageVideo"
        }
      },
      {
        "VideosSitemapGeneration",
        new string[1]{ "ViewBackendLink" }
      }
    };

    public bool IsNodeAccessPrevented(PageSiteNode pageNode)
    {
      bool flag = false;
      if (this.IsFilterEnabled("Libraries"))
      {
        Guid id = pageNode.Id;
        if (id == LibrariesModule.LibraryImagesPageId || id == LibrariesModule.ImagesHomePageId)
          flag = !this.HasGrantedAction(LibrariesNodeFilter.imagesSecuritySets);
        else if (id == LibrariesModule.LibraryDocumentsPageId || id == LibrariesModule.DocumentsHomePageId)
          flag = !this.HasGrantedAction(LibrariesNodeFilter.documentsSecuritySets);
        else if (id == LibrariesModule.LibraryVideosPageId || id == LibrariesModule.VideosHomePageId)
          flag = !this.HasGrantedAction(LibrariesNodeFilter.videosSecuritySets);
        else if (id == UserFilesConstants.UserFilesNodeId || id == UserFilesConstants.UserFilesDocumentsPageId)
          flag = !AppPermission.IsGranted("ManageUserFiles");
      }
      return flag;
    }

    private bool HasGrantedAction(Dictionary<string, string[]> securitySets)
    {
      IEnumerable<DataProviderBase> contextProviders = LibrariesManager.GetManager().GetContextProviders();
      List<ISecuredObject> roots = new List<ISecuredObject>();
      foreach (DataProviderBase dataProviderBase in contextProviders)
        roots.Add(dataProviderBase.SecurityRoot);
      foreach (KeyValuePair<string, string[]> securitySet in securitySets)
      {
        KeyValuePair<string, string[]> permissionSet = securitySet;
        if (((IEnumerable<string>) permissionSet.Value).Any<string>((Func<string, bool>) (action => roots.Any<ISecuredObject>((Func<ISecuredObject, bool>) (r => r.IsGranted(permissionSet.Key, action))))))
          return true;
      }
      return false;
    }
  }
}
