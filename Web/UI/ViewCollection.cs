// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ViewCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents a collection of <see cref="T:Telerik.Sitefinity.Web.UI.IViewInfo" /> objects.
  /// </summary>
  public class ViewCollection : KeyedCollection<string, IViewInfo>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the default equality comparer.
    /// Default initialiation with ignorecase bacase the url-s should be case insensitive
    /// </summary>
    public ViewCollection()
      : base((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer.
    /// </summary>
    /// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or null to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.
    /// </param>
    public ViewCollection(IEqualityComparer<string> comparer)
      : base(comparer)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> class that uses the specified equality comparer and creates a lookup dictionary when the specified threshold is exceeded.
    /// </summary>
    /// <param name="comparer">The implementation of the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface to use when comparing keys, or null to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.
    /// </param>
    /// <param name="dictionaryCreationThreshold">The number of elements the collection can hold without creating a lookup dictionary (0 creates the lookup dictionary when the first item is added), or –1 to specify that a lookup dictionary is never created.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="dictionaryCreationThreshold" /> is less than –1.
    /// </exception>
    public ViewCollection(IEqualityComparer<string> comparer, int dictionaryCreationThreshold)
      : base(comparer, dictionaryCreationThreshold)
    {
    }

    /// <summary>Extracts the key from the specified element.</summary>
    /// <returns>The key for the specified element.</returns>
    /// <param name="item">The element from which to extract the key.</param>
    protected override string GetKeyForItem(IViewInfo item) => item.ViewName;
  }
}
