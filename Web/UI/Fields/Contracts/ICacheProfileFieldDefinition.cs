// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.ICacheProfileFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// Defines a contract for CacheProfileField definition and configuration element
  /// </summary>
  public interface ICacheProfileFieldDefinition : 
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>Gets the cache profile choice field definition.</summary>
    /// <value>The cache profile choice field definition.</value>
    IChoiceFieldDefinition ProfileChoiceFieldDefinition { get; }

    /// <summary>
    /// Gets a value indicating whether this instance of field definition is output cache.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is output cache; otherwise, <c>false</c>.
    /// </value>
    bool IsOutputCache { get; set; }

    /// <summary>
    /// Gets or sets the cache settings location in the administration.
    /// </summary>
    /// <example>
    /// "Administration &gt; Settings &gt; Advanced settings &gt; Caching"
    /// </example>
    /// <value>The cache settings location.</value>
    string CacheSettingsLocation { get; set; }
  }
}
