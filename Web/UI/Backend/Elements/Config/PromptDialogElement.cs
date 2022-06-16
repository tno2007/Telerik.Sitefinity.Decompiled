// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.PromptDialogElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Validation.Config;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents a configuration element for Sitefinity Prompt Dialog element.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogDescription", Title = "PromptDialogTitle")]
  [DebuggerDisplay("PromptDialogElement {Name}, OpenOnCommandName={OpenOnCommandName}")]
  public class PromptDialogElement : DefinitionConfigElement, IPromptDialogDefinition, IDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private List<ICommandToolboxItemDefinition> commands;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.PromptDialogElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public PromptDialogElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new PromptDialogDefinition((ConfigElement) this);

    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogNameDescription", Title = "PromptDialogNameCaption")]
    public string DialogName
    {
      get => this["name"] as string;
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the command that will popup this dialog.
    /// </summary>
    /// <value>The name of the command.</value>
    [ConfigurationProperty("OpenOnCommandName", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogCommandNameDescription", Title = "PromptDialogCommandNameCaption")]
    public string OpenOnCommandName
    {
      get => this[nameof (OpenOnCommandName)] as string;
      set => this[nameof (OpenOnCommandName)] = (object) value;
    }

    [ConfigurationProperty("AllowCloseButton", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogAllowCloseButtonDescription", Title = "PromptDialogAllowCloseButtonCaption")]
    public bool AllowCloseButton
    {
      get => (bool) this[nameof (AllowCloseButton)];
      set => this[nameof (AllowCloseButton)] = (object) value;
    }

    [ConfigurationProperty("Commands")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogCommandsDescription", Title = "PromptDialogCommandsCaption")]
    public ConfigElementList<CommandToolboxItemElement> CommandsConfig => this["Commands"] as ConfigElementList<CommandToolboxItemElement>;

    public List<ICommandToolboxItemDefinition> Commands
    {
      get
      {
        if (this.commands == null)
          this.commands = ((IEnumerable<ICommandToolboxItemDefinition>) this.CommandsConfig.Elements).ToList<ICommandToolboxItemDefinition>();
        return this.commands;
      }
    }

    [ConfigurationProperty("DefaultInputText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogDefaultInputTextDescription", Title = "PromptDialogDefaultInputTextCaption")]
    public string DefaultInputText
    {
      get => this[nameof (DefaultInputText)] as string;
      set => this[nameof (DefaultInputText)] = (object) value;
    }

    [ConfigurationProperty("Displayed", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogDisplayedDescription", Title = "PromptDialogDisplayedCaption")]
    public bool Displayed
    {
      get => (bool) this[nameof (Displayed)];
      set => this[nameof (Displayed)] = (object) value;
    }

    [ConfigurationProperty("Height", DefaultValue = 300)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogHeightDescription", Title = "PromptDialogHeightCaption")]
    public int Height
    {
      get => (int) this[nameof (Height)];
      set => this[nameof (Height)] = (object) value;
    }

    [ConfigurationProperty("Width", DefaultValue = 300)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogWidthDescription", Title = "PromptDialogWidthCaption")]
    public int Width
    {
      get => (int) this[nameof (Width)];
      set => this[nameof (Width)] = (object) value;
    }

    [ConfigurationProperty("InputRows", DefaultValue = 5)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogInputRowsDescription", Title = "PromptDialogInputRowsCaption")]
    public int InputRows
    {
      get => (int) this[nameof (InputRows)];
      set => this[nameof (InputRows)] = (object) value;
    }

    [ConfigurationProperty("ItemTag")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogItemTagDescription", Title = "PromptDialogItemTagCaption")]
    public string ItemTag
    {
      get => this[nameof (ItemTag)] as string;
      set => this[nameof (ItemTag)] = (object) value;
    }

    [ConfigurationProperty("Message")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogMessageDescription", Title = "PromptDialogMessageCaption")]
    public string Message
    {
      get => this[nameof (Message)] as string;
      set => this[nameof (Message)] = (object) value;
    }

    [ConfigurationProperty("PromptMode", DefaultValue = PromptMode.Confirm)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogPromptModeDescription", Title = "PromptDialogPromptModeCaption")]
    public PromptMode Mode
    {
      get => (PromptMode) this["PromptMode"];
      set => this["PromptMode"] = (object) value;
    }

    [ConfigurationProperty("OnClientCommand")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogOnClientCommandDescription", Title = "PromptDialogOnClientCommandCaption")]
    public string OnClientCommand
    {
      get => this[nameof (OnClientCommand)] as string;
      set => this[nameof (OnClientCommand)] = (object) value;
    }

    [ConfigurationProperty("ShowOnLoad", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogShowOnLoadDescription", Title = "PromptDialogShowOnLoadCaption")]
    public bool ShowOnLoad
    {
      get => (bool) this[nameof (ShowOnLoad)];
      set => this[nameof (ShowOnLoad)] = (object) value;
    }

    [ConfigurationProperty("TextFieldExample")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogTextFieldExampleDescription", Title = "PromptDialogTextFieldExampleCaption")]
    public string TextFieldExample
    {
      get => this[nameof (TextFieldExample)] as string;
      set => this[nameof (TextFieldExample)] = (object) value;
    }

    [ConfigurationProperty("TextFieldTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogTextFieldTitleDescription", Title = "PromptDialogTextFieldTitleCaption")]
    public string TextFieldTitle
    {
      get => this[nameof (TextFieldTitle)] as string;
      set => this[nameof (TextFieldTitle)] = (object) value;
    }

    [ConfigurationProperty("Title")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogTitleDescription", Title = "PromptDialogTitleCaption")]
    public string Title
    {
      get => this[nameof (Title)] as string;
      set => this[nameof (Title)] = (object) value;
    }

    [ConfigurationProperty("ValidatorDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogValidatorDefinitionDescription", Title = "PromptDialogValidatorDefinitionCaption")]
    public ValidatorDefinitionElement ValidatorDefinition
    {
      get => this[nameof (ValidatorDefinition)] as ValidatorDefinitionElement;
      set => this[nameof (ValidatorDefinition)] = (object) value;
    }

    IValidatorDefinition IPromptDialogDefinition.ValidatorDefinition => (IValidatorDefinition) this.ValidatorDefinition;

    [ConfigurationProperty("WrapperCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogWrapperCssClassDescription", Title = "PromptDialogWrapperCssClassCaption")]
    public string WrapperCssClass
    {
      get => this[nameof (WrapperCssClass)] as string;
      set => this[nameof (WrapperCssClass)] = (object) value;
    }

    [ConfigurationProperty("WrapperTag")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PromptDialogWrapperTagDescription", Title = "PromptDialogWrapperTagCaption")]
    public string WrapperTag
    {
      get => this[nameof (WrapperTag)] as string;
      set => this[nameof (WrapperTag)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class.</value>
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }
  }
}
