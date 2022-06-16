// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.CommandToolboxItemDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents a definition of the Sitefinity dialog</summary>
  [ParseChildren(true)]
  [DebuggerDisplay("CommandToolboxItemDefinition")]
  public class CommandToolboxItemDefinition : 
    ToolboxItemBaseDefinition,
    ICommandToolboxItemDefinition,
    IToolboxItemBaseDefinition
  {
    private bool causesValidation;
    private string commandName;
    private CommandType commandType;
    private string text;
    private string resourceClassId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.PromptDialogDefinition" /> class.
    /// </summary>
    public CommandToolboxItemDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.PromptDialogDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public CommandToolboxItemDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public CommandToolboxItemDefinition GetDefinition() => this;

    public bool CausesValidation
    {
      get => this.ResolveProperty<bool>(nameof (CausesValidation), this.causesValidation);
      set => this.causesValidation = value;
    }

    public string CommandName
    {
      get => this.ResolveProperty<string>(nameof (CommandName), this.commandName);
      set => this.commandName = value;
    }

    public CommandType CommandType
    {
      get => this.ResolveProperty<CommandType>(nameof (CommandType), this.commandType);
      set => this.commandType = value;
    }

    public string Text
    {
      get => this.ResolveProperty<string>(nameof (Text), this.text);
      set => this.text = value;
    }

    /// <summary>
    /// Gets or sets the resource class for localizing resources.
    /// </summary>
    /// <value>The resource class.</value>
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }
  }
}
