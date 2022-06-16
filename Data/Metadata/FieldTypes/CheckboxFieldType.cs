// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.FieldTypes.CheckboxFieldType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Data.Metadata.FieldTypes
{
  /// <summary>Logical field type for boolean fields</summary>
  public class CheckboxFieldType : FieldType
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.CheckboxFieldType" /> class.
    /// </summary>
    public CheckboxFieldType()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.CheckboxFieldType" /> class.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    public CheckboxFieldType(MetaField metaField)
      : base(metaField)
    {
    }

    /// <summary>Builds the meta field.</summary>
    public override void BuildMetaField()
    {
      base.BuildMetaField();
      this.field.ClrType = typeof (bool).FullName;
      this.field.DBType = "BIT";
      this.field.DBSqlType = "BIT";
    }

    /// <summary>Gets the field type title.</summary>
    /// <value>The field type title.</value>
    public override string FieldTypeTitle
    {
      get => "Check box";
      set
      {
      }
    }

    /// <summary>Gets or sets the field type description.</summary>
    /// <value>The field type description.</value>
    public override string FieldTypeDescription
    {
      get => "Use to hold true/false values";
      set
      {
      }
    }

    /// <summary>Gets the name of the field type.</summary>
    /// <value>The name of the field type.</value>
    public override string FieldTypeName => "checkbox";

    /// <summary>Gets or sets the default value.</summary>
    /// <value>The default value.</value>
    [DisplayName("Default value")]
    [Description("Initialization")]
    public bool? DefaultValue
    {
      get => this.field.GetDefaultValue<bool?>();
      set => this.field.SetDefaultValue((object) value);
    }
  }
}
