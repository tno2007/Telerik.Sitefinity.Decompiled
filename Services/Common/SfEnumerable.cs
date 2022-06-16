// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Common.SfEnumerable
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;

namespace Telerik.Sitefinity.Services.Common
{
  public static class SfEnumerable
  {
    public static Array ToArray(this IEnumerable src, params Type[] types) => SfEnumerable.InvokeToArray(src, types);

    public static Array ToArray(this IEnumerable src) => SfEnumerable.InvokeToArray(src, SfEnumerable.GetArgs(src));

    private static Type[] GetArgs(IEnumerable src) => new Type[1]
    {
      src.GetType().GetGenericArguments()[0]
    };

    private static Array InvokeToArray(IEnumerable src, Type[] args) => (Array) typeof (Enumerable).GetMethod("ToArray").MakeGenericMethod(args).Invoke((object) null, new object[1]
    {
      (object) src
    });
  }
}
