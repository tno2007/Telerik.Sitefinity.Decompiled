// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Data.ConfigUpgradeContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Configuration.Data
{
  /// <summary>Configuration upgrade context</summary>
  internal class ConfigUpgradeContext
  {
    private Dictionary<string, object> items;
    private IList<Action<ConfigUpgradeContext, ConfigElement>> handlers;

    /// <summary>Gets or sets the upgrade from.</summary>
    /// <value>The upgrade from.</value>
    public Version UpgradeFrom { get; set; }

    public ConfigSource Source { get; set; }

    /// <summary>Called when [load element].</summary>
    /// <param name="element">The element.</param>
    public void OnLoadElement(ConfigElement element)
    {
      if (this.handlers == null)
        return;
      foreach (Action<ConfigUpgradeContext, ConfigElement> handler in (IEnumerable<Action<ConfigUpgradeContext, ConfigElement>>) this.handlers)
        handler(this, element);
    }

    /// <summary>Adds the element load handler.</summary>
    /// <param name="handler">The handler.</param>
    public void AddElementLoadHandler(
      Action<ConfigUpgradeContext, ConfigElement> handler)
    {
      if (this.handlers == null)
        this.handlers = (IList<Action<ConfigUpgradeContext, ConfigElement>>) new List<Action<ConfigUpgradeContext, ConfigElement>>();
      this.handlers.Add(handler);
    }

    /// <summary>Gets the items.</summary>
    /// <value>The items.</value>
    public IDictionary<string, object> Items
    {
      get
      {
        if (this.items == null)
          this.items = new Dictionary<string, object>();
        return (IDictionary<string, object>) this.items;
      }
    }
  }
}
