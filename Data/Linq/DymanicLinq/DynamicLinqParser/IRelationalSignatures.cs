// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Dynamic.IRelationalSignatures
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data.Linq.Dynamic
{
  internal interface IRelationalSignatures : IArithmeticSignatures
  {
    void F(string x, string y);

    void F(char x, char y);

    void F(DateTime x, DateTime y);

    void F(TimeSpan x, TimeSpan y);

    void F(char? x, char? y);

    void F(DateTime? x, DateTime? y);

    void F(TimeSpan? x, TimeSpan? y);

    new void F(int? x, int? y);

    new void F(int x, int y);

    void F(object x, DateTime y);

    void F(object x, TimeSpan y);

    void F(object x, DateTime? y);

    void F(object x, TimeSpan? y);

    void F(DateTime? x, DateTime y);
  }
}
