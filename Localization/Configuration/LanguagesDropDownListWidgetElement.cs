// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Configuration.LanguagesDropDownListWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Contracts;
using Telerik.Sitefinity.Localization.Definitions;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Localization.Configuration
{
  /// <summary>
  /// A configuration element that represents a cultures list widget definition.
  /// </summary>
  public class LanguagesDropDownListWidgetElement : 
    WidgetElement,
    ILanguagesDropDownListWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private const string languageSourcePropertyName = "languageSource";
    private const string availableCulturesPropertyName = "availableCultures";
    private const string addAllLanguagesOptionPropertyName = "addAllLanguagesOption";
    private const string commandNamePropertyName = "commandName";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Configuration.LanguagesDropDownListWidgetElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public LanguagesDropDownListWidgetElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    [ConfigurationProperty("languageSource", DefaultValue = LanguageSource.Custom)]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "LanguageSourceDescription", Title = "LanguageSourceCaption")]
    public LanguageSource LanguageSource
    {
      get => (LanguageSource) this["languageSource"];
      set => this["languageSource"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the list of all listed cultures. This is only used if LanguageSource is set to Custom.
    /// </summary>
    /// <value>All available languages.</value>
    [ConfigurationProperty("availableCultures")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "AvailableLanguagesDescription", Title = "AvailableLanguagesCaption")]
    public List<CultureInfo> AvailableCultures
    {
      get => (List<CultureInfo>) this["availableCultures"];
      set => this["availableCultures"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether an option for all languages should be added.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if an option for all languages is to be added; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("addAllLanguagesOption", DefaultValue = false)]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "AddAllLanguagesOptionDescription", Title = "AddAllLanguagesOptionCaption")]
    public bool AddAllLanguagesOption
    {
      get => (bool) this["addAllLanguagesOption"];
      set => this["addAllLanguagesOption"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    [ConfigurationProperty("commandName")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "CommandNameDescription", Title = "CommandNameCaption")]
    public string CommandName
    {
      get => (string) this["commandName"];
      set => this["commandName"] = (object) value;
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new LanguagesDropDownListWidgetDefinition((ConfigElement) this);
  }
}
