// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Definitions.LanguagesColumnMarkupGeneratorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Contracts;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Localization.Definitions
{
  /// <summary>The definition class for LanguagesColumn</summary>
  public class LanguagesColumnMarkupGeneratorDefinition : 
    DynamicMarkupGeneratorDefinition,
    ILanguagesColumnMarkupGeneratorDefinition,
    IDynamicMarkupGeneratorDefinition,
    IDefinition
  {
    private LanguageSource languageSource;
    private IList<string> availableLanguages;
    private int itemsInGroupCount;
    private string containerTag;
    private string groupTag;
    private string itemTag;
    private string containerClass;
    private string groupClass;
    private string itemClass;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Definitions.LanguagesColumnMarkupGeneratorDefinition" /> class.
    /// </summary>
    public LanguagesColumnMarkupGeneratorDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Definitions.LanguagesColumnMarkupGeneratorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public LanguagesColumnMarkupGeneratorDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public LanguagesColumnMarkupGeneratorDefinition GetDefinition() => this;

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
    public IList<string> AvailableLanguages
    {
      get => this.ResolveProperty<IList<string>>(nameof (AvailableLanguages), this.availableLanguages);
      set => this.availableLanguages = value;
    }

    /// <summary>Gets or sets the number of items in a group.</summary>
    public int ItemsInGroupCount
    {
      get => this.ResolveProperty<int>(nameof (ItemsInGroupCount), this.itemsInGroupCount);
      set => this.itemsInGroupCount = value;
    }

    /// <summary>Gets or sets the tag of the container element.</summary>
    public string ContainerTag
    {
      get => this.ResolveProperty<string>(nameof (ContainerTag), this.containerTag);
      set => this.containerTag = value;
    }

    /// <summary>Gets or sets the tag of the group element.</summary>
    public string GroupTag
    {
      get => this.ResolveProperty<string>(nameof (GroupTag), this.groupTag);
      set => this.groupTag = value;
    }

    /// <summary>Gets or sets the tag of the item element.</summary>
    public string ItemTag
    {
      get => this.ResolveProperty<string>(nameof (ItemTag), this.itemTag);
      set => this.itemTag = value;
    }

    /// <summary>Gets or sets the css class of the container element.</summary>
    public string ContainerClass
    {
      get => this.ResolveProperty<string>(nameof (ContainerClass), this.containerClass);
      set => this.containerClass = value;
    }

    /// <summary>Gets or sets the css class of the group element.</summary>
    public string GroupClass
    {
      get => this.ResolveProperty<string>(nameof (GroupClass), this.groupClass);
      set => this.groupClass = value;
    }

    /// <summary>Gets or sets the css class of the item element.</summary>
    public string ItemClass
    {
      get => this.ResolveProperty<string>(nameof (ItemClass), this.itemClass);
      set => this.itemClass = value;
    }
  }
}
