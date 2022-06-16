// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.FieldTypes.HTMLContentType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Data.Metadata.FieldTypes
{
  /// <summary>Rich text(HTML) field</summary>
  public class HTMLContentType : FieldType
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.HTMLContentType" /> class.
    /// </summary>
    public HTMLContentType()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.HTMLContentType" /> class.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    public HTMLContentType(MetaField metaField)
      : base(metaField)
    {
    }

    /// <summary>Builds the meta field.</summary>
    public override void BuildMetaField()
    {
      base.BuildMetaField();
      this.Field.ClrType = typeof (string).FullName;
      this.Field.UIHint = "HtmlEditorFieldControl";
    }

    /// <summary>Gets the name of the field type.</summary>
    /// <value>The name of the field type.</value>
    public override string FieldTypeName => "html_content";

    /// <summary>Gets the field type title.</summary>
    /// <value>The field type title.</value>
    public override string FieldTypeTitle
    {
      get => "Rich Content";
      set
      {
      }
    }

    /// <summary>Gets or sets the field type description.</summary>
    /// <value>The field type description.</value>
    public override string FieldTypeDescription
    {
      get => "Use to keep HTML formatted content";
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the the maximu length of a string control.
    /// </summary>
    /// <value>The length of the max.</value>
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
