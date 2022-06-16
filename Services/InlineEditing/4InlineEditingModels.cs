// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.FieldModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.InlineEditing
{
  /// <summary>This class represent the field model</summary>
  public class FieldModel
  {
    public string Name { get; set; }

    public string Required { get; set; }

    public string MinDate { get; set; }

    public string MaxDate { get; set; }

    public int MinLength { get; set; }

    public int MaxLength { get; set; }

    public string Pattern { get; set; }

    public object MinValue { get; set; }

    public object MaxValue { get; set; }

    public string RequiredViolationMessage { get; set; }

    public string MinLengthViolationMessage { get; set; }

    public string MaxLengthViolationMessage { get; set; }
  }
}
