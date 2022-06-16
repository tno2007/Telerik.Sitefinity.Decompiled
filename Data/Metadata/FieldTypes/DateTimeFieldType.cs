// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.FieldTypes.DateTimeFieldType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Data.Metadata.FieldTypes
{
  /// <summary>Logical fields types for Date time fields</summary>
  public class DateTimeFieldType : FieldType
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.DateTimeFieldType" /> class.
    /// </summary>
    public DateTimeFieldType()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.DateTimeFieldType" /> class.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    public DateTimeFieldType(MetaField metaField)
      : base(metaField)
    {
    }

    /// <summary>Builds the meta field.</summary>
    public override void BuildMetaField()
    {
      base.BuildMetaField();
      this.field.ClrType = typeof (DateTime?).FullName;
      this.field.DBType = "DATE";
      this.field.DBSqlType = "DATETIME";
    }

    /// <summary>Gets the name of the field type.</summary>
    /// <value>The name of the field type.</value>
    public override string FieldTypeName => "calendar";

    /// <summary>Gets the field type title.</summary>
    /// <value>The field type title.</value>
    public override string FieldTypeTitle
    {
      get => "Calendar";
      set
      {
      }
    }

    /// <summary>Gets or sets the field type description.</summary>
    /// <value>The field type description.</value>
    public override string FieldTypeDescription
    {
      get => "Use to keep date values";
      set
      {
      }
    }

    /// <summary>Gets or sets the min value.</summary>
    /// <value>The min value.</value>
    [DisplayName("Minimum date allowed")]
    [Description("Validation range")]
    [ContentSection("Validation", PositionInSection = 0)]
    public DateTime? MinValue
    {
      get => this.Field.GetMinValue<DateTime?>();
      set => this.Field.SetMinValue((object) value);
    }

    /// <summary>Gets or sets the max value.</summary>
    /// <value>The max value.</value>
    [DisplayName("Maximum date allowed")]
    [Description("Validation range")]
    [ContentSection("Validation", PositionInSection = 1)]
    public DateTime? MaxValue
    {
      get => this.Field.GetMaxValue<DateTime?>();
      set => this.Field.SetMaxValue((object) value);
    }

    /// <summary>Gets or sets the default value.</summary>
    /// <value>The default value.</value>
    [DisplayName("Default date")]
    [Description("Initialization")]
    [ContentSection("Initialization", PositionInSection = 0)]
    public DateTime? DefaultValue
    {
      get => this.Field.GetDefaultValue<DateTime?>();
      set => this.Field.SetDefaultValue((object) value);
    }
  }
}
