// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.FieldTypes.TextFieldType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Data.Metadata.FieldTypes
{
  /// <summary>Logical field type for text fields</summary>
  public class TextFieldType : FieldType
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.TextFieldType" /> class.
    /// </summary>
    public TextFieldType()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.TextFieldType" /> class.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    public TextFieldType(MetaField metaField)
      : base(metaField)
    {
    }

    public override void BuildMetaField()
    {
      base.BuildMetaField();
      this.Field.ClrType = typeof (string).FullName;
      this.Field.DBType = "VARCHAR";
      this.Field.DBSqlType = "NVARCHAR";
    }

    public override string FieldTypeName => "text";

    public override string FieldTypeTitle
    {
      get => "Text";
      set
      {
      }
    }

    public override string FieldTypeDescription
    {
      get => "Use to keep simple textual content";
      set
      {
      }
    }

    [DisplayName("Regular expression")]
    [Description("Regular expression")]
    [ContentSection("Validation", PositionInSection = 1)]
    public string RegularExpression
    {
      get => this.Field.RegularExpression;
      set => this.Field.RegularExpression = value;
    }

    [DisplayName("Maximum text length allowed")]
    [Description("Text length limitation. Type 0 for unlimited")]
    [ContentSection("Validation", PositionInSection = 0)]
    public int MaxLength
    {
      get => this.Field.MaxLength;
      set => this.Field.MaxLength = value;
    }
  }
}
