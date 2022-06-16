// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.ContentTypeResolvers.PageScopeResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Workflow.ContentTypeResolvers
{
  /// <summary>
  /// Resolves whether a page is part of a given selected scope.
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Workflow.IWorkflowItemScopeResolver" />
  internal class PageScopeResolver : IWorkflowItemScopeResolver
  {
    public bool ResolveItem(IWorkflowResolutionContext context, IWorkflowExecutionTypeScope scope)
    {
      if (!(context.ContentId != Guid.Empty))
        return string.IsNullOrWhiteSpace(scope.ContentFilter);
      string upperInvariant = context.ContentId.ToString().ToUpperInvariant();
      List<string> pageIds = this.GetPageIds(scope);
      if (pageIds.Count <= 0 || pageIds.Contains(upperInvariant))
        return true;
      return scope.IncludeChildren && this.CheckParentNodes(upperInvariant, (IList<string>) pageIds);
    }

    private List<string> GetPageIds(IWorkflowExecutionTypeScope scope)
    {
      if (string.IsNullOrEmpty(scope.ContentFilter))
        return new List<string>();
      return new List<string>(((IEnumerable<string>) scope.ContentFilter.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (s => s.ToUpperInvariant())));
    }

    private bool CheckParentNodes(string currentPageId, IList<string> selectedPageIds)
    {
      SiteMapNode siteMapNodeFromKey = ((SiteMapBase) SitefinitySiteMap.GetCurrentProvider()).FindSiteMapNodeFromKey(currentPageId, false);
      if (siteMapNodeFromKey == null)
        return false;
      for (SiteMapNode parentNode = siteMapNodeFromKey.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
      {
        if (selectedPageIds.Contains(parentNode.Key.ToString().ToUpperInvariant()))
          return true;
      }
      return false;
    }
  }
}
