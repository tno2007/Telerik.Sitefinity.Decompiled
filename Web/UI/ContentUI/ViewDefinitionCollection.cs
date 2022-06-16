// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ViewDefinitionCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// The class represents a list of objects implementing IContentViewDefinition.
  /// Each object contains common properties for constructing an actual view.
  /// </summary>
  public class ViewDefinitionCollection : DefinitionKeyedCollection<string, IContentViewDefinition>
  {
    private IList<IContentViewMasterDefinition> masterViews;
    private IList<IContentViewDetailDefinition> detailViews;

    public ViewDefinitionCollection()
    {
    }

    public ViewDefinitionCollection(
      IEnumerable<IContentViewDefinition> viewsConfigElement)
      : base(viewsConfigElement)
    {
    }

    internal IContentViewDefinition GetFirstMasterView() => this.Where<IContentViewDefinition>((Func<IContentViewDefinition, bool>) (v => v.IsMasterView)).FirstOrDefault<IContentViewDefinition>();

    internal IContentViewDefinition GetFirstDetailView() => this.Where<IContentViewDefinition>((Func<IContentViewDefinition, bool>) (v => !v.IsMasterView)).FirstOrDefault<IContentViewDefinition>();

    /// <summary>
    /// Gets the name of the first master view from <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.ViewDefinitionCollection.MasterViews" /> collection.
    /// </summary>
    /// <value>The name of the first master view.</value>
    public string FirstMasterViewName => this.GetFirstMasterView()?.ViewName;

    /// <summary>
    /// Gets the name of the first detail view from <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.ViewDefinitionCollection.DetailViews" /> collection.
    /// </summary>
    /// <value>The name of the first detail view.</value>
    public string FirstDetailViewName => this.GetFirstDetailView()?.ViewName;

    /// <summary>
    /// Determines whether a view with the specified view name is master view.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns>
    /// 	<c>true</c> if a view with the specified view name is master view; otherwise, <c>false</c>.
    /// </returns>
    public bool IsMasterView(string viewName)
    {
      IContentViewMasterDefinition masterDefinition = this.MasterViews.Where<IContentViewMasterDefinition>((Func<IContentViewMasterDefinition, bool>) (v => v.ViewName == viewName)).SingleOrDefault<IContentViewMasterDefinition>();
      return masterDefinition != null && typeof (IContentViewMasterDefinition).IsAssignableFrom(masterDefinition.GetType());
    }

    /// <summary>
    /// Determines whether a view with the specified view name is detail view.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns>
    /// 	<c>true</c> if a view with the specified view name is detail view; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDetailView(string viewName)
    {
      IContentViewDetailDefinition detailDefinition = this.DetailViews.Where<IContentViewDetailDefinition>((Func<IContentViewDetailDefinition, bool>) (v => v.ViewName == viewName)).SingleOrDefault<IContentViewDetailDefinition>();
      return detailDefinition != null && typeof (IContentViewDetailDefinition).IsAssignableFrom(detailDefinition.GetType());
    }

    /// <summary>
    /// When implemented in a derived class, extracts the key from the specified element.
    /// </summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override string GetKeyForItem(IContentViewDefinition item) => item.ViewName;

    /// <summary>
    /// Removes all elements from the <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.ViewDefinitionCollection.MasterViews" /> and <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.ViewDefinitionCollection.DetailViews" /> collections.
    /// </summary>
    protected override void ClearItems()
    {
      base.ClearItems();
      this.masterViews = (IList<IContentViewMasterDefinition>) null;
      this.detailViews = (IList<IContentViewDetailDefinition>) null;
    }

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
    protected override void InsertItem(int index, IContentViewDefinition item)
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
      this.masterViews = (IList<IContentViewMasterDefinition>) null;
      this.detailViews = (IList<IContentViewDetailDefinition>) null;
    }

    /// <summary>
    /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.KeyedCollection`2" />.
    /// </summary>
    /// <param name="index">The index of the element to remove.</param>
    protected override void RemoveItem(int index)
    {
      base.RemoveItem(index);
      this.masterViews = (IList<IContentViewMasterDefinition>) null;
      this.detailViews = (IList<IContentViewDetailDefinition>) null;
    }

    /// <summary>
    /// Replaces the item at the specified index with the specified item.
    /// </summary>
    /// <param name="index">The zero-based index of the item to be replaced.</param>
    /// <param name="item">The new item.</param>
    protected override void SetItem(int index, IContentViewDefinition item)
    {
      base.SetItem(index, item);
      this.masterViews = (IList<IContentViewMasterDefinition>) null;
      this.detailViews = (IList<IContentViewDetailDefinition>) null;
    }

    /// <summary>Gets the list of content master views.</summary>
    /// <value>The master views.</value>
    private IList<IContentViewMasterDefinition> MasterViews
    {
      get
      {
        if (this.masterViews == null)
        {
          this.masterViews = (IList<IContentViewMasterDefinition>) new List<IContentViewMasterDefinition>();
          foreach (IContentViewDefinition contentViewDefinition in (IEnumerable<IContentViewDefinition>) this.Items)
          {
            if (typeof (IContentViewMasterDefinition).IsAssignableFrom(contentViewDefinition.GetType()))
              this.masterViews.Add((IContentViewMasterDefinition) contentViewDefinition);
          }
        }
        return this.masterViews;
      }
    }

    /// <summary>Gets the list of content detail views.</summary>
    /// <value>The detail views.</value>
    private IList<IContentViewDetailDefinition> DetailViews
    {
      get
      {
        if (this.detailViews == null)
        {
          this.detailViews = (IList<IContentViewDetailDefinition>) new List<IContentViewDetailDefinition>();
          foreach (IContentViewDefinition contentViewDefinition in (IEnumerable<IContentViewDefinition>) this.Items)
          {
            if (typeof (IContentViewDetailDefinition).IsAssignableFrom(contentViewDefinition.GetType()))
              this.detailViews.Add((IContentViewDetailDefinition) contentViewDefinition);
          }
        }
        return this.detailViews;
      }
    }
  }
}
