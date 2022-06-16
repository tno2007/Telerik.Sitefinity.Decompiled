// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.LanguageChoiceFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes a choice field.
  /// </summary>
  public class LanguageChoiceFieldElement : 
    ChoiceFieldElement,
    ILanguageChoiceFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private const string LanguageSourcePropertyName = "languageSource";
    private const string HideIfSingleLanguagePropertyName = "hideSingleLanguage";
    private const string LanguagesToShowPropertyName = "languagesToShow";

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public LanguageChoiceFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.FieldControlDefinitionElement" /> class.
    /// </summary>
    internal LanguageChoiceFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new LanguageChoiceFieldDefinition((ConfigElement) this);

    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    [ConfigurationProperty("languageSource")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "LanguageSourceDescription", Title = "LanguageSourceCaption")]
    public Telerik.Sitefinity.Localization.Web.UI.LanguageSource? LanguageSource
    {
      get => (Telerik.Sitefinity.Localization.Web.UI.LanguageSource?) this["languageSource"];
      set => this["languageSource"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether to hide the field if only a single language is found.
    /// </summary>
    /// <value>True if the field must be hidden when only a single language is found.</value>
    [ConfigurationProperty("hideSingleLanguage")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "HideIfSingleLanguageDescription", Title = "HideIfSingleLanguageCaption")]
    public bool? HideIfSingleLanguage
    {
      get => (bool?) this["hideSingleLanguage"];
      set => this["hideSingleLanguage"] = (object) value;
    }

    /// <summary>Gets or sets the languages to show.</summary>
    [ConfigurationProperty("languagesToShow")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "LanguagesToShowDescription", Title = "LanguagesToShowCaption")]
    public LanguagesSelection? LanguagesToShow
    {
      get => (LanguagesSelection?) this["languagesToShow"];
      set => this["languagesToShow"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (LanguageChoiceField);

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();
  }
}
