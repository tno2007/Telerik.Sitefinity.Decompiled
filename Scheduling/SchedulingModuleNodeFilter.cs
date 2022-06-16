// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.SchedulingModuleNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Scheduling
{
  internal class SchedulingModuleNodeFilter : ISitemapNodeFilter
  {
    private IEnumerable<Guid> modulePageIds;

    private IEnumerable<Guid> ModulePageIds
    {
      get
      {
        if (this.modulePageIds == null)
          this.modulePageIds = (IEnumerable<Guid>) new List<Guid>()
          {
            SchedulingModule.PageId
          };
        return this.modulePageIds;
      }
    }

    public bool IsNodeAccessPrevented(PageSiteNode pageNode) => this.IsFilterEnabled(SchedulingModule.ModuleName) && (this.ModulePageIds.Contains<Guid>(pageNode.Id) || pageNode.IsModulePage(SchedulingModule.ModuleName)) && !ClaimsManager.IsUnrestricted();
  }
}
