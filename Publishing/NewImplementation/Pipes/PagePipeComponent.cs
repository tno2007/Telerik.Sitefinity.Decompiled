// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.PagePipeComponent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  internal class PagePipeComponent
  {
    public virtual IList<Guid> GetSiteRootIds(PublishingPoint point)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null)
      {
        IList<Guid> byPointFromCache = PublishingManager.GetSitesByPointFromCache(point);
        Guid[] siteRootIds = new Guid[byPointFromCache.Count];
        for (int index = 0; index < byPointFromCache.Count; ++index)
        {
          ISite siteById = multisiteContext.GetSiteById(byPointFromCache[index]);
          siteRootIds[index] = siteById.SiteMapRootNodeId;
        }
        return (IList<Guid>) siteRootIds;
      }
      return (IList<Guid>) new Guid[1]
      {
        SiteInitializer.CurrentFrontendRootNodeId
      };
    }

    public virtual bool CheckNode(PageNode node, PublishingPoint point)
    {
      if (point.IsSharedWithAllSites)
        return true;
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext == null)
        return true;
      IList<Guid> byPointFromCache = PublishingManager.GetSitesByPointFromCache(point);
      if (byPointFromCache.Count == 0)
        return true;
      bool flag = false;
      for (int index = 0; index < byPointFromCache.Count; ++index)
      {
        ISite siteById = multisiteContext.GetSiteById(byPointFromCache[index]);
        if (siteById != null && node.RootNodeId == siteById.SiteMapRootNodeId)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }
  }
}
