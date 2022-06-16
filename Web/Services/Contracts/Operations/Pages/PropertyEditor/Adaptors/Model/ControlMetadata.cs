// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model.ControlMetadata
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model
{
  internal class ControlMetadata : IControlMetadata
  {
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Caption { get; set; }

    [DataMember]
    public string ViewName { get; set; }

    [DataMember]
    public IList<SectionGroup> PropertyMetadata { get; set; }

    [DataMember]
    public IList<PropertyContainer> PropertyMetadataFlat { get; set; }
  }
}
