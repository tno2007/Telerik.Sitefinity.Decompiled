// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.SiteUrlResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Multisite
{
  internal class SiteUrlResolver : ISiteUrlResolver
  {
    private readonly Uri baseUri;

    public SiteUrlResolver(Uri baseUri) => this.baseUri = baseUri;

    public string ResolveUrl(string url) => UrlPath.ResolveAbsoluteUrl(this.baseUri, url);
  }
}
