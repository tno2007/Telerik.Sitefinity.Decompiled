// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.ViewDefinitionDictionary
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  internal class ViewDefinitionDictionary : DefinitionKeyedCollection<string, IViewDefinition>
  {
    public ViewDefinitionDictionary()
    {
    }

    public ViewDefinitionDictionary(IEnumerable<IViewDefinition> viewsConfigElement)
      : base(viewsConfigElement)
    {
    }

    /// <summary>
    /// When implemented in a derived class, extracts the key from the specified element.
    /// </summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override string GetKeyForItem(IViewDefinition item) => item.ViewName;

    /// <summary>
    /// Removes all elements from the <see cref="!:MasterViews" /> and <see cref="!:DetailViews" /> collections.
    /// </summary>
    protected override void ClearItems() => base.ClearItems();

    /// <summary>
    /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" /> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
    /// <param name="item">The object to insert.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="index" /> is less than 0.
    /// -or-
    /// <paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
    /// </exception>
    protected override void InsertItem(int index, IViewDefinition item)
    {
      string keyForItem = this.GetKeyForItem(item);
      if (this.Contains(keyForItem))
      {
        if (item is DefinitionBase definitionBase && definitionBase.ConfigDefinitionPath.IsNullOrEmpty())
          definitionBase.ConfigDefinitionPath = this[keyForItem].GetDefinition().ConfigDefinitionPath;
        this.Remove(keyForItem);
        --index;
      }
      base.InsertItem(index, item);
    }

    /// <summary>
    /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.
    /// </summary>
    /// <param name="index">The index of the element to remove.</param>
    protected override void RemoveItem(int index) => base.RemoveItem(index);

    /// <summary>
    /// Replaces the item at the specified index with the specified item.
    /// </summary>
    /// <param name="index">The zero-based index of the item to be replaced.</param>
    /// <param name="item">The new item.</param>
    protected override void SetItem(int index, IViewDefinition item) => base.SetItem(index, item);
  }
}
