// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.TextFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for text fields.</summary>
  public class TextFieldDefinitionElement : 
    FieldControlDefinitionElement,
    ITextFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public TextFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new TextFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the number of rows displayed in a multiline text box.
    /// </summary>
    /// <value>The rows.</value>
    [ConfigurationProperty("rows", DefaultValue = 1)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RowsDescription", Title = "RowsCaption")]
    public int Rows
    {
      get => (int) this["rows"];
      set => this["rows"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating if the text field is used as password field.
    /// </summary>
    [ConfigurationProperty("isPasswordMode")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsPasswordModeDescription", Title = "IsPasswordModeCaption")]
    public bool? IsPasswordMode
    {
      get => (bool?) this["isPasswordMode"];
      set => this["isPasswordMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the unit of the value. If set, will be displayed after the value.
    /// </summary>
    [ConfigurationProperty("unit")]
    public string Unit
    {
      get => (string) this["unit"];
      set => this["unit"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the number of rows displayed in a multiline text box.
    /// </summary>
    /// <value>The rows.</value>
    [ConfigurationProperty("HideIfValue")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HideIfValueDescription", Title = "HideIfValueCaption")]
    public string HideIfValue
    {
      get => (string) this[nameof (HideIfValue)];
      set => this[nameof (HideIfValue)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    [ConfigurationProperty("expandableDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpandableControlElementDescription", Title = "ExpandableControlElementCaption")]
    public new ExpandableControlElement ExpandableDefinitionConfig
    {
      get => (ExpandableControlElement) this["expandableDefinition"];
      set => this["expandableDefinition"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    [ConfigurationProperty("isLocalizable")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsLocalizableDescription", Title = "IsLocalizableCaption")]
    public bool IsLocalizable
    {
      get => (bool) this["isLocalizable"];
      set => this["isLocalizable"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show character counter label.
    /// </summary>
    [ConfigurationProperty("showCharacterCounter")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowCharacterCounterDescription", Title = "ShowCharacterCounterCaption")]
    public bool ShowCharacterCounter
    {
      get => (bool) this["showCharacterCounter"];
      set => this["showCharacterCounter"] = (object) value;
    }

    /// <summary>Gets or sets the recomended characters count.</summary>
    [ConfigurationProperty("recommendedCharactersCount")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RecommendedCharactersCountDescription", Title = "RecommendedCharactersCountCaption")]
    public int RecommendedCharactersCount
    {
      get => (int) this["recommendedCharactersCount"];
      set => this["recommendedCharactersCount"] = (object) value;
    }

    /// <summary>Gets or sets the max characters count.</summary>
    [ConfigurationProperty("maxCharactersCount")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxCharactersCountDescription", Title = "MaxCharactersCountCaption")]
    public int MaxCharactersCount
    {
      get => (int) this["maxCharactersCount"];
      set => this["maxCharactersCount"] = (object) value;
    }

    /// <summary>Gets or sets the character counter description.</summary>
    [ConfigurationProperty("characterCounterDescription")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CharacterCounterDescriptionDescription", Title = "CharacterCounterDescriptionCaption")]
    public string CharacterCounterDescription
    {
      get => (string) this["characterCounterDescription"];
      set => this["characterCounterDescription"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to trim spaces.
    /// </summary>
    [ConfigurationProperty("trimSpaces")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TrimSpacesDescription", Title = "TrimSpacesCaption")]
    public bool TrimSpaces
    {
      get => (bool) this["trimSpaces"];
      set => this["trimSpaces"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("allowNulls")]
    public bool AllowNulls
    {
      get => (bool) this["allowNulls"];
      set => this["allowNulls"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition => (IExpandableControlDefinition) this.ExpandableDefinitionConfig;

    /// <summary>
    /// Gets or sets a value indicating whether to show a tooltip.
    /// </summary>
    [ConfigurationProperty("toolTipVisible")]
    public bool ToolTipVisible
    {
      get => (bool) this["toolTipVisible"];
      set => this["toolTipVisible"] = (object) value;
    }

    /// <summary>Get or sets the text of the tooltip target.</summary>
    [ConfigurationProperty("toolTipText")]
    public string ToolTipText
    {
      get => (string) this["toolTipText"];
      set => this["toolTipText"] = (object) value;
    }

    /// <summary>Gets or sets the title of the tooltip.</summary>
    [ConfigurationProperty("toolTipTitle")]
    public string ToolTipTitle
    {
      get => (string) this["toolTipTitle"];
      set => this["toolTipTitle"] = (object) value;
    }

    /// <summary>Gets or sets the content of the tooltip.</summary>
    [ConfigurationProperty("toolTipContent")]
    public string ToolTipContent
    {
      get => (string) this["toolTipContent"];
      set => this["toolTipContent"] = (object) value;
    }

    /// <summary>Gets or sets the css class of the tooltip.</summary>
    [ConfigurationProperty("toolTipCssClass")]
    public string ToolTipCssClass
    {
      get => (string) this["toolTipCssClass"];
      set => this["toolTipCssClass"] = (object) value;
    }

    /// <summary>Gets or sets the css class of the tooltip target.</summary>
    [ConfigurationProperty("toolTipTargetCssClass")]
    public string ToolTipTargetCssClass
    {
      get => (string) this["toolTipTargetCssClass"];
      set => this["toolTipTargetCssClass"] = (object) value;
    }

    /// <summary>Gets or sets the read only replacement.</summary>
    [ConfigurationProperty("readOnlyReplacement")]
    public string ReadOnlyReplacement
    {
      get => (string) this["readOnlyReplacement"];
      set => this["readOnlyReplacement"] = (object) value;
    }

    /// <summary>Gets or sets the autocomplete service url.</summary>
    [ConfigurationProperty("autocompleteServiceUrl")]
    public string AutocompleteServiceUrl
    {
      get => (string) this["autocompleteServiceUrl"];
      set => this["autocompleteServiceUrl"] = (object) value;
    }

    /// <summary>Gets or sets the autocomplete suggestions count.</summary>
    [ConfigurationProperty("autocompleteSuggestionsCount")]
    public int AutocompleteSuggestionsCount
    {
      get => (int) this["autocompleteSuggestionsCount"];
      set => this["autocompleteSuggestionsCount"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("serverSideOnly")]
    public bool ServerSideOnly
    {
      get => (bool) this["serverSideOnly"];
      set => this["serverSideOnly"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (TextField);

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string Rows = "rows";
      public const string HideIfValue = "HideIfValue";
      public const string ExpandableDefinition = "expandableDefinition";
      public const string IsPasswordMode = "isPasswordMode";
      public const string unit = "unit";
      public const string IsLocalizable = "isLocalizable";
      public const string ShowCharacterCounter = "showCharacterCounter";
      public const string RecommendedCharactersCount = "recommendedCharactersCount";
      public const string ReadOnlyReplacement = "readOnlyReplacement";
      public const string MaxCharactersCount = "maxCharactersCount";
      public const string CharacterCounterDescription = "characterCounterDescription";
      public const string TrimSpaces = "trimSpaces";
      public const string AllowNulls = "allowNulls";
      public const string ToolTipVisible = "toolTipVisible";
      public const string ToolTipText = "toolTipText";
      public const string ToolTipTitle = "toolTipTitle";
      public const string ToolTipContent = "toolTipContent";
      public const string ToolTipCssClass = "toolTipCssClass";
      public const string ToolTipTargetCssClass = "toolTipTargetCssClass";
      public const string AutocompleteServiceUrl = "autocompleteServiceUrl";
      public const string AutocompleteSuggestionsCount = "autocompleteSuggestionsCount";
      public const string ServerSideOnly = "serverSideOnly";
    }
  }
}
