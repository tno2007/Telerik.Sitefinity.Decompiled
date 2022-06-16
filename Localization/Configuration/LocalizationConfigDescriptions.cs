// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Configuration.LocalizationConfigDescriptions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization.Configuration
{
  /// <summary>
  /// Represents the string resources for the localization backend configurations.
  /// </summary>
  public class LocalizationConfigDescriptions : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Configuration.LocalizationConfigDescriptions" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public LocalizationConfigDescriptions()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Configuration.LocalizationConfigDescriptions" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public LocalizationConfigDescriptions(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Configuration Descriptions</summary>
    [ResourceEntry("ConfigDescriptionsTitle", Description = "The title of this class.", LastModified = "2009/04/30", Value = "Configuration Descriptions")]
    public string ConfigDescriptionsTitle => this[nameof (ConfigDescriptionsTitle)];

    /// <summary>
    /// Contains localizable resources for Sitefinity configuration descriptions.
    /// </summary>
    [ResourceEntry("ConfigDescriptionsDescription", Description = "The description of this class.", LastModified = "2009/04/30", Value = "Contains localizable resources for Sitefinity configuration descriptions.")]
    public string ConfigDescriptionsDescription => this[nameof (ConfigDescriptionsDescription)];

    /// <summary>phrase: Source for available languages</summary>
    [ResourceEntry("LanguageSourceCaption", Description = "phrase: Source for available languages", LastModified = "10/13/2010", Value = "Source for available languages")]
    public string LanguageSourceCaption => this[nameof (LanguageSourceCaption)];

    /// <summary>phrase: Defines the source for available languages.</summary>
    [ResourceEntry("LanguageSourceDescription", Description = "phrase: Defines the source for available languages.", LastModified = "10/13/2010", Value = "Defines the source for available languages.")]
    public string LanguageSourceDescription => this[nameof (LanguageSourceDescription)];

    /// <summary>phrase: Hide if single language</summary>
    [ResourceEntry("HideIfSingleLanguageCaption", Description = "phrase: Hide if single language", LastModified = "04/07/2011", Value = "Hide if single language")]
    public string HideIfSingleLanguageCaption => this[nameof (HideIfSingleLanguageCaption)];

    /// <summary>
    /// phrase: Defines whether to hide the field if only a single language is found.
    /// </summary>
    [ResourceEntry("HideIfSingleLanguageDescription", Description = "phrase: Defines whether to hide the field if only a single language is found.", LastModified = "04/07/2011", Value = "Defines whether to hide the field if only a single language is found.")]
    public string HideIfSingleLanguageDescription => this[nameof (HideIfSingleLanguageDescription)];

    /// <summary>phrase: List of all cultures</summary>
    [ResourceEntry("AvailableLanguagesCaption", Description = "phrase: List of all cultures", LastModified = "10/13/2010", Value = "List of all cultures")]
    public string AvailableLanguagesCaption => this[nameof (AvailableLanguagesCaption)];

    /// <summary>
    /// phrase: Defines the list of all listed cultures. This is only used if LanguageSource is set to Custom.
    /// </summary>
    [ResourceEntry("AvailableLanguagesDescription", Description = "phrase: Defines the list of all listed cultures. This is only used if LanguageSource is set to Custom.", LastModified = "10/13/2010", Value = "Defines the list of all listed cultures. This is only used if LanguageSource is set to Custom.")]
    public string AvailableLanguagesDescription => this[nameof (AvailableLanguagesDescription)];

    /// <summary>phrase: Number of items in a group</summary>
    [ResourceEntry("ItemsInGroupCountCaption", Description = "phrase: Number of items in a group", LastModified = "10/13/2010", Value = "Number of items in a group")]
    public string ItemsInGroupCountCaption => this[nameof (ItemsInGroupCountCaption)];

    /// <summary>phrase: Defines the number of items in a group.</summary>
    [ResourceEntry("ItemsInGroupCountDescription", Description = "phrase: Defines the number of items in a group.", LastModified = "10/13/2010", Value = "Defines the number of items in a group.")]
    public string ItemsInGroupCountDescription => this[nameof (ItemsInGroupCountDescription)];

    /// <summary>phrase: Container tag</summary>
    [ResourceEntry("ContainerTagCaption", Description = "phrase: Container tag", LastModified = "10/13/2010", Value = "Container tag")]
    public string ContainerTagCaption => this[nameof (ContainerTagCaption)];

    /// <summary>phrase: Defines the tag of the container element.</summary>
    [ResourceEntry("ContainerTagDescription", Description = "phrase: Defines the tag of the container element.", LastModified = "10/13/2010", Value = "Defines the tag of the container element.")]
    public string ContainerTagDescription => this[nameof (ContainerTagDescription)];

    /// <summary>phrase: Group tag</summary>
    [ResourceEntry("GroupTagCaption", Description = "phrase: Group tag", LastModified = "10/13/2010", Value = "Group tag")]
    public string GroupTagCaption => this[nameof (GroupTagCaption)];

    /// <summary>phrase: Defines the tag of the group element.</summary>
    [ResourceEntry("GroupTagDescription", Description = "phrase: Defines the tag of the group element.", LastModified = "10/13/2010", Value = "Defines the tag of the group element.")]
    public string GroupTagDescription => this[nameof (GroupTagDescription)];

    /// <summary>phrase: Item tag</summary>
    [ResourceEntry("ItemTagCaption", Description = "phrase: Item tag", LastModified = "10/13/2010", Value = "Item tag")]
    public string ItemTagCaption => this[nameof (ItemTagCaption)];

    /// <summary>phrase: Defines the tag of the item element.</summary>
    [ResourceEntry("ItemTagDescription", Description = "phrase: Defines the tag of the item element.", LastModified = "10/13/2010", Value = "Defines the tag of the item element.")]
    public string ItemTagDescription => this[nameof (ItemTagDescription)];

    /// <summary>phrase: Container CSS class</summary>
    [ResourceEntry("ContainerClassCaption", Description = "phrase: Container CSS class", LastModified = "2012/01/05", Value = "Container CSS class")]
    public string ContainerClassCaption => this[nameof (ContainerClassCaption)];

    /// <summary>
    /// phrase: Defines the CSS class of the container element.
    /// </summary>
    [ResourceEntry("ContainerClassDescription", Description = "phrase: Defines the CSS class of the container element.", LastModified = "2012/01/05", Value = "Defines the CSS class of the container element.")]
    public string ContainerClassDescription => this[nameof (ContainerClassDescription)];

    /// <summary>phrase: Group css class</summary>
    [ResourceEntry("GroupClassCaption", Description = "phrase: Group css class", LastModified = "10/13/2010", Value = "Group css class")]
    public string GroupClassCaption => this[nameof (GroupClassCaption)];

    /// <summary>phrase: Defines the CSS class of the group element.</summary>
    [ResourceEntry("GroupClassDescription", Description = "phrase: Defines the CSS class of the group element.", LastModified = "2012/01/05", Value = "Defines the CSS class of the group element.")]
    public string GroupClassDescription => this[nameof (GroupClassDescription)];

    /// <summary>phrase: Item CSS class</summary>
    [ResourceEntry("ItemClassCaption", Description = "phrase: Item CSS class", LastModified = "2012/01/05", Value = "Item CSS class")]
    public string ItemClassCaption => this[nameof (ItemClassCaption)];

    /// <summary>phrase: Defines the CSS class of the item element.</summary>
    [ResourceEntry("ItemClassDescription", Description = "phrase: Defines the CSS class of the item element.", LastModified = "2012/01/05", Value = "Defines the CSS class of the item element.")]
    public string ItemClassDescription => this[nameof (ItemClassDescription)];

    /// <summary>phrase: Option for all languages</summary>
    [ResourceEntry("AddAllLanguagesOptionCaption", Description = "phrase: Option for all languages", LastModified = "10/14/2010", Value = "Option for all languages")]
    public string AddAllLanguagesOptionCaption => this[nameof (AddAllLanguagesOptionCaption)];

    /// <summary>
    /// phrase: Defines whether an option for all languages should be added.
    /// </summary>
    [ResourceEntry("AddAllLanguagesOptionDescription", Description = "phrase: Defines whether an option for all languages should be added.", LastModified = "10/14/2010", Value = "Defines whether an option for all languages should be added.")]
    public string AddAllLanguagesOptionDescription => this[nameof (AddAllLanguagesOptionDescription)];

    /// <summary>phrase: Command name</summary>
    [ResourceEntry("CommandNameCaption", Description = "phrase: Command name", LastModified = "2010/01/23", Value = "Command name")]
    public string CommandNameCaption => this[nameof (CommandNameCaption)];

    /// <summary>phrase: Name of the command that widget fires.</summary>
    [ResourceEntry("CommandNameDescription", Description = "phrase: Name of the command that widget fires.", LastModified = "2010/01/23", Value = "Name of the command that widget fires.")]
    public string CommandNameDescription => this[nameof (CommandNameDescription)];

    /// <summary>phrase: Languages to show</summary>
    [ResourceEntry("LanguagesToShowCaption", Description = "phrase: Languages to show", LastModified = "2011/05/19", Value = "Languages to show")]
    public string LanguagesToShowCaption => this[nameof (LanguagesToShowCaption)];

    /// <summary>
    /// phrase: The languages that are displayed by the widget.
    /// </summary>
    [ResourceEntry("LanguagesToShowDescription", Description = "phrase: The languages that are displayed by the widget.", LastModified = "2011/05/19", Value = "The languages that are displayed by the widget.")]
    public string LanguagesToShowDescription => this[nameof (LanguagesToShowDescription)];
  }
}
