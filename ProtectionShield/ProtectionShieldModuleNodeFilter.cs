// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.ProtectionShieldModuleNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.ProtectionShield
{
  internal class ProtectionShieldModuleNodeFilter : ISitemapNodeFilter
  {
    private IEnumerable<Guid> modulePageIds;

    private IEnumerable<Guid> ModulePageIds
    {
      get
      {
        if (this.modulePageIds == null)
          this.modulePageIds = (IEnumerable<Guid>) new List<Guid>()
          {
            ProtectionShieldModule.ProtectionShieldGroupPageId,
            ProtectionShieldModule.ProtectionShieldPageNodeId
          };
        return this.modulePageIds;
      }
    }

    public bool IsNodeAccessPrevented(PageSiteNode pageNode) => this.IsFilterEnabled("ProtectionShield") && (this.ModulePageIds.Contains<Guid>(pageNode.Id) || pageNode.IsModulePage("ProtectionShield")) && !ClaimsManager.GetCurrentIdentity().IsAdmin;
  }
}
