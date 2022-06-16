// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Restriction.CommandWidgetPackagingRestrictionStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Packaging.Configuration;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Packaging.Restriction
{
  /// <summary>
  /// Defines a command widget packaging restriction strategy.
  /// </summary>
  internal class CommandWidgetPackagingRestrictionStrategy : CommandWidgetRestrictionStrategyBase
  {
    private static readonly Dictionary<string, PackagingMode> RestrictedCommandWIdgetsByName = new Dictionary<string, PackagingMode>();

    public override bool IsRestricted(ICommandWidgetDefinition d, Type contentType)
    {
      string name = d.Name;
      return CommandWidgetPackagingRestrictionStrategy.RestrictedCommandWIdgetsByName.ContainsKey(name) && !this.IsCommandWidgetVisible(CommandWidgetPackagingRestrictionStrategy.RestrictedCommandWIdgetsByName[name]);
    }

    /// <summary>Adds the specified page node identifier.</summary>
    /// <param name="widgetName">Name of the widget.</param>
    /// <param name="packagingMode">The packaging mode.</param>
    /// <exception cref="T:System.ArgumentException">widget name</exception>
    internal static void Add(string widgetName, PackagingMode packagingMode)
    {
      if (string.IsNullOrEmpty(widgetName))
        throw new ArgumentException("widget name");
      if (CommandWidgetPackagingRestrictionStrategy.RestrictedCommandWIdgetsByName.ContainsKey(widgetName))
        return;
      CommandWidgetPackagingRestrictionStrategy.RestrictedCommandWIdgetsByName.Add(widgetName, packagingMode);
    }

    protected static Dictionary<string, PackagingMode> GetRestrictedCommandWidgets() => CommandWidgetPackagingRestrictionStrategy.RestrictedCommandWIdgetsByName;

    private bool IsCommandWidgetVisible(PackagingMode packagingMode) => (Config.Get<PackagingConfig>().PackagingMode & packagingMode) == PackagingMode.Source;
  }
}
