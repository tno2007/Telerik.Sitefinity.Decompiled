// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.LocalizationResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Localizable strings for the Localization</summary>
  [ObjectInfo(typeof (LocalizationResources), Description = "LocalizationResourcesDescription", Title = "LocalizationResourcesTitle")]
  public class LocalizationResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.LocalizationResources" /> class.
    /// </summary>
    public LocalizationResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.LocalizationResources" /> class with the given ResourceDataProvider
    /// </summary>
    /// <param name="provider">The provider.</param>
    public LocalizationResources(ResourceDataProvider provider)
      : base(provider)
    {
    }

    /// <summary>Localization resources</summary>
    [ResourceEntry("LocalizationResourcesTitle", Description = "The title of this class.", LastModified = "2010/10/14", Value = "Localization resources")]
    public string LocalizationResourcesTitle => this[nameof (LocalizationResourcesTitle)];

    /// <summary>
    /// Contains localizable resources for help information such as UI elements descriptions, usage explanations, FAQ and etc.
    /// </summary>
    [ResourceEntry("LocalizationResourcesDescription", Description = "The description of this class.", LastModified = "2010/10/14", Value = "Contains localizable resources for localization user interface.")]
    public string LocalizationResourcesDescription => this[nameof (LocalizationResourcesDescription)];

    /// <summary>word: Languages</summary>
    [ResourceEntry("Languages", Description = "word: Languages", LastModified = "2010/10/14", Value = "Languages")]
    public string Languages => this[nameof (Languages)];

    /// <summary>word: Language</summary>
    [ResourceEntry("Language", Description = "word: Language", LastModified = "2010/10/25", Value = "Language")]
    public string Language => this[nameof (Language)];

    /// <summary>word: Translations</summary>
    [ResourceEntry("Translations", Description = "word: Translations", LastModified = "2010/10/14", Value = "Translations")]
    public string Translations => this[nameof (Translations)];

    /// <summary>phrase: Other translations:</summary>
    [ResourceEntry("OtherTranslationsColon", Description = "phrase: Other translations:", LastModified = "2010/11/03", Value = "Other translations:")]
    public string OtherTranslationsColon => this[nameof (OtherTranslationsColon)];

    /// <summary>phrase: Show all translations</summary>
    [ResourceEntry("ShowAllTranslations", Description = "phrase: Show all translations", LastModified = "2010/10/14", Value = "Show all translations")]
    public string ShowAllTranslations => this[nameof (ShowAllTranslations)];

    /// <summary>phrase: Show basic translations only</summary>
    [ResourceEntry("ShowBasicTranslationsOnly", Description = "phrase: Show basic translations only", LastModified = "2010/10/14", Value = "Show basic translations only")]
    public string ShowBasicTranslationsOnly => this[nameof (ShowBasicTranslationsOnly)];

    /// <summary>phrase: More translations...</summary>
    [ResourceEntry("MoreTranslations", Description = "phrase: More translations...", LastModified = "2010/10/14", Value = "More translations&hellip;")]
    public string MoreTranslations => this[nameof (MoreTranslations)];

    /// <summary>phrase: Basic translations only...</summary>
    [ResourceEntry("BasicTranslationsOnly", Description = "phrase: Basic trasnlations only...", LastModified = "2010/10/14", Value = "Basic translations only&hellip;")]
    public string BasicTranslationsOnly => this[nameof (BasicTranslationsOnly)];

    /// <summary>phrase: not translated</summary>
    [ResourceEntry("NotTranslated", Description = "phrase: not translated", LastModified = "2010/11/05", Value = "not translated")]
    public string NotTranslated => this[nameof (NotTranslated)];

    /// <summary>All languages</summary>
    /// <value>All languages</value>
    [ResourceEntry("AllLanguages", Description = "All languages", LastModified = "2015/02/19", Value = "All languages")]
    public string AllLanguages => this[nameof (AllLanguages)];

    /// <summary>phrase: Select language</summary>
    /// <value>Select language</value>
    [ResourceEntry("SelectLanguage", Description = "phrase: Select language", LastModified = "2015/02/19", Value = "Select language")]
    public string SelectLanguage => this[nameof (SelectLanguage)];

    /// <summary>phrase: Selected languages</summary>
    /// <value>Selected languages</value>
    [ResourceEntry("SelectedLanuages", Description = "phrase: Selected languages", LastModified = "2015/02/20", Value = "Selected languages")]
    public string SelectedLanuages => this[nameof (SelectedLanuages)];
  }
}
