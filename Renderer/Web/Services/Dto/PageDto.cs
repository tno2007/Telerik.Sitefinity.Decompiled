// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Web.Services.Dto.PageDto
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Renderer.Web.Services.Dto
{
  internal class PageDto
  {
    public PageDto() => this.ComponentContext = new ComponentContext();

    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public string TemplateName { get; set; }

    [DataMember]
    public ComponentContext ComponentContext { get; set; }
  }
}
