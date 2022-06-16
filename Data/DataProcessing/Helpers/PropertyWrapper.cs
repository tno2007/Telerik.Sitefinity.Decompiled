// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.Helpers.PropertyWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data.DataProcessing.Processors;

namespace Telerik.Sitefinity.Data.DataProcessing.Helpers
{
  internal class PropertyWrapper
  {
    private IEnumerable<IDataProcessor> processors;

    public PropertyWrapper(PropertyDescriptor property, IEnumerable<IDataProcessor> processors)
    {
      this.Property = property;
      this.processors = processors;
    }

    public PropertyDescriptor Property { get; private set; }

    public bool ApplyPropertyProcessors(ref object value)
    {
      if (this.processors == null || this.processors.Count<IDataProcessor>() <= 0)
        return false;
      foreach (IDataProcessor processor in this.processors)
        processor.Process(ref value);
      return true;
    }
  }
}
