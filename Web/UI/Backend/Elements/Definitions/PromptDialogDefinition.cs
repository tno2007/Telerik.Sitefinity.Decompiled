// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.PromptDialogDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents a definition of the Sitefinity dialog</summary>
  [ParseChildren(true)]
  [DebuggerDisplay("PromptDialogDefinition {Name}, OpenOnCommandName={OpenOnCommandName}")]
  public class PromptDialogDefinition : DefinitionBase, IPromptDialogDefinition, IDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private string name;
    private string openOnCommandName;
    private string defaultInputText;
    private bool allowCloseButton;
    private bool displayed;
    private int height;
    private List<ICommandToolboxItemDefinition> commands;
    private int inputRows;
    private string itemTag;
    private string message;
    private PromptMode promptMode;
    private string onClientCommand;
    private bool showOnLoad;
    private string textFieldExample;
    private string textFieldTitle;
    private string title;
    private Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition validatorDefinition;
    private int width;
    private string wrapperCssClass;
    private string wrapperTag;
    private string resourceClassId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.PromptDialogDefinition" /> class.
    /// </summary>
    public PromptDialogDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.PromptDialogDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public PromptDialogDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public PromptDialogDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    /// <summary>
    /// Gets or sets the name of the view. Used for resolving property values.
    /// </summary>
    /// <value>The name of the view.</value>
    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string DialogName
    {
      get => this.ResolveProperty<string>("name", this.name);
      set => this.name = value;
    }

    /// <summary>
    /// Gets or sets the name of the command that will cause the dialog defined by this definition
    /// to be opened.
    /// </summary>
    /// <value>Name of the command.</value>
    public string OpenOnCommandName
    {
      get => this.ResolveProperty<string>(nameof (OpenOnCommandName), this.openOnCommandName);
      set => this.openOnCommandName = value;
    }

    public bool AllowCloseButton
    {
      get => this.ResolveProperty<bool>(nameof (AllowCloseButton), this.allowCloseButton);
      set => this.allowCloseButton = value;
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<ICommandToolboxItemDefinition> Commands
    {
      get
      {
        if (this.commands != null)
          return this.ResolveProperty<List<ICommandToolboxItemDefinition>>(nameof (Commands), this.commands);
        this.commands = new List<ICommandToolboxItemDefinition>();
        return this.commands;
      }
    }

    public string DefaultInputText
    {
      get => this.ResolveProperty<string>(nameof (DefaultInputText), this.defaultInputText);
      set => this.defaultInputText = value;
    }

    public bool Displayed
    {
      get => this.ResolveProperty<bool>(nameof (Displayed), this.displayed);
      set => this.displayed = value;
    }

    public int Height
    {
      get => this.ResolveProperty<int>(nameof (Height), this.height);
      set => this.height = value;
    }

    public int InputRows
    {
      get => this.ResolveProperty<int>(nameof (InputRows), this.inputRows);
      set => this.inputRows = value;
    }

    public string ItemTag
    {
      get => this.ResolveProperty<string>(nameof (ItemTag), this.itemTag);
      set => this.itemTag = value;
    }

    public string Message
    {
      get => this.ResolveProperty<string>(nameof (Message), this.message);
      set => this.message = value;
    }

    public PromptMode Mode
    {
      get => this.ResolveProperty<PromptMode>("PromptMode", this.promptMode);
      set => this.promptMode = value;
    }

    public string OnClientCommand
    {
      get => this.ResolveProperty<string>(nameof (OnClientCommand), this.onClientCommand);
      set => this.onClientCommand = value;
    }

    public bool ShowOnLoad
    {
      get => this.ResolveProperty<bool>(nameof (ShowOnLoad), this.showOnLoad);
      set => this.showOnLoad = value;
    }

    public string TextFieldExample
    {
      get => this.ResolveProperty<string>(nameof (TextFieldExample), this.textFieldExample);
      set => this.textFieldExample = value;
    }

    public string TextFieldTitle
    {
      get => this.ResolveProperty<string>(nameof (TextFieldTitle), this.textFieldTitle);
      set => this.textFieldTitle = value;
    }

    public string Title
    {
      get => this.ResolveProperty<string>(nameof (Title), this.title);
      set => this.title = value;
    }

    public IValidatorDefinition ValidatorDefinition => (IValidatorDefinition) this.ResolveProperty<Telerik.Sitefinity.Web.UI.Validation.Definitions.ValidatorDefinition>(nameof (ValidatorDefinition), this.validatorDefinition);

    public int Width
    {
      get => this.ResolveProperty<int>(nameof (Width), this.width);
      set => this.width = value;
    }

    public string WrapperCssClass
    {
      get => this.ResolveProperty<string>(nameof (WrapperCssClass), this.wrapperCssClass);
      set => this.wrapperCssClass = value;
    }

    public string WrapperTag
    {
      get => this.ResolveProperty<string>(nameof (WrapperTag), this.wrapperTag);
      set => this.wrapperTag = value;
    }

    /// <summary>
    /// Gets or sets the resource class pageId for styling the widget's html.
    /// </summary>
    /// <value>The resource class.</value>
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }
  }
}
