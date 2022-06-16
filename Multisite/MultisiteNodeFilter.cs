// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.MultisiteNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>
  /// Provides mechanism to deny access to Multisite management page
  /// </summary>
  internal class MultisiteNodeFilter : ISitemapNodeFilter
  {
    /// <summary>
    /// Execute custom logic to optionally prevent access to a page node
    /// </summary>
    /// <param name="pageNode">Sitefinity page node corresponding to the sitemap node</param>
    /// <returns>
    /// True if we want to mark the node as inaccessible, or false if we do not want to hide the node.
    /// </returns>
    /// <remarks>
    /// Executed by Sitefinity's sitemap provider after the security engine determines that
    /// the node is accessible.
    /// Calling this method will be skipped if its result is cached.
    /// </remarks>
    public bool IsNodeAccessPrevented(PageSiteNode pageNode)
    {
      if (!pageNode.IsBackend)
        return false;
      bool flag = false;
      if (this.IsFilterEnabled("MultisiteInternal") && (pageNode.Id == MultisiteModule.MultisitePageGroupId || pageNode.Id == MultisiteModule.HomePageId))
        flag = !AppPermission.IsGranted(AppAction.ManageMultisiteManagement);
      return flag;
    }
  }
}
