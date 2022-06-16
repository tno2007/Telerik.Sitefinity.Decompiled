// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Generators.WebFormsAndMvcGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Editor.Specifics;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Renderer.Generators
{
  internal class WebFormsAndMvcGenerator : GeneratorBase
  {
    private IAdaptorFactory adaptorFactory;

    public WebFormsAndMvcGenerator(IAdaptorFactory adaptorFactory) => this.adaptorFactory = adaptorFactory;

    public override PageDto Generate(IGeneratorArgs args) => new PageDto()
    {
      ComponentContext = this.GetComponentContext(args.ContainerResolver.GetContainer(args.IsEdit, args.Version), (string) null)
    };

    public override ComponentDto GenerateHierarchicalWidgetModel(IGetComponentArgs args) => throw new NotImplementedException();

    public override ComponentDto[] GenerateLazyComponents(IGeneratorArgs args)
    {
      SystemManager.CurrentHttpContext.RewritePath("~/" + args.OriginalPathAndQuery.TrimStart('/'));
      PageSiteNode siteNode = (args.ContainerResolver as PageDataContainerResolver).SiteNode;
      ILookup<Guid, Guid> widgetSegmentIds1 = siteNode.CurrentPageDataItem.WidgetSegmentIds;
      ILookup<Guid, Guid> segmentFromTemplateIds = siteNode.CurrentPageDataItem.WidgetSegmentFromTemplateIds;
      IDictionary<Guid, Guid> widgetSegmentIds = SystemManager.GetPersonalizationService().GetWidgetSegmentIds(widgetSegmentIds1, segmentFromTemplateIds, (IEnumerable<Guid>) null);
      PageManager manager = PageManager.GetManager(siteNode.PageProviderName);
      using (new ElevatedModeRegion((IManager) manager))
      {
        ICollection<Guid> personalizedControlIds = widgetSegmentIds.Keys;
        return manager.GetControls<Telerik.Sitefinity.Pages.Model.ControlData>().Where<Telerik.Sitefinity.Pages.Model.ControlData>((Expression<Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>>) (x => personalizedControlIds.Contains(x.PersonalizationMasterId) && widgetSegmentIds.Values.Contains(x.PersonalizationSegmentId))).ToList<Telerik.Sitefinity.Pages.Model.ControlData>().Select<Telerik.Sitefinity.Pages.Model.ControlData, ComponentDto>((Func<Telerik.Sitefinity.Pages.Model.ControlData, ComponentDto>) (x =>
        {
          ComponentDto componentDto = this.GetComponentDto((ComponentDto) null, x, (string) null);
          componentDto.Id = x.PersonalizationMasterId;
          return componentDto;
        })).ToArray<ComponentDto>();
      }
    }

    protected override ComponentDto GetComponent(
      Telerik.Sitefinity.Pages.Model.ControlData control,
      string pageId,
      bool preserveDynamicLinks)
    {
      ComponentDto component = new ComponentDto()
      {
        Id = control.Id,
        Lazy = control.IsPersonalized
      };
      IPropertiesAdaptor propertiesAdaptor1 = this.adaptorFactory.Create(control);
      if (propertiesAdaptor1 != null)
      {
        CollectionContext<WcfControlProperty> propertiesInternal = new ControlPropertyService().GetPropertiesInternal(control.Id.ToString(), (string) null, (string) null, (string) null, (string) null, (string) null, pageId);
        IPropertiesAdaptor propertiesAdaptor2 = propertiesAdaptor1;
        IEnumerable<WcfControlProperty> items = propertiesInternal.Items;
        foreach (PropertyValueContainer propertyValueContainer in (IEnumerable<PropertyValueContainer>) propertiesAdaptor2.AdaptValuesForSerialization(items, (IAdaptValuesContext) new AdaptValuesContext()
        {
          PreserveDynamicLinks = preserveDynamicLinks
        }))
        {
          if (!component.PropertiesModel.Properties.ContainsKey(propertyValueContainer.Name))
            component.AddProperty(propertyValueContainer.Name, (object) propertyValueContainer.Value);
        }
        AdaptControlArgs args = new AdaptControlArgs()
        {
          WcfProperties = propertiesInternal.Items,
          ComponentType = ControlUtilities.BehaviorResolver.GetBehaviorObjectType(control),
          ControlData = control
        };
        ControlMetadata controlMetadata = propertiesAdaptor1.AdaptControlMetadata((IAdaptControlArgs) args);
        component.Name = controlMetadata.Name;
        component.Caption = controlMetadata.Caption;
        component.ViewName = controlMetadata.ViewName;
        if (controlMetadata is ILayoutControlMetadata layoutControlMetadata)
          component.PlaceHolderMap = layoutControlMetadata.PlaceHolderMap;
      }
      else
      {
        component.Name = "NotSupported";
        component.Caption = "NotSupported";
      }
      return component;
    }

    protected override void SetComponent(
      Telerik.Sitefinity.Pages.Model.ControlData controlData,
      PropertyValueGroupContainer propertyValueGroup,
      string pageId,
      bool clearPersistedProperties = false)
    {
      ControlPropertyService controlPropertyService = new ControlPropertyService();
      WcfPropertyManager propertyManager = new WcfPropertyManager()
      {
        ResolveDynamicLinks = false
      };
      CollectionContext<WcfControlProperty> properties = controlPropertyService.GetProperties(propertyValueGroup.ComponentId, (string) null, (string) null, (string) null, (string) null, (string) null, pageId.ToString(), propertyManager);
      WcfControlProperty[] array = ComponentAdaptorBase.Create(controlData).AdaptValuesForPersistence(propertyValueGroup.Properties, properties.Items).ToArray<WcfControlProperty>();
      PropertyLocalizationType result;
      System.Enum.TryParse<PropertyLocalizationType>(propertyValueGroup.PropertyLocalizationMode, out result);
      try
      {
        controlPropertyService.SaveProperties(false, array, propertyValueGroup.ComponentId, (string) null, pageId.ToString(), "Page", propertyLocalization: result);
      }
      catch (ContentLockedException ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "This page has been unlocked by another user and your changes cannot be saved.", (Exception) null);
      }
    }

    private ComponentContext GetComponentContext(
      IControlsContainer controlsContainer,
      string pageId)
    {
      IList<IControlsContainer> containers = new ContainerIterator().GetContainers(controlsContainer);
      List<string> list1 = containers.SelectMany<IControlsContainer, Telerik.Sitefinity.Pages.Model.ControlData>((Func<IControlsContainer, IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>>) (x => x.Controls)).Where<Telerik.Sitefinity.Pages.Model.ControlData>((Func<Telerik.Sitefinity.Pages.Model.ControlData, bool>) (x => x.PlaceHolder != null && !Regex.IsMatch(x.PlaceHolder, ".+_Col\\d\\d"))).Select<Telerik.Sitefinity.Pages.Model.ControlData, string>((Func<Telerik.Sitefinity.Pages.Model.ControlData, string>) (x => x.PlaceHolder)).Distinct<string>().ToList<string>();
      List<ControlNode> controlsHierarchical = new PageHelperImplementation().GetControlsHierarchical(containers.Reverse<IControlsContainer>(), list1);
      if (controlsContainer is PageData page)
        PageHelper.ProcessOverridenControls(page, (IEnumerable<IControlsContainer>) containers);
      ComponentContext context = new ComponentContext();
      if (controlsHierarchical.Count > 0)
      {
        Dictionary<string, string> dictionary = list1.ToDictionary<string, string, string>((Func<string, string>) (x => x), (Func<string, string>) (y => y));
        List<ControlNode> list2 = controlsHierarchical.SelectMany<ControlNode, ControlNode>((Func<ControlNode, IEnumerable<ControlNode>>) (x => (IEnumerable<ControlNode>) x.Children)).ToList<ControlNode>();
        ComponentDto parent = new ComponentDto()
        {
          PlaceHolderMap = (IDictionary<string, string>) dictionary
        };
        WebFormsAndMvcGenerator.BuildContext context1 = new WebFormsAndMvcGenerator.BuildContext()
        {
          OnComponentCreating = (Action<ComponentDto>) (component =>
          {
            if (!component.Lazy)
              return;
            context.HasLazyComponents = true;
          })
        };
        context.Components = this.GetComponents(parent, (IList<ControlNode>) list2, context1, pageId).ToArray<ComponentDto>();
      }
      return context;
    }

    private IList<ComponentDto> GetComponents(
      ComponentDto parent,
      IList<ControlNode> controls,
      WebFormsAndMvcGenerator.BuildContext context,
      string pageId)
    {
      List<ComponentDto> components1 = new List<ComponentDto>();
      foreach (ControlNode control in (IEnumerable<ControlNode>) controls)
      {
        ComponentDto componentDto = this.GetComponentDto(parent, control.Control, pageId);
        if (context != null && context.OnComponentCreating != null)
          context.OnComponentCreating(componentDto);
        IList<ComponentDto> components2 = this.GetComponents(componentDto, (IList<ControlNode>) control.Children, context, pageId);
        componentDto.Children = components2;
        components1.Add(componentDto);
      }
      return (IList<ComponentDto>) components1;
    }

    private ComponentDto GetComponentDto(
      ComponentDto parent,
      Telerik.Sitefinity.Pages.Model.ControlData control,
      string pageId)
    {
      ComponentDto component = this.GetComponent(control, pageId, false);
      if (parent != null && parent.PlaceHolderMap != null)
      {
        string str = (string) null;
        if (parent.PlaceHolderMap.TryGetValue(control.PlaceHolder, out str))
          component.PlaceHolder = str;
      }
      return component;
    }

    private class BuildContext
    {
      public Action<ComponentDto> OnComponentCreating { get; set; }
    }
  }
}
