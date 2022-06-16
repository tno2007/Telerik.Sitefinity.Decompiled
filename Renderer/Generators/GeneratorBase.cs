// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Generators.GeneratorBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Renderer.Generators
{
  internal abstract class GeneratorBase : IPageModelGenerator
  {
    public abstract PageDto Generate(IGeneratorArgs args);

    public abstract ComponentDto GenerateHierarchicalWidgetModel(IGetComponentArgs args);

    public abstract ComponentDto[] GenerateLazyComponents(IGeneratorArgs args);

    public ComponentDto GetComponent(IGetComponentArgs args)
    {
      using (new ElevatedModeRegion((IManager) args.ContainerResolver.GetManager()))
      {
        Guid pageId;
        return this.GetComponent(this.GetControlData(args.ContainerResolver, args.ComponentId, out pageId), pageId.ToString(), args.PreserveDynamicLinks);
      }
    }

    public void SetComponentPropertyValues(ISetComponentArgs args)
    {
      using (new ElevatedModeRegion((IManager) args.ContainerResolver.GetManager()))
      {
        Guid pageId;
        ControlData controlData = this.GetControlData(args.ContainerResolver, args.ComponentId, out pageId);
        controlData.Demand("Controls", "EditControlProperties");
        this.SetComponent(controlData, args.PropertiesGroup, pageId.ToString(), args.CleanPersistedProperites);
      }
    }

    public ControlData GetControlData(IGetComponentArgs args, out Guid pageId) => this.GetControlData(args.ContainerResolver, args.ComponentId, out pageId);

    private ControlData GetControlData(
      IContainerResolver containerResolver,
      string componentId,
      out Guid pageId)
    {
      pageId = Guid.Empty;
      Guid componentGuid = Guid.Parse(componentId);
      IControlsContainer container = containerResolver.GetContainer(true, (string) null);
      ControlData controlData = container.Controls.FirstOrDefault<ControlData>((Func<ControlData, bool>) (x => x.Id == componentGuid || x.BaseControlId == componentGuid));
      IControlManager manager = containerResolver.GetManager();
      if (controlData == null && container is PageDraft pageDraft)
      {
        Guid templateId = pageDraft.TemplateId;
        if (templateId != Guid.Empty)
        {
          for (PageTemplate pageTemplate = (manager as PageManager).GetTemplate(templateId); pageTemplate != null && controlData == null; pageTemplate = pageTemplate.ParentTemplate)
            controlData = (ControlData) pageTemplate.Controls.FirstOrDefault<TemplateControl>((Func<TemplateControl, bool>) (x => x.Id == componentGuid));
          if (controlData != null && !controlData.Editable)
            throw new ItemNotFoundException();
        }
        pageId = container.Id;
      }
      if (controlData == null)
        controlData = this.GetPersonalizedControlData(containerResolver, componentGuid, manager, out pageId);
      else if (pageId == Guid.Empty)
        pageId = this.GetPageId(controlData);
      return controlData != null ? controlData : throw new ItemNotFoundException();
    }

    private ControlData GetPersonalizedControlData(
      IContainerResolver containerResolver,
      Guid componentGuid,
      IControlManager pageManager,
      out Guid pageId)
    {
      ControlData personalizedControlData = (ControlData) null;
      pageId = Guid.Empty;
      ControlData control = pageManager.GetControl<ControlData>(componentGuid);
      if (control != null && control.PersonalizationMasterId != Guid.Empty)
      {
        ControlData controlData = this.GetControlData(containerResolver, control.PersonalizationMasterId.ToString(), out pageId);
        if (controlData != null)
        {
          personalizedControlData = control;
          pageId = this.GetPageId(controlData);
        }
      }
      else if (control != null && control is PageDraftControl pageDraftControl && pageDraftControl.Page != null && pageDraftControl.Page.ParentPage != null && pageDraftControl.Page.ParentPage.NavigationNode.Id == (containerResolver as PageDataContainerResolver).SiteNode.Id)
      {
        personalizedControlData = control;
        pageId = pageDraftControl.Page.Id;
      }
      return personalizedControlData;
    }

    private Guid GetPageId(ControlData controlData)
    {
      Guid pageId = Guid.Empty;
      switch (controlData)
      {
        case PageDraftControl pageDraftControl:
          pageId = pageDraftControl.Page.Id;
          break;
        case TemplateControl templateControl:
          pageId = templateControl.Page.Id;
          break;
      }
      return pageId;
    }

    protected abstract ComponentDto GetComponent(
      ControlData control,
      string pageId,
      bool preserveDynamicLinks);

    protected abstract void SetComponent(
      ControlData control,
      PropertyValueGroupContainer propertiesGroup,
      string pageId,
      bool cleanPersistedProperties = false);
  }
}
