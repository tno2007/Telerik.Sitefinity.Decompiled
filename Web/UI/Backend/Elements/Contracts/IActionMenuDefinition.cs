// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IActionMenuDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Base interface that provides all information needed to construct action menu.
  /// </summary>
  public interface IActionMenuDefinition
  {
    /// <summary>Gets or sets the main action.</summary>
    /// <value>The main action.</value>
    ICommandWidgetDefinition MainAction { get; set; }

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetDefinition" /> objects.
    /// </summary>
    /// <value>The menu items.</value>
    IEnumerable<IWidgetDefinition> MenuItems { get; }
  }
}
