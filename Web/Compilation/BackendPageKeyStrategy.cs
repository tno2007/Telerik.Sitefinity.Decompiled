// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.BackendPageKeyStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.Compilation
{
  internal class BackendPageKeyStrategy : IPageKeyStrategy
  {
    public IList<Guid> GetPageIds() => (IList<Guid>) BackendSiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(PageSiteNode.GetKey(SiteInitializer.BackendRootNodeId), false).GetAllNodes().OfType<PageSiteNode>().Where<PageSiteNode>((Func<PageSiteNode, bool>) (x => x.NodeType == NodeType.Standard)).Select<PageSiteNode, Guid>((Func<PageSiteNode, Guid>) (x => x.Id)).ToList<Guid>();
  }
}
