// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.SearchWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents a definition of the search widget</summary>
  public class SearchWidgetDefinition : 
    CommandWidgetDefinition,
    ISearchWidgetDefinition,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private Type persistentTypeToSearch;
    private BackendSearchMode mode;
    private string closeSearchCommandName;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public SearchWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public SearchWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public SearchWidgetDefinition GetDefinition() => this;

    /// <summary>Gets or sets the persistent type to search.</summary>
    /// <value>The persistent type to search.</value>
    public Type PersistentTypeToSearch
    {
      get => this.ResolveProperty<Type>(nameof (PersistentTypeToSearch), this.persistentTypeToSearch);
      set => this.persistentTypeToSearch = value;
    }

    /// <summary>Gets or sets the mode of the search widget.</summary>
    /// <value>The mode.</value>
    public BackendSearchMode Mode
    {
      get => this.ResolveProperty<BackendSearchMode>(nameof (Mode), this.mode);
      set => this.mode = value;
    }

    /// <summary>
    /// Gets or sets the name of the command that widget fires when closing the search.
    /// </summary>
    /// <value></value>
    public string CloseSearchCommandName
    {
      get => this.ResolveProperty<string>(nameof (CloseSearchCommandName), this.closeSearchCommandName);
      set => this.closeSearchCommandName = value;
    }
  }
}
