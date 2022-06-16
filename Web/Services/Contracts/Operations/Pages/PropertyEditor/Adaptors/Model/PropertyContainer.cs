// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model.PropertyContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Progress.Sitefinity.Renderer.Designers.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model
{
  /// <summary>Container for properties</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class PropertyContainer
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model.PropertyContainer"></see> class
    /// </summary>
    public PropertyContainer()
    {
      this.Properties = new PropertiesModel();
      this.TypeChildProperties = (IEnumerable<PropertyContainer>) new List<PropertyContainer>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model.PropertyContainer"></see> class
    /// </summary>
    /// <param name="propContainer">The property container.</param>
    public PropertyContainer(PropertyMetadataContainerDto propContainer)
    {
      this.Name = propContainer.Name;
      this.SectionName = propContainer.SectionName;
      this.Title = propContainer.Title;
      this.Type = propContainer.Type;
      this.CategoryName = propContainer.CategoryName;
      this.DefaultValue = propContainer.DefaultValue;
      this.Properties = new PropertiesModel();
      foreach (KeyValuePair<string, string> property in (IEnumerable<KeyValuePair<string, string>>) propContainer.Properties)
        this.Properties.Properties.Add(property.Key, (object) property.Value);
      this.TypeChildProperties = (IEnumerable<PropertyContainer>) propContainer.TypeChildProperties.Select<PropertyMetadataContainerDto, PropertyContainer>((Func<PropertyMetadataContainerDto, PropertyContainer>) (p => new PropertyContainer(p))).ToList<PropertyContainer>();
    }

    /// <summary>Gets or sets the name</summary>
    [DataMember(IsRequired = true)]
    public string Name { get; set; }

    /// <summary>Gets or sets the name</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the default property value for this type.
    /// </summary>
    [DataMember]
    public string DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets the type of the property - string, int, date
    /// </summary>
    [DataMember(IsRequired = true)]
    public string Type { get; set; }

    /// <summary>Gets or sets the child properties of the type.</summary>
    [DataMember]
    public IEnumerable<PropertyContainer> TypeChildProperties { get; set; }

    /// <summary>Gets or sets the dynamic properties</summary>
    [DataMember]
    public PropertiesModel Properties { get; set; }

    internal string SectionName { get; set; }

    internal string CategoryName { get; set; }
  }
}
