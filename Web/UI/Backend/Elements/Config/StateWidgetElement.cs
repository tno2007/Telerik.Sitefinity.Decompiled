// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.StateWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>The configuration element for state widgets</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "StateWidgetDescription", Title = "StateWidgetTitle")]
  public class StateWidgetElement : 
    CommandWidgetElement,
    IStateWidgetDefinition,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    internal const string PropStates = "states";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.CommandWidgetElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public StateWidgetElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new StateWidgetDefinition((ConfigElement) this);

    /// <summary>Gets the collection of defined states.</summary>
    [ConfigurationProperty("states")]
    [ConfigurationCollection(typeof (StateCommandWidgetElement), AddItemName = "state")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "StateWidgetStatesDescription", Title = "StateWidgetStatesCaption")]
    public ConfigElementList<StateCommandWidgetElement> States => (ConfigElementList<StateCommandWidgetElement>) this["states"];

    /// <summary>Gets the collection of states.</summary>
    /// <value>The states.</value>
    IEnumerable<IStateCommandWidgetDefinition> IStateWidgetDefinition.States => this.States.AsEnumerable().Select<StateCommandWidgetElement, IStateCommandWidgetDefinition>((Func<StateCommandWidgetElement, IStateCommandWidgetDefinition>) (state => (IStateCommandWidgetDefinition) state.GetDefinition()));
  }
}
