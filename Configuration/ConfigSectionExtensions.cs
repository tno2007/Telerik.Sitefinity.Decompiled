// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigSectionExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration.Basic;
using Telerik.Sitefinity.Configuration.Web;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Configuration
{
  internal static class ConfigSectionExtensions
  {
    private const string ItemNameFormat = "Item {0}";

    internal static IEnumerable<SettingModel> GetSettingsMetadataFromSection(
      this ConfigSection section,
      bool skipLinkedItems = true)
    {
      ConfigSection configSection = Config.GetConfigSection(section.TagName);
      string printableName = ConfigUtils.GetPrintableName((object) configSection);
      SettingModel parent = new SettingModel()
      {
        Key = configSection.TagName,
        Title = printableName,
        NodeType = SettingNodeType.Navigation,
        SectionName = configSection.TagName,
        Parent = (SettingModel) null
      };
      List<SettingModel> settings = new List<SettingModel>()
      {
        parent
      };
      ConfigSectionExtensions.FillSettingsMetadata(configSection.GetType(), (object) configSection, parent, (ICollection<SettingModel>) settings, skipLinkedItems);
      return (IEnumerable<SettingModel>) settings;
    }

    private static void FillSettingsMetadata(
      Type targetType,
      object target,
      SettingModel parent,
      ICollection<SettingModel> settings,
      bool skipLinkedItems)
    {
      foreach (PropertyDescriptor property1 in TypeDescriptor.GetProperties(targetType))
      {
        if ((!skipLinkedItems || !typeof (IConfigElementLink).IsAssignableFrom(property1.PropertyType)) && property1.Attributes[typeof (ConfigurationPropertyAttribute)] is ConfigurationPropertyAttribute && ConfigUtils.ValidateProperty(property1, targetType))
        {
          if (typeof (ConfigElement).IsAssignableFrom(property1.PropertyType) || typeof (NameValueCollection).IsAssignableFrom(property1.PropertyType))
          {
            SettingModel setting = ConfigSectionExtensions.GetSetting(property1, property1.Name, parent, SettingNodeType.Navigation);
            settings.Add(setting);
            PropertyInfo property2 = targetType.GetProperty(property1.Name);
            if (property2 != (PropertyInfo) null)
            {
              object target1 = property2.GetValue(target, (object[]) null);
              switch (target1)
              {
                case ConfigElementCollection elementCollection:
                  if (elementCollection.Count > 0)
                  {
                    string name = elementCollection.ElementType.Name;
                    using (IEnumerator<IConfigElementItem> enumerator = elementCollection.Items.GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                      {
                        IConfigElementItem current = enumerator.Current;
                        if (current != null && (!skipLinkedItems || !(current is IConfigElementLink)))
                        {
                          ConfigElement element = current.Element;
                          if (element != null)
                            ConfigSectionExtensions.ProcessCollectionItem(setting, name, element, settings, skipLinkedItems);
                        }
                      }
                      continue;
                    }
                  }
                  else
                    continue;
                case NameValueCollection nameValueCollection:
                  IEnumerator enumerator1 = nameValueCollection.Keys.GetEnumerator();
                  try
                  {
                    while (enumerator1.MoveNext())
                    {
                      string current = (string) enumerator1.Current;
                      SettingModel settingModel = new SettingModel()
                      {
                        Title = current,
                        Key = nameValueCollection.GetType().Name,
                        NodeType = SettingNodeType.Form,
                        Parent = parent,
                        SectionName = parent.SectionName,
                        CollectionKey = current
                      };
                      settings.Add(settingModel);
                    }
                    continue;
                  }
                  finally
                  {
                    if (enumerator1 is IDisposable disposable)
                      disposable.Dispose();
                  }
                case null:
                  continue;
                default:
                  ConfigSectionExtensions.FillSettingsMetadata(property1.PropertyType, target1, setting, settings, skipLinkedItems);
                  continue;
              }
            }
          }
          else
          {
            SettingModel setting = ConfigSectionExtensions.GetSetting(property1, property1.Name, parent, SettingNodeType.Form);
            settings.Add(setting);
          }
        }
      }
    }

    private static SettingModel GetSetting(
      PropertyDescriptor prop,
      string key,
      SettingModel parent,
      SettingNodeType nodeType)
    {
      SettingModel configItem = new SettingModel();
      configItem.Key = key;
      configItem.NodeType = nodeType;
      configItem.SectionName = parent.SectionName;
      configItem.Parent = parent;
      ConfigSectionExtensions.FillTitleAndDescription(configItem, prop.Attributes, prop);
      return configItem;
    }

    private static void FillTitleAndDescription(
      SettingModel configItem,
      AttributeCollection attributes,
      PropertyDescriptor prop)
    {
      ObjectInfoAttribute attribute1 = (ObjectInfoAttribute) attributes[typeof (ObjectInfoAttribute)];
      if (attribute1 != null)
      {
        configItem.Title = attribute1.Title;
        configItem.Description = attribute1.Description;
      }
      else
      {
        DescriptionResourceAttribute attribute2 = (DescriptionResourceAttribute) attributes[typeof (DescriptionResourceAttribute)];
        if (attribute2 != null)
        {
          configItem.Title = ConfigUtils.GetPrintableName(prop.Name);
          configItem.Description = attribute2.Description;
        }
        else
          configItem.Title = ConfigUtils.GetPrintableName(prop.Name);
      }
    }

    private static void ProcessCollectionItem(
      SettingModel collectionData,
      string key,
      ConfigElement element,
      ICollection<SettingModel> settings,
      bool skipLinkedItems)
    {
      string str = new ConfigSectionItems.DefaultProperty("Item {0}").GetValue((object) element);
      SettingModel parent = new SettingModel()
      {
        Title = str,
        Key = key,
        NodeType = SettingNodeType.Navigation,
        Parent = collectionData,
        SectionName = collectionData.SectionName,
        CollectionKey = element.GetKey()
      };
      ObjectInfoAttribute attribute = (ObjectInfoAttribute) TypeDescriptor.GetAttributes(element.GetType())[typeof (ObjectInfoAttribute)];
      if (attribute != null)
        parent.Description = attribute.Description;
      settings.Add(parent);
      ConfigSectionExtensions.FillSettingsMetadata(element.GetType(), (object) element, parent, settings, skipLinkedItems);
    }
  }
}
