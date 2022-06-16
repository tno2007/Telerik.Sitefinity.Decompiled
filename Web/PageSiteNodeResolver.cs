// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageSiteNodeResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  [Serializable]
  internal class PageSiteNodeResolver
  {
    private readonly string siteNodeKey;
    private readonly string provider;
    private readonly Guid siteId;

    public PageSiteNodeResolver(PageSiteNode siteNode)
    {
      this.siteNodeKey = siteNode.Key;
      this.provider = siteNode.Provider.Name;
      this.siteId = SystemManager.CurrentContext.CurrentSite.Id;
    }

    public PageSiteNodeResolver(string asString)
    {
      string[] strArray = asString.Split(new string[1]
      {
        "/"
      }, 3, StringSplitOptions.None);
      this.siteNodeKey = strArray.Length == 3 ? strArray[0] : throw new ArgumentException("Bad PageSiteNodeResolver descriptor: " + asString);
      this.siteId = Guid.Parse(strArray[1]);
      this.provider = strArray[2];
    }

    public PageSiteNode Resolve()
    {
      using (SiteRegion.FromSiteId(this.siteId))
        return SiteMapBase.GetSiteMapProvider(this.provider) is SiteMapBase siteMapProvider ? siteMapProvider.FindSiteMapNodeFromKey(this.siteNodeKey, false) as PageSiteNode : (PageSiteNode) null;
    }

    public override string ToString() => this.siteNodeKey + "/" + this.siteId.ToString() + "/" + this.provider;

    public Guid SiteId => this.siteId;
  }
}
