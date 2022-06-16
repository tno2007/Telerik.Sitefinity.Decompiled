// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyLoaders.PropertyLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Pages.Model.PropertyLoaders;

namespace Telerik.Sitefinity.Modules.Pages.PropertyLoaders
{
  internal abstract class PropertyLoader
  {
    private ObjectData source;

    public PropertyLoader(ObjectData source) => this.source = source;

    protected ObjectData Source => this.source;

    internal static PropertyLoader GetLoader(ObjectData source)
    {
      switch (source.Strategy)
      {
        case PropertyPersistenceStrategy.BackwardCompatible:
          return (PropertyLoader) new BackwardCompatibilityLoader(source);
        case PropertyPersistenceStrategy.NotTranslatable:
          return (PropertyLoader) new NotTranslatableLoader(source);
        case PropertyPersistenceStrategy.Translatable:
          return (PropertyLoader) new TranslatableLoader(source);
        default:
          return (PropertyLoader) null;
      }
    }

    internal abstract IEnumerable<ControlProperty> GetProperties(
      CultureInfo language = null,
      bool fallbackToAnyLanguage = false);

    internal virtual IEnumerable<ControlProperty> ClearProperties(
      object component = null,
      CultureInfo language = null)
    {
      List<ControlProperty> controlPropertyList = new List<ControlProperty>(this.Source.GetPropertiesRaw(ControlHelper.NormalizeLanguage(language)));
      foreach (ControlProperty controlProperty in controlPropertyList)
        this.Source.Properties.Remove(controlProperty);
      return (IEnumerable<ControlProperty>) controlPropertyList;
    }
  }
}
