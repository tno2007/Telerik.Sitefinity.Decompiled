// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.CacheSettingsFieldElement
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
  /// Providers the configuration element for CacheSettingsFieldControl
  /// </summary>
  public class CacheSettingsFieldElement : 
    CompositeFieldElement,
    ICacheSettingsFieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.CacheSettingsFieldElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public CacheSettingsFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal CacheSettingsFieldElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new CacheSettingsFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the definition for the child TextField control containing cache duration
    /// </summary>
    /// <value>The text field with cache duration definition.</value>
    [ConfigurationProperty("cacheDurationTextFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheDurationTextFieldDefinitionDescription", Title = "CacheDurationTextFieldDefinitionCaption")]
    public TextFieldDefinitionElement CacheDurationTextFieldDefinition
    {
      get => (TextFieldDefinitionElement) this["cacheDurationTextFieldDefinition"];
      set => this["cacheDurationTextFieldDefinition"] = (object) value;
    }

    ITextFieldDefinition ICacheSettingsFieldDefinition.CacheDurationTextFieldDefinition => (ITextFieldDefinition) this.CacheDurationTextFieldDefinition;

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control indicating if sliding expiration is enabled
    /// </summary>
    /// <value>The choice field with value if [sliding expiration] is enabled.</value>
    [ConfigurationProperty("slidingExpirationChoiceFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SlidingExpirationChoiceFieldDefinitionDescription", Title = "SlidingExpirationChoiceFieldDefinitionCaption")]
    public ChoiceFieldElement SlidingExpirationChoiceFieldDefinition
    {
      get => (ChoiceFieldElement) this["slidingExpirationChoiceFieldDefinition"];
      set => this["slidingExpirationChoiceFieldDefinition"] = (object) value;
    }

    IChoiceFieldDefinition ICacheSettingsFieldDefinition.SlidingExpirationChoiceFieldDefinition => (IChoiceFieldDefinition) this.SlidingExpirationChoiceFieldDefinition;

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control indicating if caching is enabled
    /// </summary>
    /// <value>The choice field with value if [caching] is enabled.</value>
    [ConfigurationProperty("enableCachingFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableCachingFieldDefinitionDescription", Title = "EnableCachingFieldDefinitionCaption")]
    public ChoiceFieldElement EnableCachingFieldDefinition
    {
      get => (ChoiceFieldElement) this["enableCachingFieldDefinition"];
      set => this["enableCachingFieldDefinition"] = (object) value;
    }

    IChoiceFieldDefinition ICacheSettingsFieldDefinition.EnableCachingFieldDefinition => (IChoiceFieldDefinition) this.EnableCachingFieldDefinition;

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control indicating if "use default" is enabled
    /// </summary>
    /// <value>The choice field with value if "use default" is enabled.</value>
    [ConfigurationProperty("useDefaultSettingsForCachingFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UseDefaultSettingsForCachingFieldDefinitionDescription", Title = "UseDefaultSettingsForCachingFieldDefinitionCaption")]
    public ChoiceFieldElement UseDefaultSettingsForCachingFieldDefinition
    {
      get => (ChoiceFieldElement) this["useDefaultSettingsForCachingFieldDefinition"];
      set => this["useDefaultSettingsForCachingFieldDefinition"] = (object) value;
    }

    IChoiceFieldDefinition ICacheSettingsFieldDefinition.UseDefaultSettingsForCachingFieldDefinition => (IChoiceFieldDefinition) this.UseDefaultSettingsForCachingFieldDefinition;

    [ConfigurationProperty("isOutputCache", DefaultValue = "true")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsOutputCacheFieldDefinitionDescription", Title = "IsOutputCacheFieldDefinitionCaption")]
    public bool IsOutputCache
    {
      get => (bool) this["isOutputCache"];
      set => this["isOutputCache"] = (object) value;
    }
  }
}
