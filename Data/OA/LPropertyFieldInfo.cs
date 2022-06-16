// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.LPropertyFieldInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;

namespace Telerik.Sitefinity.Data.OA
{
  internal class LPropertyFieldInfo
  {
    public string FieldName { get; set; }

    public string Extension { get; set; }

    public string ColumnName { get; set; }

    public int? DBLength { get; set; }

    public string DBScale { get; set; }

    public string DBType { get; set; }

    public CultureInfo Language { get; set; }

    public bool IsMainProperty { get; set; }

    public bool IgnoreSplitTables { get; set; }

    public Type ClrType { get; set; }
  }
}
