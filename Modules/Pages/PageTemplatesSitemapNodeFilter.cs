// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageTemplatesSitemapNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages
{
  internal class PageTemplatesSitemapNodeFilter : ISitemapNodeFilter
  {
    public const string FilterName = "PageTemplates";

    public bool IsNodeAccessPrevented(PageSiteNode pageNode)
    {
      if (!pageNode.IsBackend || !this.IsFilterEnabled("PageTemplates") || pageNode.Id != SiteInitializer.PageTemplatesNodeId)
        return false;
      bool flag = true;
      string[] strArray = new string[5]
      {
        "Create",
        "Delete",
        "Modify",
        "ChangeOwner",
        "ChangePermissions"
      };
      ISecuredObject securityRoot = PageManager.GetManager().GetSecurityRoot();
      foreach (string str in strArray)
      {
        if (securityRoot.IsGranted("PageTemplates", str))
        {
          flag = false;
          break;
        }
      }
      return flag;
    }
  }
}
