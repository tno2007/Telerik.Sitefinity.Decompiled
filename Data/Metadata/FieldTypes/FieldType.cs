// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.FieldTypes.FieldType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Metadata.FieldTypes
{
  /// <summary>
  /// Base class for logical field types. The logical fields type represent preset MetaFields that expose only the valid Properties for the specific type
  /// For example Text field type has MaxLength but not MaxValue and RegularEpxression
  /// The logical field types also have attibutes which describe the typed properties and can have auto-generated UI designers
  /// </summary>
  [ContentSections(SectionName = "General", TitleResourceId = "MetaTypesSectionNames")]
  [ContentSections(SectionName = "Location", TitleResourceId = "MetaTypesSectionNames")]
  [ContentSections(SectionName = "Validation", TitleResourceId = "MetaTypesSectionNames")]
  [ContentSections(SectionName = "Initialization", TitleResourceId = "MetaTypesSectionNames")]
  [ContentSections(SectionName = "UI", TitleResourceId = "MetaTypesSectionNames")]
  [DataContract]
  public abstract class FieldType
  {
    /// <summary>
    /// 
    /// </summary>
    protected MetaField field;
    /// <summary>
    /// 
    /// </summary>
    private MetadataManager manager;
    private static Dictionary<string, FieldType> registeredFieldTypes;
    private static string key = typeof (FieldType).FullName + "registeredFieldTypesKey";

    /// <summary>Gets or sets the registered field types.</summary>
    /// <value>The registered field types.</value>
    static FieldType()
    {
      FieldType.registeredFieldTypes = new Dictionary<string, FieldType>();
      FieldType.AddPrototype("text", typeof (TextFieldType));
      FieldType.AddPrototype("checkbox", typeof (CheckboxFieldType));
      FieldType.AddPrototype("calendar", typeof (DateTimeFieldType));
      FieldType.AddPrototype("integer", typeof (IntegerFieldType));
      FieldType.AddPrototype("html_content", typeof (HTMLContentType));
      FieldType.AddPrototype("category", typeof (CategoryFieldType));
    }

    private static void AddPrototype(string key, Type type)
    {
      FieldType instance = Activator.CreateInstance(type) as FieldType;
      FieldType.registeredFieldTypes.Add(key, instance);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.FieldType" /> class.
    /// </summary>
    public FieldType()
    {
    }

    public virtual void BuildMetaField() => this.Field.SitefinityType = this.FieldTypeName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.FieldTypes.FieldType" /> class.
    /// </summary>
    /// <param name="metaField">The meta field.</param>
    public FieldType(MetaField metaField)
    {
      this.field = metaField;
      this.BuildMetaField();
    }

    /// <summary>Gets or sets the field.</summary>
    /// <value>The field.</value>
    [HideField]
    public MetaField Field
    {
      get => this.field;
      set => this.field = value;
    }

    [HideField]
    [DataMember]
    public Guid Id
    {
      get => this.field.Id;
      set
      {
      }
    }

    /// <summary>Gets the name of the field type.</summary>
    /// <value>The name of the field type.</value>
    [HideField]
    public abstract string FieldTypeName { get; }

    /// <summary>Gets the field type title.</summary>
    /// <value>The field type title.</value>
    [DisplayName("Field Type")]
    [ContentSection("General", PositionInSection = 0)]
    [DataMember]
    public abstract string FieldTypeTitle { get; set; }

    [HideField]
    public abstract string FieldTypeDescription { get; set; }

    /// <summary>Gets or sets the full description.</summary>
    /// <value>The full description.</value>
    public string FullDescription
    {
      get => this.FieldTypeTitle + "<br/>" + this.FieldTypeDescription;
      set
      {
      }
    }

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    [System.ComponentModel.DataAnnotations.Required]
    [DisplayName("Field Name")]
    [ContentSection("General", PositionInSection = 1)]
    [DataMember]
    public string FieldName
    {
      get => this.field.FieldName;
      set => this.field.FieldName = value;
    }

    /// <summary>Gets or sets the manager.</summary>
    /// <value>The manager.</value>
    [HideField]
    public MetadataManager Manager
    {
      set => this.manager = value;
      get => this.manager;
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [DisplayName("Title")]
    [ContentSection("General", PositionInSection = 2)]
    [DataMember]
    public Lstring Title
    {
      get => (Lstring) this.field.Title;
      set => this.field.Title = (string) value;
    }

    /// <summary>Gets or sets the description.</summary>
    /// <value>The description.</value>
    [DisplayName("Description")]
    [System.ComponentModel.Description("Describe the purpose of the field")]
    [ContentSection("General", PositionInSection = 3)]
    [DataMember]
    public Lstring Description
    {
      get => (Lstring) this.field.Description;
      set => this.field.Description = (string) value;
    }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value>The name of the section.</value>
    [DisplayName("Section")]
    [System.ComponentModel.Description("Specify, which property section should the field be placed")]
    [ContentSection("Location", PositionInSection = 0)]
    [DataMember]
    public string SectionName
    {
      get => this.Field.SectionName;
      set => this.Field.SectionName = value;
    }

    /// <summary>Gets or sets the position in section.</summary>
    /// <value>The position in section.</value>
    [DisplayName("Section position")]
    [System.ComponentModel.Description("Specify, the field position in the section")]
    [ContentSection("Location", PositionInSection = 1)]
    [DataMember]
    public int PositionInSection
    {
      get => this.Field.PositionInSection;
      set => this.Field.PositionInSection = value;
    }

    [DisplayName("Required")]
    [System.ComponentModel.Description("Specify if the field requires input")]
    [ContentSection("General", PositionInSection = 5)]
    [DataMember]
    public bool Required
    {
      get => this.field.Required;
      set => this.field.Required = value;
    }

    /// <summary>
    /// Gets or sets the UI hint - which field control to be used when rendering the field.
    /// </summary>
    /// <value>The UI hint.</value>
    [DisplayName("Control to be used")]
    [ContentSection("UI", PositionInSection = 0)]
    public string UIHint
    {
      get => this.field.UIHint;
      set => this.field.UIHint = value;
    }

    /// <summary>Creates the field.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">The name.</param>
    /// <param name="manager">The manager.</param>
    /// <returns></returns>
    public static FieldType CreateField(
      string fieldName,
      string typeName,
      MetadataManager manager)
    {
      MetaField metafield = manager.CreateMetafield(fieldName);
      metafield.SitefinityType = typeName;
      return FieldType.GetField(metafield);
    }

    /// <summary>Gets the logical field type of a metafield.</summary>
    /// <param name="field">The field.</param>
    /// <returns></returns>
    public static FieldType GetField(MetaField field)
    {
      Type type;
      if (field.SitefinityType == null)
      {
        string key = "text";
        string clrType = field.ClrType;
        if (!(clrType == "System.String"))
        {
          if (!(clrType == "System.DateTime"))
          {
            if (!(clrType == "System.Int32"))
            {
              if (!(clrType == "System.Double"))
              {
                if (clrType == "System.Boolean")
                  key = "checkbox";
              }
              else
                key = "double";
            }
            else
              key = "integer";
          }
          else
            key = "calendar";
        }
        else
          key = "text";
        if (!field.TaxonomyId.Equals(Guid.Empty))
          key = "category";
        type = FieldType.registeredFieldTypes[key].GetType();
      }
      else
        type = FieldType.registeredFieldTypes[field.SitefinityType].GetType();
      return Activator.CreateInstance(type, (object) field) as FieldType;
    }

    /// <summary>
    /// Gets the logical field types from a metatype by converting the metafields to logical types.
    /// </summary>
    /// <param name="fieldsType">Type of the fields.</param>
    /// <returns></returns>
    public static List<FieldType> GetFields(MetaType fieldsType)
    {
      List<FieldType> fields = new List<FieldType>();
      foreach (MetaField field in fieldsType.Fields.Where<MetaField>((Func<MetaField, bool>) (f => !f.IsInternal)))
        fields.Add(FieldType.GetField(field));
      return fields;
    }
  }
}
