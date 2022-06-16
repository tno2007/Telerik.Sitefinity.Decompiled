// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Web.Services.Dto.ComponentDto
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Renderer.Web.Services.Dto
{
  internal class ComponentDto
  {
    public ComponentDto()
    {
      this.Children = (IList<ComponentDto>) new List<ComponentDto>();
      this.PropertiesModel = new PropertiesModel();
      this.PlaceHolderMap = (IDictionary<string, string>) new Dictionary<string, string>();
    }

    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public Guid SiblingId { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string PlaceHolder { get; set; }

    [DataMember]
    public string Caption { get; set; }

    [DataMember]
    public string ViewName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the widget is rendered lazily. E.g. if there is personalization per widget,
    /// the widget will be rendered via a service call in the last moment on the client.
    /// </summary>
    [DataMember]
    public bool Lazy { get; set; }

    /// <summary>
    /// Gets or sets the properties.
    /// Deserialization hack: DataMember properties are used by the OData serializer
    /// JsonIgnore properties are used by the newtonsoft deserializer. OData serailizes Dictionary in a different manner
    /// </summary>
    [DataMember(EmitDefaultValue = false, Name = "Properties")]
    [JsonIgnore]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "words are spelled correctly.")]
    public PropertiesModel PropertiesModel { get; set; }

    public IDictionary<string, string> Properties { get; set; }

    [DataMember]
    public IList<ComponentDto> Children { get; set; }

    public IDictionary<string, string> PlaceHolderMap { get; set; }

    public void AddProperty(string name, object value) => this.PropertiesModel.Properties.Add(name, value);
  }
}
