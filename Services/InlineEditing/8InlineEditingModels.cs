﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.FieldValueModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.InlineEditing
{
  /// <summary>This class represents the field value model</summary>
  public class FieldValueModel
  {
    public string Name { get; set; }

    public object Value { get; set; }

    public FieldValueModel[] ComplexValue { get; set; }
  }
}
