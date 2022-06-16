// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.ISearchWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// An interface the provides the common members for all definitions that define a search widget.
  /// </summary>
  public interface ISearchWidgetDefinition : ICommandWidgetDefinition, IWidgetDefinition, IDefinition
  {
    /// <summary>Gets or sets the persistent type to search.</summary>
    /// <value>The persistent type to search.</value>
    Type PersistentTypeToSearch { get; set; }

    /// <summary>Gets or sets the mode of the search widget.</summary>
    /// <value>The mode.</value>
    BackendSearchMode Mode { get; set; }

    /// <summary>
    /// Gets or sets the name of the command that widget fires when closing the search.
    /// </summary>
    string CloseSearchCommandName { get; set; }
  }
}
