// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.LanguageChoiceFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A class that provides all information that is needed to construct a LanguageChoiceField control.
  /// </summary>
  public class LanguageChoiceFieldDefinition : 
    ChoiceFieldDefinition,
    ILanguageChoiceFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private Telerik.Sitefinity.Localization.Web.UI.LanguageSource? languageSource;
    private bool? hideIfSingleLanguage;
    private LanguagesSelection? languagesToShow;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ChoiceFieldDefinition" /> class.
    /// </summary>
    public LanguageChoiceFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ChoiceFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public LanguageChoiceFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the languages source.</summary>
    /// <value>The languages source.</value>
    public Telerik.Sitefinity.Localization.Web.UI.LanguageSource? LanguageSource
    {
      get => this.ResolveProperty<Telerik.Sitefinity.Localization.Web.UI.LanguageSource?>(nameof (LanguageSource), this.languageSource, new Telerik.Sitefinity.Localization.Web.UI.LanguageSource?(Telerik.Sitefinity.Localization.Web.UI.LanguageSource.Frontend));
      set => this.languageSource = value;
    }

    /// <summary>
    /// Gets or sets whether to hide the field if only a single language is found.
    /// </summary>
    /// <value>True if the field must be hidden when only a single language is found.</value>
    public bool? HideIfSingleLanguage
    {
      get => this.ResolveProperty<bool?>(nameof (HideIfSingleLanguage), this.hideIfSingleLanguage, new bool?(false));
      set => this.hideIfSingleLanguage = value;
    }

    /// <summary>Gets or sets the languages to show.</summary>
    /// <value>The languages to show.</value>
    public LanguagesSelection? LanguagesToShow
    {
      get => this.ResolveProperty<LanguagesSelection?>(nameof (LanguagesToShow), this.languagesToShow, new LanguagesSelection?(LanguagesSelection.Unavailable));
      set => this.languagesToShow = value;
    }
  }
}
