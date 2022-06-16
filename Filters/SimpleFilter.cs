// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.SimpleFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Filters
{
  internal class SimpleFilter
  {
    public SimpleFilter(string name)
      : this(name, Enumerable.Empty<string>())
    {
    }

    public SimpleFilter(string name, IEnumerable<string> parameters)
    {
      this.Name = name.Trim();
      this.Parameters = parameters;
    }

    public SimpleFilter(string name, string value)
      : this(name, Enumerable.Empty<string>())
    {
      if (value == null)
        return;
      this.Parameters = (IEnumerable<string>) value.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
    }

    public string Name { get; private set; }

    public IEnumerable<string> Parameters { get; private set; }
  }
}
