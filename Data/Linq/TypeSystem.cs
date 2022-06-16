// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.TypeSystem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Data.Linq
{
  internal static class TypeSystem
  {
    internal static Type GetElementType(Type seqType)
    {
      Type ienumerable = TypeSystem.FindIEnumerable(seqType);
      return ienumerable == (Type) null ? seqType : ienumerable.GetGenericArguments()[0];
    }

    private static Type FindIEnumerable(Type seqType)
    {
      if (seqType == (Type) null || seqType == typeof (string))
        return (Type) null;
      if (seqType.IsArray)
        return typeof (IEnumerable<>).MakeGenericType(seqType.GetElementType());
      if (seqType.IsGenericType)
      {
        foreach (Type genericArgument in seqType.GetGenericArguments())
        {
          Type ienumerable = typeof (IEnumerable<>).MakeGenericType(genericArgument);
          if (ienumerable.IsAssignableFrom(seqType))
            return ienumerable;
        }
      }
      Type[] interfaces = seqType.GetInterfaces();
      if (interfaces != null && interfaces.Length != 0)
      {
        foreach (Type seqType1 in interfaces)
        {
          Type ienumerable = TypeSystem.FindIEnumerable(seqType1);
          if (ienumerable != (Type) null)
            return ienumerable;
        }
      }
      return seqType.BaseType != (Type) null && seqType.BaseType != typeof (object) ? TypeSystem.FindIEnumerable(seqType.BaseType) : (Type) null;
    }
  }
}
