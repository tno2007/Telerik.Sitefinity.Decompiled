// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PlaceHoldersCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Represents a collection of content place holders.</summary>
  public class PlaceHoldersCollection : KeyedCollection<string, Control>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PlaceHoldersCollection" /> class.
    /// </summary>
    public PlaceHoldersCollection()
      : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
    }

    /// <summary>
    /// When implemented in a derived class, extracts the key from the specified element.
    /// </summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override string GetKeyForItem(Control item) => item.ID;

    /// <summary>Gets the value associated with the specified key.</summary>
    /// <returns>true if the collection contains an element with the specified key; otherwise, false.
    /// </returns>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.
    /// </param>
    public bool TryGetValue(string key, out Control value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.Dictionary != null)
        return this.Dictionary.TryGetValue(key, out value);
      foreach (Control control in (IEnumerable<Control>) this.Items)
      {
        if (this.Comparer.Equals(this.GetKeyForItem(control), key))
        {
          value = control;
          return true;
        }
      }
      value = (Control) null;
      return false;
    }

    /// <summary>Adds the range.</summary>
    /// <param name="controls">The controls.</param>
    public void AddRange(IEnumerable<Control> controls)
    {
      if (controls == null)
        throw new ArgumentNullException(nameof (controls));
      foreach (Control control in controls)
        this.Add(control);
    }
  }
}
