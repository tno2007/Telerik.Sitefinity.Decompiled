// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.SubSiteUrlResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Multisite
{
  internal class SubSiteUrlResolver : ISiteUrlResolver
  {
    private readonly MultisiteContext multisiteContext;
    private readonly MultisiteContext.SiteProxy site;
    private readonly Uri baseUri;
    private readonly string virtualFolder;

    public SubSiteUrlResolver(
      MultisiteContext multisiteContext,
      MultisiteContext.SiteProxy site,
      Uri baseUri)
    {
      this.multisiteContext = multisiteContext;
      this.site = site;
      this.baseUri = baseUri;
      this.virtualFolder = this.baseUri.AbsolutePath;
      if (this.virtualFolder.EndsWith("/"))
        return;
      this.virtualFolder += "/";
    }

    public string ResolveUrl(string url)
    {
      if (this.virtualFolder.Length > 1 && url.StartsWith(this.virtualFolder))
        url = url.Substring(this.virtualFolder.Length);
      string absoluteUri = this.baseUri.AbsoluteUri;
      return this.baseUri.AbsolutePath.Length > 1 && absoluteUri.EndsWith(this.baseUri.AbsolutePath) ? UrlPath.ResolveAbsoluteUrl(new Uri(absoluteUri.Substring(0, absoluteUri.Length - this.baseUri.AbsolutePath.Length)), this.multisiteContext.ResolveSiteRelativeUrl(this.site, url)) : UrlPath.ResolveAbsoluteUrl(this.baseUri, this.multisiteContext.ResolveSiteRelativeUrl(this.site, url));
    }
  }
}
