// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.StateWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>A definition for a state widget.</summary>
  public class StateWidgetDefinition : 
    CommandWidgetDefinition,
    IStateWidgetDefinition,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private List<IStateCommandWidgetDefinition> states;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public StateWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public StateWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>Gets or sets the states.</summary>
    /// <value>The states.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IStateCommandWidgetDefinition> States
    {
      get
      {
        if (this.states == null)
          this.states = new List<IStateCommandWidgetDefinition>();
        return this.ResolveProperty<List<IStateCommandWidgetDefinition>>(nameof (States), this.states);
      }
    }

    /// <summary>Gets the collection of states.</summary>
    /// <value>The states.</value>
    IEnumerable<IStateCommandWidgetDefinition> IStateWidgetDefinition.States => (IEnumerable<IStateCommandWidgetDefinition>) this.States;
  }
}
