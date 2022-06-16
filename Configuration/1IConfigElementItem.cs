// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.IConfigElementItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Configuration
{
  public interface IConfigElementItem
  {
    string Key { get; set; }

    ConfigElement Element { get; }

    object ElementProperties { get; }

    IDictionary<string, object> ItemProperties { get; }

    bool IsLazy { get; }

    bool IsLoaded { get; }

    bool IsDeleted { get; set; }

    ConfigElementLoadException LoadingError { get; set; }

    void Unload();

    void Unload(Func<ConfigElement> newLoader);
  }
}
