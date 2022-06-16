// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.DataProcessingEngine
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data.DataProcessing.Helpers;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.DataProcessing
{
  /// <inheritdoc />
  internal class DataProcessingEngine : IDataProcessingEngine
  {
    protected virtual IPropertyWrapperFactory GetPropertyWrapperFactory() => (IPropertyWrapperFactory) PropertyWrapperCachingFactory.Instance;

    public void Process(DataProviderBase provider, IList items)
    {
      foreach (object obj1 in (IEnumerable) items)
      {
        if (this.ShouldCheck(obj1))
        {
          foreach (PropertyWrapper propertyWrapper in this.GetPropertyWrapperFactory().GetPropertyWrappers(obj1))
          {
            foreach (IPropertyValue modifiedPropertyValue in this.GetModifiedPropertyValues(provider, propertyWrapper.Property, obj1))
            {
              object obj2 = modifiedPropertyValue.GetValue();
              if (propertyWrapper.ApplyPropertyProcessors(ref obj2))
                modifiedPropertyValue.SetValue(obj2);
            }
          }
        }
      }
    }

    private bool ShouldCheck(object item) => item is IDataItem;

    private IEnumerable<IPropertyValue> GetModifiedPropertyValues(
      DataProviderBase provider,
      PropertyDescriptor property,
      object item)
    {
      IEnumerable<IPropertyValue> modifiedPropertyValues = (IEnumerable<IPropertyValue>) new List<IPropertyValue>();
      if (property is LstringPropertyDescriptor)
      {
        string[] cultures = new string[0];
        if (provider.IsLstringFieldDirty(item, property.Name, out cultures))
          modifiedPropertyValues = (IEnumerable<IPropertyValue>) ((IEnumerable<string>) cultures).Select<string, LstringPropertyValue>((Func<string, LstringPropertyValue>) (culture => new LstringPropertyValue((LstringPropertyDescriptor) property, item, culture)));
      }
      else if (provider.IsFieldDirty(item, property.Name))
        modifiedPropertyValues = (IEnumerable<IPropertyValue>) new IPropertyValue[1]
        {
          (IPropertyValue) new PropertyValue(property, item)
        };
      return modifiedPropertyValues;
    }
  }
}
