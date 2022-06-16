// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.DataProcessingUtils
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services.Contracts;

namespace Telerik.Sitefinity.Data.DataProcessing
{
  internal class DataProcessingUtils
  {
    /// <summary>
    /// Gets the properties that should be processed by the <see cref="T:Telerik.Sitefinity.Data.DataProcessing.IDataProcessingEngine" />  for a given <see cref="T:System.Type" />
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The props</returns>
    public IEnumerable<PropertyDescriptor> GetPropertiesForProcessing(
      Type type)
    {
      List<PropertyDescriptor> propertiesForProcessing = new List<PropertyDescriptor>();
      ITypeSettings typeSettings = (ITypeSettings) null;
      foreach (KeyValuePair<string, ITypeSettings> registeredTypeSetting in (IEnumerable<KeyValuePair<string, ITypeSettings>>) this.GetRegisteredTypeSettings())
      {
        Type type1 = this.ResolveType(registeredTypeSetting.Value);
        if (type == type1 || type1.IsAbstract && type1.IsAssignableFrom(type))
        {
          typeSettings = registeredTypeSetting.Value;
          break;
        }
      }
      if (typeSettings != null)
      {
        PropertyDescriptorCollection typeProps = this.GetProperties(type);
        propertiesForProcessing.AddRange((IEnumerable<PropertyDescriptor>) typeSettings.Properties.OfType<IPersistentPropertyMapping>().Select<IPersistentPropertyMapping, PropertyDescriptor>((Func<IPersistentPropertyMapping, PropertyDescriptor>) (propMapping => typeProps[propMapping.Name])).ToList<PropertyDescriptor>());
      }
      return (IEnumerable<PropertyDescriptor>) propertiesForProcessing;
    }

    protected virtual PropertyDescriptorCollection GetProperties(
      Type type)
    {
      return TypeDescriptor.GetProperties(type);
    }

    protected virtual Type ResolveType(ITypeSettings ts) => TypeResolutionService.ResolveType(ts.ClrType);

    protected virtual IDictionary<string, ITypeSettings> GetRegisteredTypeSettings() => DataProcessingTypeSettingsProvider.GetTypeSettings();
  }
}
