// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.FieldDefinitionElementAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  public class FieldDefinitionElementAttribute : Attribute
  {
    private string fieldDefinitionTypeName;
    private Type fieldDefinitionType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.FieldDefinitionElementAttribute" /> class.
    /// </summary>
    public FieldDefinitionElementAttribute()
      : this((Type) null, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.FieldDefinitionElementAttribute" /> class.
    /// </summary>
    /// <param name="controlDesignerType">Type of the control designer.</param>
    public FieldDefinitionElementAttribute(Type fieldDefinitionType)
      : this(fieldDefinitionType, fieldDefinitionType.FullName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.FieldDefinitionElementAttribute" /> class.
    /// </summary>
    /// <param name="controlDesignerTypeName">Name of the control designer type.</param>
    public FieldDefinitionElementAttribute(string controlDesignerTypeName)
      : this((Type) null, controlDesignerTypeName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.FieldDefinitionElementAttribute" /> class.
    /// </summary>
    /// <param name="fieldDefinitionType">Type of the field definition.</param>
    /// <param name="fieldDefinitionTypeName">Name of the field definition type.</param>
    private FieldDefinitionElementAttribute(
      Type fieldDefinitionType,
      string fieldDefinitionTypeName)
    {
      this.fieldDefinitionType = fieldDefinitionType;
      this.fieldDefinitionTypeName = fieldDefinitionTypeName;
    }

    /// <summary>Gets the name of the field definition type.</summary>
    /// <value>The name of the field definition type.</value>
    public string FieldDefinitionTypeName => this.fieldDefinitionTypeName;

    /// <summary>Gets the type of the field definition.</summary>
    /// <value>The type of the field definition.</value>
    public Type FieldDefinitionType => this.fieldDefinitionType;
  }
}
