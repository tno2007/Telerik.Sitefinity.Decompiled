// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Definitions.LanguagesDropDownListWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Contracts;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Localization.Definitions
{
  /// <summary>The definition for CulturesListWidget</summary>
  public class LanguagesDropDownListWidgetDefinition : 
    WidgetDefinition,
    ILanguagesDropDownListWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private LanguageSource languageSource;
    private List<CultureInfo> availableCultures;
    private bool addAllLanguagesOption;
    private string commandName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Definitions.LanguagesDropDownListWidgetDefinition" /> class.
    /// </summary>
    public LanguagesDropDownListWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Definitions.LanguagesDropDownListWidgetDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public LanguagesDropDownListWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public LanguagesDropDownListWidgetDefinition GetDefinition() => this;

    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    public LanguageSource LanguageSource
    {
      get => this.ResolveProperty<LanguageSource>(nameof (LanguageSource), this.languageSource);
      set => this.languageSource = value;
    }

    /// <summary>
    /// Gets or sets the list of all listed cultures. This is only used if LanguageSource is set to Custom.
    /// </summary>
    /// <value>All available languages.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<CultureInfo> AvailableCultures
    {
      get => this.ResolveProperty<List<CultureInfo>>(nameof (AvailableCultures), this.availableCultures);
      set => this.availableCultures = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether an option for all languages should be added.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if an option for all languages is to be added; otherwise, <c>false</c>.
    /// </value>
    public bool AddAllLanguagesOption
    {
      get => this.ResolveProperty<bool>(nameof (AddAllLanguagesOption), this.addAllLanguagesOption);
      set => this.addAllLanguagesOption = value;
    }

    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    public string CommandName
    {
      get => this.ResolveProperty<string>(nameof (CommandName), this.commandName);
      set => this.commandName = value;
    }
  }
}
