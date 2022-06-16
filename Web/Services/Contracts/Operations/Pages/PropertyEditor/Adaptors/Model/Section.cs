// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model.Section
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Progress.Sitefinity.Renderer.Designers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model
{
  internal class Section
  {
    public Section(string name)
    {
      this.Properties = (IList<PropertyContainer>) new List<PropertyContainer>();
      this.Name = name;
    }

    public Section(string name, string title)
      : this(name)
    {
      this.Title = title;
    }

    public Section(string name, string title, string categoryName)
      : this(name, title)
    {
      this.CategoryName = categoryName;
    }

    public Section(
      string name,
      string title,
      string categoryName,
      IList<PropertyMetadataContainerDto> properties)
      : this(name, title, categoryName)
    {
      this.Properties = (IList<PropertyContainer>) properties.Select<PropertyMetadataContainerDto, PropertyContainer>((Func<PropertyMetadataContainerDto, PropertyContainer>) (p => new PropertyContainer(p))).ToList<PropertyContainer>();
    }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public IList<PropertyContainer> Properties { get; set; }

    internal string CategoryName { get; set; }
  }
}
