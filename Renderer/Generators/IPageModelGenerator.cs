// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Generators.IPageModelGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;

namespace Telerik.Sitefinity.Renderer.Generators
{
  internal interface IPageModelGenerator
  {
    PageDto Generate(IGeneratorArgs args);

    ComponentDto GenerateHierarchicalWidgetModel(IGetComponentArgs args);

    ComponentDto[] GenerateLazyComponents(IGeneratorArgs args);

    ComponentDto GetComponent(IGetComponentArgs args);

    void SetComponentPropertyValues(ISetComponentArgs args);

    ControlData GetControlData(IGetComponentArgs args, out Guid pageId);
  }
}
