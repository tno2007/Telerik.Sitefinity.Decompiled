// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.PageNodeDataItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.RelatedData
{
  internal class PageNodeDataItem : IDataItem
  {
    private Guid id;
    private object transaction;
    private object provider;
    private DateTime lastModified;
    private string applicationName;

    public PageNodeDataItem()
    {
    }

    public PageNodeDataItem(PageSiteNode pageSiteNode)
    {
      this.id = pageSiteNode.Id;
      this.transaction = (object) Guid.NewGuid();
      this.provider = (object) pageSiteNode.Provider;
      this.applicationName = pageSiteNode.Provider.Name;
    }

    public Guid Id => this.id;

    public object Transaction
    {
      get => this.transaction;
      set => this.transaction = value;
    }

    public object Provider
    {
      get => this.provider;
      set => this.provider = value;
    }

    public DateTime LastModified
    {
      get => this.lastModified;
      set => this.lastModified = value;
    }

    public string ApplicationName
    {
      get => this.applicationName;
      set => this.applicationName = value;
    }

    public static List<PageNodeDataItem> GetPageNodeDataItemsList(
      List<PageSiteNode> items)
    {
      List<PageNodeDataItem> nodeDataItemsList = new List<PageNodeDataItem>();
      foreach (PageSiteNode pageSiteNode in items)
        nodeDataItemsList.Add(new PageNodeDataItem(pageSiteNode));
      return nodeDataItemsList;
    }
  }
}
