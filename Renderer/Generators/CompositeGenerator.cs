// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Generators.CompositeGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Editor.Specifics;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;

namespace Telerik.Sitefinity.Renderer.Generators
{
  internal class CompositeGenerator
  {
    private IPageModelGenerator[] generators;

    public CompositeGenerator(IPageModelGenerator[] generators) => this.generators = generators;

    public ComponentDto GetComponent(IGetComponentArgs args) => this.GetGenerator((IGeneratorArgs) args, args.ContainerResolver).GetComponent(args);

    public ControlData GetControlData(IGetComponentArgs args, out Guid pageId) => this.GetGenerator((IGeneratorArgs) args, args.ContainerResolver).GetControlData(args, out pageId);

    public void SetComponentPropertyValues(ISetComponentArgs args) => this.GetGenerator((IGeneratorArgs) args, args.ContainerResolver).SetComponentPropertyValues(args);

    public PageDto Generate(IGeneratorArgs args) => this.GetGenerator(args, args.ContainerResolver).Generate(args);

    public ComponentDto GenerateHierarchicalWidgetModel(IGetComponentArgs args) => this.GetGenerator((IGeneratorArgs) args, args.ContainerResolver).GenerateHierarchicalWidgetModel(args);

    public ComponentsResponse GenerateLazyComponents(IGeneratorArgs args)
    {
      IPageModelGenerator generator = this.GetGenerator(args, args.ContainerResolver);
      return new ComponentsResponse()
      {
        Components = generator.GenerateLazyComponents(args)
      };
    }

    private IPageModelGenerator GetGenerator(
      IGeneratorArgs args,
      IContainerResolver containerResolver)
    {
      return containerResolver.GetContainer(args.IsEdit, args.Version).GetLastContainer<IRendererCommonData>().IsExternallyRendered() ? ((IEnumerable<IPageModelGenerator>) this.generators).FirstOrDefault<IPageModelGenerator>((Func<IPageModelGenerator, bool>) (x => x is AgnosticGenerator)) : ((IEnumerable<IPageModelGenerator>) this.generators).FirstOrDefault<IPageModelGenerator>((Func<IPageModelGenerator, bool>) (x => x is WebFormsAndMvcGenerator));
    }
  }
}
