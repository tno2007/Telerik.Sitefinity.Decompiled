// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.FieldTypes.IntegerFieldType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Data.Metadata.FieldTypes
{
  /// <summary>Logical field type for Integer fields</summary>
  public class IntegerFieldType : FieldType
  {
    public IntegerFieldType()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.IntegerFieldType" /> class.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    public IntegerFieldType(MetaField metaField)
      : base(metaField)
    {
    }

    /// <summary>Builds the meta field.</summary>
    public override void BuildMetaField()
    {
      base.BuildMetaField();
      this.field.ClrType = typeof (int).FullName;
      this.field.DBType = "INTEGER";
      this.field.DBSqlType = "INT";
    }

    /// <summary>Gets the name of the field type.</summary>
    /// <value>The name of the field type.</value>
    public override string FieldTypeName => "integer";

    /// <summary>Gets the field type title.</summary>
    /// <value>The field type title.</value>
    public override string FieldTypeTitle
    {
      get => "Integer numeric";
      set
      {
      }
    }

    /// <summary>Gets or sets the field type description.</summary>
    /// <value>The field type description.</value>
    public override string FieldTypeDescription
    {
      get => "Use to keep integer numbers";
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the minimum allowed value for this field.
    /// </summary>
    /// <value>The min value.</value>
    [DisplayName("Minimum number allowed")]
    [Description("Validation range")]
    [ContentSection("Validation", PositionInSection = 0)]
    public int? MinValue
    {
      get => this.Field.GetMinValue<int?>();
      set => this.Field.SetMinValue((object) value);
    }

    /// <summary>
    /// Gets or sets the maximum allowed value for this field.
    /// </summary>
    /// <value>The max value.</value>
    [DisplayName("Maximum number allowed")]
    [Description("Validation range")]
    [ContentSection("Validation", PositionInSection = 1)]
    public int? MaxValue
    {
      get => this.Field.GetMaxValue<int?>();
      set => this.Field.SetMaxValue((object) value);
    }

    /// <summary>Gets or sets the default value for this field.</summary>
    /// <value>The default value.</value>
    [DisplayName("Default number")]
    [Description("Initialization")]
    [ContentSection("Initialization", PositionInSection = 0)]
    public int? DefaultValue
    {
      get => this.Field.GetDefaultValue<int?>();
      set => this.Field.SetDefaultValue((object) value);
    }
  }
}
