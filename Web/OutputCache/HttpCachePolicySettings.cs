// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.HttpCachePolicySettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telerik.Sitefinity.Web.OutputCache
{
  [Serializable]
  internal sealed class HttpCachePolicySettings
  {
    public HttpCacheability Cacheability { get; set; }

    public string ETag { get; set; }

    public bool IgnoreParams { get; set; }

    public int OmitVaryStar { get; set; }

    public bool IgnoreRangeRequests { get; set; }

    public TimeSpan MaxAge { get; set; }

    public bool SlidingExpiration { get; set; }

    public DateTime UtcExpires { get; set; }

    public DateTime UtcLastModified { get; set; }

    public DateTime UtcTimestampCreated { get; set; }

    public IEnumerable<KeyValuePair<HttpCacheValidateHandler, object>> ValidationCallbackInfo { get; set; }

    public bool ValidUntilExpires => !this.SlidingExpiration && this.ValidationCallbackInfo == null;

    public string[] VaryByContentEncodings { get; set; }

    public string VaryByCustom { get; set; }

    public string[] VaryByHeaders { get; set; }

    public string[] VaryByParams { get; set; }

    public bool HasValidationPolicy() => this.ValidUntilExpires || this.ValidationCallbackInfo != null;

    public bool IsValidationCallbackSerializable() => this.ValidationCallbackInfo.All<KeyValuePair<HttpCacheValidateHandler, object>>((Func<KeyValuePair<HttpCacheValidateHandler, object>, bool>) (info => info.Key.Method.IsStatic));
  }
}
