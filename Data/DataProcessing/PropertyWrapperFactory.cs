// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.PropertyWrapperFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.DataProcessing.Helpers;
using Telerik.Sitefinity.Data.DataProcessing.Processors;

namespace Telerik.Sitefinity.Data.DataProcessing
{
  internal class PropertyWrapperFactory : IPropertyWrapperFactory
  {
    public IEnumerable<PropertyWrapper> GetPropertyWrappers(object item)
    {
      IEnumerable<PropertyWrapper> propertyWrappers = (IEnumerable<PropertyWrapper>) new List<PropertyWrapper>();
      Type type = item.GetType();
      foreach (PropertyDescriptor propertyDescriptor in this.GetPropertiesForProcessing(type))
      {
        List<IDataProcessor> processors = new List<IDataProcessor>();
        foreach (IDataProcessor processor in this.GetProcessorFactory().GetProcessors())
        {
          if (processor.ShouldProcess(propertyDescriptor, type))
            processors.Add(processor);
        }
        if (processors.Count > 0)
          ((List<PropertyWrapper>) propertyWrappers).Add(new PropertyWrapper(propertyDescriptor, (IEnumerable<IDataProcessor>) processors));
      }
      return propertyWrappers;
    }

    protected virtual ProcessorFactory<DataConfig, IDataProcessor> GetProcessorFactory() => ProcessorFactory<DataConfig, IDataProcessor>.Instance;

    protected virtual IEnumerable<PropertyDescriptor> GetPropertiesForProcessing(
      Type type)
    {
      return new DataProcessingUtils().GetPropertiesForProcessing(type);
    }
  }
}
