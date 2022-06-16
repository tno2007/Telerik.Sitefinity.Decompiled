// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.FieldViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>
  /// Defines WCF data transfer object for the data structure type and the backend user interface of the modules.
  /// </summary>
  [DataContract]
  public class FieldViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ModuleEditor.Web.Services.Model.FieldViewModel" /> class.
    /// </summary>
    public FieldViewModel()
    {
    }

    internal FieldViewModel(PropertyDescriptor descriptor)
    {
      MetaFieldAttributeAttribute attributeAttribute = descriptor.Attributes.OfType<MetaFieldAttributeAttribute>().FirstOrDefault<MetaFieldAttributeAttribute>();
      if (attributeAttribute != null)
      {
        string str = (string) null;
        this.IsBuiltIn = attributeAttribute.Attributes.TryGetValue(DynamicAttributeNames.IsBuiltIn, out str) && bool.Parse(str);
      }
      if (!(descriptor is RelatedDataPropertyDescriptor propertyDescriptor))
        return;
      this.RelatedFieldDefinition = WcfDefinitionBuilder.GetWcfDefinition(typeof (RelatedMediaField), propertyDescriptor.ComponentType, propertyDescriptor.MetaField.FieldName, "editCustomField", propertyDescriptor.IsLocalizable);
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    [DataMember]
    public string ContentType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is custom.
    /// </summary>
    /// <value><c>true</c> if this instance is custom; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool IsCustom { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this field is built in (Installed by default).
    /// </summary>
    /// <value><c>true</c> if this instance is built in; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool IsBuiltIn { get; set; }

    /// <summary>Gets or sets the non-friendly type name of the field</summary>
    [DataMember]
    public string FieldTypeName { get; set; }

    /// <summary>Gets or sets field definition of custom related field</summary>
    [DataMember]
    public WcfFieldDefinition RelatedFieldDefinition { get; private set; }
  }
}
