// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Restriction.CommandWidgetReadOnlyConfigRestrictionStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Restriction
{
  /// <summary>Defines a command widget restriction strategy.</summary>
  internal class CommandWidgetReadOnlyConfigRestrictionStrategy : 
    CommandWidgetRestrictionStrategyBase
  {
    private static readonly Dictionary<string, RestrictionLevel> RestrictedCommandWIdgetsByName = new Dictionary<string, RestrictionLevel>();

    public override bool IsRestricted(ICommandWidgetDefinition def, Type contentType)
    {
      string name = def.Name;
      return CommandWidgetReadOnlyConfigRestrictionStrategy.RestrictedCommandWIdgetsByName.ContainsKey(name) && !SystemManager.IsOperationEnabled(CommandWidgetReadOnlyConfigRestrictionStrategy.RestrictedCommandWIdgetsByName[name]);
    }

    /// <summary>Adds the specified page node identifier.</summary>
    /// <param name="widgetName">Name of the widget.</param>
    /// <param name="restrictionLevel">The restriction level.</param>
    /// <exception cref="T:System.ArgumentException">widget name</exception>
    internal static void Add(string widgetName, RestrictionLevel restrictionLevel)
    {
      if (string.IsNullOrEmpty(widgetName))
        throw new ArgumentException("widget name");
      if (CommandWidgetReadOnlyConfigRestrictionStrategy.RestrictedCommandWIdgetsByName.ContainsKey(widgetName))
        return;
      CommandWidgetReadOnlyConfigRestrictionStrategy.RestrictedCommandWIdgetsByName.Add(widgetName, restrictionLevel);
    }

    protected static Dictionary<string, RestrictionLevel> GetRestrictedCommandWidgets() => CommandWidgetReadOnlyConfigRestrictionStrategy.RestrictedCommandWIdgetsByName;
  }
}
