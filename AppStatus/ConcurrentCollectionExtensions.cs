// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.AppStatus.ConcurrentCollectionExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.AppStatus
{
  internal static class ConcurrentCollectionExtensions
  {
    internal static IEnumerable<TSource> BatchTake<TSource>(
      this BlockingCollection<TSource> collection,
      int startIndex,
      int batchSize)
    {
      return collection == null || collection.Count == 0 || batchSize == 0 || startIndex > collection.Count ? Enumerable.Empty<TSource>() : collection.Skip<TSource>(startIndex).Take<TSource>(batchSize);
    }
  }
}
