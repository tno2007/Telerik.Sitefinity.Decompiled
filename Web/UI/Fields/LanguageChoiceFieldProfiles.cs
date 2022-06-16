// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceFieldProfiles
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  public class LanguageChoiceFieldProfiles : LanguageChoiceField
  {
    private CultureElement[] languages;

    protected override void BindLanguages()
    {
      foreach (CultureElement languageElement in this.GetLanguageElements())
      {
        CultureInfo cultureInfo = new CultureInfo(languageElement.UICulture);
        this.Choices.Add(new ChoiceItem()
        {
          Text = cultureInfo.NativeName,
          Value = languageElement.Key
        });
      }
    }

    protected override void ValueChanged()
    {
      if (this.DisplayMode != FieldDisplayMode.Read)
        return;
      string key = this.Value as string;
      if (string.IsNullOrEmpty(key))
      {
        this.ReadModeLabel.Text = Res.Get<Labels>().NotSelected;
      }
      else
      {
        ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
        ConfigElementDictionary<string, CultureElement> elementDictionary = (ConfigElementDictionary<string, CultureElement>) null;
        if (this.LanguageSource == LanguageSource.Backend)
          elementDictionary = resourcesConfig.BackendCultures;
        else if (this.LanguageSource == LanguageSource.Frontend)
          elementDictionary = resourcesConfig.Cultures;
        CultureElement cultureElement = (CultureElement) null;
        elementDictionary.TryGetValue(key, out cultureElement);
        if (cultureElement == null)
          return;
        this.ReadModeLabel.Text = new CultureInfo(cultureElement.UICulture).NativeName;
      }
    }

    protected virtual CultureElement[] GetLanguageElements()
    {
      if (this.languages == null)
      {
        List<CultureElement> cultureElementList = new List<CultureElement>();
        if (this.LanguageSource == LanguageSource.Custom)
          throw new NotImplementedException();
        List<CultureElement> collection = (List<CultureElement>) null;
        ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
        if (this.LanguageSource == LanguageSource.Backend)
          collection = new List<CultureElement>((IEnumerable<CultureElement>) resourcesConfig.BackendCultures.Values);
        else if (this.LanguageSource == LanguageSource.Frontend)
          collection = new List<CultureElement>((IEnumerable<CultureElement>) resourcesConfig.Cultures.Values);
        cultureElementList.AddRange((IEnumerable<CultureElement>) collection);
        this.languages = cultureElementList.ToArray();
      }
      return this.languages;
    }

    protected override string ScriptDescriptorTypeName => typeof (LanguageChoiceField).FullName;
  }
}
