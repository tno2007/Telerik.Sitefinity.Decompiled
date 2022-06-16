// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DefinitionCollection`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// This class represents a collection of definition objects (objects that implement <see cref="!:IDefiniton" /> interface).
  /// </summary>
  /// <typeparam name="TDefinition">
  /// Type of definition that the collection is made of.
  /// </typeparam>
  /// <remarks>
  /// The functionality of this collection is in the fact that it will automatically set the definition configuration element (if
  /// available) to the items of the collection.
  /// </remarks>
  public class DefinitionCollection<TDefinition> : List<TDefinition> where TDefinition : IDefinition
  {
    private ConfigElement configCollection;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.DefinitionCollection`1" /> class.
    /// </summary>
    /// <param name="configCollection">The config collection.</param>
    public DefinitionCollection(ConfigElement configCollection) => this.configCollection = configCollection;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Definitions.DefinitionKeyedCollection`2" /> class.
    /// </summary>
    /// <param name="configDictionary">The config dictionary.</param>
    public DefinitionCollection(IEnumerable<TDefinition> configDictionary)
    {
      foreach (TDefinition config in configDictionary)
        this.Add(config);
    }

    protected internal ConfigElement GetConfigDefinition(int definitionIndex)
    {
      if (!(this.configCollection is ConfigElementCollection configCollection))
        return (ConfigElement) null;
      if (definitionIndex >= configCollection.Count)
        return (ConfigElement) null;
      for (int index = 0; index < configCollection.Count; ++index)
      {
        if (index == definitionIndex)
          return (ConfigElement) configCollection;
      }
      return (ConfigElement) null;
    }
  }
}
