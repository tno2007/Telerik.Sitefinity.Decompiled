// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Generators.AgnosticGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Pages.Model.PropertyLoaders;
using Telerik.Sitefinity.Renderer.Editor;
using Telerik.Sitefinity.Renderer.Editor.Specifics;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Renderer.Generators
{
  internal class AgnosticGenerator : GeneratorBase
  {
    public override PageDto Generate(IGeneratorArgs args)
    {
      PageDto pageDto = new PageDto();
      IControlsContainer container = args.ContainerResolver.GetContainer(args.IsEdit, args.Version);
      IList<IControlsContainer> containers = new ContainerIterator().GetContainers(container);
      if (containers.Last<IControlsContainer>() is IRendererCommonData rendererCommonData)
        pageDto.TemplateName = rendererCommonData.TemplateName;
      if (!string.IsNullOrEmpty(args.Version))
      {
        containers.RemoveAt(0);
        containers.Insert(0, container);
      }
      List<ControlData> list = containers.SelectMany<IControlsContainer, ControlData>((Func<IControlsContainer, IEnumerable<ControlData>>) (x => x.Controls)).ToList<ControlData>();
      pageDto.ComponentContext = this.GetComponentContext((IList<ControlData>) list, (string) null);
      return pageDto;
    }

    public override ComponentDto GenerateHierarchicalWidgetModel(IGetComponentArgs args)
    {
      IControlsContainer container = args.ContainerResolver.GetContainer(args.IsEdit, args.Version);
      Guid guid = Guid.Parse(args.ComponentId);
      Dictionary<Guid, ComponentDto> componentsMap = new Dictionary<Guid, ComponentDto>()
      {
        {
          guid,
          this.GetComponent(args)
        }
      };
      List<ControlData> list = container.Controls.ToList<ControlData>();
      this.AddControlsRecuresively((IDictionary<Guid, ComponentDto>) componentsMap, (IList<ControlData>) list, guid);
      return componentsMap[guid];
    }

    public override ComponentDto[] GenerateLazyComponents(IGeneratorArgs args) => throw new NotImplementedException();

    private ComponentContext GetComponentContext(
      IList<ControlData> allControls,
      string pageId)
    {
      Dictionary<Guid, ComponentDto> componentsMap = new Dictionary<Guid, ComponentDto>()
      {
        {
          Guid.Empty,
          new ComponentDto()
        }
      };
      this.AddControlsRecuresively((IDictionary<Guid, ComponentDto>) componentsMap, allControls, Guid.Empty);
      List<ComponentDto> componentDtoList = new List<ComponentDto>();
      if (allControls.Count != componentsMap.Count - 1)
      {
        foreach (Guid guid in allControls.Select<ControlData, Guid>((Func<ControlData, Guid>) (x => x.Id)).Except<Guid>((IEnumerable<Guid>) componentsMap.Keys))
        {
          Guid key = guid;
          ControlData control = allControls.FirstOrDefault<ControlData>((Func<ControlData, bool>) (x => x.Id == key));
          componentDtoList.Add(this.GetComponent(control, (string) null, false));
        }
      }
      ComponentContext componentContext = new ComponentContext();
      componentContext.Components = componentsMap[Guid.Empty].Children.ToArray<ComponentDto>();
      componentContext.OrphanedControls = componentDtoList.ToArray();
      return componentContext;
    }

    private void AddControlsRecuresively(
      IDictionary<Guid, ComponentDto> componentsMap,
      IList<ControlData> controls,
      Guid parentId)
    {
      if (controls.Count == 0)
        return;
      foreach (IEnumerable<ControlData> source in controls.Where<ControlData>((Func<ControlData, bool>) (x => x.ParentId == parentId)).GroupBy<ControlData, string>((Func<ControlData, string>) (x => x.PlaceHolder)))
      {
        foreach (ControlData control in (IEnumerable<ControlData>) this.OrderControlsInPlaceholder(source))
        {
          ComponentDto component = this.GetComponent(control, (string) null, false);
          componentsMap[parentId].Children.Add(component);
          componentsMap[control.Id] = component;
          List<ControlData> list = controls.Where<ControlData>((Func<ControlData, bool>) (x => x.ParentId != parentId)).ToList<ControlData>();
          this.AddControlsRecuresively(componentsMap, (IList<ControlData>) list, control.Id);
        }
      }
    }

    private IList<ControlData> OrderControlsInPlaceholder(
      IEnumerable<ControlData> source)
    {
      List<ControlData> controlDataList1 = new List<ControlData>();
      Dictionary<Guid, List<ControlData>> dictionary1 = source.GroupBy<ControlData, Guid>((Func<ControlData, Guid>) (x => x.SiblingId)).ToDictionary<IGrouping<Guid, ControlData>, Guid, List<ControlData>>((Func<IGrouping<Guid, ControlData>, Guid>) (x => x.Key), (Func<IGrouping<Guid, ControlData>, List<ControlData>>) (y => y.ToList<ControlData>()));
      Guid[] guidArray = new Guid[1]{ Guid.Empty };
      List<Guid> guidList = new List<Guid>();
      Dictionary<Guid, int> dictionary2 = new Dictionary<Guid, int>();
      while ((guidArray.Length != 0 || dictionary1.Count <= 0) && dictionary1.Count != 0)
      {
        foreach (Guid guid in guidArray)
        {
          Guid siblingId = guid;
          List<ControlData> controlDataList2;
          if (dictionary1.TryGetValue(siblingId, out controlDataList2))
          {
            int index;
            if (dictionary2.TryGetValue(siblingId, out index) && controlDataList1.Count > 0 && controlDataList1[index].Id == siblingId)
            {
              foreach (ControlData controlData in controlDataList2)
              {
                controlDataList1.Insert(++index, controlData);
                guidList.Add(controlData.Id);
                dictionary2[controlData.Id] = index;
              }
            }
            else
            {
              if (siblingId != Guid.Empty)
              {
                index = controlDataList1.FindIndex((Predicate<ControlData>) (x => x.Id == siblingId));
                if (index != -1)
                  ++index;
              }
              if (index != -1)
              {
                foreach (ControlData controlData in controlDataList2)
                {
                  dictionary2[controlData.Id] = index;
                  controlDataList1.Insert(index++, controlData);
                  guidList.Add(controlData.Id);
                }
              }
            }
            dictionary1.Remove(siblingId);
          }
        }
        guidArray = guidList.ToArray();
        guidList.Clear();
      }
      return (IList<ControlData>) controlDataList1;
    }

    protected override ComponentDto GetComponent(
      ControlData control,
      string pageId,
      bool preserveDynamicLinks)
    {
      ComponentDto component = new ComponentDto()
      {
        Id = control.Id,
        Name = control.ObjectType,
        Caption = control.Caption,
        PlaceHolder = control.PlaceHolder,
        SiblingId = control.SiblingId
      };
      foreach (ControlProperty property in control.GetProperties(SystemManager.CurrentContext.Culture))
      {
        this.ResolveLinks(property);
        component.AddProperty(property.Name, (object) property.Value);
      }
      return component;
    }

    protected override void SetComponent(
      ControlData controlData,
      PropertyValueGroupContainer propertiesGroup,
      string pageId,
      bool cleanPersistedProperties)
    {
      if (propertiesGroup.Properties == null)
        return;
      PropertyLocalizationType result;
      Enum.TryParse<PropertyLocalizationType>(propertiesGroup.PropertyLocalizationMode, out result);
      if (propertiesGroup.Caption != null)
        controlData.Caption = propertiesGroup.Caption;
      if (result == PropertyLocalizationType.AllTranslations)
        controlData.Strategy = PropertyPersistenceStrategy.NotTranslatable;
      else
        controlData.SetPersistanceStrategy();
      string persistenceLanguage = this.ResolvePersistenceLanguage((ObjectData) controlData);
      PageManager manager = PageManager.GetManager();
      if (cleanPersistedProperties)
      {
        string currentLanguage = SystemManager.CurrentContext.Culture.Name;
        foreach (ControlProperty controlProperty in result != PropertyLocalizationType.AllTranslations ? (IEnumerable<ControlProperty>) controlData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == currentLanguage || x.Language == null)).ToList<ControlProperty>() : (IEnumerable<ControlProperty>) controlData.Properties.ToList<ControlProperty>())
          manager.Delete(controlProperty);
      }
      if (result == PropertyLocalizationType.AllTranslations)
      {
        string currentLanguage = SystemManager.CurrentContext.Culture.Name;
        foreach (ControlProperty controlProperty in controlData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language != currentLanguage && x.Language != null)).ToList<ControlProperty>())
          manager.Delete(controlProperty);
        List<ControlProperty> list1 = controlData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == currentLanguage && x.Language != null)).ToList<ControlProperty>();
        List<ControlProperty> list2 = controlData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == null)).ToList<ControlProperty>();
        foreach (ControlProperty controlProperty1 in list1)
        {
          ControlProperty prop = controlProperty1;
          ControlProperty controlProperty2 = list2.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == prop.Name));
          if (controlProperty2 != null)
            manager.Delete(controlProperty2);
          prop.Language = (string) null;
        }
        persistenceLanguage = (string) null;
      }
      foreach (PropertyValueContainer propertyValueContainer in propertiesGroup.Properties.ToList<PropertyValueContainer>())
      {
        PropertyValueContainer prop = propertyValueContainer;
        ControlProperty controlProperty3 = controlData.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == prop.Name && p.Language == persistenceLanguage));
        PropertyContainer propertyContainer = propertiesGroup.PropertyMetadata != null ? propertiesGroup.PropertyMetadata.FirstOrDefault<PropertyContainer>((Func<PropertyContainer, bool>) (m => m.Name == prop.Name)) : (PropertyContainer) null;
        if (controlData.Strategy == PropertyPersistenceStrategy.Translatable)
        {
          ControlProperty controlProperty4 = controlData.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == prop.Name && p.Language == null));
          if (controlProperty4 != null && controlProperty4.Value == prop.Value)
          {
            if (controlProperty3 != null)
            {
              controlData.Properties.Remove(controlProperty3);
              continue;
            }
            continue;
          }
          if (propertyContainer != null && propertyContainer.DefaultValue == prop.Value && controlProperty4 == null)
            continue;
        }
        if (controlData.Strategy == PropertyPersistenceStrategy.NotTranslatable && propertyContainer != null && propertyContainer.DefaultValue == prop.Value)
        {
          if (controlProperty3 != null)
            controlData.Properties.Remove(controlProperty3);
        }
        else
        {
          if (controlProperty3 == null)
          {
            controlProperty3 = manager.CreateProperty();
            controlProperty3.Name = prop.Name;
            controlProperty3.Language = persistenceLanguage;
            controlProperty3.SetFlag(1);
            controlData.Properties.Add(controlProperty3);
          }
          if (propertyContainer != null && (propertyContainer.Type == "linkInsert" || propertyContainer.Properties.Properties.ContainsKey(WidgetMetadataConstants.ContainsLinks)))
          {
            controlProperty3.SetFlag(16);
            if (prop.Value != null)
              prop.Value = LinkParser.UnresolveContentLinks(prop.Value);
          }
          controlProperty3.Value = prop.Value;
        }
      }
      manager.SaveChanges();
    }

    private string ResolvePersistenceLanguage(ObjectData objectData) => objectData.Strategy == PropertyPersistenceStrategy.NotTranslatable ? (string) null : SystemManager.CurrentContext.Culture.Name;

    private void ResolveLinks(ControlProperty prop)
    {
      if (prop.Value == null || !prop.HasDynamicLinks())
        return;
      LinkDto linkDto = (LinkDto) null;
      try
      {
        linkDto = JsonConvert.DeserializeObject<LinkDto>(prop.Value);
      }
      catch (Exception ex)
      {
      }
      if (linkDto != null && linkDto.Sfref != null)
      {
        HtmlParser htmlParser = new HtmlParser(LinkParser.ResolveLinks(string.Format("<a href=\"{0}\"></a>", (object) linkDto.Sfref), new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, true, true));
        htmlParser.SetChunkHashMode(false);
        HtmlChunk next;
        while ((next = htmlParser.ParseNext()) != null)
        {
          if (next.Type == HtmlChunkType.OpenTag && next.HasAttribute("href"))
          {
            linkDto.Href = next.GetParamValue("href");
            break;
          }
        }
        prop.Value = JsonConvert.SerializeObject((object) linkDto);
      }
      else
      {
        if (linkDto != null)
          return;
        prop.Value = LinkParser.ResolveLinks(prop.Value, new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, true, true);
      }
    }
  }
}
