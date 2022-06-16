// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageSiteNodeCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;
using System.Web;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// 
  /// </summary>
  public class PageSiteNodeCollection : SiteMapNodeCollection
  {
    private bool overflowed;

    /// <summary>Retrieves a reference to an enumerator object, which is used to iterate
    /// over the collection. </summary>
    /// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" />.
    /// </returns>
    public override IEnumerator GetEnumerator() => (IEnumerator) new PageSiteNodeCollection.InnerEnumerator(this);

    public override SiteMapNode this[int index]
    {
      get
      {
        SiteMapNode siteMapNode = base[index];
        if (!(siteMapNode is PageSiteNode))
          return siteMapNode;
        PageSiteNode node = siteMapNode as PageSiteNode;
        if (this.UpdateNode(ref node))
          base[index] = (SiteMapNode) node;
        return (SiteMapNode) node;
      }
      set => base[index] = value;
    }

    /// <summary>
    /// Updates the sitemap node if it is stale. Returns <c>true</c> if the node is updated; otherwise <c>false</c>.
    /// </summary>
    /// <param name="node">The sitemap node.</param>
    /// <returns></returns>
    protected virtual bool UpdateNode(ref PageSiteNode node)
    {
      CompoundCacheDependency cacheDependency = node.CacheDependency;
      bool flag = false;
      for (int index = 0; index < cacheDependency.CacheDependencies.Count; ++index)
      {
        if (cacheDependency.CacheDependencies[index].HasExpired())
        {
          flag = true;
          break;
        }
      }
      if (!flag || !(((SiteMapBase) node.Provider).FindSiteMapNodeFromKey(node.Key, false) is PageSiteNode siteMapNodeFromKey))
        return false;
      node = siteMapNodeFromKey;
      return true;
    }

    /// <summary>
    /// Gets a value indicating whether this node has more child pages then the maximum allowed in the provider configuration.
    /// The default value for maximum number of children returned is 100.
    /// </summary>
    /// <value><c>true</c> if overflowed; otherwise, <c>false</c>.</value>
    public bool Overflowed
    {
      get => this.overflowed;
      internal set => this.overflowed = value;
    }

    protected class InnerEnumerator : IEnumerator
    {
      private PageSiteNodeCollection collection;
      private int currentIndex;

      public InnerEnumerator(PageSiteNodeCollection collection)
      {
        this.collection = collection;
        this.currentIndex = -1;
      }

      public object Current => (object) this.collection[this.currentIndex];

      public bool MoveNext()
      {
        if (this.currentIndex == this.collection.Count - 1)
          return false;
        ++this.currentIndex;
        return true;
      }

      public void Reset() => this.currentIndex = -1;
    }
  }
}
