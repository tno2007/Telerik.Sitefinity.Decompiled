// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.ComponentAdaptorBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using Progress.Sitefinity.Renderer.Designers.Dto;
using Progress.Sitefinity.Renderer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors
{
  internal class ComponentAdaptorBase : IPropertiesAdaptor
  {
    protected const string BasicFilter = "Basic";
    protected const string AdvancedFilter = "Advanced";
    internal const string DefaultBasicSectionName = "Main";
    internal const string DefaultAdvancedSectionName = "AdvancedMain";
    private const string ControllerSuffix = "controller";

    public static IPropertiesAdaptor Create(ControlData controlData) => ObjectFactory.Container.ResolveAll(typeof (IPropertiesAdaptor)).Cast<IPropertiesAdaptor>().Where<IPropertiesAdaptor>((Func<IPropertiesAdaptor, bool>) (x => x.CanAdaptComponent(controlData))).OrderByDescending<IPropertiesAdaptor, int>((Func<IPropertiesAdaptor, int>) (x => x.Priority)).FirstOrDefault<IPropertiesAdaptor>();

    public static bool SupportsAdminAppEditor(ControlData controlData) => Config.Get<PagesConfig>().EnableAdminAppWidgetEditors && ObjectFactory.Container.ResolveAll(typeof (IPropertiesAdaptor)).Cast<IPropertiesAdaptor>().Any<IPropertiesAdaptor>((Func<IPropertiesAdaptor, bool>) (x => x.CanAdaptComponent(controlData)));

    public virtual IEnumerable<WcfControlProperty> AdaptValuesForPersistence(
      IEnumerable<PropertyValueContainer> properties,
      IEnumerable<WcfControlProperty> sourceProperties)
    {
      List<WcfControlProperty> wcfControlPropertyList = new List<WcfControlProperty>();
      foreach (PropertyValueContainer property1 in properties)
      {
        PropertyValueContainer property = property1;
        WcfControlProperty wcfProp = sourceProperties.FirstOrDefault<WcfControlProperty>((Func<WcfControlProperty, bool>) (x => x.OriginalPropertyName == property.Name));
        if (wcfProp.NeedsEditor)
        {
          string propertyPath = wcfProp.PropertyPath;
          if (property.Value != null)
          {
            object obj1 = !(wcfProp.ItemTypeName == typeof (LinkModel).FullName) ? JsonConvert.DeserializeObject<object>(property.Value) : (object) JsonConvert.DeserializeObject<LinkModel>(property.Value);
            foreach (WcfControlProperty wcfProp1 in sourceProperties.Where<WcfControlProperty>((Func<WcfControlProperty, bool>) (x => x.PropertyPath.StartsWith(wcfProp.PropertyPath + "/") && x.PropertyPath != wcfProp.PropertyPath)))
            {
              object obj2 = (object) null;
              if (wcfProp.ItemTypeName == typeof (LinkModel).FullName)
              {
                System.Reflection.PropertyInfo property2 = typeof (LinkModel).GetProperty(wcfProp1.OriginalPropertyName);
                if (property2 != (System.Reflection.PropertyInfo) null)
                {
                  // ISSUE: reference to a compiler-generated field
                  if (ComponentAdaptorBase.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    ComponentAdaptorBase.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, System.Reflection.PropertyInfo, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetValue", (IEnumerable<Type>) null, typeof (ComponentAdaptorBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                    {
                      CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                      CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                    }));
                  }
                  // ISSUE: reference to a compiler-generated field
                  // ISSUE: reference to a compiler-generated field
                  obj2 = ComponentAdaptorBase.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) ComponentAdaptorBase.\u003C\u003Eo__4.\u003C\u003Ep__0, property2, obj1);
                }
              }
              else
              {
                // ISSUE: reference to a compiler-generated field
                if (ComponentAdaptorBase.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  ComponentAdaptorBase.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetIndex(CSharpBinderFlags.None, typeof (ComponentAdaptorBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                obj2 = ComponentAdaptorBase.\u003C\u003Eo__4.\u003C\u003Ep__1.Target((CallSite) ComponentAdaptorBase.\u003C\u003Eo__4.\u003C\u003Ep__1, obj1, wcfProp1.OriginalPropertyName);
              }
              wcfProp1.PropertyValue = obj2 != null ? ComponentAdaptorBase.UnresolveDynamicLinkValue(obj2.ToString(), wcfProp1) : (string) null;
              wcfControlPropertyList.Add(wcfProp1);
            }
          }
        }
        else
        {
          wcfProp.PropertyValue = ComponentAdaptorBase.UnresolveDynamicLinkValue(property.Value, wcfProp);
          wcfControlPropertyList.Add(wcfProp);
        }
      }
      return (IEnumerable<WcfControlProperty>) wcfControlPropertyList;
    }

    public virtual IList<PropertyValueContainer> AdaptValuesForSerialization(
      IEnumerable<WcfControlProperty> wcfProperties,
      IAdaptValuesContext context)
    {
      List<PropertyValueContainer> propertyValueContainerList = new List<PropertyValueContainer>();
      foreach (WcfControlProperty wcfProperty in wcfProperties)
      {
        WcfControlProperty prop = wcfProperty;
        if (!(prop.PropertyName == "Settings"))
        {
          List<string> list = ((IEnumerable<string>) prop.PropertyPath.Split(new char[1]
          {
            '/'
          }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
          if (list.Count > 0)
          {
            if (list[0] == "Settings")
              list.RemoveAt(0);
            string str;
            if (prop.NeedsEditor)
            {
              Dictionary<string, object> dictionary = new Dictionary<string, object>();
              foreach (WcfControlProperty wcfControlProperty in wcfProperties.Where<WcfControlProperty>((Func<WcfControlProperty, bool>) (x => x.PropertyPath.StartsWith(prop.PropertyPath + "/") && x.PropertyPath != prop.PropertyPath && x.PropertyPath.Count<char>((Func<char, bool>) (ch => ch == '/')) == prop.PropertyPath.Count<char>((Func<char, bool>) (ch => ch == '/')) + 1)))
              {
                string key = prop.ItemTypeName == typeof (LinkModel).FullName ? wcfControlProperty.OriginalPropertyName.ToLowerInvariant() : wcfControlProperty.OriginalPropertyName;
                dictionary.Add(key, (object) wcfControlProperty.PropertyValue);
              }
              str = JsonConvert.SerializeObject((object) dictionary);
            }
            else
              str = this.GetPropertyValue(prop, context.PreserveDynamicLinks);
            PropertyValueContainer propertyValueContainer = new PropertyValueContainer()
            {
              Name = prop.OriginalPropertyName,
              Value = str
            };
            propertyValueContainerList.Add(propertyValueContainer);
          }
        }
      }
      return (IList<PropertyValueContainer>) propertyValueContainerList;
    }

    public virtual bool CanAdaptComponent(ControlData component) => !ComponentAdaptorBase.HasCustomDesigners(component) || ComponentAdaptorBase.IsWhitelistedComponent(component);

    public virtual int Priority => 1;

    public virtual ControlMetadata AdaptControlMetadata(IAdaptControlArgs args)
    {
      List<SectionGroup> list1 = WidgetRendererResolver.GetPropertyConfigurator().GetPropertiesHierarchy(TypeResolutionService.ResolveType(args.ComponentType), (string) null).Select<CategoryGroupDto, SectionGroup>((Func<CategoryGroupDto, SectionGroup>) (sg => new SectionGroup(sg.Name, sg.Sections))).ToList<SectionGroup>();
      List<PropertyContainer> list2 = list1.SelectMany<SectionGroup, Section>((Func<SectionGroup, IEnumerable<Section>>) (x => x.Sections)).SelectMany<Section, PropertyContainer>((Func<Section, IEnumerable<PropertyContainer>>) (x => (IEnumerable<PropertyContainer>) x.Properties)).ToList<PropertyContainer>();
      return new ControlMetadata()
      {
        Name = args.ComponentType,
        Caption = this.ResolveCaption(args.ComponentType, args.ControlData),
        ViewName = (string) null,
        PropertyMetadata = (IList<SectionGroup>) list1,
        PropertyMetadataFlat = (IList<PropertyContainer>) list2
      };
    }

    private static string UnresolveDynamicLinkValue(string initialValue, WcfControlProperty wcfProp)
    {
      if (!wcfProp.SupportsDynamicLinks)
        return initialValue;
      IList<LinkMetadata> contentLinks = (IList<LinkMetadata>) new List<LinkMetadata>();
      return LinkParser.UnresolveContentLinks(initialValue, out contentLinks, true);
    }

    private static bool IsWhitelistedComponent(ControlData component)
    {
      PagesConfig pagesConfig = Config.Get<PagesConfig>();
      if (string.IsNullOrEmpty(pagesConfig.WhitelistedAdminAppWidgetEditors))
        return false;
      string componentName = ComponentAdaptorBase.GetComponentWhitelistName(component);
      ControlProperty widgetName = component.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "WidgetName"));
      return ((IEnumerable<string>) pagesConfig.WhitelistedAdminAppWidgetEditors.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (x => x.ToLowerInvariant().Trim())).Any<string>((Func<string, bool>) (x =>
      {
        if (x == componentName.ToLowerInvariant())
          return true;
        return widgetName != null && x == widgetName.Value.ToLowerInvariant();
      }));
    }

    private static bool HasCustomDesigners(ControlData controlData)
    {
      if (controlData.ObjectType == "Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy")
      {
        string controlType = controlData.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == "ControllerName")).Value;
        if (ObjectFactory.IsTypeRegistered<IDesignerResolver>())
        {
          IDesignerResolver designerResolver = ObjectFactory.Resolve<IDesignerResolver>();
          if (controlType != null)
            return designerResolver.HasCustomDesigners(controlType);
        }
      }
      return true;
    }

    private static string GetComponentWhitelistName(ControlData component)
    {
      string componentWhitelistName = ControlUtilities.BehaviorResolver.GetBehaviorObjectType(component);
      if (componentWhitelistName != null)
        componentWhitelistName = componentWhitelistName.Substring(componentWhitelistName.LastIndexOf('.') + 1);
      return componentWhitelistName;
    }

    private void AddToSection(
      PropertyContainer propertyContainer,
      List<Section> groups,
      string sectionName,
      string categoryName)
    {
      Section section = groups.FirstOrDefault<Section>((Func<Section, bool>) (x => x.CategoryName == categoryName && x.Name == sectionName));
      if (section == null)
      {
        section = new Section(sectionName, sectionName, categoryName);
        if (sectionName == "Main" || sectionName == "AdvancedMain")
          groups.Insert(0, section);
        else
          groups.Add(section);
      }
      section.Properties.Add(propertyContainer);
    }

    private string GetPropertyValue(WcfControlProperty prop, bool preserveDynamicLinks)
    {
      if (string.IsNullOrEmpty(prop.PropertyValue))
        return (string) null;
      if (prop.PropertyValue == Guid.Empty.ToString())
        return (string) null;
      if (prop.PropertyValue == bool.TrueString || prop.PropertyValue == bool.FalseString)
        return prop.PropertyValue;
      if (prop.OriginalPropertyValue is DateTime)
        return ((DateTime) prop.OriginalPropertyValue).ToIsoFormat(CultureInfo.InvariantCulture);
      if (!prop.SupportsDynamicLinks)
        return prop.PropertyValue;
      bool flag = string.IsNullOrEmpty(SystemManager.CurrentHttpContext.Request.Headers.Get("X-SF-Integration-Mode"));
      return new DynamicLinksParser(preserveDynamicLinks, new bool?(flag)).Apply(prop.PropertyValue);
    }

    private string ResolveCaption(string objectType, ControlData controlData)
    {
      if (!string.IsNullOrEmpty(controlData.Caption))
        return controlData.Caption;
      Type type = TypeResolutionService.ResolveType(objectType, false);
      return type != (Type) null ? type.Name : objectType;
    }
  }
}
