// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.EmailTextFieldDefinitionElement
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
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for text fields.</summary>
  public class EmailTextFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IEmailTextFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.EmailTextFieldDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public EmailTextFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns>The definition instance</returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new EmailTextFieldDefinition((ConfigElement) this);

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
    /// Gets or sets a value indicating whether to trim spaces.
    /// </summary>
    [ConfigurationProperty("trimSpaces")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TrimSpacesDescription", Title = "TrimSpacesCaption")]
    public bool TrimSpaces
    {
      get => (bool) this["trimSpaces"];
      set => this["trimSpaces"] = (object) value;
    }

    /// <summary>Gets or sets the read only replacement.</summary>
    [ConfigurationProperty("readOnlyReplacement")]
    public string ReadOnlyReplacement
    {
      get => (string) this["readOnlyReplacement"];
      set => this["readOnlyReplacement"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("allowNulls")]
    public bool AllowNulls
    {
      get => (bool) this["allowNulls"];
      set => this["allowNulls"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show a tooltip.
    /// </summary>
    [ConfigurationProperty("toolTipVisible")]
    public bool ToolTipVisible
    {
      get => (bool) this["toolTipVisible"];
      set => this["toolTipVisible"] = (object) value;
    }

    /// <summary>Gets or sets the text of the tooltip target.</summary>
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

    /// <inheritdoc />
    [ConfigurationProperty("serverSideOnly")]
    public bool ServerSideOnly
    {
      get => (bool) this["serverSideOnly"];
      set => this["serverSideOnly"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (EmailTextField);

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
      public const string Unit = "unit";
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
