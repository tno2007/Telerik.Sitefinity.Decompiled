// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model.PropertyValueGroupContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model
{
  /// <summary>
  /// Container for group of properties
  /// Intended to use as set context for editing properties.
  /// </summary>
  public class PropertyValueGroupContainer
  {
    /// <summary>Gets or sets the property localization mode.</summary>
    /// <value>The property localization mode.</value>
    [DataMember]
    public string ComponentId { get; set; }

    /// <summary>Gets or sets the widget caption.</summary>
    [DataMember]
    public string Caption { get; set; }

    /// <summary>Gets or sets the property localization mode.</summary>
    /// <value>The property localization mode.</value>
    [DataMember]
    public string PropertyLocalizationMode { get; set; }

    /// <summary>Gets or sets the properties.</summary>
    /// <value>The properties.</value>
    [DataMember]
    public IEnumerable<PropertyValueContainer> Properties { get; set; }

    /// <summary>Gets or sets the properties metadata.</summary>
    /// <value>The properties metadata.</value>
    [DataMember]
    public IEnumerable<PropertyContainer> PropertyMetadata { get; set; }
  }
}
