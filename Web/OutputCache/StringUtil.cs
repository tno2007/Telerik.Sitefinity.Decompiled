// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.StringUtil
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal static class StringUtil
  {
    public static bool StringArrayEquals(string[] a, string[] b)
    {
      if (a == null != (b == null))
        return false;
      if (a == null)
        return true;
      int length = a.Length;
      if (length != b.Length)
        return false;
      for (int index = 0; index < length; ++index)
      {
        if (a[index] != b[index])
          return false;
      }
      return true;
    }

    public static bool CacheVariationsEquals(
      IList<ICustomOutputCacheVariation> a,
      IList<ICustomOutputCacheVariation> b)
    {
      if (a == null != (b == null))
        return false;
      if (a == null)
        return true;
      int count = a.Count;
      if (count != b.Count)
        return false;
      for (int index = 0; index < count; ++index)
      {
        if (a[index].Key != b[index].Key)
          return false;
      }
      return true;
    }
  }
}
