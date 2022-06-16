// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.DataItemIDComparer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  internal class DataItemIDComparer : IEqualityComparer<IDataItem>
  {
    private static readonly Lazy<IEqualityComparer<IDataItem>> instance = new Lazy<IEqualityComparer<IDataItem>>((Func<IEqualityComparer<IDataItem>>) (() => (IEqualityComparer<IDataItem>) new DataItemIDComparer()));

    public bool Equals(IDataItem x, IDataItem y)
    {
      if (x != null && y != null)
        return x.Id.Equals(y.Id);
      if (x != null && y == null || x == null && y != null)
        return false;
      return x == null && y == null || object.Equals((object) x, (object) y);
    }

    public int GetHashCode(IDataItem obj) => obj.Id.GetHashCode();

    public static IEqualityComparer<IDataItem> Instance => DataItemIDComparer.instance.Value;
  }
}
