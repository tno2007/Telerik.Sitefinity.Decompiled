// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IStateWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>Declares the contract for a state widget definition.</summary>
  public interface IStateWidgetDefinition : ICommandWidgetDefinition, IWidgetDefinition, IDefinition
  {
    /// <summary>Gets the collection of states.</summary>
    /// <value>The states.</value>
    IEnumerable<IStateCommandWidgetDefinition> States { get; }
  }
}
