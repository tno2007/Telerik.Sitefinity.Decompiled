// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IContentFilteringWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// An interface the provides the common members for all definitions that define a widget
  /// that can fire a command.
  /// </summary>
  internal interface IContentFilteringWidgetDefinition : 
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>Gets or sets the predefined filtering ranges.</summary>
    /// <value>The predefined filtering ranges.</value>
    IEnumerable<IFilterRangeDefinition> PredefinedFilteringRanges { get; }

    /// <summary>
    /// Gets or sets the name of the property to filter against.
    /// </summary>
    string PropertyNameToFilter { get; set; }
  }
}
