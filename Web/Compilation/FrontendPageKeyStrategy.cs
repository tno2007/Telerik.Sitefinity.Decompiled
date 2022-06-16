// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.FrontendPageKeyStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.Compilation
{
  /// <summary>
  /// A class that provides the front-end page ids that will be compiled.
  /// </summary>
  public class FrontendPageKeyStrategy : IPageKeyStrategy
  {
    /// <summary>Gets the page its that will be used for compilation.</summary>
    /// <returns>The page ids.</returns>
    public virtual IList<Guid> GetPageIds() => (IList<Guid>) PageManager.GetManager().GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (x => x.RootNodeId != SiteInitializer.BackendRootNodeId)).Where<PageNode>((Expression<Func<PageNode, bool>>) (x => (int) x.NodeType == 0 && !x.IsDeleted)).OrderBy<PageNode, Guid>((Expression<Func<PageNode, Guid>>) (x => x.RootNodeId)).Select<PageNode, Guid>((Expression<Func<PageNode, Guid>>) (x => x.Id)).ToList<Guid>();
  }
}
