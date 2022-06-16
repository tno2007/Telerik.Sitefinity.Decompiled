// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentUsages.Comparers.ContentItemSourceEqualityComparer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.ContentUsages.Model;

namespace Telerik.Sitefinity.ContentUsages.Comparers
{
  internal class ContentItemSourceEqualityComparer : IEqualityComparer<IContentItemUsage>
  {
    public bool Equals(IContentItemUsage x, IContentItemUsage y) => x.ItemId == y.ItemId && x.ItemType == y.ItemType;

    public int GetHashCode(IContentItemUsage obj) => obj.ItemId.GetHashCode() + obj.ItemType.GetHashCode();
  }
}
