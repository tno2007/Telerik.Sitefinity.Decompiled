// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.CachedVary
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Web.OutputCache
{
  [Serializable]
  internal sealed class CachedVary
  {
    public string[] ContentEncodings { get; set; }

    public string[] Headers { get; set; }

    public string[] Params { get; set; }

    public bool VaryByAllParams { get; set; }

    public string VaryByCustom { get; set; }

    public string SiteId { get; set; }

    public string Culture { get; set; }

    public IList<ICustomOutputCacheVariation> CustomVariationParams { get; set; }

    public override bool Equals(object obj)
    {
      if (obj == this)
        return true;
      return obj is CachedVary cachedVary && this.VaryByAllParams == cachedVary.VaryByAllParams && this.VaryByCustom == cachedVary.VaryByCustom && StringUtil.StringArrayEquals(this.ContentEncodings, cachedVary.ContentEncodings) && StringUtil.StringArrayEquals(this.Headers, cachedVary.Headers) && !(this.SiteId != cachedVary.SiteId) && string.Compare(this.Culture, cachedVary.Culture, true) == 0 && StringUtil.CacheVariationsEquals(this.CustomVariationParams, cachedVary.CustomVariationParams) && StringUtil.StringArrayEquals(this.Params, cachedVary.Params);
    }

    public override int GetHashCode() => this.GetHashCode();
  }
}
