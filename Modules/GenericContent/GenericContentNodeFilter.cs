// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.GenericContentNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  internal class GenericContentNodeFilter : ISitemapNodeFilter
  {
    public bool IsNodeAccessPrevented(PageSiteNode pageNode)
    {
      if (!pageNode.IsBackend || !this.IsFilterEnabled("GenericContent") || pageNode.Id != ContentModule.HomePageId)
        return false;
      IEnumerable<ISecuredObject> securityRoots = SecuredModuleBase.GetSecurityRoots(typeof (ContentManager));
      bool flag = true;
      foreach (KeyValuePair<string, string[]> filterSecuritySet in SecurityConstants.BackendNodeFilterSecuritySets)
      {
        KeyValuePair<string, string[]> securitySet = filterSecuritySet;
        foreach (string str in securitySet.Value)
        {
          string actionName = str;
          if (securityRoots.Any<ISecuredObject>((Func<ISecuredObject, bool>) (r => r.IsGranted(securitySet.Key, actionName))))
          {
            flag = false;
            break;
          }
        }
      }
      return flag;
    }
  }
}
