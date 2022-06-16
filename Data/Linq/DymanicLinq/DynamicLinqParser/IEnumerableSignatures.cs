// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Dynamic.IEnumerableSignatures
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data.Linq.Dynamic
{
  internal interface IEnumerableSignatures
  {
    void Where(bool predicate);

    void Any();

    void Any(bool predicate);

    void All(bool predicate);

    void Count();

    void Count(bool predicate);

    void Min(object selector);

    void Max(object selector);

    void Sum(int selector);

    void Sum(int? selector);

    void Sum(long selector);

    void Sum(long? selector);

    void Sum(float selector);

    void Sum(float? selector);

    void Sum(double selector);

    void Sum(double? selector);

    void Sum(Decimal selector);

    void Sum(Decimal? selector);

    void Average(int selector);

    void Average(int? selector);

    void Average(long selector);

    void Average(long? selector);

    void Average(float selector);

    void Average(float? selector);

    void Average(double selector);

    void Average(double? selector);

    void Average(Decimal selector);

    void Average(Decimal? selector);

    void Contains(object selector);
  }
}
