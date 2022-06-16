// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyLoaders.TranslatableLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Pages.PropertyLoaders
{
  internal class TranslatableLoader : PropertyLoader
  {
    internal TranslatableLoader(ObjectData source)
      : base(source)
    {
    }

    internal override IEnumerable<ControlProperty> GetProperties(
      CultureInfo language = null,
      bool fallbackToAnyLanguage = false)
    {
      language = ControlHelper.NormalizeLanguage(language);
      if (language == null)
        language = SystemManager.CurrentContext.Culture;
      IEnumerable<string> languagePropsNames = this.Source.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == language.Name)).Select<ControlProperty, string>((Func<ControlProperty, string>) (x => x.Name));
      return this.Source.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Language == null && !languagePropsNames.Contains<string>(x.Name) || x.Language == language.Name));
    }

    internal override IEnumerable<ControlProperty> ClearProperties(
      object component = null,
      CultureInfo language = null)
    {
      List<ControlProperty> controlPropertyList = new List<ControlProperty>(base.ClearProperties(component, language));
      IEnumerable<PropertyDescriptor> source1 = TypeDescriptor.GetProperties(component).OfType<PropertyDescriptor>();
      IEnumerable<string> collection1 = source1.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Attributes[typeof (PropertyPersistenceAttribute)] != null && (x.Attributes[typeof (PropertyPersistenceAttribute)] as PropertyPersistenceAttribute).IsKey)).Select<PropertyDescriptor, string>((Func<PropertyDescriptor, string>) (x => x.Name));
      IEnumerable<string> collection2 = source1.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (x => x.Attributes[typeof (NonMultilingualAttribute)] != null)).Select<PropertyDescriptor, string>((Func<PropertyDescriptor, string>) (x => x.Name));
      List<string> source2 = new List<string>(collection1);
      source2.AddRange(collection2);
      source2.AddRange((IEnumerable<string>) ObjectData.NonMultilingualStandardPropNames);
      IEnumerable<string> strings = source2.Distinct<string>();
      IList<ControlProperty> properties = this.Source.Properties;
      foreach (string str in strings)
      {
        string name = str;
        ControlProperty controlProperty = properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == name));
        if (controlProperty != null)
        {
          properties.Remove(controlProperty);
          controlPropertyList.Add(controlProperty);
        }
      }
      return (IEnumerable<ControlProperty>) controlPropertyList;
    }
  }
}
