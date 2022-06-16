// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.CacheProfileFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes a cache profile field.
  /// </summary>
  public class CacheProfileFieldElement : 
    CompositeFieldElement,
    ICacheProfileFieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private const string ProfileChoiceFieldDefinitionKey = "profileChoiceFieldDefinition";
    private const string IsOutputCacheKey = "isOutputCache";
    private const string CacheSettingsLocationKey = "cacheSettingsLocation";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.CacheProfileFieldElement" /> class with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public CacheProfileFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.CacheProfileFieldElement" /> class.
    /// </summary>
    internal CacheProfileFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new CacheProfileFieldDefinition((ConfigElement) this);

    /// <summary>Gets the cache profile choice field definition.</summary>
    /// <value>The cache profile choice field definition.</value>
    [ConfigurationProperty("profileChoiceFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProfileChoiceFieldDefinitionDescription", Title = "ProfileChoiceFieldDefinitionCaption")]
    public ChoiceFieldElement ProfileChoiceFieldDefinition
    {
      get => (ChoiceFieldElement) this["profileChoiceFieldDefinition"];
      set => this["profileChoiceFieldDefinition"] = (object) value;
    }

    IChoiceFieldDefinition ICacheProfileFieldDefinition.ProfileChoiceFieldDefinition => (IChoiceFieldDefinition) this.ProfileChoiceFieldDefinition;

    /// <summary>
    /// Gets a value indicating whether this instance of field definition is output cache.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is output cache; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("isOutputCache", DefaultValue = "true")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsOutputCacheDescription", Title = "IsOutputCacheCaption")]
    public bool IsOutputCache
    {
      get => (bool) this["isOutputCache"];
      set => this["isOutputCache"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the cache settings location in the administration.
    /// </summary>
    /// <example>
    /// "Administration &gt; Settings &gt; Advanced settings &gt; Caching"
    /// </example>
    /// <value>The cache settings location.</value>
    [ConfigurationProperty("cacheSettingsLocation", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheSettingsLocationDescription", Title = "CacheSettingsLocationCaption")]
    public string CacheSettingsLocation
    {
      get => (string) this["cacheSettingsLocation"];
      set => this["cacheSettingsLocation"] = (object) value;
    }
  }
}
