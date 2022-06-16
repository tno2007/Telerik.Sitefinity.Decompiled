// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.OutputCacheEntry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Caching;

namespace Telerik.Sitefinity.Web.OutputCache
{
  [Serializable]
  internal sealed class OutputCacheEntry
  {
    public NameValueCollection HeaderElements { get; set; }

    public string KernelCacheUrl { get; set; }

    public IEnumerable<ResponseElement> ResponseBuffers { get; set; }

    public HttpCachePolicySettings Settings { get; set; }

    public int StatusCode { get; set; }

    public string StatusDescription { get; set; }

    public string SubstitutionInfo { get; set; }
  }
}
