// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.PropertyCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Represents a collection of <see cref="T:Telerik.Sitefinity.Localization.ResourceProperty" /> objects.
  /// </summary>
  public class PropertyCollection : KeyedCollection<string, ResourceProperty>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.PropertyCollection" /> class that uses case insensitive equality comparer.
    /// </summary>
    public PropertyCollection()
      : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
    }

    /// <summary>
    /// When implemented in a derived class, extracts the key from the specified element.
    /// </summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override string GetKeyForItem(ResourceProperty item) => item.Key;
  }
}
