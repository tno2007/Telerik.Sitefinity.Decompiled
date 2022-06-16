// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.ICacheSettingsFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// Defines a contract for CacheSettingsFieldControl definition and configuration element
  /// </summary>
  public interface ICacheSettingsFieldDefinition : 
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>Gets the cache duration</summary>
    /// <value>The cache duration text field definition.</value>
    ITextFieldDefinition CacheDurationTextFieldDefinition { get; }

    /// <summary>Gets the sliding expiration flag</summary>
    /// <value>The sliding expiration choice field definition.</value>
    IChoiceFieldDefinition SlidingExpirationChoiceFieldDefinition { get; }

    /// <summary>Gets the enable caching flag</summary>
    /// <value>The enable caching choice field definition.</value>
    IChoiceFieldDefinition EnableCachingFieldDefinition { get; }

    /// <summary>Gets the use default flag</summary>
    /// <value>The use default choice field definition.</value>
    IChoiceFieldDefinition UseDefaultSettingsForCachingFieldDefinition { get; }

    /// <summary>
    /// Gets a value indicating whether this instance of field definition is output cache.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is output cache; otherwise, <c>false</c>.
    /// </value>
    bool IsOutputCache { get; }
  }
}
