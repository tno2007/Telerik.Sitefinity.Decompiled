// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlPartsStack
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web
{
  public class UrlPartsStack : Stack<string>
  {
    public string Url => this.UrlData.Url;

    public PageUrlData UrlData { get; set; }

    public PageNode Node { get; set; }

    public UrlPartsStack(PageNode node, PageUrlData url)
    {
      this.UrlData = url;
      this.Node = node;
    }
  }
}
