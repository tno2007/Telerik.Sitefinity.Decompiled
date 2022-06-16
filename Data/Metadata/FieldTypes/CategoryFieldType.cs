// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.FieldTypes.CategoryFieldType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Data.Metadata.FieldTypes
{
  /// <summary>Logical type for hierarchiccal taxonomy fields</summary>
  public class CategoryFieldType : FieldType
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.CategoryFieldType" /> class.
    /// </summary>
    public CategoryFieldType()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.CategoryFieldType" /> class.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    public CategoryFieldType(MetaField metaField)
      : base(metaField)
    {
    }

    /// <summary>Gets or sets the field type description.</summary>
    /// <value>The field type description.</value>
    public override string FieldTypeDescription
    {
      get => "Use to keep categorization data";
      set
      {
      }
    }

    /// <summary>Gets or sets the taxonomy ID.</summary>
    /// <value>The taxonomy ID.</value>
    [ContentSection("General", PositionInSection = 4)]
    [UIHint("TaxonomyPickerFieldControl")]
    [DisplayName("Choose the taxonomy to be used for this field")]
    public Guid TaxonomyID
    {
      get => this.Field.TaxonomyId;
      set => this.Field.TaxonomyId = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is single taxon.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is single taxon; otherwise, <c>false</c>.
    /// </value>
    [ContentSection("General", PositionInSection = 5)]
    [DisplayName("Single choice field")]
    [Description("Check if you want the field to allow only single choice of category items")]
    public bool IsSingleTaxon
    {
      get => this.Field.IsSingleTaxon;
      set => this.Field.IsSingleTaxon = value;
    }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    [ContentSection("General", PositionInSection = 3)]
    [Required]
    [DefaultValue("OpenAccessDataProvider")]
    public string TaxonomyProvider
    {
      get => this.Field.TaxonomyProvider;
      set => this.Field.TaxonomyProvider = value;
    }

    /// <summary>Gets the name of the field type.</summary>
    /// <value>The name of the field type.</value>
    public override string FieldTypeName => "category";

    /// <summary>Gets the field type title.</summary>
    /// <value>The field type title.</value>
    public override string FieldTypeTitle
    {
      get => "Category and Tags";
      set
      {
      }
    }
  }
}
