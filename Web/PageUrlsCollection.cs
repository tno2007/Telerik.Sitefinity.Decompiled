// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageUrlsCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Web
{
  internal class PageUrlsCollection
  {
    private Dictionary<string, PageUrlsCollection> nextLevelUrls = new Dictionary<string, PageUrlsCollection>();

    public DummyPageUrlData UrlData { get; set; }

    public DummyPageNode Node { get; set; }

    public PageUrlsCollection GetNextLevelUrls(string urlPart)
    {
      PageUrlsCollection nextLevelUrls;
      this.nextLevelUrls.TryGetValue(urlPart, out nextLevelUrls);
      return nextLevelUrls;
    }

    public void AddUrl(UrlPartsStack urlStack)
    {
      string key = urlStack.Pop();
      PageUrlsCollection pageUrlsCollection;
      if (!this.nextLevelUrls.TryGetValue(key, out pageUrlsCollection))
      {
        pageUrlsCollection = new PageUrlsCollection();
        this.nextLevelUrls.Add(key, pageUrlsCollection);
      }
      if (urlStack.Count == 0)
      {
        pageUrlsCollection.Node = new DummyPageNode(urlStack.Node);
        pageUrlsCollection.UrlData = new DummyPageUrlData(urlStack.UrlData);
      }
      else
        pageUrlsCollection.AddUrl(urlStack);
    }
  }
}
