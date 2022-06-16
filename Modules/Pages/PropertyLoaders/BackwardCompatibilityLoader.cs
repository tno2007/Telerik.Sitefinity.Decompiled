// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyLoaders.BackwardCompatibilityLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Pages.PropertyLoaders
{
  internal class BackwardCompatibilityLoader : PropertyLoader
  {
    internal BackwardCompatibilityLoader(ObjectData source)
      : base(source)
    {
    }

    internal override IEnumerable<ControlProperty> GetProperties(
      CultureInfo currentLanguage = null,
      bool fallbackToAnyLanguage = false)
    {
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (currentLanguage == null)
          currentLanguage = SystemManager.CurrentContext.Culture;
      }
      else
        currentLanguage = (CultureInfo) null;
      currentLanguage = ControlHelper.NormalizeLanguage(currentLanguage);
      IEnumerable<ControlProperty> source;
      if (!this.Source.IsTranslatable)
      {
        source = (IEnumerable<ControlProperty>) this.Source.Properties;
      }
      else
      {
        IEnumerable<ControlProperty> propertiesRaw = this.Source.GetPropertiesRaw(currentLanguage);
        if (DataExtensions.AppSettings.ContextSettings.Multilingual && this.Source.Properties.Count > 0 && propertiesRaw.Count<ControlProperty>() == 0)
        {
          if (currentLanguage != null)
          {
            CultureInfo language = this.Source.IsBackendObject ? DataExtensions.AppSettings.ContextSettings.DefaultBackendLanguage : DataExtensions.AppSettings.ContextSettings.DefaultFrontendLanguage;
            if (currentLanguage.Equals((object) language))
            {
              if (this.Source.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Language != null && p.Language != "")).Count<ControlProperty>() == 0)
                propertiesRaw = this.Source.GetPropertiesRaw((string) null);
            }
            else if (fallbackToAnyLanguage)
              propertiesRaw = this.Source.GetPropertiesRaw(language);
          }
          if (fallbackToAnyLanguage && propertiesRaw.Count<ControlProperty>() == 0)
          {
            string languageName = this.Source.Properties.First<ControlProperty>().Language;
            if (string.IsNullOrEmpty(languageName))
              languageName = (string) null;
            propertiesRaw = this.Source.GetPropertiesRaw(languageName);
          }
        }
        source = this.EnsureRequiredProperties(this.Source, propertiesRaw, currentLanguage);
      }
      List<ControlProperty> list = source.ToList<ControlProperty>();
      this.EnsureRequiredIdPropertyIsSet(currentLanguage, (IList<ControlProperty>) list);
      return (IEnumerable<ControlProperty>) list;
    }

    private IEnumerable<ControlProperty> EnsureRequiredProperties(
      ObjectData source,
      IEnumerable<ControlProperty> properties,
      CultureInfo language)
    {
      IEnumerable<ControlProperty> controlProperties = properties;
      if (properties.Count<ControlProperty>() == 0 && source.Properties.Count > 0)
      {
        List<ControlProperty> controlPropertyList = new List<ControlProperty>();
        string requiredPropertyName = "ID";
        ControlProperty sourceProperty = source.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == requiredPropertyName)).FirstOrDefault<ControlProperty>();
        if (sourceProperty != null)
        {
          ControlProperty controlProperty = source.CloneProperty(sourceProperty);
          controlProperty.Language = language == null ? (string) null : language.Name;
          controlPropertyList.Add(controlProperty);
        }
        controlProperties = (IEnumerable<ControlProperty>) controlPropertyList;
      }
      return controlProperties;
    }

    private void EnsureRequiredIdPropertyIsSet(
      CultureInfo currentLanguage,
      IList<ControlProperty> properties)
    {
      if (currentLanguage == null || properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name.Equals("ID"))))
        return;
      ControlProperty controlProperty = this.Source.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name.Equals("ID") && p.Language == null));
      if (controlProperty == null)
        return;
      properties.Add(controlProperty);
    }
  }
}
