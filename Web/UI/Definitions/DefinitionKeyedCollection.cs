// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.DefinitionKeyedCollection`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  /// <summary>
  /// This abstract class represents a keyed collection of definition objects (objects that implement <see cref="!:IDefiniton" /> interface).
  /// </summary>
  /// <typeparam name="TKey">Type of the key of the definition.</typeparam>
  /// <typeparam name="TValue">Type of the value (definition).</typeparam>
  public abstract class DefinitionKeyedCollection<TKey, TValue> : KeyedCollection<TKey, TValue>
    where TValue : IDefinition
  {
    public DefinitionKeyedCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Definitions.DefinitionKeyedCollection`2" /> class.
    /// </summary>
    /// <param name="configDictionary">The config dictionary.</param>
    public DefinitionKeyedCollection(IEnumerable<TValue> configDictionary)
    {
      foreach (TValue config in configDictionary)
        this.Add(config);
    }
  }
}
