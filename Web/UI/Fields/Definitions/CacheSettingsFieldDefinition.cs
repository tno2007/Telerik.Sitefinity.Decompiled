// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.CacheSettingsFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// Contains all properties needed to construct an instance of the CacheSettingsFieldDefinition control
  /// </summary>
  public class CacheSettingsFieldDefinition : 
    CompositeFieldDefinition,
    ICacheSettingsFieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private bool isOutputCache;
    private ITextFieldDefinition cacheDurationTextFieldDefinition;
    private IChoiceFieldDefinition slidingExpirationChoiceFieldDefinition;
    private IChoiceFieldDefinition enableCachingFieldDefinition;
    private IChoiceFieldDefinition useDefaultSettingsForCachingFieldDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.CacheSettingsFieldDefinition" /> class.
    /// </summary>
    public CacheSettingsFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.CacheSettingsFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public CacheSettingsFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets or sets the definition for the child TextField control containing cache duration
    /// </summary>
    /// <value>The text field with cache duration definition.</value>
    public ITextFieldDefinition CacheDurationTextFieldDefinition
    {
      get => this.ResolveProperty<ITextFieldDefinition>(nameof (CacheDurationTextFieldDefinition), this.cacheDurationTextFieldDefinition);
      set => this.cacheDurationTextFieldDefinition = value;
    }

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control indicating if sliding expiration is enabled
    /// </summary>
    /// <value>The choice field with value if [sliding expiration] is enabled.</value>
    public IChoiceFieldDefinition SlidingExpirationChoiceFieldDefinition
    {
      get => this.ResolveProperty<IChoiceFieldDefinition>(nameof (SlidingExpirationChoiceFieldDefinition), this.slidingExpirationChoiceFieldDefinition);
      set => this.slidingExpirationChoiceFieldDefinition = value;
    }

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control indicating if "use default" is enabled
    /// </summary>
    /// <value>The choice field with value if "use default" is enabled.</value>
    public IChoiceFieldDefinition UseDefaultSettingsForCachingFieldDefinition
    {
      get => this.ResolveProperty<IChoiceFieldDefinition>(nameof (UseDefaultSettingsForCachingFieldDefinition), this.useDefaultSettingsForCachingFieldDefinition);
      set => this.useDefaultSettingsForCachingFieldDefinition = value;
    }

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control indicating if caching is enabled
    /// </summary>
    /// <value>The choice field with value if [caching] is enabled.</value>
    public IChoiceFieldDefinition EnableCachingFieldDefinition
    {
      get => this.ResolveProperty<IChoiceFieldDefinition>(nameof (EnableCachingFieldDefinition), this.enableCachingFieldDefinition);
      set => this.enableCachingFieldDefinition = value;
    }

    /// <summary>
    /// Gets a value indicating whether this instance of field definition is output cache.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is output cache; otherwise, <c>false</c>.
    /// </value>
    public bool IsOutputCache
    {
      get => this.ResolveProperty<bool>(nameof (IsOutputCache), this.isOutputCache);
      set => this.isOutputCache = value;
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (CacheSettingsFieldControl);
  }
}
