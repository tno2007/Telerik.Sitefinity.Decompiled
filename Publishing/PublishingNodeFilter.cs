// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Publishing
{
  internal class PublishingNodeFilter : ISitemapNodeFilter
  {
    public bool IsNodeAccessPrevented(PageSiteNode pageNode)
    {
      if (!pageNode.IsBackend)
        return false;
      bool flag = false;
      if (this.IsFilterEnabled("Publishing") && pageNode.Id == PublishingModule.HomePageId)
        flag = !AppPermission.IsGranted(AppAction.ManagePublishingSystem);
      return flag;
    }
  }
}
