// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.CacheProfileFieldDefinition
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
  /// Contains all properties needed to construct an instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.CacheProfileField" /> control.
  /// </summary>
  public class CacheProfileFieldDefinition : 
    CompositeFieldDefinition,
    ICacheProfileFieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private bool isOutputCache;
    private IChoiceFieldDefinition profileChoiceFieldDefinition;
    private string cacheSettingsLocation;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.CacheProfileFieldDefinition" /> class.
    /// </summary>
    public CacheProfileFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.CacheProfileFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The configuration definition.</param>
    public CacheProfileFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (CacheProfileField);

    /// <summary>Gets the cache profile choice field definition.</summary>
    /// <value>The cache profile choice field definition.</value>
    public IChoiceFieldDefinition ProfileChoiceFieldDefinition
    {
      get => this.ResolveProperty<IChoiceFieldDefinition>(nameof (ProfileChoiceFieldDefinition), this.profileChoiceFieldDefinition);
      set => this.profileChoiceFieldDefinition = value;
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

    /// <summary>
    /// Gets or sets the cache settings location in the administration.
    /// </summary>
    /// <example>
    /// "Administration &gt; Settings &gt; Advanced settings &gt; Caching"
    /// </example>
    /// <value>The cache settings location.</value>
    public string CacheSettingsLocation
    {
      get => this.ResolveProperty<string>(nameof (CacheSettingsLocation), this.cacheSettingsLocation);
      set => this.cacheSettingsLocation = value;
    }
  }
}
