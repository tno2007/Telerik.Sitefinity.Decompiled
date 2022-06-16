// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.InvariantComparer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;
using System.Globalization;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal class InvariantComparer : IComparer
  {
    private static readonly InvariantComparer DefaultComparer = new InvariantComparer();
    private readonly CompareInfo compareInfo;

    public static InvariantComparer Default => InvariantComparer.DefaultComparer;

    private InvariantComparer() => this.compareInfo = CultureInfo.InvariantCulture.CompareInfo;

    public int Compare(object a, object b)
    {
      string string1 = a as string;
      string string2 = b as string;
      return string1 == null || string2 == null ? Comparer.Default.Compare(a, b) : this.compareInfo.Compare(string1, string2);
    }
  }
}
