// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.CompilationHelpers
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Compilation;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Compilation
{
  /// <summary>
  /// Provides a set of methods to help manage the compilation of <see cref="T:System.Web.UI.Page" /> and <see cref="!:System.Web.UI.PageControl" /> items.
  /// </summary>
  internal class CompilationHelpers
  {
    internal const string CompilationIdName = "CompilationId";
    internal const string CompilationStartName = "Start";
    internal const string CompilationEndName = "End";
    internal const string PageTitleName = "Title";
    internal const string PageUrlName = "PageUrl";
    internal const string RootNodeIdName = "RootNodeId";
    internal const string VirtualPathName = "VirtualPath";
    internal const string IsBackendRequestName = "IsBackendRequest";
    internal const string TimestampName = "Timestamp";
    internal const string SiteId = "SiteId";

    /// <summary>
    /// Loads the compiled content of an item (e.g. <see cref="T:System.Web.UI.Page" /> and <see cref="!:System.Web.UI.PageControl" />) from a given virtual path.
    /// If the item is a page then a page site node is provided.
    /// </summary>
    /// <typeparam name="T">The type of the compiled content of an item.</typeparam>
    /// <param name="virtualPath">The item virtual path.</param>
    /// <param name="isCausedByUserInteraction">True if the loading of the compiled content of the item is caused by the user (e.g. by navigating to a front end or back end page).</param>
    /// <param name="pageSiteNode">The page site node Default value is null.</param>
    /// <returns>The compiled content of an item (e.g. page or control).</returns>
    internal static T LoadControl<T>(
      string virtualPath,
      bool isCausedByUserInteraction = false,
      PageSiteNode pageSiteNode = null)
    {
      T content = default (T);
      if (pageSiteNode != null && BuildManager.GetCachedBuildDependencySet((HttpContext) null, virtualPath) == null)
      {
        HttpContextBase parentContext = SystemManager.CurrentHttpContext;
        SystemManager.RunWithElevatedPrivilege((SystemManager.RunWithElevatedPrivilegeDelegate) (p =>
        {
          HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
          currentHttpContext.Request.RequestContext.RouteData.DataTokens["SiteMapNode"] = parentContext.Request.RequestContext.RouteData.DataTokens["SiteMapNode"];
          foreach (DictionaryEntry dictionaryEntry in parentContext.Items.OfType<DictionaryEntry>().Where<DictionaryEntry>((Func<DictionaryEntry, bool>) (e => e.Key is string && !(e.Key as string).Equals("SfTransactions"))))
            currentHttpContext.Items[dictionaryEntry.Key] = dictionaryEntry.Value;
          Guid rootNodeId = Guid.Empty;
          if (pageSiteNode.Provider is SiteMapBase provider2 && provider2.GetRootNodeCore(false) is PageSiteNode rootNodeCore2)
            rootNodeId = rootNodeCore2.Id;
          Guid compilationId = Guid.NewGuid();
          EventHub.Raise((IEvent) new PageCompilationStartEvent(compilationId, pageSiteNode, rootNodeId, virtualPath));
          content = (T) BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof (T));
          EventHub.Raise((IEvent) new PageCompilationEndEvent(compilationId, pageSiteNode, rootNodeId, virtualPath));
        }));
      }
      else
        content = (T) BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof (T));
      return content;
    }
  }
}
