// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.BackendSiteMap
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Provides implementation for <see cref="T:System.Web.SiteMapProvider" /> for Sitefinity pages.
  /// </summary>
  public class BackendSiteMap : SiteMapBase
  {
    /// <summary>Gets the key of the root node for this provider.</summary>
    /// <value>The root node key.</value>
    public override string RootNodeKey
    {
      get
      {
        Guid backendHomePageId = Config.Get<PagesConfig>().BackendHomePageId;
        return backendHomePageId == Guid.Empty ? string.Empty : backendHomePageId.ToString();
      }
    }

    /// <summary>
    /// Retrieves a <see cref="T:System.Web.SiteMapNode" /> object based on a specified key.
    /// </summary>
    /// <param name="pageId">The pageId of the site map node to be found.</param>
    /// <param name="ifAccessible">if set to <c>true</c> checks if the node is accessible to the user
    /// making the current request and if node is not accessible it returns null even if the node exists.</param>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="key" />; otherwise, null, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user. The default is null.
    /// </returns>
    public static SiteMapNode FindSiteMapNode(Guid id, bool ifAccessible) => BackendSiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(id.ToString(), ifAccessible);

    /// <summary>Gets the current site map provider.</summary>
    /// <returns></returns>
    public static SiteMapBase GetCurrentProvider() => (SiteMapBase) SiteMapBase.GetSiteMapProvider<BackendSiteMap>(Config.Get<PagesConfig>().BackendRootNode);

    /// <inheritdoc />
    public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
    {
      PageSiteNode pageSiteNode = node as PageSiteNode;
      foreach (IRestrictionStrategy restrictionStrategy in ObjectFactory.Container.ResolveAll<IPageNodeRestrictionStrategy>())
      {
        if (restrictionStrategy.IsRestricted((object) pageSiteNode))
          return false;
      }
      return (pageSiteNode == null || string.IsNullOrEmpty(pageSiteNode.ModuleName) || SystemManager.IsModuleAccessible(pageSiteNode.ModuleName) && (!(SystemManager.GetModule(pageSiteNode.ModuleName) is IMultisiteContextModule module) || module.IsAccessible(pageSiteNode))) && base.IsAccessibleToUser(context, node);
    }

    /// <inheritdoc />
    protected override bool IsPageNodeEnabled(PageNode pageNode)
    {
      foreach (IRestrictionStrategy restrictionStrategy in ObjectFactory.Container.ResolveAll<IPageNodeRestrictionStrategy>())
      {
        if (restrictionStrategy.IsRestricted((object) pageNode))
          return false;
      }
      return pageNode.ModuleName.IsNullOrEmpty() || SystemManager.IsModuleEnabled(pageNode.ModuleName);
    }
  }
}
