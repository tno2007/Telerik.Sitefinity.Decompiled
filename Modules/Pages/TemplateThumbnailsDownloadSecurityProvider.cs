// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.TemplateThumbnailsDownloadSecurityProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Determines whether the current user can download the data item.
  /// </summary>
  internal class TemplateThumbnailsDownloadSecurityProvider : IDownloadSecurityProvider
  {
    /// <summary>
    /// The name of this security provider. Should be assigned in library's DownloadSecurityProviderName property.
    /// It will be used for dependency injection so it has to match the name of the class.
    /// </summary>
    public const string SecurityProviderName = "TemplateThumbnailsDownloadSecurityProvider";

    /// <summary>
    /// Returns true if item can be downloaded; otherwise false.
    /// </summary>
    /// <param name="downloadItem">The item that is to be downloaded.</param>
    /// <returns>True if item can be downloaded; otherwise false.</returns>
    public bool IsAllowed(MediaContent downloadItem)
    {
      Guid[] principals = new Guid[1]
      {
        SecurityManager.CurrentUserId
      };
      ISecuredObject securityRoot = PageManager.GetManager().GetSecurityRoot(false);
      return securityRoot.IsGranted("Pages", principals, "Create") | securityRoot.IsGranted("Pages", principals, "Modify") | securityRoot.IsGranted("Pages", principals, "View") | securityRoot.IsGranted("Pages", principals, "EditContent") | securityRoot.IsGranted("PageTemplates", principals, "Create") | securityRoot.IsGranted("PageTemplates", principals, "Modify") | securityRoot.IsGranted("PageTemplates", principals, "View") | AppPermission.Root.IsGranted("Backend", principals, "ManageBackendPages");
    }
  }
}
