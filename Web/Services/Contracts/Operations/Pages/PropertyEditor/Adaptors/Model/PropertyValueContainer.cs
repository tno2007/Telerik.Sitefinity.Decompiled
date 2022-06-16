// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model.PropertyValueContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model
{
  /// <summary>Container for property value</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class PropertyValueContainer
  {
    /// <summary>Gets or sets the name</summary>
    [DataMember(IsRequired = true)]
    public string Name { get; set; }

    /// <summary>Gets or sets the name</summary>
    [DataMember]
    public string Value { get; set; }
  }
}
