// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model.SectionGroup
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
  internal class SectionGroup
  {
    public SectionGroup()
    {
    }

    public SectionGroup(string name, IEnumerable<SectionDto> sections)
    {
      this.Name = name;
      this.Sections = sections.Select<SectionDto, Section>((Func<SectionDto, Section>) (s => new Section(s.Name, s.Title, s.CategoryName, s.Properties)));
    }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public IEnumerable<Section> Sections { get; set; }
  }
}
