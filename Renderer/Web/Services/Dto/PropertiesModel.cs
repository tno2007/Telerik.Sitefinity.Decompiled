// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Web.Services.Dto.PropertiesModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.Renderer.Web.Services.Dto
{
  /// <summary>A container class for holding dynamic properties</summary>
  public class PropertiesModel : IDynamicPropertiesContainer
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Renderer.Web.Services.Dto.PropertiesModel" /> class.
    /// </summary>
    public PropertiesModel() => this.Properties = (IDictionary<string, object>) new Dictionary<string, object>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Renderer.Web.Services.Dto.PropertiesModel" /> class.
    /// </summary>
    /// <param name="properties">The properties.</param>
    public PropertiesModel(IDictionary<string, object> properties) => this.Properties = properties;

    /// <summary>Gets or the dynamic properties.</summary>
    public IDictionary<string, object> Properties { get; private set; }
  }
}
