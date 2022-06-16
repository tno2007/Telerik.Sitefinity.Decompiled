// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ExtensionMethods.LinkedListExtensionMethods
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.ExtensionMethods
{
  internal static class LinkedListExtensionMethods
  {
    public static LinkedListNode<T> FindNode<T>(
      this LinkedList<T> list,
      Func<T, bool> cb)
    {
      for (LinkedListNode<T> node = list.First; node != null; node = node.Next)
      {
        if (cb(node.Value))
          return node;
      }
      return (LinkedListNode<T>) null;
    }
  }
}
