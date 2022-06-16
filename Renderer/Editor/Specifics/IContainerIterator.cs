// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.Specifics.ContainerIterator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Renderer.Editor.Specifics
{
  internal class ContainerIterator : IContainerIterator
  {
    public IList<IControlsContainer> GetContainers(IControlsContainer source) => (IList<IControlsContainer>) this.GetControlContainers(source).ToList<IControlsContainer>();

    public IList<ControlData> GetControlsFromAllContainers(IControlsContainer source) => (IList<ControlData>) this.GetControlContainers(source).SelectMany<IControlsContainer, ControlData>((Func<IControlsContainer, IEnumerable<ControlData>>) (x => x.Controls)).ToList<ControlData>();

    private IEnumerable<IControlsContainer> GetControlContainers(
      IControlsContainer controlsContainer)
    {
      if (controlsContainer is BaseControlsContainerWrapper containerWrapper)
        controlsContainer = containerWrapper.Container;
      if (controlsContainer is PageData dataItem)
      {
        IList<IControlsContainer> controlsContainers = this.GetPageTemplateControlsContainers(dataItem.Template);
        controlsContainers.Insert(0, (IControlsContainer) new PageDataControlsContainerWrapper(controlsContainer, dataItem.GetProvider() as PageDataProvider));
        return (IEnumerable<IControlsContainer>) controlsContainers;
      }
      if (controlsContainer is PageDraft pageDraft)
      {
        IList<IControlsContainer> controlsContainers = this.GetPageTemplateControlsContainers(pageDraft.Template);
        controlsContainers.Insert(0, (IControlsContainer) new PageDraftControlsContainerWrapper((IControlsContainer) pageDraft, pageDraft.GetProvider() as PageDataProvider));
        return (IEnumerable<IControlsContainer>) controlsContainers;
      }
      if (controlsContainer is TemplateDraft templateDraft)
      {
        IList<IControlsContainer> controlsContainers = this.GetPageTemplateControlsContainers(templateDraft.DraftTemplate);
        controlsContainers.Insert(0, (IControlsContainer) new TemplateDraftControlsContainerWrapper((IControlsContainer) templateDraft, templateDraft.GetProvider() as PageDataProvider));
        return (IEnumerable<IControlsContainer>) controlsContainers;
      }
      if (controlsContainer is PageTemplate pageTemplate)
      {
        IList<IControlsContainer> controlsContainers = this.GetPageTemplateControlsContainers(pageTemplate.ParentTemplate);
        controlsContainers.Insert(0, (IControlsContainer) new TemplateControlsContainerWrapper((IControlsContainer) pageTemplate, pageTemplate.GetProvider() as PageDataProvider));
        return (IEnumerable<IControlsContainer>) controlsContainers;
      }
      return (IEnumerable<IControlsContainer>) new IControlsContainer[1]
      {
        controlsContainer
      };
    }

    private IList<IControlsContainer> GetPageTemplateControlsContainers(
      PageTemplate pageTemplate)
    {
      List<IControlsContainer> controlsContainers = new List<IControlsContainer>();
      if (pageTemplate != null)
      {
        controlsContainers.Add((IControlsContainer) new TemplateControlsContainerWrapper((IControlsContainer) pageTemplate, pageTemplate.GetProvider() as PageDataProvider));
        if (pageTemplate.ParentTemplate != null)
          controlsContainers.AddRange((IEnumerable<IControlsContainer>) this.GetPageTemplateControlsContainers(pageTemplate.ParentTemplate));
      }
      return (IList<IControlsContainer>) controlsContainers;
    }
  }
}
