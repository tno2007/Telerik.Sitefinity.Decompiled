// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.ExternalPageSiteNode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Web;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  internal class ExternalPageSiteNode : SiteMapNode, ISitefinitySiteMapNode
  {
    private NameValueCollection attributes = new NameValueCollection();

    public ExternalPageSiteNode(SiteMapProvider provider, string key, string url, string title)
      : base(provider, key)
    {
      this.Url = url;
      this.Title = title;
    }

    public override SiteMapNodeCollection ChildNodes => new SiteMapNodeCollection();

    public override bool HasChildNodes => false;

    public new NameValueCollection Attributes
    {
      get => this.attributes;
      set => this.attributes = value;
    }

    public Guid Id { get; set; }

    public float Ordinal { get; set; }
  }
}
