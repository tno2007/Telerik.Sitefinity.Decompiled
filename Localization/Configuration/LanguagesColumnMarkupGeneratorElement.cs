// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Configuration.LanguagesColumnMarkupGeneratorElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Contracts;
using Telerik.Sitefinity.Localization.Definitions;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Localization.Configuration
{
  /// <summary>
  /// Configuration element for LanguagesColumnMarkupGenerator
  /// </summary>
  public class LanguagesColumnMarkupGeneratorElement : 
    DynamicMarkupGeneratorElement,
    ILanguagesColumnMarkupGeneratorDefinition,
    IDynamicMarkupGeneratorDefinition,
    IDefinition
  {
    private const string LanguageSourcePropertyName = "languageSource";
    private const string AvailableLanguagesPropertyName = "availableLanguages";
    private const string ItemsInGroupCountPropertyName = "itemsInGroupCount";
    private const string ContainerTagPropertyName = "containerTag";
    private const string GroupTagPropertyName = "groupTag";
    private const string ItemTagPropertyName = "itemTag";
    private const string ContainerClassPropertyName = "containerClass";
    private const string GroupClassPropertyName = "groupClass";
    private const string ItemClassPropertyName = "itemClass";

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public LanguagesColumnMarkupGeneratorElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new LanguagesColumnMarkupGeneratorDefinition((ConfigElement) this);

    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    [ConfigurationProperty("languageSource")]
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
    [TypeConverter(typeof (StringListConverter))]
    [ConfigurationProperty("availableLanguages")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "AvailableLanguagesDescription", Title = "AvailableLanguagesCaption")]
    public IList<string> AvailableLanguages
    {
      get => (IList<string>) this["availableLanguages"];
      set => this["availableLanguages"] = (object) value;
    }

    /// <summary>Gets or sets the number of items in a group.</summary>
    [ConfigurationProperty("itemsInGroupCount")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "ItemsInGroupCountDescription", Title = "ItemsInGroupCountCaption")]
    public int ItemsInGroupCount
    {
      get => (int) this["itemsInGroupCount"];
      set => this["itemsInGroupCount"] = (object) value;
    }

    /// <summary>Gets or sets the tag of the container element.</summary>
    [ConfigurationProperty("containerTag", DefaultValue = "div")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "ContainerTagDescription", Title = "ContainerTagCaption")]
    public string ContainerTag
    {
      get => (string) this["containerTag"];
      set => this["containerTag"] = (object) value;
    }

    /// <summary>Gets or sets the tag of the group element.</summary>
    [ConfigurationProperty("groupTag", DefaultValue = "ul")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "GroupTagDescription", Title = "GroupTagCaption")]
    public string GroupTag
    {
      get => (string) this["groupTag"];
      set => this["groupTag"] = (object) value;
    }

    /// <summary>Gets or sets the tag of the item element.</summary>
    [ConfigurationProperty("itemTag", DefaultValue = "li")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "ItemTagDescription", Title = "ItemTagCaption")]
    public string ItemTag
    {
      get => (string) this["itemTag"];
      set => this["itemTag"] = (object) value;
    }

    /// <summary>Gets or sets the css class of the container element.</summary>
    [ConfigurationProperty("containerClass")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "ContainerClassDescription", Title = "ContainerClassCaption")]
    public string ContainerClass
    {
      get => (string) this["containerClass"];
      set => this["containerClass"] = (object) value;
    }

    /// <summary>Gets or sets the css class of the group element.</summary>
    [ConfigurationProperty("groupClass")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "GroupClassDescription", Title = "GroupClassCaption")]
    public string GroupClass
    {
      get => (string) this["groupClass"];
      set => this["groupClass"] = (object) value;
    }

    /// <summary>Gets or sets the css class of the item element.</summary>
    [ConfigurationProperty("itemClass")]
    [ObjectInfo(typeof (LocalizationConfigDescriptions), Description = "ItemClassDescription", Title = "ItemClassCaption")]
    public string ItemClass
    {
      get => (string) this["itemClass"];
      set => this["itemClass"] = (object) value;
    }
  }
}
